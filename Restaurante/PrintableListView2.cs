using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
namespace Restaurante
{
    /// <summary>
    ///		Summary description for PrintableListView.
    /// </summary>
    public class PrintableListView2 : System.Windows.Forms.ListView
    {
   //    private bool MultiplePages = false;
        // Print fields
        private PrintDocument m_printDoc = new PrintDocument();
        private PageSetupDialog m_setupDlg = new PageSetupDialog();
        private PrintPreviewDialog m_previewDlg = new PrintPreviewDialog();
        private PrintDialog m_printDlg = new PrintDialog();

        private int m_nPageNumber = 1;
        private int m_nStartRow = 0;
        private int m_nStartCol = 0;
        private float pre_colwidth = 0;
        private bool m_bPrintSel = false;

        private bool m_bFitToPage = false;

        private float m_fListWidth = 0.0f;
        private float[] m_arColsWidth;

        private float m_fDpi = 96.0f;

        private string m_strTitle = "";
        private Font m_strFont=new Font("Arial", 12);
        private Font m_strFont2 = new Font("Arial", 12);
        private Font m_strFont3 = new Font("Arial", 12);
        
        private string m_strTitle2 = "";
        private string m_strTitle3 = "";
        private string summe = "";
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        #region Properties
        /// <summary>
        ///		Gets or sets whether to fit the list width on a single page
        /// </summary>
        /// <value>
        ///		<c>True</c> if you want to scale the list width so it will fit on a single page.
        /// </value>
        /// <remarks>
        ///		If you choose false (the default value), and the list width exceeds the page width, the list
        ///		will be broken in multiple page.
        /// </remarks>
        public bool FitToPage
        {
            get { return m_bFitToPage; }
            set { m_bFitToPage = value; }
        }

        /// <summary>
        ///		Gets or sets the title to dispaly as page header in the printed list
        /// </summary>
        /// <value>
        ///		A <see cref="string"/> the represents the title printed as page header.
        /// </value>
        
        public bool printsumme = false;
        public Font font1 = new Font("Arial", 12, FontStyle.Bold);
              
        public bool printMwst = false;
        public string Title
        {
            get { return m_strTitle; }
            set { m_strTitle = value; }
        }
        public Font TitleFont
        {
            get { return m_strFont; }
            set { m_strFont = value; }
        }
        public string Title2
        {
            get { return m_strTitle2; }
            set { m_strTitle2 = value; }
        }
        public Font TitleFont2
        {
            get { return m_strFont2; }
            set { m_strFont2 = value; }
        }

        public string Title3
        {
            get { return m_strTitle3; }
            set { m_strTitle3 = value; }
        }
        public Font TitleFont3
        {
            get { return m_strFont3; }
            set { m_strFont3 = value; }
        }
        public string summesatz
        {
            get { return summe; }
            set { summe = value; }
        }
        #endregion

        public PrintableListView2()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            m_printDoc.BeginPrint += new PrintEventHandler(OnBeginPrint);
            m_printDoc.PrintPage += new PrintPageEventHandler(OnPrintPage);

            m_setupDlg.Document = m_printDoc;
            m_previewDlg.Document = m_printDoc;
            m_printDlg.Document = m_printDoc;

            m_printDlg.AllowSomePages = false;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion

        /// <summary>
        ///		Show the standard page setup dialog box that lets the user specify
        ///		margins, page orientation, page sources, and paper sizes.
        /// </summary>
        public void PageSetup()
        {
            m_setupDlg.ShowDialog();
         
        }
        public void setToA5()
        {
            int a5index = 0;     
            System.Drawing.Printing.PaperSize  pkSize;
                    for (int i = 0; i <m_printDoc.PrinterSettings.PaperSizes.Count; i++)
                    {
                        pkSize = m_printDoc.PrinterSettings.PaperSizes[i];
                        if (pkSize.PaperName.ToString() == "A5")
                        {
                            a5index = i;
                        }  

                    }
                    if(m_printDoc.PrinterSettings.PaperSizes[a5index].PaperName=="A5")
                    m_printDoc.DefaultPageSettings.PaperSize=m_printDoc.PrinterSettings.PaperSizes[a5index];
                   
              
        }
        public void setToA4()
        {
            int a5index = 0;
            System.Drawing.Printing.PaperSize pkSize;
            for (int i = 0; i < m_printDoc.PrinterSettings.PaperSizes.Count; i++)
            {
                pkSize = m_printDoc.PrinterSettings.PaperSizes[i];
                if (pkSize.PaperName.ToString() == "A4")
                {
                    a5index = i;
                }

            }
            if (m_printDoc.PrinterSettings.PaperSizes[a5index].PaperName == "A4")
                m_printDoc.DefaultPageSettings.PaperSize = m_printDoc.PrinterSettings.PaperSizes[a5index];


        }


