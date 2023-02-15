using DataObjects;
using LogicLayer;
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
using System.Windows.Shapes;

namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for GameDetailWindow.xaml
    /// </summary>
    public partial class GameDetailWindow : Window
    {
        GamesVM _game = null;
        GameManager _gameManager = null;
        public GameDetailWindow(GamesVM game,GameManager gameManager)
        {

            _game = game;

            InitializeComponent();
        }

    

        private void GameWindow_Loaded(object sender, RoutedEventArgs ev)
        {
            
        }
    }
}
