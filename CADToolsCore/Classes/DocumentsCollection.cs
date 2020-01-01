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

namespace CADToolsCore.Classes
{
    /// <summary>
    /// Коллекция документов. В качестве ключей используются полные имена файлов документов.
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public class DocumentsCollection<TDocument> : IDictionary<string, TDocument> where TDocument : IDocument
    {
        #region Поля, свойства

        /// <summary>
        /// Внутренний словарь с документами.
        /// </summary>
        private Dictionary<string, TDocument> _documentsCollection;

        #endregion

        #region Поля, свойства (реализация интерфейса)

        /// <summary>
        /// Возвращает или задает документ с указанным полным именем файла.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <returns></returns>
        public TDocument this[string fullFileName]
        {
            get { return _documentsCollection[fullFileName]; }
            set { _documentsCollection[fullFileName] = value; }
        }

        /// <summary>
        /// Возвращает число документов в коллекции.
        /// </summary>
        public int Count
        {
            get { return _documentsCollection.Count; }
        }

        /// <summary>
        /// Возвращает значение, указывающее, является ли коллекция доступной только для чтения.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Получает коллекцию, содержащую полные имена файлов документов.
        /// </summary>
        public ICollection<string> Keys
        {
            get { return _documentsCollection.Keys; }
        }

        /// <summary>
        /// Получает коллекцию, содержащую документы.
        /// </summary>
        public ICollection<TDocument> Values
        {
            get { return _documentsCollection.Values; }
        }

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        public DocumentsCollection()
        {
            _documentsCollection = new Dictionary<string, TDocument>();
        }

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        /// <param name="dictionary">Коллекция, элементы которой копируются в новый экземпляр класса.</param>
        public DocumentsCollection(IDictionary<string, TDocument> dictionary)
        {
            _documentsCollection = new Dictionary<string, TDocument>(dictionary);
        }

        #endregion

        #region Методы (реализация интерфейса)

        /// <summary>
        /// Метод не поддерживается. Используйте вместо него метод <see cref="AddDocument(TDocument)"/>.
        /// </summary>
        /// <param name="item"></param>
        [Obsolete]
        public void Add(KeyValuePair<string, TDocument> item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Метод не поддерживается. Используйте вместо него метод <see cref="AddDocument(TDocument)"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        [Obsolete]
        public void Add(string key, TDocument value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Удаляет все документы из коллекции.
        /// </summary>
        public void Clear()
        {
            _documentsCollection.Clear();
        }

        /// <summary>
        /// Метод не поддерживается. Используйте вместо него метод <see cref="AddDocument(TDocument)"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [Obsolete]
        public bool Contains(KeyValuePair<string, TDocument> item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Определяет, содержится ли документ с указанным именем файла в коллекции.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <returns></returns>
        public bool ContainsKey(string fullFileName)
        {
            return _documentsCollection.ContainsKey(fullFileName);
        }

        /// <summary>
        /// Метод не поддерживается.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        [Obsolete]
        public void CopyTo(KeyValuePair<string, TDocument>[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Возвращает перечислитель, осуществляющий перебор документов коллекции.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, TDocument>> GetEnumerator()
        {
            return _documentsCollection.GetEnumerator();
        }

        /// <summary>
        /// Метод не поддерживается.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [Obsolete]
        public bool Remove(KeyValuePair<string, TDocument> item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Удаляет документ с указанным именем файла из коллекции.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <returns></returns>
        public bool Remove(string fullFileName)
        {
            return _documentsCollection.Remove(fullFileName);
        }

        /// <summary>
        /// Получает документ с указанным именем файла.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <param name="document">Возвращаемый документ.</param>
        /// <returns></returns>
        public bool TryGetValue(string fullFileName, out TDocument document)
        {
            return _documentsCollection.TryGetValue(fullFileName, out document);
        }

        /// <summary>
        /// Возвращает перечислитель, осуществляющий перебор документов коллекции.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Методы (дополнительные)

        /// <summary>
        /// Добавляет указанный документ в коллекцию.
        /// </summary>
        /// <param name="document">Документ.</param>
        public void AddDocument(TDocument document)
        {
            string fullFileName = document.FullFileName;
            if (!_documentsCollection.ContainsKey(fullFileName))
            {
                _documentsCollection.Add(fullFileName, document);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public bool ContainsDocument(TDocument document)
        {
            return ContainsKey(document.FullFileName);
        }

        /// <summary>
        /// Возвращает индекс документа в коллекции по его имени файла.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <returns></returns>
        public int GetIndexByKey(string fullFileName)
        {
            return _documentsCollection.Keys.ToList().IndexOf(fullFileName);
        }

        /// <summary>
        /// Возвращает массив с именами файлов документов.
        /// </summary>
        /// <returns></returns>
        public string[] GetFileNames()
        {
            return _documentsCollection.Values.Select(doc => System.IO.Path.GetFileName(doc.FullFileName)).ToArray();
        }

        /// <summary>
        /// Возвращает коллекцию документов заданного типа.
        /// </summary>
        /// <param name="documentTypes">Набор требуемых типов документов.</param>
        /// <returns></returns>
        public DocumentsCollection<TDocument> GetDocumentsByType(DocumentType.DocumentTypeEnum[] documentTypes)
        {
            return (DocumentsCollection<TDocument>)_documentsCollection.Values.Where(doc => documentTypes.Contains(doc.DocumentType));
        }

        /// <summary>
        /// Возвращает документ по его полному или частичному имени.
        /// </summary>
        /// <param name="documentFileName">Имя файла искомого документа.</param>
        /// <returns></returns>
        public TDocument GetDocumentByName(string documentFileName)
        {
            return _documentsCollection.Values.Where(doc => doc.FullFileName.ToLower().Contains(documentFileName.ToLower())).FirstOrDefault();
        }

        /// <summary>
        /// Возвращает полное имя файла документа, если он содержится в коллекции.
        /// </summary>
        /// <param name="documentFileName">Имя файла искомого документа.</param>
        /// <returns></returns>
        public string GetFullFileName(string documentFileName)
        {
            var resultValue = string.Empty;
            try
            {
                resultValue = GetDocumentByName(documentFileName).FullFileName;
            }
            catch (System.Exception)
            {
            }
            return resultValue;
        }

        /// <summary>
        /// Возвращает отсортированную коллекцию документов.
        /// </summary>
        /// <returns></returns>
        public DocumentsCollection<TDocument> GetSortedCollection()
        {
            var sortedCollection = new SortedDictionary<string, TDocument>(_documentsCollection);
            return new DocumentsCollection<TDocument>(sortedCollection);
        }

        /// <summary>
        /// Выполняет сортировку коллекции документов.
        /// </summary>
        public void Sort()
        {
            var sortedCollection = new SortedDictionary<string, TDocument>(_documentsCollection);
            _documentsCollection.Clear();
            for (var index = 0; index < sortedCollection.Count; index++)
            {
                this.AddDocument(sortedCollection.ElementAt(index).Value);
            }
        }

        #endregion
    }
}
