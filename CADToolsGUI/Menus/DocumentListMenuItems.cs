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

namespace CADToolsGUI.Menus
{
    /// <summary>
    /// Элементы меню для списка документов.
    /// </summary>
    public static class DocumentListMenuItems
    {
        #region Элементы меню

        /// <summary>
        /// Открыть документы.
        /// </summary>
        static ToolStripMenuItem _openDocumentsItem = new ToolStripMenuItem()
        {
            Image = Properties.Resources.image_FolderOpen_16x16,
            Name = "OpenDocuments",
            ShortcutKeys = Keys.Control | Keys.O,
            ShowShortcutKeys = true,
            Text = "Открыть документы"
        };

        /// <summary>
        /// Открыть документы.
        /// </summary>
        public static ToolStripMenuItem OpenDocumentsItem
        {
            get { return _openDocumentsItem; }
        }

        /// <summary>
        /// Просмотр растрового изображения документа.
        /// </summary>
        static ToolStripMenuItem _previewDocumentItem = new ToolStripMenuItem()
        {
            Image = Properties.Resources.image_PrintPreviewHS_16x16,
            Name = "PreviewDocument",
            ShortcutKeys = Keys.F3,
            ShowShortcutKeys = true,
            Text = "Просмотр"
        };

        /// <summary>
        /// Просмотр растрового изображения документа.
        /// </summary>
        public static ToolStripMenuItem PreviewDocumentItem
        {
            get { return _previewDocumentItem; }
        }

        /// <summary>
        /// Открыть документы в CAD-системе.
        /// </summary>
        static ToolStripMenuItem _openDocumenstWithCADItem = new ToolStripMenuItem()
        {
            Image = Properties.Resources.image_RightArrowShort_Blue_16x16,
            Name = "OpenDocumentsWithCAD",
            Text = "Открыть в CAD"
        };

        /// <summary>
        /// Открыть документы в CAD-системе.
        /// </summary>
        public static ToolStripMenuItem OpenDocumentsWithCADItem
        {
            get { return _openDocumenstWithCADItem; }
        }

        /// <summary>
        /// Открыть документы в CAD-системе (расширенные варианты).
        /// </summary>
        static ToolStripMenuItem _openDocumentsWithCADExtendedItem = new ToolStripMenuItem()
        {
            Image = Properties.Resources.image_RightArrowShort_Orange_16x16,
            Name = "OpenDocumentsWithCADExtended",
            Text = "Открыть в CAD..."
        };

        /// <summary>
        /// Открыть документы в CAD-системе (расширенные варианты).
        /// </summary>
        public static ToolStripMenuItem OpenDocumentsWithCADExtendedItem
        {
            get { return _openDocumentsWithCADExtendedItem; }
        }

        /// <summary>
        /// Открыть в CAD-системе документы, отмеченные в списке.
        /// </summary>
        static ToolStripMenuItem _openCheckedDocumentsItem = new ToolStripMenuItem()
        {
            Name = "OpenCheckedDocuments",
            Text = "отмеченные флажком"
        };

        /// <summary>
        /// Открыть в CAD-системе документы, отмеченные в списке.
        /// </summary>
        public static ToolStripMenuItem OpenCheckedDocumentsItem
        {
            get { return _openCheckedDocumentsItem; }
        }

        /// <summary>
        /// Открыть в CAD-системе все документы из списка.
        /// </summary>
        static ToolStripMenuItem _openAllDocumentsItem = new ToolStripMenuItem()
        {
            Name = "OpenAllDocuments",
            Text = "все в списке"
        };

        /// <summary>
        /// Открыть в CAD-системе все документы из списка.
        /// </summary>
        public static ToolStripMenuItem OpenAllDocumentsItem
        {
            get { return _openAllDocumentsItem; }
        }

        /// <summary>
        /// Перезагрузить документ.
        /// </summary>
        static ToolStripMenuItem _reloadDocumentItem = new ToolStripMenuItem()
        {
            Image = Properties.Resources.image_RefreshArrow_Green_16x16,
            Name = "ReloadDocument",
            ShortcutKeys = Keys.Control | Keys.R,
            ShowShortcutKeys = true,
            Text = "Перезагрузить файл"
        };

        /// <summary>
        /// Перезагрузить документ.
        /// </summary>
        public static ToolStripMenuItem ReloadDocumentItem
        {
            get { return _reloadDocumentItem; }
        }

        /// <summary>
        /// Найти документ в списке.
        /// </summary>
        static ToolStripMenuItem _findDocumentItem = new ToolStripMenuItem()
        {
            Image = Properties.Resources.image_Search_Glyph_16x16,
            Name = "FindDocument",
            ShortcutKeys = Keys.Control | Keys.F,
            ShowShortcutKeys = true,
            Text = "Найти в списке"
        };

        /// <summary>
        /// Найти документ в списке.
        /// </summary>
        public static ToolStripMenuItem FindDocumentItem
        {
            get { return _findDocumentItem; }
        }

        /// <summary>
        /// Показать справку.
        /// </summary>
        static ToolStripMenuItem _helpItem = new ToolStripMenuItem()
        {
            Image = Properties.Resources.image_Help_16x16,
            Name = "Help",
            Text = "Справка"
        };

        /// <summary>
        /// Показать справку.
        /// </summary>
        public static ToolStripMenuItem HelpItem
        {
            get { return _helpItem; }
        }

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Статический конструктор класса.
        /// </summary>
        static DocumentListMenuItems()
        {
            _openDocumentsWithCADExtendedItem.DropDownItems.AddRange(new ToolStripMenuItem[]
                                                                    { _openCheckedDocumentsItem, _openAllDocumentsItem});
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает набор элеменов меню для списка документов.
        /// </summary>
        /// <returns></returns>
        public static ToolStripMenuItem[] GetDocumentListMenuItems()
        {
            ToolStripMenuItem[] resultValue = {
                _openDocumentsItem,
                _previewDocumentItem,
                _findDocumentItem,
                _openDocumenstWithCADItem,
                _openDocumentsWithCADExtendedItem,
                _reloadDocumentItem
            };
            return resultValue;
        }

        #endregion
    }
}
