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
    public partial class NewWizardParkban : Form
    {
        Connection con;
        [DllImport("wininet.dll")]///پیغام قطع/ وصلی نت
        private extern static bool InternetGetConnectedState(out int Conn, int val);///پیغام قطع/ وصلی نت
        
        public NewWizardParkban()
        {
            InitializeComponent();
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("fa-IR"));
            txt_Nam.Validated += new EventHandler(txt_Nam_Validated);
            txt_Famil.Validated += new EventHandler(txt_Famil_Validated);
            txt_Melli.Validated += new EventHandler(txt_Melli_Validated);
            txt_Mob.Validated += new EventHandler(txt_Mob_Validated);

            txt_Phon.Validated += new EventHandler(txt_Phon_Validated);
            txt_Acc.Validated += new EventHandler(txt_Acc_Validated);
            txt_Addrs.Validated += new EventHandler(txt_Addrs_Validated);
            txt_Pass.Validated += new EventHandler(txt_Pass_Validated);
            txt_Usr.Validated += new EventHandler(txt_Usr_Validated);
        }

        public Form1 f1;
        public NewWizardParkban(Form1 f1)
            : this()
        {
            this.f1 = f1;
        }

        private void NewWizardParkban_Load(object sender, EventArgs e)
        {
            lbl_UsrSys.Text = Login.SetUsr;
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

            Combo_degreePark();        
            //EndDate_List();
            ListDeviceUser();
            ListUserPark();
            ListNoDevice();
            UserRemain();          
            //Fill_combo_device_Park();
            Fill_Check_deliv();
            Fill_Check_Receve();
            txt_Nam.Focus();
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;           
        }

        /// <summary>
        /// /////////////////////////////////////////////تابع عدد////////////////////////////////////       
        /// </summary>
        private bool Num(string str)
        {
            if (str != string.Empty)
            {
                foreach (char c in str)
                {
                    if (!char.IsDigit(c))
                        return false;
                }
                return true;
            }
            else
                return false;
        }
        ///////////////////////////////////////////////////////////////////////////////////////

        private void Combo_degreePark()////////////پر کردن کومبو باکس  تحصیلات پارکبان
        {
            ArrayList list = new ArrayList();
            ArrayList list1 = list;
            list1.Add("زیردیپلم");
            list1.Add("دیپلم");
            list1.Add("فوق دیپلم");
            list1.Add("کارشناسی");
            list1.Add("کارشناسی ارشد و بالاتر");
            list1 = null;
            combo_Dgre.DataSource = list;
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

        private void timer_Clock_Tick(object sender, EventArgs e)
        {
            lbl_Tme.Text = DateTime.Now.ToShortTimeString();
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        DataTable dt_lstUsr, dt_lstWrk;
        private void ListDeviceUser()///////////پر کردن کل شماره سیم کارت از جدل دستگاه
        {
            try
            {
                con = new Connection();
                con.GetConnection.Open();
                SqlCommand cm = new SqlCommand("SELECT sim_no FROM device", con.GetConnection);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                dt_lstUsr = new DataTable();
                da.Fill(dt_lstUsr);

                foreach (DataRow dr in dt_lstUsr.Rows)
                {
                    list_Usr.Items.Add(dr["sim_no"]);
                }
            }
            catch
            {
                MessageBox.Show("خطا در کل سیم کارتهای", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                lbl_WUsr.Text = "خطا در کل سیم کارتهای ";
                txt_Nam.Focus();
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        DataTable dt_lstNoDi;
        private void ListNoDevice()
        {
            try
            {
                con = new Connection();
                con.GetConnection.Open();
                SqlCommand cm = new SqlCommand("SELECT device_no FROM device", con.GetConnection);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                dt_lstNoDi = new DataTable();
                da.Fill(dt_lstNoDi);

                foreach (DataRow dr in dt_lstNoDi.Rows)
                {
                    list_NoDiv.Items.Add(dr["device_no"]);
                }
            }
            catch
            {
                MessageBox.Show("خطا در کل شماره گوشی ها", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                txt_Nam.Focus();
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        private void ListUserPark()/////////پر کردن سیم کارتهای ثبت شده با عنوان نام کاربری برای پارکبان
        {
            try
            {
                SqlCommand cm = new SqlCommand("SELECT  D1.sim_no FROM Parkban_Property P1 " +
                        " LEFT JOIN Device D1 ON  D1.ID_device=P1.ID_device " +
                        "", con.GetConnection);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                dt_lstWrk = new DataTable();
                da.Fill(dt_lstWrk);

                foreach (DataRow dr in dt_lstWrk.Rows)
                {
                    list_Wrk.Items.Add(dr["sim_no"]);
                }
            }
            catch
            {
                MessageBox.Show("خطا درسیم کارتهای ثبت شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                lbl_WUsr.Text = "خطا درسیم کارتهای ثبت شده";
                txt_Nam.Focus();
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        private void UserRemain()/////////////////سیم کارتهای مانده
        {
            try
            {
                con = new Connection();
                con.GetConnection.Open();
                SqlCommand cm = new SqlCommand("SELECT sim_no FROM  Device WHERE ID_device NOT IN (SELECT ID_device FROM Parkban_Property )", con.GetConnection);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                dt_lstWrk = new DataTable();
                da.Fill(dt_lstWrk);

                foreach (DataRow dr in dt_lstWrk.Rows)
                {
                    combo_UsrSimN.Items.Add(dr["sim_no"]);
                }
            }
            catch
            {
                MessageBox.Show("خطا در جدول شماره های خالی", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                lbl_WUsr.Text = "خطا در جدول شماره های خالی";
                txt_Nam.Focus();
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>           
       
        private void Fill_Check_deliv()
        {
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Setting_Delive", con.GetConnection);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    checkL_Dlv.Items.Add(dt.Rows[i]["sett_delive"].ToString());
                }
            }
            catch
            {
                MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                lbl_WCD.Text = "خطادربرقراری ارتباط";
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        private void Fill_Check_Receve()
        {
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Setting_Receive", con.GetConnection);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    checkL_Rcv.Items.Add(dt.Rows[i]["sett_receiv"].ToString());
                }
            }
            catch
            {
                MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                lbl_WCR.Text = "خطادربرقراری ارتباط";
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }     

        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Register_Parkban()
        {
            con = new Connection();
            con.GetConnection.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Parkban_Property", con.GetConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "id");
            DataTable tb = ds.Tables["id"];
            DataRow dr = tb.NewRow();

            dr["Name"] = txt_Nam.Text + " " + txt_Famil.Text;//1
            //dr["Family"] = txt_Famil.Text;//2
            dr["Melii"] = txt_Melli.Text;//3
            dr["Mobile"] = txt_Mob.Text;//4
            dr["Phone"] = txt_Phon.Text;//5
            dr["Account"] = txt_Acc.Text;//6
            dr["Degree"] = combo_Dgre.SelectedItem;//7            
            if (string.IsNullOrEmpty(bCT_StrtDte.Text))
            {
                dr["Str_Dte"] = lbl_Dte.Text;//8
            }
            else
            {
                dr["Str_Dte"] = bCT_StrtDte.Text;
            }
            //dr["UserName"] = txt_Usr.Text;//9
            dr["Pass"] = txt_Pass.Text;//10
            //dr["No_Device"] = combo_NoDv.SelectedItem;//11
            dr["Addrs"] = txt_Addrs.Text;//12
            dr["Desc_P"] = rich_Dsc.Text;//13
            dr["Deliv"] = txt_CDlv.Text;///تحویلی14
            dr["Receiv"] = txt_CRcv.Text;///دریافتی15
            dr["UsSys_P"] = lbl_UsrSys.Text;//16

            dr["ID_device"] = lbl_IDDvc.Text;//16

            tb.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(ds, "id");
            con.GetConnection.Close();           
        }

        private void ClearItem()
        {
            lbl_AccS3.Text = string.Empty;
            txt_Acc.Text = string.Empty;
            lbl_AddrS3.Text = string.Empty;
            txt_Addrs.Text = string.Empty;
            lbl_DgrS3.Text = string.Empty;
            //combo_Dgre.SelectedItem = string.Empty;//3
            lbl_DscS3.Text = string.Empty;
            rich_Dsc.Text = string.Empty; ;//4
            lbl_FamilS3.Text = string.Empty;
            txt_Famil.Text = string.Empty; //5
            lbl_MelliS3.Text = string.Empty;
            txt_Melli.Text = string.Empty;//6
            lbl_MobS3.Text = string.Empty;
            txt_Mob.Text = string.Empty;//7
            lbl_NamS3.Text = string.Empty;
            txt_Nam.Text = string.Empty;//8
            lbl_PhonS3.Text = string.Empty;
            txt_Phon.Text = string.Empty;//9
            lbl_UsrS3.Text = string.Empty;
            txt_Usr.Text = string.Empty;
            lbl_NoDvS3.Text = string.Empty;
            combo_Dgre.SelectedIndex = 0;
            lbl_PassS3.Text = string.Empty;
            txt_Pass.Text = string.Empty;
            bCT_StrtDte.Text = string.Empty;

            lbl_AccS2.Text = string.Empty;//1
            lbl_AddrS2.Text = string.Empty;//2
            lbl_DgrS2.Text = string.Empty; //3
            lbl_DscS2.Text = string.Empty;//4
            lbl_FamilS2.Text = string.Empty;//5
            lbl_MelliS2.Text = string.Empty;//6
            lbl_MobS2.Text = string.Empty;//7
            lbl_NamS2.Text = string.Empty;//8
            lbl_PhonS2.Text = string.Empty;//9

            checkL_Rcv.ClearSelected();
            checkL_Dlv.ClearSelected();
            check_AllRcv.Checked = false;
            check_AllDlv.Checked = false;
            txt_CRcv.Text = string.Empty;
            txt_CDlv.Text = string.Empty;
            errorPro_NWP.Clear();
            for (int i = 0; i < checkL_Rcv.Items.Count; i++)
            {
                checkL_Rcv.SetItemChecked(i, false);
            }
            for (int i = 0; i < checkL_Dlv.Items.Count; i++)
            {
                checkL_Dlv.SetItemChecked(i, false);
            }
            combo_UsrSimN.Items.Clear();
            UserRemain();
            list_Wrk.Items.Clear();
            ListUserPark();
            //txt_Nam.Focus();           
            this.Activate();
        }                 
       /// <summary>
       /// ////////////////////////////////////////////////////////
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void btn_Next1_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(txt_Nam.Text)) &&
                (string.IsNullOrEmpty(txt_Famil.Text)) &&
                (string.IsNullOrEmpty(txt_Melli.Text)) &&
                (string.IsNullOrEmpty(txt_Phon.Text)) &&
                (string.IsNullOrEmpty(txt_Mob.Text)) &&
                (string.IsNullOrEmpty(txt_Acc.Text)) &&
                (string.IsNullOrEmpty(txt_Addrs.Text)) &&
                (string.IsNullOrEmpty(combo_Dgre.SelectedText))
                )
            {
                this.ValidateChildren();
                errorPro_NWP.SetError(txt_Nam, "پرشود");
                errorPro_NWP.SetError(txt_Famil, "پرشود");
                errorPro_NWP.SetError(txt_Melli, "پرشود");
                errorPro_NWP.SetError(txt_Phon, "پرشود");
                errorPro_NWP.SetError(txt_Mob, "پرشود");
                errorPro_NWP.SetError(txt_Acc, "پرشود");
                errorPro_NWP.SetError(txt_Addrs, "پرشود");
                //errorPro_NWP.SetError(combo_Dgre, "پرشود");                
            }
            else if ((string.IsNullOrEmpty(txt_Nam.Text)))
            {
                errorPro_NWP.SetError(txt_Nam, "پرشود");
            }
            else if ((string.IsNullOrEmpty(txt_Famil.Text)))
            {
                errorPro_NWP.SetError(txt_Famil, "پرشود");
            }
            else if ((string.IsNullOrEmpty(txt_Melli.Text)))
            {
                errorPro_NWP.SetError(txt_Melli, "پرشود");
            }
            else if ((string.IsNullOrEmpty(txt_Phon.Text)))
            {
                errorPro_NWP.SetError(txt_Phon, "پرشود");
            }
            else if ((string.IsNullOrEmpty(txt_Mob.Text)))
            {
                errorPro_NWP.SetError(txt_Mob, "پرشود");
            }
            else if ((string.IsNullOrEmpty(txt_Acc.Text)))
            {
                errorPro_NWP.SetError(txt_Acc, "پرشود");
            }
            else if ((string.IsNullOrEmpty(txt_Addrs.Text)))
            {
                errorPro_NWP.SetError(txt_Addrs, "پرشود");
            }

            else
            {
                try
                {
                    con = new Connection();
                    con.GetConnection.Open();
                    SqlCommand com = new SqlCommand("SELECT * FROM Parkban_Property WHERE Melii=@Melii ", con.GetConnection);
                    com.Parameters.AddWithValue("@Melii", txt_Melli.Text);
                    SqlDataReader rd = com.ExecuteReader();
                    if (rd.Read())
                    {
                        MessageBox.Show("شماره ملی تکراری است", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                        errorPro_NWP.SetError(txt_Melli, "شماره ملی تکراری است");
                        txt_Melli.Focus();
                        NewWizardParkban nwp = new NewWizardParkban();
                        nwp.Activate();
                        this.Activate();
                    }

                    else
                    {
                        rd.Close();
                        panel2.Visible = true;
                        
                        lbl_AccS2.Text = txt_Acc.Text;//1
                        lbl_AddrS2.Text = txt_Addrs.Text;//2
                        lbl_DgrS2.Text = Convert.ToString(combo_Dgre.SelectedItem);//3
                        lbl_DscS2.Text = rich_Dsc.Text;//4
                        lbl_FamilS2.Text = txt_Famil.Text;//5
                        lbl_MelliS2.Text = txt_Melli.Text;//6
                        lbl_MobS2.Text = txt_Mob.Text;//7
                        lbl_NamS2.Text = txt_Nam.Text;//8
                        lbl_PhonS2.Text = txt_Phon.Text;//9

                        if (string.IsNullOrEmpty(bCT_StrtDte.Text))
                        {
                            lbl_DteS2.Text = lbl_Dte.Text;
                        }
                        else
                        {
                            lbl_DteS2.Text = bCT_StrtDte.Text;
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
                    con.GetConnection.Close();
                }
            }
        }

        private void txt_Nam_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Nam.Text))
            {
                errorPro_NWP.SetError(txt_Nam, "فیلد پر شود");
            }
            else
            {
                errorPro_NWP.Clear();
            }
        }

        private void txt_Nam_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_NWP.SetError(tb, "فیلد پر شود");
            }     
        }

        private void txt_Famil_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_NWP.SetError(tb, "فیلد پر شود");
            }    
        }

        private void txt_Famil_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Famil.Text))
            {
                errorPro_NWP.SetError(txt_Famil, "پرشود");
            }
            else
            {
                errorPro_NWP.Clear();
            }
        }

        private void txt_Melli_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_NWP.SetError(tb, "فیلد پر شود");
            }

            if (!Num(txt_Melli.Text))
            {
                errorPro_NWP.SetError(txt_Melli, "عدد وارد شود");
            }  
        }

        private void txt_Melli_TextChanged(object sender, EventArgs e)
        {
            if (!Num(txt_Melli.Text))
            {
                errorPro_NWP.SetError(txt_Melli, "عدد وارد شود");
            }
            else
            {
                errorPro_NWP.Clear();
            }
        }

        private void txt_Mob_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_NWP.SetError(tb, "فیلد پر شود");
            }
            if (!Num(txt_Mob.Text))
            {
                errorPro_NWP.SetError(txt_Mob, "عدد وارد شود");
            } 
        }

        private void txt_Mob_TextChanged(object sender, EventArgs e)
        {
            if (!Num(txt_Mob.Text))
            {
                errorPro_NWP.SetError(txt_Mob, "عدد وارد شود");
            }
            else
            {
                errorPro_NWP.Clear();
            }
        }

        private void txt_Phon_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_NWP.SetError(tb, "فیلد پر شود");
            }
            if (!Num(txt_Phon.Text))
            {
                errorPro_NWP.SetError(txt_Phon, "عدد وارد شود");
            } 
        }

        private void txt_Phon_TextChanged(object sender, EventArgs e)
        {
            if (!Num(txt_Phon.Text))
            {
                errorPro_NWP.SetError(txt_Phon, "عدد وارد شود");
            }
            else
            {
                errorPro_NWP.Clear();
            }
        }

        private void txt_Acc_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_NWP.SetError(tb, "فیلد پر شود");
            }   
        }

        private void txt_Acc_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Acc.Text))
            {
                errorPro_NWP.SetError(txt_Acc, "پرشود");
            }
            else
            {
                errorPro_NWP.Clear();
            }
        }

        private void txt_Addrs_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_NWP.SetError(tb, "فیلد پر شود");
            }  
        }

        private void txt_Addrs_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Addrs.Text))
            {
                errorPro_NWP.SetError(txt_Addrs, "پرشود");
            }
            else
            {
                errorPro_NWP.Clear();
            }
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Next2_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(combo_UsrSimN.SelectedText)) &&
                (string.IsNullOrEmpty(txt_Pass.Text)) &&
                (string.IsNullOrEmpty(txt_Usr.Text))
               )
            {
                this.ValidateChildren();
                errorPro_NWP.SetError(combo_UsrSimN, "انتخاب شود");
                errorPro_NWP.SetError(txt_Pass, "پرشود");
                errorPro_NWP.SetError(txt_Usr, "پرشود");
            }
            else if ((string.IsNullOrEmpty(txt_Pass.Text)))
            {
                errorPro_NWP.SetError(txt_Pass, "پرشود");
            }
            else if ((string.IsNullOrEmpty(txt_Usr.Text)))
            {
                errorPro_NWP.SetError(txt_Usr, "برای پر شدن یک شماره از لیست پایین انتخاب کنید");
            }
            else
            {
                try
                {
                    con = new Connection();
                    con.GetConnection.Open();


                    SqlCommand com1 = new SqlCommand("SELECT * FROM Parkban_Property WHERE ID_device=@ID_device AND End_Dte IS NULL", con.GetConnection);
                    com1.Parameters.AddWithValue("@ID_device", txt_Usr.Text);
                    SqlDataReader rd1 = com1.ExecuteReader();
                    if (rd1.Read())
                    {
                        MessageBox.Show("این نام کاربری قبلا ثبت شده است", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                        errorPro_NWP.SetError(txt_Usr, "نام کاربری قبلا ثبت شده است");
                        this.Activate();
                    }

                        //and End_Dte IS NULL

                    else
                    {
                        rd1.Close();
                        panel3.Visible = true;
                        lbl_AccS3.Text = txt_Acc.Text;//1
                        lbl_AddrS3.Text = txt_Addrs.Text;//2
                        lbl_DgrS3.Text = Convert.ToString(combo_Dgre.SelectedItem);//3
                        lbl_DscS3.Text = rich_Dsc.Text;//4
                        lbl_FamilS3.Text = txt_Famil.Text;//5
                        lbl_MelliS3.Text = txt_Melli.Text;//6
                        lbl_MobS3.Text = txt_Mob.Text;//7
                        lbl_NamS3.Text = txt_Nam.Text;//8
                        lbl_PhonS3.Text = txt_Phon.Text;//9
                        lbl_UsrS3.Text = txt_Usr.Text;
                        lbl_PassS3.Text = txt_Pass.Text;
                        if (string.IsNullOrEmpty(bCT_StrtDte.Text))
                        {
                            lbl_DteS3.Text = lbl_Dte.Text;
                        }
                        else
                        {
                            lbl_DteS3.Text = bCT_StrtDte.Text;
                        }
                    }
                }
                catch
                {
                    //Validation_text_regpark();
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                finally
                {
                    con.GetConnection.Close();
                }
            }
        }

        private void btn_Prev2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
        }

        private void combo_UsrSimN_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Usr.Text = combo_UsrSimN.Text;
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Device WHERE sim_no=@sim_no ", con.GetConnection);
                com.Parameters.AddWithValue("@sim_no", combo_UsrSimN.SelectedItem);
                SqlDataReader rd = com.ExecuteReader();
                if (rd.Read())
                {
                    lbl_IDDvc.Text = Convert.ToString(rd["ID_device"]);
                    lbl_NoSimS.Text = Convert.ToString(rd["device_no"]);
                }
                else
                {
                    rd.Close();
                    MessageBox.Show("رکوردی یافت نشد ", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    txt_Nam.Focus();
                    this.Activate();
                }
            }
            catch
            {
                MessageBox.Show("خطا در فیلد شماره گوشی ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                txt_Nam.Focus();
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        private void txt_Usr_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_NWP.SetError(tb, "فیلد پر شود");
            }    
        }

        private void txt_Usr_TextChanged(object sender, EventArgs e)
        {
            string myString = txt_Usr.Text;
            int index = list_Usr.FindString(myString, -1);
            if (index != -1)
            {
                list_Usr.SetSelected(index, true);
            }
            else
            {
                MessageBox.Show("آیتم پیدا نشد!");
                this.Activate();
            }       
        }

        private void txt_Pass_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_NWP.SetError(tb, "فیلد پر شود");
            }     
        }

        private void txt_Pass_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Pass.Text))
            {
                errorPro_NWP.SetError(txt_Pass, "پرشود");
            }
            else
            {
                errorPro_NWP.Clear();
            }
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void check_AllDlv_CheckedChanged(object sender, EventArgs e)
        {
             if (check_AllDlv.Checked)
             {
                 txt_CDlv.Text = string.Empty;
                 string s = null;
                 for (int i = 0; i <= (checkL_Dlv.Items.Count - 1); i++)
                 {
                     checkL_Dlv.SetItemChecked(i, true);
                     s += checkL_Dlv.Items[i].ToString() + "/";
                 }
                 txt_CDlv.Text = s;
             }
             else
             {
                 for (int i = 0; i <= (checkL_Dlv.Items.Count - 1); i++)
                 {
                     checkL_Dlv.SetItemChecked(i, false);
                     txt_CDlv.Text = string.Empty;
                 }
             }
        }

        private void check_AllRcv_CheckedChanged(object sender, EventArgs e)
        {
            if (check_AllRcv.Checked)
            {
                txt_CRcv.Text = string.Empty;
                string s = null;
                for (int i = 0; i <= (checkL_Rcv.Items.Count - 1); i++)
                {
                    checkL_Rcv.SetItemChecked(i, true);
                    s += checkL_Rcv.Items[i].ToString() + "/";
                }
                txt_CRcv.Text = s;
            }
            else
            {
                for (int i = 0; i <= (checkL_Rcv.Items.Count - 1); i++)
                {
                    checkL_Rcv.SetItemChecked(i, false);
                    txt_CRcv.Text = string.Empty;
                }
            }
        }

        private void checkL_Dlv_Validated(object sender, EventArgs e)
        {
            if (this.checkL_Dlv.SelectedIndex == -1)
            {
                errorPro_NWP.SetError(checkL_Dlv, "نیاز به انتخاب حداقل یک فیلد میباشد");
                //e.Cancel = true;
            }
            else
                errorPro_NWP.SetError(checkL_Dlv, ""); 
        }

        private void checkL_Rcv_Validated(object sender, EventArgs e)
        {
            if (this.checkL_Rcv.SelectedIndex == -1)
            {
                errorPro_NWP.SetError(checkL_Rcv, "نیاز به انتخاب حداقل یک فیلد میباشد");
                //e.Cancel = true;
            }
            else
                errorPro_NWP.SetError(checkL_Rcv, "");
        }

        private void checkL_Dlv_SelectedIndexChanged(object sender, EventArgs e)
        {
            string checkedItems = string.Empty;
            int i = 0;
            foreach (object obj in checkL_Dlv.CheckedItems)
            {
                checkedItems = checkedItems + obj.ToString() + "/";
                i++;
            }
            //checkedItems = checkedItems.Substring(1, checkedItems.Length - 1);
            txt_CDlv.Text = checkedItems;
        }

        private void checkL_Rcv_SelectedIndexChanged(object sender, EventArgs e)
        {
            string checkedItems = string.Empty;
            int i = 0;
            foreach (object obj in checkL_Rcv.CheckedItems)
            {
                checkedItems = checkedItems + obj.ToString() + "/";
                i++;
            }
            //checkedItems = checkedItems.Substring(1, checkedItems.Length - 1);
            txt_CRcv.Text = checkedItems;
        }

        private void btn_Reg3_Click(object sender, EventArgs e)
        {
            if (lbl_IDDvc.Text == "-")
            {
                MessageBox.Show("شماره سیم کارت را به عنوان نام کاربری انتخاب نکرده اید به مرحله قبل باز گشته و انتخاب نمایید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                this.Activate();
            }
            else
            {

                if ((checkL_Dlv.SelectedIndex == -1) &&
                   (checkL_Rcv.SelectedIndex == -1) &&
                    (string.IsNullOrEmpty(txt_CDlv.Text)) &&
                    (string.IsNullOrEmpty(txt_CRcv.Text))
                   )
                {
                    errorPro_NWP.SetError(checkL_Dlv, "پرشود");
                    errorPro_NWP.SetError(checkL_Rcv, "پرشود");
                    errorPro_NWP.SetError(txt_CDlv, "پرشود");
                    errorPro_NWP.SetError(txt_CRcv, "پرشود");
                }
                else
                {
                    //try
                    //{
                    Register_Parkban();
                    MessageBox.Show("ثبت با موفقیت انجام شد", "اعلام", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    panel3.Visible = false;
                    panel2.Visible = false;
                    ClearItem();
                    f1.RefGWPark();
                    txt_Nam.Focus();
                    this.Activate();
                    //}
                    //catch
                    //{
                    //    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                    //}
                }
            }
        }

        private void btn_Perv3_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel3.Visible = false;
        }

        private void btn_HmPg_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel2.Visible = false;
        }

      
        

        

        

       
       
       
       

       

       

        

        

        
        

        








    }
}
