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

namespace CADToolsCore.CADApplication
{
    /// <summary>
    /// Документы CAD-системы.
    /// </summary>
    public static class ApplicationDocuments
    {
        /// <summary>
        /// Уровни видимости документов в CAD-системе.
        /// </summary>
        public enum ApplicationDocumentsEnum
        {
            /// <summary>
            /// Документы, полностью загруженные в CAD-систему (видимые для пользователя).
            /// </summary>
            Visible,

            /// <summary>
            /// Все документы, загруженные в CAD-систему (видимые и фоновые).
            /// </summary>
            All,

            /// <summary>
            /// Документы, загруженные в CAD-систему в фоне (невидимые для пользователя).
            /// </summary>
            Invisible
        }
    }
}
