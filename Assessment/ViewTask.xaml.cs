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
    /// Interaction logic for ViewTask.xaml
    /// </summary>
    public partial class ViewTask : Page
    {
        projectTaskBEntities db = new projectTaskBEntities();
        public ViewTask(string name)
        {
            InitializeComponent();
            labelname.Content = name;
                      

            PendingDG.ItemsSource = db.Tasks.Where(x => x.Status_ == "Pending" || x.Status_ == "In Progress")
                                            .Select(x => new {x.TaskID,x.Title,x.Description_,x.Status_}).ToList();

            CompletedDG.ItemsSource = db.Tasks.Where(x => x.Status_ == "Completed")
                                            .Select(x => new { x.TaskID, x.Title, x.Description_, x.Status_ }).ToList();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                if (idtext.Text == null && combo.Text == null)
                {
                    MessageBox.Show("enter id and stauts");
                    return;
                }

                 int id = int.Parse(idtext.Text);   
                var check = db.Tasks.FirstOrDefault(x => x.TaskID == id);

                if (check == null)
                {
                    MessageBox.Show("id not found");
                }

              

                check.Status_ = combo.Text;

                db.Tasks.AddOrUpdate(check);
                db.SaveChanges();

               
                PendingDG.ItemsSource = db.Tasks
                    .Where(x => x.Status_ == "Pending" || x.Status_ == "In Progress")
                    .Select(x => new { x.TaskID, x.Title, x.Description_, x.Status_ }).ToList();

                CompletedDG.ItemsSource = db.Tasks
                    .Where(x => x.Status_ == "Completed")
                    .Select(x => new { x.TaskID, x.Title, x.Description_, x.Status_ }).ToList();

                MessageBox.Show("you make update scuss");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
