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

namespace CADToolsCore.DataTable
{
    /// <summary>
    /// Класс для чтения и записи таблиц данных в формате XML.
    /// </summary>
    public class XMLDataTableReadWrite
    {
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
        public XMLDataTableReadWrite() { }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает таблицу данных из указанных значений столбцов.
        /// </summary>
        /// <param name="columnsValues">Набор значений в таблице по столбцам.</param>
        /// <param name="columnsNames">Имена столбцов в таблице данных.</param>
        /// <param name="tableName">Имя таблицы данных.</param>
        /// <returns></returns>
        public System.Data.DataTable DataTableFromColumnValues(List<string[]> columnsValues, string[] columnsNames, string tableName)
        {
            System.Data.DataTable resultTable = new System.Data.DataTable
            {
                TableName = tableName ?? string.Empty
            };
            if ((columnsValues == null) || (columnsNames == null))
            {
                return resultTable;
            }
            // Создание столбцов.
            resultTable.Columns.AddRange(columnsNames.Select(colName => new DataColumn(colName, typeof(string))).ToArray());
            // Для правильного заполнения строк таблицы необходимо определить наибольшую длину строкового массива в наборе.
            int rowCount = columnsValues.Select(arrayObj => arrayObj?.Count() ?? 0).Max();
            // Добавление в таблицу строк с пустыми ячейками.
            for (int index = 0; index < rowCount; index++)
            {
                resultTable.Rows.Add(resultTable.NewRow());
            }
            // Заполнение таблицы по столбцам.
            int columnCount = System.Math.Min(columnsValues.Count, columnsNames.Count());
            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                string[] columnValuesArray = columnsValues[columnIndex] ?? new string[] { };
                for (int rowIndex = 0; rowIndex < columnValuesArray.Count(); rowIndex++)
                {
                    resultTable.Rows[rowIndex][columnIndex] = columnValuesArray[rowIndex];
                }
            }
            return resultTable;
        }

        /// <summary>
        /// Возвращает таблицу данных из указанных значений строк.
        /// </summary>
        /// <param name="rowsValues">Набор значений в таблице по строкам.</param>
        /// <param name="columnsNames">Имена столбцов в таблице данных.</param>
        /// <param name="tableName">Имя таблицы данных.</param>
        /// <returns></returns>
        public System.Data.DataTable DataTableFromRowValues(List<string[]> rowsValues, string[] columnsNames, string tableName)
        {
            System.Data.DataTable resultTable = new System.Data.DataTable
            {
                TableName = tableName ?? string.Empty
            };
            if ((rowsValues == null) || (columnsNames == null))
            {
                return resultTable;
            }
            // Создание столбцов.
            resultTable.Columns.AddRange(columnsNames.Select(colName => new DataColumn(colName, typeof(string))).ToArray());
            // Заполнение строк таблицы. В случае необходимости добавляются недостающие столбцы.
            for (int rowIndex = 0; rowIndex < rowsValues.Count(); rowIndex++)
            {
                if ((rowsValues[rowIndex]?.Count() ?? 0) > resultTable.Columns.Count)
                {
                    resultTable.Columns.Add();
                }
                resultTable.Rows.Add(rowsValues[rowIndex] ?? new string[] { });
            }
            return resultTable;
        }

        /// <summary>
        /// Возвращает таблицу данных из указанного файла.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <param name="tableName">Имя таблицы данных.</param>
        /// <returns></returns>
        public System.Data.DataTable ReadDataTable(string fullFileName, string tableName)
        {
            System.Data.DataTable resultTable = new System.Data.DataTable();
            if (FileManager.CheckFileExists(fullFileName))
            {
                try
                {
                    resultTable.ReadXml(fullFileName);
                    resultTable.TableName = tableName;
                    OnDataLoaded();
                }
                catch (System.Exception)
                {
                    FileManager.ShowOpenFileError(fullFileName);
                }
            }
            return resultTable;
        }

        /// <summary>
        /// Сохраняет таблицу данных в указанный файл.
        /// </summary>
        /// <param name="dataTable">Таблица данных.</param>
        /// <param name="fullFileName">Полное имя файла.</param>
        public void WriteDataTable(System.Data.DataTable dataTable, string fullFileName)
        {
            try
            {
                dataTable.WriteXml(fullFileName);
                OnDataSaved();
            }
            catch (System.Exception)
            {
                FileManager.ShowSaveFileError(fullFileName);
            }
        }

        #endregion
    }
}
