using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Assessment
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        projectTaskBEntities db = new projectTaskBEntities();
        public LoginPage()
        {
            InitializeComponent();
        }
        private void Login(object sender, RoutedEventArgs e)
        {
            try
            {

                var user = db.Users.FirstOrDefault(x => x.Email == emailtext.Text);
                int incorrec = 0;

                if (user == null)
                {
                    MessageBox.Show("Email not found.");
                }


                if (user.Password_ != password.Text)
                {
                   
                    MessageBox.Show("invalid password.");


                    return;

                }
                if (user.Password_ != password.Text)
                {
                    incorrec++;
                    MessageBox.Show("invald password.");

                    if (incorrec >= 2)
                    {
                        MessageBox.Show("incorrec pass navgaite to forget pass page.");
                        NavigationService.Navigate(new sinup(user.Email));
                        password.Clear();
                       

                    }
                

                }

                    if (user.Role_ == "Manger")
                    {
                        NavigationService.Navigate(new UserManagement());
                    }

                    else if (user.Role_ == "Employee")
                    {
                        NavigationService.Navigate(new ViewTask(user.Name_));
                    }

                    if (emailtext.Text == "")
                    {
                        MessageBox.Show("enter valid email .");
                    }


                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void sigup(object sender, RoutedEventArgs e)
        {
            var email =db.Users.FirstOrDefault(x=>x.Email == emailtext.Text);
            sinup sinup=new sinup();
            this.NavigationService.Navigate(sinup);
        }
    }
}
