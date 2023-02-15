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
    
    public partial class StatsWindow : Window
    {
       StatsVM _stats = null;
       StatsManager _statsManager = null;
        
        public StatsWindow(StatsVM stats, StatsManager statsManager)
        {

            _stats = stats;
            InitializeComponent();

        }
        private void Window_Loaded(object sender, RoutedEventArgs ev)
        {
            txtRankID.Text = _stats.RankID.ToString();
            txtRank.Text = _stats.Rank;
            txtKDRatio.Text = _stats.KDRatio;
            txtACS.Text = _stats.ACS;


        }
    }
}
