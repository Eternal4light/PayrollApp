using PayrollApp.Model.Entity;
using PayrollApp.Model;
using PayrollApp.Utility;
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

namespace PayrollApp.View
{
    public partial class LoginWindow : Window
    {
        ApplicationContext db;
        public LoginWindow()
        {
            //ClearDataBaseForTest();
            SetDemoContent();
            InitializeComponent();
        }
        private void ClearDataBaseForTest()
        {
            db = new ApplicationContext();
            List<Worker> works = db.Workers.ToList();
            List<Authorizer> auths = db.Authorizers.ToList();
            foreach (var el in works)
            {
                db.Workers.Remove(el);
            }
            foreach (var el in auths)
            {
                db.Authorizers.Remove(el);
            }

            db.SaveChanges();
        }
        private void Button_Entry_Click(object sender, RoutedEventArgs e)
        {
            Worker user = null;
            List<Worker> dbWorkers = db.Workers.ToList();
            db = new ApplicationContext();
            Authorizer example = new Authorizer()
            {
                Login = LoginTB.Text,
                Password = Protector.GetSafePassword(PassBox.Password)
            };
            
            List<Authorizer> loginList = db.Authorizers.ToList();
            foreach (var el in loginList)
            {
               if ( el.Login == example.Login)
                {
                    if (el.Password == example.Password)
                    {
                       
                        foreach (var dbWorker in dbWorkers)
                        {
                            if (el.WorkerId == dbWorker.Id)
                            {
                                user = dbWorker;
                                break;
                            }
                        }
                        break;
                    }  
                } 
            }
            if (user == null) { MessageBox.Show("Ошибка"); }
            else if (user.Position == "Админ") 
            {
                var mv = new MainWindow();
                mv.RefreshButton.Click += mv.SetViewContentForOtherVersions;
                this.Close();
                mv.Show();
            }
            else
            { 
                var mv = new MainWindow();
                mv.SLButton.Visibility = Visibility.Collapsed;
                mv.AddButton.Visibility = Visibility.Collapsed;
                mv.ChangeButton.Visibility = Visibility.Collapsed;
                mv.DeleteButton.Visibility = Visibility.Collapsed;
                mv.user = user;
                mv.SetPartOfContent(sender, e);
                mv.RefreshButton.Click += mv.SetPartOfContent;

                this.Close();
                mv.Show();
            }
        }
       
        private void SetDemoContent()
        {
            db = new ApplicationContext();
            List<Worker> checkList = db.Workers.ToList();
            List<Authorizer> authCheck = db.Authorizers.ToList();
            List<Worker> Admins = new List<Worker>();
            if (checkList.Count < 1)
            {
                Worker superUser = new Salesman()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "suName",
                    SecondName = "suSecName",
                    LastName = "Нет",
                    EmploymentDate = DateTime.Now,
                    Position = "Админ",
                    Rate = 0
                };
                checkList.Add(superUser);
                db.Workers.Add(superUser);
                db.SaveChanges();
            }
            foreach (var el in checkList)
            {
                if (el.Position == "Админ")
                    Admins.Add(el);
            }
            Admins.Reverse();
            if (authCheck.Count == 0)
            {
                var demoAuthorizer = new Authorizer()
                {
                    WorkerId = Admins[0].Id,
                    Login = "superUser",
                    Password = Protector.GetSafePassword("suPEruSEr")
                };
                db.Authorizers.Add(demoAuthorizer);
                db.SaveChanges();
            }
        }
    }
}
