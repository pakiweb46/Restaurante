using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using LieferDienst;
namespace Restaurante
{
    class Print_List:System.Drawing.Printing.PrintDocument
    {
        float font_factor = 0.0f;
     //   public int a5w, a5h;
        private PrintPreviewDialog m_previewDlg = new PrintPreviewDialog();
        private PrintDocument m_prnDoc = new PrintDocument();
       MySqlCommand cmd;
       static string connStr = Class1.connString;
        MySqlConnection conn = new MySqlConnection(connStr);
      MySqlDataReader rdr;
      private double Mwst7,Mwst19,summe,totalSumme,totalMwst7,totalMwst19;
     private int startTag,startMonat,startJahr,endTag,endMonat,endJahr,noOfDays,sno;
     private DateTime startDate, endDate,workingDate;

        #region  Property Variables
        /// <summary>
        /// Property variable for the Font the user wishes to use
        /// </summary>
        /// <remarks></remarks>
        private Font Title_font;
        private Font Title2_font;
        private Font Text_font;
        private Font Title3_font;
        /// <summary>
        /// Property variable for the text to be printed
        /// </summary>
        /// <remarks></remarks>
        private string Title_text;
        private string Title_text2;
        private string Title_text3;
        #endregion
        #region  Class Properties
        /// <summary>
        /// Property to hold the text that is to be printed
        /// </summary>
        /// <value></value>
        /// <returns>A string</returns>
        /// <remarks></remarks>
        public string Title_Text
        {
            get { return Title_text; }
            set { Title_text = value; }
        }
        public string Title_Text2
        {
            get { return Title_text2; }
            set { Title_text2 = value; }
        }
        public string Title_Text3
        {
            get { return Title_text3; }
            set { Title_text3 = value; }
        }
        public Font TitleFont
        {
            // Allows the user to override the default font
            get { return Title_font; }
            set { Title_font = value; }
        }
        public Font Title2Font
        {
            // Allows the user to override the default font
            get { return Title2_font; }
            set { Title2_font = value; }
        }
        public Font Title3Font
        {
            // Allows the user to override the default font
            get { return Title3_font; }
            set { Title3_font = value; }
        }
        public Font TextFont
        {
            // Allows the user to override the default font
            get { return Text_font; }
            set { Text_font = value; }
        }
        #endregion

        public Print_List ():base()
{
    //Set the file stream
    //Instantiate out Text property to an empty string
    Title_text = string.Empty;
    Title_text2 = string.Empty;
    Title_text3 = string.Empty;

}
        public Print_List (DateTime startDat,DateTime endDat):base()
{
    //Set the file stream
    //Instantiate out Text property to an empty string
    Title_text = string.Empty;
    Title_text2 = string.Empty;
    Title_text3 = string.Empty;
    startTag = startDate.Day;
    startMonat = startDate.Month;
    startJahr = startDate.Year;
    endJahr = endDate.Year;
    endMonat = endDate.Month;
    endTag = endDate.Day;
    this.startDate = startDat;
    this.endDate = endDat;
    this.workingDate = startDat;
    sno = 0;
    // count the no of days
    noOfDays = Convert.ToInt32((endDate - startDate).TotalDays);
    
    totalMwst19 = 0;
    totalSumme = 0;
    totalMwst7 = 0;
    if (base.DefaultPageSettings.PaperSize.PaperName != "A5")
    {

       
        PageSetupDialog ds = new PageSetupDialog();
        ds.Document = this;
        ds.ShowDialog();
                
    }
              

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
    if (this.PrinterSettings.DefaultPageSettings.PaperSize.PaperName.ToString() == "A4")
        font_factor = 1F;
    else if (this.PrinterSettings.DefaultPageSettings.PaperSize.PaperName.ToString() == "A5")
        font_factor = 0.5F;
    else
        font_factor = 0.5F;
    //Check to see if the user provided a font
    //if they didn't then we default to Times New Roman
    if (Title_font == null)
    {
        //Create the font we need
        Title_font = new Font("ARIAL", 20*font_factor,FontStyle.Bold);
    }
    if (Title2_font == null)
    {
        //Create the font we need
        Title2_font = new Font("ARIAL", 18*font_factor,FontStyle.Bold);
    }
    if (Title3_font == null)
    {
        //Create the font we need
        Title3_font = new Font("ARIAL", 16*font_factor, FontStyle.Bold);
    }
   
    if (Text_font == null)
    {
        //Create the font we need
        Text_font = new Font("ARIAL", 16*font_factor,FontStyle.Bold);
    }
    
}
#endregion
        private int pageNo = 0;
        private int currentPage = 0;
bool multiplepages = false;
     
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
    SizeF Title_text_ln = g.MeasureString(Title_text, Title_font);
    SizeF Title_text2_ln = g.MeasureString(Title_text2, Title2_font);
    SizeF Title_text3_ln = g.MeasureString(Title_text3, Title3_font);
    nextline = Convert.ToInt32(p.PrintableArea.Top); // TOP 
    // Draws the Title_font Top Left
    g.DrawString(Title_text, Title_font, Brushes.Black, ((rightMargin / 2)-(Title_text_ln.Width/2)), nextline);
    // goto next line
    nextline += Title_font.Height;
    g.DrawString(Title_text2, Title2_font, Brushes.Black, ((rightMargin / 2) - (Title_text2_ln.Width/2)), nextline);
    // goto next line
    nextline += Title2_font.Height;
    g.DrawString(Title_text3, Title3_font, Brushes.Black, ((rightMargin / 2) - (Title_text3_ln.Width/2)), nextline);
    g.DrawString("Seite "+(pageNo+1), Title3_font, Brushes.Black, (rightMargin-100  ), nextline);
   
