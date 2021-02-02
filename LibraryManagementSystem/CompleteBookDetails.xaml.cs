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
    /// Interaction logic for CompleteBookDetails.xaml
    /// </summary>
    public partial class CompleteBookDetails : Window
    {
        public CompleteBookDetails()
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
            List<BookDetails> listDetails = new List<BookDetails>();
            listDetails = Globals.db.GetAllDetails();
            lvDetails.ItemsSource = listDetails;
        }
    }
}
