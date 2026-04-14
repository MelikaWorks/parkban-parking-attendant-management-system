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

namespace Parkban
{
    public partial class ManagementDB : Form
    {
        Connection con;
        private SqlConnection conn;
        private SqlCommand command;
        private SqlDataReader read;
        string sql = "";
        string constr = "";

        [DllImport("wininet.dll")]///پیغام قطع/ وصلی نت
        private extern static bool InternetGetConnectedState(out int Conn, int val);///پیغام قطع/ وصلی نت
        
        public ManagementDB()
        {
            InitializeComponent();
        }

        public Form1 f1;
        public ManagementDB(Form1 f1)
            : this()
        {
            this.f1 = f1;
        }

        private void ManagementDB_Load(object sender, EventArgs e)
        {
            ribTbI_Connect.Select();
            tabControl1.SelectedTab = tbI_Connect;

            if (ribControl_All.SelectedRibbonTabItem == ribTbI_Connect)
            {
                tbI_Connect.Visible = true;
                tbI_BckUp.Visible = false;
                tbI_Restore.Visible = false;
                tbI_Reset.Visible = false;
                tbI_ImpExc.Visible = false;
            }
            if (ribControl_All.SelectedRibbonTabItem == ribTbI_BckUp)
            {
                tbI_Connect.Visible = false;
                tbI_BckUp.Visible = true;
                tbI_Restore.Visible = false;
                tbI_Reset.Visible = false;
                tbI_ImpExc.Visible = false;
            }
            if (ribControl_All.SelectedRibbonTabItem == ribTbI_Restore)
            {
                tbI_Connect.Visible = false;
                tbI_BckUp.Visible = false;
                tbI_Restore.Visible = true;
                tbI_Reset.Visible = false;
                tbI_ImpExc.Visible = false;
            }
            if (ribControl_All.SelectedRibbonTabItem == ribTbI_Reset)
            {
                tbI_Connect.Visible = false;
                tbI_BckUp.Visible = false;
                tbI_Restore.Visible = false;
                tbI_Reset.Visible = true;
                tbI_ImpExc.Visible = false;
            }
            if (ribControl_All.SelectedRibbonTabItem == ribTbI_ImpExc)
            {
                tbI_Connect.Visible = false;
                tbI_BckUp.Visible = false;
                tbI_Restore.Visible = false;
                tbI_Reset.Visible = false;
                tbI_ImpExc.Visible = true;
            }
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
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("en-GB"));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            lbl_Tme.Text = DateTime.Now.ToShortTimeString();
            lbl_Dte.Text = SahmsiDate.ShamsiDate;

            txt_DtaSrc.Focus();
            combo_SlctDb.Enabled = false;

            lbl_Usr.Text = Login.SetUsr;
            lbl_Lvl.Text = Login.SetTypeLev;
            lbl_LvlT.Text = Login.SetcomboLvlSys;
        }

