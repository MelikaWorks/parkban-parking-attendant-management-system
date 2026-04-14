using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Net;

namespace Parkban
{
    public partial class PrintSalary : Form
    {        
        public PrintSalary()
        {
            InitializeComponent();
        }

        private void PrintSalary_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("fa-IR"));

            lbl_namS.Text = Salary.Setcombo_Name;
            lbl_time.Text = Salary.SetTime;
            lbl_dateAz.Text = Salary.SetbCal_Az;
            lbl_dateTa.Text = Salary.SetbCal_Ta;           
            //////////////////////////////////////////////
            lbl_TrnsR.Text = Salary.Setlbl_TrnsR;
            lbl_ResShR.Text = Salary.Setlbl_ResShR;
            lbl_txt_PyShrg.Text = Salary.Settxt_PyShrg;
            lbl_ResCaR.Text = Salary.Setlbl_ResCaR;
            lbl_txt_PyCrd.Text = Salary.Settxt_PyCrd;
            lbl_KaraneR.Text = Salary.Setlbl_KaraneR;
            lbl_TashviqiR.Text = Salary.Setlbl_TashviqiR;
            lbl_SumR.Text = Salary.Setlbl_SumR;
            /////////////////////////////////////////////
            lbl_TaxR.Text = Salary.Setlbl_TaxR;
            lbl_InsurR.Text = Salary.Setlbl_InsurR;
            lbl_InsDbtR.Text = Salary.Setlbl_InsDbtR;
            lbl_TInsurKR.Text = Salary.Setlbl_TInsurKR;
            lbl_ImprsR.Text = Salary.Setlbl_ImprsR;
            lbl_TImprstKR.Text = Salary.Setlbl_TImprstKR;
            lbl_CrdKR.Text = Salary.Setlbl_CrdKR;
            lbl_TCrdKR.Text = Salary.Setlbl_TCrdKR;
            lbl_ShrgKR.Text = Salary.Setlbl_ShrgKR;
            lbl_TShargKR.Text = Salary.Setlbl_TShargKR;
            lbl_TDebtKR.Text = Salary.Setlbl_TDebtKR;
            lbl_DebtR.Text = Salary.Setlbl_DebtR;
            lbl_KasrR.Text = Salary.Setlbl_KasrR;
            lbl_SumAllR.Text = Salary.Setlbl_SumAllR;
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        private Bitmap memoryImage;

        private void PrintScreen()
        {
            Graphics mygraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, mygraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            IntPtr dc1 = mygraphics.GetHdc();
            IntPtr dc2 = memoryGraphics.GetHdc();
            BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376);
            mygraphics.ReleaseHdc(dc1);
            memoryGraphics.ReleaseHdc(dc2);
        }

        private void tool_Print_Click(object sender, EventArgs e)
        {
            PrintScreen();

            this.printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);

            System.Windows.Forms.PrintDialog PrintDialog1 = new PrintDialog();
            PrintDialog1.AllowSomePages = true;
            PrintDialog1.ShowHelp = true;
            PrintDialog1.Document = printDoc;

            DialogResult result = PrintDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDoc.Print();
            }
        }

        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }


    }
}
