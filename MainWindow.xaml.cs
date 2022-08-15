using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Курсова_робота
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Players players = new Players();
        private Player currentPlayer;

        private bool IsStarted = false;
        private string PrevScreen = StrConst.Menu;

        private Stack<string> NextScreen = new Stack<string>();

        private Tree<string> menu = new Tree<string>(StrConst.Menu);


        public MainWindow()
        {
            InitializeComponent();
            board.Buttons = new List<List<Button>> { new List<Button> { b00, b01, b02, b03, b04, b05, b06 },
                                                     new List<Button> { b10, b11, b12, b13, b14, b15, b16 },
                                                     new List<Button> { b20, b21, b22, b23, b24, b25, b26 },
                                                     new List<Button> { b30, b31, b32, b33, b34, b35, b36 },
                                                     new List<Button> { b40, b41, b42, b43, b44, b45, b46 },
                                                     new List<Button> { b50, b51, b52, b53, b54, b55, b56 },
                                                     new List<Button> { b60, b61, b62, b63, b64, b65, b66 } };

            currentPlayer = players.First;
            menu.AddConnectionWith(new List<string> { StrConst.SubMenu, StrConst.Help }, StrConst.Menu);
            menu.AddNewNode(StrConst.SubMenu);
            menu.AddConnectionWith(new List<string> { StrConst.Game }, StrConst.SubMenu);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            NextScreen.Clear();
            ShowSubMenu();
        }

        private void ContGameButton_Click(object sender, RoutedEventArgs e)
        {
            NextScreen.Clear();
            if (!IsStarted)
            {
                MessageBox.Show("Немає гри для продовження", "Повідомленя");
                return;
            }
            ShowGame();
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            NextScreen.Clear();
            if (IsStarted)
            {
                RestartGame();
            }
            currentPlayer = players.First;
            IsStarted = true;
            for (int i = 0; i < board.Buttons.Count; i++)
            {
                for (int j = 0; j < board.Buttons[i].Count; j++)
                {
                    if (i <= 2 && j <= 3)
                    {
                        var tempImage = new Image();
                        tempImage.Source = new BitmapImage(new Uri(@"Images\White.png", UriKind.Relative));
                        board.Buttons[i][j].Content = tempImage;
                        currentPlayer.Chekers.Add(new Checker(board.Buttons[i][j], currentPlayer.Color));
                        if (i == 2 && j == 3)
                        {
                            currentPlayer = players.SwitchPlayer();
                        }
                    }
                    else if (i >= 4 && j >= 3)
                    {
                        var tempImage = new Image();
                        tempImage.Source = new BitmapImage(new Uri(@"Images\Black.png", UriKind.Relative));
                        currentPlayer.Chekers.Add(new Checker(board.Buttons[i][j], currentPlayer.Color));
                        board.Buttons[i][j].Content = tempImage;
                    }
                    board.Buttons[i][j].Background = Brushes.PeachPuff;
                }
            }
            currentPlayer = players.SwitchPlayer();
            ShowGame();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            NextScreen.Clear();
            ShowHelp();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            string temp;
            NextScreen.Push(Header.Content.ToString());
            if (Header.Content.ToString() == StrConst.Help)
            {
                temp = PrevScreen;
            }
            else
            {
                temp = menu.GetFatherNode(Header.Content.ToString());
            }
            if (temp == StrConst.Menu)
            {
                ShowMenu();
            }
            else if (temp == StrConst.SubMenu)
            {
                ShowSubMenu();
            }
            else if (temp == StrConst.Game)
            {
                ShowGame();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (NextScreen.Count == 0)
            {
                return;
            }
            else
            {
                var temp = NextScreen.Pop();
                if (temp == StrConst.SubMenu)
                {
                    ShowSubMenu();
                }
                else if (temp == StrConst.Game)
                {
                    ShowGame();
                }
                else if (temp == StrConst.Help)
                {
                    ShowHelp();
                }

            }
        }

        private void Checker_Click(object sender, RoutedEventArgs e)
        {
            var but = (Button)sender;
            if (currentPlayer.ChekToMove == null)
            {
                if (currentPlayer.Remember(but))
                {
                    but.BorderThickness = new Thickness(4);
                    board.ShowPossibleMoves(currentPlayer.GetChekerByButton(but), currentPlayer);
                }
                return;
            }
            else
            {
                if (currentPlayer.Move(but))
                {
                    board.SetDefaultColors();
                    if (currentPlayer.CheckForWin())
                    {
                        MessageBox.Show(currentPlayer.Color == StrConst.Black ? "Пермога чорних" : "Пермога білих", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                        RestartProgram();
                        return;
                    }
                    currentPlayer = players.SwitchPlayer();
                }
                else
                {
                    board.SetDefaultColors();
                }
            }
        }

        private void KeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                if (Header.Content.ToString() != StrConst.Help)
                {
                    PrevScreen = Header.Content.ToString();
                }
                ShowHelp();
            }
            else if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift && e.Key == Key.Tab)
            {
                BackButton_Click(BackButton, e);
            }
            else if (e.Key == Key.Tab)
            {
                NextButton_Click(NextButton, e);
            }
        }

        /// <summary>
        /// Показати говне меню
        /// </summary>
        private void ShowMenu()
        {
            RightArrow();

            Header.Content = StrConst.Menu;

            StartButton.Visibility = Visibility.Visible;
            HelpButton.Visibility = Visibility.Visible;
            ExitButton.Visibility = Visibility.Visible;

            HideHelp();
            HideSubMenu();
            HideGame();


            BackButton.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Показати розділ "МЕНЮ ГРИ"
        /// </summary>
        private void ShowSubMenu()
        {
            RightArrow();

            Header.Content = StrConst.SubMenu;

            NewGameButton.Visibility = Visibility.Visible;
            ContGameButton.Visibility = Visibility.Visible;

            HideMainMenu();
            HideHelp();
            HideGame();

            BackButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Показати дошку
        /// </summary>
        private void ShowGame()
        {
            RightArrow();

            Header.Content = StrConst.Game;

            foreach (var item in board.Buttons)
            {
                foreach (var el in item)
                {
                    el.Visibility = Visibility.Visible;
                }
            }

            HideMainMenu();
            HideSubMenu();
            HideHelp();

            RightArrow();
            BackButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Показати посібник користувача
        /// </summary>
        private void ShowHelp()
        {
            RightArrow();
            Header.Content = StrConst.Help;

            HelpText.Visibility = Visibility.Visible;

            HideMainMenu();
            HideSubMenu();
            HideGame();
            BackButton.Visibility = Visibility.Visible;
            NextButton.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Показати чи сховати праву стрілку
        /// </summary>
        private void RightArrow()
        {
            if (NextScreen.Count != 0)
            {
                NextButton.Visibility = Visibility.Visible;
            }
            else
            {
                if (Header.Content.ToString() != StrConst.Help)
                {
                    PrevScreen = Header.Content.ToString();
                }
                NextButton.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Сховати елементи головного меню
        /// </summary>
        private void HideMainMenu()
        {
            StartButton.Visibility = Visibility.Hidden;
            HelpButton.Visibility = Visibility.Hidden;
            ExitButton.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Сховати елементи "МЕНЮ ГРИ"
        /// </summary>
        private void HideSubMenu()
        {
            NewGameButton.Visibility = Visibility.Hidden;
            ContGameButton.Visibility = Visibility.Hidden;
        }
        
        /// <summary>
        /// Сховати "ПОСІБНИК КОРИСТУВАЧА"
        /// </summary>
        private void HideHelp()
        {
            HelpText.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Сховати дошку
        /// </summary>
        private void HideGame()
        {
            foreach (var item in board.Buttons)
            {
                foreach (var el in item)
                {
                    el.Visibility = Visibility.Hidden;
                }
            }
        }

        /// <summary>
        /// Переведення всіх елементів в початковий стан
        /// </summary>
        private void RestartProgram()
        {
            ShowMenu();
            NextScreen.Clear();
            RestartGame();
        }

        /// <summary>
        /// Переведення дошки в початковий стан
        /// </summary>
        private void RestartGame()
        {
            IsStarted = false;
            foreach (var item in board.Buttons)
            {
                foreach (var el in item)
                {
                    el.Visibility = Visibility.Hidden;
                    el.Content = null;
                }
            }
            currentPlayer.Chekers.Clear();
            currentPlayer.ChekToMove = null;
            currentPlayer = players.SwitchPlayer();
            currentPlayer.Chekers.Clear();
            currentPlayer.ChekToMove = null;
            board.SetDefaultColors();
            board.SetDefaultThickness();
        }
    }

}
