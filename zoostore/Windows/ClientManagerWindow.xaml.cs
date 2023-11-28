using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
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
using Microsoft.Data.SqlClient;
using zoostore.Utils;

namespace zoostore.Windows
{
    /// <summary>
    /// Interaction logic for ClientManagerWindow.xaml
    /// </summary>
    public partial class ClientManagerWindow : Window
    {
        public ClientManagerWindow()
        {
            InitializeComponent();
            GetTableView();

            GetAllCategories();
            SetPriceSort();
            LabelFullName();
            if (Utils.StateManager.UserRole == "3")
            {
                AddRowButton.Visibility = Visibility.Visible;
            }
        }



        private void SetPriceSort()
        {
            PriceSortComboBox.Items.Add("Сначала дешёвые");
            PriceSortComboBox.Items.Add("Сначала дорогие");
        }
        /// <summary>
        /// Getting all products categories from DB 
        /// </summary>
        private void GetAllCategories()
        {
            using(SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["zooStoreDB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                }
                catch(SqlException)
                {
                    MessageBox.Show("Не удалось получить категории");
                }
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT DISTINCT [ProductCategory] FROM [Product]"
                };
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CategorySortComboBox.Items.Add(reader[0].ToString());
                }
                CategorySortComboBox.SelectedValue = null;
                connection.Close();
            }
        }

        /// <summary>
        /// Get and insert data from DB into DataGrid in xaml
        /// </summary>
        private void GetTableView()
        {
            ProductView.ItemsSource = Utils.Class2.GetTableItems(TextBoxSearch, CategorySortComboBox, PriceSortComboBox);
        }
        /// <summary>
        /// Open EditingWindow
        /// </summary>
        private void UpdateRow()
        {
            var selectedRowInfo = ProductView.SelectedCells[0];
            var selectedRowValue = (selectedRowInfo.Column.GetCellContent(selectedRowInfo.Item) as TextBlock).Text;

            Utils.StateManager.ProductVendorCode = selectedRowValue.ToString();
            Utils.StateManager.IsEditing = true;

            Window climawin = Window.GetWindow(this);
            Windows.EditingWindow editor = new Windows.EditingWindow();
            editor.Show();
            climawin.Owner = editor;
            climawin.Close();

            //MessageBox.Show(Utils.StateManager.ProductVendorCode);
        }

        /// <summary>
        /// Handler for ComboBox of categories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategorySortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetTableView();
        }

        /// <summary>
        /// Handler for ComboBox of prices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PriceSortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetTableView();
        }

        /// <summary>
        /// Handler for TextBox to search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetTableView();
        }

        /// <summary>
        /// Handler for DataGridRow when double click on it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(Utils.StateManager.UserRole == "3")
            {
                UpdateRow();
            }
        }

        /// <summary>
        /// Handler for Button that open AddRowWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRowButton_Click(object sender, RoutedEventArgs e)
        {
            Utils.StateManager.IsEditing = false;
            Window climawin = Window.GetWindow(this);
            Windows.AddRowWindow addwin = new Windows.AddRowWindow();
            addwin.Show();
            climawin.Owner = addwin;
            climawin.Close();
        }

        /// <summary>
        /// Handler for label to auto insert full name of user
        /// </summary>
        private void LabelFullName()
        {
            string fullName = StateManager.UserName +" "+ StateManager.UserSurname +" "+ StateManager.UserPatronymic;
            UserFullNameLabel.Content = fullName;
        }
    }
}
