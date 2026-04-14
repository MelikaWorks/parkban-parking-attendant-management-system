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
    public partial class Management : Form
    {
        Connection con;
        [DllImport("wininet.dll")]///پیغام قطع/ وصلی نت
        private extern static bool InternetGetConnectedState(out int Conn, int val);///پیغام قطع/ وصلی نت
        
        public Management()
        {
            InitializeComponent();
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("fa-IR"));
            txt_NamUsr.Validated += new EventHandler(txt_NamUsr_Validated);
            txt_UsrNam.Validated += new EventHandler(txt_UsrNam_Validated);
            txt_Pass.Validated += new EventHandler(txt_Pass_Validated);
            combo_LvlUsr.Validated += new EventHandler(combo_LvlUsr_Validated);
        }

         public Form1 f1;
         public Management(Form1 f1)
             : this()
         {
             this.f1 = f1;
         }

        private void Management_Load(object sender, EventArgs e)
        {
            ribTI_Level.Select();           
            tabControl1.SelectedTab = tbI_Lvl;

            if (ribControl_All.SelectedRibbonTabItem == ribTI_Level)
            {
                tbI_Lvl.Visible = true;
                tbI_UsM.Visible = false;
                tbI_ChngPass.Visible = false;
                tbI_Hlp.Visible = false;                
            }
            if (ribControl_All.SelectedRibbonTabItem == ribTI_UsSys)
            {
                tbI_UsM.Visible = true;
                tbI_Lvl.Visible = false;
                tbI_ChngPass.Visible = false;
                tbI_Hlp.Visible = false;               
            }
            if (ribControl_All.SelectedRibbonTabItem == ribTI_ChngPass)
            {
                tbI_ChngPass.Visible = true;
                tbI_UsM.Visible = false;
                tbI_Lvl.Visible = false;
                tbI_Hlp.Visible = false;               
            }            
            if (ribControl_All.SelectedRibbonTabItem == ribTI_Hlp)
            {
                tbI_Hlp.Visible = true;
                tbI_ChngPass.Visible = false;
                tbI_UsM.Visible = false;
                tbI_Lvl.Visible = false;               
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            lbl_Tme.Text = DateTime.Now.ToShortTimeString();           
            lbl_Dte.Text = SahmsiDate.ShamsiDate;

            ComboLevel();
            ComboUser();
            GridLevel();
            Grid_UserSystem();
            GridHelp();

            lbl_Usr.Text = Login.SetUsr;
            lbl_LvlU.Text = lbl_Lvl.Text = Login.SetTypeLev;
            lbl_LvlUT.Text = Login.SetcomboLvlSys;
        }

        private void ribControl_All_SelectedRibbonTabChanged(object sender, EventArgs e)
        {
            if (ribControl_All.SelectedRibbonTabItem == ribTI_Level)
            {
                tbI_Lvl.Visible = true;
                tbI_UsM.Visible = false;
                tbI_ChngPass.Visible = false;
                tbI_Hlp.Visible = false;                
            }
            if (ribControl_All.SelectedRibbonTabItem == ribTI_UsSys)
            {
                tbI_UsM.Visible = true;
                tbI_Lvl.Visible = false;
                tbI_ChngPass.Visible = false;
                tbI_Hlp.Visible = false;               
            }
            if (ribControl_All.SelectedRibbonTabItem == ribTI_ChngPass)
            {
                tbI_ChngPass.Visible = true;
                tbI_UsM.Visible = false;
                tbI_Lvl.Visible = false;
                tbI_Hlp.Visible = false;              
            }           
            if (ribControl_All.SelectedRibbonTabItem == ribTI_Hlp)
            {
                tbI_Hlp.Visible = true;
                tbI_ChngPass.Visible = false;
                tbI_UsM.Visible = false;
                tbI_Lvl.Visible = false;               
            }
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

        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void ComboLevel()
        {
            try
            {
                con = new Connection();
                SqlCommand cm = new SqlCommand("SELECT new_level FROM access_level", con.GetConnection);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    combo_LvlUsr.Items.Add(dr["new_level"]);
                }
            }
            catch
            {
                MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                errorPro_M.SetError(combo_LvlUsr, "خطا در پر کردن");
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        private void ComboUser()
        {
            try
            {
                con = new Connection();
                SqlCommand cm = new SqlCommand("SELECT sys_username FROM Management_User", con.GetConnection);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    combo_ChPasUsr.Items.Add(dr["sys_username"]);
                }
            }
            catch
            {
                MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                errorPro_M.SetError(combo_ChPasUsr, "خطا در پر کردن");
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////سطح دسترسی///////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegLevel()
        {
            con = new Connection();
            con.GetConnection.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Access_Level", con.GetConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "id");
            DataTable tb = ds.Tables["id"];
            DataRow dr = tb.NewRow();
            dr["new_level"] = txt_Lvl.Text;
            tb.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(ds, "id");
            dtG_Lvl.Refresh();
            MessageBox.Show("ثبت سطح دسترسی با موفقیت انجام شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
            con.GetConnection.Close();
            this.Activate();
        }

        private void Clear_Lvl()
        {
            txt_Lvl.Text = string.Empty;
            errorPro_M.Clear();
            GridLevel();
            dtG_Lvl.Refresh();
            combo_LvlUsr.Items.Clear();
            ComboLevel();
        }

        SqlDataAdapter sda_L;
        DataTable dt_L;
        private void GridLevel()
        {
            try
            {
                con = new Connection();
                con.GetConnection.Open();
                sda_L = new SqlDataAdapter(@"SELECT * FROM Access_Level", con.GetConnection);
                dt_L = new DataTable();
                sda_L.Fill(dt_L);

                dtG_Lvl.DataSource = dt_L;
                dtG_Lvl.Columns[0].Visible = false;
                dtG_Lvl.Columns[1].HeaderText = "سطح دسترسی";
                dtG_Lvl.Columns[1].Width = 250;

                con.GetConnection.Close();
            }
            catch
            {
                MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        private void btn_RegLvl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Lvl.Text))
            {
                errorPro_M.SetError(txt_Lvl, "پر شود");
            }
            else
            {
                try
                {
                    con = new Connection();
                    con.GetConnection.Open();
                    SqlCommand com = new SqlCommand("SELECT * FROM access_level WHERE new_level=@new_level", con.GetConnection);
                    com.Parameters.AddWithValue("@new_level", txt_Lvl.Text);
                    SqlDataReader rd = com.ExecuteReader();
                    if (rd.Read())
                    {
                        errorPro_M.SetError(txt_Lvl, "سطح قبلا ثبت شده است");
                    }
                    else
                    {
                        rd.Close();
                        Clear_Lvl();
                    }
                }
                catch
                {
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    con.GetConnection.Close();
                    Clear_Lvl();
                }
            }
        }

        private void btn_EditLvl_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ویرایش سطح دسترسی فعلا غیر فعال است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
            this.Activate();
        }

        private void dtG_Lvl_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void btn_RefLvl_Click(object sender, EventArgs e)
        {
            Clear_Lvl();
        }
        /// <summary>
        /// ////////////////////////////////////////////////////مشخصات کاربر سیستم////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        SqlDataAdapter sda_Us;        
        private void Grid_UserSystem()
        {
            try
            {
                con = new Connection();
                con.GetConnection.Open();
                sda_Us = new SqlDataAdapter(@"SELECT  M.ID_Sys,P1.new_level,M.sys_user,M.sys_username,M.sys_pass,M.sys_TypLevel FROM Management_User M " +
                    " LEFT JOIN access_level P1 ON  P1.id_level=M.id_level " +
                    "", con.GetConnection);
                DataSet ds = new DataSet();
                sda_Us.Fill(ds);
                dtG_UsrM.DataSource = ds.Tables[0].DefaultView;

                dtG_UsrM.Columns[0].HeaderText = "ID";
                dtG_UsrM.Columns[0].Width = 50;
                dtG_UsrM.Columns[1].HeaderText = "سطح دسترسی";
                dtG_UsrM.Columns[1].Width = 90;
                dtG_UsrM.Columns[2].HeaderText = "نام";
                dtG_UsrM.Columns[2].Width = 85;
                dtG_UsrM.Columns[3].HeaderText = "نام کاربری";
                dtG_UsrM.Columns[3].Width = 85;
                dtG_UsrM.Columns[4].Visible = false;
                dtG_UsrM.Columns[5].HeaderText = "نوع سطح";
                dtG_UsrM.Columns[5].Width = 68;

                con.GetConnection.Close();
            }
            catch
            {
                MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        private void ClearUsr()
        {
            f1.RefUserSys();
            errorPro_M.Clear();
            lbl_IDLvl.Text = "-"; ;
            lbl_IDU.Text = "-";
            txt_Pass.Text = string.Empty;
            txt_NamUsr.Text = string.Empty;
            txt_UsrNam.Text = string.Empty;
            combo_LvlUsr.Items.Clear();
            ComboLevel();
            combo_LvlUsr.SelectedItem = null;
            combo_ChPasUsr.Items.Clear();
            ComboUser();           
            radio_levelTyp1.Checked = false;
            radio_levelTyp2.Checked = false;
            radio_levelTyp3.Checked = false;
            Grid_UserSystem();
            dtG_UsrM.Refresh();
            dtG_UsrM.ClearSelection();
            dtG_UsrM.CurrentCell = null;
            this.Activate();
        }

        private void btn_RegUsr_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(txt_NamUsr.Text)) &&
              (string.IsNullOrEmpty(txt_UsrNam.Text)) &&
              (string.IsNullOrEmpty(txt_Pass.Text)) &&
              (string.IsNullOrEmpty(combo_LvlUsr.Text))                
              )
            {
                this.ValidateChildren();
                errorPro_M.SetError(txt_NamUsr, "پر شود");
                errorPro_M.SetError(txt_UsrNam, "پر شود");
                errorPro_M.SetError(txt_Pass, "پر شود");
                errorPro_M.SetError(combo_LvlUsr, "انتخاب شود");  
            }
            else
            {

                try
                {
                    con = new Connection();
                    con.GetConnection.Open();
                    SqlCommand com = new SqlCommand("SELECT * FROM Management_User WHERE sys_username=@sys_username", con.GetConnection);
                    com.Parameters.AddWithValue("@sys_username", txt_UsrNam.Text);
                    SqlDataReader rd = com.ExecuteReader();
                    if (rd.Read())
                    {
                        MessageBox.Show("نام کاربری قبلا ثبت شده است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                        this.Activate();
                    }
                    else
                    {
                        rd.Close();
                        con.GetConnection.Close();
                        if (radio_levelTyp1.Checked)
                        {
                            con.GetConnection.Open();
                            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Management_User", con.GetConnection);
                            DataSet ds = new DataSet();
                            da.Fill(ds, "id");
                            DataTable tb = ds.Tables["id"];
                            DataRow dr = tb.NewRow();

                            dr["sys_user"] = txt_NamUsr.Text;
                            dr["sys_username"] = txt_UsrNam.Text;
                            dr["sys_pass"] = txt_Pass.Text;
                            dr["id_level"] = Convert.ToInt32(lbl_IDLvl.Text);
                            dr["sys_TypLevel"] = "1";

                            tb.Rows.Add(dr);
                            SqlCommandBuilder cb = new SqlCommandBuilder(da);
                            da.Update(ds, "id");
                            MessageBox.Show("ثبت کاربر با موفقیت انجام شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                            con.GetConnection.Close();
                            ClearUsr();
                            f1.RefUserSys();
                            this.Activate();
                        }
                        else if (radio_levelTyp2.Checked)
                        {
                            con.GetConnection.Open();
                            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Management_User", con.GetConnection);
                            DataSet ds = new DataSet();
                            da.Fill(ds, "id");
                            DataTable tb = ds.Tables["id"];
                            DataRow dr = tb.NewRow();

                            dr["sys_user"] = txt_NamUsr.Text;
                            dr["sys_username"] = txt_UsrNam.Text;
                            dr["sys_pass"] = txt_Pass.Text;
                            dr["id_level"] = Convert.ToInt32(lbl_IDLvl.Text);
                            dr["sys_TypLevel"] = "2";

                            tb.Rows.Add(dr);
                            SqlCommandBuilder cb = new SqlCommandBuilder(da);
                            da.Update(ds, "id");
                            MessageBox.Show("ثبت کاربر با موفقیت انجام شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                            con.GetConnection.Close();
                            ClearUsr();
                            f1.RefUserSys();
                            this.Activate();
                        }
                        else if (radio_levelTyp3.Checked)
                        {
                            con.GetConnection.Open();
                            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Management_User", con.GetConnection);
                            DataSet ds = new DataSet();
                            da.Fill(ds, "id");
                            DataTable tb = ds.Tables["id"];
                            DataRow dr = tb.NewRow();

                            dr["sys_user"] = txt_NamUsr.Text;
                            dr["sys_username"] = txt_UsrNam.Text;
                            dr["sys_pass"] = txt_Pass.Text;
                            dr["id_level"] = Convert.ToInt32(lbl_IDLvl.Text);
                            dr["sys_TypLevel"] = "3";

                            tb.Rows.Add(dr);
                            SqlCommandBuilder cb = new SqlCommandBuilder(da);
                            da.Update(ds, "id");
                            MessageBox.Show("ثبت کاربر با موفقیت انجام شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                            con.GetConnection.Close();
                            ClearUsr();
                            f1.RefUserSys();
                            this.Activate();
                        }
                        else
                        {
                            MessageBox.Show("نوع سطح انتخاب شود", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                            this.Activate();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    Grid_UserSystem();
                    f1.RefUserSys();
                    ClearUsr();
                }
            }
        }

        private void txt_NamUsr_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_M.SetError(tb, "فیلد پر شود");
            }
        }

        private void txt_NamUsr_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_NamUsr.Text))
            {
                errorPro_M.SetError(txt_NamUsr, "فیلد پر شود");
            }
            else
            {
                errorPro_M.Clear();
            }
        }

        private void txt_UsrNam_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_M.SetError(tb, "فیلد پر شود");
            }
        }

        private void txt_UsrNam_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_UsrNam.Text))
            {
                errorPro_M.SetError(txt_UsrNam, "فیلد پر شود");
            }
            else
            {
                errorPro_M.Clear();
            }
        }

        private void txt_Pass_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_M.SetError(tb, "فیلد پر شود");
            }
        }

        private void txt_Pass_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Pass.Text))
            {
                errorPro_M.SetError(txt_Pass, "فیلد پر شود");
            }
            else
            {
                errorPro_M.Clear();
            }
        }

        private void combo_LvlUsr_Validated(object sender, EventArgs e)
        {
            var tb = (ComboBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_M.SetError(tb, "انتخاب شود");
            }
        }

        private void txt_UsrNam_Enter(object sender, EventArgs e)
        {
            Application.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("en-GB"));
        }

        private void txt_UsrNam_Leave(object sender, EventArgs e)
        {
            Application.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("fa-IR"));
        }

        private void btn_RefUsr_Click(object sender, EventArgs e)
        {
            ClearUsr();
        }

        private void btn_DelUsr_Click(object sender, EventArgs e)
        {
            if (lbl_IDU.Text != "-")
            {
                try
                {
                    con = new Connection();
                    cmd_usr = new SqlCommand("DELETE management_user WHERE ID_Sys=@ID_Sys", con.GetConnection);
                    con.GetConnection.Open();
                    cmd_usr.Parameters.AddWithValue("@ID_Sys", lbl_IDU.Text);
                    cmd_usr.ExecuteNonQuery();
                    con.GetConnection.Close();
                    MessageBox.Show("حذف کاربر با موفقیت انجام شد", "اعلام", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    DisplayDataUsr();
                    ClearUsr();
                    con.GetConnection.Close();
                    f1.RefUserSys();
                    this.Activate();
                }
                catch
                {

                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    this.Activate();
                }
            }
            else
            {
                MessageBox.Show("برای حذف لطفا یک رکورد از جدول را انتخاب کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                this.Activate();
            }           
        }

        private void btn_EditUsr_Click(object sender, EventArgs e)
        {           
            //if (toolLbl_UsernamS.Text == "Me.Mehr")
            //{
            //    DataBase dbf = new DataBase();
            //    dbf.ShowDialog();               

            if (lbl_IDU.Text != "-")
            {
                try
                {
                    con = new Connection();
                    con.GetConnection.Open();
                    SqlCommand com = new SqlCommand("UPDATE management_user SET sys_user='" + txt_NamUsr.Text + "' , id_level='" + Convert.ToInt32(lbl_IDLvl.Text) + "' ,  sys_TypLevel='" + lbl_W_levelTyp.Text + "'  WHERE ID_Sys=@ID_Sys ", con.GetConnection);
                    com.Parameters.AddWithValue("@ID_Sys", lbl_IDU.Text);
                    SqlTransaction tr = con.GetConnection.BeginTransaction();
                    com.Transaction = tr;
                    com.ExecuteNonQuery();
                    com.Dispose();
                    tr.Commit();

                    Grid_UserSystem();
                    dtG_UsrM.Refresh();
                    MessageBox.Show(",ویرایش با موفقیت انجام شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f1.RefUserSys();
                    this.Activate();
                }
                catch
                {
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    con.GetConnection.Close();
                    f1.RefUserSys();
                }
                }
                else
                {
                    MessageBox.Show("جهت ویرایش یک ردیف انتخاب کنید", "آگاهی", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Activate();
                }
            //}
            //else
            //{
            //    MessageBox.Show("شما دسترسی لازم را ندارید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            //}
        }

        private void dtG_UsrM_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                lbl_IDU.Text = Convert.ToString(dtG_UsrM.Rows[e.RowIndex].Cells[0].Value.ToString());
                combo_LvlUsr.Text = (dtG_UsrM.Rows[e.RowIndex].Cells[1].Value.ToString());
                txt_NamUsr.Text = dtG_UsrM.Rows[e.RowIndex].Cells[2].Value.ToString();
                txt_UsrNam.Text = dtG_UsrM.Rows[e.RowIndex].Cells[3].Value.ToString();
                lbl_W_levelTyp.Text = dtG_UsrM.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            catch
            {
                MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }

            if (lbl_W_levelTyp.Text == "1")
            {
                radio_levelTyp1.Checked = true;
            }
            if (lbl_W_levelTyp.Text == "2")
            {
                radio_levelTyp2.Checked = true;
            }
            if (lbl_W_levelTyp.Text == "3")
            {
                radio_levelTyp3.Checked = true;
            }
        }

        SqlDataAdapter adapt_usr;
        SqlCommand cmd_usr;       
        private void DisplayDataUsr()
        {
            con = new Connection();
            con.GetConnection.Open();
            DataTable dt = new DataTable();
            adapt_usr = new SqlDataAdapter("SELECT * FROM setting_receive", con.GetConnection);
            adapt_usr.Fill(dt);
            dtG_UsrM.DataSource = dt;
            con.GetConnection.Close();
            this.Activate();
        }
        
        /// <summary>
        /// /////////////////////////////////////////////////////تغییر رمز عبور/////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ChangPass_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ChPasOP.Text) && (string.IsNullOrEmpty(txt_ChPasNP.Text)) && (string.IsNullOrEmpty(combo_ChPasUsr.Text)))
            {
                errorPro_M.SetError(txt_ChPasNP, "پر شود");
                errorPro_M.SetError(txt_ChPasOP, "پر شود");
                errorPro_M.SetError(combo_ChPasUsr, "انتخاب شود");  
            }
            else
            {
                try
                {
                    con = new Connection();
                    con.GetConnection.Open();
                    SqlCommand com = new SqlCommand("UPDATE management_user SET sys_pass='" + txt_ChPasNP.Text + "' WHERE sys_username=@sys_username and sys_pass=@sys_pass", con.GetConnection);
                    com.Parameters.AddWithValue("@sys_username", combo_ChPasUsr.Text);
                    com.Parameters.AddWithValue("@sys_pass", txt_ChPasOP.Text);
                    SqlDataReader rd = com.ExecuteReader();
                    if (rd.Read())
                    {
                        SqlTransaction tr = con.GetConnection.BeginTransaction();
                        com.Transaction = tr;
                        com.ExecuteNonQuery();
                        com.Dispose();
                        tr.Commit();
                        MessageBox.Show("رمز عبور با موفقیت تغییر کرد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                        this.Activate();
                    }
                    else
                    {
                        MessageBox.Show("کاربر موجود نیست", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                        this.Activate();
                    }
                }
                catch
                {
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    txt_ChPasNP.Text = string.Empty;
                    txt_ChPasOP.Text = string.Empty;
                    combo_ChPasUsr.SelectedItem = string.Empty;
                    errorPro_M.Clear();
                    con.GetConnection.Close();
                }
            }
        }

        private void combo_ChPasUsr_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// ////////////////////////////////////////راهنما///////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        SqlDataAdapter sda_Hlp;
        DataTable dt_Hlp;
        private void GridHelp()
        {
            try
            {
                con = new Connection();
                con.GetConnection.Open();
                sda_Hlp = new SqlDataAdapter(@"SELECT * FROM Help", con.GetConnection);
                dt_Hlp = new DataTable();
                sda_Hlp.Fill(dt_Hlp);

                dtG_Hlp.DataSource = dt_Hlp;
                dtG_Hlp.Columns[0].HeaderText = "ID";
                dtG_Hlp.Columns[0].Width = 50;
                dtG_Hlp.Columns[1].HeaderText = "راهنمای گزارش";
                dtG_Hlp.Columns[1].Width = 220;
                dtG_Hlp.Columns[2].HeaderText = "راهنمای شارژ";
                dtG_Hlp.Columns[2].Width = 220;

                con.GetConnection.Close();
            }
            catch
            {
                MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        private void Reg_RepHelp()
        {
            con = new Connection();
            con.GetConnection.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Help", con.GetConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "id");
            DataTable tb = ds.Tables["id"];
            DataRow dr = tb.NewRow();

            dr["HelpRep"] = rich_HlpRep.Text;
            dr["HelpSharg"] = rich_HlpSh.Text;

            tb.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(ds, "id");
            MessageBox.Show("ثبت راهنما با موفقیت انجام شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
            dtG_Hlp.Refresh();
            con.GetConnection.Close();
            this.Activate();
        }

        private void ClearHlp()
        {
            rich_HlpRep.Text = string.Empty;
            rich_HlpSh.Text = string.Empty;
            GridHelp();
            dtG_Hlp.Refresh();
        }

        private void btn_RegHlp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rich_HlpRep.Text))
            {
                MessageBox.Show("متن راهنما پر شود", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
            else
            {
                try
                {
                    Reg_RepHelp();
                    ClearHlp();
                }
                catch
                {
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    ClearHlp();
                }
            }
        }

        private void btn_EditHlp_Click(object sender, EventArgs e)
        {
            if ((lbl_IDHlp.Text != "-"))
            {
                try
                {
                    con = new Connection();
                    con.GetConnection.Open();
                    SqlCommand com = new SqlCommand("UPDATE Help SET HelpRep='" + rich_HlpRep.Text + "' , HelpSharg='" + rich_HlpSh.Text + "' WHERE id_Help=@id_Help ", con.GetConnection);
                    com.Parameters.AddWithValue("@id_Help", lbl_IDHlp.Text);
                    SqlTransaction tr = con.GetConnection.BeginTransaction();
                    com.Transaction = tr;
                    com.ExecuteNonQuery();
                    com.Dispose();
                    tr.Commit();

                    ClearHlp();

                    MessageBox.Show(".ویرایش راهنما با موفقیت انجام شد", "آگاهی", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Activate();
                }
                catch
                {
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    con.GetConnection.Close();
                }
            }
            else
            {
                MessageBox.Show("جهت ویرایش یک ردیف انتخاب کنید", "آگاهی", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Activate();
            }
        }

        private void btn_RefHlp_Click(object sender, EventArgs e)
        {
            ClearHlp();
        }

        private void dtG_Hlp_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                lbl_IDHlp.Text = dtG_Hlp.Rows[e.RowIndex].Cells[0].Value.ToString();
                rich_HlpRep.Text = dtG_Hlp.Rows[e.RowIndex].Cells[1].Value.ToString();
                rich_HlpSh.Text = dtG_Hlp.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch
            {
                MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
        }

        private void combo_LvlUsr_SelectedIndexChanged(object sender, EventArgs e)
        {
            con = new Connection();
            con.GetConnection.Open();
            try
            {

                SqlCommand com = new SqlCommand("SELECT * FROM access_level WHERE new_level=@new_level ", con.GetConnection);
                com.Parameters.AddWithValue("@new_level", combo_LvlUsr.SelectedItem);
                SqlDataReader rd = com.ExecuteReader();
                if (rd.Read())
                {
                    lbl_IDLvl.Text = Convert.ToString(rd["id_level"]);
                }
                rd.Close();
            }
            catch
            {
                //MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                //this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
       
        private void txt_NamUsr_Leave(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("en-GB"));
        }

        private void txt_NamUsr_Enter(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("fa-IR"));
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
       
        
        

        

       

        





    }
}
