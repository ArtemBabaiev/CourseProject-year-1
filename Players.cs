namespace Курсова_робота
{
    /// <summary>
    /// Представляє пару гравців за допомогою циклічного списку
    /// </summary>
    internal class Players
    {
        private LoopList<Player> players = new LoopList<Player>();
        private Element<Player> current;

        /// <summary>
        /// Ініціалізація гравців
        /// </summary>
        public Players()
        {
            players.Add(new Player(StrConst.White));
            players.Add(new Player(StrConst.Black));
            current = players.Head;
        }

        /// <summary>
        /// Отримати поточного гравця
        /// </summary>
        internal Player Current { get => current.Data; }

        /// <summary>
        /// Отримати першого гравця і встановити його поточним
        /// </summary>
        internal Player First
        {
            get
            {
                current = players.Head;
                return current.Data;
            }
        }
        
        /// <summary>
        /// Отримати наступного гравця
        /// </summary>
        /// <returns></returns>
        public Player SwitchPlayer()
        {
            current = current.Next;
            return current.Data;
        }

        /// <summary>
        /// Очистити список гравців
        /// </summary>
        public void ClearList()
        {
            players.Clear();
        }
    }
}
