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
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using Org.BouncyCastle.Utilities;

namespace EuroGUI
{

    public partial class MainWindow : Window
    {
        ObservableCollection<Music> MusicDataList = new();
        private readonly string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=qweasd;database=eurovizio";
        MySqlConnection connection;
        public MainWindow()
        {
            InitializeComponent();
            ReadDatas();
        }

        private void ReadDatas()
        {
            string getDatas = "SELECT ev, eloado, cim, helyezes, pontszam FROM dal";
            try
            {
                using (MySqlConnection connection = new(connectionString))
                {
                    connection.Open();
                    MySqlCommand getDatasCommand = new(getDatas, connection);
                    MySqlDataReader reader = getDatasCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        try
                        {
                            Music newMusicDatas = new(reader.GetInt32("ev"),
                                                           reader.GetString("eloado"),
                                                           reader.GetString("cim"),
                                                           reader.GetInt32("helyezes"),
                                                           reader.GetInt32("pontszam"));
                            MusicDataList.Add(newMusicDatas);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                            throw;
                        }
                    }
                    reader.Close();
                    dgMusicDatas.ItemsSource = MusicDataList;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                connection.Close();
            }
        }

        private void FourthTask(object sender, RoutedEventArgs e)
        {
            string getDatas = "SELECT COUNT(eloado), MIN(helyezes) FROM dal WHERE dal.orszag = \"Magyarország\"";
            try
            {
                using (MySqlConnection connection = new(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new(getDatas, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        int HungarianMusicians = reader.GetInt32(0);
                        int Placement = reader.GetInt32(1);

                        MessageBox.Show($"Magyar előadók: {HungarianMusicians} \t Legjobb helyezés: {Placement}");
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void FifthTask(object sender, RoutedEventArgs e)
        {
            string getDatas = "SELECT AVG(pontszam) from dal where orszag = \"Németország\"";

            try
            {
                using (MySqlConnection connection = new(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new(getDatas, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        double AvgPoints = reader.GetDouble(0);

                        MessageBox.Show($"Németország által átlagosan elért pontok száma: {Math.Round(AvgPoints, 2)}");
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }


        }

        private void SixthTask(object sender, RoutedEventArgs e)
        {
            string getDatas = "SELECT eloado, cim from dal where cim LIKE \"%Luck%\"";
            List<Music> MusicList = new();
            try
            {
                using (MySqlConnection connection = new(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new(getDatas, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Music newMusic = new(reader.GetString(0), reader.GetString(1));
                        MusicList.Add(newMusic);
                    }
                    reader.Close();
                }
                MessageBox.Show(Music.FormattedString(MusicList));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();

            }

        }

        private void SeventhTask(object sender, RoutedEventArgs e)
        {
            string getDatas = $"SELECT cim from dal where eloado LIKE '%{txtSearch.Text}%'";
            string searchText = txtSearch.Text;
            lbResultBox.Items.Clear();

            var filteredSongs = MusicDataList.Where(song => song.Eloado.Contains(searchText))
                                     .OrderByDescending(song => song.Eloado)
                                     .ThenByDescending(song => song.Cim);

            foreach (var song in filteredSongs)
            {
                lbResultBox.Items.Add(song.Cim);
            }
        }

        private void EighthTask(object sender, RoutedEventArgs e)
        {
            string competitionDate;
            string getDatas;

            if (dgMusicDatas.SelectedIndex == -1)
            {
                getDatas = $"select datum from verseny INNER JOIN dal on verseny.ev = dal.ev where eloado = '{MusicDataList[0].Eloado}'";
            }
            else
            {
                getDatas = $"select datum from verseny INNER JOIN dal on verseny.ev = dal.ev where eloado = '{MusicDataList[dgMusicDatas.SelectedIndex].Eloado}'";

            }

            try
            {
                using (MySqlConnection connection = new(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new(getDatas, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        competitionDate = reader.GetString(0);
                        lblDate.Content = $"Verseny dátuma: {competitionDate[..12]}";
                    }

                    reader.Close();
                }
            }
            catch (Exception)
            {
                connection.Close();
            }
        }
    }
}