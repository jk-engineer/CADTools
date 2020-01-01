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

namespace CADToolsGUI.ListBoxes
{
    /// <summary>
    /// Элемент управления для отображения списка документов с возможностью расстановки "флажков".
    /// </summary>
    public class CheckedDocumentListBox : CheckedListBox
    {
        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        public CheckedDocumentListBox()
        {
            this.AllowDrop = true;
            this.IntegralHeight = false;
            this.Location = GUISizes.DefaultControlLocation;
            this.Name = "CheckedDocumentList";
            // Изменение поведения флажков.
            this.ItemCheck += CheckedDocumentListBox_ItemCheck;
        }

        #endregion

        #region Методы

        private void CheckedDocumentListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // При обработке события неверно считывается состояние CheckState элементов списка
            // (при установке флажка не выполняется изменение свойства Checked = True выбранного элемента списка).
            // Поэтому состояние CheckState задается вручную.
            var clb = (CheckedListBox)sender;
            clb.ItemCheck -= CheckedDocumentListBox_ItemCheck;
            clb.SetItemCheckState(e.Index, e.NewValue);
            clb.ItemCheck += CheckedDocumentListBox_ItemCheck;
        }

        #endregion
    }
}
