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
    /// Interaction logic for AddRowWindow.xaml
    /// </summary>
    public partial class AddRowWindow : Window
    {
        public AddRowWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["zooStoreDB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException)
                {
                    MessageBox.Show("Не удалось добавить запись");
                }
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"INSERT INTO [Product] ([ProductArticleNumber], [ProductName], [ProductDescription], [ProductCategory], [ProductManufacturer], [ProductProvider], [ProductUnitType], [ProductCost], [ProductMaxDiscount], [ProductQuantityInStock]) VALUES ('{PRarticle.Text}', '{PRname.Text}', '{PRdescription.Text}', '{PRcategory.Text}', '{PRmanufacter.Text}', '{PRprovider.Text}', '{PRunitType.Text}', {PRcost.Text}, {PRmaxDiscount.Text}, {PRinStock.Text})"
                };
                command.ExecuteNonQuery();
                MessageBox.Show("Запись внесена");
                Window editor = Window.GetWindow(this);
                Windows.ClientManagerWindow climawin = new Windows.ClientManagerWindow();
                climawin.Show();
                editor.Owner = climawin;
                editor.Close();
            }
        }
    }
}
