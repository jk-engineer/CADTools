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

using System.Linq;

namespace CADToolsCore.Classes
{
    /// <summary>
    /// Класс для проверки типа документа.
    /// </summary>
    public static class CheckDocumentType
    {
        #region Методы

        /// <summary>
        /// Возвращает <see cref="true"/> при совпадении типа документа хотя бы с одним из указанных типов.
        /// </summary>
        /// <param name="document">Документ CAD-системы.</param>
        /// <param name="documentTypes">Типы документов для проверки.</param>
        /// <returns></returns>
        public static bool Invoke(IDocument document, DocumentType.DocumentTypeEnum[] documentTypes)
        {
            return documentTypes.Contains(document.DocumentType);
        }

        #endregion
    }
}
