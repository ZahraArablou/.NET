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
    /// Interaction logic for ViewBook.xaml
    /// </summary>
    public partial class ViewBook : Window
    {
        List<Book> bookList = new List<Book>();
        public ViewBook()
        {
            InitializeComponent();
            try
            {
                Globals.db = new Database();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Fatal Error " + ex.Message);
                Environment.Exit(1);
            }
            grid1.Visibility = Visibility.Hidden;
            refreshList();
            // gridlist.ItemsSource = bookList;
        }

        private void refreshList()
        {
            bookList.Clear();
            bookList = Globals.db.GetAllBooks();
            lvBooks.ItemsSource = bookList;
        }
        private void clearFields()
        {
            tbName.Clear();
            tbAuthor.Clear();
            tbPubl.Clear();
            datePicker.Text = "";
            tbPrice.Clear();
            tbQuan.Clear();
        }

        private void lvBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvBooks.SelectedIndex < 0)
            {
                return;
            }
            grid1.Visibility = Visibility.Visible;
            Book b = (Book)lvBooks.SelectedItem;
            tbName.Text = b.bName;
            tbAuthor.Text = b.bAuthor;
            tbPubl.Text = b.bPubl;
            datePicker.SelectedDate = b.bPDate;
            tbPrice.Text = b.bPrice.ToString();
            tbQuan.Text = b.bQuan.ToString();
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            Book b = (Book)lvBooks.SelectedItem;
            try
            {
                if (MessageBox.Show("Data will be deleted. Confirm? ", " Deletion", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    
                {
                    Globals.db.DeleteBookFromBorrowBook(b.bId);
                    Globals.db.DeleteBook(b.bId);
                    refreshList();
                    MessageBox.Show("Data Deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearFields();

                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Errot: " + ex.Message);
            }

        }

      

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            grid1.Visibility = Visibility.Hidden;
            //if (MessageBox.Show("Are you sure?", "confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            //{
            //    this.Close();
            //}
        }

        private void tbBName_TextChanged(object sender, TextChangedEventArgs e)
        {
            bookList.Clear();
            bookList= Globals.db.SearchBook(tbBName.Text);
            lvBooks.ItemsSource = bookList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tbBName.Clear();
            refreshList();
            grid1.Visibility = Visibility.Hidden;
           

        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Data will be updated.Confirm?", "Success", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                try
                {
                    if (tbAuthor.Text == "" && tbName.Text == "" && tbPubl.Text == "" && tbPrice.Text == "" && tbQuan.Text == "")
                    {
                        throw new FormatException("Empty fileds NOT Allowed !");
                    }

                    var b = bookList[lvBooks.SelectedIndex];
                    int id = b.bId;
                    string bName = tbName.Text;
                    string bAuthor = tbAuthor.Text;
                    string bPubl = tbPubl.Text;
                    if (datePicker.SelectedDate == null)
                    {
                        MessageBox.Show("please enter a date");
                        return;
                    }
                    DateTime pubDate = (DateTime)datePicker.SelectedDate;
                    int price = Int32.Parse(tbPrice.Text);//TODO Exceptions
                    int quan = Int32.Parse(tbQuan.Text);//TODO Exceptions
                    Book book = new Book(id, bName, bAuthor, bPubl, pubDate, price, quan);
                    Globals.db.UpdateBook(book);
                    refreshList();
                  //  MessageBox.Show("Data Saved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearFields();

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
