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
using System.Drawing.Text;
using System.Data.Sql;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using System.Runtime;///پیغام قطع/ وصلی نت
using System.Runtime.InteropServices;///پیغام قطع/ وصلی نت
using DevComponents.DotNetBar;

namespace Parkban
{
    public partial class ReportDevice : Form
    {
        Connection con;
        [DllImport("wininet.dll")]///پیغام قطع/ وصلی نت
        private extern static bool InternetGetConnectedState(out int Conn, int val);///پیغام قطع/ وصلی نت
                                                                                    ///
        public ReportDevice()
        {
            InitializeComponent();
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("fa-IR"));
        }

        private void ReportDevice_Load(object sender, EventArgs e)
        {
            ///پیغام قطع/ وصلی نت
            int Desc;
            if (InternetGetConnectedState(out Desc, 0) == true)
            {
                lbl_Net.ForeColor = Color.LimeGreen;
                lbl_Net.Text = "Connected Net";
            }
            else
            {
                lbl_Net.ForeColor = Color.Red;
                lbl_Net.Text = "Not Connected Net";
            }
            ///پیغام قطع/ وصلی نت
            ///

            lbl_Tme.Text = DateTime.Now.ToShortTimeString();           
            lbl_Dte.Text = SahmsiDate.ShamsiDate;

            lbl_Usr.Text = Login.SetUsr;
            lbl_Lvl.Text = Login.SetTypeLev;
            lbl_LvlT.Text = Login.SetcomboLvlSys;
            //////////////////////////////////////
            GridShowDivce();
        }

        private void timer_Net_Tick(object sender, EventArgs e)
        {
            ///پیغام قطع/ وصلی نت
            int Desc;
            if (InternetGetConnectedState(out Desc, 0) == true)
            {
                lbl_Net.ForeColor = Color.LimeGreen;
                lbl_Net.Text = "Connected Net";
            }
            else
            {
                lbl_Net.ForeColor = Color.Red;
                lbl_Net.Text = "Not Connected Net";
            }
            ///پیغام قطع/ وصلی نت
            ///
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            lbl_Tme.Text = DateTime.Now.ToShortTimeString();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////
        SqlDataAdapter sda_dvc;
        DataTable dt_dvc;
        public void GridShowDivce()
        {
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                sda_dvc = new SqlDataAdapter(@"SELECT * FROM Device", con.GetConnection);
                dt_dvc = new DataTable();
                sda_dvc.Fill(dt_dvc);

                dtG_.DataSource = dt_dvc;
                dtG_.Columns[0].HeaderText = "ID";
                dtG_.Columns[0].Width = 50;
                dtG_.Columns[1].HeaderText = "IMEI";
                dtG_.Columns[1].Width = 200;
                dtG_.Columns[2].HeaderText = "شماره گوشی";
                dtG_.Columns[2].Width = 100;
                dtG_.Columns[3].HeaderText = "سریال شارژر";
                dtG_.Columns[3].Width = 200;
                dtG_.Columns[4].HeaderText = "شماره سیم کارت";
                dtG_.Columns[4].Width = 200;
                dtG_.Columns[5].HeaderText = "کاربر سیستم";
                dtG_.Columns[5].Width = 150;
                dtG_.Columns[6].HeaderText = "توضیحات";
                dtG_.Columns[6].Width = 150;
            }
            catch
            {
                lbl_W.Text = "خطا در پر کردن جدول دستگاه";
            }
            finally
            {
                con.GetConnection.Close();
            }
        }



    }
}
