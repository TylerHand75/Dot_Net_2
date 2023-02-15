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
using DataObjects;
using LogicLayer;

namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for UpdatePasswordWindow.xaml
    /// </summary>
    public partial class UpdatePasswordWindow : Window
    {
        User _user = null;
        UserManager _userManager = null;
        bool _newUser = false;
        public UpdatePasswordWindow(User user, UserManager userManager, bool newUser = false)
        {
            _user = user;
            _userManager = userManager;
            _newUser = newUser;







            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // set  up the ui for a new user update  or a volunteer password update
            btnSubmit.IsDefault = true;


            if (_newUser)
            {
                txtInstructions.Text = "First login! you must update your password\nor be logged out do it besh ";
                txtEmail.Text = _user.Email;
                txtEmail.IsEnabled = false;
                txtOldPass.Password = "newuser";
                txtOldPass.IsEnabled = false;
                txtNewPass.Focus();


            }
            else
            {
                txtInstructions.Text = "Please fill out all feilds to change your password";
                txtEmail.Focus();
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string oldPass = txtOldPass.Password;
            string newPass = txtNewPass.Password;
            string confirm = txtConfirmPass.Password;

            // error checks
            if (email == "")
            {
                MessageBox.Show("You need to enter your email");
                txtEmail.Focus();
                txtEmail.SelectAll();
                return;

            }
            if (oldPass == "")
            {
                MessageBox.Show("You need to enter your old password");
                txtOldPass.Focus();
                txtOldPass.SelectAll();
                return;

            }
            if (newPass== "")
            {
                MessageBox.Show("You need to enter your new password");
                txtNewPass.Focus();
                txtNewPass.SelectAll();
                return;

            }
            if (newPass == "newuser" || newPass== oldPass)
            {
                MessageBox.Show("You cannot use this password you big dummy");
                txtNewPass.Focus();
                txtNewPass.SelectAll();
                return;

            }

            if (confirm == "")
            {
                MessageBox.Show("You need to confirm your new password");
                txtConfirmPass.Focus();
                txtConfirmPass.SelectAll();
                return;

            }
            if (newPass  != confirm)
            {
                MessageBox.Show("You need to enter your email");
                txtNewPass.Clear();
                txtConfirmPass.Clear();
                txtNewPass.Focus();
                return;

            }
            try
            {
                if (_userManager.ResetPassword(_user, email, newPass, oldPass))
                {
                    MessageBox.Show("Password changed");
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Bad Email or Password");

                }
       
            }
            catch (Exception ex)
            {

                MessageBox.Show("Update Failed" + "\n\n" + ex.InnerException.Message);
             }
            


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (_newUser)
            {
                var result = MessageBox.Show("Are you sure? Don't be a Dummy! ", "You will be logged out", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    this.DialogResult = false;
                }
            }
            else
            {
                this.DialogResult = false;
            }
            
        }
    }
}
