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

namespace NbaStatistics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<SingleRecord> Teams;
        public MainWindow()
        {
            InitializeComponent();
            Teams = new List<SingleRecord>();
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Dominik\Desktop\Mecze_NBA.csv");
            for(int i = 1; i < lines.Length;i++)
            {
                Teams.Add(new SingleRecord(lines[i]));
                WinerCombobox.Items.Add(new ComboBoxItem() { Content = Teams[Teams.Count - 1].TeamName });
                LoserCombobox.Items.Add(new ComboBoxItem() { Content = Teams[Teams.Count - 1].TeamName });
            }
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            if (WinerCombobox.SelectedItem != null && LoserCombobox.SelectedItem != null)
            {
                SingleRecord winner = Teams.Find(t => t.TeamName.Equals(((ComboBoxItem)WinerCombobox.SelectedItem).Content));
                SingleRecord loser = Teams.Find(t => t.TeamName.Equals(((ComboBoxItem)LoserCombobox.SelectedItem).Content));
                winner.AddGame(true, (bool)WinnerIsBiddedCheckbox.IsChecked, (bool)FavouriteCheckbox.IsChecked);
                loser.AddGame(false, (bool)!WinnerIsBiddedCheckbox.IsChecked, (bool)FavouriteCheckbox.IsChecked);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Dominik\Desktop\Mecze_NBA.csv"))
                {
                    file.WriteLine("Druzyna,Wygrane,Przegrane,Obstawione Wygrane,Obstawione Przegrane,Pewniaki Wygrane,Pewniaki Przegrane,Procent Pewniakow,Procent Obstawionych");
                    for (int i = 0; i < Teams.Count; i++)
                    {
                        file.WriteLine(Teams[i].Serialize());
                    }
                }
                MessageBox.Show(this, "mecz dodany");
            }
        }
    }
}
