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
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;

namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User _user = null;
        private List<CabinVM> _cabinsForRent = null;
        private CabinManager _cabinManager = new CabinManager();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(btnLogin.Content.ToString() == "Log Out")
            {
                // stuff to do for logout
                updateUIForLogout();
                return;
            }
            UserManager userManager = new UserManager();            // talking to data access class thats talking to the DB

            string email = txtEmail.Text;               // cause its a text box 
            string password = txtPassword.Password;     // password box not a text box so no text property

            if(email.Length < 6)
            {
                MessageBox.Show("Invalid email address.");
                txtEmail.Text = "";         // clear email
                txtEmail.Focus();           // focus cursor to email box
                return;
            }

            if(password == "")
            {
                MessageBox.Show("Password is required.");
                txtPassword.Focus();
                return;
            }

            try
            {
                _user = userManager.LoginUser(email, password);
                // MessageBox.Show("Welcome " + _user.GivenName + "\n" + "You are logged in as " + _user.Roles[0]);  // [0] just to test, will change
              
                if(txtPassword.Password == "newuser")
                {
                    //we need to open and change password dialog
                    var passwordWindow = new UpdatePasswordWindow(_user, userManager,true);

                    if ((bool)passwordWindow.ShowDialog())
                    {
                        MessageBox.Show("Password updated.");
                    }
                    else
                    {
                        MessageBox.Show("Update failed. Yeet");
                        _user = null;
                        txtEmail.Clear();
                        txtPassword.Clear();
                        updateUIForLogout();
                        return;
                    }

                }
                
                
                
                showTabsForUser();
                updateUIForUser();
            }
            catch (Exception up)
            {

                MessageBox.Show(up.Message + "\n\n" + up.InnerException.Message);
            }
        }

        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            updateUIForLogout();
        }

        private void updateUIForUser()
        {
            string rolesList = "";

            for (int i = 0; i < _user.Roles.Count; i++)
            {
                rolesList += " " + _user.Roles[i];

                if (i == _user.Roles.Count - 2)
                {
                    if (_user.Roles.Count > 2)
                    {
                        rolesList += ",";
                    }
                    rolesList += " and";
                }
                else if (i < _user.Roles.Count - 2)
                {
                    rolesList += ",";
                }

            }
            lblGreeting.Content = "Welcome, " + _user.GivenName + " " + _user.FamilyName + "! You are logged in as: " + rolesList + ".";
            statMessage.Content = "Logged in on " + DateTime.Now.ToLongDateString() + ", at" + DateTime.Now.ToShortTimeString() + ". Please remember to log out before you leave.";

            txtEmail.Text = "";
            txtPassword.Password = "";

            txtEmail.Visibility = Visibility.Hidden;
            txtPassword.Visibility = Visibility.Hidden;
            lblEmail.Visibility = Visibility.Hidden;
            lblPassword.Visibility = Visibility.Hidden;

            btnLogin.Content = "Log Out";
            btnLogin.IsDefault = false;

        }

        private void updateUIForLogout()
        {
            
            hideAllUserTabs();
            btnLogin.IsDefault = true;

            lblGreeting.Content = "You are not logged in.";
            statMessage.Content = "Welcome. Please log in to continue.";

            txtEmail.Visibility = Visibility.Visible;
            txtPassword.Visibility = Visibility.Visible;
            lblEmail.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;

            btnLogin.Content = "Log In";
            btnLogin.IsDefault = true;

            txtEmail.Focus();
        }

        private void showTabsForUser()
        {
            // loop through the rols
            foreach (var role in _user.Roles)
            {
                switch(role)
                {
                    case "Rental":
                        tabRental.Visibility = Visibility.Visible;
                        tabRental.IsSelected = true;
                        break;

                    case "CheckIn":
                        tabCheckIn.Visibility = Visibility.Visible;
                        tabCheckIn.IsSelected = true;
                        break;

                    case "CheckOut":
                        tabCheckOut.Visibility = Visibility.Visible;
                        tabCheckOut.IsSelected = true;
                        break;

                    case "Inspection":
                        tabInspection.Visibility = Visibility.Visible;
                        tabInspection.IsSelected = true;
                        break;

                    case "Prep":
                        tabPrep.Visibility = Visibility.Visible;
                        tabPrep.IsSelected = true;
                        break;

                    case "Maintenance":
                        tabMaintenance.Visibility = Visibility.Visible;
                        tabMaintenance.IsSelected = true;
                        break;

                    case "Admin":
                        tabAdmin.Visibility = Visibility.Visible;
                        tabAdmin.IsSelected = true;
                        break;

                    case "Manager":
                        tabManager.Visibility = Visibility.Visible;
                        tabManager.IsSelected = true;
                        break;

                    default:
                        break;
                }
                pnlTabs.Visibility = Visibility.Visible;
            }
        }

        private void hideAllUserTabs()
        {
            pnlTabs.Visibility = Visibility.Hidden;
            foreach(var tab in tabsetMain.Items)
            {
                ((TabItem)tab).Visibility = Visibility.Collapsed;
            }
        }

        private void tabRental_GotFocus(object sender, RoutedEventArgs e)
        {
            if(_cabinsForRent == null)
            {
                try
                {
                    _cabinsForRent = _cabinManager.RetrieveCabinVMsByStatus("Available");
                    datRental.ItemsSource = _cabinsForRent;
                    datRental.Columns.RemoveAt(0);
                    datRental.Columns[0].Header = "Cabin ID";
                    datRental.Columns[3].Header = "Cabin Type";
                    datRental.Columns[4].Header = "Cabin Status";
                    datRental.Columns.RemoveAt(5);
                    
                } 
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        private void datRental_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedCabin = (CabinVM)(datRental.SelectedItem);

            try
            {
                selectedCabin = _cabinManager.PopulateAmenitiesOnCabinVM(selectedCabin);

                //MessageBox.Show(selectedCabin.CabinID.ToString() + " - " + selectedCabin.Amenitites[0]);

                var detailWindow = new CabinDetailWindow(selectedCabin, _cabinManager);
                detailWindow.ShowDialog();

            }
            catch (Exception up)
            {
                MessageBox.Show(up.Message + "\n\n" + up.InnerException.Message);
            }

            
            
        }

        private void mnuChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (_user == null)
            {
                return;
            }
            var userManager = new UserManager();
            var passwordWindow = new UpdatePasswordWindow(_user, userManager);

            if ((bool)passwordWindow.ShowDialog())
            {
                MessageBox.Show("Password updated.");
            }
            else
            {
                MessageBox.Show("Update failed. Yeet");
            }

        }
    }
}
