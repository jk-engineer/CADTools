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
    /// Документ CAD-системы.
    /// </summary>
    public interface IDocument
    {
        #region Свойства

        /// <summary>
        /// Возвращает <see cref="true"/>, если документ не был сохранен после внесенных изменений.
        /// </summary>
        bool Dirty { get; }

        /// <summary>
        /// Имя документа, отображаемое в CAD системе.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Обозначение документа.
        /// </summary>
        string DocumentNumber { get; set; }

        /// <summary>
        /// Наименование документа.
        /// </summary>
        string DocumentTitle { get; set; }

        /// <summary>
        /// Тип документа.
        /// </summary>
        DocumentType.DocumentTypeEnum DocumentType { get; }

        /// <summary>
        /// Полное имя файла документа.
        /// </summary>
        string FullFileName { get; }

        /// <summary>
        /// Материал детали.
        /// </summary>
        Material PartMaterial { get; set; }

        /// <summary>
        /// Возвращает <see cref="true"/>, если документ является стандартным изделием.
        /// </summary>
        bool Standard { get; }

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
        /// Возвращает расширение файла заданного типа документа.
        /// </summary>
        /// <param name="documentType">Тип документа.</param>
        /// <returns></returns>
        string GetDocumentTypeFileExtension(DocumentType.DocumentTypeEnum documentType);

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
