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

using System.Linq;
using System.Windows.Forms;
using CADToolsGUI.Buttons;
using CADToolsGUI.Classes;
using CADToolsGUI.Enumerators;

namespace CADToolsGUI.GroupBoxes
{
    /// <summary>
    /// Контейнер для редактирования табличных данных.
    /// </summary>
    public class TableEditGroupBox : GroupBox
    {
        #region Поля, свойства

        private const string SAVE_IMAGE_NAME = "actions-document-save";

        private const string CANCEL_IMAGE_NAME = "actions-dialog-cancel";

        private const string MOVE_UP_IMAGE_NAME = "actions-arrow-up";

        private const string MOVE_DOWN_IMAGE_NAME = "actions-arrow-down";

        private const string MOVE_TO_BEGIN_IMAGE_NAME = "actions-arrow-up-double";

        private const string MOVE_TO_END_IMAGE_NAME = "actions-arrow-down-double";

        private const string ADD_IMAGE_NAME = "actions-list-add";

        private const string DELETE_IMAGE_NAME = "actions-edit-delete";

        private const string SORT_IMAGE_NAME = "actions-view-sort-ascending";

        private const string REVERSE_IMAGE_NAME = "actions-view-sort-descending";

        private const string UPDATE_IMAGE_NAME = "actions-view-refresh";

        /// <summary>
        /// Количество столбцов кнопок редактирования таблицы.
        /// </summary>
        private const int BUTTONS_COLUMN_COUNT = 5;

        #endregion

        #region Элементы интерфейса

        /// <summary>
        /// Таблица данных.
        /// </summary>
        private DataGridView _dataGridViewObject = new DataGridView()
        {
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToOrderColumns = false,
            AllowUserToResizeColumns = false,
            AllowUserToResizeRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
            Location = GUISizes.DefaultControlLocation,
            Name = "DataTable",
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing,
            Size = new System.Drawing.Size(250, 250),
            TabIndex = 0
        };

        /// <summary>
        /// Таблица данных.
        /// </summary>
        public DataGridView DataGridViewObject
        {
            get { return _dataGridViewObject; }
        }

        /// <summary>
        /// Контейнер с кнопками редактирования таблицы.
        /// </summary>
        private GroupBox _editTableGroupBox = new GroupBox()
        {
            TabIndex = 1,
            Text = "Редактирование таблицы"
        };

        /// <summary>
        /// Контейнер с кнопками редактирования таблицы.
        /// </summary>
        public GroupBox EditTableGroupBox
        {
            get { return _editTableGroupBox; }
        }

        /// <summary>
        /// Кнопка сохранения внесенных изменений.
        /// </summary>
        private BaseImageButton _saveChangesButton =
            new BaseImageButton("SaveChangesButton", ImagesFromResources.GetImage(SAVE_IMAGE_NAME, IconSize.IconSizeEnum.Size_48x48))
            {
                TabIndex = 2
            };

        /// <summary>
        /// Кнопка сохранения внесенных изменений.
        /// </summary>
        public BaseImageButton SaveChangesButton
        {
            get { return _saveChangesButton; }
        }

        /// <summary>
        /// Кнопка отмены.
        /// </summary>
        private BaseImageButton _cancelButtonObject =
            new BaseImageButton("CancelButton", ImagesFromResources.GetImage(CANCEL_IMAGE_NAME, IconSize.IconSizeEnum.Size_48x48))
            {
                TabIndex = 3
            };

        /// <summary>
        /// Кнопка отмены.
        /// </summary>
        public BaseImageButton CancelButtonObject
        {
            get { return _cancelButtonObject; }
        }

        /// <summary>
        /// Кнопка перемещения строки вверх.
        /// </summary>
        private BaseImageButton _moveRowsUpButton =
            new BaseImageButton("MoveRowsUpButton", ImagesFromResources.GetImage(MOVE_UP_IMAGE_NAME, IconSize.IconSizeEnum.Size_16x16))
            {
                Location = GUISizes.DefaultControlLocation
            };

        /// <summary>
        /// Кнопка перемещения строки вверх.
        /// </summary>
        public BaseImageButton MoveRowsUpButton
        {
            get { return _moveRowsUpButton; }
        }

