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

using CADToolsCore.Enumerators;

namespace CADToolsCore.Interfaces
{
    /// <summary>
    /// Чертежный вид.
    /// </summary>
    public interface IDrawingView
    {
        #region Свойства

        /// <summary>
        /// Возвращает <see cref="true"/>, если вид содержит развертку листовой детали.
        /// </summary>
        bool IsFlatPattern { get; }

        /// <summary>
        /// Возвращает <see cref="true"/>, если угол поворота вида отличается от нуля.
        /// </summary>
        bool IsRotated { get; }

        /// <summary>
        /// Заголовок вида на чертеже.
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// Имя вида.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Лист чертежа, на котором находится вид.
        /// </summary>
        IDrawingSheet Parent { get; }

        /// <summary>
        /// Масштаб вида, выраженный в виде десятичного числа.
        /// </summary>
        double Scale { get; set; }

        /// <summary>
        /// Масштаб вида в формате текстовой строки (например, 1:2)
        /// </summary>
        string ScaleString { get; }

        /// <summary>
        /// Тип вида (главный, проекционный, разрез и т.д.).
        /// </summary>
        DrawingViewType.DrawingViewTypeEnum Type { get; }

        #endregion
    }
}
