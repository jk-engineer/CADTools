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
    /// Объект для выполнения операции, в том числе асинхронной.
    /// </summary>
    public interface IWorker
    {
        #region Свойства

        /// <summary>
        /// Возвращает <see cref="true"/> при выполнении операции в текущий момент.
        /// </summary>
        bool IsBusy { get; }

        /// <summary>
        /// Возвращает процент выполнения операции.
        /// </summary>
        int ProgressPercentage { get; }

        #endregion

        #region События

        /// <summary>
        /// Происходит при запуске операции.
        /// </summary>
        event System.Action ProcessBegin;

        /// <summary>
        /// Происходит при отмене операции.
        /// </summary>
        event System.Action ProcessCancel;

        /// <summary>
        /// Происходит при завершении операции.
        /// </summary>
        event System.Action ProcessEnd;

        /// <summary>
        /// Происходит при ошибке выполнения операции.
        /// </summary>
        event System.Action ProcessError;

        /// <summary>
        /// Происходит при изменении прогресса выполнения операции.
        /// </summary>
        event System.Action ProgressChanged;

        #endregion

        #region Методы

        /// <summary>
        /// Запускает выполнение операции.
        /// </summary>
        void StartProcess();

        /// <summary>
        /// Прерывает выполнение операции.
        /// </summary>
        void StopProcess();

        #endregion
    }
}