        /// <summary>
        /// Кнопка перемещения строки вниз.
        /// </summary>
        private BaseImageButton _moveRowsDownButton =
            new BaseImageButton("MoveRowsDownButton", ImagesFromResources.GetImage(MOVE_DOWN_IMAGE_NAME, IconSize.IconSizeEnum.Size_16x16));

        /// <summary>
        /// Кнопка перемещения строки вниз.
        /// </summary>
        public BaseImageButton MoveRowsDownButton
        {
            get { return _moveRowsDownButton; }
        }

        /// <summary>
        /// Кнопка перемещения строки в начало таблицы.
        /// </summary>
        private BaseImageButton _moveRowsToBeginButton =
            new BaseImageButton("MoveRowsToBeginButton", ImagesFromResources.GetImage(MOVE_TO_BEGIN_IMAGE_NAME, IconSize.IconSizeEnum.Size_16x16));

        /// <summary>
        /// Кнопка перемещения строки в начало таблицы.
        /// </summary>
        public BaseImageButton MoveRowsToBeginButton
        {
            get { return _moveRowsToBeginButton; }
        }

        /// <summary>
        /// Кнопка перемещения строки в конец таблицы.
        /// </summary>
        private BaseImageButton _moveRowsToEndButton =
            new BaseImageButton("MoveRowsToEndButton", ImagesFromResources.GetImage(MOVE_TO_END_IMAGE_NAME, IconSize.IconSizeEnum.Size_16x16));

        /// <summary>
        /// Кнопка перемещения строки в конец таблицы.
        /// </summary>
        public BaseImageButton MoveRowsToEndButton
        {
            get { return _moveRowsToEndButton; }
        }

        /// <summary>
        /// Кнопка добавления строки в таблицу.
        /// </summary>
        private BaseImageButton _addRowsButton =
            new BaseImageButton("AddRowsButton", ImagesFromResources.GetImage(ADD_IMAGE_NAME, IconSize.IconSizeEnum.Size_16x16));

        /// <summary>
        /// Кнопка добавления строки в таблицу.
        /// </summary>
        public BaseImageButton AddRowsButton
        {
            get { return _addRowsButton; }
        }

        /// <summary>
        /// Кнопка удаления строки из таблицы.
        /// </summary>
        private BaseImageButton _deleteRowsButton =
            new BaseImageButton("DeleteRowsButton", ImagesFromResources.GetImage(DELETE_IMAGE_NAME, IconSize.IconSizeEnum.Size_16x16));

        /// <summary>
        /// Кнопка удаления строки из таблицы.
        /// </summary>
        public BaseImageButton DeleteRowsButton
        {
            get { return _deleteRowsButton; }
        }

        /// <summary>
        /// Кнопка сортировки таблицы.
        /// </summary>
        private BaseImageButton _sortRowsButton =
            new BaseImageButton("SortRowsButton", ImagesFromResources.GetImage(SORT_IMAGE_NAME, IconSize.IconSizeEnum.Size_16x16));

        /// <summary>
        /// Кнопка сортировки таблицы.
        /// </summary>
        public BaseImageButton SortRowsButton
        {
            get { return _sortRowsButton; }
        }

        /// <summary>
        /// Кнопка реверсирования строк в таблице.
        /// </summary>
        private BaseImageButton _reverseRowsButton =
            new BaseImageButton("ReverseRowsButton", ImagesFromResources.GetImage(REVERSE_IMAGE_NAME, IconSize.IconSizeEnum.Size_16x16));

        /// <summary>
        /// Кнопка реверсирования строк в таблице.
        /// </summary>
        public BaseImageButton ReverseRowsButton
        {
            get { return _reverseRowsButton; }
        }

        /// <summary>
        /// Кнопка повторного считывания данных в таблицу.
        /// </summary>
        private BaseImageButton _updateTableButton =
            new BaseImageButton("UpdageButton", ImagesFromResources.GetImage(UPDATE_IMAGE_NAME, IconSize.IconSizeEnum.Size_16x16));

