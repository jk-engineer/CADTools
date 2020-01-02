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

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CADToolsCore.Classes
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
        private DataTable _dataTable;

        /// <summary>
        /// Список значений ячеек строк исходной таблицы.
        /// </summary>
        private List<object[]> _sourceRowsValues = new List<object[]>();

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        /// <param name="dataTable">Таблица данных.</param>
        public XMLDataTableEditor(DataTable dataTable)
        {
            _dataTable = dataTable;
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
            if (!RowIndicesIsOK(rowIndices))
            {
                return;
            }
            rowIndices = AbsIndices.Invoke(rowIndices);
            // Для предотвращения выхода за пределы таблицы производится проверка величины сдвига строк.
            if (rowIndices.First() + offset < 0 | rowIndices.Last() + offset > _sourceRowsValues.Count - 1)
            {
                return;
            }
            // Операция сдвига строк выполняется в два этапа:.
            // 1) в новый список копируются выбранные строки с новыми значениями индексов.
            // 2) в оставшиеся пустые места копируются по порядку все остальные строки исходной таблицы.
            var newIndices = rowIndices.Select(rowIndex => rowIndex + offset).ToArray();
            var newRowsValues = new List<object[]>(new object[_sourceRowsValues.Count - 1][]);
            // Копирование выделенных строк.
            for (var index = 0; index < rowIndices.Count(); index++)
            {
                newRowsValues[newIndices[index]] = _sourceRowsValues[rowIndices[index]];
            }
            // Копирование остальных строк.
            for (var rowIndex = 0; rowIndex < _sourceRowsValues.Count; rowIndex++)
            {
                // Пропуск строк, которые были выделены в исходной таблице.
                if (rowIndices.Contains(rowIndex))
                {
                    continue;
                }
                // Поиск первой свободной позиции в новом списке и копирование в нее текущей строки.
                for (var index = 0; index < newRowsValues.Count; index++)
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
            FillDataTableRows(true, newRowsValues);
            UpdateSourceValuesList();
        }

        /// <summary>
        /// Перемещает строки в начало таблицы.
        /// </summary>
        /// <param name="rowIndices">Индексы перемещаемых строк.</param>
        public void MoveRowsToBegin(int[] rowIndices)
        {
            if (!RowIndicesIsOK(rowIndices))
            {
                return;
            }
            MoveRows(-rowIndices.First(), rowIndices);
        }

        /// <summary>
        /// Перемещает строки в конец таблицы.
        /// </summary>
        /// <param name="rowIndices">Индексы перемещаемых строк.</param>
        public void MoveRowsToEnd(int[] rowIndices)
        {
            if (!RowIndicesIsOK(rowIndices))
            {
                return;
            }
            MoveRows(_sourceRowsValues.Count - 1 - rowIndices.Last(), rowIndices);
        }

        /// <summary>
        /// Сортирует строки таблицы по значениям ячеек в указанном столбце.
        /// </summary>
        /// <param name="columnIndex">Индекс столбца, по значениям ячеек которого производится сортировка.</param>
        public virtual void SortRows(int columnIndex)
        {
            // Проверка индекса столбца.
            columnIndex = AbsIndices.Invoke(columnIndex, _dataTable.Columns.Count - 1);
            // Сортировка строк производится в несколько этапов:
            // 1) считываются значения из указанного столбца, удаляются повторяющиеся значения, выполняется сортировка значений.
            // 2) берется первое значение из отсортированного списка, из таблицы выбираются все строки, в которых значения ячеек
            //    в указанном столбце совпадают с первым значением отсортированного списка. Найденные строки заносятся в итоговый список.
            // 3) аналогично выполняется заполнение остальных строк таблицы.
            var sortedColumnValues = new List<object>(GetColumnValues(columnIndex, false));
            sortedColumnValues = sortedColumnValues.Distinct().ToList();
            sortedColumnValues.Sort();
            var sortedRowsValues = new List<object[]>();
            for (var index = 0; index < sortedColumnValues.Count; index++)
            {
                sortedRowsValues.AddRange(_sourceRowsValues.Where(values => values[index] == sortedColumnValues[index]));
            }
            // Очистка исходной таблицы и заполнение ее новыми данными.
            FillDataTableRows(true, sortedRowsValues);
            UpdateSourceValuesList();
        }

        /// <summary>
        /// Сортирует строки таблицы по значениям ячеек в указанном столбце.
        /// </summary>
        /// <param name="columnName">Имя столбца, по значениям ячеек которого производится сортировка.</param>
        public virtual void SortRows(string columnName)
        {
            SortRows(GetColumnIndex(columnName));
        }

        /// <summary>
        /// Сортирует строки таблицы по значениям ячеек в указанном столбце.
        /// </summary>
        /// <param name="dataColumn">Столбец таблицы, по значениям ячеек которого производится сортировка.</param>
        public virtual void SortRows(DataColumn dataColumn)
        {
            SortRows(GetColumnIndex(dataColumn));
        }

        /// <summary>
        /// Изменяет порядок строк в таблице на обратный.
        /// </summary>
        public void ReverseRows()
        {
            FillDataTableRows(true, _sourceRowsValues.Reverse<object[]>().ToList());
            UpdateSourceValuesList();
        }

        /// <summary>
        /// Добавляет строки в конец таблицы.
        /// </summary>
        /// <param name="rowCount">Количество строк, которое необходимо добавить.</param>
        public void AddRows(int rowCount)
        {
            rowCount = AbsIndices.Invoke(rowCount);
            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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
            rowIndices = AbsIndices.Invoke(rowIndices);
            // При непосредственном удалении строк после удаления первой же выделенной строки индексы остальных строк изменяются,
            // что потребует сложной логики для сохранения правильных индексов.
            // Вместо этого итоговая таблица получается следующим образом:
            // 1) исходная таблица очищается (при этом значения ячеек остаются в резервной переменной).
            // 2) из резервной переменной в таблицу копируются только те строки, индексы которых не совпадают с индексами выбранных строк.
            for (var rowIndex = 0; rowIndex < _sourceRowsValues.Count; rowIndex++)
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
        public virtual List<string> GetColumnValues(DataColumn dataColumn, bool removeEmptyValues)
        {
            var resultValue = new List<string>();
            var cellValue = string.Empty;
            object checkValue;
            var dataTableObject = dataColumn.Table;
            try
            {
                for (var rowIndex = 0; rowIndex < dataTableObject.Rows.Count; rowIndex++)
                {
                    checkValue = dataTableObject.Rows[rowIndex][dataColumn];
                    cellValue = (checkValue == null) ? string.Empty : checkValue.ToString();
                    if (removeEmptyValues & !cellValue.Any())
                    {
                        continue;
                    }
                    resultValue.Add(cellValue);
                }
            }
            catch (System.Exception)
            {
                ShowColumnError();
            }
            return resultValue;
        }

        /// <summary>
        /// Возвращает список значений из указанного столбца таблицы.
        /// </summary>
        /// <param name="columnName">Имя столбца.</param>
        /// <param name="removeEmptyValues">Удалить из списка пустые значения.</param>
        /// <returns></returns>
        public virtual List<string> GetColumnValues(string columnName, bool removeEmptyValues)
        {
            var resultValue = new List<string>();
            try
            {
                resultValue = GetColumnValues(_dataTable.Columns[columnName], removeEmptyValues);
            }
            catch (System.Exception)
            {
                ShowColumnError();
            }
            return resultValue;
        }

        /// <summary>
        /// Возвращает список значений из указанного столбца таблицы.
        /// </summary>
        /// <param name="columnIndex">Индекс столбца.</param>
        /// <param name="removeEmptyValues">Удалить из списка пустые значения.</param>
        /// <returns></returns>
        public virtual List<string> GetColumnValues(int columnIndex, bool removeEmptyValues)
        {
            var resultValue = new List<string>();
            columnIndex = AbsIndices.Invoke(columnIndex);
            try
            {
                resultValue = GetColumnValues(_dataTable.Columns[columnIndex], removeEmptyValues);
            }
            catch (System.Exception)
            {
                ShowColumnError();
            }
            return resultValue;
        }

        /// <summary>
        /// Возвращает индекс столбца в таблице.
        /// </summary>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns></returns>
        public virtual int GetColumnIndex(string columnName)
        {
            var resultValue = -1;
            for (var columnIndex = 0; columnIndex < _dataTable.Columns.Count; columnIndex++)
            {
                if (_dataTable.Columns[columnIndex].ColumnName.ToLower() == columnName.ToLower())
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
        public virtual int GetColumnIndex(DataColumn dataColumn)
        {
            return GetColumnIndex(dataColumn.ColumnName);
        }

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
        private bool RowIndicesIsOK(int[] rowIndices)
        {
            var resultValue = true;
            if (rowIndices == null || !rowIndices.Any())
            {
                resultValue = false;
            }
            return resultValue;
        }

        /// <summary>
        /// Заполняет таблицу данными.
        /// </summary>
        /// <param name="clearTable">Выполнить очистку таблицы перед заполнением.</param>
        /// <param name="rowsValues">Список значений ячеек строк исходной таблицы.</param>
        private void FillDataTableRows(bool clearTable, List<object[]> rowsValues)
        {
            if (clearTable)
            {
                _dataTable.Clear();
            }
            for (var rowIndex = 0; rowIndex < rowsValues.Count(); rowIndex++)
            {
                _dataTable.Rows.Add(rowsValues[rowIndex]);
            }
        }

        /// <summary>
        /// Выводит сообщение об ошибке обращения к столбцу таблицы.
        /// </summary>
        private void ShowColumnError()
        {
            MessageBox.Show("Не удалось найти заданный столбец", "Ошибка", MessageBoxButtons.OK);
        }

        #endregion
    }
}
