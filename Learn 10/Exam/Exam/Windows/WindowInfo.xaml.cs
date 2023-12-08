using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using Exam.DBContext;

namespace Exam.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowInfo.xaml
    /// </summary>
    public partial class WindowInfo : Window
    {
        public ObservableCollection<MedalTableRow> Info { get; set; } = new ObservableCollection<MedalTableRow>();

        public WindowInfo()
        {
            InitializeComponent();
            DataContext = this;
            OlympicsComboBox.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (NintendoContext db = new NintendoContext())
            {
                Info.Clear();

                var medalInfoList = GetMedalInfoForEachOlympics();

                foreach (var medalInfo in medalInfoList)
                {
                    Info.Add(medalInfo);
                }
            }
        }

        public List<MedalTableRow> GetMedalInfoForEachOlympics()
        {
            using (NintendoContext db = new NintendoContext())
            {
                var medalInfoList = new List<MedalTableRow>();

                var olympics = db.Olympics.ToList();

                foreach (var olympic in olympics)
                {
                    var olympicsMedalInfo = new MedalTableRow
                    {
                        Country = olympic.Country,
                        City = olympic.City
                    };

                    var sportsInOlympic = db.Sports.Where(s => s.OlympicId == olympic.Id).ToList();

                    foreach (var sport in sportsInOlympic)
                    {
                        var results = db.Results.Include(r => r.Members).Where(r => r.SportId == sport.Id).ToList();

                        var goldCount = results.Count(r => r.Medal == EnuMedal.Gold);
                        var silverCount = results.Count(r => r.Medal == EnuMedal.Silver);
                        var bronzeCount = results.Count(r => r.Medal == EnuMedal.Bronze);

                        olympicsMedalInfo.GoldCount += goldCount;
                        olympicsMedalInfo.SilverCount += silverCount;
                        olympicsMedalInfo.BronzeCount += bronzeCount;
                    }

                    medalInfoList.Add(olympicsMedalInfo);
                }

                return medalInfoList;
            }
        }

        private void FillOlympicsComboBox()
        {
            using (NintendoContext db = new NintendoContext())
            {
                if (OlympicsComboBox.IsEnabled != true && InfoPage2.IsSelected || InfoPage3.IsSelected)
                {
                    OlympicsComboBox.IsEnabled = true;
                    List<Olympics> olympicsList = db.Olympics.ToList();
                    OlympicsComboBox.ItemsSource = olympicsList;
                    OlympicsComboBox.DisplayMemberPath = "Year";
                }
                else if (OlympicsComboBox.IsEnabled != true && InfoPage4.IsSelected)
                {
                    OlympicsComboBox.IsEnabled = true;
                    List<Sports> sportsList = db.Sports.ToList();
                    OlympicsComboBox.ItemsSource = sportsList;
                    OlympicsComboBox.DisplayMemberPath = "Name";
                }
                else if (OlympicsComboBox.IsEnabled != true && InfoPage6.IsSelected)
                {
                    OlympicsComboBox.IsEnabled = true;
                    var members = db.Members
                        .Select(m => new
                        {
                            Country = m.Country
                        })
                        .Distinct()
                        .ToList();

                    OlympicsComboBox.ItemsSource = members;
                    OlympicsComboBox.DisplayMemberPath = "Country";

                }
                else if (OlympicsComboBox.IsEnabled == true)
                {
                    OlympicsComboBox.IsEnabled = false;
                    OlympicsComboBox.ItemsSource = null;
                    Info2View.ClearValue(ItemsControl.ItemsSourceProperty);
                }
            }
        }

        private void OlympicsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InfoPage2.IsSelected)
            {
                if (OlympicsComboBox.SelectedItem != null)
                {
                    using (NintendoContext db = new NintendoContext())
                    {
                        Olympics selectedOlympics = (Olympics)OlympicsComboBox.SelectedItem;
                        int selectedOlympicsYear = selectedOlympics.Year;

                        var medalists = db.Results
                            .Join(db.Sports, result => result.SportId, sport => sport.Id, (result, sport) => new { result, sport })
                            .Join(db.Members, combined => combined.result.MemberId, member => member.Id, (combined, member) => new { combined, member })
                            .Join(db.Olympics, combined2 => combined2.combined.sport.OlympicId, olympics => olympics.Id, (combined2, olympics) => new { combined2, olympics })
                            .Where(joined => joined.olympics.Year == selectedOlympicsYear)
                            .Select(joined => new
                            {
                                MemberName = joined.combined2.member.FullName,
                                Sports = joined.combined2.combined.sport.Name
                            })
                            .ToList();

                        Info2View.ItemsSource = medalists;
                    }
                }
            }
            else if (InfoPage3.IsSelected)
            {
                if (OlympicsComboBox.SelectedItem != null)
                {
                    using (NintendoContext db = new NintendoContext())
                    {
                        Olympics selectedOlympics = (Olympics)OlympicsComboBox.SelectedItem;
                        int selectedOlympicsYear = selectedOlympics.Year;
                    
                        var goldMedalsByCountry = db.Results
                            .Join(db.Sports, result => result.SportId, sport => sport.Id, (result, sport) => new { result, sport })
                            .Join(db.Olympics, combined => combined.sport.OlympicId, olympics => olympics.Id, (combined, olympics) => new { combined, olympics })
                            .Where(joined => joined.olympics.Year == selectedOlympicsYear && joined.combined.result.Medal == EnuMedal.Gold)
                            .GroupBy(joined => joined.olympics.Country)
                            .Select(group => new
                            {
                                Country = group.Key,
                                GoldMedal = group.Count()
                            })
                            .OrderByDescending(g => g.GoldMedal)
                            .FirstOrDefault();

                        MessageBox.Show($"Країна: {goldMedalsByCountry.Country}\nКількість золотих медалей: {goldMedalsByCountry.GoldMedal}");
                    
                    }

                }
            }
            else if (InfoPage4.IsSelected)
            {
                if (OlympicsComboBox.SelectedItem != null)
                {
                    using (NintendoContext db = new NintendoContext())
                    {
                        Sports selectedSport = (Sports)OlympicsComboBox.SelectedItem;
                        int selectedSportId = selectedSport.Id;

                        var topGoldMedalist = db.Results
                            .Where(r => r.SportId == selectedSportId && r.Medal == EnuMedal.Gold)
                            .GroupBy(r => r.MemberId)
                            .Select(g => new
                            {
                                MemberId = g.Key,
                                GoldMedalsCount = g.Count()
                            })
                            .OrderByDescending(g => g.GoldMedalsCount)
                            .FirstOrDefault();

                        if (topGoldMedalist != null)
                        {
                            var member = db.Members.FirstOrDefault(m => m.Id == topGoldMedalist.MemberId);
                            MessageBox.Show($"Найкращий золотий медаліст в {selectedSport.Name}:\nІм'я: {member.FullName}\nЗолоті медалі: {topGoldMedalist.GoldMedalsCount}");
                        }
                        else
                        {
                            MessageBox.Show("No top gold medalist found for the selected sport.");
                        }
                    }
                }
            }
            else if (InfoPage6.IsSelected)
            {
                if (OlympicsComboBox.SelectedItem != null)
                {
                    using (NintendoContext db = new NintendoContext())
                    {
                        var selectedMember = (dynamic)OlympicsComboBox.SelectedItem;
                        string selectedMemberCountry = selectedMember.Country;

                        var members = db.Members
                            .Where(m => m.Country == selectedMemberCountry)
                            .Select(m => new
                            {
                                Name = m.FullName
                            })
                            .ToList();

                        Info6View.ItemsSource = members;
                    }
                }
            }
        }

        private void Button_Click2_Click(object sender, RoutedEventArgs e)
        {
            if (InfoPage2.IsSelected)
            {
                FillOlympicsComboBox();
            }
            else
            {
                MessageBox.Show("Не суй сюди руки");
            }
        }

        private void Button_Click3_Click(object sender, RoutedEventArgs e)
        {
            if (InfoPage3.IsSelected)
            {
                FillOlympicsComboBox();
            }
            else
            {
                MessageBox.Show("Не суй сюди руки");
            }
        }

        private void Button_Click4_Click(object sender, RoutedEventArgs e)
        {
            if (InfoPage4.IsSelected)
            {
                FillOlympicsComboBox();
            }
            else
            {
                MessageBox.Show("Не суй сюди руки");
            }
        }

        private void Button_Click5_Click(object sender, RoutedEventArgs e)
        {
            using (NintendoContext db = new NintendoContext())
            {
                var mostFrequentHostCountry = db.Olympics
                    .GroupBy(o => o.Country)
                    .Select(g => new
                    {
                        Country = g.Key,
                        OlympicsCount = g.Count()
                    })
                    .OrderByDescending(g => g.OlympicsCount)
                    .FirstOrDefault();

                if (mostFrequentHostCountry != null)
                {
                    MessageBox.Show($"Most frequent host country: {mostFrequentHostCountry.Country}");
                }
                else
                {
                    MessageBox.Show("No host country found.");
                }
            }
        }

        private void Button_Click6_Click(object sender, RoutedEventArgs e)
        {
            if (InfoPage6.IsSelected)
            {
                FillOlympicsComboBox();
            }
            else
            {
                MessageBox.Show("Не суй сюди руки");
            }
        }

        private void Button_Click7_Click(object sender, RoutedEventArgs e)
        {
            using (NintendoContext db = new NintendoContext())
            {
                Info.Clear();

                var medalInfoList = GetMedalInfoForEachOlympics();

                foreach (var medalInfo in medalInfoList)
                {
                    Info.Add(medalInfo);
                }
            }
        }
    }
}
