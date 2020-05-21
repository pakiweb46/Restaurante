using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Restaurante
{
    class Zutat
    {
        int menge;
        string beschreibung;
        double einzelpreis;

        int mwstKlasse;

        public Zutat(int menge, string beschreibung, double einzelpreis, int mwstKlasse)
        {
            this.menge = menge;
            this.beschreibung = beschreibung;
            this.einzelpreis = einzelpreis;
            this.mwstKlasse = mwstKlasse;
        }

    }
    class Artikel
    {
        int menge;
        string beschreibung;
        double einzelpreis;
        double summe;
        int mwstKlasse;
        Zutat[] zutaten ;

        
        public Artikel(int menge, string beschreibung, double einzelpreis, double summe,int mwstKlasse)
        {
            this.menge= menge;
            this.beschreibung = beschreibung;
            this.einzelpreis = einzelpreis;
            this.summe = summe;
            this.mwstKlasse = mwstKlasse;
            
        }
        public Artikel(int menge, string beschreibung, double einzelpreis, double summe, int mwstKlasse, Zutat[] zutaten)
        {
            this.menge = menge;
            this.beschreibung = beschreibung;
            this.einzelpreis = einzelpreis;
            this.summe = summe;
            this.mwstKlasse = mwstKlasse;
            this.zutaten = zutaten;
        }

        bool AddZutat(int menge, string beschreibung, double einzelpreis, int mwstKlasse)
        {
            try
            {
                zutaten.Append(new Zutat(
                    menge,
                    beschreibung,
                    einzelpreis,
                    mwstKlasse
                    ));
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }


    }
  
    class Amounts
    {

        public int RechnungNr { get; set; }
        public int KundenNr { get; set; }
        private string date;
        private string time;
        private double total;
        private double mwst1;
        private double mwst2;
        private double Anfahrtkosten;
        Hashtable positionsNetto;
        Hashtable positionsBrutto;
        Hashtable positionsMwst;
        Hashtable positionsMwstKlasse;
        Hashtable positionsArtikel;
        private int totalPositions;

       
        #region Object generation

        public Amounts()
        {
            total = 0;
            mwst1 = 0;
            mwst2 = 0;
            Anfahrtkosten = 0;
            totalPositions = 0;
            positionsNetto = new Hashtable();
            positionsBrutto = new Hashtable();
            positionsMwst = new Hashtable();
            positionsMwstKlasse = new Hashtable();
            positionsArtikel = new Hashtable();
            date = System.DateTime.Now.ToShortDateString();
            time = System.DateTime.Now.ToShortTimeString();
        }
        public Amounts(string date, string time, double Anfahrtkosten)
        {
            this.date = date;
            this.time = time;
            this.Anfahrtkosten = Anfahrtkosten;
            // Add 19% Mwst
            double NettoPrice = Anfahrtkosten / 1.19;
            mwst2 += NettoPrice * 0.19;
            total = Anfahrtkosten;
            mwst1 = 0;
            positionsNetto = new Hashtable();
            positionsBrutto = new Hashtable();
            positionsMwst = new Hashtable();
            positionsMwstKlasse = new Hashtable(); 
            positionsArtikel = new Hashtable(); // Artikel nummer des Position
            totalPositions = 0;
        }
        public Amounts(string date, string time)
        {
            this.date = date;
            this.time = time;
            total = 0;
            mwst1 = 0;
            mwst2 = 0;
            Anfahrtkosten = 0;
            positionsNetto = new Hashtable();
            positionsBrutto = new Hashtable();
            positionsMwst = new Hashtable();
            positionsMwstKlasse = new Hashtable();
            positionsArtikel = new Hashtable();
            totalPositions = 0;
        }
        public Amounts(string date, string time, int KundenNr)
        {
            this.date = date;
            this.time = time;
            total = 0;
            mwst1 = 0;
            mwst2 = 0;
            Anfahrtkosten = 0;
            positionsNetto = new Hashtable();
            positionsBrutto = new Hashtable();
            positionsMwst = new Hashtable();
            positionsMwstKlasse = new Hashtable();
            positionsArtikel = new Hashtable();
            totalPositions = 0;
            this.KundenNr = KundenNr;
        }
        public Amounts(string date,string time, double Anfahrtkosten, double position, int mwstKlasse, int Artikel)
        {
            date = System.DateTime.Now.ToShortDateString();
            time = System.DateTime.Now.ToShortTimeString();
            this.Anfahrtkosten = Anfahrtkosten;
            // Add 19% Mwst
            double NettoAnfahrt = Anfahrtkosten / 1.19;
            mwst2 += NettoAnfahrt * 0.19;
            total = Anfahrtkosten;
            positionsNetto = new Hashtable();
            positionsBrutto = new Hashtable();
            positionsMwst = new Hashtable();
            positionsMwstKlasse = new Hashtable();
            positionsArtikel = new Hashtable();
            totalPositions = 1;
            double Netto;
            double steuer;
            switch (mwstKlasse)
            {
                case 1:
                    Netto = position / 1.07;
                    steuer = Netto * 0.07;
                    mwst1 += steuer;
                    total += position;
                    positionsNetto.Add(totalPositions, Netto);
                    positionsBrutto.Add(totalPositions, position);
                    positionsMwst.Add(totalPositions, steuer);
                    positionsMwstKlasse.Add(totalPositions, 1);
                    positionsArtikel.Add(totalPositions, Artikel);
                    break;
                case 2:
                    Netto = position / 1.19;
                    steuer = Netto * 0.19;
                    mwst2 += steuer;
                    total += position;
                    positionsNetto.Add(totalPositions, Netto);
                    positionsBrutto.Add(totalPositions, position);
                    positionsMwst.Add(totalPositions, steuer);
                    positionsMwstKlasse.Add(totalPositions, 2);
                    positionsArtikel.Add(totalPositions, Artikel);
                    break;
                default:
                    Netto = position / 1.07;
                    steuer = Netto * 0.07;
                    mwst1 += steuer;
                    total += position;
                    positionsNetto.Add(totalPositions, Netto);
                    positionsBrutto.Add(totalPositions, position);
                    positionsMwst.Add(totalPositions, steuer);
                    positionsMwstKlasse.Add(totalPositions, 1);
                    positionsArtikel.Add(totalPositions, Artikel);
                    break;
            }

        }
        #endregion

        public bool AddAnfahrtKosten(double kosten)
        {
            Anfahrtkosten += kosten;
            double NettoPrice = kosten / 1.19;
            mwst2 += NettoPrice * 0.19;
            return true;
        }

        private bool addToMwst(double amount, int mwstKlasse)
        {
            if (mwstKlasse == 1)
            {
                mwst1 += amount;
                return true;
            }
            if (mwstKlasse == 2)
            {
                mwst2 += amount;
                return true;
            }
            return false;
        }

        public bool addPosition(double position, int mwstKlasse, int Artikel)
        {
            totalPositions++;
            double Netto;
            double steuer;
            switch (mwstKlasse)
            {
                case 1:
                    Netto = position / 1.07;
                    steuer = Netto * 0.07;
                    mwst1 += steuer;
                    total += position;
                    positionsNetto.Add(totalPositions, Netto);
                    positionsBrutto.Add(totalPositions, position);
                    positionsMwst.Add(totalPositions, steuer);
                    positionsMwstKlasse.Add(totalPositions, 1);
                    positionsArtikel.Add(totalPositions, Artikel);
                    break;
                case 2:
                    Netto = position / 1.19;
                    steuer = Netto * 0.19;
                    mwst2 += steuer;
                    total += position;
                    positionsNetto.Add(totalPositions, Netto);
                    positionsBrutto.Add(totalPositions, position);
                    positionsMwst.Add(totalPositions, steuer);
                    positionsMwstKlasse.Add(totalPositions, 2);
                    positionsArtikel.Add(totalPositions, Artikel);
                    break;
                default:
                    Netto = position / 1.07;
                    steuer = Netto * 0.07;
                    mwst1 += steuer;
                    total += position;
                    positionsNetto.Add(totalPositions, Netto);
                    positionsBrutto.Add(totalPositions, position);
                    positionsMwst.Add(totalPositions, steuer);
                    positionsMwstKlasse.Add(totalPositions, 1);
                    positionsArtikel.Add(totalPositions, Artikel);
                    break;
            }
            return true;
        }

       
        
    }
}