        /// <summary>
        ///		Show the standard print preview dialog box.
        /// </summary>
        public void PrintPreview()
        {
            m_printDoc.DocumentName = "List View";
          
            m_nPageNumber = 1;
            m_bPrintSel = false;


            m_previewDlg.ShowDialog(this);
        }

        /// <summary>
        ///		Start the print process.
        /// </summary>
        public void Print()
        {
            m_printDlg.AllowSelection = this.SelectedItems.Count > 0;

            // Show the standard print dialog box, that lets the user select a printer
            // and change the settings for that printer.
            if (m_printDlg.ShowDialog(this) == DialogResult.OK)
            {
                m_printDoc.DocumentName = m_strTitle;

                m_bPrintSel = m_printDlg.PrinterSettings.PrintRange == PrintRange.Selection;

                m_nPageNumber = 1;

                // Start print
                m_printDoc.Print();
            }
        }

        #region Events Handlers
        private void OnBeginPrint(object sender, PrintEventArgs e)
        {
            PreparePrint();
        }

        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            int nNumItems = GetItemsCount();  // Number of items to print

            if (nNumItems == 0 || m_nStartRow >= nNumItems)
            {
                e.HasMorePages = false;
       //         MultiplePages = false;
                return;
            }

            int nNextStartCol = 0; 			  // First column exeeding the page width
            float x = 0.0f;					  // Current horizontal coordinate
            float y = 0.0f;					  // Current vertical coordinate
            float cx = 5.0f;                  // The horizontal space, in hundredths of an inch,
            // of the padding between items text and
            // their cell boundaries.
            float fScale = 1.0f;              // Scale factor when fit to page is enabled
            float fRowHeight = 0.0f;		  // The height of the current row
            float fColWidth = 0.0f;		  // The width of the current column

            RectangleF rectFull;			  // The full available space
            RectangleF rectBody;			  // Area for the list items
           // RectangleF rectKunde;

            bool bUnprintable = false;

            Graphics g = e.Graphics;

            if (g.VisibleClipBounds.X < 0)	// Print preview
            {
                rectFull = e.MarginBounds;

                // Convert to hundredths of an inch
                rectFull = new RectangleF(rectFull.X / m_fDpi * 100.0f,
                    rectFull.Y / m_fDpi * 100.0f,
                    rectFull.Width / m_fDpi * 100.0f,
                    rectFull.Height / m_fDpi * 100.0f);
            }
            else							// Print
            {
                // Printable area (approximately) of the page, taking into account the user margins
                rectFull = new RectangleF(
                    e.MarginBounds.Left - (e.PageBounds.Width - g.VisibleClipBounds.Width) / 2,
                    e.MarginBounds.Top - (e.PageBounds.Height - g.VisibleClipBounds.Height) / 2,
                    e.MarginBounds.Width,
                    e.MarginBounds.Height);
            }
            //rectKunde = RectangleF.Inflate(rectFull, 0, -1 * Font.GetHeight(g));
           
          //  rectBody = RectangleF.Inflate(rectFull, 0, -8 * Font.GetHeight(g));
            rectBody = RectangleF.Inflate(rectFull, 0, -2 * Font.GetHeight(g));
    
        // Display title at top
            Font titleFont = m_strFont;//new Font("Arial", 12,FontStyle.Bold);
            StringFormat sfmt = new StringFormat();
            sfmt.Alignment = StringAlignment.Center;
            g.DrawString(m_strTitle, titleFont, Brushes.Black, rectFull, sfmt);
            // Display Kunden 
           /* StringFormat sfmt2 = new StringFormat();
            sfmt2.Alignment = StringAlignment.Center;
            g.DrawString(m_strTitle2, m_strFont2, Brushes.Black, rectKunde, sfmt2);
            // Display Rechnung Nr
            rectKunde = RectangleF.Inflate(rectFull, 0, -3 * Font.GetHeight(g));
 
            sfmt2.Alignment = StringAlignment.Near;
            g.DrawString(m_strTitle3, m_strFont3, Brushes.Black, rectKunde, sfmt2);
            */
            // Display page number at bottom
              sfmt.LineAlignment = StringAlignment.Far;
             g.DrawString("Page " + m_nPageNumber, Font, Brushes.Black, rectFull, sfmt);

