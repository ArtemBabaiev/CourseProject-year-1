using System;
using System.Collections;
using System.Collections.Generic;

namespace Курсова_робота
{
    /// <summary>
    /// Представляє елемент зв'язного списку
    /// Наслідується IEnumerable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Element<T>
    {
        private T data;
        private Element<T> next;

        /// <summary>
        /// Ініціалізація нового елемента
        /// </summary>
        /// <param name="current">Значення, яке зберігяє елемент</param>
        public Element(T current)
        {
            Data = current;
        }

        /// <summary>
        /// Отримати або встановити значення елементу
        /// </summary>
        public T Data { get => data; set => data = value; }

        /// <summary>
        /// Отримати вбо встановити значення елементу після поточного
        /// </summary>
        public Element<T> Next { get => next; set => next = value; }
    }

    /// <summary>
    /// Представляє строго типізований циклічний список, останій елемент має посилання перший
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class LoopList<T> : IEnumerable<T>
    {
        private Element<T> head;
        private Element<T> tail;
        private int size = 0;

        /// <summary>
        /// Встановити або отримати перший елемент
        /// </summary>
        internal Element<T> Head { get => head; set => head = value; }

        /// <summary>
        /// Додавання нового елементу в кінець списку
        /// </summary>
        /// <param name="element">елемент для додавання</param>
        public void Add(T element)
        {
            Element<T> ElNew = new Element<T>(element);
            if (head == null)
            {
                head = ElNew;
                tail = ElNew;
                tail.Next = head;
            }
            else
            {
                ElNew.Next = head;
                tail.Next = ElNew;
                tail = ElNew;
            }
            size++;
        }

        /// <summary>
        /// Очистити список
        /// </summary>
        public void Clear()
        {
            head.Next = null;
            tail.Next = null;
            head = null;
            tail = null;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Element<T> current = head;
            while (true)
            {
                if (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
