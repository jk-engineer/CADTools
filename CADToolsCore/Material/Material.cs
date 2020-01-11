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

namespace CADToolsCore.Material
{
    /// <summary>
    /// Материал изделия.
    /// </summary>
    public struct Material : IMaterial
    {
        #region Поля, свойства

        /// <summary>
        /// Типоразмер проката.
        /// </summary>
        public string AssortmentSize { get; set; }

        /// <summary>
        /// Полное название материала, представленное в виде одной строки.
        /// </summary>
        public string FullName
        {
            get
            {
                string resultValue = Name ?? string.Empty;
                resultValue += Separator + AssortmentSize ?? string.Empty;
                resultValue += Separator + Standard ?? string.Empty;
                return resultValue;
            }
        }

        /// <summary>
        /// Название материала или проката.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Обозначение стандарта на прокат.
        /// </summary>
        public string Standard { get; set; }

        /// <summary>
        /// Символ-разделитель, используемый в названии материала.
        /// </summary>
        public string Separator { get; private set; }

        #endregion

        #region Конструктор

        /// <summary>
        /// Новый экземпляр структуры.
        /// </summary>
        /// <param name="name">Название материала или проката.</param>
        /// <param name="assortmentSize">Типоразмер проката.</param>
        /// <param name="standard">Обозначение стандарта на прокат.</param>
        /// <param name="separator">Символ-разделитель, используемый в названии материала.</param>
        public Material(string name, string assortmentSize, string standard, string separator) : this()
        {
            Name = name ?? string.Empty;
            AssortmentSize = assortmentSize ?? string.Empty;
            Standard = standard ?? string.Empty;
            Separator = separator ?? string.Empty;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает полное название материала, представленное в виде одной строки.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => FullName;

        #endregion
    }
}
