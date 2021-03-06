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

using System.Linq;

namespace CADToolsCore.Utils
{
    /// <summary>
    /// Класс для получения неотрицательных индексов.
    /// </summary>
    public static class AbsIndices
    {
        #region Методы

        /// <summary>
        /// Возвращает массив неотрицательных индексов.
        /// </summary>
        /// <param name="indices">Исходный массив индексов.</param>
        /// <returns></returns>
        public static int[] GetIndices(int[] indices) =>
            indices?.Select(value => System.Math.Abs(value)).ToArray() ?? new int[] { };

        /// <summary>
        /// Возвращает неотрицательный индекс.
        /// </summary>
        /// <param name="index">Исходный индекс.</param>
        /// <param name="maxIndex">Максимальное значение индекса. Задайте значение, отличное от -1,
        /// чтобы ограничить максимальное возвращаемое значение индекса.</param>
        /// <returns></returns>
        public static int GetIndex(int index, int maxIndex = -1)
        {
            int maxAbsIndex = System.Math.Abs(maxIndex);
            int absIndex = System.Math.Abs(index);
            return ((maxIndex != -1) && (absIndex > maxAbsIndex)) ? maxAbsIndex : absIndex;
        }

        #endregion
    }
}
