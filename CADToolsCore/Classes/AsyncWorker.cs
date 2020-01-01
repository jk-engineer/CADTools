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
using System.ComponentModel;
using System.Windows.Forms;
using CADToolsCore.Interfaces;

namespace CADToolsCore.Classes
{
    /// <summary>
    /// Класс для выполнения операции в отдельном потоке.
    /// </summary>
    public class AsyncWorker : IWorker
    {
        #region Поля, свойства

        /// <summary>
        /// Делегат операции, выполняемой асинхронно.
        /// </summary>
        /// <param name="index">Индекс для итерации.</param>
        /// <returns></returns>
        public delegate void AsyncOperationDelegate(int index);

        /// <summary>
        /// Делегат операции, выполняемой асинхронно.
        /// </summary>
        private AsyncOperationDelegate _asyncOperationDelegate;

        /// <summary>
        /// Объект <see cref="BackgroundWorker"/>.
        /// </summary>
        private BackgroundWorker _worker = new BackgroundWorker()
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };

        /// <summary>
        /// Возвращает <see cref="true"/> при выполнении операции в текущий момент.
        /// </summary>
        public bool IsBusy
        {
            get { return _worker.IsBusy; }
        }

        /// <summary>
        /// Процент выполнения операции.
        /// </summary>
        private int _progressPercentage = 0;

        /// <summary>
        /// Возвращает процент выполнения операции.
        /// </summary>
        public int ProgressPercentage
        {
            get { return _progressPercentage; }
        }

        /// <summary>
        /// Сообщение, выводимое при отмене операции.
        /// </summary>
        private string _processCancelMessage = "Операция отменена!";

        /// <summary>
        /// Сообщение, выводимое при отмене операции.
        /// </summary>
        public string ProcessCancelMessage
        {
            get { return _processCancelMessage; }
            set { _processCancelMessage = value; }
        }

        /// <summary>
        /// Сообщение, выводимое при окончании операции.
        /// </summary>
        private string _processEndMessage = "Операция завершена.";

        /// <summary>
        /// Сообщение, выводимое при окончании операции.
        /// </summary>
        public string ProcessEndMessage
        {
            get { return _processEndMessage; }
            set { _processEndMessage = value; }
        }

        /// <summary>
        /// Флаг остановки асинхронной операции.
        /// </summary>
        private bool _stopAsyncFlag = false;

        /// <summary>
        /// Начальное значение счетчика цикла в асинхронной операции.
        /// </summary>
        private int _loopStartCount = 0;

        /// <summary>
        /// Конечное значение счетчика цикла в асинхронной операции.
        /// </summary>
        private int _loopEndCount = 0;

        #endregion

        #region События

        /// <summary>
        /// Происходит при запуске операции.
        /// </summary>
        public event Action ProcessBegin;

        /// <summary>
        /// Вызывает событие <see cref="ProcessBegin"/>.
        /// </summary>
        protected virtual void OnProcessBegin()
        {
            ProcessBegin?.Invoke();
        }

        /// <summary>
        /// Происходит при отмене операции.
        /// </summary>
        public event Action ProcessCancel;

        /// <summary>
        /// Вызывает событие <see cref="ProcessCancel"/>.
        /// </summary>
        protected virtual void OnProcessCancel()
        {
            ProcessCancel?.Invoke();
        }

        /// <summary>
        /// Происходит при завершении операции.
        /// </summary>
        public event Action ProcessEnd;

        /// <summary>
        /// Вызывает событие <see cref="ProcessEnd"/>.
        /// </summary>
        protected virtual void OnProcessEnd()
        {
            ProcessEnd?.Invoke();
        }

        /// <summary>
        /// Происходит при ошибке выполнения операции.
        /// </summary>
        public event Action ProcessError;

        /// <summary>
        /// Вызывает событие <see cref="ProcessError"/>.
        /// </summary>
        protected virtual void OnProcessError()
        {
            ProcessError?.Invoke();
        }

        /// <summary>
        /// Происходит при изменении прогресса выполнения операции.
        /// </summary>
        public event Action ProgressChanged;

        /// <summary>
        /// Вызывает событие <see cref="ProgressChanged"/>.
        /// </summary>
        protected virtual void OnProgressChanged()
        {
            ProgressChanged?.Invoke();
        }

        #endregion

        #region Конструктор класса

        /// <summary>
        /// Новый экземпляр класса.
        /// </summary>
        public AsyncWorker()
        {
            _worker.DoWork += _worker_DoWork;
            _worker.ProgressChanged += _worker_ProgressChanged;
            _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Запускает выполнение указанной операции (пустой метод-заглушка, используйте перегруженный вариант).
        /// </summary>
        [Obsolete]
        public virtual void StartProcess()
        {
        }

        /// <summary>
        /// Запускает выполнение указанной операции.
        /// </summary>
        /// <param name="asyncOperationDelegate">Делегат операции, которую необходимо выполнить.</param>
        /// <param name="loopEndCount">Конечное значение счетчика цикла в асинхронной операции.</param>
        /// <param name="loopStartCount">Начальное значение счетчика цикла в асинхронной операции.</param>
        public virtual void StartProcess(AsyncOperationDelegate asyncOperationDelegate,
                                         int loopEndCount, int loopStartCount = 0)
        {
            // Запрет на запуск более одной синхронной операции.
            if (_worker.IsBusy)
            {
                return;
            }
            _asyncOperationDelegate = asyncOperationDelegate;
            _progressPercentage = 0;
            _loopStartCount = loopStartCount;
            _loopEndCount = loopEndCount;
            _stopAsyncFlag = false;
            OnProcessBegin();
            // Запуск асинхронной операции.
            _worker.RunWorkerAsync();
        }

        /// <summary>
        /// Прерывает выполнение операции.
        /// </summary>
        public void StopProcess()
        {
            _stopAsyncFlag = true;
        }

        #endregion

        #region Асинхронные операции

        /// <summary>
        /// Основная операция, выполняемая в фоне.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;
            for (int index = _loopStartCount; index < _loopEndCount; index++)
            {
                // Вызов операции.
                _asyncOperationDelegate(index);
                // Отчет об изменении прогресса выполнения.
                worker.ReportProgress(index / _loopEndCount * 100);
                // Выход из цикла при прерывании операции.
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Изменение прогресса выполнения операции.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Обновление процента выполнения операции.
            _progressPercentage = e.ProgressPercentage;
            OnProgressChanged();
            // Прерывание асинхронной операции.
            if (_worker.IsBusy & _stopAsyncFlag)
            {
                _worker.CancelAsync();
            }
        }

        /// <summary>
        /// Завершение операции.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show(_processCancelMessage, "Отчет", MessageBoxButtons.OK);
                OnProcessCancel();
            }
            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                OnProcessError();
            }
            else
            {
                // Отчет об успешном выполнении.
                MessageBox.Show(_processEndMessage, "Отчет", MessageBoxButtons.OK);
            }
            OnProcessEnd();
        }

        #endregion
    }
}
