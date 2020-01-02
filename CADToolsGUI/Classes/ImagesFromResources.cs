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

using CADToolsGUI.Enumerators;

namespace CADToolsGUI.Classes
{
    /// <summary>
    /// Класс, обеспечивающий доступ к изображениям в ресурсах проекта.
    /// </summary>
    public static class ImagesFromResources
    {
        #region Методы

        /// <summary>
        /// Возвращает изображение из ресурсов решения.
        /// </summary>
        /// <param name="imageName">Название изображения (без указания размеров).</param>
        /// <param name="iconSize">Размер изображения на кнопке.</param>
        /// <returns></returns>
        public static System.Drawing.Bitmap GetImage(string imageName, IconSize.IconSizeEnum iconSize)
        {
            System.Drawing.Bitmap resultValue = null;
            string iconName = iconSize.ToString().Replace("Size", imageName);
            try
            {
                resultValue = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.
                                                     GetObject(iconName, Properties.Resources.Culture);
            }
            catch (System.Exception)
            {
            }
            return resultValue;
        }

        /// <summary>
        /// Возвращает изображение из ресурсов решения.
        /// </summary>
        /// <param name="imageName">Название изображения (без указания размеров).</param>
        /// <param name="iconColor">Цвет значка на кнопке.</param>
        /// <param name="iconSize">Размер изображения на кнопке.</param>
        /// <returns></returns>
        public static System.Drawing.Bitmap GetImage(string imageName, IconColor.IconColorEnum iconColor,
                                                     IconSize.IconSizeEnum iconSize)
        {
            System.Drawing.Bitmap resultValue = null;
            string iconName = imageName + "-" + iconSize.ToString().Replace("Size", iconColor.ToString().ToLower());
            try
            {
                resultValue = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.
                                                     GetObject(iconName, Properties.Resources.Culture);
            }
            catch (System.Exception)
            {
            }
            return resultValue;
        }

        #endregion
    }
}
