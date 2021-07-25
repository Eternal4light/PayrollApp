using PayrollApp.Model;
using PayrollApp.Model.Entity;
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

namespace PayrollApp
{
    public partial class MainWindow : Window
    {
        ApplicationContext db;
        Chiefs _chiefs;

        public MainWindow()
        {

            SetDemoContent();
            InitializeComponent();
            SetViewContent();
            SetDefaultAddTabContent();
        }

        

        private decimal GetWorkerSalary(Worker selected, DateTime date)
        {
            var vm = new ViewModel.PayViewModel();
            return vm.CalculateSalary(selected, date);
        }

        #region ButtonClick

        private void Subordinate_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ViewDataGrid.SelectedItem == null)
                return;
            var vm = new ViewModel.CountSubordinates();
            var selected = ViewDataGrid.SelectedItem as Worker;

             List<Worker> allSubs =  vm.GetAllSubs(selected);
            List<Worker> immediateSubs = vm.GetImmediateSubs(selected);
            List<Worker> secondarySubs = vm.GetSecondarySubs(selected);
            ViewDataGrid.ItemsSource = allSubs;
        }
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ViewDataGrid.SelectedItem == null)
                    return;
                var selected = ViewDataGrid.SelectedItem as Worker;
                currentSalaryTB.Text = GetWorkerSalary(selected, DateTime.Now).ToString();
                calculatedSalaryTB.Text = GetWorkerSalary(selected, calculateDP.DisplayDate).ToString();
                decimal sumNow = 0;
                decimal calcSum = 0;
                foreach (Worker el in ViewDataGrid.ItemsSource)
                {
                    if (el.LastName != "Зубенко")
                    {
                        sumNow += GetWorkerSalary(el, DateTime.Now);
                        calcSum += GetWorkerSalary(el, calculateDP.DisplayDate);
                    }
                }
                currentSummTB.Text = sumNow.ToString();
                calculatedSummTB.Text = calcSum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ViewDataGrid.SelectedItem == null)
                    return;
                var selected = ViewDataGrid.SelectedItem as Worker;
                empDate.SelectedDate = selected.EmploymentDate;
                position.SelectedItem = selected.Position;
                chief.SelectedItem = selected.Chief.LastName;
                rate.Text = selected.Rate.ToString();
                firstName.Text = selected.FirstName;
                secondName.Text = selected.SecondName;
                lastName.Text = selected.LastName;

                AddWorkerTabItem.Visibility = Visibility.Visible;
                ChangeWorkerHeaderTB.Visibility = Visibility.Visible;
                SaveChangesButton.Visibility = Visibility.Visible;
                MainTab.SelectedItem = AddWorkerTabItem;
                ViewTabItem.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultAddTabContent();
            AddWorkerTabItem.Visibility = Visibility.Visible;
            AddWorkerHeaderTB.Visibility = Visibility.Visible;
            UploadWorker.Visibility = Visibility.Visible;
            MainTab.SelectedItem = AddWorkerTabItem;
            ViewTabItem.Visibility = Visibility.Collapsed;
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewDataGrid.SelectedItem == null)
                return;
            var selected = ViewDataGrid.SelectedItem as Worker;
            db.Workers.Remove(selected);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не могу удалить работника, у которого есть подчиненные");
                MessageBox.Show(ex.Message);
            }
            SetViewContent();
        }
        private void UploadWorker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fName, sName, lName, pos;
                DateTime employDate;
                decimal rt;
                Guid chiefid, id;
                id = Guid.NewGuid();

                SetAddForm(out fName, out sName, out lName, out employDate, out rt, out pos, out chiefid);

                if (fName.Length >= 2 & sName.Length < 2 == false & lName.Length < 2 == false & empDate.SelectedDate != null & rate.Text != null &
                   rate.Text != "" & position.SelectedIndex < 0 == false & chief.SelectedIndex < 0 == false)
                {
                    Worker _worker;
                    if (pos == "Employee") { _worker = new Employee(); }
                    else if (pos == "Manager") { _worker = new Manager(); }
                    else if (pos == "Salesman") { _worker = new Salesman(); }
                    else _worker = new Employee();

                    _worker.Id = id;
                    _worker.FirstName = fName;
                    _worker.SecondName = sName;
                    _worker.LastName = lName;
                    _worker.EmploymentDate = employDate;
                    _worker.Rate = rt;
                    _worker.Position = pos;
                    _worker.ChiefId = chiefid;

                    db.Workers.Add(_worker);
                    db.SaveChanges();
                    SetViewContent();
                    ViewTabItem.Visibility = Visibility.Visible;
                    MainTab.SelectedItem = ViewTabItem;
                    AddWorkerHeaderTB.Visibility = Visibility.Collapsed;
                    UploadWorker.Visibility = Visibility.Collapsed;
                    AddWorkerTabItem.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            string fName, sName, lName, pos;
            DateTime employDate;
            decimal rt;
            Guid chiefid;

            SetAddForm(out fName, out sName, out lName, out employDate, out rt, out pos, out chiefid);
            if (fName.Length >= 2 & sName.Length < 2 == false & lName.Length < 2 == false & empDate.SelectedDate != null & rate.Text != null &
                   rate.Text != "" & position.SelectedIndex < 0 == false & chief.SelectedIndex < 0 == false)
            {
                Worker selected = ViewDataGrid.SelectedItem as Worker;
                string idString = selected.Id.ToString();               //Sqlite хранит Guid немного по-другому, поэтому сравнивать пришлось именно так
                var _worker = db.Workers.Find(Guid.Parse(idString));

                string _type = _worker.GetType().Name;
                Guid id = _worker.Id;
                if (_type.Contains(pos))
                {
                    if (pos == "Employee") { _worker = _worker as Employee; }
                    else if (pos == "Manager")
                    { _worker = _worker as Manager; }
                    else if (pos == "Salesman")
                    { _worker = _worker as Salesman; }
                    else { _worker = _worker as Employee; }

                    _worker.FirstName = fName;
                    _worker.SecondName = sName;
                    _worker.LastName = lName;
                    _worker.EmploymentDate = employDate;
                    _worker.Rate = rt;
                    _worker.Position = pos;
                    _worker.ChiefId = chiefid;
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        db.Workers.Remove(_worker);
                        if (pos == "Employee") { _worker = new Employee(); }
                        else if (pos == "Manager") { _worker = new Manager(); }
                        else if (pos == "Salesman") { _worker = new Salesman(); }
                        else _worker = new Employee();
                        _worker.Id = id;
                        _worker.FirstName = fName;
                        _worker.SecondName = sName;
                        _worker.LastName = lName;
                        _worker.EmploymentDate = employDate;
                        _worker.Rate = rt;
                        _worker.Position = pos;
                        _worker.ChiefId = chiefid;
                        db.Workers.Add(_worker);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не могу изменить должность работника, у которого есть подчиненные");
                        MessageBox.Show(ex.Message);
                    }
                }
                SetViewContent();
                ViewTabItem.Visibility = Visibility.Visible;
                MainTab.SelectedItem = ViewTabItem;
                ChangeWorkerHeaderTB.Visibility = Visibility.Collapsed;
                SaveChangesButton.Visibility = Visibility.Collapsed;
                AddWorkerTabItem.Visibility = Visibility.Collapsed;
            }
        }
        #endregion
        #region SetContent
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
                    Password = "suPEruSEr"
                };
                db.Authorizers.Add(demoAuthorizer);
                //db.SaveChanges
            }
        }

        private void SetDefaultAddTabContent()
        {
            var positionBoxContent = new Model.Position("Employee", "Manager", "Salesman");
            position.ItemsSource = positionBoxContent.CodeNames;

            firstName.Text = "";
            secondName.Text = "";
            lastName.Text = "";
            empDate.SelectedDate = DateTime.Now;
            position.SelectedItem = position.Items[0];
            chief.SelectedItem = chief.Items[0];
            rate.Text = "0";
        }
        private void SetViewContent()
        {
           
        

            db = new ApplicationContext();
            List<Worker> dbListWorkers = new List<Worker>();
            dbListWorkers = db.Workers.ToList();

            _chiefs = new Chiefs(dbListWorkers);  //Синхронные списки
            List<string> ChiefLNames = new List<string>();

            foreach (var el in _chiefs.AllChiefs)
            {
                ChiefLNames.Add(el.LastName);
            }

            int admins = -1;
            for (int i=0; i< dbListWorkers.Count; i++)
            {
                if (dbListWorkers[i].Position == "Админ")
                    admins = i;

            }
            if (admins >= 0)
                dbListWorkers.Remove(dbListWorkers[admins]);

           
           
            ViewDataGrid.ItemsSource = dbListWorkers;
            chief.ItemsSource = ChiefLNames;
            calculateDP.SelectedDate = DateTime.Now;
        }
        private void SetAddForm(out string fName, out string sName, out string lName, out DateTime employDate, out decimal rt, out string pos, out Guid chiefid)
        {
            fName = firstName.Text.Trim();
            sName = secondName.Text.Trim();
            lName = lastName.Text.Trim();
            employDate = (DateTime)empDate.SelectedDate;
            rt = decimal.Parse(rate.Text);
            pos = position.Text.Trim();
            chiefid = _chiefs.AllChiefs[chief.SelectedIndex].Id;

            if (fName.Length < 2)
            {
                firstName.ToolTip = "Введите по меньшей мере две буквы (например \"Ян\")";
                firstName.Background = Brushes.DarkRed;
            }
            else
            {
                firstName.ToolTip = "";
                firstName.Background = Brushes.Transparent;
            }
            if (sName.Length < 2)
            {
                secondName.ToolTip = "Введите по меньшей мере две буквы";
                secondName.Background = Brushes.DarkRed;
            }
            else
            {
                secondName.ToolTip = "";
                secondName.Background = Brushes.Transparent;
            }
            if (lName.Length < 2)
            {
                lastName.ToolTip = "Введите по меньшей мере две буквы (например \"Ли\")";
                lastName.Background = Brushes.DarkRed;
            }
            else
            {
                lastName.ToolTip = "";
                lastName.Background = Brushes.Transparent;
            }
            if (empDate.SelectedDate == null) { empDate.Background = Brushes.DarkRed; }
            else { empDate.Background = Brushes.Transparent; }
            if (rate.Text == null | rate.Text == "") { rate.Background = Brushes.DarkRed; }
            else { rate.Background = Brushes.Transparent; }
            if (position.SelectedIndex < 0) { position.Background = Brushes.DarkRed; }
            else { position.Background = Brushes.Transparent; }
            if (chief.SelectedIndex < 0) { chief.Background = Brushes.DarkRed; }
            else { chief.Background = Brushes.Transparent; }
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
