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

using CADToolsCore.FileSystem;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CADToolsCore.DataTable
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
        public System.Data.DataTable DataTableObject { get; private set; }

        /// <summary>
        /// Полное имя файла с таблицей данных.
        /// </summary>
        private readonly string _fullFileName;

        /// <summary>
        /// Имя файла с таблицей данных.
        /// </summary>
        private readonly string _fileName;

        /// <summary>
        /// Имя таблицы данных.
        /// </summary>
        private readonly string _tableName;

        #endregion

        #region События

        /// <summary>
        /// Происходит после считывания данных из файла.
        /// </summary>
        public event System.Action DataLoaded;

        /// <summary>
        /// Вызывает событие <see cref="DataLoaded"/>.
        /// </summary>
        protected virtual void OnDataLoaded() => DataLoaded?.Invoke();

        /// <summary>
        /// Происходит после сохранения данных в файл.
        /// </summary>
        public event System.Action DataSaved;

        /// <summary>
        /// Вызывает событие <see cref="DataSaved"/>.
        /// </summary>
        protected virtual void OnDataSaved() => DataSaved?.Invoke();

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла с таблицей данных.</param>
        /// <param name="tableName">Имя таблицы данных.</param>
        public XMLDataTableReadWrite(string fullFileName, string tableName)
        {
            _fullFileName = fullFileName ?? string.Empty;
            try
            {
                _fileName = System.IO.Path.GetFileName(_fullFileName);
            }
            catch (System.ArgumentException)
            {
                _fileName = string.Empty;
            }
            _tableName = tableName ?? string.Empty;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает таблицу данных из файла, указанного при создании экземпляра класса.
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable GetDataTable()
        {
            System.Data.DataTable resultValue = new System.Data.DataTable();
            if (FileManager.CheckFileExists(_fullFileName))
            {
                try
                {
                    DataTableObject.ReadXml(_fullFileName);
                    DataTableObject.TableName = _tableName;
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
                DataTableObject.WriteXml(_fullFileName);
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
        public virtual void SaveDataTable(System.Data.DataTable dataTable)
        {
            DataTableObject = dataTable ?? new System.Data.DataTable();
            DataTableObject.TableName = _tableName;
            SaveDataTable();
        }

        /// <summary>
        /// Сохраняет таблицу данных в файл, указанный при создании экземпляра класса.
        /// </summary>
        /// <param name="columnNames">Имена столбцов в таблице данных.</param>
        /// <param name="columnValues">Набор значений в таблице по столбцам.</param>
        public void SaveDataTableFromColumnValues(string[] columnNames, List<string[]> columnValues)
        {
            System.Data.DataTable resultDataTable = new System.Data.DataTable
            {
                TableName = _tableName
            };
            // Создание столбцов.
            resultDataTable.Columns.AddRange(columnNames?.Select(colName => new DataColumn(colName)).ToArray() ?? new DataColumn[] { });
            // Для правильного заполнения строк таблицы необходимо определить наибольшую длину строкового массива в наборе.
            int rowCount = columnValues?.Select(arrayObj => arrayObj?.Count() ?? 0).Max() ?? 0;
            // Добавление в таблицу строк с пустыми ячейками.
            for (int index = 0; index < rowCount; index++)
            {
                resultDataTable.Rows.Add(columnNames?.Select(value => string.Empty) ?? new string[] { });
            }
            // Заполнение таблицы по столбцам.
            for (int columnIndex = 0; columnIndex < (columnNames?.Count() ?? 0); columnIndex++)
            {
                string[] columnValuesArray = columnValues?[columnIndex] ?? new string[] { };
                for (int rowIndex = 0; rowIndex < columnValuesArray.Count(); rowIndex++)
                {
                    resultDataTable.Rows[rowIndex][columnIndex] = columnValuesArray[rowIndex];
                }
            }
            // Запись таблицы данных в файл.
            DataTableObject = resultDataTable;
            SaveDataTable();
        }

        /// <summary>
        /// Сохраняет таблицу данных в файл, указанный при создании экземпляра класса.
        /// </summary>
        /// <param name="columnNames">Имена столбцов в таблице данных.</param>
        /// <param name="rowValues">Набор значений в таблице по строкам.</param>
        public void SaveDataTableFromRowValues(string[] columnNames, List<string[]> rowValues)
        {
            System.Data.DataTable resultDataTable = new System.Data.DataTable
            {
                TableName = _tableName
            };
            // Создание столбцов.
            resultDataTable.Columns.AddRange(columnNames?.Select(colName => new DataColumn(colName)).ToArray() ?? new DataColumn[] { });
            // Заполнение строк таблицы. В случае необходимости добавляются недостающие столбцы.
            for (int rowIndex = 0; rowIndex < (rowValues?.Count() ?? 0); rowIndex++)
            {
                if ((rowValues[rowIndex]?.Count() ?? 0) > resultDataTable.Columns.Count)
                {
                    resultDataTable.Columns.Add(string.Empty);
                }
                resultDataTable.Rows.Add(rowValues[rowIndex] ?? new string[] { });
            }
            // Запись таблицы данных в файл.
            DataTableObject = resultDataTable;
            SaveDataTable();
        }

        #endregion
    }
}
