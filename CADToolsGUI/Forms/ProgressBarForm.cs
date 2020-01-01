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

namespace CADToolsGUI.Forms
{
    /// <summary>
    /// Форма с индикатором выполнения операции.
    /// </summary>
    public class ProgressBarForm : Form
    {
        #region Элементы интерфейса

        /// <summary>
        /// Индикатор прогресса выполнения операции.
        /// </summary>
        private ProgressBar _progressBar = new ProgressBar()
        {
            Location = GUISizes.DefaultControlLocation,
            Minimum = 0,
            Maximum = 100,
            Name = "ProgressBar",
            Size = new System.Drawing.Size(160, GUISizes.BUTTON_HEIGHT),
            Style = ProgressBarStyle.Continuous
        };

        /// <summary>
        /// Индикатор прогресса выполнения операции.
        /// </summary>
        public ProgressBar ProgressBarObject
        {
            get { return _progressBar; }
        }

        /// <summary>
        /// Кнопка отмены операции.
        /// </summary>
        private CancelButton _cancelButton = new CancelButton()
        {
            TabIndex = 0
        };

        /// <summary>
        /// Кнопка отмены операции.
        /// </summary>
        public CancelButton CancelButtonObject
        {
            get { return _cancelButton; }
        }

        #endregion

        #region Поля, свойства

        /// <summary>
        /// Возвращает или задает текущее положение индикатора выполнения операции.
        /// </summary>
        public int ProgressPercentage
        {
            get { return _progressBar.Value; }
            set { _progressBar.Value = value; }
        }

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        /// <param name="caption">Заголовок формы.</param>
        /// <param name="cancelEnabled">Доступ к кнопке "Отмена"</param>
        public ProgressBarForm(string caption, bool cancelEnabled)
        {
            // Свойства формы.
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressBarForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = caption;
            // Свойства элементов.
            _cancelButton.Enabled = cancelEnabled;
            // Размещение элементов на форме.
            Control[] controlArray = { _progressBar, _cancelButton };
            this.Controls.AddRange(controlArray);
            GUIManager.PlaceControlsVertically(controlArray, GUISizes.VERTICAL_OFFSET);
            GUIManager.CenterControlsVertically(controlArray);
            GUIManager.FitContainerSize(this, GUIManager.FitSizeMode.FitWidthAndHeight, GUISizes.HORIZONTAL_OFFSET, GUISizes.VERTICAL_OFFSET);
        }

        #endregion
    }
}
