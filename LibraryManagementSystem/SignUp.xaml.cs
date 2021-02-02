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
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            string email = tbEmail.Text;
            string user = tbUserName.Text;
            string pass1 = tbpass1.Password;
            string pass2 = tbpass2.Password;
            if (pass1 == pass2)
            {
                try
                {
                    Login login = new Login(user, email, pass1);
                    bool foundEmail = Globals.db.CheckEmailForSignUp(login);
                    if (foundEmail)
                    {
                        MessageBox.Show("this email address has been  already registered.");
                        return;
                    }
                }
                catch(InvalidValueException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                
            }
            else
            {
                MessageBox.Show("Password Error!");
                return;
            }
            MessageBox.Show("Your account Created succesfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            DialogResult = true;
        }

    }
}
