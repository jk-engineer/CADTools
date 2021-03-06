﻿#region Copyright
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
using System.Linq;

namespace CADToolsCore.Document.DrawingDocument
{
    /// <summary>
    /// Класс для подсчета количества форматов чертежей.
    /// </summary>
    public class DrawingSheetSizeCount
    {
        #region Поля, свойства

        /// <summary>
        /// Коллекция, содержащая счетчик листов каждого из форматов.
        /// </summary>
        private readonly Dictionary<DrawingSheetSize.DrawingSheetSizeEnum, int> _sheetSizeCount =
            new Dictionary<DrawingSheetSize.DrawingSheetSizeEnum, int>();

        /// <summary>
        /// Площадь формата А4 в квадратных миллиметрах.
        /// </summary>
        private readonly int _a4SizeArea = DrawingSheetSizeManager.GetSheetSizeArea(DrawingSheetSize.DrawingSheetSizeEnum.A4);

        /// <summary>
        /// Значения перечисления форматов чертежа.
        /// </summary>
        private readonly System.Array _sheetSizeValues = DrawingSheetSizeManager.SheetSizeValues;

        /// <summary>
        /// Возвращает суммарное количество форматов А4 для всех чертежей.
        /// </summary>
        public int SummaryA4SizeCount { get; private set; } = 0;

        #endregion

        #region События

        /// <summary>
        /// Происходит при окончании подсчета форматов.
        /// </summary>
        public event System.Action SheetSizeCounted;

        /// <summary>
        /// Вызывает событие <see cref="SheetSizeCounted"/>.
        /// </summary>
        protected virtual void OnSheetSizeCounted() => SheetSizeCounted?.Invoke();

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        public DrawingSheetSizeCount()
        {
            // Заполнение коллекции счетчиками соответствующих форматов.
            foreach (DrawingSheetSize.DrawingSheetSizeEnum size in _sheetSizeValues)
            {
                _sheetSizeCount.Add(size, 0);
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Выполняет подсчет количества форматов чертежей.
        /// </summary>
        /// <param name="drawings">Коллекция чертежей.</param>
        public void CountSizes(IDocumentsCollection<IDrawingDocument> drawings)
        {
            // Обнуление счетчиков.
            SummaryA4SizeCount = 0;
            foreach (DrawingSheetSize.DrawingSheetSizeEnum size in _sheetSizeValues)
            {
                _sheetSizeCount[size] = 0;
            }
            // Подсчет количества форматов.
            foreach (IDrawingDocument draw in drawings?.Values ?? new IDrawingDocument[] { })
            {
                foreach (IDrawingSheet sheet in draw?.Sheets ?? new IDrawingSheet[] { })
                {
                    if (sheet != null)
                    {
                        _sheetSizeCount[sheet.Size] += 1;
                    }
                }
            }
            // Подсчет общего количества форматов А4 производится следующим образом:
            // для каждого листа определяется количество форматов А4, из которых его можно составить.
            // Площадь листа делится на площадь формата А4. Полученное значение округляется до целого числа.
            int sheetSizeArea;
            foreach (DrawingSheetSize.DrawingSheetSizeEnum size in _sheetSizeValues)
            {
                sheetSizeArea = DrawingSheetSizeManager.GetSheetSizeArea(size);
                SummaryA4SizeCount += (int)System.Math.Round((double)(sheetSizeArea / _a4SizeArea)) * _sheetSizeCount[size];
            }
            // Сигнал о завершении подсчета.
            OnSheetSizeCounted();
        }

        /// <summary>
        /// Возвращает количество листов указанного формата.
        /// </summary>
        /// <param name="drawingSheetSize">Формат листа чертежа согласно ГОСТ 2.301.</param>
        /// <returns></returns>
        public int GetSheetSizeCount(DrawingSheetSize.DrawingSheetSizeEnum drawingSheetSize) => _sheetSizeCount[drawingSheetSize];

        /// <summary>
        /// Возвращает строковое представление счетчика форматов чертежей.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string resultValue = string.Empty;
            for (int index = 0; index < _sheetSizeCount.Count(); index++)
            {
                // Пропуск нулевых значений счетчика (для улучшения читаемости данных).
                if (_sheetSizeCount.Values.ElementAt(index) == 0)
                {
                    continue;
                }
                resultValue += _sheetSizeCount.Keys.ElementAt(index).ToString() + ": " +
                               _sheetSizeCount.Values.ElementAt(index).ToString() + System.Environment.NewLine;
            }
            return resultValue;
        }

        #endregion
    }
}
