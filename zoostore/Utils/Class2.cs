using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace zoostore.Utils
{
    internal class Class2
    {
        public static SqlDataReader GetTableItems(TextBox TextBoxSearch, ComboBox CategorySortComboBox, ComboBox PriceSortComboBox)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["zooStoreDB"].ConnectionString);
            connection.Open();
            string cmdTxt = $"SELECT * FROM [Продукты]";

            if (TextBoxSearch.Text != "")
            {
                cmdTxt = cmdTxt + $" WHERE ([Описание] LIKE '%{TextBoxSearch.Text}%' OR [Название] LIKE '%{TextBoxSearch.Text}%')";
            }
            if (CategorySortComboBox.SelectedValue != null)
            {
                if (cmdTxt.Contains("WHERE"))
                {
                    cmdTxt = cmdTxt + $" AND [Категория] = '{CategorySortComboBox.SelectedValue}'";
                }
                else
                {
                    cmdTxt = cmdTxt + $" WHERE [Категория] = '{CategorySortComboBox.SelectedValue}'";
                }
            }
            if (PriceSortComboBox.SelectedValue != null)
            {
                if ((string)PriceSortComboBox.SelectedValue == "Сначала дешёвые")
                {
                    cmdTxt = cmdTxt + $" ORDER BY [Стоимость] ASC";
                }
                else
                {
                    cmdTxt = cmdTxt + $" ORDER BY [Стоимость] DESC";
                }
            }

            SqlCommand commnad = new SqlCommand
            {
                Connection = connection,
                CommandText = cmdTxt
            };
            return commnad.ExecuteReader();
        }
    }
}
