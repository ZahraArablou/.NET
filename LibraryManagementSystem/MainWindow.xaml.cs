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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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

        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tbusername_MouseEnter(object sender, MouseEventArgs e)
        {
            if (tbusername.Text == "Username")
            { tbusername.Clear(); }
        }

        private void tbPassword_MouseEnter(object sender, MouseEventArgs e)
        {
            if(tbPassword.Password=="Password")
            {
                tbPassword.Clear();
            }
        }

        private void tbPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void imgFacebook_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=YhAwNITpnno");
                
        }
        //SqlConnection con = new SqlConnection("Data Source=(local);Initial catalog=DBLogin; Integrated Security=True");

        //SqlCommand cmd = new SqlCommand();
        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            //  con.Open();
            //string query = "select * from Table_login1 where username='" + tbusername.Text + "'and pass='" + tbPassword.Password + "' ";
            int i = Globals.db.loginCheck(tbusername.Text,tbPassword.Password);
        
            if (i > 0)
            {
                this.Hide();
                Dashboard ds=new Dashboard();
                ds.ShowDialog();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void imgFacebook_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void btSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Owner = this;
            signUp.ShowDialog();

        }
    }
}