    // goto next line
    nextline += (Title3_font.Height + 3);
    xy[0] = new Point(0, nextline);
    xy[1] = new Point(printWidth, nextline);
    
    g.DrawLine(new Pen(Color.Black), xy[0], xy[1]);
    nextline += 3;
    float pad_nr,pad_datum,pad_rest = 0;
    SizeF sz_Nr, sz_Datum, sz_TotalUmsatz, sz_Mwst7, sz_Mwst19, sz_TotalMwst;
    sz_Nr = g.MeasureString("Nr.", Text_font);
    sz_Datum = g.MeasureString("Datum", Text_font);
    sz_Mwst19 = g.MeasureString("Mwst 19%", Text_font);
    sz_Mwst7 = g.MeasureString("Mwst 7%", Text_font);
    sz_TotalMwst = g.MeasureString("Total Mwst", Text_font);
    sz_TotalUmsatz = g.MeasureString("Total Umsatz", Text_font);
    //calculate padding
    float freespace =rightMargin- sz_Nr.Width + sz_Datum.Width + sz_Mwst19.Width + sz_Mwst7.Width + sz_TotalMwst.Width + sz_TotalUmsatz.Width;
    float div_factor = rightMargin / 30;
    leftMargin += Convert.ToInt16(div_factor);
    pad_nr = freespace / (div_factor+(div_factor/4));
    pad_datum = freespace / div_factor;
    pad_rest = freespace / div_factor;
    g.DrawString("Nr.", Text_font, Brushes.Black, leftMargin, nextline,center);
    g.DrawString("Datum", Text_font, Brushes.Black, leftMargin+sz_Nr.Width+pad_nr, nextline,center);
    g.DrawString("Total Umsatz", Text_font, Brushes.Black, leftMargin + sz_Nr.Width +sz_Datum.Width+pad_nr+pad_datum, nextline,center);
    g.DrawString("Mwst 19%", Text_font, Brushes.Black, leftMargin + sz_Nr.Width + sz_Datum.Width +sz_TotalUmsatz.Width+ pad_nr+pad_datum+pad_rest, nextline,center);
    g.DrawString("Mwst 7%", Text_font, Brushes.Black, leftMargin + sz_Nr.Width + sz_Datum.Width + sz_TotalUmsatz.Width + sz_Mwst19.Width + pad_nr + pad_datum + pad_rest+pad_rest, nextline,center);
    g.DrawString("Total Mwst", Text_font, Brushes.Black, leftMargin + sz_Nr.Width + sz_Datum.Width + sz_TotalUmsatz.Width + sz_Mwst19.Width + sz_Mwst7.Width + pad_nr + pad_datum + 3*pad_rest, nextline,center);
    
    nextline += Text_font.Height;
    xy[0] = new Point(0, nextline);
    xy[1] = new Point(p.PaperSize.Width, nextline);
    g.DrawLine(new Pen(Color.Black), xy[0], xy[1]);
    nextline += 3;
    remainingHeight -= nextline;
    // load startdate umsatz in umsatz variables
    loadUmsatz(startTag,startMonat,startJahr);
    
