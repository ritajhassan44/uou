using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Page
    {
        projectTaskBEntities db = new projectTaskBEntities();
        public UserManagement()
        {
            InitializeComponent();
            TaskDG.ItemsSource = db.Tasks.Include(x => x.User)
            .Select(x => new { x.User.Email, x.TaskID, x.Title, x.Description_, x.Status_ }).ToList();
        }

        private void Add(object sender, RoutedEventArgs e)
        {

            try
            {

                var user = db.Users.FirstOrDefault(x => x.Email == emailtext.Text);
                if (user == null)
                {
                    MessageBox.Show("Enter invald eamil");
                }

                if (titletext.Text == "" || descriptiontext.Text == "" || emailtext.Text == "")
                {
                    MessageBox.Show("all fields  requir");
                }

                Task task = new Task
                {
                    Title = titletext.Text,
                    Description_ = descriptiontext.Text,
                    Status_ = "Pending",
                    TaskID = user.UserID
                };

                db.Tasks.Add(task);
                db.SaveChanges();

                TaskDG.ItemsSource = db.Tasks.Include(x => x.User.Email)
                    .Select(x => new { x.User.Email, x.TaskID, x.Title, x.Description_, x.Status_ })
                    .ToList();

                MessageBox.Show("Added Succe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            try
            {


                var id = int.Parse(taskidtext.Text);
                var task = db.Tasks.FirstOrDefault(x => x.TaskID == id);
                if (taskidtext.Text == "")
                {
                    MessageBox.Show("enter task ID.");
                }
                if (task == null)
                {
                    MessageBox.Show("task not found.");
                }
                 

                task.Title = titletext.Text;
                task.Description_ = descriptiontext.Text;
                task.Status_ = combo.Text;


                db.Tasks.AddOrUpdate(task);
                db.SaveChanges();

                TaskDG.ItemsSource = db.Tasks.Include(x => x.User.Email)
                    .Select(x => new { x.User.Email, x.TaskID, x.Title, x.Description_, x.Status_ })
                    .ToList();

                MessageBox.Show("updated Succes");
           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            try
            {

                var id = int.Parse(taskidtext.Text);
                var task = db.Tasks.FirstOrDefault(x => x.TaskID == id);

                if (taskidtext.Text == "")
                {
                    MessageBox.Show("Enter Task ID.");
                }

                if (task == null)
                {
                    MessageBox.Show("Task not found.");
                }


                db.Tasks.Remove(task);
                db.SaveChanges();

                TaskDG.ItemsSource = db.Tasks.Include(x => x.User.Email)
                    .Select(x => new { x.User.Email, x.TaskID, x.Title, x.Description_, x.Status_ })
                    .ToList();

                MessageBox.Show("Deleted Success");
            }
          
       
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
