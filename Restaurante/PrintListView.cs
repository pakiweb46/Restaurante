using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Restaurante
{
    class PrintListView: System.Drawing.Printing.PrintDocument
    {
        public int noOfItems;
        #region  Property Variables
        /// <summary>
        /// Property variable for the Font the user wishes to use
        /// </summary>
        /// <remarks></remarks>
        private Font TitleHead_font;
        private Font PrintDate_font;
        private Font Title_font;
        private Font Date_font;
        private Font Text_font;
        private Font Text_font_bold;
        private string summe_text;
        private string[] LVHeader;
        private ListView derList;
       // private int sno=0;
        #endregion
        #region  Constructor
        public PrintListView(ListView inCom):base()
        {
            derList = inCom;
            //sno = 0;
            j = 0;
            noOfItems = derList.Items.Count;
          
        }
        #endregion
        private string TitleHead_text = "Unbekannt";
        private string PrintDate_text = "Unbekannt";
        private string Title_text = "Unbekannt";
        private string Date_text = "Unbekannt";
        public string TitleHead_Text
        {
            get { return TitleHead_text; }
            set { TitleHead_text = value; }
        }
        public string summe_Text
        {
            get { return summe_text; }
            set { summe_text = value; }
        }

        public string Title_Text
        {
            get { return Title_text; }
            set { Title_text = value; }
        }
        public string PrintDatetext
        {
            get { return PrintDate_text; }
            set { PrintDate_text = value; }
        }
        public string Date_Text
        {
            get { return Date_text; }
            set { Date_text = value; }
        }
        public Font TitleFont
        {
            // Allows the user to override the default font
            get { return Title_font; }
            set { Title_font = value; }
        }



        public Font TitleHeadFont
        {
            // Allows the user to override the default font
            get { return TitleHead_font; }
            set { TitleHead_font = value; }
        }
        public Font PrintDateFont
        {
            // Allows the user to override the default font
            get { return PrintDate_font; }
            set { PrintDate_font = value; }
        }
        public Font DateFont
        {
            // Allows the user to override the default font
            get { return Date_font; }
            set { Date_font = value; }
        }
        public Font TextFont
        {
            // Allows the user to override the default font
            get { return Text_font; }
            set { Text_font = value; }
        }
        #region  onbeginPrint
        /// <summary>
        /// Override the default onbeginPrint method of the PrintDocument Object
        /// </summary>
        /// <param name=e></param>
        /// <remarks></remarks>
        protected override void OnBeginPrint(System.Drawing.Printing.PrintEventArgs e)
        {
            // Run base code
            base.OnBeginPrint(e);

            //Check to see if the user provided a font
            //if they didn't then we default to Times New Roman
            if (TitleHead_font == null)
            {
                //Create the font we need
                TitleHead_font = new Font("New Times Roman", 18, FontStyle.Bold);
            }
            if (Title_font == null)
            {
                Title_font = new Font("ARIAL", 16, FontStyle.Bold);
            }
            if (Date_font == null)
            {
                Date_font = new Font("ARIAL", 12, FontStyle.Bold);

            }
            if (PrintDate_font == null)
            {
                PrintDate_font = new Font("Times New Roman", 11, FontStyle.Regular);
            }
            if (Text_font == null)
            {
                Text_font = new Font("Times New Roman", 12, FontStyle.Regular);
            }
            if (Text_font_bold == null)
            {
                Text_font_bold = new Font("Times New Roman", 12, FontStyle.Bold);
            }
             LVHeader = getHeader();
            j = 0;
        }
        #endregion
        private int pageNo = 0;
        private int currentPage = 0;
        bool multiplepages = false;
        #region PrintListView Functions
        private string[] getHeader()
        {
            
            var names = (from col in derList.Columns.Cast<ColumnHeader>()
                         orderby col.DisplayIndex
                         select col.Text).ToList();
            names.Remove("ID");
            names.Remove("Datum");
            names.Remove("Fahrer");
            string[] ret = names.ToArray();
            return ret;
        }
        #endregion
        #region  OnPrintPage
        /// <summary>
        private int j;
        /// Override the default OnPrintPage method of the PrintDocument
        /// </summary>
        /// <param name=e></param>
        /// <remarks>This provides the print logic for our document</remarks>
        protected override void OnPrintPage(System.Drawing.Printing.PrintPageEventArgs e)
        {
            //---- Height 800 bis kunden daten ca.250 plus 100 abschluss 20 pro no of artikles
            // 800-230 =570 zur verfügung560/20= 28 artikel machen wir 25 
            // Run base code
            base.OnPrintPage(e);
            Graphics g = e.Graphics;


            PageSettings p = e.PageSettings;
            StringFormat left = new StringFormat();
            StringFormat right = new StringFormat();
            StringFormat center = new StringFormat();
            right.Alignment = StringAlignment.Far;
            left.Alignment = StringAlignment.Near;
            center.Alignment = StringAlignment.Center;
            int printHeight;
            int printWidth;
            int remainingHeight;
            int leftMargin;
            int rightMargin;
            int nextline;// holds the vertikel curser
            //int position; // holds the horizental curser
            //double stringwidth;// calculate the width of the string in pixel
            Point[] xy = new Point[2]; // array of points to hold different points for line drawing

            //Set print area size and margins
            {
                printHeight = Convert.ToInt32(p.PrintableArea.Height);//base.DefaultPageSettings.PaperSize.Height - base.DefaultPageSettings.Margins.Top - base.DefaultPageSettings.Margins.Bottom;
                printWidth = Convert.ToInt32(p.PrintableArea.Width);//base.DefaultPageSettings.PaperSize.Width - base.DefaultPageSettings.Margins.Left - base.DefaultPageSettings.Margins.Right;
                leftMargin = Convert.ToInt32(p.PrintableArea.Left);//base.DefaultPageSettings.Margins.Left;  //X
                rightMargin = Convert.ToInt32(p.PrintableArea.Right);// base.DefaultPageSettings.Margins.Top;  //Y
            }
            remainingHeight = printHeight;
            if (base.DefaultPageSettings.Landscape)
            {
                int tmp;
                tmp = printHeight;
                printHeight = printWidth;
                printWidth = tmp;
            }
  
            SizeF TitleHead_text_ln = g.MeasureString(TitleHead_text, TitleHead_font);
            SizeF Title_text_ln = g.MeasureString(Title_text, Title_font);
            SizeF PrintDate_text_ln = g.MeasureString(PrintDate_text, PrintDate_font);
            
            nextline = Convert.ToInt32(p.PrintableArea.Top); // TOP 
            // Draws the Title_font Top Left
            g.DrawString(TitleHead_text, TitleHead_font, Brushes.Black, ((rightMargin / 2) - (Title_text_ln.Width / 2)), nextline);
            // goto next line
            nextline += TitleHead_font.Height;
            g.DrawString(Title_text, Title_font, Brushes.Black, ((rightMargin / 2) - (Title_text_ln.Width / 2)), nextline);
            // goto next line
            nextline += Title_font.Height;
            g.DrawString(PrintDate_text, PrintDate_font, Brushes.Black, ((rightMargin / 2) - (PrintDate_text_ln.Width / 2)), nextline);
            g.DrawString("Seite " + (pageNo + 1), PrintDate_font, Brushes.Black, (rightMargin - 100), nextline);
            // goto next line
            nextline += (PrintDate_font.Height + 3);
            xy[0] = new Point(0, nextline);
            xy[1] = new Point(printWidth, nextline);

            g.DrawLine(new Pen(Color.Black), xy[0], xy[1]);
            nextline += 3;
            float pad_nr = 0;
            
            SizeF[] sz=new SizeF[LVHeader.Length];
            for (int i=0;i<LVHeader.Length;i++)
            sz[i] = g.MeasureString(LVHeader[i].Trim(), Text_font);
            //calculate padding
            float freespace=rightMargin;
            for (int i=0;i<LVHeader.Length;i++)
                freespace-=sz[i].Width;
            float div_factor = rightMargin / 40;
            //leftMargin += Convert.ToInt16(div_factor);
            pad_nr = freespace / (div_factor + (div_factor / 4));
            
                g.DrawString(LVHeader[0], Text_font, Brushes.Black, leftMargin , nextline, left);
                   g.DrawString(LVHeader[1], Text_font, Brushes.Black, leftMargin + sz[0].Width + pad_nr , nextline, left);
                   g.DrawString(LVHeader[2], Text_font, Brushes.Black, leftMargin +sz[0].Width+ sz[1].Width + pad_nr*5, nextline, left);
                   g.DrawString(LVHeader[3], Text_font, Brushes.Black, leftMargin + sz[0].Width + sz[1].Width+sz[2].Width + pad_nr * 6+pad_nr*6, nextline, left);
                   g.DrawString(LVHeader[4], Text_font, Brushes.Black, leftMargin + sz[0].Width + sz[1].Width + sz[2].Width+sz[3].Width + pad_nr * 14, nextline, left);
                 //  g.DrawString(LVHeader[5], Text_font, Brushes.Black, leftMargin + sz[0].Width + sz[1].Width + sz[2].Width+sz[3].Width+sz[4].Width + pad_nr * 5, nextline, left);
            
  
            nextline += Text_font.Height;
            xy[0] = new Point(0, nextline);
            xy[1] = new Point(p.PaperSize.Width, nextline);
            g.DrawLine(new Pen(Color.Black), xy[0], xy[1]);
            nextline += 3;
            remainingHeight -= nextline;
            // load startdate umsatz in umsatz variables
          //  loadUmsatz(startTag, startMonat, startJahr);
            noOfItems = derList.Items.Count;
            
            while (pageNo == currentPage & noOfItems>0)
            {

                try
                {
                    g.DrawString(derList.Items[j].SubItems[7].Text, Text_font, Brushes.Black, leftMargin, nextline, left);
                    g.DrawString(derList.Items[j].SubItems[1].Text, Text_font, Brushes.Black, leftMargin + sz[0].Width + pad_nr, nextline, left);
                    if (derList.Items[j].SubItems[2].Text.Length > 36)
                        g.DrawString(derList.Items[j].SubItems[2].Text.Substring(0, 36) + "..", Text_font, Brushes.Black, leftMargin + sz[1].Width + pad_nr * 7, nextline, left);
                    else
                        g.DrawString(derList.Items[j].SubItems[2].Text, Text_font, Brushes.Black, leftMargin + sz[1].Width + pad_nr * 7, nextline, left);
                    g.DrawString(derList.Items[j].SubItems[4].Text, Text_font, Brushes.Black, leftMargin + sz[0].Width + sz[1].Width + sz[2].Width + sz[3].Width + pad_nr * 11, nextline, center);
                    g.DrawString(derList.Items[j].SubItems[5].Text, Text_font, Brushes.Black, leftMargin + sz[0].Width + sz[1].Width + sz[2].Width + pad_nr * 12 + sz[3].Width + pad_nr * 2, nextline, left);

                    nextline += Text_font.Height;
                    noOfItems--;
                    j++;
                    if (nextline >= e.PageBounds.Height - 50)
                    {
                        pageNo++;

                    }
                }
                // workingDate = workingDate.AddDays(1);

                catch //handle index overflow exception
                {
                    noOfItems--;// do nothing
                }
               


            }
            if ((pageNo == currentPage))
            {
                // draw two lines
                xy[0] = new Point(0, nextline);
                xy[1] = new Point(Convert.ToInt32(p.PaperSize.Width), nextline);
                g.DrawLines(new Pen(Color.Black), xy);
                nextline = nextline + 5;
                xy[0] = new Point(0, nextline);
                xy[1] = new Point(Convert.ToInt32(p.PaperSize.Width), nextline);
                e.Graphics.DrawLines(new Pen(Color.Black), xy);
                nextline = nextline + 5;
                g.DrawString("    ", Text_font, Brushes.Black, leftMargin, nextline, center);
                g.DrawString("Gesamt:     ", Text_font, Brushes.Black, leftMargin + sz[0].Width + pad_nr*32, nextline, center);
                g.DrawString(summe_text, Text_font, Brushes.Black, leftMargin + sz[0].Width + sz[1].Width + pad_nr *32, nextline, center);
                               nextline += Text_font.Height;

                // draw two lines
                xy[0] = new Point(0, nextline);
                xy[1] = new Point(Convert.ToInt32(p.PaperSize.Width), nextline);
                g.DrawLines(new Pen(Color.Black), xy);
                nextline = nextline + 5;
                xy[0] = new Point(0, nextline);
                xy[1] = new Point(Convert.ToInt32(p.PaperSize.Width), nextline);
                e.Graphics.DrawLines(new Pen(Color.Black), xy);
                nextline = nextline + 5;

                //    string satz = "                   " + totalSumme.ToString() + "     " + totalMwst7 + "    " + totalMwst19 + "             " + (totalMwst7 + totalMwst19).ToString() + "  ";

                //    e.Graphics.DrawString(satz, Text_font, Brushes.Black, leftMargin, nextline);

            }
            if (pageNo > currentPage || multiplepages)
            {
                e.HasMorePages = true;
                currentPage++;
            }
            else
            {
                e.HasMorePages = false;
                 //     multiplepages = false;
            }
            
        }

        #endregion
        #region  RemoveZeros
        /// <summary>
        /// Function to replace any zeros in the size to a 1
        /// Zero's will mess up the printing area
        /// </summary>
        /// <param name=value>Value to check</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int RemoveZeros(int value)
        {
            //Check the value passed into the function,
            //if the value is a 0 (zero) then return a 1,
            //otherwise return the value passed in
            switch (value)
            {
                case 0:
                    return 1;
                default:
                    return value;
            }
        }
        #endregion
        #region  PrintDocument
        public void PrintDocument()
        {
            //Create an instance of our printer class
            //   RecieptPrint printer = new RecieptPrint();
            //Issue print command
            this.Print();
            //printer.Print();
        }
        #endregion

        
    }
}
