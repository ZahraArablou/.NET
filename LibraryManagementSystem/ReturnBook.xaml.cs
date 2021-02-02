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
    /// Interaction logic for ReturnBook.xaml
    /// </summary>
    public partial class ReturnBook : Window
    {
        List<BookIssued> listBorrowed = new List<BookIssued>();
        public ReturnBook()
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
        }
       int idStudent=-1;
        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            Student s = Globals.db.GetStudentByEnrollNo(tbEnroll.Text);
            //MessageBox.Show(s.SName);
            if (s != null)
            {
                tbName.Text = s.SName;
                tbDep.Text = s.Dep;
                tbSem.Text = s.Sem;
                tbContact.Text = s.Contact;
                tbEmail.Text = s.Email;
                idStudent = s.SId;
                listBorrowed = Globals.db.GetBorrowedBook(idStudent);
                lvBooks.ItemsSource = listBorrowed;


            }
            else
            {
                tbName.Clear();
                tbDep.Clear();
                tbSem.Clear();
                tbContact.Clear();
                tbEmail.Clear();
                MessageBox.Show("Invalid Enrollment No", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            grid1.Visibility = Visibility.Hidden;
            tbEnroll.Clear();
            tbName.Clear();
            tbDep.Clear();
            tbSem.Clear();
            tbContact.Clear();
            tbEmail.Clear();

            listBorrowed.Clear();
            lvBooks.Items.Refresh();
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        int selectedBookId = -1;
        private void lvBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lvBooks.SelectedIndex==-1)
            {
                return;
            }
            grid1.Visibility = Visibility.Visible;
            tbBookName.Text = listBorrowed[lvBooks.SelectedIndex].bName;
            DateTime date = listBorrowed[lvBooks.SelectedIndex].IssueDate;
            selectedBookId = listBorrowed[lvBooks.SelectedIndex].bId;
            string date1 = date.ToShortDateString();
            tbDate.Text = date1;

            DateTime dateToday= DateTime.Today;
            dateReturn.SelectedDate = dateToday;

        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            grid1.Visibility = Visibility.Hidden;
        }

        private void btReturn_Click(object sender, RoutedEventArgs e)
        {
                 
           
            DateTime returnDate = (DateTime)dateReturn.SelectedDate;
            Globals.db.UpdateBorrowBook(returnDate,selectedBookId);
            MessageBox.Show("Return Succesful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            listBorrowed.Clear();
            listBorrowed = Globals.db.GetBorrowedBook(idStudent);
            lvBooks.ItemsSource = listBorrowed;


        }
    }
}
