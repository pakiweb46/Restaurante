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

    class RecieptPrint:System.Drawing.Printing.PrintDocument
    {
        private PrintPreviewDialog m_previewDlg = new PrintPreviewDialog();
       
        #region Logo Informations

        // Logo Image fileC:\Users\Bari\Documents\Restaurante\Restaurante\Resources\logo.jpg

        Image logo = new Bitmap(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("LieferDienst.Resources.logo.jpg"));
        // point to place the loge 
        Point pLogo = new Point(0,0);
            
#endregion
 
        #region  Property Variables
        /// <summary>
        /// Property variable for the Font the user wishes to use
        /// </summary>
        /// <remarks></remarks>
        private Font Title_font;
        private Font Addresse_font;
        private Font Bestellung_font;
        private Font KundenDaten_font;
        private Font Text_font;
        /// <summary>
        /// Property variable for the text to be printed
        /// </summary>
        /// <remarks></remarks>
        private int bestellNr=0;
        private string Title_text;
        private string Addresse_text_line1;
        private string Addresse_text_line2;
        private string Addresse_text_line3;
        private string oeffenung_text_line1;
        private string oeffenung_text_line2;
        private string Bestellung_text;
        private string KundenNr_text;
        private string idkunde_text;
        private string KundenName_text;
        private string KundenAddresse_text, KundenAddresse_text2;
        private string Hinweise_text;
        private string []Artikel_text;
        private string  []Artikel_nummer;
        private double []Artikel_preis;
        private int []Artikel_anzahl;
        private double Artikel_Summe;// keine Property
        private double Anfahrt_kosten;
        private double mwst_19;
        private double mwst_7;
        private double Gesamt_betrag;
      //  private double MwSt_Total;// keine Property
        private double rabatt;
        private int noOfArtikle; 
        #endregion

        #region  Class Properties
        /// <summary>
        /// Property to hold the text that is to be printed
        /// </summary>
        /// <value></value>
        /// <returns>A string</returns>
        /// <remarks></remarks>
        public int BestellNr
        {
            get { return bestellNr; }
            set { BestellNr = value; }
        }
        public string idkunde
        {
            get { return idkunde_text; }
            set { idkunde_text = value; }
        }
        public string Title_Text
        {
            get { return Title_text; }
            set { Title_text = value; }
        }
        public string Addresse_Text_Line1
        {
            get { return Addresse_text_line1; }
            set { Addresse_text_line1 = value; }
        }
        public string Addresse_Text_Line2
        {
            get { return Addresse_text_line2; }
            set { Addresse_text_line2 = value; }
        }
        public string Addresse_Text_Line3
        {
            get { return Addresse_text_line3; }
            set { Addresse_text_line3 = value; }
        }
        public string Oeffenung_Text_Line1
        {
            get { return oeffenung_text_line1; }
            set { oeffenung_text_line1 = value; }
        }
        public string Oeffenung_Text_Line2
        {
            get { return oeffenung_text_line2; }
            set { oeffenung_text_line2 = value; }
        }
        public string Bestellung_Text
        {
            get { return Bestellung_text; }
            set { Bestellung_text = value; }

        }
        public string KundenNr_Text
        {
            get { return KundenNr_text; }
            set { KundenNr_text = value; }
        
        }
        public string KundenName_Text
        {
            get { return KundenName_text; }
            set { KundenName_text = value; }
        }
        public string KundenAddresse_Text
        {
            get { return KundenAddresse_text; }
            set { KundenAddresse_text = value; }
        }
        public string KundenAddresse_Text2
        {
            get { return KundenAddresse_text2; }
            set { KundenAddresse_text2 = value; }
        }
        public string Hinweise_Text
        {
            get { return Hinweise_text; }
            set { Hinweise_text = value; }
        }
        public string[] Artikel_Text
        {
            get { return Artikel_text; }
            set {Artikel_text=value;}
         
        }
        
        public string[] Artikel_Nummer
        {
            get { return Artikel_nummer; }
            set { Artikel_nummer = value; }

        }
        public double[] Artikel_Preis
        {
            get { return Artikel_preis; }
            set { Artikel_preis = value; }

        }
        public int[] Artikel_Anzahl
        {
            get { return Artikel_anzahl; }
            set { Artikel_anzahl = value; }

        }
        /* private double Anfahrt_kosten;
        private double mwst_19;
        private double mwst_7;
        private double Gesamt_betrag;
        private double MwSt_Total;// keine Property
        private double rabatt;*/
        public double Anfahrt_Kosten
        {
            get { return Anfahrt_kosten; }
            set { Anfahrt_kosten = value; }
        }
        public double MwSt19
        {
            get { return mwst_19; }
            set { mwst_19 = value; }
        }
        public double MwSt7
        {
            get { return mwst_7; }
            set { mwst_7 = value; }
        }
        public double Rabatt
        {
            get { return rabatt; }
            set { rabatt = value; }
        }
        public double Gesamt_Betrag
        {
            get { return Gesamt_betrag; }
            set { Gesamt_betrag = value; }
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
        public Font AddresseFont
        {
            // Allows the user to override the default font
            get { return Addresse_font; }
            set { Addresse_font = value; }
        }
        public Font BestellungFont
        {
            // Allows the user to override the default font
            get { return Bestellung_font; }
            set { Bestellung_font = value; }
        }
        public Font KundenDatenFont
        {
            // Allows the user to override the default font
            get { return KundenDaten_font; }
            set { Title_font = value; }
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
public RecieptPrint() : base()
{
    //Set the file stream
    //Instantiate out Text property to an empty string
         Title_text= string.Empty;;
         Addresse_text_line1= string.Empty;
         Addresse_text_line2= string.Empty;
         Addresse_text_line3= string.Empty;
         oeffenung_text_line1= string.Empty;
         oeffenung_text_line2= string.Empty;;
         Bestellung_text= string.Empty;;
         KundenName_text= string.Empty;;
         KundenAddresse_text= string.Empty;;
         Hinweise_text= string.Empty;;
         Artikel_text = new string[1];
         Artikel_text[0]= string.Empty;
       
    
}
public RecieptPrint(int ArtikelCount)
    : base()
{
    //Set the file stream
    //Instantiate out Text property to an empty string
    Title_text = string.Empty; ;
    Addresse_text_line1 = string.Empty;
    Addresse_text_line2 = string.Empty;
    Addresse_text_line3 = string.Empty;
    oeffenung_text_line1 = string.Empty;
    oeffenung_text_line2 = string.Empty; ;
    Bestellung_text = string.Empty; ;
    KundenName_text = string.Empty; ;
    KundenAddresse_text = string.Empty; ;
    Hinweise_text = string.Empty; ;
    Artikel_text = new string[ArtikelCount];
    for(int i=0;i<ArtikelCount;i++)
    Artikel_text[i] = string.Empty; ;
    noOfArtikle = ArtikelCount;


}

/// <summary>
/// Constructor to initialize our printing object
/// and the text it's supposed to be printing
/// </summary>
/// <param name=str>Text that will be printed</param>
/// <remarks></remarks>
public RecieptPrint(string str) : base()
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
    if (Title_font == null)
    {
        //Create the font we need
        Title_font = new Font("ARIAL", 14,FontStyle.Bold);
    }
    if (Addresse_font == null)
    {
        Addresse_font = new Font("ARIAL", 11, FontStyle.Regular);
    }
    if (Bestellung_font == null)
    {
        Bestellung_font = new Font("ARIAL", 12, FontStyle.Bold);
    
    }
    if (KundenDaten_font == null)
    {
        KundenDaten_font = new Font("Times New Roman", 12, FontStyle.Bold);
    }
    if (Text_font == null)
    {
        Text_font = new Font("Times New Roman", 12, FontStyle.Regular);
    }
}
#endregion


#region  OnPrintPage
/// <summary>
/// Override the default OnPrintPage method of the PrintDocument
/// </summary>
/// <param name=e></param>
/// <remarks>This provides the print logic for our document</remarks>
private int pageNo = 0;
private int currentPage = 0,i=0;
bool multiplepages = false;
protected override void OnPrintPage(System.Drawing.Printing.PrintPageEventArgs e)
{
    //---- Height 800 bis kunden daten ca.250 plus 100 abschluss 20 pro no of artikles
    // 800-230 =570 zur verfügung560/20= 28 artikel machen wir 25 
    // Run base code
    base.OnPrintPage(e);
    
   // int CountArtikel;
    //Declare local variables needed
     Graphics g = e.Graphics;
    PageSettings p=e.PageSettings;
    StringFormat left = new StringFormat();
    StringFormat right=new StringFormat();
    StringFormat center = new StringFormat();
    
//    p.PaperSize = new PaperSize("A5", 400, 200);
   
    right.Alignment = StringAlignment.Far;
    left.Alignment = StringAlignment.Near;
    center.Alignment = StringAlignment.Center;
    int printHeight;
    int printWidth;
    int remainingHeight;
    int leftMargin;
    int rightMargin;
    int nextline;// holds the vertikel curser
    int position; // holds the horizental curser
    double stringwidth;// calculate the width of the string in pixel
     Point[] xy=new Point[2]; // array of points to hold different points for line drawing
           
    //Set print area size and margins
    {
        printHeight = Convert.ToInt32(p.PrintableArea.Height);//base.DefaultPageSettings.PaperSize.Height - base.DefaultPageSettings.Margins.Top - base.DefaultPageSettings.Margins.Bottom;
        printWidth = Convert.ToInt32(p.PrintableArea.Width);//base.DefaultPageSettings.PaperSize.Width - base.DefaultPageSettings.Margins.Left - base.DefaultPageSettings.Margins.Right;
        leftMargin = Convert.ToInt32(p.PrintableArea.Left);//base.DefaultPageSettings.Margins.Left;  //X
        rightMargin =Convert.ToInt32(p.PrintableArea.Right);// base.DefaultPageSettings.Margins.Top;  //Y
    }
    remainingHeight = printHeight;

    //Check if the user selected to print in Landscape mode
    //if they did then we need to swap height/width parameters
    if (base.DefaultPageSettings.Landscape)
     {
        int tmp;
        tmp = printHeight;
        printHeight = printWidth;
        printWidth = tmp;
    }
    nextline = Convert.ToInt32(p.PrintableArea.Top); // TOP 

    if (pageNo == 0)
    {
        // -- New Implementation
        // copy the graphics i think it will fastens the print
        i = 0;
        try
        {
            //if (Title_text == "Tamarinde")
              //  logo = new Bitmap(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("LieferDienst.Resources.logo.jpg"));
            //else
                logo = new Bitmap(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("LieferDienst.Resources.logo.jpg"));
                    //TODO:: Read from File instead

           
            
        }
        catch (Exception ex)
        {
            
            MessageBox.Show("Es ein fehler eingetreten :"+ex.Message+"catchinng on 430");
        }

        // set new logo Location
        pLogo = new Point(Convert.ToInt32(p.PrintableArea.Width - (logo.Width + 70)), 0);
        // Draw the Logo
        g.DrawImage(logo, pLogo);
        //---------------------------TITLE--------------------------        
        // Draw Title 

        // Draws the Title_font Top Left
        g.DrawString(Title_text, Title_font, Brushes.Black, leftMargin, nextline);
        // goto next line
        nextline += Title_font.Height;

        // Draw Addresse line one Here Bringdiesnt addresse
        g.DrawString(Addresse_text_line1, Addresse_font, Brushes.Black, leftMargin, nextline);
        nextline += Addresse_font.Height;
        // Draw Address line 2 Here Telefone
        g.DrawString(Addresse_text_line2, Addresse_font, Brushes.Black, leftMargin, nextline);
        nextline += Addresse_font.Height;
        // Draw Address line 3 Here Fax
        g.DrawString(Addresse_text_line3, Addresse_font, Brushes.Black, leftMargin, nextline);
        nextline += Addresse_font.Height;
        // Draw oeffnungzeiten Here webseite
        g.DrawString(oeffenung_text_line1, Addresse_font, Brushes.Black, leftMargin, nextline);
        nextline += Addresse_font.Height;
        nextline +=10;
        // Draw Bestellung Nr date and time1
        stringwidth = Convert.ToInt32(g.MeasureString(Bestellung_text, Bestellung_font).Width);
        int middle_position = Convert.ToInt32((printWidth / 2) - (stringwidth / 2));
        g.DrawString(Bestellung_text, Bestellung_font, Brushes.Black, middle_position, nextline);
        nextline = nextline + Bestellung_font.Height;
        // Draw Line
        xy[0] = new Point(0, nextline);
        xy[1] = new Point(printWidth, nextline);

        g.DrawLine(new Pen(Color.Black), xy[0], xy[1]);
        nextline += 3;
        // --------------------------------------- Kunden Daten -----------------------------
        // Draw kundenName
        g.DrawString("Name: " + KundenName_text, KundenDaten_font, Brushes.Black, leftMargin, nextline);
        nextline += KundenDaten_font.Height;
        // Draw kundenTelefone
        g.DrawString("Kunden Nr: " + idkunde_text, KundenDaten_font, Brushes.Black, leftMargin, nextline);
        nextline += KundenDaten_font.Height;
       
        // Draw kundenTelefone
        g.DrawString("Telefon: " + KundenNr_text, KundenDaten_font, Brushes.Black, leftMargin, nextline);
        nextline += KundenDaten_font.Height;
        // Draw kundenAddresse
        g.DrawString("Addresse: " + KundenAddresse_text, KundenDaten_font, Brushes.Black, leftMargin, nextline);
        nextline += KundenDaten_font.Height;
        g.DrawString("                : " + KundenAddresse_text2, KundenDaten_font, Brushes.Black, leftMargin, nextline);
        nextline += KundenDaten_font.Height;
        
        // Draw kundenHinweise
        g.DrawString("Hinweis: " + Hinweise_text, KundenDaten_font, Brushes.Black, leftMargin, nextline);
        nextline += KundenDaten_font.Height;
    
    }
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
    remainingHeight-=nextline;
    Artikel_Summe = 0;
    
    
    while(i <noOfArtikle && pageNo==currentPage)
    {
        // Draw Artikel Nummer
       /* Artikel_Summe += Artikel_Preis[i];
        g.DrawString(Artikel_nummer[i], Text_font, Brushes.Black, leftMargin, nextline,left);
        position = leftMargin + 65;
        g.DrawString(Artikel_text[i], Text_font, Brushes.Black, position, nextline,left);
       */
        try
        {
            if (Artikel_nummer[i] == "+"||Artikel_nummer[i] == "-")// nächste ist plus
            {
              //  Artikel_Summe += Artikel_Preis[i];
                g.DrawString("", Text_font, Brushes.Black, leftMargin, nextline, left);
                position = leftMargin + 65;
                g.DrawString(Artikel_text[i], Text_font, Brushes.Black, position, nextline, left);
       
                position = leftMargin + 330;
                if (Artikel_Text[i].Contains("pfand"))
                
                    g.DrawString(String.Format("{0:0.00}", Artikel_preis[i]), Text_font, Brushes.Black, position, nextline, right);
                
                else
                    g.DrawString(" ", Text_font, Brushes.Black, position, nextline, right);
                
                position = leftMargin + 410;
                g.DrawString(Artikel_anzahl[i].ToString(), Text_font, Brushes.Black, position, nextline, right);
                position = leftMargin + 520;
                ///------------------- momentat Aritkel zustaten comes in summe rein------------
                if (Artikel_Text[i].Contains("pfand"))
                g.DrawString(String.Format("{0:0.00}", Artikel_preis[i]), Text_font, Brushes.Black, position, nextline, right);
                else
                g.DrawString("  ", Text_font, Brushes.Black, position, nextline, right);
             
            }
            else if (Artikel_nummer[i] == "+-" )// letzte
            {
                Artikel_Summe += Artikel_Preis[i];
                g.DrawString("*", Text_font, Brushes.Black, leftMargin, nextline, left);
                position = leftMargin + 65;
                g.DrawString(Artikel_text[i], Text_font, Brushes.Black, position, nextline, left);
                    position = leftMargin + 330;
                    g.DrawString(String.Format("{0:0.00}", Artikel_preis[i]), Text_font, Brushes.Black, position, nextline, right);
                    position = leftMargin + 410;
                    g.DrawString(Artikel_anzahl[i].ToString(), Text_font, Brushes.Black, position, nextline, right);
                    position = leftMargin + 520;
                    ///------------------- momentat Aritkel zustaten comes in summe rein------------
                    g.DrawString(String.Format("{0:0.00}", Artikel_preis[i]), Text_font, Brushes.Black, position, nextline, right);
                  
            }
            else
            {
                Artikel_Summe += Artikel_Preis[i];
                g.DrawString(Artikel_nummer[i], Text_font, Brushes.Black, leftMargin, nextline, left);
                position = leftMargin + 65;
                g.DrawString(Artikel_text[i], Text_font, Brushes.Black, position, nextline, left);
       
                position = leftMargin + 330;
                g.DrawString(String.Format("{0:0.00}", Artikel_preis[i]), Text_font, Brushes.Black, position, nextline, right);
                position = leftMargin + 410;
                g.DrawString(Artikel_anzahl[i].ToString(), Text_font, Brushes.Black, position, nextline, right);
                position = leftMargin + 520;

                g.DrawString(String.Format("{0:0.00}", Artikel_preis[i] * Artikel_anzahl[i]), Text_font, Brushes.Black, position, nextline, right);
            }
        }
        catch
        {
                    }
        nextline = nextline + Text_font.Height;
        i++;
       // remainingHeight -= nextline;
        if (nextline >= e.PageBounds.Height-100)
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
        //------------------------- Abschluss --------------------------------

        // Anfahrt Kosten
        position = leftMargin + 450;
        g.DrawString("Anfahrtkosten: ", Text_font, Brushes.Black, position, nextline, right);
        position = leftMargin + 520;
        g.DrawString(String.Format("{0:0.00}", Anfahrt_kosten), Text_font, Brushes.Black, position, nextline, right);
        nextline = nextline + Text_font.Height;
        // MwSt7
        position = leftMargin + 450;
        g.DrawString("MwSt 7%: ", Text_font, Brushes.Black, position, nextline, right);
        position = leftMargin + 520;
        g.DrawString(String.Format("{0:0.00}", mwst_7), Text_font, Brushes.Black, position, nextline, right);
        nextline = nextline + Text_font.Height;
        // MwSt19
        position = leftMargin + 450;
        g.DrawString("MwSt 19%: ", Text_font, Brushes.Black, position, nextline, right);
        position = leftMargin + 520;
        g.DrawString(String.Format("{0:0.00}", mwst_19), Text_font, Brushes.Black, position, nextline, right);
        nextline = nextline + Text_font.Height;
        // Gesamt MwSt
        position = leftMargin + 450;
        g.DrawString("Gesamt Mwst: ", Text_font, Brushes.Black, position, nextline, right);
        position = leftMargin + 520;
        g.DrawString(String.Format("{0:0.00}", mwst_19 + mwst_7), Text_font, Brushes.Black, position, nextline, right);
        nextline = nextline + Text_font.Height;

        // Rabbat
        position = leftMargin + 450;
        g.DrawString("Rabbat: ", Text_font, Brushes.Black, position, nextline, right);
        position = leftMargin + 520;
        g.DrawString(String.Format("{0:0.00}", rabatt), Text_font, Brushes.Black, position, nextline, right);
        nextline = nextline + Text_font.Height;
        // gesamt
        position = leftMargin + 450;
        g.DrawString("Gesamtbetrag:  ", Text_font, Brushes.Black, position, nextline, right);
        position = leftMargin + 520;
        g.DrawString((String.Format("{0:0.00}", (Gesamt_betrag - rabatt))), Text_font, Brushes.Black, position, nextline, right);
        nextline = nextline + Text_font.Height;
        
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
/*public void PrintDocument()
{
    //Create an instance of our printer class
    RecieptPrint printer = new RecieptPrint();
  
    //Issue print command
  //  m_previewDlg.Document = this;
   // m_previewDlg.ShowDialog();
  
 //   this.Print();
   printer.Print();
}*/
#endregion


  }
}
