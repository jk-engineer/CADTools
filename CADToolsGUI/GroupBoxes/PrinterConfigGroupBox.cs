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

using System.Windows.Forms;
using CADToolsGUI.Buttons;
using CADToolsGUI.Classes;
using CADToolsGUI.Enumerators;

namespace CADToolsGUI.GroupBoxes
{
    /// <summary>
    /// Контейнер со средствами настройки принтера.
    /// </summary>
    public class PrinterConfigGroupBox : GroupBox
    {
        #region Поля, свойства

        /// <summary>
        /// Название изображения.
        /// </summary>
        private const string IMAGE_NAME = "categories-applications-system";

        #endregion

        #region Элементы интерфейса

        /// <summary>
        /// Список принтеров.
        /// </summary>
        private ComboBox _printersComboBox = new ComboBox()
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = GUIFonts.MainFont,
            Location = GUISizes.DefaultControlLocation,
            Name = "PrintersComboBox",
            TabIndex = 0
        };

        /// <summary>
        /// Список принтеров.
        /// </summary>
        public ComboBox PrintersComboBox
        {
            get { return _printersComboBox; }
        }

        /// <summary>
        /// Кнопка настройки выбранного принтера.
        /// </summary>
        private BaseImageButton _printerConfigButton =
            new BaseImageButton("Свойства", ImagesFromResources.GetImage("PrinterConfigButton", IconSize.IconSizeEnum.Size_16x16))
            {
                TabIndex = 1
            };

        /// <summary>
        /// Кнопка настройки выбранного принтера.
        /// </summary>
        public BaseImageButton PrinterConfigButton
        {
            get { return _printerConfigButton; }
        }

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        /// <param name="caption">Заголовок контейнера.</param>
        public PrinterConfigGroupBox(string caption = "Принтеры:")
        {
            // Свойства контейнера.
            this.Font = GUIFonts.MainFont;
            this.Location = GUISizes.DefaultControlLocation;
            this.Name = "PrinterConfigGroupBox";
            this.Text = caption;
            // Размещение элементов в контейнере.
            this.Controls.AddRange(new Control[] { _printersComboBox, _printerConfigButton });
            this.SizeChanged += PrinterConfigGroupBox_SizeChanged;
            this.Size = new System.Drawing.Size(350, 54);
        }

        #endregion

        #region Методы

        private void PrinterConfigGroupBox_SizeChanged(object sender, System.EventArgs e)
        {
            _printersComboBox.Size = new System.Drawing.Size(this.Width - _printerConfigButton.Width -
                                                              GUISizes.HORIZONTAL_OFFSET * 3, GUISizes.COMBOBOX_HEIGHT);
            Control[] controlArray = { _printersComboBox, _printerConfigButton };
            GUIManager.PlaceControlsHorizontally(controlArray, GUISizes.HORIZONTAL_OFFSET);
            GUIManager.CenterControlVerticallyByContainer(controlArray);
        }

        #endregion
    }
}
