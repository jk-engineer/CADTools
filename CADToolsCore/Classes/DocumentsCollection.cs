﻿#region Copyright
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
    /// <typeparam name="TDocument">Документ CAD-системы.</typeparam>
    public class DocumentsCollection<TDocument> : IDocumentsCollection<TDocument> where TDocument : IDocument
    {
        #region Поля, свойства

        /// <summary>
        /// Внутренний словарь с документами.
        /// </summary>
        private readonly SortedDictionary<string, TDocument> _documents;

        #endregion

        #region Поля, свойства (реализация интерфейса)

        /// <summary>
        /// Возвращает документ по указанному имени.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <returns></returns>
        public TDocument this[string fullFileName] => _documents[fullFileName];

        /// <summary>
        /// Возвращает документ по указанному индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns></returns>
        public TDocument this[int index] => _documents.ElementAt(index).Value;

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
            _documents = new SortedDictionary<string, TDocument>();
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
        /// Выполняет копирование элементов коллекции в массив, начиная с указанного индекса.
        /// </summary>
        /// <param name="array">Массив, в который выполняется копирование.</param>
        /// <param name="arrayIndex">Индекс в массиве, начиная с которого заполняется массив.</param>
        public void CopyTo(TDocument[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("Ошибка обращения к массиву.");
            }
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("Недопустимое значение индекса.");
            }
            if (array.Length - arrayIndex < this.Count)
            {
                throw new ArgumentException("Недостаточно элементов в массиве для выполнения копирования.");
            }
            for (int index = 0; index < this.Count; index++)
            {
                array[index + arrayIndex] = this[index];
            }
        }

        /// <summary>
        /// Возвращает документ по его полному или частичному имени.
        /// </summary>
        /// <param name="documentFileName">Имя файла искомого документа.</param>
        /// <returns></returns>
        public TDocument GetDocumentByName(string documentFileName) =>
            _documents.Values.Where(doc => doc.FullFileName.ToLower().Contains(documentFileName.ToLower())).FirstOrDefault();

        /// <summary>
        /// Возвращает коллекцию документов заданного типа.
        /// </summary>
        /// <param name="documentTypes">Набор требуемых типов документов.</param>
        /// <returns></returns>
        public IDocumentsCollection<TDocument> GetDocumentsByType(DocumentType.DocumentTypeEnum[] documentTypes) =>
            (IDocumentsCollection<TDocument>)_documents.Values.Where(doc => documentTypes.Contains(doc.Type));

        /// <summary>
        /// Возвращает перечислитель, осуществляющий перебор документов коллекции.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TDocument> GetEnumerator() => _documents.Values.GetEnumerator();

        /// <summary>
        /// Возвращает перечислитель, осуществляющий перебор документов коллекции.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// Возвращает массив с именами файлов документов.
        /// </summary>
        /// <returns></returns>
        public string[] GetFileNames() => _documents.Keys.Select(key => System.IO.Path.GetFileName(key)).ToArray();

        /// <summary>
        /// Возвращает полное имя файла документа, если он содержится в коллекции.
        /// </summary>
        /// <param name="documentFileName">Имя файла искомого документа.</param>
        /// <returns></returns>
        public string GetFullFileName(string documentFileName)
        {
            TDocument doc = this.GetDocumentByName(documentFileName);
            return (doc != null) ? doc.FullFileName : String.Empty;
        }

        /// <summary>
        /// Возвращает индекс документа в коллекции по его имени файла.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <returns></returns>
        public int GetIndexByKey(string fullFileName) => _documents.Keys.ToList().IndexOf(fullFileName);

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
        /// Удаляет документ с указанным индексом из коллекции.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns></returns>
        public bool Remove(int index) => this.Remove(this[index]);

        /// <summary>
        /// Получает документ с указанным именем файла.
        /// </summary>
        /// <param name="fullFileName">Полное имя файла.</param>
        /// <param name="document">Возвращаемый документ.</param>
        /// <returns></returns>
        public bool TryGetValue(string fullFileName, out TDocument document) => _documents.TryGetValue(fullFileName, out document);

        #endregion
    }
}