    while (pageNo == currentPage & noOfDays>=0)
    {
        
        sno++;
        loadUmsatz(workingDate);
       
        g.DrawString(sno.ToString(), Text_font, Brushes.Black, leftMargin, nextline,center);
        g.DrawString(workingDate.ToShortDateString(), Text_font, Brushes.Black, leftMargin + sz_Nr.Width + pad_nr, nextline,center);
        g.DrawString(summe.ToString(), Text_font, Brushes.Black, leftMargin + sz_Nr.Width + sz_Datum.Width + pad_nr + pad_datum, nextline,center);
        g.DrawString(Mwst19.ToString(), Text_font, Brushes.Black, leftMargin + sz_Nr.Width + sz_Datum.Width + sz_TotalUmsatz.Width + pad_nr + pad_datum + pad_rest, nextline,center);
        g.DrawString(Mwst7.ToString(), Text_font, Brushes.Black, leftMargin + sz_Nr.Width + sz_Datum.Width + sz_TotalUmsatz.Width + sz_Mwst19.Width + pad_nr + pad_datum + pad_rest + pad_rest, nextline,center);
        g.DrawString((Mwst7+Mwst19).ToString(), Text_font, Brushes.Black, leftMargin + sz_Nr.Width + sz_Datum.Width + sz_TotalUmsatz.Width + sz_Mwst19.Width + sz_Mwst7.Width + pad_nr + pad_datum + 3 * pad_rest, nextline,center);
    
         nextline += Text_font.Height;
        workingDate= workingDate.AddDays(1);
        
        noOfDays--;
        if (nextline >= e.PageBounds.Height - 50)
        {
            pageNo++;

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
        g.DrawString("Gesamt:     ", Text_font, Brushes.Black, leftMargin + sz_Nr.Width + pad_nr, nextline, center);
        g.DrawString(totalSumme.ToString(), Text_font, Brushes.Black, leftMargin + sz_Nr.Width + sz_Datum.Width + pad_nr + pad_datum, nextline, center);
        g.DrawString(totalMwst7.ToString(), Text_font, Brushes.Black, leftMargin + sz_Nr.Width + sz_Datum.Width + sz_TotalUmsatz.Width + pad_nr + pad_datum + pad_rest, nextline, center);
        g.DrawString(totalMwst19.ToString(), Text_font, Brushes.Black, leftMargin + sz_Nr.Width + sz_Datum.Width + sz_TotalUmsatz.Width + sz_Mwst19.Width + pad_nr + pad_datum + pad_rest + pad_rest, nextline, center);
        g.DrawString((totalMwst7 + totalMwst19).ToString(), Text_font, Brushes.Black, leftMargin + sz_Nr.Width + sz_Datum.Width + sz_TotalUmsatz.Width + sz_Mwst19.Width + sz_Mwst7.Width + pad_nr + pad_datum + 3 * pad_rest, nextline, center);
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
        //        multiplepages = false;
    }
  

}

private void loadUmsatz(DateTime date)
{
    summe = 0;
    Mwst19 = 0;
    Mwst7 = 0;
    string datestring = date.ToShortDateString();
    if (conn.State.ToString() == "Closed")
        conn.Open();
    
    string sql = "SELECT dbbari.abbrechnung.Betrag, dbbari.abbrechnung.Mwst19,dbbari.abbrechnung.Mwst7 FROM dbbari.abbrechnung where dbbari.abbrechnung.Datum='" + datestring + "';";
    cmd = new MySqlCommand(sql, conn);
    try
    {
        if (rdr.IsClosed)
            rdr = cmd.ExecuteReader();
        else
        {
            rdr.Close();
            rdr = cmd.ExecuteReader();
        }

    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.ToString());
    }

    if (rdr.HasRows)
    {
        while (rdr.Read())
        {
            try
            {
                summe += Convert.ToDouble(rdr["Betrag"].ToString());
                Mwst7 += Convert.ToDouble(rdr["Mwst7"].ToString());
                Mwst19 += Convert.ToDouble(rdr["Mwst19"].ToString());
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "Here");
            }

        }
        totalSumme += summe;
        //MessageBox.Show(totalSumme.ToString());
        totalMwst7 += Mwst7;
        totalMwst19 += Mwst19;
    }
    try
    {
        rdr.Close();
        conn.Close();
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.ToString());
    }

}
private void loadUmsatz(int tag,int monat, int jahr)
{
    summe=0;
    Mwst19=0;
    Mwst7=0;
    string datestring = tag.ToString() + "." + monat.ToString() + "." + jahr.ToString();
    if (conn.State.ToString() == "Closed")
        conn.Open();
    string sql = "SELECT dbbari.abbrechnung.Betrag, dbbari.abbrechnung.Mwst19,dbbari.abbrechnung.Mwst7 FROM dbbari.Kundendaten,dbbari.abbrechnung where dbbari.abbrechnung.Datum='"+datestring+"';"  ;
    cmd = new MySqlCommand(sql, conn);
    try
            {

                rdr = cmd.ExecuteReader();

            }
    catch (Exception ex)
    {
        MessageBox.Show(ex.ToString());
    }

    if (rdr.HasRows)
    {
        while (rdr.Read())
        {
            try
            {
                summe += Convert.ToDouble(rdr["Betrag"].ToString());
                Mwst7 += Convert.ToDouble(rdr["Mwst7"].ToString());
                Mwst19 += Convert.ToDouble(rdr["Mwst19"].ToString());
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "Here");
            }
        }
    }
}  //obsulete
        #region  PrintDocument
      public void PrintDocument()
{
    //Create an instance of our printer class
   // Print_List m_previewDlg = new Print_List(new DateTime(2012, 9, 1), new System.DateTime(2012, 10, 30));
    //Issue print command
    m_previewDlg.Document = this;
    m_prnDoc = this;
    //m_previewDlg.ShowDialog();
    m_prnDoc.Print();
 //   this.Print();
 //  printer.Print();
}
        #endregion


    }
}
