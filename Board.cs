using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Курсова_робота
{
    /// <summary>
    /// Представляє дошку, з наслідуванням класу Grid
    /// Доступні методи для підсвічування можливих ходів та зміну кольрів клітинок на стандартний
    /// </summary>
    internal class Board : Grid
    {
        private List<List<Button>> buttons;
        private bool[,] IsVisited = new bool[7, 7] { { false, false, false, false, false, false, false },
                                                     { false, false, false, false, false, false, false },
                                                     { false, false, false, false, false, false, false },
                                                     { false, false, false, false, false, false, false },
                                                     { false, false, false, false, false, false, false },
                                                     { false, false, false, false, false, false, false },
                                                     { false, false, false, false, false, false, false }};

        public Board(): base()
        {
        }

        /// <summary>
        /// Отримати або встановити двовимірний масив кнопок
        /// </summary>
        internal List<List<Button>> Buttons { get => buttons; set => buttons = value; }
        
        /// <summary>
        /// Підсвітити можливі ходи для певної шашки
        /// </summary>
        /// <param name="cell">Шашка для пересування</param>
        /// <param name="player">Грвець за яким закріплена шашка</param>
        public void ShowPossibleMoves(Checker cell, Player player)
        {
            int x = cell.X;
            int y = cell.Y;
            for (int i = -1; i <= 1; i += 2)
            {
                try
                {
                    if (buttons[x + i][y].Content == null)
                    {
                        buttons[x + i][y].Background = Brushes.Yellow;
                        IsVisited[x + i, y] = true;
                    }
                    else
                    {
                        if (!player.IsOwnedByPlayer(x + i, y))
                        {
                            if (buttons[x + 2 * i][y].Content == null)
                            {
                                ShowJumps(x + 2 * i, y, cell.Color, x, y, player);
                            }
                        }
                    }
                }
                catch (System.Exception) { }
                try
                {
                    if (buttons[x][y + i].Content == null)
                    {
                        buttons[x][y + i].Background = Brushes.Yellow;
                        IsVisited[x, y + i] = true;
                    }
                    else
                    {
                        if (!player.IsOwnedByPlayer(x, y + i))
                        {
                            if (buttons[x][y + 2 * i].Content == null)
                            {
                                ShowJumps(x, y + 2 * i, cell.Color, x, y, player);
                            }
                        }
                    }
                }
                catch (System.Exception) { }
            }

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    IsVisited[i, j] = false;
                }
            }
        }

        /// <summary>
        /// Приватий методи для підсвічення можливих стрибків через ворожу шашку
        /// </summary>
        /// <param name="x">Куди стрибнути по Х</param>
        /// <param name="y">Куди стрибнути по У</param>
        /// <param name="color">Колір шашки</param>
        /// <param name="prevX">Позиція Х до стрибка</param>
        /// <param name="prevY">Позиція У до стрибка</param>
        /// <param name="player"></param>
        private void ShowJumps(int x, int y, string color, int prevX, int prevY, Player player)
        {
            buttons[x][y].Background = Brushes.Yellow;
            IsVisited[x, y] = true;
            for (int i = -1; i <= 1; i += 2)
            {
                try
                {
                    if (!IsVisited[x+2*i, y])   // Чи була відвідана ця ячека
                    {
                        if (buttons[x + i][y].Content != null && !player.IsOwnedByPlayer(x+i, y)) // Чи є на сусідній клітинці шашка, яку не має поточний гравець
                        {
                            if (buttons[x + 2 * i][y].Content == null)
                            {
                                ShowJumps(x + 2 * i, y, color, x, y, player);
                            }
                        }
                    }
                }
                catch (System.Exception) { }
                try
                {
                    if (!IsVisited[x, y+2*i])
                    {
                        if (buttons[x][y + i].Content != null && !player.IsOwnedByPlayer(x, y+i))
                        {
                            if (buttons[x][y + 2 * i].Content == null)
                            {
                                ShowJumps(x, y + 2 * i, color, x, y, player);
                            }
                        }
                    }
                }
                catch (System.Exception) { }
            }
            return;
        }

        /// <summary>
        /// Прибирати підсвічення кнопок на дошці
        /// </summary>
        public void SetDefaultColors()
        {
            foreach (var item in buttons)
            {
                foreach (var el in item)
                {
                    el.Background = Brushes.PeachPuff;
                }
            }
        }

        /// <summary>
        /// Прибрати виділення кнопок на дошці
        /// </summary>
        public void SetDefaultThickness()
        {
            foreach (var item in buttons)
            {
                foreach (var el in item)
                {
                    el.BorderThickness = new Thickness(1);
                }
            }
        }
    }
}
