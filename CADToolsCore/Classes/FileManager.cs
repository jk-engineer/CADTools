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
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
#endregion

using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace CADToolsCore.Classes
{
    /// <summary>
    /// Класс для работы с файлами, директориями.
    /// </summary>
    public static class FileManager
    {
        #region Методы

        /// <summary>
        /// Возвращает <see cref="true"/> при существовании указанного файла. В случае отсутствия файл может быть создан заново.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <param name="createFile">Создать файл при его отсутствии (создается файл текстового типа).</param>
        /// <returns></returns>
        public static bool CheckFileExist(string fullFileName, bool createFile)
        {
            bool resultValue = System.IO.File.Exists(fullFileName);
            string fileName = System.IO.Path.GetFileName(fullFileName);
            if (!resultValue & createFile)
            {
                MessageBox.Show("Файл " + fileName + " не найден и будет создан заново", "Ошибка", MessageBoxButtons.OK);
                try
                {
                    System.IO.File.WriteAllText(fullFileName, string.Empty);
                }
                catch (System.Exception)
                {
                    ShowCreateFileError(fileName);
                }
            }
            return resultValue;
        }

        /// <summary>
        /// Возвращает полное имя файла, находящегося в одной директории с вызывающей сборкой.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <returns></returns>
        public static string GetFullFileNameFromCallingAssemblyLocation(string fileName)
        {
            string callingAssemblyPath = System.IO.Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            return System.IO.Path.Combine(new string[] { callingAssemblyPath, fileName });
        }

        /// <summary>
        /// Возвращает <see cref="true"/> при успешном запуске указанного файла.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <returns></returns>
        public static bool RunFile(string fullFileName)
        {
            bool resultValue = false;
            try
            {
                Process.Start(fullFileName);
                resultValue = true;
            }
            catch (System.Exception)
            {
                ShowOpenFileError(System.IO.Path.GetFileName(fullFileName));
            }
            return resultValue;
        }

        /// <summary>
        /// Возвращает <see cref="true"/> при успешном запуске указанного файла
        /// (файл должен находиться в одной директории со сборкой, вызывающей данный метод).
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <returns></returns>
        public static bool RunFileFromCallingAssemblyLocation(string fileName)
        {
            return RunFile(GetFullFileNameFromCallingAssemblyLocation(fileName));
        }

        /// <summary>
        /// Выводит сообщение об ошибке создания файла.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        public static void ShowCreateFileError(string fileName)
        {
            MessageBox.Show("Не удалось создать файл " + fileName, "Ошибка", MessageBoxButtons.OK);
        }

        /// <summary>
        /// Выводит сообщение об ошибке удаления файла.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        public static void ShowDeleteFileError(string fileName)
        {
            MessageBox.Show("Не удалось удалить файл " + fileName, "Ошибка", MessageBoxButtons.OK);
        }

        /// <summary>
        /// Выводит сообщение об ошибке открытия файла.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        public static void ShowOpenFileError(string fileName)
        {
            MessageBox.Show("Не удалось открыть файл " + fileName, "Ошибка", MessageBoxButtons.OK);
        }

        /// <summary>
        /// Выводит сообщение об ошибке сохранения файла.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        public static void ShowSaveFileError(string fileName)
        {
            MessageBox.Show("Не удалось сохранить файл " + fileName, "Ошибка", MessageBoxButtons.OK);
        }

        #endregion
    }
}
