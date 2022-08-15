using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсова_робота
{
    /// <summary>
    /// Представляє собою граф, з методами додавання вершини і зв'язків
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Tree<T>
    {
        private Dictionary<T, List<T>> graph = new Dictionary<T, List<T>>();

        /// <summary>
        /// Ініціалізація нового об'єкту класу Graph
        /// </summary>
        /// <param name="head">Початкова вершина</param>
        public Tree(T head)
        {
            graph.Add(head, new List<T>());
        }

        /// <summary>
        /// Додавання списку суміжних вершин до вершини <paramref name="head"/>
        /// </summary>
        /// <param name="newElem">Список суміжних вершин</param>
        /// <param name="head">Вершина за якою заріплюється список</param>
        /// <returns>Повертає true, якщо вставка пройшла успішно</returns>
        public bool  AddConnectionWith(List<T> newElem, T head)
        {
            if (graph.ContainsKey(head))
            {
                graph[head] = newElem;
                return true;
            }
            return false;
            
        }

        /// <summary>
        /// Додає нову вершину до графа
        /// </summary>
        /// <param name="newNode">Нова вершина</param>
        public void AddNewNode(T newNode)
        {
            graph.Add(newNode, new List<T>());
        }

        /// <summary>
        /// Шукає батьківську вершину <paramref name="toSearch"/>
        /// </summary>
        /// <param name="toSearch">Вершина, в якої шукають батьківськувешину</param>
        /// <returns></returns>
        public T GetFatherNode(T toSearch)
        {
            foreach (var head in graph.Keys)
            {
                foreach (var item in graph[head])
                {
                    if (toSearch.Equals(item))
                    {
                        return head;
                    }
                }
            }
            return default;

        }
    }
}
