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
    /// Interaction logic for IssueBooks.xaml
    /// </summary>
    public partial class IssueBooks : Window
    {
        List<Book> listBook = new List<Book>();
        public IssueBooks()
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
            listBook = Globals.db.GetAllBooks();
            for (int i=0;i<listBook.Count;i++)
            {
                comboBook.Items.Add(listBook[i].bName+"  by  " + listBook[i].bAuthor);
            }

        }
       int idStudent = -1;
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
            }
            else
            {
                tbName.Clear();
                tbDep.Clear();
                tbSem.Clear();
                tbContact.Clear();
                tbEmail.Clear();
                 MessageBox.Show("Invalid Enrollment No","Error",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }
        int count = 0;
        private void btIssue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbName.Text != "")
                {
                    count = Globals.db.countIssuedBooks(idStudent);
                    if (comboBook.SelectedIndex != -1 && count <= 2)
                    {
                        
                       
                        int idBook = listBook[comboBook.SelectedIndex].bId;
                        if (dateIssue.SelectedDate == null)
                        {
                            MessageBox.Show("Issue Date can not be null");
                            return;
                        }
                        DateTime issueDate = (DateTime)dateIssue.SelectedDate;
                        Globals.db.AddIssuedBook(idStudent, idBook, issueDate);
                        MessageBox.Show("Book Issued.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    else
                    {
                        if(count>=3)
                        MessageBox.Show("Maximum number of book ISSUED ", "limit", MessageBoxButton.OK, MessageBoxImage.Information);
                        else
                            MessageBox.Show("Selecte a book please. ", "select a book", MessageBoxButton.OK, MessageBoxImage.Information);


                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("This book has been  alredy Issued.");
            }
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            tbName.Clear();
            tbDep.Clear();
            tbSem.Clear();
            tbContact.Clear();
            tbEmail.Clear();
            comboBook.SelectedIndex = -1;
            dateIssue.Text = "";
            tbEnroll.Clear();

        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tbEnroll_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
