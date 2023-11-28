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
using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;
using zoostore.Utils;

namespace zoostore
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, string> user = new Dictionary<string, string>();
        public MainWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Login into system method
        /// </summary>
        private void Login()
        {
            using SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["zooStoreDB"].ConnectionString);
            try
            {
                connection.Open();
                
            }
            catch (SqlException)
            {
                MessageBox.Show("Не удалось подключиться к бд");
            }
            SqlCommand getData = new SqlCommand
            {
                Connection = connection,
                CommandText = $"SELECT [UserLogin], [UserPassword], [UserRole], [UserName], [UserSurname], [UserPatronymic] FROM [User] WHERE [UserLogin] = '{LoginField.Text}' and [UserPassword] = '{PasswordField.Password}'"
            };

            SqlDataReader readerData = getData.ExecuteReader();
            user.Clear();
            while (readerData.Read())
            {
                user.Add("Role", readerData[2].ToString());
                StateManager.UserName = readerData[3].ToString();
                StateManager.UserSurname = readerData[4].ToString();
                StateManager.UserPatronymic = readerData[5].ToString();

            }
            readerData.Close();
            if (user.ContainsKey("Role"))
            {
                MessageBox.Show("Вход успешен");
                Window logwin = Window.GetWindow(this);
                if (user["Role"] == "1" || user["Role"] == "2" || user["Role"] == "3")
                {
                    Utils.StateManager.UserRole = user["Role"];
                    Windows.ClientManagerWindow climawin = new Windows.ClientManagerWindow();
                    climawin.Show();
                    logwin.Owner = climawin;
                }
                else
                {
                    MessageBox.Show("Окно админа в разработке");
                }
                logwin.Close();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль");
            }

        }



        /// <summary>
        /// Handler for Login Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void LoginAsGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Window logwin = Window.GetWindow(this);
            Utils.StateManager.UserRole = "1";
            Windows.ClientManagerWindow climawin = new Windows.ClientManagerWindow();
            climawin.Show();
            logwin.Owner = climawin;
            logwin.Close();
        }
    }
}
