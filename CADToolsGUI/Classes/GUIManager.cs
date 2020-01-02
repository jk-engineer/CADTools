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

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CADToolsGUI.Buttons;

namespace CADToolsGUI.Classes
{
    /// <summary>
    /// Класс для работы с элементами пользовательского интерфейса.
    /// </summary>
    public static class GUIManager
    {
        #region Поля, свойства

        /// <summary>
        /// Способ вписывания размера элемента интерфейса.
        /// </summary>
        public enum FitSizeMode
        {
            /// <summary>
            /// Вписать по ширине.
            /// </summary>
            FitWidth,
            /// <summary>
            /// Вписать по высоте.
            /// </summary>
            FitHeight,
            /// <summary>
            /// Вписать по ширине и высоте.
            /// </summary>
            FitWidthAndHeight
        }

        #endregion

        #region Методы

        /// <summary>
        /// Центрирует по горизонтали указанный элемент интерфейса относительно его контейнера.
        /// </summary>
        /// <param name="control">Элемент интерфейса.</param>
        public static void CenterControlHorizontallyByContainer(Control control)
        {
            // Для корректного расчета координат точка привязки элемента приводится к Top, Left.
            control.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            Control container = GetContainer(control);
            if (container == null)
            {
                return;
            }
            // Для правильного расположения элемента на форме необходимо ввести поправку.
            // Для других контейнеров поправка не нужна.
            var correction = 0;
            if (CheckControlType(control, typeof(Form)))
            {
                correction = GUISizes.LEFT_CORRECTION;
            }
            control.Left = (container.Width - control.Width - correction) / 2;
        }

        /// <summary>
        /// Центрирует по горизонтали набор элементов интерфейса относительно контейнера.
        /// </summary>
        /// <param name="controls">Набор элементов интерфейса.</param>
        public static void CenterControlHorizontallyByContainer(Control[] controls)
        {
            foreach (var control in controls)
            {
                CenterControlHorizontallyByContainer(control);
            }
        }

        /// <summary>
        /// Центрирует по вертикали указанный элемент интерфейса относительно его контейнера.
        /// </summary>
        /// <param name="control">Элемент интерфейса.</param>
        public static void CenterControlVerticallyByContainer(Control control)
        {
            // Для корректного расчета координат точка привязки элемента приводится к Top, Left.
            control.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            Control container = GetContainer(control);
            if (container == null)
            {
                return;
            }
            // Для правильного расположения элемента на форме необходимо ввести поправку.
            // Также необходима поправка в элементах GroupBox и PictureBox.
            // Для других контейнеров поправка не нужна.
            var correction = 0;
            if (CheckControlType(control, typeof(Form)))
            {
                correction = GUISizes.TOP_CORRECTION;
            }
            if (CheckControlType(control, typeof(GroupBox)))
            {
                correction = -6;
            }
            if (CheckControlType(control, typeof(PictureBox)))
            {
                correction = GUISizes.TOP_CORRECTION - 6;
            }
            control.Top = (container.Height - control.Height - correction) / 2;
        }

        /// <summary>
        /// Центрирует по вертикали набор элементов интерфейса относительно контейнера.
        /// </summary>
        /// <param name="controls">Набор элементов интерфейса.</param>
        public static void CenterControlVerticallyByContainer(Control[] controls)
        {
            foreach (var control in controls)
            {
                CenterControlVerticallyByContainer(control);
            }
        }

        /// <summary>
        /// Центрирует указанный элемент интерфейса относительно его контейнера.
        /// </summary>
        /// <param name="control">Элемент интерфейса.</param>
        public static void CenterControlByContainer(Control control)
        {
            CenterControlHorizontallyByContainer(control);
            CenterControlVerticallyByContainer(control);
        }

        /// <summary>
        /// Центрирует набор элементов интерфейса относительно контейнера.
        /// </summary>
        /// <param name="controls">Набор элементов интерфейса.</param>
        public static void CenterControlByContainer(Control[] controls)
        {
            CenterControlHorizontallyByContainer(controls);
            CenterControlVerticallyByContainer(controls);
        }

