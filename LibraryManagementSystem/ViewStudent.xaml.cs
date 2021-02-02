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
    /// Interaction logic for ViewStudent.xaml
    /// </summary>
    public partial class ViewStudent : Window
    {
        List<Student> studentList = new List<Student>();
        public ViewStudent()
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
            gridStudent.Visibility = Visibility.Hidden;
            refreshList();
        }
        private void refreshList()
        {
            studentList.Clear();
            studentList = Globals.db.GetAllStudents();
            lvStudents.ItemsSource = studentList;
        }
        void clearfields()
        {
            tbSearchEnrol.Clear();
            tbSName.Clear();
            tbEnroll.Clear();
            tbDep.Clear();
            tbSem.Clear();
            tbContact.Clear();
            tbEmail.Clear();
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            tbSearchEnrol.Clear();
            refreshList();
            gridStudent.Visibility = Visibility.Hidden;
        }

        private void tbSearchEnrol_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSearchEnrol.Text != "")
            {
                image1.Source = new BitmapImage(new Uri("C:/Users/heshmat/Documents/dotnet/LibraryManagementSystem/Image/search1.gif"));
                lblView.Visibility = Visibility.Hidden;
            }
            else
            {
                image1.Source = new BitmapImage(new Uri("C:/Users/heshmat/Documents/dotnet/LibraryManagementSystem/Image/search.gif"));
                lblView.Visibility = Visibility.Visible;
            }
                studentList.Clear();
            studentList = Globals.db.SearchStudent(tbSearchEnrol.Text);
            lvStudents.ItemsSource = studentList;
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            Student s = (Student)lvStudents.SelectedItem;
            try
            {
                if (MessageBox.Show("Data will be deleted. Confirm? ", " Deletion", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                
                        Globals.db.deleteStudentFromBorrowBook(s.SId);
                    Globals.db.DeleteStudent(s.SId);
                    refreshList();
                    //MessageBox.Show("Data Deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    //clearfields();

                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Errot: " + ex.Message);
            }

        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Unsaved data will be lost", "Are you sure", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK) { 
            gridStudent.Visibility = Visibility.Hidden;
        }
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
           
            
                try
                {
                    if (tbSName.Text != "" && tbEnroll.Text != "" && tbDep.Text != "" && tbSem.Text != "" && tbContact.Text != "" && tbEmail.Text != "")

                     {
                        Student s = studentList[lvStudents.SelectedIndex];
                        int id = s.SId;
                        string name = tbSName.Text;
                        string enroll = tbEnroll.Text;
                        string dep = tbDep.Text;

                        string sem = tbSem.Text;
                        string email = tbEmail.Text;//TODO Exceptions
                        string contact = tbContact.Text;//TODO Exceptions
                        Student student = new Student(id, name, enroll, dep, sem, contact, email);
                    if (MessageBox.Show("Data will be updated.Confirm?", "Success", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                    {
                        Globals.db.UpdateStudent(student);
                        refreshList();
                        //  MessageBox.Show("Data Saved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        clearfields();
                    }
                    }else
                    {
                        
                            throw new FormatException("Empty fileds NOT Allowed !");
                            return;

                      
                    }
                                   

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (InvalidValueException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
       

        private void lvStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lvStudents.SelectedIndex < 0)
            {
                return;
            }
            gridStudent.Visibility = Visibility.Visible;
            Student s = (Student)lvStudents.SelectedItem;
            tbSName.Text = s.SName;
            tbEnroll.Text = s.Enroll;
            tbDep.Text = s.Dep;
            tbSem.Text = s.Sem;
            tbContact.Text = s.Contact;
            tbEmail.Text = s.Email;

        }
    }
}
