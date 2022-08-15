using System;
using System.Media;
using System.Windows.Controls;

namespace Курсова_робота
{
    /// <summary>
    /// Представляє шашку, наслідує клас ActiveElements
    /// </summary>
    internal class Checker:ActiveElements
    {
        private int x;
        private int y;
        private Button posButton;


        /// <summary>
        /// Ініціалізація нової шашки
        /// </summary>
        /// <param name="button">Кнопка за якою закріплюється шашка</param>
        /// <param name="color">Колір шашки</param>
        public Checker(Button button, string color):base(color)
        {
            this.x = Board.GetRow(button);
            this.y = Board.GetColumn(button);
            this.posButton = button;
        }

        /// <summary>
        /// Отримати або встановити координату x
        /// </summary>
        public int X { get => x; set => x = value; }

        /// <summary>
        /// Отримати або встановити координату y
        /// </summary>
        public int Y { get => y; set => y = value; }

        /// <summary>
        /// Отримати або встановити кнопку, за якою закріплюється шакша
        /// </summary>
        public Button PosButton { get => posButton; set => posButton = value; }

        /// <summary>
        /// Перевіряє на рівність шашок за параметрами х, у, color
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Повертає булеве значення</returns>
        public override bool Equals(object obj)
        {
            Checker temp = (Checker)obj;
            if (temp.X == this.X && temp.Y == this.Y && temp.Color == this.Color)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод закріплює за шашкою новою кнопкою
        /// Зміна координат автоматично
        /// </summary>
        /// <param name="destination">Кнопка, за яко здійснюється закріплення</param>
        /// <returns></returns>
        public override bool Move(Button destination)
        {
            x = Board.GetRow(destination);
            y = Board.GetColumn(destination);
            posButton = destination;
            PlaySound();
            return true;
        }

        /// <summary>
        /// Відтворити звук пересування шашки
        /// </summary>
        protected override void PlaySound()
        {
            var rand = new Random();
            var sound = new SoundPlayer(@$"Sounds\CheckerSound{rand.Next(1, 4)}.wav");
            sound.Play();
        }
    }

}