        /// <summary>
        /// Размещает элементы интерфейса через равные промежутки по горизонтали.
        /// </summary>
        /// <param name="controls">Набор элементов интерфейса. Первый элемент является базовым и не меняет своего положения.</param>
        /// <param name="horizontalOffset">Расстояние между элементами интерфейса по горизонтали.</param>
        public static void PlaceControlsHorizontally(Control[] controls, int horizontalOffset)
        {
            Control controlObj;
            // Первый элемент в массиве является базовым и не меняет своего положения.
            // Поэтому обработка начинается со второго элемента массива, и верхняя граница цикла должна быть на 2 меньше,
            // чем количество элементов в массиве.
            for (var index = 0; index < controls.Count() - 1; index++)
            {
                controlObj = controls[index];
                controlObj.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                controls[index + 1].Anchor = AnchorStyles.Left | AnchorStyles.Top;
                controls[index + 1].Left = controlObj.Left + controlObj.Width + horizontalOffset;
            }
        }

        /// <summary>
        /// Размещает элементы интерфейса через равные промежутки по вертикали.
        /// </summary>
        /// <param name="controls">Набор элементов интерфейса. Первый элемент является базовым и не меняет своего положения.</param>
        /// <param name="verticalOffset">Расстояние между элементами интерфейса по вертикали.</param>
        public static void PlaceControlsVertically(Control[] controls, int verticalOffset)
        {
            Control controlObj;
            // Первый элемент в массиве является базовым и не меняет своего положения.
            // Поэтому обработка начинается со второго элемента массива, и верхняя граница цикла должна быть на 2 меньше,
            // чем количество элементов в массиве.
            for (var index = 0; index < controls.Count() - 1; index++)
            {
                controlObj = controls[index];
                controlObj.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                controls[index + 1].Anchor = AnchorStyles.Left | AnchorStyles.Top;
                controls[index + 1].Top = controlObj.Top + controlObj.Height + verticalOffset;
            }
        }

        /// <summary>
        /// Размещает элементы интерфейса по горизонтали и вертикали.
        /// </summary>
        /// <param name="controls">Набор элементов интерфейса. Первый элемент является базовым и не меняет своего положения.</param>
        /// <param name="horizontalOffset">Расстояние между элементами интерфейса по горизонтали.</param>
        /// <param name="verticalOffset">Расстояние между элементами интерфейса по вертикали.</param>
        /// <param name="columnCount">Количество столбцов при размещении элементов интерфейса.
        /// <para>Задайте значение, отличное от нуля, чтобы получить необходимое количество столбцов.</para>
        /// Данный параметр имеет преимущество перед параметром rowCount.</param>
        /// <param name="rowCount">Количество рядов при размещении элементов интерфейса.
        /// <para>Задайте значение, отличное от нуля, чтобы получить необходимое количество рядов.</para>
        /// Игнорируется, если параметр columnCount отличен от нуля.</param>
        public static void PlaceControls(Control[] controls, int horizontalOffset, int verticalOffset,
                                         int columnCount = 0, int rowCount = 0)
        {
            // Проверка количества столбцов и рядов.
            columnCount = System.Math.Abs(columnCount);
            rowCount = System.Math.Abs(rowCount);
            if (columnCount == 0 & rowCount == 0)
            {
                return;
            }
            var controlList = new List<Control>();
            // Расположение элементов по горизонтальным рядам (в соответствии с количеством столбцов).
            if (columnCount != 0)
            {
                for (var index = 0; index < controls.Count(); index++)
                {
                    controlList.Add(controls[index]);
                    // После заполнения ряда, либо по достижении последнего элемента выполняется размещение элементов.
                    if (controlList.Count == columnCount | index == controls.Count() - 1)
                    {
                        PlaceControlsHorizontally(controlList.ToArray(), horizontalOffset);
                        AlignControlsByTopBorder(controlList.ToArray());
                        // После заполнения ряда необходимо следующий элемент перенести на новый ряд.
                        if (index + 1 <= controls.Count() - 1)
                        {
                            Control[] controlArray = { controlList.First(), controls[index + 1] };
                            PlaceControlsVertically(controlArray, verticalOffset);
                            AlignControlsByLeftBorder(controlArray);
                        }
                        controlList.Clear();
                    }
                }
                return;
            }
            // Расположение элементов по вертикальным столбцам (в соответствии с количеством строк).
            for (var index = 0; index < controls.Count(); index++)
            {
                controlList.Add(controls[index]);
                // После заполнения столбца, либо по достижении последнего элемента выполняется размещение элементов.
                if (controlList.Count == rowCount | index == controls.Count() - 1)
                {
                    PlaceControlsVertically(controlList.ToArray(), verticalOffset);
                    AlignControlsByLeftBorder(controlList.ToArray());
                    // После заполнения столбца необходимо следующий элемент перенести на новый столбец.
                    if (index + 1 <= controls.Count() - 1)
                    {
                        Control[] controlArray = { controlList.First(), controls[index + 1] };
                        PlaceControlsHorizontally(controlArray, horizontalOffset);
                        AlignControlsByTopBorder(controlArray);
                    }
                    controlList.Clear();
                }
            }
        }

