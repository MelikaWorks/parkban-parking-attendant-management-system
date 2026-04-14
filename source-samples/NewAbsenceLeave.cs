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
    public partial class NewAbsenceLeave : Form
    {
        Connection con;
        [DllImport("wininet.dll")]///پیغام قطع/ وصلی نت
        private extern static bool InternetGetConnectedState(out int Conn, int val);///پیغام قطع/ وصلی نت
                                                                                    ///
        public NewAbsenceLeave()
        {
            InitializeComponent();
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("fa-IR"));
        }

        private void NewAbsenceLeave_Load(object sender, EventArgs e)
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            lbl_Tme.Text = DateTime.Now.ToShortTimeString();          
            lbl_Dte.Text = SahmsiDate.ShamsiDate;

            NamALShow();
            NamePresent();
            StreALShow();
            GridAbsLve();
            NameAll();
            lbl_Usr.Text = Login.SetUsr;
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
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        int idp1 = 0;
        private void UserNameShow()
        {
            con = new Connection();
            con.GetConnection.Open();
            try
            {

                SqlCommand com = new SqlCommand("select * from parkban_property where Name=@Name ", con.GetConnection);
                com.Parameters.AddWithValue("@Name", lbl_NameS.Text);
                SqlDataReader rd = com.ExecuteReader();
                if (rd.Read())
                {                   
                    idp1 = Convert.ToInt32(rd["ID_Park"]);
                    lbl_IDPark.Text = Convert.ToString(idp1);
                }
                rd.Close();
            }
            catch
            {
                MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
            }
            finally
            {
                con.GetConnection.Close();
            }
        }
        
        SqlDataAdapter sdt_;       
        private void GridAbsLve()
        {
            try
            {
                con = new Connection();
                con.GetConnection.Open();
                con = new Connection();
                con.GetConnection.Open();
                sdt_ = new SqlDataAdapter(@"SELECT  P1.Name,Abs_Lve,date_AL FROM AbsentLeave AL " +                    
                    " LEFT JOIN Parkban_Property P1 ON  P1.ID_Park=AL.ID_Park " +                   
                    "", con.GetConnection);
                DataSet ds = new DataSet();
                sdt_.Fill(ds);
                dtG_.DataSource = ds.Tables[0].DefaultView;
                
                dtG_.Columns[0].HeaderText = "نام پارکبان";
                dtG_.Columns[0].Width = 90;
                dtG_.Columns[1].HeaderText = "حضوری";
                dtG_.Columns[1].Width = 110;
                dtG_.Columns[2].HeaderText = "تاریخ";
                dtG_.Columns[2].Width = 100;              
            }
            catch
            {
                lbl_W.Text = "خطا در پر کردن جدول لوحه";
            }
            finally
            {
                con.GetConnection.Close();
            }
        }
        
        DataTable dt_lstWrk;
        SqlDataAdapter da1;
        private void NamALShow()
        {
           
        }
       
        private void btn_Show_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(bcal.Text))
            {
                MessageBox.Show("تاریخ را وارد کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
            else
            {
                try
                {
                    int y = 0;
                    var list1 = Enumerable.Intersect(list_Name.Items.Cast<string>().ToArray(), list_NamPrsnt.Items.Cast<string>().ToArray());
                    for (int i = 0; i < list_Name.Items.Count; i++)
                    {
                        if (list1.Contains(list_Name.Items[i]))
                        {
                            int a = i + 1;
                            list_Name.SetSelected(i, true);
                        }
                        else
                        {
                            y++;
                            list_NamAbsnt.Items.Add(list_Name.Items[i]).ToString();
                            list_NamAbsnt.SetSelected(0, true);
                        }
                    }
                    lbl_Sho.Text = y.ToString();
                }
                catch
                {
                }
                finally
                {
                    btn_Show.Enabled = false;
                }
            }
        }        

        DataTable dt_tbl;
        private void NamePresent()//پارکبان های حاظر
        {
            con = new Connection();
            con.GetConnection.Open();
            SqlCommand cm = new SqlCommand(@"SELECT  P1.Name,P1.ID_Park,date FROM lohh L " +                    
                    " LEFT JOIN Parkban_Property P1 ON  P1.ID_Park=L.ID_Park1 OR P1.ID_Park=L.ID_Park2 OR P1.ID_Park=L.ID_Park3 OR P1.ID_Park=L.ID_Park4 OR P1.ID_Park=L.ID_Park5 " +
                    "WHERE date='" + bcal.Text + "'", con.GetConnection);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            dt_tbl = new DataTable();
            da.Fill(dt_tbl);

            foreach (DataRow dr in dt_tbl.Rows)
            {
                list_NamPrsnt.Items.Add(dr["Name"]);
            }
            con.GetConnection.Close();           
        }

        DataTable list1;
        private void NameAll()
        {
            con = new Connection();
            con.GetConnection.Open();
            SqlCommand cm = new SqlCommand("SELECT Name FROM Parkban_Property", con.GetConnection);
            da1 = new SqlDataAdapter(cm);
            list1 = new DataTable();
            da1.Fill(list1);

            foreach (DataRow dr in list1.Rows)
            {
                list_Name.Items.Add(dr["Name"]);
            }

            con.GetConnection.Close();                    
        }

        private void StreALShow()
        {
            try
            {
                con = new Connection();
                con.GetConnection.Open();
                SqlCommand cm = new SqlCommand("SELECT ID_street,street_name FROM  setting_street where ID_street NOT IN (SELECT ID_street FROM lohh WHERE date='" + bcal.Text + "')", con.GetConnection);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                dt_lstWrk = new DataTable();
                da.Fill(dt_lstWrk);

                foreach (DataRow dr in dt_lstWrk.Rows)
                {
                    list_Stre.Items.Add(dr["street_name"]);
                }
            }
            catch
            {
                MessageBox.Show("خطا در جدول شماره های خالی", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                con.GetConnection.Close();
            }         
        }

        private void Clear()
        {
            bcal.Text = string.Empty;
            lbl_User.Text = "-";
            lbl_NameS.Text = "-";
            lbl_IDPark.Text = "-";
            lbl_Sho.Text = "-";
            GridAbsLve();
            dtG_.Refresh();
            radio_Absnt.Checked = false;
            radio_Leave.Checked = false;
            list_Name.ClearSelected();
            list_Name.SelectedIndex = -1;
            dtG_.ClearSelection();
            dtG_.CurrentCell = null;
            btn_Show.Enabled = true;
            list_NamAbsnt.Items.Clear();
            this.Activate();
        }

        private void list_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void list_Stre_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_NewAL_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(bcal.Text))
            {
                MessageBox.Show("تاریخ را وارد کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
            else
            {
                if ((lbl_NameS.Text == "-") && (lbl_IDPark.Text == "-"))
                {
                    MessageBox.Show("نام پارکبان غایب را از لیست انتخاب نمایید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }
                else if (radio_Absnt.Checked || radio_Leave.Checked)
                {
                    try
                    {
                        con = new Connection();
                        con.GetConnection.Open();

                        SqlCommand com = new SqlCommand("SELECT * FROM AbsentLeave WHERE ID_Park=@ID_Park AND date_AL=@date_AL ", con.GetConnection);
                        com.Parameters.AddWithValue("@ID_Park", lbl_IDPark.Text);
                        com.Parameters.AddWithValue("@date_AL", bcal.Text);
                        SqlDataReader rd = com.ExecuteReader();
                        if (rd.Read())
                        {
                            MessageBox.Show("عدم حضوری این پارکبان در این تاریخ قبلا ثبت شده است", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                            this.Activate();
                        }
                        else
                        {
                            rd.Close();
                            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM AbsentLeave", con.GetConnection);
                            DataSet ds1 = new DataSet();
                            da1.Fill(ds1, "id");
                            DataTable tb1 = ds1.Tables["id"];
                            DataRow dr1 = tb1.NewRow();

                            if (radio_Absnt.Checked)
                            {
                                dr1["Abs_Lve"] = (radio_Absnt.Text);
                            }
                            else if (radio_Leave.Checked)
                            {
                                dr1["Abs_Lve"] = (radio_Leave.Text);
                            }
                            dr1["date_AL"] = (bcal.Text);
                            dr1["ID_Park"] = Convert.ToInt32(lbl_IDPark.Text);

                            tb1.Rows.Add(dr1);
                            SqlCommandBuilder cb1 = new SqlCommandBuilder(da1);
                            da1.Update(ds1, "id");
                            //////////////////////////////////////////////////
                            MessageBox.Show("ثبت حضوری با موفقیت انجام شد", "اعلام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                            Clear();
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
                        con.GetConnection.Close();
                        GridAbsLve();
                        dtG_.Refresh();
                        Clear();
                        this.Activate();
                    }
                }
                else
                {
                    MessageBox.Show("علت عدم حضوری را تیک بزنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    errorPro_AL.SetError(radio_Absnt, "یک مورد را بزنید");
                    errorPro_AL.SetError(radio_Leave, "یک مورد را بزنید");
                    this.Activate();
                }
            }
            
        }

        private void btn_RefAL_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void list_NamAbsnt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lbl_NameS.Text = list_NamAbsnt.SelectedItem.ToString();
                UserNameShow();
            }
            catch
            {
            }
        }

        private void radio_Absnt_CheckedChanged(object sender, EventArgs e)
        {
            if ((radio_Absnt.Checked) ||(radio_Absnt.Checked))
            {
                errorPro_AL.Clear();
            }

        }

        private void radio_Leave_CheckedChanged(object sender, EventArgs e)
        {
            if ((radio_Absnt.Checked) || (radio_Absnt.Checked))
            {
                errorPro_AL.Clear();
            }
        }

        private void bcal_TextChanged(object sender, EventArgs e)
        {
            NamePresent();
        }

       

      

       

        







        //SqlCommand cm = new SqlCommand("SELECT Name FROM  Parkban_Property WHERE Name NOT IN (SELECT _Name FROM Lohe WHERE date='" + lbl_Dte.Text + "')", con.GetConnection);
        //SqlCommand cm = new SqlCommand("SELECT ID_Park,Name FROM Parkban_Property WHERE ID_Park NOT IN (SELECT ID_Park FROM Loh_bak WHERE date='" + lbl_Dte.Text + "') ", con.GetConnection);//ID_Park1,ID_Park2,ID_Park3,ID_Park4,ID_Park5

        //SqlCommand cm = new SqlCommand("" +
        //                                "SELECT Name,ID_Park FROM Parkban_Property WHERE ID_Park NOT IN (SELECT ID_Park1 FROM lohh WHERE date='" + lbl_Dte.Text + "') " +
        //                                "SELECT Name,ID_Park FROM Parkban_Property WHERE ID_Park NOT IN (SELECT ID_Park2 FROM lohh WHERE date='" + lbl_Dte.Text + "') " +
        //                                "SELECT Name,ID_Park FROM Parkban_Property WHERE ID_Park NOT IN (SELECT ID_Park3 FROM lohh WHERE date='" + lbl_Dte.Text + "') " +
        //                                "SELECT Name,ID_Park FROM Parkban_Property WHERE ID_Park NOT IN (SELECT ID_Park4 FROM lohh WHERE date='" + lbl_Dte.Text + "') " +
        //                                "SELECT Name,ID_Park FROM Parkban_Property WHERE ID_Park NOT IN (SELECT ID_Park5 FROM lohh WHERE date='" + lbl_Dte.Text + "') ", con.GetConnection);

        //ID_Park1,ID_Park2,ID_Park3,ID_Park4,ID_Park5,date


    }
}
