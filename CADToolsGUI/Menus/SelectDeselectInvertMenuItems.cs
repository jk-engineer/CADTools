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
using CADToolsGUI.Enumerators;

namespace CADToolsGUI.Menus
{
    /// <summary>
    /// Элементы меню для установки, снятия и инверсии выделения в списке или состояния флажков.
    /// </summary>
    public static class SelectDeselectInvertMenuItems
    {
        #region Элементы меню

        /// <summary>
        /// Выбрать все.
        /// </summary>
        static ToolStripMenuItem _selectAllItem = new ToolStripMenuItem()
        {
            Name = "SelectAll",
            Text = "Выбрать все"
        };

        /// <summary>
        /// Выбрать все.
        /// </summary>
        public static ToolStripMenuItem SelectAllItem
        {
            get { return _selectAllItem; }
        }

        /// <summary>
        /// Снять выбор.
        /// </summary>
        static ToolStripMenuItem _deselectAllItem = new ToolStripMenuItem()
        {
            Name = "DeselectAll",
            Text = "Снять выбор"
        };

        /// <summary>
        /// Снять выбор.
        /// </summary>
        public static ToolStripMenuItem DeselectAllItem
        {
            get { return _deselectAllItem; }
        }

        /// <summary>
        /// Инверсия.
        /// </summary>
        static ToolStripMenuItem _invertItem = new ToolStripMenuItem()
        {
            Name = "Invert",
            Text = "Инверсия"
        };

        /// <summary>
        /// Инверсия.
        /// </summary>
        public static ToolStripMenuItem InvertItem
        {
            get { return _invertItem; }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает набор элементов меню для установки, снятия и инверсии выделения в списке или состояния флажков.
        /// </summary>
        /// <param name="iconColor">Цвет значков элементов меню (доступны голубой и красный цвета)</param>
        /// <returns></returns>
        public static ToolStripMenuItem[] GetSelectDeselectInvertMenuItems(IconColor.IconColorEnum iconColor)
        {
            switch (iconColor)
            {
                case IconColor.IconColorEnum.Blue:
                    _selectAllItem.Image = Properties.Resources.image_SetSelect_Blue_16x16;
                    _deselectAllItem.Image = Properties.Resources.image_Deselect_Blue_16x16;
                    _invertItem.Image = Properties.Resources.image_Invert_Blue_16x16;
                    break;
                case IconColor.IconColorEnum.Red:
                    _selectAllItem.Image = Properties.Resources.image_SetSelect_Red_16x16;
                    _deselectAllItem.Image = Properties.Resources.image_Deselect_Red_16x16;
                    _invertItem.Image = Properties.Resources.image_Invert_Red_16x16;
                    break;
            }
            ToolStripMenuItem[] resultValue = {
                _selectAllItem,
                _deselectAllItem,
                _invertItem
            };
            return resultValue;
        }

        #endregion
    }
}