        /// <summary>
        /// Размещает кнопки "Запуск" и "Выход из приложения" в контейнере, выравнивая правую и нижнюю границу кнопок.
        /// </summary>
        /// <param name="startButton">Экземпляр кнопки "Запуск".</param>
        /// <param name="quitButton">Экземпляр кнопки "Выход из приложения".</param>
        public static void PlaceStartQuitButtons(StartButton startButton, QuitButton quitButton)
        {
            Control container = GetContainer(quitButton);
            if (container == null)
            {
                return;
            }
            quitButton.Left = container.Width - GUISizes.HORIZONTAL_OFFSET - quitButton.Width;
            quitButton.Height = container.Height - GUISizes.VERTICAL_OFFSET - quitButton.Height;
            startButton.Left = quitButton.Left - GUISizes.HORIZONTAL_OFFSET - startButton.Width;
            Control[] controlArray = { quitButton, startButton };
            AlignControlsByBottomBorder(controlArray);
        }

        /// <summary>
        /// Размещает элементы интерфейса, выравнивая их по левой границе.
        /// </summary>
        /// <param name="controls">Набор элементов интерфейса. Первый элемент является базовым.</param>
        /// <param name="leftOffset">Расстояние по горизонтали от левой границы контейнера до элементов.
        /// <para>При LeftOffset = 0 базовый элемент не изменяет свое положение.</para></param>
        public static void AlignControlsByLeftBorder(Control[] controls, int leftOffset = 0)
        {
            if (controls.Count() <= 1)
            {
                return;
            }
            Control baseControl = controls[0];
            baseControl.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            if (leftOffset != 0)
            {
                baseControl.Left = leftOffset;
            }
            for (var index = 1; index < controls.Count(); index++)
            {
                controls[index].Anchor = AnchorStyles.Left | AnchorStyles.Top;
                controls[index].Left = baseControl.Left;
            }
        }

        /// <summary>
        /// Размещает элементы интерфейса, выравнивая их по правой границе.
        /// </summary>
        /// <param name="controls">Набор элементов интерфейса. Первый элемент является базовым.</param>
        /// <param name="rightOffset">Расстояние по горизонтали от правой границы контейнера до элементов.
        /// <para>При RightOffset = 0 базовый элемент не изменяет свое положение.</para></param>
        public static void AlignControlsByRightBorder(Control[] controls, int rightOffset = 0)
        {
            if (controls.Count() <= 1)
            {
                return;
            }
            Control baseControl = controls[0];
            Control container = GetContainer(baseControl);
            if (container == null)
            {
                return;
            }
            baseControl.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            if (rightOffset != 0)
            {
                baseControl.Left = container.Width - baseControl.Width - rightOffset;
            }
            for (var index = 1; index < controls.Count(); index++)
            {
                controls[index].Anchor = AnchorStyles.Left | AnchorStyles.Top;
                controls[index].Left = baseControl.Left + baseControl.Width - controls[index].Width;
            }
        }

