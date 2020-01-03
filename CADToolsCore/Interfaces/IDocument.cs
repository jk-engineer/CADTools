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

using CADToolsCore.Classes;
using CADToolsCore.Enumerators;

namespace CADToolsCore.Interfaces
{
    /// <summary>
    /// Документ.
    /// </summary>
    public interface IDocument
    {
        #region Свойства

        /// <summary>
        /// Имя документа, отображаемое в CAD системе.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Полное имя файла документа.
        /// </summary>
        string FullFileName { get; }

        /// <summary>
        /// Возвращает <see cref="true"/>, если документ не был сохранен после внесенных изменений.
        /// </summary>
        bool IsDirty { get; }

        /// <summary>
        /// Возвращает <see cref="true"/>, если документ является стандартным изделием.
        /// </summary>
        bool IsStandard { get; }

        /// <summary>
        /// Материал детали.
        /// </summary>
        IMaterial Material { get; set; }

        /// <summary>
        /// Обозначение документа.
        /// </summary>
        string Number { get; set; }

        /// <summary>
        /// Возвращает обозначение стандарта.
        /// </summary>
        string Standard { get; }

        /// <summary>
        /// Наименование документа.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Тип документа.
        /// </summary>
        DocumentType.DocumentTypeEnum Type { get; }

        /// <summary>
        /// Растровое изображение документа.
        /// </summary>
        System.Drawing.Image Thumbnail { get; }

        #endregion

        #region Методы

        /// <summary>
        /// Активирует документ в CAD системе.
        /// </summary>
        void Activate();

        /// <summary>
        /// Закрывает документ.
        /// </summary>
        void Close();

        /// <summary>
        /// Запускает печать документа.
        /// </summary>
        void Print();

        /// <summary>
        /// Сохраняет документ.
        /// </summary>
        void Save();

        #endregion
    }
}
