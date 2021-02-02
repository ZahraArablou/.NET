using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for AddBooks.xaml
    /// </summary>
    public partial class AddBooks : Window
    {
        public AddBooks()
        {
            InitializeComponent();
            Globals.db = new Database();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {   if (tbAuthor.Text=="" && tbBookName.Text=="" && tbPublication.Text=="" && tbPrice.Text==""&& tbPublication.Text == "")
                {
                    throw new FormatException("Empty fileds NOT Allowed !");
                }

                    
                string bName = tbBookName.Text;
                string bAuthor = tbAuthor.Text;
                string bPubl = tbPublication.Text;
                if (datePurchase.SelectedDate == null)
                {
                    MessageBox.Show("please enter a date");
                    return;
                }
                DateTime pubDate = (DateTime)datePurchase.SelectedDate;
                int price = Int32.Parse(tbPrice.Text);//TODO Exceptions
                int quan = Int32.Parse(tbQuantity.Text);//TODO Exceptions
                Book book = new Book(0, bName, bAuthor, bPubl, pubDate, price, quan);
                Globals.db.AddBook(book);
                MessageBox.Show("Data Saved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                tbBookName.Clear();
                tbAuthor.Clear();
                tbPrice.Clear();
                tbQuantity.Clear();
                tbPublication.Clear();
                datePurchase.Text = "";

            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }catch(FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
          
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure?\nThis will DELETE your unsaved DATA.", "Are you sure?",MessageBoxButton.OKCancel,MessageBoxImage.Warning)==MessageBoxResult.OK)
                    {
                this.Close();
            }
        }
    }
}
