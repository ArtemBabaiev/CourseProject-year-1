using System.Windows.Controls;

namespace Курсова_робота
{
    abstract class ActiveElements
    {
        protected string color;

        protected ActiveElements(string color)
        {
            this.color = color;
        }

        virtual public bool Move(Button destination) 
        {
            return true;
        }

        /// <summary>
        /// Отримати колір
        /// </summary>
        public string Color { get => color; }

        /// <summary>
        /// Відтворити звук
        /// </summary>
        protected abstract void PlaySound();
    }
}













