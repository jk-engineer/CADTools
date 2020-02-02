#region Copyright
/*
This file is part of CADTools project.
Copyright (C) 2020 Evgeniy Ipatov

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see <https://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CADToolsCore.Printer
{
    /// <summary>
    /// Класс для работы с настройками принтера.
    /// </summary>
    public class PrinterConfig
    {
        #region Поля, функции для доступа к настройкам принтера

        private const int D_IN_BUFFER = 8;
        private const int D_IN_PROMT = 4;
        private const int D_OUT_BUFFER = 2;
        private const int F_MODE = 0;
        private const int ID_CANCEL = 2;

        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalLock(IntPtr handle);

        [DllImport("kernel32.dll")]
        private static extern bool GlobalUnlock(IntPtr hanle);

        [DllImport("kernel32.dll")]
        private static extern bool GlobalFree(IntPtr handle);

        [DllImport("winspool.drv", EntryPoint = "DocumentPropertiesW", SetLastError = true, ExactSpelling = true,
        CallingConvention = CallingConvention.StdCall)]
        private static extern int DocumentProperties(IntPtr hWnd, IntPtr hPrinter, [MarshalAs(UnmanagedType.LPWStr)] string pDeviceName,
                                                     IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);

        [DllImport("Gdi32.dll")]
        private static extern IntPtr CreateDC(IntPtr driver, string deviceName, IntPtr output, IntPtr devMode);

        #endregion

        #region Поля, свойства

        /// <summary>
        /// Структура DEVMODE принтера.
        /// </summary>
        private IntPtr _devModeData;

        /// <summary>
        /// Структура DEVMODE принтера.
        /// </summary>
        public IntPtr DevModeData { get => _devModeData; set => _devModeData = value; }

        /// <summary>
        /// Имя принтера по умолчанию.
        /// </summary>
        public string DefaultPrinterName
        {
            get
            {
                GetPrinterNames(out string _defaultPrinterName);
                return _defaultPrinterName;
            }
        }

        /// <summary>
        /// Настройки печати.
        /// </summary>
        public PrinterSettings PrinterSettings { get; private set; }

        #endregion

        #region События

        /// <summary>
        /// Происходит при изменении настроек принтера.
        /// </summary>
        public event Action PrinterSettingsChanged;

        /// <summary>
        /// Вызывает событие <see cref="PrinterSettingsChanged"/>.
        /// </summary>
        protected virtual void OnPrinterSettingsChanged() => PrinterSettingsChanged?.Invoke();

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        public PrinterConfig()
        {
            // Инициализация новых настроек печати.
            ResetPrinterSettings();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Вызывает окно настроек принтера.
        /// </summary>
        /// <param name="printerName">Имя принтера.</param>
        /// <param name="formHandle">Дескриптор формы.</param>
        public void ShowPrinterSettings(string printerName, IntPtr formHandle)
        {
            // Получение дескриптора принтера.
            _devModeData = GetDevMode(printerName, formHandle);
            IntPtr pDevMode = _devModeData;
            // Запуск диалогового окна настроек принтера.
            int returnCode = DocumentProperties(formHandle, IntPtr.Zero, printerName, _devModeData, pDevMode,
                                                D_IN_BUFFER | D_OUT_BUFFER | D_IN_PROMT);
            if (returnCode < 0)
            {
                MessageBox.Show("Не удалось получить доступ к настройкам принтера", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            // При нажатии кнопки "Отмена" окна свойств принтера все настройки сбрасываются.
            if (returnCode == ID_CANCEL)
            {
                ResetPrinterSettings();
                PrinterSettings.PrinterName = printerName;
            }
            // Сохранение новых настроек принтера.
            if (_devModeData != IntPtr.Zero)
            {
                PrinterSettings.SetHdevmode(_devModeData);
                PrinterSettings.DefaultPageSettings.SetHdevmode(_devModeData);
            }
            OnPrinterSettingsChanged();
        }

        /// <summary>
        /// Возвращает структуру DEVMODE принтера.
        /// </summary>
        /// <param name="printerName">Имя принтера.</param>
        /// <param name="formHandle">Дескриптор формы.</param>
        /// <returns></returns>
        public IntPtr GetDevMode(string printerName, IntPtr formHandle)
        {
            IntPtr resultValue;
            // Выделение памяти для хранения настроек принтера.
            PrinterSettings.PrinterName = printerName;
            IntPtr hDevMode = PrinterSettings.GetHdevmode(PrinterSettings.DefaultPageSettings);
            IntPtr pDevMode = GlobalLock(hDevMode);
            // Получение размера памяти, необходимой для хранения данных драйвера принтера.
            int bufferSize = DocumentProperties(formHandle, IntPtr.Zero, printerName, IntPtr.Zero, pDevMode, F_MODE);
            resultValue = bufferSize >= 0 ? Marshal.AllocHGlobal(bufferSize) : IntPtr.Zero;
            // Освобождение памяти.
            UnlockMemory(ref hDevMode);
            return resultValue;
        }

        /// <summary>
        /// Возвращает дескриптор указанного принтера.
        /// </summary>
        /// <param name="printerName">Имя принтера.</param>
        /// <returns></returns>
        public IntPtr GetPrinterHdc(string printerName) => CreateDC(IntPtr.Zero, printerName, IntPtr.Zero, _devModeData);

        /// <summary>
        /// Возвращает имена принтеров, установленных в системе.
        /// </summary>
        /// <param name="defaultPrinterName">Возвращает имя принтера по умолчанию.</param>
        /// <returns></returns>
        public string[] GetPrinterNames(out string defaultPrinterName)
        {
            PrinterSettings printSettings = new PrinterSettings();
            // Получение списка установленных принтеров.
            List<string> printerNames = new List<string>(PrinterSettings.InstalledPrinters.Cast<string>());
            printerNames.Sort();
            // Получение имени принтера по умолчанию.
            defaultPrinterName = printerNames.Where(pName =>
            {
                printSettings.PrinterName = pName;
                return printSettings.IsDefaultPrinter;
            }).FirstOrDefault();
            return printerNames.ToArray();
        }

        /// <summary>
        /// Выполняет сброс настроек печати.
        /// </summary>
        public void ResetPrinterSettings()
        {
            PrinterSettings = new PrinterSettings();
            UnlockMemory(ref _devModeData);
        }

        /// <summary>
        /// Снимает блокировку памяти, которая была выделена под данные.
        /// </summary>
        /// <param name="value">Данные в памяти.</param>
        private void UnlockMemory(ref IntPtr value)
        {
            GlobalUnlock(value);
            if (value != IntPtr.Zero)
            {
                GlobalFree(value);
                value = IntPtr.Zero;
            }
        }

        #endregion

        #region Финализация

        ~PrinterConfig() => UnlockMemory(ref _devModeData);

        #endregion
    }
}
