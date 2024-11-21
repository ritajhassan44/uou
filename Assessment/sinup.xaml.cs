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

namespace Assessment
{
    /// <summary>
    /// Interaction logic for sinup.xaml
    /// </summary>
    public partial class sinup : Page
    {

        projectTaskBEntities db = new projectTaskBEntities();
        public sinup()
        {
            InitializeComponent();

        
            
        }

        private void Login(object sender, RoutedEventArgs e)
        {

            string em = email.Text;
            int pass = int.Parse(newPassword.Text);
            var user = db.Users.FirstOrDefault(x => x.Email== email.Text);

            if (user == null)
            {
                MessageBox.Show("email not found.");
                return;
            }
            if (user.Email != em)
            {
                MessageBox.Show("incorrect eamil");
            }

            if (pass < 8)
            {
                MessageBox.Show("must new pass more than 8");
            }
            if (newPassword.Text != confirmPassword.Text)
            {
                MessageBox.Show("passwords do not match.");
                return;
            }
            if (!string.IsNullOrEmpty(newPassword.Text))
            {
                MessageBox.Show("write pass");
            }


            user.Password_ = newPassword.Text;
            
            db.SaveChanges();

            MessageBox.Show("psassword has been reset succes");
            NavigationService.Navigate(new LoginPage());
        }
    }
}
