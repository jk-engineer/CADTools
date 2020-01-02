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
using CADToolsGUI.Classes;
using CADToolsGUI.ListBoxes;

namespace CADToolsGUI.GroupBoxes
{
    /// <summary>
    /// Контейнер со списком документов и элементами управления.
    /// </summary>
    public class DocumentListGroupBox : GroupBox
    {
        #region Элементы интерфейса

        /// <summary>
        /// Список документов.
        /// </summary>
        private CheckedDocumentListBox _checkedDocumentListBox = new CheckedDocumentListBox()
        {
            Location = new System.Drawing.Point(GUISizes.HORIZONTAL_OFFSET, GUISizes.TOP_OFFSET_GROUPBOX)
        };

        /// <summary>
        /// Список документов.
        /// </summary>
        public CheckedDocumentListBox CheckedDocumentListBoxObject
        {
            get { return _checkedDocumentListBox; }
        }

        /// <summary>
        /// Кнопка установки/снятия флажков в списке документов.
        /// </summary>
        private SelectDeselectButton _selectDeselectButton = new SelectDeselectButton();

        /// <summary>
        /// Кнопка установки/снятия флажков в списке документов.
        /// </summary>
        public SelectDeselectButton SelectDeselectButtonObject
        {
            get { return _selectDeselectButton; }
        }

        /// <summary>
        /// Кнопка инверсии флажков в списке документов.
        /// </summary>
        private InvertButton _invertButton = new InvertButton();

        /// <summary>
        /// Кнопка инверсии флажков в списке документов.
        /// </summary>
        public InvertButton InvertButtonObject
        {
            get { return _invertButton; }
        }

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        /// <param name="caption">Заголовок контейнера.</param>
        public DocumentListGroupBox(string caption = "Документы:")
        {
            // Свойства контейнера.
            Font = GUIFonts.MainFont;
            Location = GUISizes.DefaultControlLocation;
            Name = "DocumentListGroupBox";
            Text = caption;
            // Размещение элементов в контейнере.
            Controls.AddRange(new Control[] { _checkedDocumentListBox, _selectDeselectButton, _invertButton });
            SizeChanged += DocumentListGroupBox_SizeChanged;
            Size = new System.Drawing.Size(250, 350);
        }

        #endregion

        #region Методы

        private void DocumentListGroupBox_SizeChanged(object sender, System.EventArgs e)
        {
            _checkedDocumentListBox.Width = Width - 2 * GUISizes.HORIZONTAL_OFFSET;
            _checkedDocumentListBox.Height = Height - _checkedDocumentListBox.Top -
                                              2 * GUISizes.VERTICAL_OFFSET - GUISizes.BUTTON_HEIGHT;
            GUIManager.PlaceControlsVertically(new Control[] { _checkedDocumentListBox, _selectDeselectButton }, GUISizes.VERTICAL_OFFSET);
            GUIManager.AlignControlsByTopBorder(new Control[] { _selectDeselectButton, _invertButton });
            GUIManager.AlignControlsByLeftBorder(new Control[] { _checkedDocumentListBox, _selectDeselectButton });
            GUIManager.AlignControlsByRightBorder(new Control[] { _checkedDocumentListBox, _invertButton });
        }

        #endregion
    }
}
