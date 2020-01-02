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

using System.Drawing;

namespace CADToolsGUI.Classes
{
    /// <summary>
    /// Набор шрифтов для приложений.
    /// </summary>
    public static class GUIFonts
    {
        #region Поля, свойства

        /// <summary>
        /// Основной шрифт для графических элементов управления.
        /// </summary>
        public static Font MainFont
        {
            get { return new Font(FontFamily.GenericSansSerif, (float)9.75); }
        }

        #endregion
    }
}
