using PayrollApp.Model;
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
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Xml.Linq;

namespace PayrollApp.View
{
    public partial class SetLoginWindow : Window
{
        public Worker worker = null;
        ApplicationContext db;
        public SetLoginWindow()
        {
            InitializeComponent(); 
        }
        private void Button_Click (object sender, RoutedEventArgs e)
        {
            string _login = LoginTB.Text.Trim();
            string _pass = SetPassBox.Password;
            string _pass2 = SetPassBox2.Password;

            if (_login.Length < 3)
            {
                LoginTB.ToolTip = "Введите по меньшей мере три буквы";
                LoginTB.Background = Brushes.DarkRed;
            }
            else
            {
                LoginTB.ToolTip = "";
                LoginTB.Background = Brushes.Transparent;
            }
            if (_pass.Length < 3)
            {
                SetPassBox.ToolTip = "Введите по меньшей мере три буквы";
                SetPassBox.Background = Brushes.DarkRed;
            }
            else
            {
                SetPassBox.ToolTip = "";
                SetPassBox.Background = Brushes.Transparent;
            }
            if (_pass != _pass2)
            {
                SetPassBox2.ToolTip = "Пароли должны совпадать";
                SetPassBox2.Background = Brushes.DarkRed;
            }
            else
            {
                SetPassBox2.ToolTip = "";
                SetPassBox2.Background = Brushes.Transparent;
            }
            if (_login.Length >= 3 & _pass.Length >= 3 & _pass == _pass2)
            {
                db = new ApplicationContext();

                var auth = new Model.Entity.Authorizer()
                {
                    WorkerId = worker.Id,
                    Login = _login,
                    Password = Utility.Protector.GetSafePassword(_pass)
                };
                db.Authorizers.Add(auth);
                db.SaveChanges();
                Close();
            }
        }
    }
}
