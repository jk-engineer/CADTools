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

using System.Linq;

namespace CADToolsCore.Utils
{
    /// <summary>
    /// Класс для поиска текста в массиве строк.
    /// </summary>
    public static class SearchInTextArray
    {
        #region Методы

        /// <summary>
        /// Возвращает массив строк, содержащих искомый текст.
        /// </summary>
        /// <param name="textArray">Исходный массив строк.</param>
        /// <param name="expression">Искомое выражение.</param>
        /// <returns>Производит поиск выражения в каждой строке исходного массива и возвращает в виде массива только те строки,
        /// которые содержат искомое выражение</returns>
        public static string[] Search(string[] textArray, string expression) =>
            textArray?.Where(textValue => textValue?.ToLower().Contains(expression?.ToLower()) ?? false).ToArray() ?? new string[] { };

        #endregion
    }
}
