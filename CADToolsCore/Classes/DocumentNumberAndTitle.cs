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

using System.Linq;

namespace CADToolsCore.Classes
{
    /// <summary>
    /// Класс для работы с обозначением и наименованием документа.
    /// </summary>
    public static class DocumentNumberAndTitle
    {
        #region Методы

        /// <summary>
        /// Разделяет имя документа на обозначение и наименование.
        /// </summary>
        /// <param name="documentName">Имя документа.</param>
        /// <param name="separator">Подстрока, по которой выполняется разделение на обозначение и наименование.</param>
        /// <param name="occurenceNumber">Номер вхождения подстроки-разделителя.</param>
        /// <param name="documentNumber">Возвращает обозначение документа.</param>
        /// <param name="documentTitle">Возвращает наименование документа.</param>
        public static void Split(string documentName, string separator, int occurenceNumber,
                                 out string documentNumber, out string documentTitle)
        {
            if (string.IsNullOrEmpty(documentName) || string.IsNullOrEmpty(separator))
            {
                documentNumber = string.Empty;
                documentTitle = string.Empty;
                return;
            }
            // Разделение имени документа на подстроки.
            string[] separatedDocumentName = documentName.Split(new string[] { separator },
                                                                System.StringSplitOptions.RemoveEmptyEntries);

            // Для обращения к элементам массива вводится переменная,
            // при этом ее значение не должно выходить за границы строкового массива.
            int substringCount = (occurenceNumber > separatedDocumentName.Count()) ? separatedDocumentName.Count() : occurenceNumber;
            string[] separatedPartNumber = separatedDocumentName.ToList().GetRange(0, substringCount).ToArray();

            // Обозначение документа.
            documentNumber = string.Join(separator, separatedPartNumber);
            // Наименование документа.
            documentTitle = (string.IsNullOrWhiteSpace(documentNumber)) ? documentName :
                             documentName.Replace(documentNumber, string.Empty).TrimStart(separator.ToCharArray());
        }

        #endregion
    }
}
