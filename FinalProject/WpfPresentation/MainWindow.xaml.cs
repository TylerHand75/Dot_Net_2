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
using static System.Net.Mime.MediaTypeNames;

namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User _user = null;
        private List<GamesVM> _matchesPlayed = null;
        private GameManager _gameManager = new GameManager();
        private List<StatsVM> _stats = null;
        private StatsManager _statsManager = new StatsManager();
        private List<Players> _players = null;
        private PlayerManager _playerManager = new PlayerManager();
        private List<Teams> _teams = null;
        private TeamManager _teamManager = new TeamManager();
        private List<Agents> _agent = null;
        private AgentManager _agentManager = new AgentManager();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (btnLogin.Content.ToString() == "Log Out")
            {
                // stuff to do for logout
                updateUIForLogout();
                return;
            }
            UserManager userManager = new UserManager();            // talking to data access class thats talking to the DB

            string email = txtEmail.Text;               // cause its a text box 
            string password = txtPassword.Password;     // password box not a text box so no text property

            if (email.Length < 6)
            {
                MessageBox.Show("Invalid email address.");
                txtEmail.Text = "";         // clear email
                txtEmail.Focus();           // focus cursor to email box
                return;
            }

            if (password == "")
            {
                MessageBox.Show("Password is required.");
                txtPassword.Focus();
                return;
            }

            try
            {
                _user = userManager.LoginUser(email, password);
                // MessageBox.Show("Welcome " + _user.GivenName + "\n" + "You are logged in as " + _user.Roles[0]);  // [0] just to test, will change

                if (txtPassword.Password == "newuser")
                {
                    //we need to open and change password dialog
                    var passwordWindow = new UpdatePasswordWindow(_user, userManager, true);

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
                switch (role)
                {
                    case "Commissioner":
                        tabAgents.Visibility = Visibility.Visible;
                        tabAgents.IsSelected = true;
                        tabPlayers.Visibility = Visibility.Visible;
                        tabPlayers.IsSelected = true;
                        tabMatches.Visibility = Visibility.Visible;
                        tabMatches.IsSelected = true;
                        tabStats.Visibility = Visibility.Visible;
                        tabStats.IsSelected = true;
                        tabTeams.Visibility = Visibility.Visible;
                        tabTeams.IsSelected = true;
                        break;

                    case "Player":
                        tabStats.Visibility = Visibility.Visible;
                        tabStats.IsSelected = true;
                        tabMatches.Visibility = Visibility.Visible;
                        tabMatches.IsSelected = true;
                        tabPlayers.Visibility = Visibility.Visible;
                        tabPlayers.IsSelected = true;
                        break;

                    case "Admin":
                        tabAgents.Visibility = Visibility.Visible;
                        tabAgents.IsSelected = true;
                        tabMatches.Visibility = Visibility.Visible;
                        tabMatches.IsSelected = true;
                        tabStats.Visibility = Visibility.Visible;
                        tabStats.IsSelected = true;
                        tabTeams.Visibility = Visibility.Visible;
                        tabTeams.IsSelected = true;
                        tabPlayers.Visibility = Visibility.Visible;
                        tabPlayers.IsSelected = true;
                        break;

                    case "Captain":
                        tabAgents.Visibility = Visibility.Visible;
                        tabAgents.IsSelected = true;
                        tabPlayers.Visibility = Visibility.Visible;
                        tabPlayers.IsSelected = true;
                        tabMatches.Visibility = Visibility.Visible;
                        tabMatches.IsSelected = true;
                        tabStats.Visibility = Visibility.Visible;
                        tabStats.IsSelected = true;

                        break;

                    case "General Manager":
                        tabAgents.Visibility = Visibility.Visible;
                        tabAgents.IsSelected = true;
                        tabPlayers.Visibility = Visibility.Visible;
                        tabPlayers.IsSelected = true;
                        tabMatches.Visibility = Visibility.Visible;
                        tabMatches.IsSelected = true;
                        tabStats.Visibility = Visibility.Visible;
                        tabStats.IsSelected = true;

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

        private void tabMatches_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_matchesPlayed == null)
            {
                try
                {
                    _matchesPlayed = _gameManager.retrieveGamesVmsByStatus("Played");
                    datMatches.ItemsSource = _matchesPlayed;
                    datMatches.Columns.RemoveAt(0);
                    datMatches.Columns[0].Header = "Game ID";
                    datMatches.Columns[4].Header = "Game Time";
                    datMatches.Columns[5].Header = "Game Status";
                    datMatches.Columns.RemoveAt(6);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        private void datMatches_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedMatch = (GamesVM)(datMatches.SelectedItem);

            try
            {
                selectedMatch = _gameManager.populateGamesOnGamesVm(selectedMatch);



                var detailWindow = new GameDetailsWindow(selectedMatch, _gameManager);
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

        private void datStats_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedStat = (StatsVM)(datStats.SelectedItem);

            try
            {
                selectedStat = _statsManager.populatetStatByRank(selectedStat);



                var detailWindow = new StatsWindow(selectedStat, _statsManager);
                detailWindow.ShowDialog();

            }
            catch (Exception up)
            {
                MessageBox.Show(up.Message + "\n\n" + up.InnerException.Message);
            }

        }
        private void StatsHeader()
        {
            try
            {
                _stats = _statsManager.retrieveStatsVmsByRankId("Diamond1");
                datStats.ItemsSource = _stats;

                datStats.Columns[0].Header = "";
                datStats.Columns[2].Header = "Rank";
                datStats.Columns[3].Header = "K/D Ratio";
                datStats.Columns[4].Header = "Average Combat Score";


            }
            catch (ArgumentOutOfRangeException)
            {

            }

        }
        private void tabStats_GotFocus(object sender, RoutedEventArgs e)
        {
            datStats.ItemsSource = _stats;
            StatsHeader();
            if (_stats == null)
            {

                try
                {
                    _stats = _statsManager.retrieveStatsVmsByRankId("Diamond1");

                }
                catch (Exception ext)
                {

                    MessageBox.Show(ext.Message + "\n\n" + ext.InnerException.Message);
                }

            }

        }


        private void tabPlayer_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_players == null)
            {
                try
                {
                    _players = _playerManager.RetrievePlayerVMsByUserID(IsActive);
                    datPlayers.ItemsSource = _players;

                    datPlayers.Columns[0].Header = "User ID";
                    datPlayers.Columns[2].Header = "Family Name";
                    datPlayers.Columns[3].Header = "Gamer Tag";

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        private void TeamHeader()
        {
            try
            {
                _teams = _teamManager.RetrieveTeamsByActive(IsActive);
                datTeams.ItemsSource = _teams;
                datTeams.Columns[0].Header = "Team Name";
                datTeams.Columns[1].Header = "Team Ranking";

            }
            catch (ArgumentOutOfRangeException)
            {

            }
           
        }
        private void tabTeams_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_teams == null)
            {
                datTeams.ItemsSource = _teams;
                TeamHeader();
                try
                {
                    _teams = _teamManager.RetrieveTeamsByActive(IsActive);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }
        private void AgentHeader()
        {
            try
            {
                _agent = _agentManager.RetrieveAgentsByActive(IsActive);
                datAgents.ItemsSource = _agent;
                datAgents.Columns[0].Header = "Agent Name";
                datAgents.Columns[1].Header = "Agent Description";
               

            }
            catch (ArgumentOutOfRangeException)
            {

            }

        }
        private void tabAgents_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_agent == null)
            {
                datAgents.ItemsSource = _agent;
                AgentHeader();
                try
                {
                    _agent = _agentManager.RetrieveAgentsByActive(IsActive);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }
    }
}


