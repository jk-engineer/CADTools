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

namespace CADToolsGUI.GroupBoxes
{
    /// <summary>
    /// Контейнер для отображения счетчиков форматов чертежей.
    /// </summary>
    public class DrawingSheetSizeCountGroupBox : GroupBox
    {
        #region Элементы интерфейса

        /// <summary>
        /// Список счетчиков форматов чертежей.
        /// </summary>
        private ListBox _drawingSheetSizeCountListBox = new ListBox()
        {
            Font = GUIFonts.MainFont,
            Location = new System.Drawing.Point(GUISizes.HORIZONTAL_OFFSET, GUISizes.TOP_OFFSET_GROUPBOX),
            SelectionMode = SelectionMode.MultiExtended
        };

        /// <summary>
        /// Список счетчиков форматов чертежей.
        /// </summary>
        public ListBox DrawingSheetSizeCountListBox
        {
            get { return _drawingSheetSizeCountListBox; }
        }

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        /// <param name="caption">Заголовок контейнера.</param>
        public DrawingSheetSizeCountGroupBox(string caption = "Форматы:")
        {
            // Свойства контейнера.
            Font = GUIFonts.MainFont;
            Location = GUISizes.DefaultControlLocation;
            Name = "DrawingSheetSizeCountGroupBox";
            Text = caption;
            // Размещение элементов в контейнере.
            Controls.Add(_drawingSheetSizeCountListBox);
            SizeChanged += DrawingSheetSizeCountGroupBox_SizeChanged;
            Size = new System.Drawing.Size(100, 200);
        }

        #endregion

        #region Методы

        private void DrawingSheetSizeCountGroupBox_SizeChanged(object sender, System.EventArgs e)
        {
            _drawingSheetSizeCountListBox.Width = Width - 2 * GUISizes.HORIZONTAL_OFFSET;
            _drawingSheetSizeCountListBox.Height = Height - GUISizes.TOP_OFFSET_GROUPBOX - GUISizes.VERTICAL_OFFSET;
            GUIManager.CenterControlByContainer(_drawingSheetSizeCountListBox);
        }

        #endregion
    }
}
