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

namespace CADToolsGUI.Buttons
{
    /// <summary>
    /// Базовый класс для создания кнопок с изображением.
    /// </summary>
    public class BaseImageButton : Button
    {
        #region Поля, свойства

        /// <summary>
        /// Смещение границ кнопки относительно границ ее изображения.
        /// </summary>
        private const int IMAGE_OFFSET = 12;

        /// <summary>
        /// Получает или задает изображение, отображаемое в кнопке.
        /// </summary>
        public System.Drawing.Image Icon
        {
            get { return base.Image; }
            set
            {
                base.Image = value;
                UpdateButtonSize(value);
            }
        }

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        /// <param name="buttonName">Имя кнопки.</param>
        /// <param name="icon">Изображение на кнопке.</param>
        public BaseImageButton(string buttonName, System.Drawing.Image icon = null)
        {
            this.Font = GUIFonts.MainFont;
            this.Location = GUISizes.DefaultControlLocation;
            this.Name = buttonName;
            this.Icon = icon;
            this.Text = string.Empty;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Изменяет размер кнопки в соответствии с размерами ее изображения.
        /// </summary>
        /// <param name="icon">Изображение на кнопке.</param>
        private void UpdateButtonSize(System.Drawing.Image icon)
        {
            if (icon != null)
            {
                this.Size = new System.Drawing.Size(icon.Width + IMAGE_OFFSET, icon.Height + IMAGE_OFFSET);
            }
            else
            {
                this.Size = new System.Drawing.Size(GUISizes.BUTTON_WIDTH, GUISizes.BUTTON_HEIGHT);
            }
        }

        #endregion
    }
}
