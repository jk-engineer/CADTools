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

using System.Windows.Forms;

namespace CADToolsCore.Utils
{
    /// <summary>
    /// Класс для создания запроса типа "Да"/"Нет".
    /// </summary>
    public static class YesNoQuestion
    {
        #region Методы

        /// <summary>
        /// Возвращает <see cref="true"/> при ответе пользователя "Да".
        /// </summary>
        /// <param name="question">Текст запроса.</param>
        /// <param name="caption">Заголовок окна запроса.</param>
        /// <returns></returns>
        public static bool GetAnswer(string question, string caption)
        {
            bool resultValue = false;
            if (!string.IsNullOrEmpty(question) & !string.IsNullOrEmpty(caption))
            {
                DialogResult answer = MessageBox.Show(question, caption, MessageBoxButtons.YesNo);
                if (answer == DialogResult.Yes) resultValue = true;
            }
            return resultValue;
        }

        #endregion
    }
}
