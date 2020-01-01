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

using System.Windows.Forms;
using CADToolsGUI.Classes;

namespace CADToolsGUI.Forms
{
    /// <summary>
    /// Форма для просмотра растрового изображения документа.
    /// </summary>
    public class PreviewDocumentForm : Form
    {
        #region Элементы интерфейса

        /// <summary>
        /// Изображение документа на форме.
        /// </summary>
        private PictureBox _previewBox = new PictureBox()
        {
            BorderStyle = BorderStyle.FixedSingle,
            Location = GUISizes.DefaultControlLocation,
            Name = "PreviewBox",
            Size = new System.Drawing.Size(360, 350),
            SizeMode = PictureBoxSizeMode.StretchImage
        };

        #endregion

        #region Поля, свойства

        /// <summary>
        /// Растровое изображение документа.
        /// </summary>
        public System.Drawing.Image DocumentThumbnail
        {
            get { return _previewBox.Image; }
            set { _previewBox.Image = value; }
        }

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        public PreviewDocumentForm()
        {
            // Свойства формы.
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreviewForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Просмотр";
            // Размещение элеметов на форме.
            this.Controls.Add(_previewBox);
            GUIManager.FitContainerSize(this, GUIManager.FitSizeMode.FitWidthAndHeight, GUISizes.HORIZONTAL_OFFSET, GUISizes.VERTICAL_OFFSET - 4);
        }

        #endregion
    }
}
