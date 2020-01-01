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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CADToolsCore.Enumerators;
using CADToolsCore.Interfaces;

namespace CADToolsCore.Classes
{
    /// <summary>
    /// Коллекция документов. В качестве ключей используются полные имена файлов документов.
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public class DocumentsCollection<TDocument> : IDocumentsCollection<TDocument> where TDocument : IDocument
    {
        #region Поля, свойства

        /// <summary>
        /// Внутренний словарь с документами.
        /// </summary>
        private Dictionary<string, TDocument> _documents;

        #endregion

        #region Поля, свойства (реализация интерфейса)

        /// <summary>
        /// Возвращает или задает документ с указанным полным именем файла.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <returns></returns>
        public TDocument this[string fullFileName] => _documents[fullFileName];

        /// <summary>
        /// Возвращает число документов в коллекции.
        /// </summary>
        public int Count => _documents.Count;

        /// <summary>
        /// Возвращает значение, указывающее, является ли коллекция доступной только для чтения.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Получает коллекцию, содержащую полные имена файлов документов.
        /// </summary>
        public ICollection<string> Keys => _documents.Keys;

        /// <summary>
        /// Получает коллекцию, содержащую документы.
        /// </summary>
        public ICollection<TDocument> Values => _documents.Values;

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        public DocumentsCollection()
        {
            _documents = new Dictionary<string, TDocument>();
        }

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        /// <param name="documents">Коллекция, элементы которой копируются в новый экземпляр класса.</param>
        public DocumentsCollection(IDocumentsCollection<TDocument> documents) : this()
        {
            foreach (TDocument doc in documents)
            {
                _documents.Add(doc.FullFileName, doc);
            }
        }

        #endregion

        #region Методы (реализация интерфейса)

        /// <summary>
        /// Добавляет документ в коллекцию.
        /// </summary>
        /// <param name="document">Документ.</param>
        public void Add(TDocument document)
        {
            string fullFileName = document.FullFileName;
            if (!_documents.ContainsKey(fullFileName))
            {
                _documents.Add(fullFileName, document);
            }
        }

        /// <summary>
        /// Удаляет все документы из коллекции.
        /// </summary>
        public void Clear() => _documents.Clear();

        /// <summary>
        /// Определяет, содержится ли документ с указанным именем файла в коллекции.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <returns></returns>
        public bool Contains(string fullFileName) => _documents.ContainsKey(fullFileName);

        /// <summary>
        /// Определяет, содержится ли документ в коллекции.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <returns></returns>
        public bool Contains(TDocument document) => this.Contains(document.FullFileName);

        /// <summary>
        /// Удаляет документ с указанным именем файла из коллекции.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <returns></returns>
        public bool Remove(string fullFileName) => _documents.Remove(fullFileName);

        /// <summary>
        /// Удаляет документ из коллекции.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <returns></returns>
        public bool Remove(TDocument document) => this.Remove(document.FullFileName);

        /// <summary>
        /// Получает документ с указанным именем файла.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <param name="document">Возвращаемый документ.</param>
        /// <returns></returns>
        public bool TryGetValue(string fullFileName, out TDocument document) => _documents.TryGetValue(fullFileName, out document);

        /// <summary>
        /// Возвращает индекс документа в коллекции по его имени файла.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <returns></returns>
        public int GetIndexByKey(string fullFileName) => _documents.Keys.ToList().IndexOf(fullFileName);

        /// <summary>
        /// Возвращает массив с именами файлов документов.
        /// </summary>
        /// <returns></returns>
        public string[] GetFileNames() => _documents.Keys.Select(key => System.IO.Path.GetFileName(key)).ToArray();

        /// <summary>
        /// Возвращает коллекцию документов заданного типа.
        /// </summary>
        /// <param name="documentTypes">Набор требуемых типов документов.</param>
        /// <returns></returns>
        public IDocumentsCollection<TDocument> GetDocumentsByType(DocumentType.DocumentTypeEnum[] documentTypes)
        {
            return (IDocumentsCollection<TDocument>)_documents.Values.Where(doc => documentTypes.Contains(doc.DocumentType));
        }

        /// <summary>
        /// Возвращает документ по его полному или частичному имени.
        /// </summary>
        /// <param name="documentFileName">Имя файла искомого документа.</param>
        /// <returns></returns>
        public TDocument GetDocumentByName(string documentFileName)
        {
            return _documents.Values.Where(doc => doc.FullFileName.ToLower().Contains(documentFileName.ToLower())).FirstOrDefault();
        }

        /// <summary>
        /// Возвращает полное имя файла документа, если он содержится в коллекции.
        /// </summary>
        /// <param name="documentFileName">Имя файла искомого документа.</param>
        /// <returns></returns>
        public string GetFullFileName(string documentFileName)
        {
            string resultValue = string.Empty;
            try
            {
                resultValue = GetDocumentByName(documentFileName).FullFileName;
            }
            catch (System.Exception)
            {
            }
            return resultValue;
        }

        public void CopyTo(TDocument[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TDocument> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
