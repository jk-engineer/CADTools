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
using System.Linq;

namespace CADToolsCore.Document.DrawingDocument
{
    /// <summary>
    /// Класс для работы с форматами листов чертежей.
    /// </summary>
    public static class DrawingSheetSizeManager
    {
        #region Поля, свойства

        /// <summary>
        /// Возвращает названия форматов.
        /// </summary>
        public static string[] SheetSizeNames => System.Enum.GetNames(typeof(DrawingSheetSize.DrawingSheetSizeEnum));

        /// <summary>
        /// Возвращает массив значений констант для перечисления форматов листов.
        /// </summary>
        public static System.Array SheetSizeValues => System.Enum.GetValues(typeof(DrawingSheetSize.DrawingSheetSizeEnum));

        /// <summary>
        /// Список значений высоты форматов в миллиметрах.
        /// </summary>
        private static readonly Dictionary<DrawingSheetSize.DrawingSheetSizeEnum, int> _heightList =
            new Dictionary<DrawingSheetSize.DrawingSheetSizeEnum, int>();

        /// <summary>
        /// Список значений ширины форматов в миллиметрах.
        /// </summary>
        private static readonly Dictionary<DrawingSheetSize.DrawingSheetSizeEnum, int> _widthList =
            new Dictionary<DrawingSheetSize.DrawingSheetSizeEnum, int>();

        /// <summary>
        /// Значения высоты форматов.
        /// </summary>
        private static readonly int[] _heightValues = {
            841,
            594,
            420,
            297,
            210,
            148,
            1189,
            1189,
            841,
            841,
            594,
            594,
            594,
            420,
            420,
            420,
            420,
            420,
            297,
            297,
            297,
            297,
            297,
            297,
            297
        };

        /// <summary>
        /// Значения ширины форматов.
        /// </summary>
        private static readonly int[] _widthValues = {
            1189,
            841,
            594,
            420,
            297,
            210,
            1682,
            2523,
            1783,
            2378,
            1261,
            1682,
            2102,
            891,
            1189,
            1486,
            1783,
            2080,
            630,
            841,
            1051,
            1261,
            1471,
            1682,
            1892
        };

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Статический конструктор класса.
        /// </summary>
        static DrawingSheetSizeManager()
        {
            // Заполнение коллекций значениями высоты и ширины форматов.
            for (int index = 0; index < SheetSizeNames.Count(); index++)
            {
                _heightList.Add((DrawingSheetSize.DrawingSheetSizeEnum)index, _heightValues[index]);
                _widthList.Add((DrawingSheetSize.DrawingSheetSizeEnum)index, _widthValues[index]);
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает имя указанного формата.
        /// </summary>
        /// <param name="drawingSheetSize">Формат листа чертежа согласно ГОСТ 2.301.</param>
        /// <returns></returns>
        public static string GetSheetSizeName(DrawingSheetSize.DrawingSheetSizeEnum drawingSheetSize) =>
            System.Enum.GetName(typeof(DrawingSheetSize.DrawingSheetSizeEnum), drawingSheetSize);

        /// <summary>
        /// Возвращает высоту указанного формата в миллиметрах (для альбомной ориентации).
        /// </summary>
        /// <param name="drawingSheetSize">Формат листа чертежа согласно ГОСТ 2.301.</param>
        /// <returns></returns>
        public static int GetSheetSizeHeight(DrawingSheetSize.DrawingSheetSizeEnum drawingSheetSize) =>
            _heightList[drawingSheetSize];

        /// <summary>
        /// Возвращает ширину указанного формата в миллиметрах (для альбомной ориентации).
        /// </summary>
        /// <param name="drawingSheetSize">Формат листа чертежа согласно ГОСТ 2.301.</param>
        /// <returns></returns>
        public static int GetSheetSizeWidth(DrawingSheetSize.DrawingSheetSizeEnum drawingSheetSize) =>
            _widthList[drawingSheetSize];

        /// <summary>
        /// Возвращает площадь указанного формата в квадратных миллиметрах.
        /// </summary>
        /// <param name="drawingSheetSize">Формат листа чертежа согласно ГОСТ 2.301.</param>
        /// <returns></returns>
        public static int GetSheetSizeArea(DrawingSheetSize.DrawingSheetSizeEnum drawingSheetSize) =>
            GetSheetSizeHeight(drawingSheetSize) * GetSheetSizeWidth(drawingSheetSize);

        #endregion
    }
}
