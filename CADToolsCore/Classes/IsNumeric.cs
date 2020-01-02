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

using System;
using System.Windows.Forms;

namespace CADToolsCore.Classes
{
    /// <summary>
    /// Класс для проверки строкового выражения на число.
    /// </summary>
    public static class IsNumeric
    {
        /// <summary>
        /// Возвращает <see cref="true"/>, если выражение является числом.
        /// </summary>
        /// <param name="expression">Строковое выражение для проверки.</param>
        /// <param name="checkInteger">Проверить на целочисленность.</param>
        /// <param name="showError">Показать предупреждение о неверном вводе выражения.</param>
        /// <returns></returns>
        public static bool Invoke(string expression, bool checkInteger, bool showError)
        {
            bool resultValue = true;
            // Проверка на число.
            if (!Double.TryParse(expression, out _))
            {
                if (showError)
                {
                    MessageBox.Show("Введите число", "Ошибка", MessageBoxButtons.OK);
                }
                resultValue = false;
            }
            // Проверка на целочисленность.
            if (checkInteger && !Int32.TryParse(expression, out _))
            {
                if (showError)
                {
                    MessageBox.Show("Введите целое число", "Ошибка", MessageBoxButtons.OK);
                }
                resultValue = false;
            }
            return resultValue;
        }
    }
}
