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

namespace CADToolsGUI.Forms
{
    /// <summary>
    /// Форма для поиска документа в списке.
    /// </summary>
    public class SearchDocumentListForm : Form
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
            Size = new System.Drawing.Size(CONTROL_WIDTH, 264),
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
        /// Изображение документа на форме.
        /// </summary>
        private PictureBox _previewPictureBox = new PictureBox()
        {
            BorderStyle = BorderStyle.FixedSingle,
            Name = "PreviewPictureBox",
            Size = new System.Drawing.Size(CONTROL_WIDTH, 190),
            SizeMode = PictureBoxSizeMode.StretchImage
        };

        /// <summary>
        /// Информация о выбранном документе.
        /// </summary>
        private GroupBox _docInfoGroupBox = new GroupBox()
        {
            Font = GUIFonts.MainFont,
            Name = "DocInfoGroupBox",
            Size = new System.Drawing.Size(CONTROL_WIDTH, 60),
            Text = "Информация о документе"
        };

        /// <summary>
        /// Тип выбранного документа.
        /// </summary>
        private Label _documentTypeLabel = new Label()
        {
            AutoSize = true,
            Font = GUIFonts.MainFont,
            Location = new System.Drawing.Point(GUISizes.HORIZONTAL_OFFSET, GUISizes.TOP_OFFSET_GROUPBOX + 6),
            Name = "DocTypeLabel",
            Text = DOCTYPE_TEXT
        };

        /// <summary>
        /// Количество листов чертежа.
        /// </summary>
        private Label _drawingSheetsCountLabel = new Label()
        {
            AutoSize = true,
            Font = GUIFonts.MainFont,
            Name = "SheetsCountLabel",
            Text = SHEETCOUNT_TEXT
        };

        /// <summary>
        /// Кнопка перехода к выбранному документу.
        /// </summary>
        private Button _goToDocumentButton = new Button()
        {
            Font = GUIFonts.MainFont,
            Name = "GoToDocButton",
            Size = new System.Drawing.Size(CONTROL_WIDTH, GUISizes.BUTTON_HEIGHT),
            TabIndex = 2,
            Text = "Перейти к документу"
        };

        /// <summary>
        /// Кнопка перехода к выбранному документу.
        /// </summary>
        public Button GoToDocumentButton
        {
            get { return _goToDocumentButton; }
        }

        #endregion

        #region Поля, свойства

        /// <summary>
        /// Растровое изображение документа.
        /// </summary>
        public System.Drawing.Image DocumentThumbnail
        {
            get { return _previewPictureBox.Image; }
            set { _previewPictureBox.Image = value; }
        }

        /// <summary>
        /// Тип выбранного документа.
        /// </summary>
        public string DocumentTypeName
        {
            get { return _documentTypeLabel.Text; }
            set { _documentTypeLabel.Text = value; }
        }

        /// <summary>
        /// Количество листов чертежа.
        /// </summary>
        public int DrawingSheetsCount
        {
            get
            {
                int resultValue;
                int.TryParse(_drawingSheetsCountLabel.Text, out resultValue);
                return resultValue;
            }
            set { _drawingSheetsCountLabel.Text = value.ToString(); }
        }

        /// <summary>
        /// Текст для типа документа.
        /// </summary>
        public const string DOCTYPE_TEXT = "Тип документа: ";

        /// <summary>
        /// Текст для количества листов.
        /// </summary>
        public const string SHEETCOUNT_TEXT = "Кол-во листов: ";

        /// <summary>
        /// Ширина элементов интерфейса.
        /// </summary>
        private const int CONTROL_WIDTH = 200;

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        public SearchDocumentListForm()
        {
            // Свойства формы.
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchDocumentForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Поиск документа в списке";
            // Размещение элементов на форме.
            Control[] controlArray1 = { _documentTypeLabel, _drawingSheetsCountLabel };
            _docInfoGroupBox.Controls.AddRange(controlArray1);
            GUIManager.PlaceControlsVertically(controlArray1, GUISizes.VERTICAL_OFFSET);
            GUIManager.AlignControlsByLeftBorder(controlArray1);
            GUIManager.FitContainerSize(_docInfoGroupBox, GUIManager.FitSizeMode.FitHeight, 0, GUISizes.VERTICAL_OFFSET);

            this.Controls.AddRange(new Control[] {_searchTextBox, _searchResultListBox, _previewPictureBox,
                                   _docInfoGroupBox, _goToDocumentButton});

            Control[] controlArray2 = { _searchTextBox, _previewPictureBox };
            GUIManager.PlaceControlsHorizontally(controlArray2, GUISizes.HORIZONTAL_OFFSET);
            GUIManager.AlignControlsByTopBorder(controlArray2);

            Control[] controlArray3 = { _searchTextBox, _searchResultListBox };
            GUIManager.PlaceControlsVertically(controlArray3, GUISizes.VERTICAL_OFFSET);
            GUIManager.AlignControlsByLeftBorder(controlArray3);

            Control[] controlArray4 = { _previewPictureBox, _docInfoGroupBox, _goToDocumentButton };
            GUIManager.PlaceControlsVertically(controlArray4, GUISizes.VERTICAL_OFFSET);
            GUIManager.AlignControlsByLeftBorder(controlArray4);

            _searchResultListBox.Height = _goToDocumentButton.Top + _goToDocumentButton.Height - _searchResultListBox.Top;
            GUIManager.FitContainerSize(this, GUIManager.FitSizeMode.FitWidthAndHeight, GUISizes.HORIZONTAL_OFFSET, GUISizes.BOTTO_OFFSET_FORM);
            // Подсказки элементов.
            var controlToolTip = new ToolTip();
            controlToolTip.SetToolTip(_searchTextBox, "Наберите текст для поиска документа");
            controlToolTip.SetToolTip(_searchResultListBox, "Выберите необходимый документ");
            controlToolTip.SetToolTip(_goToDocumentButton, "Нажмите, чтобы перейти к документу");
            controlToolTip.SetToolTip(_previewPictureBox, "Слайд документа");
            // Заполнение поля для поискового запроса.
            _searchTextBox.Text = "Введите текст для поиска...";
            _searchTextBox.SelectAll();
            _searchTextBox.Focus();
        }

        #endregion
    }
}
