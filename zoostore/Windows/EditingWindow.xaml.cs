using Microsoft.Data.SqlClient;
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
using System.Configuration;

namespace zoostore.Windows
{
    /// <summary>
    /// Interaction logic for EditingWindow.xaml
    /// </summary>
    public partial class EditingWindow : Window
    {
        public EditingWindow()
        {
            InitializeComponent();
            if (Utils.StateManager.IsEditing)
            {
                Editing();
            }
        }
        /// <summary>
        /// Editing row in DB method
        /// </summary>
        private void Editing()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["zooStoreDB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException)
                {
                    MessageBox.Show("Не удалось получить данные");
                }
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT * FROM [Продукты] WHERE [Артикль] = '{Utils.StateManager.ProductVendorCode}'"
                };
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PRname.Text = reader[1].ToString();
                    PRdescription.Text = reader[2].ToString();
                    PRinStock.Text = reader[3].ToString();
                    PRunitType.Text = reader[4].ToString();
                    PRcost.Text = reader[5].ToString();
                    PRmanufacter.Text = reader[6].ToString();
                    PRprovider.Text = reader[7].ToString();
                    PRcategory.Text = reader[8].ToString();
                }
                connection.Close();
            }
        }
        /// <summary>
        /// Handler for Save Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["zooStoreDB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException)
                {
                    MessageBox.Show("Не удалось отправить новые данные");
                }
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE [Product] SET [ProductName] = '{PRname.Text}', [ProductDescription] = '{PRdescription.Text}', [ProductQuantityInStock] = {PRinStock.Text}, [ProductUnitType] = '{PRunitType.Text}', [ProductCost] = {PRcost.Text}, [ProductManufacturer] = '{PRmanufacter.Text}', [ProductProvider] = '{PRprovider.Text}', [ProductCategory] = '{PRcategory.Text}' WHERE [ProductArticleNumber] = '{Utils.StateManager.ProductVendorCode}'"
                };
                command.ExecuteNonQuery();
                MessageBox.Show("Изменения сохранены");
                Window editor = Window.GetWindow(this);
                Windows.ClientManagerWindow climawin = new Windows.ClientManagerWindow();
                climawin.Show();
                editor.Owner = climawin;
                editor.Close();
            }
        }

        /// <summary>
        /// Handler for delete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["zooStoreDB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException)
                {
                    MessageBox.Show("Не удалось удалить запись");
                }
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"DELETE FROM [Product] WHERE [ProductArticleNumber] = '{Utils.StateManager.ProductVendorCode}'"
                };
                command.ExecuteNonQuery();
                MessageBox.Show("Запись удалена");
                Window editor = Window.GetWindow(this);
                Windows.ClientManagerWindow climawin = new Windows.ClientManagerWindow();
                climawin.Show();
                editor.Owner = climawin;
                editor.Close();
            }
        }
    }
}