        /// <summary>
        /// Размещает элементы интерфейса, выравнивая их по верхней границе.
        /// </summary>
        /// <param name="controls">Набор элементов интерфейса. Первый элемент является базовым.</param>
        /// <param name="topOffset">Расстояние по вертикали от верхней границы контейнера до элементов.
        /// <para>При TopOffset = 0 базовый элемент не изменяет свое положение.</para></param>
        public static void AlignControlsByTopBorder(Control[] controls, int topOffset = 0)
        {
            if (controls.Count() <= 1)
            {
                return;
            }
            Control baseControl = controls[0];
            baseControl.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            if (topOffset != 0)
            {
                baseControl.Top = topOffset;
            }
            for (var index = 1; index < controls.Count(); index++)
            {
                controls[index].Anchor = AnchorStyles.Left | AnchorStyles.Top;
                controls[index].Top = baseControl.Top;
            }
        }

        /// <summary>
        /// Размещает элементы интерфейса, выравнивая их по нижней границе.
        /// </summary>
        /// <param name="controls">Набор элементов интерфейса. Первый элемент является базовым.</param>
        /// <param name="bottomOffset">Расстояние по вертикали от нижней границы контейнера до элементов.
        /// <para>При BottomOffset = 0 базовый элемент не изменяет свое положение.</para></param>
        public static void AlignControlsByBottomBorder(Control[] controls, int bottomOffset = 0)
        {
            if (controls.Count() <= 1)
            {
                return;
            }
            Control baseControl = controls[0];
            Control container = GetContainer(baseControl);
            if (container == null)
            {
                return;
            }
            baseControl.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            if (bottomOffset != 0)
            {
                baseControl.Top = container.Height - baseControl.Height - bottomOffset;
            }
            for (var index = 1; index < controls.Count(); index++)
            {
                controls[index].Anchor = AnchorStyles.Left | AnchorStyles.Top;
                controls[index].Top = baseControl.Top + baseControl.Height - controls[index].Height;
            }
        }

        /// <summary>
        /// Размещает центры элементов интерфейса на общей горизонтали (элементы располагаются в ряд).
        /// </summary>
        /// <param name="controls">Набор элементов интерфейса. Первый элемент является базовым и не меняет своего положения.</param>
        public static void CenterControlsHorizontally(Control[] controls)
        {
            Control controlObj;
            for (var index = 0; index < controls.Count() - 1; index++)
            {
                controlObj = controls[index];
                controlObj.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                controls[index + 1].Top = controlObj.Top - (controls[index + 1].Height - controlObj.Height) / 2;
            }
        }

        /// <summary>
        /// Размещает центры элементов интерфейса на общей вертикали (элементы располагаются в столбец).
        /// </summary>
        /// <param name="controls">Набор элементов интерфейса. Первый элемент является базовым и не меняет своего положения.</param>
        public static void CenterControlsVertically(Control[] controls)
        {
            Control controlObj;
            for (var index = 0; index < controls.Count() - 1; index++)
            {
                controlObj = controls[index];
                controlObj.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                controls[index + 1].Left = controlObj.Left - (controls[index + 1].Width - controlObj.Width) / 2;
            }
        }

