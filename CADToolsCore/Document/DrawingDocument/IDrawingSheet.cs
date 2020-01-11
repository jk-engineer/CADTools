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
    /// Лист чертежа.
    /// </summary>
    public interface IDrawingSheet
    {
        #region Свойства

        /// <summary>
        /// Высота листа в миллиметрах.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Возвращает <see cref="true"/>, если лист имеет альбомную ориентацию.
        /// </summary>
        bool Landscape { get; }

        /// <summary>
        /// Чертеж, в котором содержится лист.
        /// </summary>
        IDrawingDocument Parent { get; }

        /// <summary>
        /// Формат листа чертежа согласно ГОСТ 2.301-68.
        /// </summary>
        DrawingSheetSize.DrawingSheetSizeEnum Size { get; }

        /// <summary>
        /// Возвращает набор видов на листе чертежа.
        /// </summary>
        IDrawingView[] Views { get; }

        /// <summary>
        /// Ширина листа в миллиметрах.
        /// </summary>
        int Width { get; }

        #endregion
    }
}