        private void ribControl_All_SelectedRibbonTabChanged(object sender, EventArgs e)
        {
            if (ribControl_All.SelectedRibbonTabItem == ribTbI_Connect)
            {
                tbI_Connect.Visible = true;
                tbI_BckUp.Visible = false;
                tbI_Restore.Visible = false;
                tbI_Reset.Visible = false;
                tbI_ImpExc.Visible = false;
            }
            if (ribControl_All.SelectedRibbonTabItem == ribTbI_BckUp)
            {
                tbI_Connect.Visible = false;
                tbI_BckUp.Visible = true;
                tbI_Restore.Visible = false;
                tbI_Reset.Visible = false;
                tbI_ImpExc.Visible = false;
            }
            if (ribControl_All.SelectedRibbonTabItem == ribTbI_Restore)
            {
                tbI_Connect.Visible = false;
                tbI_BckUp.Visible = false;
                tbI_Restore.Visible = true;
                tbI_Reset.Visible = false;
                tbI_ImpExc.Visible = false;
            }
            if (ribControl_All.SelectedRibbonTabItem == ribTbI_Reset)
            {
                tbI_Connect.Visible = false;
                tbI_BckUp.Visible = false;
                tbI_Restore.Visible = false;
                tbI_Reset.Visible = true;
                tbI_ImpExc.Visible = false;
            }
            if (ribControl_All.SelectedRibbonTabItem == ribTbI_ImpExc)
            {
                tbI_Connect.Visible = false;
                tbI_BckUp.Visible = false;
                tbI_Restore.Visible = false;
                tbI_Reset.Visible = false;
                tbI_ImpExc.Visible = true;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////
        private void Del_AbsPrs()
        {
            DialogResult res = MessageBox.Show(" آیا از حذف (ریست ) جدول حضور و غیاب اطمینان دارید ؟!\n با ریست جدول تمامی داده ها پاک خواهد شد ", "هشدار", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            if (res == DialogResult.Yes)//
            {
                try
                {
                    con = new Connection();
                    con.GetConnection.Open();
                    string query = "TRUNCATE TABLE AbsentLeave ";
                    SqlCommand cmd = new SqlCommand(query, con.GetConnection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("جدول حضور غیاب با موفقیت ریست شد.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    check_AbsPrs.Checked = false;
                    this.Activate();
                    con.GetConnection.Close();
                }
                catch
                {
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    check_AbsPrs.Checked = false;
                }
            }
            else
            {
                check_AbsPrs.Checked = false;
                MessageBox.Show("عملیات ریست لغو شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        private void Del_Card()
        {
            DialogResult res = MessageBox.Show(" آیا از حذف (ریست ) جدول کارت اطمینان دارید ؟!\n با ریست جدول تمامی داده ها پاک خواهد شد ", "هشدار", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            if (res == DialogResult.Yes)//
            {
                try
                {
                    con = new Connection();
                    con.GetConnection.Open();
                    string query = "TRUNCATE TABLE Pay_Card ";
                    SqlCommand cmd = new SqlCommand(query, con.GetConnection);
                    cmd.ExecuteNonQuery();                  
                    MessageBox.Show("جدول فروش کارت با موفقیت ریست شد.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    check_Card.Checked = false;
                    this.Activate();
                    con.GetConnection.Close();
                }
                catch
                {
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    check_Card.Checked = false;
                }
            }
            else
            {
                check_Card.Checked = false;
                MessageBox.Show("عملیات ریست لغو شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        private void Del_CheckOut()
        {
            DialogResult res = MessageBox.Show(" آیا از حذف (ریست ) جدول تسویه پارکبان اطمینان دارید ؟!\n با ریست جدول تمامی داده ها پاک خواهد شد ", "هشدار", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            if (res == DialogResult.Yes)//
            {
                try
                {
                    con = new Connection();
                    con.GetConnection.Open();
                    string query = "TRUNCATE TABLE CheckOut ";
                    SqlCommand cmd = new SqlCommand(query, con.GetConnection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("جدول تسویه پارکبان با موفقیت ریست شد.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    check_CheckOut.Checked = false;
                    this.Activate();
                    con.GetConnection.Close();
                }
                catch
                {
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    check_CheckOut.Checked = false;
                }
            }
            else
            {
                check_CheckOut.Checked = false;
                MessageBox.Show("عملیات ریست لغو شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        private void Del_CostOffice()
        {
            DialogResult res = MessageBox.Show(" آیا از حذف (ریست ) جدول هزینه های دفتر اطمینان دارید ؟!\n با ریست جدول تمامی داده ها پاک خواهد شد ", "هشدار", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            if (res == DialogResult.Yes)//
            {
                try
                {
                    con = new Connection();
                    con.GetConnection.Open();
                    string query = "TRUNCATE TABLE OfficCost ";
                    SqlCommand cmd = new SqlCommand(query, con.GetConnection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("جدول هزینه های دفتر با موفقیت ریست شد.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    check_CostOffice.Checked = false;
                    this.Activate();
                    con.GetConnection.Close();
                }
                catch
                {
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    check_CostOffice.Checked = false;
                }
            }
            else
            {
                check_CostOffice.Checked = false;
                MessageBox.Show("عملیات ریست لغو شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        private void Del_Debt()
        {
            DialogResult res = MessageBox.Show(" آیا از حذف (ریست ) جدول بدهی لوازم و غیره اطمینان دارید ؟!\n با ریست جدول تمامی داده ها پاک خواهد شد ", "هشدار", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            if (res == DialogResult.Yes)//
            {
                try
                {
                    con = new Connection();
                    con.GetConnection.Open();
                    string query = "TRUNCATE TABLE Debt ";
                    SqlCommand cmd = new SqlCommand(query, con.GetConnection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("جدول بدهی پارکبان با موفقیت ریست شد.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    check_Debt.Checked = false;
                    this.Activate();
                    con.GetConnection.Close();
                }
                catch
                {
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    check_Debt.Checked = false;
                }
            }
            else
            {
                check_Debt.Checked = false;
                MessageBox.Show("عملیات ریست لغو شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        private void Del_Device()
        {
            DialogResult res = MessageBox.Show(" آیا از حذف (ریست ) جدول مشخصات دستگاه اطمینان دارید ؟!\n با ریست جدول تمامی داده ها پاک خواهد شد ", "هشدار", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            if (res == DialogResult.Yes)//
            {
                try
                {
                    con = new Connection();
                    con.GetConnection.Open();
                    string query = "TRUNCATE TABLE Device ";
                    SqlCommand cmd = new SqlCommand(query, con.GetConnection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("جدول مشخصات دستگاه (موبایل) با موفقیت ریست شد.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    check_Device.Checked = false;
                    this.Activate();
                    con.GetConnection.Close();
                }
                catch
                {
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    check_Device.Checked = false;
                }
            }
            else
            {
                check_Device.Checked = false;
                MessageBox.Show("عملیات ریست لغو شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        private void Del_Import()
        {
            DialogResult res = MessageBox.Show(" آیا از حذف (ریست ) جدول ایمپورت داده ها اطمینان دارید ؟!\n با ریست جدول تمامی داده ها پاک خواهد شد ", "هشدار", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            if (res == DialogResult.Yes)//
            {
                try
                {
                    con = new Connection();
                    con.GetConnection.Open();
                    string query = "TRUNCATE TABLE ImpTransExe ";
                    SqlCommand cmd = new SqlCommand(query, con.GetConnection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("جدول موقت ایمپورت با موفقیت ریست شد.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    check_Import.Checked = false;
                    this.Activate();
                    con.GetConnection.Close();

                    con = new Connection();
                    con.GetConnection.Open();
                    string query1 = "TRUNCATE TABLE Transact ";
                    SqlCommand cmd1 = new SqlCommand(query1, con.GetConnection);
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("جدول تراکنش با موفقیت ریست شد.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    check_Import.Checked = false;
                    this.Activate();
                    con.GetConnection.Close();

                    con = new Connection();
                    con.GetConnection.Open();
                    string query2 = "TRUNCATE TABLE Transact1 ";
                    SqlCommand cmd2 = new SqlCommand(query2, con.GetConnection);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("جدول تراکنش دوم با موفقیت ریست شد.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    check_Import.Checked = false;
                    this.Activate();
                    con.GetConnection.Close();

                    con = new Connection();
                    con.GetConnection.Open();
                    string query3 = "TRUNCATE TABLE Transact2 ";
                    SqlCommand cmd3 = new SqlCommand(query3, con.GetConnection);
                    cmd3.ExecuteNonQuery();
                    MessageBox.Show("جدول تراکنش نهایی با موفقیت ریست شد.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    check_Import.Checked = false;
                    this.Activate();
                    con.GetConnection.Close();
                }
                catch
                {
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    check_Import.Checked = false;
                }
            }
            else
            {
                check_Import.Checked = false;
                MessageBox.Show("عملیات ریست لغو شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        private void Del_Imprest()
        {
             DialogResult res = MessageBox.Show(" آیا از حذف (ریست ) جدول مساعده اطمینان دارید ؟!\n با ریست جدول تمامی داده ها پاک خواهد شد ", "هشدار", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
             if (res == DialogResult.Yes)//
             {
                 try
                 {
                     con = new Connection();
                     con.GetConnection.Open();
                     string query = "TRUNCATE TABLE Imprest ";
                     SqlCommand cmd = new SqlCommand(query, con.GetConnection);
                     cmd.ExecuteNonQuery();
                     MessageBox.Show("جدول مساعده پارکبان با موفقیت ریست شد.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     check_Imprest.Checked = false;
                     this.Activate();
                     con.GetConnection.Close();
                 }
                 catch
                 {
                     MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                     this.Activate();
                 }
                 finally
                 {
                     check_Imprest.Checked = false;
                 }
             }
             else
             {
                 check_Imprest.Checked = false;
                 MessageBox.Show("عملیات ریست لغو شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                 this.Activate();
             }
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        private void Del_Insurance()
        {
             DialogResult res = MessageBox.Show(" آیا از حذف (ریست ) جدول بیمه اطمینان دارید ؟!\n با ریست جدول تمامی داده ها پاک خواهد شد ", "هشدار", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
             if (res == DialogResult.Yes)//
             {
                 try
                 {
                     con = new Connection();
                     con.GetConnection.Open();
                     string query = "TRUNCATE TABLE Insurance ";
                     SqlCommand cmd = new SqlCommand(query, con.GetConnection);
                     cmd.ExecuteNonQuery();
                     MessageBox.Show("جدول بیمه با موفقیت ریست شد.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     check_Insurance.Checked = false;
                     this.Activate();
                     con.GetConnection.Close();
                 }
                 catch
                 {
                     MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                     this.Activate();
                 }
                 finally
                 {
                     check_Insurance.Checked = false;
                 }
             }
             else
             {
                 check_Insurance.Checked = false;
                 MessageBox.Show("عملیات ریست لغو شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                 this.Activate();
             }
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        private void Del_Loh()
        {
            DialogResult res = MessageBox.Show(" آیا از حذف (ریست ) جدول لوحه اطمینان دارید ؟!\n با ریست جدول تمامی داده ها پاک خواهد شد ", "هشدار", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            if (res == DialogResult.Yes)//
            {
                try
                {
                    con = new Connection();
                    con.GetConnection.Open();
                    string query = "TRUNCATE TABLE lohh ";
                    SqlCommand cmd = new SqlCommand(query, con.GetConnection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("جدول لوحه با موفقیت ریست شد.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    check_Loh.Checked = false;
                    this.Activate();
                    con.GetConnection.Close();
                }
                catch
                {
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    check_Loh.Checked = false;
                }
            }
            else
            {
                check_Loh.Checked = false;
                MessageBox.Show("عملیات ریست لغو شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        private void Del_Parkban()
        {
            DialogResult res = MessageBox.Show(" آیا از حذف (ریست ) جدول مشخصات پارکبان اطمینان دارید ؟!\n با ریست جدول تمامی داده ها پاک خواهد شد ", "هشدار", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            if (res == DialogResult.Yes)//
            {
                try
                {
                    con = new Connection();
                    con.GetConnection.Open();
                    string query = "TRUNCATE TABLE Parkban_Property ";
                    SqlCommand cmd = new SqlCommand(query, con.GetConnection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("جدول مشخصات پارکبان با موفقیت ریست شد.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    check_Park.Checked = false;
                    this.Activate();
                    con.GetConnection.Close();
                }
                catch
                {
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    check_Park.Checked = false;
                }
            }
            else
            {
                check_Park.Checked = false;
                MessageBox.Show("عملیات ریست لغو شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        private void Del_Salary()
        {
            DialogResult res = MessageBox.Show(" آیا از حذف (ریست ) جدول حقوق اطمینان دارید ؟!\n با ریست جدول تمامی داده ها پاک خواهد شد ", "هشدار", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            if (res == DialogResult.Yes)//
            {
                try
                {
                    con = new Connection();
                    con.GetConnection.Open();
                    string query = "TRUNCATE TABLE Salary ";
                    SqlCommand cmd = new SqlCommand(query, con.GetConnection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("جدول حقوق با موفقیت ریست شد.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    check_Card.Checked = false;
                    this.Activate();
                    con.GetConnection.Close();

                    con = new Connection();
                    con.GetConnection.Open();
                    string query1 = "TRUNCATE TABLE Transact_Salary ";
                    SqlCommand cmd1 = new SqlCommand(query1, con.GetConnection);
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("جدول تراکنش حقوق با موفقیت ریست شد.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    check_Salary.Checked = false;
                    this.Activate();
                    con.GetConnection.Close();
                }
                catch
                {
                    check_Salary.Checked = false;
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    check_Salary.Checked = false;
                }
            }
            else
            {
                check_Salary.Checked = false;
                MessageBox.Show("عملیات ریست لغو شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        private void Del_Sharge()
        {
            DialogResult res = MessageBox.Show(" آیا از حذف (ریست ) جدول شارژ اطمینان دارید ؟!\n با ریست جدول تمامی داده ها پاک خواهد شد ", "هشدار", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            if (res == DialogResult.Yes)//
            {
                try
                {
                    con = new Connection();
                    con.GetConnection.Open();
                    string query = "TRUNCATE TABLE Pay_Sharg ";
                    SqlCommand cmd = new SqlCommand(query, con.GetConnection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("جدول ،فروش شارژ با موفقیت ریست شد.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    check_Sharge.Checked = false;
                    this.Activate();
                    con.GetConnection.Close();
                }
                catch
                {
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    check_Sharge.Checked = false;
                }
            }
            else
            {
                check_Sharge.Checked = false;
                MessageBox.Show("عملیات ریست لغو شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
        }
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Reset_Click(object sender, EventArgs e)
        {
            if ((check_AbsPrs.Checked == false) &&
               (check_Card.Checked == false) &&
               (check_CheckOut.Checked == false) &&
               (check_CostOffice.Checked == false) &&
               (check_Debt.Checked == false) &&
               (check_Device.Checked == false) &&
               (check_Import.Checked == false) &&
               (check_Imprest.Checked == false) &&
               (check_Insurance.Checked == false) &&
               (check_Loh.Checked == false) &&
               (check_Park.Checked == false) &&
               (check_Salary.Checked == false) &&
               (check_Sharge.Checked == false))
            {
                MessageBox.Show("برای پاکسازی جدول مورد نظر خود را تیک بزنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
            else
            {
                if (check_AbsPrs.Checked)
                {
                    Del_AbsPrs();
                    f1.RefDB();
                }
                else
                {
                }
                //////////////////////////////////////////////////////////////////////////////////////////
                if (check_Card.Checked)
                {
                    Del_Card();
                    f1.RefDB();
                }
                else
                {
                }
                //////////////////////////////////////////////////////////////////////////////////////////
                if (check_CheckOut.Checked)
                {
                    Del_CheckOut();
                    f1.RefDB();
                }
                else
                {
                }
                //////////////////////////////////////////////////////////////////////////////////////////
                if (check_CostOffice.Checked)
                {
                    Del_CostOffice();
                    f1.RefDB();
                }
                else
                {
                }
                //////////////////////////////////////////////////////////////////////////////////////////
                if (check_Debt.Checked)
                {
                    Del_Debt();
                    f1.RefDB();
                }
                else
                {
                }
                //////////////////////////////////////////////////////////////////////////////////////////
                if (check_Device.Checked)
                {
                    Del_Device();
                    f1.RefDB();
                }
                else
                {
                }
                //////////////////////////////////////////////////////////////////////////////////////////
                if (check_Import.Checked)
                {
                    Del_Import();
                    f1.RefDB();
                }
                else
                {
                }
                //////////////////////////////////////////////////////////////////////////////////////////
                if (check_Imprest.Checked)
                {
                    Del_Imprest();
                    f1.RefDB();
                }
                else
                {
                }
                //////////////////////////////////////////////////////////////////////////////////////////
                if (check_Insurance.Checked)
                {
                    Del_Insurance();
                    f1.RefDB();
                }
                else
                {
                }
                //////////////////////////////////////////////////////////////////////////////////////////
                if (check_Loh.Checked)
                {
                    Del_Loh();
                    f1.RefDB();
                }
                else
                {
                }
                //////////////////////////////////////////////////////////////////////////////////////////
                if (check_Park.Checked)
                {
                    Del_Parkban();
                    f1.RefDB();
                }
                else
                {
                }
                //////////////////////////////////////////////////////////////////////////////////////////
                if (check_Salary.Checked)
                {
                    Del_Salary();
                    f1.RefDB();
                }
                else
                {
                }
                //////////////////////////////////////////////////////////////////////////////////////////
                if (check_Sharge.Checked)
                {
                    Del_Sharge();
                    f1.RefDB();
                }
                else
                {
                }
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        private void Connect()
        {
            try
            {
                constr = "Data Source=" + txt_DtaSrc.Text + ";User Id=" + txt_UsrId.Text + ";Password=" + txt_Pass.Text + "";
                conn = new SqlConnection(constr);
                conn.Open();
                sql = "SELECT * FROM sys.databases";
                command = new SqlCommand(sql, conn);
                read = command.ExecuteReader();
                combo_SlctDb.Items.Clear();
                while (read.Read())
                {
                    combo_SlctDb.Items.Add(read[0].ToString());
                }
                read.Close();
                read.Dispose();
                conn.Close();
                conn.Dispose();
                combo_SlctDb.SelectedIndex = 0;
                this.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Activate();
            }
            finally
            {
                MessageBox.Show("Successfuly Connection Fill Combobox Choose Select Database  ", "Successfuly", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if((string.IsNullOrEmpty(txt_DtaSrc.Text))&&
                (string.IsNullOrEmpty(txt_Pass.Text))&&
                (string.IsNullOrEmpty(txt_UsrId.Text)))
            {
                MessageBox.Show("Please Fill Items");
            }
            else if ((string.IsNullOrEmpty(txt_DtaSrc.Text))||
                (string.IsNullOrEmpty(txt_Pass.Text))||
                (string.IsNullOrEmpty(txt_UsrId.Text)))
            {
                MessageBox.Show("Please Fill Items");
            }
            else
            {
                combo_SlctDb.Enabled = true;         
                Connect();
            }
        }

        private void btn_DisCon_Click(object sender, EventArgs e)
        {
            //txt_DtaSrc.Enabled = true;
            //txt_Pass.Enabled = true;
            //txt_UsrId.Enabled = true;
            combo_SlctDb.Enabled = false;
            combo_SlctDb.Items.Clear();
            lbl_DbSlct.Text = "-";
            //btn_BckUp.Enabled = false;
            //btn_Restore.Enabled = false;
        }

        private void combo_SlctDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_DbSlct.Text = Convert.ToString(combo_SlctDb.SelectedItem);
        }

        private void btn_BckUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (combo_SlctDb.Text.CompareTo("") == 0)
                {
                    MessageBox.Show("Please Select a Database in Tab Connection From List");
                    return;
                }
                conn = new SqlConnection(constr);
                conn.Open();
                sql = "BACKUP DATABASE " + combo_SlctDb.Text + " TO DISK = '" + txt_Bck.Text + "\\" + combo_SlctDb.Text + "-" + DateTime.Now.Ticks.ToString() + ".bak' ";
                command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                MessageBox.Show("Successfully Database Backup Complated.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Activate();
            }
        }

        private void btn_BckBrw_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txt_Bck.Text = dlg.SelectedPath;
            }
        }

        private void btn_RstrBrws_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Backup Files(*.bak)|*.bak|All Files(*.*)|*.*";
            dlg.FilterIndex = 0;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txt_Rstr.Text = dlg.FileName;
            }
        }

        private void btn_Restore_Click(object sender, EventArgs e)
        {
            try
            {
                if (combo_SlctDb.Text.CompareTo("") == 0)
                {
                    MessageBox.Show("Please Select a Database in Tab Connection From List");
                    return;
                }
                conn = new SqlConnection(constr);
                conn.Open();
                sql = "Alter DATABASE " + combo_SlctDb.Text + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE ";
                sql += "RESTORE DATABASE " + combo_SlctDb.Text + " FROM DISK = '" + txt_Rstr.Text +"' WITH REPLACE";
                command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                MessageBox.Show("Successfully Restore Database Complated.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Activate();
            }
        }

        private void txt_Pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Connect_Click((object)sender, (EventArgs)e);
            }
        }

        private void txt_Pass_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    btn_Connect_Click((object)sender, (EventArgs)e);
            //}
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_BckUpR_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (combo_SlctDb.Text.CompareTo("") == 0)
            //    {
            //        MessageBox.Show("Please Select a Database in Tab Connection From List");
            //        return;
            //    }
            //    con = new Connection();
            //    con.GetConnection.Open();
            //    sql = "BACKUP DATABASE " + combo_SlctDb.Text + " TO DISK = '" + txt_Bck.Text + "\\" + combo_SlctDb.Text + "-" + DateTime.Now.Ticks.ToString() + ".bak'";
            //    command = new SqlCommand(sql, con.GetConnection);
            //    command.ExecuteNonQuery();
            //    con.GetConnection.Close();
            //    con.GetConnection.Dispose();
            //    MessageBox.Show("Successfully Database Backup Complated.");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    this.Activate();
            //}
        }

       

       









    }
}
