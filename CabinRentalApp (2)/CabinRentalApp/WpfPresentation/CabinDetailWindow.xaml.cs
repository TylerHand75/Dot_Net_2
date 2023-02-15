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
using LogicLayer;
using DataObjects;


namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for CabinDetailWindow.xaml
    /// </summary>
    public partial class CabinDetailWindow : Window
    {


        CabinVM _cabin = null;
        CabinManager _cabinManager = null;
        public CabinDetailWindow(CabinVM cabin, CabinManager cabinManager)
        {
            

            _cabin = cabin;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtSite.Text = _cabin.Site;
            txtTrail.Text = _cabin.Trail;
            txtType.Text = _cabin.CabinTypeID;
            txtStatus.Text = _cabin.CabinStatusID;

            lstAmenities.ItemsSource = _cabin.Amenitites;
        }
    }
}