            if (m_nStartCol == 0 && m_bFitToPage && m_fListWidth > rectBody.Width)
            {
                // Calculate scale factor
                fScale = rectBody.Width / m_fListWidth;
            }

            // Scale the printable area
            rectFull = new RectangleF(rectFull.X / fScale,
                                    rectFull.Y / fScale,
                                    rectFull.Width / fScale,
                                    rectFull.Height / fScale);

            rectBody = new RectangleF(rectBody.X / fScale,
                                      rectBody.Y / fScale,
                                      rectBody.Width / fScale,
                                      rectBody.Height / fScale);

            // Setting scale factor and unit of measure
            g.ScaleTransform(fScale, fScale);
            g.PageUnit = GraphicsUnit.Inch;
            g.PageScale = 0.01f;

            // Start print
            nNextStartCol = 0;
            y = rectBody.Top;

            // Columns headers ----------------------------------------
            Brush brushHeader = new SolidBrush(Color.LightGray);
            Font fontHeader = new Font(this.Font, FontStyle.Bold);
            fRowHeight = font1.GetHeight(g) * 4.0f;
            x = rectBody.Left;

            for (int i = m_nStartCol; i < Columns.Count; i++)
            {
                ColumnHeader ch = Columns[i];
                fColWidth = m_arColsWidth[i];
                if (i > 2)
                    pre_colwidth = m_arColsWidth[i - 2];
                if ((x + fColWidth) <= rectBody.Right)
                {
                    // Rectangle
                    g.FillRectangle(brushHeader, x, y, fColWidth, fRowHeight);
                    g.DrawRectangle(Pens.Black, x, y, fColWidth, fRowHeight);

                    // Text
                    StringFormat sf = new StringFormat();
                    if (ch.TextAlign == HorizontalAlignment.Left)
                        sf.Alignment = StringAlignment.Near;
                    else if (ch.TextAlign == HorizontalAlignment.Center)
                        sf.Alignment = StringAlignment.Center;
                    else
                        sf.Alignment = StringAlignment.Far;

                    sf.LineAlignment = StringAlignment.Center;
                    sf.FormatFlags = StringFormatFlags.NoWrap;
                    sf.Trimming = StringTrimming.EllipsisCharacter;

                    RectangleF rectText = new RectangleF(x + cx, y, fColWidth - 1 - 2 * cx, fRowHeight);
                    g.DrawString(ch.Text, font1, Brushes.Black, rectText, sf);
                    x += fColWidth;
                }
                else
                {
                    if (i == m_nStartCol)
                        bUnprintable = true;

                    nNextStartCol = i;
                    break;
                }
            }
            y += fRowHeight;

