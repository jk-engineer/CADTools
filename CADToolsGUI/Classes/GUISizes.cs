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

namespace CADToolsGUI.Classes
{
    /// <summary>
    /// Класс, содержащий размеры элементов интерфейса, отступы и т.д.
    /// </summary>
    public static class GUISizes
    {
        #region Поля, свойства

        /// <summary>
        /// Ширина кнопки.
        /// </summary>
        public const int BUTTON_WIDTH = 100;

        /// <summary>
        /// Высота кнопки.
        /// </summary>
        public const int BUTTON_HEIGHT = 28;

        /// <summary>
        /// Ширина раскрывающегося списка.
        /// </summary>
        public const int COMBOBOX_WIDTH = 100;

        /// <summary>
        /// Высота выпадающего списка.
        /// </summary>
        public const int COMBOBOX_HEIGHT = BUTTON_HEIGHT - 2;

        /// <summary>
        /// Поправка отступа элемента интерфейса по горизонтали.
        /// <para>Применяется для получения равного отступа элемента от левой и правой границ формы.</para>
        /// </summary>
        public const int LEFT_CORRECTION = 16;

        /// <summary>
        /// Поправка отступа элемента интерфейса по вертикали.
        /// <para>Применяется для получения равного отступа элемента от верхней и нижней границ формы.</para>
        /// </summary>
        public const int TOP_CORRECTION = 38;

        /// <summary>
        /// Расстояние по горизонтали между соседними элементами.
        /// </summary>
        public const int HORIZONTAL_OFFSET = 10;

        /// <summary>
        /// Расстояние по вертикали между соседними элементами.
        /// </summary>
        public const int VERTICAL_OFFSET = 10;

        /// <summary>
        /// Расстояние от верхней границы контейнера <see cref="GroupBox"/> до верхней границы элемента интерфейса
        /// внутри этого контейнера.
        /// </summary>
        public const int TOP_OFFSET_GROUPBOX = VERTICAL_OFFSET + 6;

        /// <summary>
        /// Расстояние от нижней границы формы до нижней границы ближайшего элемента интерфейса.
        /// </summary>
        public const int BOTTO_OFFSET_FORM = VERTICAL_OFFSET - 6;

        /// <summary>
        /// Начальная точка расположения элемента интерфейса.
        /// </summary>
        public static System.Drawing.Point DefaultControlLocation
        {
            get { return new System.Drawing.Point(HORIZONTAL_OFFSET, VERTICAL_OFFSET); }
        }

        #endregion
    }
}
