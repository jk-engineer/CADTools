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

using System.Collections.Generic;
using CADToolsCore.Enumerators;

namespace CADToolsCore.Interfaces
{
    /// <summary>
    /// Коллекция документов. В качестве ключей используются полные имена файлов документов.
    /// </summary>
    /// <typeparam name="TDocument">Документ CAD-системы.</typeparam>
    public interface IDocumentsCollection<TDocument> : ICollection<TDocument> where TDocument : IDocument
    {
        /// <summary>
        /// Возвращает документ по указанному имени.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <returns></returns>
        TDocument this[string fullFileName] { get; }

        #region Свойства

        /// <summary>
        /// Возвращает коллекцию, содержащую полные имена файлов документов.
        /// </summary>
        ICollection<string> Keys { get; }

        /// <summary>
        /// Возвращает коллекцию, содержащую документы.
        /// </summary>
        ICollection<TDocument> Values { get; }

        #endregion

        #region Методы

        /// <summary>
        /// Определяет, содержится ли документ с указанным именем файла в коллекции.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <returns></returns>
        bool Contains(string fullFileName);

        /// <summary>
        /// Возвращает документ по его полному или частичному имени.
        /// </summary>
        /// <param name="documentFileName">Имя файла искомого документа.</param>
        /// <returns></returns>
        TDocument GetDocumentByName(string documentFileName);

        /// <summary>
        /// Возвращает коллекцию документов заданного типа.
        /// </summary>
        /// <param name="documentTypes">Набор требуемых типов документов.</param>
        /// <returns></returns>
        IDocumentsCollection<TDocument> GetDocumentsByType(DocumentType.DocumentTypeEnum[] documentTypes);

        /// <summary>
        /// Возвращает массив с именами файлов документов.
        /// </summary>
        /// <returns></returns>
        string[] GetFileNames();

        /// <summary>
        /// Возвращает полное имя файла документа, если он содержится в коллекции.
        /// </summary>
        /// <param name="documentFileName">Имя файла искомого документа.</param>
        /// <returns></returns>
        string GetFullFileName(string documentFileName);

        /// <summary>
        /// Возвращает индекс документа в коллекции по его имени файла.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <returns></returns>
        int GetIndexByKey(string fullFileName);

        /// <summary>
        /// Удаляет документ с указанным именем файла из коллекции.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <returns></returns>
        bool Remove(string fullFileName);

        /// <summary>
        /// Получает документ с указанным именем файла.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <param name="document">Возвращаемый документ.</param>
        /// <returns></returns>
        bool TryGetValue(string fullFileName, out TDocument document);

        #endregion
    }
}
