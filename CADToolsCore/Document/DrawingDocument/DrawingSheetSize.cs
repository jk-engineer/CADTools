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

namespace CADToolsCore.Document.DrawingDocument
{
    /// <summary>
    /// Формат листа чертежа согласно ГОСТ 2.301.
    /// </summary>
    public static class DrawingSheetSize
    {
        /// <summary>
        /// Формат листа чертежа согласно ГОСТ 2.301.
        /// </summary>
        public enum DrawingSheetSizeEnum
        {
            /// <summary>
            /// Формат А0.
            /// </summary>
            A0,

            /// <summary>
            /// Формат А1.
            /// </summary>
            A1,

            /// <summary>
            /// Формат А2.
            /// </summary>
            A2,

            /// <summary>
            /// Формат А3.
            /// </summary>
            A3,

            /// <summary>
            /// Формат А4.
            /// </summary>
            A4,

            /// <summary>
            /// Формат А5.
            /// </summary>
            A5,

            /// <summary>
            /// Формат А0x2.
            /// </summary>
            A0x2,

            /// <summary>
            /// Формат А0x3.
            /// </summary>
            A0x3,


            /// <summary>
            /// Формат А1x3.
            /// </summary>
            A1x3,

            /// <summary>
            /// Формат А1x4.
            /// </summary>
            A1x4,

            /// <summary>
            /// Формат А2x3.
            /// </summary>
            A2x3,

            /// <summary>
            /// Формат А2x4.
            /// </summary>
            A2x4,

            /// <summary>
            /// Формат А2x5.
            /// </summary>
            A2x5,

            /// <summary>
            /// Формат А3x3.
            /// </summary>
            A3x3,

            /// <summary>
            /// Формат А3x4.
            /// </summary>
            A3x4,

            /// <summary>
            /// Формат А3x5.
            /// </summary>
            A3x5,

            /// <summary>
            /// Формат А3x6.
            /// </summary>
            A3x6,

            /// <summary>
            /// Формат А3x7.
            /// </summary>
            A3x7,

            /// <summary>
            /// Формат А4x3.
            /// </summary>
            A4x3,

            /// <summary>
            /// Формат А4x4.
            /// </summary>
            A4x4,

            /// <summary>
            /// Формат А4x5.
            /// </summary>
            A4x5,

            /// <summary>
            /// Формат А4x6.
            /// </summary>
            A4x6,

            /// <summary>
            /// Формат А4x7.
            /// </summary>
            A4x7,

            /// <summary>
            /// Формат А4x8.
            /// </summary>
            A4x8,

            /// <summary>
            /// Формат А4x9.
            /// </summary>
            A4x9
        }
    }
}
