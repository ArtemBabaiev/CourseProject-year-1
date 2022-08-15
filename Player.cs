using System;
using System.Collections;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Курсова_робота
{
    /// <summary>
    /// Представляє гравця
    /// Наслідується від ActiveElements
    /// </summary>
    internal class Player:ActiveElements
    {
        private List<Checker> chekers;
        private Checker ChToMove;
        

        /// <summary>
        /// Ініціалізація нового гравця
        /// </summary>
        /// <param name="color">Колір сторони</param>
        public Player(string color):base(color)
        {
            this.chekers = new List<Checker>();
        }

        /// <summary>
        /// Отримати або встановити список шашок, які має гравець
        /// </summary>
        public List<Checker> Chekers { get => chekers; set => chekers = value; }

        /// <summary>
        /// Отримати або встановити шашку для пересування
        /// </summary>
        public Checker ChekToMove { get => ChToMove; set => ChToMove = value; }

        /// <summary>
        /// Запам'ятовування шашки для пересування
        /// </summary>
        /// <param name="button"></param>
        /// <returns>Повертає <c>true</c>, якщо в гравця існує шашка, яка закріплена за цією кнопкою</returns>
        public bool Remember(Button button)
        {
            ChToMove = GetChekerByButton(button);
            if (ChToMove==null)
            {
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// Пересування запам'ятованої шашки на нову позицію
        /// </summary>
        /// <param name="destination">Кнопка, за якою закріпиться шашка</param>
        /// <returns></returns>
        public override bool Move(Button destination)
        {
            if (destination.Background == Brushes.Yellow)
            {
                destination.Content = ChToMove.PosButton.Content;
                ChToMove.PosButton.Content = null;
                ChToMove.PosButton.BorderThickness = new Thickness(1);
                ChToMove.Move(destination);
                ChToMove = null;
                return true;
            }
            else
            {
                ChToMove.PosButton.BorderThickness = new Thickness(1);
                ChToMove = null;
                return false;
            }
            
        }

        /// <summary>
        /// Отримати шашку через кнопку
        /// </summary>
        /// <param name="button">Кнопка за якою здійснюється пошук</param>
        /// <returns>Повертає шашку, якщо така є у грваця; якщо ні, то <c>null</c></returns>
        public Checker GetChekerByButton(Button button)
        {
            foreach (var item in chekers)
            {
                if (button.Equals(item.PosButton))
                {
                    return item;
                }
            }
            return default;
        }

        /// <summary>
        /// Перевірка чи наявна в гравця шашка
        /// </summary>
        /// <param name="cheker">Шашка для пошуку</param>
        /// <returns><c>true</c> якщо шашка міститься у гравця</returns>
        public bool IsOwnedByPlayer(Checker cheker)
        {
            foreach (var item in chekers)
            {
                if (item.Equals(cheker))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Перевірка чи наявна в гравця шашка
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns><c>true</c> якщо шашка міститься у гравця</returns>
        public bool IsOwnedByPlayer(int x, int y)
        {
            foreach (var item in chekers)
            {
                if (item.X == x && item.Y == y)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Первірка перемоги
        /// </summary>
        /// <returns><c>true</c> якщо перемога</returns>
        public bool CheckForWin()
        {
            if (this.Color == StrConst.Black)
            {
                return WinBlack();
            }
            else
            {
                return WinWhite();
            }
        }

        /// <summary>
        /// Перевірка позицій чорних шашок для умови перемоги
        /// </summary>
        /// <returns><c>true</c> якщо всі шашки в лівому верхньому куту</returns>
        private bool WinBlack()
        {
            foreach (var item in chekers)
            {
                if (item.X > 2 || item.Y > 3)
                {
                    return false;
                }
            }
            PlaySound();
            return true;

        }

        /// <summary>
        /// Перевірка позицій білих шашок для умови перемоги
        /// </summary>
        /// <returns><c>true</c> якщо всі шашки в правому нижньому куту</returns>
        private bool WinWhite()
        {
            foreach (var item in chekers)
            {
                if (item.X < 4 || item.Y < 3)
                {
                    return false;
                }
            }
            PlaySound();
            return true;   
        }

        /// <summary>
        /// Відтворити звук перемоги
        /// </summary>
        protected override void PlaySound()
        {
            var sound = new SoundPlayer(@$"Sounds\WinSound.wav");
            sound.Play();
        }
    }
}
