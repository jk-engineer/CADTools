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
    /// Контейнер элементов для выбора источника документов.
    /// </summary>
    public class DocumentSourceGroupBox : GroupBox
    {
        #region Поля, свойства

        /// <summary>
        /// Название изображения.
        /// </summary>
        private const string UPDATE_IMAGE_NAME = "actions-view-refresh";

        /// <summary>
        /// Название изображения.
        /// </summary>
        private const string OPEN_FOLDER_IMAGE_NAME = "actions-document-open-folder";

        /// <summary>
        /// Расстояние от левой границы контейнера до кнопок.
        /// </summary>
        private const int BUTTON_LEFT_OFFSET = 94;

        /// <summary>
        /// Высота элемента интерфейса <see cref="RadioButton"/>.
        /// </summary>
        private const int RADIO_BUTTON_HEIGHT = 20;

        /// <summary>
        /// Ширина элемента интерфейса <see cref="RadioButton"/>.
        /// </summary>
        private const int RADIO_BUTTON_WIDTH = 85;

        #endregion

        #region Элементы интерфейса

        /// <summary>
        /// Переключатель на режим чтения документов из CAD-системы.
        /// </summary>
        private RadioButton _cadSourceRadioButton = new RadioButton()
        {
            Font = GUIFonts.MainFont,
            Location = GUISizes.DefaultControlLocation,
            Name = "CADSourceRadioButton",
            Size = new System.Drawing.Size(RADIO_BUTTON_WIDTH, RADIO_BUTTON_HEIGHT),
            TabIndex = 0,
            Text = "CAD-система"
        };

        /// <summary>
        /// Переключатель на режим чтения документов из CAD-системы.
        /// </summary>
        public RadioButton CADSourceRadioButton
        {
            get { return _cadSourceRadioButton; }
        }

        /// <summary>
        /// Переключатель на режим чтения документов из указанных файлов.
        /// </summary>
        private RadioButton _filesSourceRadioButton = new RadioButton()
        {
            Font = GUIFonts.MainFont,
            Location = GUISizes.DefaultControlLocation,
            Name = "FilesSourceRadioButton",
            Size = new System.Drawing.Size(RADIO_BUTTON_WIDTH, RADIO_BUTTON_HEIGHT),
            TabIndex = 1,
            Text = "Открыть"
        };

        /// <summary>
        /// Переключатель на режим чтения документов из указанных файлов.
        /// </summary>
        public RadioButton FilesSourceRadioButton
        {
            get { return _filesSourceRadioButton; }
        }

        /// <summary>
        /// Кнопка повторного считывания документов из CAD-системы.
        /// </summary>
        private BaseImageButton _updateButton =
            new BaseImageButton("UpdateButton", ImagesFromResources.GetImage(UPDATE_IMAGE_NAME, IconSize.IconSizeEnum.Size_16x16))
            {
                Location = new System.Drawing.Point(BUTTON_LEFT_OFFSET, GUISizes.VERTICAL_OFFSET),
                TabIndex = 2
            };

        /// <summary>
        /// Кнопка повторного считывания документов из CAD-системы.
        /// </summary>
        public BaseImageButton UpdateButtonObject
        {
            get { return _updateButton; }
        }

        /// <summary>
        /// Кнопка запуска диалога открытия документов.
        /// </summary>
        private BaseImageButton _openDocumentsButton =
            new BaseImageButton("OpenFolderButton", ImagesFromResources.GetImage(OPEN_FOLDER_IMAGE_NAME, IconSize.IconSizeEnum.Size_16x16))
            {
                Location = new System.Drawing.Point(BUTTON_LEFT_OFFSET, GUISizes.VERTICAL_OFFSET),
                TabIndex = 3
            };

        /// <summary>
        /// Кнопка запуска диалога открытия документов.
        /// </summary>
        public BaseImageButton OpenDocumentsButtonObject
        {
            get { return _openDocumentsButton; }
        }

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        /// <param name="caption">Заголовок контейнера.</param>
        public DocumentSourceGroupBox(string caption = "Источник:")
        {
            // Свойства контейнера.
            this.Font = GUIFonts.MainFont;
            this.Location = GUISizes.DefaultControlLocation;
            this.Name = "DocumentSourceGroupBox";
            this.Text = caption;
            // Размещение элементов в контейнере.
            this.Controls.AddRange(new Control[] { _cadSourceRadioButton, _filesSourceRadioButton,
                                                   _updateButton, _openDocumentsButton });
            GUIManager.PlaceControlsVertically(new Control[] { _updateButton, _openDocumentsButton }, GUISizes.VERTICAL_OFFSET - 6);
            GUIManager.CenterControlsHorizontally(new Control[] { _updateButton, _cadSourceRadioButton });
            GUIManager.CenterControlsHorizontally(new Control[] { _openDocumentsButton, _filesSourceRadioButton });
            GUIManager.FitContainerSize(this, GUIManager.FitSizeMode.FitWidthAndHeight, GUISizes.HORIZONTAL_OFFSET, GUISizes.VERTICAL_OFFSET);
        }

        #endregion
    }
}
