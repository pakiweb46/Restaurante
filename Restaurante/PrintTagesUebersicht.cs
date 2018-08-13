using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Restaurante   
{
    class PrintTagesUebersicht : System.Drawing.Printing.PrintDocument
    {
        private PrintPreviewDialog m_previewDlg = new PrintPreviewDialog();
      
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
        /// <summary>
        /// Property variable for the text to be printed
        /// </summary>
        /// <remarks></remarks>
        private string TitleHead_text = "Unbekannt";
        private string PrintDate_text="Unbekannt";
        private string Title_text="Unbekannt";
        private string Date_text = "Unbekannt";
        private double Umsatz_text;
       private double Hausverkauf_text;
      private double Abholer_text;
      private double Ausserhaus_text;
       private double MwSt19_text;
      private double MwSt7_text;
      
        #endregion

        #region  Class Properties
        /// <summary>
        /// Property to hold the text that is to be printed
        /// </summary>
        /// <value></value>
        /// <returns>A string</returns>
        /// <remarks></remarks>
        public string TitleHead_Text
        {
            get { return TitleHead_text; }
            set { TitleHead_text = value; }
        }
        
        public string Title_Text
        {
            get { return Title_text; }
            set { Title_text = value; }
        }
        public string Date_Text
        {
            get { return Date_text; }
            set { Date_text = value; }
        }
        public double Umsatz_Text
        {
            get { return Umsatz_text; }
            set { Umsatz_text = value; }
        }
         public double Hausverkauf_Text
         {
             get { return Hausverkauf_text; }
            set { Hausverkauf_text = value; }
         }
         public bool Fahrer
         {
             get { return Fahrer; }
             set { Fahrer = value; }
         }
      public double Abholer_Text
      {
      get { return Abholer_text; }
            set { Abholer_text = value; }
      }
      public double Ausserhaus_Text{
      get { return Ausserhaus_text; }
            set { Ausserhaus_text = value; }
      }
      public double MwSt19_Text{
      get { return MwSt19_text; }
            set { MwSt19_text = value; }
      }
      public double MwSt7_Text{
      get { return MwSt7_text; }
            set { MwSt7_text = value; }
      }
      
        /// <summary>
        /// 
        /// Property to hold the font the users wishes to use
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
  /*      private Font Title_font;
        private Font Addresse_font;
        private Font Bestellung_font;
        private Font KundenDaten_Font;
        private Font Text_Font;
  */
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
        #endregion

       
        #region  Class Constructors 
/// <summary>
/// Empty constructor
/// </summary>
/// <remarks></remarks>
public PrintTagesUebersicht() : base()
{
    //Set the file stream
    //Instantiate out Text property to an empty string
       
        PrintDate_text=string.Empty;
        Title_text=string.Empty;
        Date_text=string.Empty;
        Umsatz_text=0;
        Hausverkauf_text=0;
       Abholer_text=0;
       Ausserhaus_text=0;
       MwSt19_text=0;
       MwSt7_text=0;
       
    
}
/// <summary>
/// Constructor to initialize our printing object
/// and the text it's supposed to be printing
/// </summary>
/// <param name=str>Text that will be printed</param>
/// <remarks></remarks>
public PrintTagesUebersicht(string str) : base()
{
    //Set the file stream
    //Set our Text property value
    Title_text = str;
}
#endregion
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
        TitleHead_font = new Font("New Times Roman", 18,FontStyle.Bold);
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
}
#endregion


#region  OnPrintPage
/// <summary>
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
    
    //Declare local variables needed
     Graphics g = e.Graphics;
    PageSettings p=e.PageSettings;
    StringFormat left = new StringFormat();
    StringFormat right=new StringFormat();
    StringFormat center = new StringFormat();
    
    right.Alignment = StringAlignment.Far;
    left.Alignment = StringAlignment.Near;
    center.Alignment = StringAlignment.Center;
    int printHeight;
    int printWidth;
    int leftMargin;
    int rightMargin;
    int nextline;// holds the vertikel curser
    int position; // holds the horizental curser
  //  double stringwidth;// calculate the width of the string in pixel
     Point[] xy=new Point[2]; // array of points to hold different points for line drawing
           
    //Set print area size and margins
    {
        printHeight = Convert.ToInt32(p.PrintableArea.Height);//base.DefaultPageSettings.PaperSize.Height - base.DefaultPageSettings.Margins.Top - base.DefaultPageSettings.Margins.Bottom;
        printWidth = Convert.ToInt32(p.PrintableArea.Width);//base.DefaultPageSettings.PaperSize.Width - base.DefaultPageSettings.Margins.Left - base.DefaultPageSettings.Margins.Right;
        leftMargin = Convert.ToInt32(p.PrintableArea.Left);//base.DefaultPageSettings.Margins.Left;  //X
        rightMargin =Convert.ToInt32(p.PrintableArea.Right);// base.DefaultPageSettings.Margins.Top;  //Y
    }

    //Check if the user selected to print in Landscape mode
    //if they did then we need to swap height/width parameters
    if (base.DefaultPageSettings.Landscape)
     {
        int tmp;
        tmp = printHeight;
        printHeight = printWidth;
        printWidth = tmp;
    }
    // -- New Implementation
    // copy the graphics i think it will fastens the print
    
    //---------------------------TITLE--------------------------        
    // Draw Title 
    nextline=Convert.ToInt32(p.PrintableArea.Top+20); // TOP 
   // Draws the Title_font Top Left
    g.DrawString(TitleHead_text, TitleHead_font, Brushes.Black, rightMargin/2,nextline,center);
    // goto next line
    nextline += TitleHead_font.Height+PrintDate_font.Height;

    // Draws the Title_font Top Left
    PrintDate_text ="Datum : "+ System.DateTime.Now.ToShortDateString();
    g.DrawString(PrintDate_text, PrintDate_font, Brushes.Black, rightMargin, nextline, right);
    // goto next line
    nextline += (PrintDate_font.Height*2);
    g.DrawString(Title_text, Title_font, Brushes.Black, rightMargin / 2, nextline, center);
    nextline += Title_font.Height+3;
    g.DrawString(Date_text, Date_font, Brushes.Black, rightMargin / 2, nextline, center);
    // Draw Line
    nextline += Date_font.Height*2;
    
    xy[0] = new Point(leftMargin, nextline);
    xy[1] = new Point(printWidth, nextline);

    g.DrawLine(new Pen(Color.Black), xy[0], xy[1]);
    nextline += 3;
    nextline = nextline + Text_font.Height + Text_font.Height / 2;
   
    // Umsatz
    position = rightMargin/2;
    g.DrawString("Umsatz: ", Text_font, Brushes.Black, position, nextline, right);
    position = (rightMargin / 2)+80;
    
    g.DrawString(String.Format("{0:0.00}", Umsatz_text), Text_font_bold, Brushes.Black, position, nextline, right);
    nextline = nextline + Text_font.Height + Text_font.Height/2;
    // MwSt7
    position = rightMargin / 2;
    g.DrawString("Hausverkauf: ", Text_font, Brushes.Black, position, nextline, right);
    position = (rightMargin / 2) + 80; 
    g.DrawString(String.Format("{0:0.00}", Hausverkauf_text), Text_font, Brushes.Black, position, nextline, right);
    nextline = nextline + Text_font.Height + Text_font.Height / 2;
    // MwSt19
    position = rightMargin / 2;
    g.DrawString("Abholer: ", Text_font, Brushes.Black, position, nextline, right);
    position = (rightMargin / 2) + 80;
    g.DrawString(String.Format("{0:0.00}", Abholer_text), Text_font, Brushes.Black, position, nextline, right);
    nextline = nextline + Text_font.Height + Text_font.Height / 2;
    // Rabbat
    position = rightMargin / 2;
    g.DrawString("Auserhaus: ", Text_font, Brushes.Black, position, nextline, right);
    position = (rightMargin / 2) + 80;
    g.DrawString(String.Format("{0:0.00}", Ausserhaus_text), Text_font, Brushes.Black, position, nextline, right);
    nextline = nextline + Text_font.Height + Text_font.Height / 2;
    // gesamt
    position = rightMargin / 2;
    g.DrawString("Gesamt Mwst:  ", Text_font, Brushes.Black, position, nextline, right);
    position = (rightMargin / 2) + 80;
    g.DrawString((String.Format("{0:0.00}", MwSt19_Text+MwSt7_text)), Text_font, Brushes.Black, position, nextline, right);
    nextline = nextline + Text_font.Height + Text_font.Height / 2;

    position = rightMargin / 2;
    g.DrawString("Gesamt Mwst 19%:  ", Text_font, Brushes.Black, position, nextline, right);
    position = (rightMargin / 2) + 80;
    g.DrawString((String.Format("{0:0.00}",MwSt19_text )), Text_font, Brushes.Black, position, nextline, right);
    nextline = nextline + Text_font.Height + Text_font.Height / 2;

    position = rightMargin / 2;
    g.DrawString("Gesamt Mwst 7%:  ", Text_font, Brushes.Black, position, nextline, right);
    position = (rightMargin / 2) + 80;
    g.DrawString((String.Format("{0:0.00}", MwSt7_text)), Text_font, Brushes.Black, position, nextline, right);
    nextline = nextline +3+ Text_font.Height + Text_font.Height / 2;
    // Draw Line
    xy[0] = new Point(leftMargin, nextline);
    xy[1] = new Point(printWidth, nextline);

    g.DrawLine(new Pen(Color.Black), xy[0], xy[1]);
    nextline += 3;
   
 
    /*
    // Draw Addresse line one Here Bringdiesnt addresse
    g.DrawString(Addresse_text_line1, Addresse_font, Brushes.Black, leftMargin, nextline);
    nextline += Addresse_font.Height;
    // Draw Address line 2 Here Telefone
    g.DrawString(Addresse_text_line2, Addresse_font, Brushes.Black, leftMargin, nextline);
    nextline += Addresse_font.Height;
    // Draw Address line 3 Here Fax
    g.DrawString(Addresse_text_line2, Addresse_font, Brushes.Black, leftMargin, nextline);
    nextline += Addresse_font.Height;
    // Draw oeffnungzeiten Here webseite
    g.DrawString(oeffenung_text_line1, Addresse_font, Brushes.Black, leftMargin, nextline);
    nextline += Addresse_font.Height;
    // Draw Bestellung Nr date and time
    stringwidth = Convert.ToInt32(g.MeasureString(Bestellung_text, Bestellung_font).Width);
    int middle_position=Convert.ToInt32((printWidth/2)-(stringwidth/2));
    g.DrawString(Bestellung_text, Bestellung_font, Brushes.Black,middle_position , nextline);
    nextline = nextline + Bestellung_font.Height;       
    // Draw Line
    xy[0] = new Point(0, nextline);
    xy[1] = new Point(printWidth, nextline);
           
    g.DrawLine(new Pen(Color.Black), xy[0],xy[1]);
    nextline += 3;
    // --------------------------------------- Kunden Daten -----------------------------
    // Draw kundenName
    g.DrawString("Name: "+KundenName_text, KundenDaten_font, Brushes.Black, leftMargin, nextline);
    nextline +=KundenDaten_font.Height;
    // Draw kundenTelefone
    g.DrawString("Kunden Nr: "+KundenNr_text, KundenDaten_font, Brushes.Black, leftMargin, nextline);
    nextline += KundenDaten_font.Height;
    // Draw kundenAddresse
    g.DrawString("Addresse: "+KundenAddresse_text, KundenDaten_font, Brushes.Black, leftMargin, nextline);
    nextline += KundenDaten_font.Height;
    // Draw kundenHinweise
    g.DrawString("Hinweis: "+Hinweise_text, KundenDaten_font, Brushes.Black, leftMargin, nextline);
    nextline += KundenDaten_font.Height;
    // Draw Line
    xy[0] = new Point(0, nextline);
    xy[1] = new Point(p.PaperSize.Width, nextline);
    g.DrawLine(new Pen(Color.Black), xy[0], xy[1]);
    nextline += 3;
    // Draw fixed text Nr Bezeichnung Preis Anzahl Summe
    string text = "Nr.          Bezeichnung                                Preis         Anzahl            Summe";
    e.Graphics.DrawString(text, KundenDaten_font, Brushes.Black, leftMargin, nextline);
    nextline += KundenDaten_font.Height;
    // Draw Line
    xy[0] = new Point(0, nextline);
    xy[1] = new Point(p.PaperSize.Width, nextline);
    g.DrawLine(new Pen(Color.Black), xy[0], xy[1]);
    nextline += 3;
    // ------------------------ Artikel----------------------------
    // Loop
    Artikel_Summe = 0;
    for (int i = 0; i < noOfArtikle; i++)
    {
        // Draw Artikel Nummer
        Artikel_Summe += Artikel_Preis[i];
        g.DrawString(Artikel_nummer[i], Text_font, Brushes.Black, leftMargin, nextline,left);
        position = leftMargin + 65;
        g.DrawString(Artikel_text[i], Text_font, Brushes.Black, position, nextline,left);
        position = leftMargin + 330;
        g.DrawString(String.Format("{0:0.00}",Artikel_preis[i]), Text_font, Brushes.Black, position, nextline,right);
        position = leftMargin + 410;
        g.DrawString(Artikel_anzahl[i].ToString(), Text_font, Brushes.Black, position, nextline,right);
        position = leftMargin + 520;
        ///------------------- momentat Aritkel zustaten comes in summe rein------------
            g.DrawString(String.Format("{0:0.00}",Artikel_preis[i]*Artikel_anzahl[i]), Text_font, Brushes.Black, position, nextline,right);
        
        nextline = nextline + Text_font.Height;
    }
    // draw two lines
    xy[0] = new Point(0, nextline);
    xy[1] = new Point(Convert.ToInt32(p.PaperSize.Width), nextline);
    g.DrawLines(new Pen(Color.Black), xy);
    nextline = nextline + 5;
    xy[0] = new Point(0, nextline);
    xy[1] = new Point(Convert.ToInt32(p.PaperSize.Width), nextline);
    e.Graphics.DrawLines(new Pen(Color.Black), xy);
    nextline = nextline + 5;
   //------------------------- Abschluss --------------------------------
   
    // Anfahrt Kosten
    position = leftMargin + 450;
    g.DrawString("Anfahrtkosten: ", Text_font, Brushes.Black, position, nextline, right);
    position = leftMargin + 520;
    g.DrawString(String.Format("{0:0.00}",Anfahrt_kosten), Text_font, Brushes.Black, position, nextline,right);   
    nextline = nextline + Text_font.Height;
    // MwSt7
    position = leftMargin + 450;
    g.DrawString("MwSt 7%: ", Text_font, Brushes.Black, position, nextline, right);
    position = leftMargin + 520;
    g.DrawString(String.Format("{0:0.00}",mwst_7), Text_font, Brushes.Black, position, nextline,right);
    nextline = nextline + Text_font.Height;
    // MwSt19
    position = leftMargin + 450;
    g.DrawString("MwSt 19%: ", Text_font, Brushes.Black, position, nextline,right);
    position = leftMargin + 520;
    g.DrawString(String.Format("{0:0.00}",mwst_19), Text_font, Brushes.Black, position, nextline,right);
    nextline = nextline + Text_font.Height;
    // Rabbat
    position = leftMargin + 450;
    g.DrawString("Rabbat: ", Text_font, Brushes.Black, position, nextline,right);
    position = leftMargin + 520;
    g.DrawString(String.Format("{0:0.00}",rabatt), Text_font, Brushes.Black, position, nextline,right);
    nextline = nextline + Text_font.Height;
    // gesamt
    position = leftMargin + 450;
    g.DrawString("Gesamtbetrag:  ", Text_font, Brushes.Black, position, nextline,right);
    position = leftMargin + 520;
    g.DrawString((String.Format("{0:0.00}",(Gesamt_betrag+Anfahrt_Kosten-rabatt))), Text_font, Brushes.Black, position, nextline,right);
    nextline = nextline + Text_font.Height;
   
/* -- old impelmentation--
    //Create a rectangle printing are for our document
    RectangleF printArea = new RectangleF(leftMargin, rightMargin, printWidth, printHeight);

    //Use the StringFormat class for the text layout of our document
    StringFormat format = new StringFormat(StringFormatFlags.LineLimit);
    
    //Fit as many characters as we can into the print area      
    e.Graphics.MeasureString(Title_Text, TitleFont, new SizeF(printWidth, printHeight), format, out chars, out lines);

    //Print the page
    e.Graphics.DrawString(Title_text, TitleFont, Brushes.Black, printArea, format);

    //Increase current char count
   curChar += chars;

    //Detemine if there is more text to print, if
    //there is the tell the printer there is more coming
    if (curChar < Title_text.Length)
    {
        e.HasMorePages = true;
    }
    else
    {
        e.HasMorePages = false;
        curChar = 0;
    }
    */
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
    m_previewDlg.Document = this;
    m_previewDlg.ShowDialog();
    //printer.Print();
}
#endregion


  }
    }

