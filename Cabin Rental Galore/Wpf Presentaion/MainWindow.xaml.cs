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

namespace Wpf_Presentaion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User _user = null; 
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (btnLogin.Content == "Log Out")
            {
                _user = null;
                //stuff to do for logout
                updateUIForLogout();
                return;
            }
            IUserManager userManager = new UserManager();

            string email = txtEmail.Text;
            string password = txtPassword.Password;

            if (email.Length < 6 )
            {
                MessageBox.Show("Invalid email address");
                txtEmail.Text = " ";
                txtEmail.Focus();
                return; 
            }
            if (password == "")
            {
                MessageBox.Show("You must enter password");
                txtPassword.Focus();
                return;
            }
            try
            {
                _user = userManager.LoginUser(email, password);
                //MessageBox.Show("Welcome" + _user.GivenName + "\n" + "You are Logged in as " + _user.Roles[0]);
                showTabsForUser();
                updateUIForUser();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }
       


        private void FrmMain_Loaded(object sender, RoutedEventArgs e)
        {
            updateUIForLogout();
            
        }
        private void showTabsForUser()
        {
            foreach (var role in _user.Roles)
            {
                switch (role)
                {
                    case "Admin":
                        tabRental.Visibility = Visibility.Visible;
                        tabRental.IsSelected = true;
                        break;
                    case "Commissioner":
                        tabCheckIn.Visibility = Visibility.Visible;
                        tabCheckIn.IsSelected = true;
                        break;

                    case "Player":
                        tabCheckOut.Visibility = Visibility.Visible;
                        tabCheckOut.IsSelected = true;
                        break;

                    case "Captain":
                        tabInspection.Visibility = Visibility.Visible;
                        tabInspection.IsSelected = true;
                        break;

                    case "General Manager":
                        tabPrep.Visibility = Visibility.Visible;
                        tabPrep.IsSelected = true;
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
            foreach (var tab in tabsetMain.Items)
            {
                ((TabItem)tab).Visibility = Visibility.Collapsed;
            }
        }
        private void hideLogin()
        {
            
        }
        private void updateUIForLogout()
        {
            txtEmail.Focus();
            hideAllUserTabs();
            btnLogin.IsDefault = true;

            lblGreeting.Content = "You are not logged in";
            statMessage.Content = "Welcome please log in to continue";

            txtEmail.Visibility = Visibility.Visible;
            txtPassword.Visibility = Visibility.Visible;
            lblEmail.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;

            btnLogin.Content = "Log In";
            btnLogin.IsDefault = true;

        }
        private void updateUIForUser()
        {
            string rolesList = " ";

            for (int i = 0; i  <  _user.Roles.Count; i++)
            {
                rolesList += " " + _user.Roles[i];

                if (i == _user.Roles.Count - 2)
                {
                    if (_user.Roles.Count > 2 )
                    {
                        rolesList += " ,";
                    }
                    rolesList += "  and ";
                }else if (i < _user.Roles.Count - 2) 
                {
                    rolesList += " ,";
                }
            }
            lblGreeting.Content = "Welcome, "  + _user.GivenName  + " " 
                +  _user.FamilyName  +  " you are logged in as : "  + rolesList  +  ".";

            statMessage.Content = "Logged in on " + DateTime.Now.ToLongDateString() + "," +
                DateTime.Now.ToShortDateString() + " Please log out before you leave ";


            txtEmail.Text = "";
            txtPassword.Password = "";

            txtEmail.Visibility = Visibility.Hidden;
            txtPassword.Visibility = Visibility.Hidden;
            lblEmail.Visibility = Visibility.Hidden;
            lblPassword.Visibility = Visibility.Hidden;

            btnLogin.Content = "Log Out";
            btnLogin.IsDefault = false;
        }
        
    }
}
