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
    /// Interaction logic for GameDetailsWindow.xaml
    /// </summary>
    public partial class GameDetailsWindow : Window
    {
        GamesVM _game = null;
        GameManager _gameManager = null;
        public GameDetailsWindow(GamesVM game, GameManager gameManager)
        {

            _game = game;

            InitializeComponent();
        }



        private void Window_Loaded(object sender, RoutedEventArgs ev)
        {
            txtMap.Text = _game.Maps;
           txtMaptype.Text = _game.MapType;
            txtScore.Text= _game.Score;
            txtTime.Text = _game.GameTime;
            txtStatus.Text = _game.GameStatusID;


        }

        private void btnOkay_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
