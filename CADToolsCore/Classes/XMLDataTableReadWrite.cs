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

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CADToolsCore.Classes
{
    /// <summary>
    /// Класс для чтения и записи таблиц данных в формате XML.
    /// </summary>
    public class XMLDataTableReadWrite
    {
        #region Поля, свойства

        /// <summary>
        /// Таблица данных.
        /// </summary>
        private DataTable _dataTableObject;

        /// <summary>
        /// Таблица данных.
        /// </summary>
        public DataTable DataTableObject
        {
            get { return _dataTableObject; }
        }

        /// <summary>
        /// Полное имя файла с таблицей данных.
        /// </summary>
        private string _fullFileName;

        /// <summary>
        /// Имя файла с таблицей данных.
        /// </summary>
        private string _fileName;

        /// <summary>
        /// Имя таблицы данных.
        /// </summary>
        private string _tableName;

        #endregion

        #region События

        /// <summary>
        /// Происходит после считывания данных из файла.
        /// </summary>
        public event System.Action DataLoaded;

        /// <summary>
        /// Вызывает событие <see cref="DataLoaded"/>.
        /// </summary>
        protected virtual void OnDataLoaded()
        {
            DataLoaded?.Invoke();
        }

        /// <summary>
        /// Происходит после сохранения данных в файл.
        /// </summary>
        public event System.Action DataSaved;

        /// <summary>
        /// Вызывает событие <see cref="DataSaved"/>.
        /// </summary>
        protected virtual void OnDataSaved()
        {
            DataSaved?.Invoke();
        }

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла с таблицей данных.</param>
        /// <param name="tableName">Имя таблицы данных.</param>
        public XMLDataTableReadWrite(string fullFileName, string tableName)
        {
            _fullFileName = fullFileName;
            _fileName = System.IO.Path.GetFileName(_fullFileName);
            _tableName = tableName;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает таблицу данных из файла, указанного при создании экземпляра класса.
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable()
        {
            var resultValue = new DataTable();
            if (FileManager.CheckFileExist(_fullFileName, true))
            {
                try
                {
                    _dataTableObject.ReadXml(_fullFileName);
                    _dataTableObject.TableName = _tableName;
                    OnDataLoaded();
                }
                catch (System.Exception)
                {
                    FileManager.ShowOpenFileError(_fileName);
                }
            }
            return resultValue;
        }

        /// <summary>
        /// Сохраняет таблицу данных в файл, указанный при создании экземпляра класса.
        /// </summary>
        public virtual void SaveDataTable()
        {
            try
            {
                _dataTableObject.WriteXml(_fullFileName);
                OnDataSaved();
            }
            catch (System.Exception)
            {
                FileManager.ShowSaveFileError(_fileName);
            }
        }

        /// <summary>
        /// Сохраняет таблицу данных в файл, указанный при создании экземпляра класса.
        /// </summary>
        /// <param name="dataTable">Таблица данных.</param>
        public virtual void SaveDataTable(DataTable dataTable)
        {
            _dataTableObject = dataTable;
            _dataTableObject.TableName = _tableName;
            SaveDataTable();
        }

        /// <summary>
        /// Сохраняет таблицу данных в файл, указанный при создании экземпляра класса.
        /// </summary>
        /// <param name="columnNames">Имена столбцов в таблице данных.</param>
        /// <param name="columnValues">Набор значений в таблице по столбцам.</param>
        public void SaveDataTableFromColumnValues(string[] columnNames, List<string[]> columnValues)
        {
            var resultDataTable = new DataTable();
            resultDataTable.TableName = _tableName;
            // Создание столбцов.
            resultDataTable.Columns.AddRange(columnNames.Select(colName => new DataColumn(colName)).ToArray());
            // Для правильного заполнения строк таблицы необходимо определить наибольшую длину строкового массива в наборе.
            var rowCount = 0;
            try
            {
                rowCount = columnValues.Select(arrayObj => arrayObj.Count()).Max();
            }
            catch (System.Exception)
            {
                WrongArrayMessage();
                return;
            }
            // Добавление в таблицу строк с пустыми ячейками.
            for (var index = 0; index < rowCount; index++)
            {
                resultDataTable.Rows.Add(columnNames.Select(value => string.Empty));
            }
            // Заполнение таблицы по столбцам.
            string[] columnValuesArray;
            try
            {
                for (var columnIndex = 0; columnIndex < columnNames.Count(); columnIndex++)
                {
                    columnValuesArray = columnValues[columnIndex];
                    for (var rowIndex = 0; rowIndex < columnValuesArray.Count(); rowIndex++)
                    {
                        resultDataTable.Rows[rowIndex][columnIndex] = columnValuesArray[rowIndex];
                    }
                }
            }
            catch (System.Exception)
            {
                WrongArrayMessage();
                return;
            }
            // Запись таблицы данных в файл.
            _dataTableObject = resultDataTable;
            SaveDataTable();
        }

        /// <summary>
        /// Сохраняет таблицу данных в файл, указанный при создании экземпляра класса.
        /// </summary>
        /// <param name="columnNames">Имена столбцов в таблице данных.</param>
        /// <param name="rowValues">Набор значений в таблице по строкам.</param>
        public void SaveDataTableFromRowValues(string[] columnNames, List<string> rowValues)
        {
            var resultDataTable = new DataTable();
            resultDataTable.TableName = _tableName;
            // Создание столбцов.
            resultDataTable.Columns.AddRange(columnNames.Select(colName => new DataColumn(colName)).ToArray());
            // Заполнение строк таблицы. В случае необходимости добавляются недостающие столбцы.
            for (var rowIndex = 0; rowIndex < rowValues.Count(); rowIndex++)
            {
                if (rowValues[rowIndex].Count() > resultDataTable.Columns.Count)
                {
                    resultDataTable.Columns.Add(string.Empty);
                }
                resultDataTable.Rows.Add(rowValues[rowIndex]);
            }
            // Запись таблицы данных в файл.
            _dataTableObject = resultDataTable;
            SaveDataTable();
        }

        /// <summary>
        /// Выводит сообщение об ошибке при обработке массивов.
        /// </summary>
        private void WrongArrayMessage()
        {
            MessageBox.Show("Неверный набор строковых массивов", "Ошибка", MessageBoxButtons.OK);
        }

        #endregion
    }
}
