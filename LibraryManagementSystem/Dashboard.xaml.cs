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

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
           
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
           if( MessageBox.Show("Are you sure you want to exit?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }

        }

        private void itemAddBook_Click(object sender, RoutedEventArgs e)
        {
            AddBooks abs = new AddBooks();
            abs.ShowDialog();


        }

        private void miViewBook_Click(object sender, RoutedEventArgs e)
        {
            ViewBook vb = new ViewBook();
            vb.ShowDialog();
        }

       

        private void miAddStudent_Click(object sender, RoutedEventArgs e)
        {
            AddStudent ast = new AddStudent();
            ast.ShowDialog();
        }

        private void miViewStudent_Click(object sender, RoutedEventArgs e)
        {
            ViewStudent vst = new ViewStudent();
            vst.ShowDialog();
        }

        private void miIssueBooks_Click(object sender, RoutedEventArgs e)
        {
            IssueBooks issueB = new IssueBooks();
            issueB.ShowDialog();

        }

        private void miReturnBook_Click(object sender, RoutedEventArgs e)
        {
            ReturnBook rb = new ReturnBook();
            rb.ShowDialog();
        }

        private void mibookDetail_Click(object sender, RoutedEventArgs e)
        {
            CompleteBookDetails completeBookDetails = new CompleteBookDetails();
            completeBookDetails.ShowDialog();
        }
    }
}
