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
using CADToolsGUI.Buttons;
using CADToolsGUI.Classes;

namespace CADToolsGUI.Forms
{
    /// <summary>
    /// Форма для вывода отчета.
    /// </summary>
    public class ReportForm : Form
    {
        #region Элементы интерфейса

        /// <summary>
        /// Заголовок отчета.
        /// </summary>
        private Label _reportCaptionLabel = new Label()
        {
            AutoSize = true,
            Font = GUIFonts.MainFont,
            Location = GUISizes.DefaultControlLocation,
            Name = "CaptionLabel"
        };

        /// <summary>
        /// Поле с текстом отчета.
        /// </summary>
        private TextBox _reportTextBox = new TextBox()
        {
            Font = GUIFonts.MainFont,
            Location = GUISizes.DefaultControlLocation,
            Multiline = true,
            ScrollBars = ScrollBars.Both,
            Size = new System.Drawing.Size(360, 220),
            TabIndex = 0
        };

        /// <summary>
        /// Поле с текстом отчета.
        /// </summary>
        public TextBox ReportTextBox
        {
            get { return _reportTextBox; }
        }

        /// <summary>
        /// Кнопка "ОК".
        /// </summary>
        private OKButton _OKButton = new OKButton()
        {
            TabIndex = 1
        };

        /// <summary>
        /// Кнопка "ОК".
        /// </summary>
        public OKButton OKButtonObject
        {
            get { return _OKButton; }
        }

        #endregion

        #region Поля, свойства

        /// <summary>
        /// Заголовок отчета.
        /// </summary>
        public string ReportCaption
        {
            get { return _reportCaptionLabel.Text; }
            set { _reportCaptionLabel.Text = value; }
        }

        /// <summary>
        /// Текст отчета.
        /// </summary>
        public string ReportText
        {
            get { return _reportTextBox.Text; }
            set { _reportTextBox.Text = value; }
        }

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        public ReportForm()
        {
            // Свойства формы.
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Отчет";
            // Размещение элементов на форме.
            Control[] controlArray = { _reportCaptionLabel, _reportTextBox, _OKButton };
            this.Controls.AddRange(controlArray);
            GUIManager.PlaceControlsVertically(controlArray, GUISizes.VERTICAL_OFFSET);
            GUIManager.CenterControlsVertically(new Control[] { _reportTextBox, _OKButton });
            GUIManager.FitContainerSize(this, GUIManager.FitSizeMode.FitWidthAndHeight, GUISizes.HORIZONTAL_OFFSET, GUISizes.VERTICAL_OFFSET);
        }

        #endregion
    }
}
