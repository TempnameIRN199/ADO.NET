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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Exam.DBContext;
using Exam.EditWindow;
using Microsoft.SqlServer.Server;

namespace Exam
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Olympics olympics1 = new Olympics();
        Sports sports1 = new Sports();
        Members members1 = new Members();
        Results results1 = new Results();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded()
        {
            using (NintendoContext db = new NintendoContext())
            {
                OlympicsListView.ItemsSource = null;
                SportsListView.ItemsSource = null;
                MembersListView.ItemsSource = null;
                ResultsListView.ItemsSource = null;

                var olympicsList = (from olympics in db.Olympics select olympics).ToList();
                OlympicsListView.ItemsSource = olympicsList;

                var sportsList = (from sports in db.Sports select sports).ToList();
                SportsListView.ItemsSource = sportsList;

                var membersList = (from members in db.Members select members).ToList();
                MembersListView.ItemsSource = membersList;

                var resultsList = (from results in db.Results select results).ToList();
                ResultsListView.ItemsSource = resultsList;
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            using (NintendoContext db = new NintendoContext())
            {
                var olympicsList = (from olympics in db.Olympics select olympics).ToList();
                OlympicsListView.ItemsSource = olympicsList;

                var sportsList = (from sports in db.Sports select sports).ToList();
                SportsListView.ItemsSource = sportsList;

                var membersList = (from members in db.Members select members).ToList();
                MembersListView.ItemsSource = membersList;

                var resultsList = (from results in db.Results select results).ToList();
                ResultsListView.ItemsSource = resultsList;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (OlympicsPage.IsSelected)
            {
                AddWindow.WindowAdd windowAdd = new AddWindow.WindowAdd("OlympicsAdd");
                windowAdd.ShowDialog();
            }
            else if (SportsPage.IsSelected)
            {
                AddWindow.WindowAdd windowAdd = new AddWindow.WindowAdd("SportsAdd");
                windowAdd.ShowDialog();
            }
            else if (MembersPage.IsSelected)
            {
                AddWindow.WindowAdd windowAdd = new AddWindow.WindowAdd("MembersAdd");
                windowAdd.ShowDialog();
            }
            else if (ResultsPage.IsSelected)
            {
                AddWindow.WindowAdd windowAdd = new AddWindow.WindowAdd("ResultsAdd");
                windowAdd.ShowDialog();
            }
            Window_Loaded();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (OlympicsPage.IsSelected)
            {
                if (OlympicsListView.SelectedItems != null)
                {
                    if (OlympicsListView.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Виберіть відділ для редагування");
                        return;
                    }
                    else
                    {
                        using (NintendoContext db = new NintendoContext())
                        {
                            foreach (Olympics olympics in OlympicsListView.SelectedItems)
                            {
                                olympics1 = db.Olympics.Find(olympics.Id);
                            }
                            if (olympics1 == null)
                            {
                                MessageBox.Show("Не суй туди руки");
                                return;
                            }
                            else
                            {
                                EditWindow.WindowEdit windowEdit = new EditWindow.WindowEdit("OlympicsEdit", typeof(Olympics), olympics1);
                                windowEdit.ShowDialog();
                            }
                        }
                    }
                }
            }
            else if (SportsPage.IsSelected)
            {
                if (SportsListView.SelectedItems != null)
                {
                    if (SportsListView.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Виберіть відділ для редагування");
                        return;
                    }
                    else
                    {
                        using (NintendoContext db = new NintendoContext())
                        {
                            foreach (Sports sports in SportsListView.SelectedItems)
                            {
                                sports1 = db.Sports.Find(sports.Id);
                            }
                            if (sports1 == null)
                            {
                                MessageBox.Show("Не суй туди руки");
                                return;
                            }
                            else
                            {
                                EditWindow.WindowEdit windowEdit = new EditWindow.WindowEdit("SportsEdit", typeof(Sports), olympics1, sports1);
                                windowEdit.ShowDialog();
                            }
                        }
                    }
                }
            }
            else if (MembersPage.IsSelected)
            {
                if (MembersListView.SelectedItems != null)
                {
                    if (MembersListView.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Виберіть відділ для редагування");
                        return;
                    }
                    else
                    {
                        using (NintendoContext db = new NintendoContext())
                        {
                            foreach (Members members in MembersListView.SelectedItems)
                            {
                                members1 = db.Members.Find(members.Id);
                            }
                            if (members1 == null)
                            {
                                MessageBox.Show("Не суй туди руки");
                                return;
                            }
                            else
                            {
                                EditWindow.WindowEdit windowEdit = new EditWindow.WindowEdit("MembersEdit", typeof(Members), olympics1, sports1, members1);
                                windowEdit.ShowDialog();
                            }
                        }
                    }
                }
            }
            else if (ResultsPage.IsSelected)
            {
                if (ResultsListView.SelectedItems != null)
                {
                    if (ResultsListView.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Виберіть відділ для редагування");
                        return;
                    }
                    else
                    {
                        using (NintendoContext db = new NintendoContext())
                        {
                            foreach (Results results in ResultsListView.SelectedItems)
                            {
                                results1 = db.Results.Find(results.Id);
                            }
                            if (results1 == null)
                            {
                                MessageBox.Show("Не суй туди руки");
                                return;
                            }
                            else
                            {
                                EditWindow.WindowEdit windowEdit = new EditWindow.WindowEdit("ResultsEdit", typeof(Results), olympics1, sports1, members1, results1);
                                windowEdit.ShowDialog();
                            }
                        }
                    }
                }
            }
            Window_Loaded();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (OlympicsPage.IsSelected)
            {
                if (OlympicsListView.SelectedItems != null)
                {
                    if (OlympicsListView.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Виберіть олімпіаду для видалення");
                        return;
                    }
                    else
                    {
                        using (NintendoContext db = new NintendoContext())
                        {
                            for (int i = 0; i < OlympicsListView.SelectedItems.Count; i++)
                            {
                                Olympics olympics = OlympicsListView.SelectedItems[i] as Olympics;
                                if (olympics != null)
                                {
                                    db.Olympics.Attach(olympics);
                                    try
                                    {
                                        db.Olympics.Remove(olympics);
                                    }
                                    catch (SqlException ex)
                                    {
                                        MessageBox.Show($"Не можна видалити цей елемент, оскільки він пов'язаний із сутністю. ex: {ex}");
                                    }
                                    finally
                                    {
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (SportsPage.IsSelected)
            {
                if (SportsListView.SelectedItems != null)
                {
                    if (SportsListView.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Виберіть вид спорту для видалення");
                        return;
                    }
                    else
                    {
                        using (NintendoContext db = new NintendoContext())
                        {
                            for (int i = 0; i < SportsListView.SelectedItems.Count; i++)
                            {
                                Sports sports = SportsListView.SelectedItems[i] as Sports;
                                if (sports != null)
                                {
                                    db.Sports.Attach(sports);
                                    try
                                    {
                                        db.Sports.Remove(sports);
                                    }
                                    catch(SqlException ex)
                                    {
                                        MessageBox.Show($"Не можна видалити цей елемент, оскільки він пов'язаний із сутністю. ex: {ex}");
                                    }
                                    finally
                                    {
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (MembersPage.IsSelected)
            {
                if (MembersListView.SelectedItems != null)
                {
                    if (MembersListView.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Виберіть спортсмена для видалення");
                        return;
                    }
                    else
                    {
                        using (NintendoContext db = new NintendoContext())
                        {
                            for (int i = 0; i < MembersListView.SelectedItems.Count; i++)
                            {
                                Members members = MembersListView.SelectedItems[i] as Members;
                                if (members != null)
                                {
                                    db.Members.Attach(members);
                                    try
                                    {
                                        db.Members.Remove(members);
                                    }
                                    catch (SqlException ex)
                                    {
                                        MessageBox.Show($"Не можна видалити цей елемент, оскільки він пов'язаний із сутністю. ex: {ex}");
                                    }
                                    finally
                                    {
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (ResultsPage.IsSelected)
            {
                if (ResultsListView.SelectedItems != null)
                {
                    if (ResultsListView.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Виберіть результат для видалення");
                        return;
                    }
                    else
                    {
                        using (NintendoContext db = new NintendoContext())
                        {
                            for (int i = 0; i < ResultsListView.SelectedItems.Count; i++)
                            {
                                Results results = ResultsListView.SelectedItems[i] as Results;
                                if (results != null)
                                {
                                    db.Results.Attach(results);
                                    try
                                    {
                                        db.Results.Remove(results);
                                    }
                                    catch (SqlException ex)
                                    {
                                        MessageBox.Show($"Не можна видалити цей елемент, оскільки він пов'язаний із сутністю. ex: {ex}");
                                    }
                                    finally
                                    {
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Window_Loaded();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            Windows.WindowInfo windowInfo = new Windows.WindowInfo();
            windowInfo.ShowDialog();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
