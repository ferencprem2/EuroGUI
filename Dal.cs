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

        public int Ev { get; set; }
        public string Eloado { get; set; }
        public string Cim { get; set; }
        public int Helyezes { get; set; }
        public int Pontszam { get; set; }
    }
}