            // Rows ---------------------------------------------------
            int nRow = m_nStartRow;
            bool bEndOfPage = false;
            while (!bEndOfPage && nRow < nNumItems)
            {
                ListViewItem item = GetItem(nRow);

                fRowHeight = item.Bounds.Height / m_fDpi * 100.0f + 10.0f;

                if (y + fRowHeight > rectBody.Bottom)
                {
                    bEndOfPage = true;
                }
                else
                {
                    x = rectBody.Left;

                    for (int i = m_nStartCol; i < Columns.Count; i++)
                    {
                        ColumnHeader ch = Columns[i];
                        fColWidth = m_arColsWidth[i];

                        if ((x + fColWidth) <= rectBody.Right)
                        {
                            // Rectangle
                            g.DrawRectangle(Pens.Black, x, y, fColWidth, fRowHeight);

                            // Text
                            StringFormat sf = new StringFormat();
                            if (ch.TextAlign == HorizontalAlignment.Left)
                                sf.Alignment = StringAlignment.Near;
                            else if (ch.TextAlign == HorizontalAlignment.Center)
                                sf.Alignment = StringAlignment.Center;
                            else
                                sf.Alignment = StringAlignment.Far;

                            sf.LineAlignment = StringAlignment.Center;
                            sf.FormatFlags = StringFormatFlags.NoWrap;
                            sf.Trimming = StringTrimming.EllipsisCharacter;

                            // Text
                            string strText = i == 0 ? item.Text : item.SubItems[i].Text;
                            Font font = i == 0 ? item.Font : item.SubItems[i].Font;
                          
                            RectangleF rectText = new RectangleF(x + cx, y, fColWidth - 1 - 2 * cx, fRowHeight);
                            g.DrawString(strText, font1, Brushes.Black, rectText, sf);
                            x += fColWidth;
                        }
                        else
                        {
                            nNextStartCol = i;
                            break;
                        }
                    }

                    y += fRowHeight;
                    nRow++;
                }
                
            }
            if (printsumme && nRow==nNumItems)
            {
                StringFormat sf1 = new StringFormat();
                g.DrawRectangle(Pens.Black, x - (fColWidth + pre_colwidth) + 2, y, pre_colwidth - 2, fRowHeight);

                g.DrawRectangle(Pens.Black, x - fColWidth, y, fColWidth, fRowHeight);

                string strText1 = summe;
                //Font font1 = new Font("Arial", 12, FontStyle.Bold);
                RectangleF rectText1 = new RectangleF(x - (fColWidth + pre_colwidth), y, fColWidth - 1 - 2 * cx, fRowHeight);
                sf1.Alignment = StringAlignment.Near;
                g.DrawString("  Summe", font1, Brushes.Black,rectText1, sf1);
                sf1.Alignment = StringAlignment.Near;
                rectText1 = new RectangleF(x - fColWidth, y, fColWidth - 1 - 2 * cx, fRowHeight);
                g.DrawString("  " + strText1, font1, Brushes.Black, rectText1, sf1);
            }
            if (printMwst)
            {
                StringFormat sf1 = new StringFormat();
                g.DrawRectangle(Pens.Black, x - (fColWidth + pre_colwidth) + 2, y, pre_colwidth - 1, fRowHeight);

                g.DrawRectangle(Pens.Black, x - (fColWidth + pre_colwidth+pre_colwidth)+2, y, pre_colwidth-1, fRowHeight);

                
               // Font font1 = new Font("Arial", 12, FontStyle.Bold);
                RectangleF rectText1 = new RectangleF(x - (fColWidth + pre_colwidth)- 1, y, 70, fRowHeight);
                sf1.Alignment = StringAlignment.Center;
                g.DrawString(summe, font1, Brushes.Black, rectText1, sf1);
                sf1.Alignment = StringAlignment.Center;
                //MessageBox.Show((fColWidth + 10).ToString());
                rectText1 = new RectangleF(x - (fColWidth + pre_colwidth + pre_colwidth), y, 70 , fRowHeight);
                g.DrawString("Summe", font1, Brushes.Black, rectText1, sf1);
            }
            if (nNextStartCol == 0)
                m_nStartRow = nRow;

            m_nStartCol = nNextStartCol;

            m_nPageNumber++;

            e.HasMorePages = (!bUnprintable && (m_nStartRow > 0 && m_nStartRow < nNumItems) || m_nStartCol > 0);

            if (!e.HasMorePages)
            {
                m_nPageNumber = 1;
                m_nStartRow = 0;
                m_nStartCol = 0;
            }

            brushHeader.Dispose();
        }
        #endregion

        private int GetItemsCount()
        {
            return m_bPrintSel ? SelectedItems.Count : Items.Count;
        }

        private ListViewItem GetItem(int index)
        {
            return m_bPrintSel ? SelectedItems[index] : Items[index];
        }

        private void PreparePrint()
        {
            // Gets the list width and the columns width in units of hundredths of an inch.
            this.m_fListWidth = 0.0f;
            this.m_arColsWidth = new float[this.Columns.Count];

            Graphics g = CreateGraphics();
            m_fDpi = g.DpiX;
            g.Dispose();

            for (int i = 0; i < Columns.Count; i++)
            {
                ColumnHeader ch = Columns[i];
                float fWidth = (ch.Width / m_fDpi * 100 + 1)-30; // Column width + separator
                m_fListWidth += fWidth;
                m_arColsWidth[i] = fWidth;
            }
            m_fListWidth += 1; // separator
        }

    }
}
