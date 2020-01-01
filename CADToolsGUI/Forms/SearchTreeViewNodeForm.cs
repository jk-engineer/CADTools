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
    /// Форма для поиска узла в дереве <see cref="TreeView"/>.
    /// </summary>
    public class SearchTreeViewNodeForm : Form
    {
        #region Элементы интерфейса

        /// <summary>
        /// Поле для ввода поискового запроса.
        /// </summary>
        private TextBox _searchTextBox = new TextBox()
        {
            Font = GUIFonts.MainFont,
            Location = GUISizes.DefaultControlLocation,
            Name = "SearchTextBox",
            Size = new System.Drawing.Size(CONTROL_WIDTH, GUISizes.COMBOBOX_HEIGHT),
            TabIndex = 0,
            WordWrap = false
        };

        /// <summary>
        /// Поле для ввода поискового запроса.
        /// </summary>
        public TextBox SearchTextBox
        {
            get { return _searchTextBox; }
        }

        /// <summary>
        /// Список с результатами поиска.
        /// </summary>
        private ListBox _searchResultListBox = new ListBox()
        {
            Font = GUIFonts.MainFont,
            HorizontalScrollbar = true,
            IntegralHeight = false,
            ItemHeight = 16,
            Name = "ResultListBox",
            Size = new System.Drawing.Size(CONTROL_WIDTH, 200),
            TabIndex = 1
        };

        /// <summary>
        /// Список с результатами поиска.
        /// </summary>
        public ListBox SearchResultListBox
        {
            get { return _searchResultListBox; }
        }

        /// <summary>
        /// Кнопка перехода к выбранному узлу дерева.
        /// </summary>
        private Button _goToNodeButton = new Button()
        {
            Font = GUIFonts.MainFont,
            Name = "GoToNodeButton",
            Size = new System.Drawing.Size(CONTROL_WIDTH, GUISizes.BUTTON_HEIGHT),
            TabIndex = 2
        };

        /// <summary>
        /// Кнопка перехода к выбранному узлу дерева.
        /// </summary>
        public Button GoToNodeButton
        {
            get { return _goToNodeButton; }
        }

        #endregion

        #region Поля, свойства

        /// <summary>
        /// Ширина элементов интерфейса.
        /// </summary>
        private const int CONTROL_WIDTH = 200;

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        /// <param name="formCaption">Заголовок формы.</param>
        /// <param name="buttonCaption">Текст, отображаемый на кнопке перехода к узлу дерева.</param>
        public SearchTreeViewNodeForm(string formCaption, string buttonCaption)
        {
            // Свойства формы.
            this.AcceptButton = _goToNodeButton;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchTreeViewNodeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = formCaption;
            // Свойства элементов.
            _goToNodeButton.Text = buttonCaption;
            // Размещение элементов на форме.
            Control[] controlArray = { _searchTextBox, _searchResultListBox, _goToNodeButton };
            this.Controls.AddRange(controlArray);
            GUIManager.PlaceControlsVertically(controlArray, GUISizes.VERTICAL_OFFSET);
            GUIManager.AlignControlsByLeftBorder(controlArray);
            GUIManager.FitContainerSize(this, GUIManager.FitSizeMode.FitWidthAndHeight, GUISizes.HORIZONTAL_OFFSET, GUISizes.BOTTO_OFFSET_FORM);
            // Подсказки элементов.
            var controlToolTip = new ToolTip();
            controlToolTip.SetToolTip(_searchTextBox, "Наберите текст для поиска");
            controlToolTip.SetToolTip(_searchResultListBox, "Выберите необходимый пункт");
            controlToolTip.SetToolTip(_goToNodeButton, "Нажимте, чтобы перейти к выбранному пункту");
            // Заполнение поля для поискового запроса.
            _searchTextBox.Text = "Введите текст для поиска...";
            _searchTextBox.SelectAll();
            _searchTextBox.Focus();
        }

        #endregion
    }
}
