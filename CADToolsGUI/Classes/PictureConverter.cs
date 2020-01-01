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

using System.Drawing;
using stdole;
using System.Windows.Forms;

namespace CADToolsGUI.Classes
{
    /// <summary>
    /// Класс для конвертирования изображений.
    /// </summary>
    public class PictureConverter : AxHost
    {
        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        public PictureConverter() : base(string.Empty)
        {
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает объект <see cref="IPictureDisp"/>, полученный из объекта <see cref="Image"/>.
        /// </summary>
        /// <param name="image">Изображение <see cref="Image"/>.</param>
        /// <returns></returns>
        public static IPictureDisp ImageToPictureDisp(Image image)
        {
            IPictureDisp resultValue = null;
            try
            {
                resultValue = (IPictureDisp)GetIPictureDispFromPicture(image);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return resultValue;
        }

        /// <summary>
        /// Возвращает объект <see cref="Image"/>, полученный из объекта <see cref="IPictureDisp"/>.
        /// </summary>
        /// <param name="pictureDisp">Изображение <see cref="IPictureDisp"/>.</param>
        /// <returns></returns>
        public static Image PictureDispToImage(IPictureDisp pictureDisp)
        {
            Image resultValue = null;
            try
            {
                resultValue = GetPictureFromIPictureDisp(pictureDisp);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return resultValue;
        }

        #endregion
    }
}