        /// <summary>
        /// Подгоняет размеры контейнера под его содержимое.
        /// </summary>
        /// <param name="container">Контейнер элементов.</param>
        /// <param name="fitSizeMode">Способ подгонки размеров контейнера.</param>
        /// <param name="rightOffset">Расстояние по горизонтали от правой границы контейнера до ближайшего элемента.</param>
        /// <param name="bottomOffset">Расстояние по вертикали от нижней границы контейнера до ближайшего элемента.</param>
        public static void FitContainerSize(Control container, FitSizeMode fitSizeMode,
                                            int rightOffset, int bottomOffset)
        {
            if (container.Controls.Count == 0)
            {
                return;
            }
            // Подбор ширины контейнера производится по крайнему правому элементу.
            if (fitSizeMode == FitSizeMode.FitWidth | fitSizeMode == FitSizeMode.FitWidthAndHeight)
            {
                var rightBorderLocation = 0;
                var widthCorrection = 0;
                if (CheckControlType(container, typeof(Form)))
                {
                    widthCorrection = GUISizes.LEFT_CORRECTION;
                }
                // Получение координаты крайней правой границы.
                rightBorderLocation = (from Control control in container.Controls
                                       select control.Left + control.Width).Max();
                // Смещение правой границы контейнера.
                container.Width = rightBorderLocation + rightOffset + widthCorrection;
            }
            // Подбор высоты контейнера производится по крайнему нижнему элементу.
            if (fitSizeMode == FitSizeMode.FitHeight | fitSizeMode == FitSizeMode.FitWidthAndHeight)
            {
                var bottomBorderLocation = 0;
                var heightCorrection = 0;
                if (CheckControlType(container, typeof(Form)))
                {
                    heightCorrection = GUISizes.TOP_CORRECTION;
                }
                // Получение координаты крайней нижней границы.
                bottomBorderLocation = (from Control control in container.Controls
                                        select control.Top + control.Height).Max();
                // Смещение нижней границы контейнера.
                container.Height = bottomBorderLocation + bottomOffset + heightCorrection;
            }
        }

        /// <summary>
        /// Задает шрифт для указанного элемента интерфейса.
        /// </summary>
        /// <param name="control">Элемент интерфейса.</param>
        /// <param name="font">Шрифт.</param>
        /// <param name="changeSubControls">Изменить шрифт вложенных элементов интерфейса.</param>
        public static void SetControlFont(Control control, System.Drawing.Font font, bool changeSubControls)
        {
            control.Font = font;
            // Рекурсивный вызов метода для изменения шрифта вложенных элементов интерфейса.
            if (changeSubControls)
            {
                foreach (Control controlObj in control.Controls)
                {
                    SetControlFont(controlObj, font, changeSubControls);
                }
            }
        }

        /// <summary>
        /// Задает шрифт для указанных элементов интерфейса.
        /// </summary>
        /// <param name="controls">Набор элементов интерфейса.</param>
        /// <param name="font">Шрифт.</param>
        /// <param name="changeSubControls">Изменить шрифт вложенных элементов интерфейса.</param>
        public static void SetControlFont(Control[] controls, System.Drawing.Font font, bool changeSubControls)
        {
            foreach (Control control in controls)
            {
                SetControlFont(control, font, changeSubControls);
            }
        }

        /// <summary>
        /// Задает свойство <see cref="Control.TabIndex"/> для указанных элементов интерфейса.
        /// </summary>
        /// <param name="controls">Элементы интерфейса.</param>
        /// <param name="startIndex">Начальное значение индекса.</param>
        public static void SetTabIndex(Control[] controls, int startIndex = 0)
        {
            for (var index = startIndex; index < controls.Count(); index++)
            {
                controls[index].TabIndex = index;
            }
        }

        /// <summary>
        /// Возвращает контейнер указанного элемента интерфейса.
        /// </summary>
        /// <param name="control">Элемент интерфейса.</param>
        /// <returns></returns>
        static Control GetContainer(Control control)
        {
            Control resultValue = null;
            try
            {
                resultValue = control.Parent;
            }
            catch (System.Exception)
            {
            }
            return resultValue;
        }

        /// <summary>
        /// Возвращает результат проверки типа элемента интерфейса.
        /// </summary>
        /// <param name="control">Элемент интерфейса.</param>
        /// <param name="type">Ожидаемый тип элемента интерфейса.</param>
        /// <returns>Если тип элемента интерфейса совпадает с ожидаемым типом, то возвращается True.</returns>
        static bool CheckControlType(Control control, System.Type type)
        {
            var resultValue = false;
            if (control.GetType() == type || control.GetType().BaseType == type)
            {
                resultValue = true;
            }
            return resultValue;
        }

        #endregion
    }
}