        /// <summary>
        /// Кнопка повторного считывания данных в таблицу.
        /// </summary>
        public BaseImageButton UpdateTableButton
        {
            get { return _updateTableButton; }
        }

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        /// <param name="caption">Заголовок контейнера.</param>
        public TableEditGroupBox(string caption = "Редактор:")
        {
            // Свойства контейнера.
            Font = GUIFonts.MainFont;
            Location = GUISizes.DefaultControlLocation;
            Name = "TableEditGroupBox";
            Text = caption;
            // Свойства элементов.
            BaseImageButton[] buttonArray = {
                _moveRowsUpButton,
                _moveRowsDownButton,
                _moveRowsToBeginButton,
                _moveRowsToEndButton,
                _addRowsButton,
                _deleteRowsButton,
                _sortRowsButton,
                _reverseRowsButton,
                _updateTableButton
            };
            GUIManager.SetTabIndex(buttonArray);
            // Размещение кнопок в контейнере.
            _editTableGroupBox.Controls.AddRange(buttonArray);
            GUIManager.PlaceControls(buttonArray, GUISizes.HORIZONTAL_OFFSET, GUISizes.VERTICAL_OFFSET, BUTTONS_COLUMN_COUNT);
            GUIManager.FitContainerSize(_editTableGroupBox, GUIManager.FitSizeMode.FitWidthAndHeight, GUISizes.HORIZONTAL_OFFSET, GUISizes.VERTICAL_OFFSET);
            // Размещение элементов в контейнере.
            Control[] controlArray1 = { _dataGridViewObject, _editTableGroupBox, _saveChangesButton, _cancelButtonObject };
            Controls.AddRange(controlArray1);

            Control[] controlArray2 = { _dataGridViewObject, _editTableGroupBox };
            GUIManager.PlaceControlsVertically(controlArray2, GUISizes.VERTICAL_OFFSET);
            GUIManager.AlignControlsByLeftBorder(controlArray2);

            Control[] controlArray3 = { _editTableGroupBox, _saveChangesButton, _cancelButtonObject };
            GUIManager.PlaceControlsHorizontally(controlArray3, GUISizes.HORIZONTAL_OFFSET);
            GUIManager.AlignControlsByBottomBorder(controlArray3);
            _dataGridViewObject.Width = _cancelButtonObject.Left + _cancelButtonObject.Width - _dataGridViewObject.Left;
            GUIManager.FitContainerSize(this, GUIManager.FitSizeMode.FitWidthAndHeight, GUISizes.HORIZONTAL_OFFSET, GUISizes.VERTICAL_OFFSET);
            // Подсказки элементов.
            var controlToolTip = new ToolTip();
            controlToolTip.SetToolTip(_moveRowsUpButton, "Переместить строку вверх (Alt+Стрелка вверх)");
            controlToolTip.SetToolTip(_moveRowsDownButton, "Переместить строку вниз (Alt+Стрелка вниз)");
            controlToolTip.SetToolTip(_moveRowsToBeginButton, "Переместить строку в начало таблицы (Ctrl+Alt+Стрелка вверх)");
            controlToolTip.SetToolTip(_moveRowsToEndButton, "Переместить строку в конец таблицы (Ctrl+Alt+Стрелка вниз)");
            controlToolTip.SetToolTip(_addRowsButton, "Добавить строку (Alt+Знак\"+\")");
            controlToolTip.SetToolTip(_deleteRowsButton, "Удалить выделенные строки (Alt+Delete)");
            controlToolTip.SetToolTip(_sortRowsButton, "Сортировать строки (Alt+S)");
            controlToolTip.SetToolTip(_reverseRowsButton, "Реверсировать строки (Alt+R)");
            controlToolTip.SetToolTip(_updateTableButton, "Заново считать данные (F5)");

            _dataGridViewObject.Click += _dataGridViewObject_Click;
        }

        #endregion

        #region Методы

        private void _dataGridViewObject_Click(object sender, System.EventArgs e)
        {
            // Отключение стандартной сортировки строк.
            if (_dataGridViewObject.ColumnCount == 0)
            {
                return;
            }
            foreach (DataGridViewColumn column in _dataGridViewObject.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion
    }
}
