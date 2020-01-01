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
    /// Объект приложения CAD-системы.
    /// </summary>
    public interface ICADApplication
    {
        #region Свойства

        /// <summary>
        /// Активный документ CAD-системы.
        /// </summary>
        IDocument ActiveDocument { get; }

        #endregion

        #region Методы

        /// <summary>
        /// Активирует окно CAD-системы.
        /// </summary>
        void ActivateApplication();

        /// <summary>
        /// Закрывает в CAD-системе указанный документ.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла документа.</param>
        void CloseDocument(string fullFileName);

        /// <summary>
        /// Возвращает коллекцию документов из CAD-системы.
        /// </summary>
        /// <typeparam name="TDocument">Тип документа CAD-системы.</typeparam>
        /// <param name="applicationDocuments">Уровень видимости документов в CAD-системе.</param>
        /// <returns></returns>
        DocumentsCollection<TDocument> GetDocuments<TDocument>(ApplicationDocuments.ApplicationDocumentsEnum applicationDocuments)
            where TDocument : IDocument;

        /// <summary>
        /// Открывает указанный документ в CAD-системе и возвращает его.
        /// </summary>
        /// <typeparam name="TDocument">Тип документа CAD-системы.</typeparam>
        /// <param name="fullFileName">Полное имя файла документа.</param>
        /// <returns></returns>
        TDocument OpenDocument<TDocument>(string fullFileName) where TDocument : IDocument;

        #endregion
    }
}
