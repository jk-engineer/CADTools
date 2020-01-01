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

namespace CADToolsCore.Classes
{
    /// <summary>
    /// Материал изделия.
    /// </summary>
    public class Material
    {
        #region Поля, свойства

        /// <summary>
        /// Название материала или проката.
        /// </summary>
        private string _materialName = string.Empty;

        /// <summary>
        /// Название материала или проката.
        /// </summary>
        public string MaterialName
        {
            get { return _materialName; }
            set { _materialName = value; }
        }

        /// <summary>
        /// Типоразмер проката.
        /// </summary>
        private string _assortmentSize = string.Empty;

        /// <summary>
        /// Типоразмер проката.
        /// </summary>
        public string AssortmentSize
        {
            get { return _assortmentSize; }
            set { _assortmentSize = value; }
        }

        /// <summary>
        /// Обозначение стандарта на прокат.
        /// </summary>
        private string _materialStandard = string.Empty;

        /// <summary>
        /// Обозначение стандарта на прокат.
        /// </summary>
        public string MaterialStandard
        {
            get { return _materialStandard; }
            set { _materialStandard = value; }
        }

        /// <summary>
        /// Символ-разделитель, используемый в названии материала.
        /// </summary>
        public const string MATERIAL_NAME_SEPARATOR = "_";

        /// <summary>
        /// Полное название материала, представленное в виде одной строки.
        /// </summary>
        public string MaterialFullName
        {
            get
            {
                string resultValue = _materialName;
                resultValue += (string.IsNullOrWhiteSpace(_assortmentSize)) ? string.Empty : MATERIAL_NAME_SEPARATOR + _assortmentSize;
                resultValue += (string.IsNullOrWhiteSpace(_materialStandard)) ? string.Empty : MATERIAL_NAME_SEPARATOR + _materialStandard;
                return resultValue;
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает полное название материала, представленное в виде одной строки.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return MaterialFullName;
        }

        #endregion
    }
}
