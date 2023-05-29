using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroGUI
{
    internal class Music
    {
        private int ev;
        private string eloado;
        private string cim;
        private int helyezes;
        private int pontszam;

        public Music(int ev, string eloado, string cim, int helyezes, int pontszam) 
        {
            this.ev = ev;
            this.eloado = eloado;
            this.cim = cim;
            this.helyezes = helyezes;
            this.pontszam = pontszam;

        }
        public Music(string eloado, string cim)
        {
            this.eloado = eloado;
            this.cim= cim;
        }

        public static string FormattedString(List<Music> dataList)
        {
            List<string> formattedSongs = new();

            foreach (var song in dataList)
            {
                formattedSongs.Add($"{song.eloado} - {song.cim}");
            }


            string result = string.Join(", ", formattedSongs);

            return result;
        }

        public int Ev { get => ev; set => ev = value; }
        public string Eloado { get => eloado; set => eloado = value; }
        public string Cim { get => cim; set => cim = value; }
        public int Helyezes { get => helyezes; set => helyezes = value; }
        public int Pontszam { get => pontszam; set => pontszam = value; }
    }
    
}
