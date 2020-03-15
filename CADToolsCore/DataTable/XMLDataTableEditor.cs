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

using CADToolsCore.Utils;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CADToolsCore.DataTable
{
    /// <summary>
    /// Класс для редактирования таблиц данных.
    /// </summary>
    public class XMLDataTableEditor
    {
        #region Поля, свойства

        /// <summary>
        /// Таблица данных.
        /// </summary>
        private readonly System.Data.DataTable _dataTable;

        /// <summary>
        /// Список значений ячеек строк исходной таблицы.
        /// </summary>
        private readonly List<object[]> _sourceRowsValues = new List<object[]>();

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        /// <param name="dataTable">Таблица данных.</param>
        public XMLDataTableEditor(System.Data.DataTable dataTable)
        {
            _dataTable = dataTable ?? new System.Data.DataTable();
            UpdateSourceValuesList();
        }

        #endregion

        #region Методы обработки строк таблицы

        /// <summary>
        /// Перемещает строки на указанное число позиций.
        /// </summary>
        /// <param name="offset">Число позиций, на которое необходимо переместить строки.
        /// <para>Отрицательное значение сдвигает строки вверх, положительное - вниз.</para></param>
        /// <param name="rowIndices">Индексы перемещаемых строк.</param>
        public void MoveRows(int offset, int[] rowIndices)
        {
            if (!RowIndicesIsCorrect(rowIndices))
            {
                return;
            }
            rowIndices = AbsIndices.GetIndices(rowIndices);
            // Для предотвращения выхода за пределы таблицы производится проверка величины сдвига строк.
            if ((rowIndices.First() + offset) < 0 | (rowIndices.Last() + offset) > _sourceRowsValues.Count - 1)
            {
                return;
            }
            // Операция сдвига строк выполняется в два этапа:
            // 1) в новый список копируются выбранные строки с новыми значениями индексов.
            // 2) в оставшиеся пустые места копируются по порядку все остальные строки исходной таблицы.
            int[] newIndices = rowIndices.Select(rowIndex => rowIndex + offset).ToArray();
            List<object[]> newRowsValues = new List<object[]>(new object[_sourceRowsValues.Count][]);
            // Копирование выделенных строк.
            for (int index = 0; index < rowIndices.Count(); index++)
            {
                newRowsValues[newIndices[index]] = _sourceRowsValues[rowIndices[index]];
            }
            // Копирование остальных строк.
            for (int rowIndex = 0; rowIndex < _sourceRowsValues.Count; rowIndex++)
            {
                // Пропуск строк, которые были выделены в исходной таблице.
                if (rowIndices.Contains(rowIndex))
                {
                    continue;
                }
                // Поиск первой свободной позиции в новом списке и копирование в нее текущей строки.
                for (int index = 0; index < newRowsValues.Count; index++)
                {
                    if (newRowsValues[index] != null)
                    {
                        continue;
                    }
                    newRowsValues[index] = _sourceRowsValues[rowIndex];
                    break;
                }
            }
            // Очистка исходной таблицы и заполнение ее новыми данными.
            FillDataTableRows(newRowsValues);
            UpdateSourceValuesList();
        }

        /// <summary>
        /// Перемещает строки в начало таблицы.
        /// </summary>
        /// <param name="rowIndices">Индексы перемещаемых строк.</param>
        public void MoveRowsToBegin(int[] rowIndices) => MoveRows(-rowIndices?.First() ?? 0, rowIndices);

        /// <summary>
        /// Перемещает строки в конец таблицы.
        /// </summary>
        /// <param name="rowIndices">Индексы перемещаемых строк.</param>
        public void MoveRowsToEnd(int[] rowIndices) => MoveRows(_sourceRowsValues.Count - 1 - (rowIndices?.Last() ?? 0), rowIndices);

        /// <summary>
        /// Сортирует строки таблицы по значениям ячеек в указанном столбце.
        /// </summary>
        /// <param name="columnIndex">Индекс столбца, по значениям ячеек которого производится сортировка.</param>
        public virtual void SortRows(int columnIndex)
        {
            // Сортировка строк производится в несколько этапов:
            // 1) считываются значения из указанного столбца, удаляются повторяющиеся значения, выполняется сортировка значений.
            // 2) берется первое значение из отсортированного списка, из таблицы выбираются все строки, в которых значения ячеек
            //    в указанном столбце совпадают с первым значением отсортированного списка. Найденные строки заносятся в итоговый список.
            // 3) аналогично выполняется заполнение остальных строк таблицы.
            List<string> sortedColumnValues = new List<string>(GetColumnValues(columnIndex));
            sortedColumnValues = sortedColumnValues.Distinct().ToList();
            sortedColumnValues.Sort();
            List<object[]> sortedRowsValues = new List<object[]>();
            for (int index = 0; index < sortedColumnValues.Count; index++)
            {
                sortedRowsValues.AddRange(_sourceRowsValues.Where(values => values[columnIndex].ToString() == sortedColumnValues[index]));
            }
            // Очистка исходной таблицы и заполнение ее новыми данными.
            FillDataTableRows(sortedRowsValues);
            UpdateSourceValuesList();
        }

        /// <summary>
        /// Сортирует строки таблицы по значениям ячеек в указанном столбце.
        /// </summary>
        /// <param name="columnName">Имя столбца, по значениям ячеек которого производится сортировка.</param>
        public virtual void SortRows(string columnName) => SortRows(GetColumnIndex(columnName));

        /// <summary>
        /// Сортирует строки таблицы по значениям ячеек в указанном столбце.
        /// </summary>
        /// <param name="dataColumn">Столбец таблицы, по значениям ячеек которого производится сортировка.</param>
        public virtual void SortRows(DataColumn dataColumn) => SortRows(GetColumnIndex(dataColumn));

        /// <summary>
        /// Изменяет порядок строк в таблице на обратный.
        /// </summary>
        public void ReverseRows()
        {
            FillDataTableRows(_sourceRowsValues.Reverse<object[]>().ToList());
            UpdateSourceValuesList();
        }

        /// <summary>
        /// Добавляет строки в конец таблицы.
        /// </summary>
        /// <param name="rowCount">Количество строк, которое необходимо добавить.</param>
        public void AddRows(int rowCount)
        {
            rowCount = AbsIndices.GetIndex(rowCount);
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                _dataTable.Rows.Add(_dataTable.NewRow());
            }
            UpdateSourceValuesList();
        }

        /// <summary>
        /// Удаляет строки с указанными индексами.
        /// </summary>
        /// <param name="rowIndices">Индексы строк, которые необходимо удалить.</param>
        public void DeleteRows(int[] rowIndices)
        {
            rowIndices = AbsIndices.GetIndices(rowIndices);
            // При непосредственном удалении строк после удаления первой же выделенной строки индексы остальных строк изменяются,
            // что потребует сложной логики для отслеживания индексов.
            // Вместо этого итоговая таблица получается следующим образом:
            // 1) исходная таблица очищается (при этом значения ячеек остаются в резервной переменной _sourceRowsValues).
            // 2) из резервной переменной в таблицу копируются только те строки, индексы которых не совпадают с индексами выбранных для удаления строк.
            _dataTable.Clear();
            for (int rowIndex = 0; rowIndex < _sourceRowsValues.Count; rowIndex++)
            {
                if (rowIndices.Contains(rowIndex))
                {
                    continue;
                }
                _dataTable.Rows.Add(_sourceRowsValues[rowIndex]);
            }
            UpdateSourceValuesList();
        }

        #endregion

        #region Методы обработки столбцов таблицы

        /// <summary>
        /// Возвращает список значений из указанного столбца таблицы.
        /// </summary>
        /// <param name="dataColumn">Столбец таблицы.</param>
        /// <param name="removeEmptyValues">Удалить из списка пустые значения.</param>
        /// <returns></returns>
        public virtual string[] GetColumnValues(DataColumn dataColumn, bool removeEmptyValues = false)
        {
            List<string> resultValue = new List<string>();
            System.Data.DataTable dataTable = dataColumn?.Table ?? new System.Data.DataTable();
            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
            {
                object checkValue = dataTable.Rows[rowIndex][dataColumn];
                string cellValue = checkValue?.ToString() ?? string.Empty;
                if (removeEmptyValues & string.IsNullOrEmpty(cellValue))
                {
                    continue;
                }
                resultValue.Add(cellValue);
            }
            return resultValue.ToArray();
        }

        /// <summary>
        /// Возвращает список значений из указанного столбца таблицы.
        /// </summary>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="removeEmptyValues">Удалить из списка пустые значения.</param>
        /// <returns></returns>
        public virtual string[] GetColumnValues(string columnName, bool removeEmptyValues = false) =>
            GetColumnValues(_dataTable.Columns[columnName ?? string.Empty], removeEmptyValues);

        /// <summary>
        /// Возвращает список значений из указанного столбца таблицы.
        /// </summary>
        /// <param name="columnIndex">Индекс столбца.</param>
        /// <param name="removeEmptyValues">Удалить из списка пустые значения.</param>
        /// <returns></returns>
        public virtual string[] GetColumnValues(int columnIndex, bool removeEmptyValues = false)
        {
            columnIndex = AbsIndices.GetIndex(columnIndex, _dataTable.Columns.Count - 1);
            return GetColumnValues(_dataTable.Columns[columnIndex], removeEmptyValues);
        }

        /// <summary>
        /// Возвращает индекс столбца в таблице.
        /// </summary>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns></returns>
        public virtual int GetColumnIndex(string columnName)
        {
            int resultValue = -1;
            for (int columnIndex = 0; columnIndex < _dataTable.Columns.Count; columnIndex++)
            {
                if (_dataTable.Columns[columnIndex].ColumnName.ToLower() == (columnName?.ToLower() ?? string.Empty))
                {
                    resultValue = columnIndex;
                    break;
                }
            }
            return resultValue;
        }

        /// <summary>
        /// Возвращает индекс столбца в таблице.
        /// </summary>
        /// <param name="dataColumn">Столбец таблицы.</param>
        /// <returns></returns>
        public virtual int GetColumnIndex(DataColumn dataColumn) => GetColumnIndex(dataColumn?.ColumnName ?? string.Empty);

        #endregion

        #region Методы

        /// <summary>
        /// Синхронизирует значения ячеек в таблице и исходном списке.
        /// </summary>
        private void UpdateSourceValuesList()
        {
            _sourceRowsValues.Clear();
            _sourceRowsValues.AddRange(_dataTable.AsEnumerable().Select(row => row.ItemArray));
        }

        /// <summary>
        /// Возвращает <see cref="true"/> при корректном массиве индексов строк (массив не null и не пустой).
        /// </summary>
        /// <param name="">Индексы строк.</param>
        /// <returns></returns>
        private bool RowIndicesIsCorrect(int[] rowIndices) => ((rowIndices != null) && rowIndices.Any());

        /// <summary>
        /// Заполняет таблицу данными.
        /// </summary>
        /// <param name="rowsValues">Список значений ячеек строк исходной таблицы.</param>
        /// <param name="clearTable">Выполнить очистку таблицы перед заполнением.</param>
        private void FillDataTableRows(List<object[]> rowsValues, bool clearTable = true)
        {
            if (clearTable)
            {
                _dataTable.Clear();
            }
            for (int rowIndex = 0; rowIndex < (rowsValues?.Count() ?? 0); rowIndex++)
            {
                _dataTable.Rows.Add(rowsValues[rowIndex] ?? new object[] { });
            }
        }

        #endregion
    }
}
