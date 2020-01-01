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
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
#endregion

namespace CADToolsCore.Interfaces
{
    /// <summary>
    /// Документ-чертеж CAD-системы.
    /// </summary>
    public interface IDrawingDocument : IDocument
    {
        #region Свойства

        /// <summary>
        /// Документы, на который ссылается чертеж.
        /// </summary>
        IDocument[] ReferencedDocuments { get; }

        /// <summary>
        /// Возвращает набор листов чертежа.
        /// </summary>
        IDrawingSheet[] Sheets { get; }

        /// <summary>
        /// Масштаб, указанный в основной надписи, выраженный в виде десятичного числа.
        /// </summary>
        double Scale { get; set; }

        /// <summary>
        /// Масштаб, указанный в основной надписи, в формате текстовой строки (например, 1:2)
        /// </summary>
        string ScaleString { get; }

        #endregion
    }
}
