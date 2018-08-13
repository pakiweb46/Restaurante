using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LieferDienst;

namespace Restaurante
{
    class Zutaten
    {
        private double gesamtPrice;
        public double gesamtMwst;
        public string[] ZutatName;
        public double[] ZutatPrice;
        private int index;
        public Zutaten()
        {
            gesamtPrice = 0;
            ZutatName = new string[25];
            ZutatPrice = new double[25];
            index = 0;
       
        }
        public void addZutat(string Name, double price,double mwst)
        {
            ZutatName[index] = Name;
            ZutatPrice[index] = price;
            gesamtPrice +=price;
            gesamtMwst += mwst;
            index++;
        }
        public void removeZutat(string Name, double price, double mwst)
        {
            ZutatName[index] = Name;
            ZutatPrice[index] = price;
            gesamtPrice -= price;
            gesamtMwst -= mwst;
            index++;
        }
        public double getValueadded()
        {
            if (gesamtPrice > 0)

                return gesamtPrice;
            else
                return 0;
        }
        public double getMwst()
        {
            if (gesamtPrice > 0)

                return gesamtMwst;
            else
                return 0;
        }
       
    }
}
