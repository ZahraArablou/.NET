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
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        public AddStudent()
        {
            InitializeComponent();
            Globals.db = new Database();
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        { if(MessageBox.Show("Confirm?","Alert",MessageBoxButton.OKCancel,MessageBoxImage.Warning)==MessageBoxResult.OK)
                    {
                this.Close();
            }
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            tbSName.Clear();
            tbSEnrollNo.Clear();
            tbDep.Clear();
            tbSSem.Clear();
            tbSContact.Clear();
            tbSEmail.Clear();

        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((tbSName.Text != "") && (tbSEnrollNo.Text != "") && (tbDep.Text != "") && (tbSSem.Text != "") && (tbSContact.Text != "") && (tbSEmail.Text != "") && (tbSEmail.Text != ""))
                // if(tbSName.Text != "" &&)
                {
                    Student s;
                    string name = tbSName.Text;
                    string enroll = tbSEnrollNo.Text;
                    string dep = tbDep.Text;
                    string sem = tbSSem.Text;
                    string contact = tbSContact.Text;
                    string email = tbSEmail.Text;
                    s = new Student(0, name, enroll, dep, sem, contact, email);
                    Globals.db.AddStudent(s);
                    MessageBox.Show("Data Saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {

                    throw new InvalidValueException("Empty fields NOT allowed!");
                    return;

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
            catch (InvalidValueException ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }
    }
}
