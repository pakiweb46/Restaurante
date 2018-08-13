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
    class AbrechnungDruck : System.Drawing.Printing.PrintDocument
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
        private double AnzahlBestellungen_text;
        private double AnzahlStunden_text;
        private double Stundenlohn_text;
      private double KilometerGeld_text;
      
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
         public double AnzahlBestellungen_Text
         {
             get { return AnzahlBestellungen_text; }
             set { AnzahlBestellungen_text = value; }
         }
         public bool Fahrer
         {
             get { return Fahrer; }
             set { Fahrer = value; }
         }
         public double AnzahlStunden_Text
      {
          get { return AnzahlStunden_text; }
          set { AnzahlStunden_text = value; }
      }
         public double KilometerGeld_Text
         {
             get { return KilometerGeld_text; }
             set { KilometerGeld_text = value; }
      }
         public double Stundenlohn_Text
         {
             get { return Stundenlohn_text; }
             set { Stundenlohn_text = value; }
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
        #endregion

       
        #region  Class Constructors 
/// <summary>
/// Empty constructor
/// </summary>
/// <remarks></remarks>
public AbrechnungDruck() : base()
{
    //Set the file stream
    //Instantiate out Text property to an empty string
       
        PrintDate_text=string.Empty;
        Title_text=string.Empty;
        Date_text=string.Empty;
        Umsatz_text=0;
        AnzahlBestellungen_text = 0;
        AnzahlStunden_text = 0;
       KilometerGeld_text = 0;
       Stundenlohn_text = 0;
       
    
}
/// <summary>
/// Constructor to initialize our printing object
/// and the text it's supposed to be printing
/// </summary>
/// <param name=str>Text that will be printed</param>
/// <remarks></remarks>

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
    // StudenZahl
    position = rightMargin / 2;
    g.DrawString("Anzahl der Bestellungen:  ", Text_font, Brushes.Black, position, nextline, right);
    position = (rightMargin / 2) + 80;
    g.DrawString((String.Format("{0:0.00}", AnzahlBestellungen_text)), Text_font, Brushes.Black, position, nextline, right);
    nextline = nextline + Text_font.Height + Text_font.Height / 2;

    position = rightMargin / 2;
    g.DrawString("Anzahl der Stunden:  ", Text_font, Brushes.Black, position, nextline, right);
    position = (rightMargin / 2) + 80;
    g.DrawString((String.Format("{0:0.00}",AnzahlStunden_text )), Text_font, Brushes.Black, position, nextline, right);
    nextline = nextline + Text_font.Height + Text_font.Height / 2;

    position = rightMargin / 2;
    g.DrawString("Stunden Lohn:  ", Text_font, Brushes.Black, position, nextline, right);
    position = (rightMargin / 2) + 80;
    g.DrawString((String.Format("{0:0.00}", Stundenlohn_text)), Text_font, Brushes.Black, position, nextline, right);
    nextline = nextline +3+ Text_font.Height + Text_font.Height / 2;
    // Draw Line
    xy[0] = new Point(leftMargin, nextline);
    xy[1] = new Point(printWidth, nextline);

    g.DrawLine(new Pen(Color.Black), xy[0], xy[1]);
    nextline += 3;
    position = rightMargin / 2;
    g.DrawString("Gesamt Lohn:  ", Text_font, Brushes.Black, position, nextline, right);
    position = (rightMargin / 2) + 80;
    g.DrawString((String.Format("{0:0.00}", AnzahlStunden_text*Stundenlohn_text + 0.5 *AnzahlBestellungen_text)), Text_font, Brushes.Black, position, nextline, right);
    nextline = nextline + Text_font.Height + Text_font.Height / 2;

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


