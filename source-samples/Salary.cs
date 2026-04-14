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
    public partial class Salary : Form
    {
        Connection con;
        [DllImport("wininet.dll")]///پیغام قطع/ وصلی نت
        private extern static bool InternetGetConnectedState(out int Conn, int val);///پیغام قطع/ وصلی نت
                                                                                    ///
        public Salary()
        {
            InitializeComponent();
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("fa-IR"));
        }
        public Form1 f1;
        public Salary(Form1 f1)
            : this()
        {
            this.f1 = f1;
        }

        private void Salary_Load(object sender, EventArgs e)
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

            Fill_Combo_namepark();
            Fill_AmountKasr();
            Fill_AmountShCa();
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

        private void timerClock_Tick(object sender, EventArgs e)
        {
            lbl_Tme.Text = DateTime.Now.ToShortTimeString();
        }

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
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        private void Fill_Combo_namepark()////پر کردن کمبو باکس نام پارکبان
        {
            con = new Connection();
            try
            {
                SqlCommand cm = new SqlCommand("SELECT Name FROM parkban_property", con.GetConnection);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    combo_Name.Items.Add(dr["Name"].ToString());                    
                   
                }
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

        /// <summary>
        /// ///////////////////////////////////////////////پر کردن فیلدهای حقوقی/////////////////////////////////////////////
        /// </summary>
        private void Fill_AmountKasr()
        {
            double tax2 = 0, insur = 0, MS = 0;
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Setting_Amount", con.GetConnection);
                SqlDataReader rd = com.ExecuteReader();
                if (rd.Read())
                {
                    lbl_InsurS.Text = Convert.ToString(rd["Insur"]);//
                    insur = Convert.ToDouble(lbl_InsurS.Text);
                    lbl_InsurR.Text = (insur.ToString("N0"));///////////////////////////////                 

                    lbl_TaxS.Text = Convert.ToString(rd["Tax"]);
                    tax2 = Convert.ToDouble(lbl_TaxS.Text);                                   

                    lbl_MinSalS.Text = Convert.ToString(rd["Min_Sal"]);
                    MS = Convert.ToDouble(lbl_MinSalS.Text);
                    lbl_MinSalR.Text = (MS.ToString("N0"));///////////////////////////////          
                
                }
                else
                {
                    //lbl_WPayR_sum.Text = "خالی است";
                }
                rd.Close();
            }
            catch
            {
                lbl_TaxS.Text = "خطا";
            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        private void Fill_Imperest()
        {
            double pr_imprest = 0;
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Imprest WHERE ID_Park=@ID_Park  ", con.GetConnection);//and fiscal_mellip=@fiscal_mellip
                com.Parameters.AddWithValue("@ID_Park", lbl_ID.Text);                
                SqlDataReader rd = com.ExecuteReader();
                while (rd.Read())
                {
                    pr_imprest = pr_imprest + (Convert.ToDouble(rd["Impre_I"]));
                }
                lbl_TImprstK.Text = Convert.ToString(pr_imprest);
                lbl_TImprstKR.Text = (pr_imprest.ToString("N0"));////////////////////////////////
                rd.Close();
            }
            catch
            {
                lbl_ImprstS.Text = "خطا";
            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        private void Fill_Debt()
        {
            double pr_regdebt = 0;
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Debt WHERE ID_Park=@ID_Park ", con.GetConnection);
                com.Parameters.AddWithValue("@ID_Park", lbl_ID.Text);               
                SqlDataReader rd = com.ExecuteReader();
                while (rd.Read())
                {
                    pr_regdebt = pr_regdebt + (Convert.ToDouble(rd["DebtAm_D"]));
                }
                lbl_TDebtK.Text = Convert.ToString(pr_regdebt);
                lbl_TDebtKR.Text = (pr_regdebt.ToString("N0"));////////////////////////////////
                rd.Close();
            }
            catch
            {
                lbl_DebtS.Text = "خطا";
            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        private void Fill_ShargeDebt()
        {
            double c_debt = 0, c_paid = 0, c_ns = 0, anys = 0, anysR, r_nc = 0, r_dbts = 0;
            anys = Convert.ToDouble(lbl_AnyShS.Text);
            anysR = anys / 1000;            
            con = new Connection();
            try
            {
                con.GetConnection.Open();                
                SqlCommand com2 = new SqlCommand("SELECT * FROM Pay_Sharg WHERE ID_Park=@ID_Park ", con.GetConnection);
                com2.Parameters.AddWithValue("@ID_Park", lbl_ID.Text);
                SqlDataReader rd2 = com2.ExecuteReader();
                while (rd2.Read())
                {
                    c_debt = c_debt + (Convert.ToDouble(rd2["debt_PS"]));
                    c_paid = c_paid + (Convert.ToDouble(rd2["paid_PS"]));
                    c_ns = c_ns + (Convert.ToDouble(rd2["NoSharg_PS"]));
                }
                r_nc = c_ns * anysR;               
                r_dbts = r_nc - c_paid;
                r_dbts = r_dbts * 1000;
                lbl_TShrgK.Text = Convert.ToString(r_dbts);
                lbl_TShargKR.Text = (r_dbts.ToString("N0"));///////////////////////////////
                rd2.Close();
            }
            catch
            {
                lbl_TShrgK.Text = "خطا";
            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        private void Fill_Card()
        {
            //lbl_paycard_TdebtShow
            double pr_debt = 0, pr_deliv = 0, pr_nosals = 0, pr_remaind = 0, pr_paybl = 0, pr_paid = 0;//, pr_subpay = 0
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlCommand com2 = new SqlCommand("SELECT * FROM Pay_Card WHERE ID_Park=@ID_Park AND _date BETWEEN '" + bCal_Az.Text + "' AND '" + bCal_Ta.Text + "' ", con.GetConnection);
                com2.Parameters.AddWithValue("@ID_Park", lbl_ID.Text);
                SqlDataReader rd2 = com2.ExecuteReader();
                while (rd2.Read())
                {
                    pr_debt = pr_debt + (Convert.ToDouble(rd2["_debt"]));

                    //paycard_remainC
                    pr_remaind = pr_remaind + (Convert.ToDouble(rd2["_remainC"]));

                    ////بدهی کارت
                    //txt_paycard_delivC
                    pr_deliv = pr_deliv + (Convert.ToDouble(rd2["_delivC"]));
                    //paycard_nosalesC
                    pr_nosals = pr_nosals + (Convert.ToDouble(rd2["_nosalesC"]));

                    /////بدهی پول
                    //paycard_payabl
                    pr_paybl = pr_paybl + (Convert.ToDouble(rd2["_payabl"]));
                    //paycard_paid
                    pr_paid = pr_paid + (Convert.ToDouble(rd2["_paid"]));
                }
                //pr_subpay = pr_paybl - pr_paid;
                //lbl_TCrdK.Text = Convert.ToString(pr_subpay);
                //lbl_TCrdKR.Text = (pr_subpay.ToString("N0"));/////////////////////////
                
                //pr_subcard = (pr_deliv) - (pr_nosals);
                //lbl_MandeS.Text = Convert.ToString(pr_subcard);

                ////lbl_paycard_TdebtShow.Text = Convert.ToString(pr_debt_c);
                ////lbl_paycard_mande.Text = Convert.ToString(pr_remaind);
                txt_PyCrd.Text = Convert.ToString(pr_nosals);// فروخته شده ی کل
                //lbl_TCaS.Text = Convert.ToString(pr_deliv);// کل کارتها

                //Grid_PayCard_debt();

                rd2.Close();
            }
            catch
            {
                lbl_TCrdK.Text = "خطا";
                
            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        private void Fill_CardDbt()
        {
            //lbl_paycard_TdebtShow
            double pr_debt = 0, pr_deliv = 0, pr_nosals = 0, pr_remaind = 0, pr_paybl = 0, pr_paid = 0, pr_subpay = 0;
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlCommand com2 = new SqlCommand("SELECT * FROM Pay_Card WHERE ID_Park=@ID_Park ", con.GetConnection);
                com2.Parameters.AddWithValue("@ID_Park", lbl_ID.Text);
                SqlDataReader rd2 = com2.ExecuteReader();
                while (rd2.Read())
                {
                    pr_debt = pr_debt + (Convert.ToDouble(rd2["_debt"]));

                    //paycard_remainC
                    pr_remaind = pr_remaind + (Convert.ToDouble(rd2["_remainC"]));

                    ////بدهی کارت
                    //txt_paycard_delivC
                    pr_deliv = pr_deliv + (Convert.ToDouble(rd2["_delivC"]));
                    //paycard_nosalesC
                    pr_nosals = pr_nosals + (Convert.ToDouble(rd2["_nosalesC"]));

                    /////بدهی پول
                    //paycard_payabl
                    pr_paybl = pr_paybl + (Convert.ToDouble(rd2["_payabl"]));
                    //paycard_paid
                    pr_paid = pr_paid + (Convert.ToDouble(rd2["_paid"]));
                }
                pr_subpay = pr_paybl - pr_paid;
                lbl_TCrdK.Text = Convert.ToString(pr_subpay);
                lbl_TCrdKR.Text = (pr_subpay.ToString("N0"));/////////////////////////
                //pr_subcard = (pr_deliv) - (pr_nosals);
                //lbl_MandeS.Text = Convert.ToString(pr_subcard);

                ////lbl_paycard_TdebtShow.Text = Convert.ToString(pr_debt_c);
                ////lbl_paycard_mande.Text = Convert.ToString(pr_remaind);
                //txt_PyCrd.Text = Convert.ToString(pr_nosals);// فروخته شده ی کل
                //lbl_TCaS.Text = Convert.ToString(pr_deliv);// کل کارتها

                //Grid_PayCard_debt();

                rd2.Close();
            }
            catch
            {
                lbl_TCrdK.Text = "خطا";

            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        private void Fill_ShargeBuy()
        {
            int no_sharge = 0;
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlCommand com2 = new SqlCommand("SELECT * FROM Pay_Sharg WHERE ID_Park=@ID_Park AND date_PS BETWEEN '" + bCal_Az.Text + "' AND '" + bCal_Ta.Text + "' ", con.GetConnection);
                com2.Parameters.AddWithValue("@ID_Park", lbl_ID.Text);
                SqlDataReader rd2 = com2.ExecuteReader();
                while (rd2.Read())
                {
                    no_sharge = no_sharge + (Convert.ToInt32(rd2["NoSharg_PS"]));
                }

                rd2.Close();
                txt_PyShrg.Text = Convert.ToString(no_sharge);
            }
            catch
            {
                lbl_ResSh.Text = "خطا";
            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        private void Fill_AmountShCa()  
        {
            double CardOrg = 0, AnySharge = 0;
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Setting_Amount ", con.GetConnection);
                SqlDataReader rd = com.ExecuteReader();
                if (rd.Read())
                {
                    CardOrg = (Convert.ToDouble(rd["CrdFee"]));
                    AnySharge = (Convert.ToDouble(rd["AnyShrg"]));
                    lbl_ShPrcnt.Text = ((rd["txt_OfShSls"]).ToString());
                    lbl_CrdPrcnt.Text = ((rd["OfCrdSls"]).ToString());
                }
                lbl_CrdFeeS.Text = Convert.ToString(CardOrg);
                lbl_CrdFeeR.Text = (CardOrg.ToString("N0"));///////////////////////////////////    
                lbl_AnyShS.Text = Convert.ToString(AnySharge);
                lbl_AnyShR.Text = (AnySharge.ToString("N0"));///////////////////////////////////      
                rd.Close();
            }
            catch
            {
                lbl_CrdFeeS.Text = "خطا";
                lbl_AnyShS.Text = "خطا";
            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        private void Fill_Transct()
        {
            int cal = 0;
            con = new Connection();
            con.GetConnection.Open();
            SqlCommand com2 = new SqlCommand("SELECT t2.calc,t2.ID_Trns FROM Transact2 t2 " +
                       " LEFT JOIN Transact1 P1 ON  P1.ID_Trns=t2.ID_Trns " +
                       " WHERE t2.date_T2=P1.date_TT AND ID_Park=@ID_Park AND t2.ID_Trns=P1.ID_Trns AND t2.date_T2 BETWEEN '" + bCal_Az.Text + "' AND '" + bCal_Ta.Text + "' ", con.GetConnection);
                com2.Parameters.AddWithValue("@ID_Park", lbl_ID.Text);
                SqlDataReader rd2 = com2.ExecuteReader();
                while (rd2.Read())
                {                  
                    cal += Convert.ToInt32((rd2["calc"]));
                    lbl_IDTrns.Text = Convert.ToString(rd2["ID_Trns"]);
                }              
                rd2.Close();    
                lbl_Trns.Text = Convert.ToString(cal);
                lbl_TrnsR.Text = (cal.ToString("N0"));///////////////////////////////////           
            con.GetConnection.Close();           
        }      

        private void Insurance()
        {
            double insu = 0,insS=0,res=0,resP=0;
            int i = 0;
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlCommand com2 = new SqlCommand("SELECT * FROM Insurance WHERE ID_Park=@ID_Park ", con.GetConnection);
                com2.Parameters.AddWithValue("@ID_Park", lbl_ID.Text);
                SqlDataReader rd2 = com2.ExecuteReader();
                while (rd2.Read())
                {
                    insu = insu + (Convert.ToDouble(rd2["Insur"]));
                    i++;
                }               
                rd2.Close();
                insS = Convert.ToDouble(lbl_InsurS.Text);
                lbl_NoInsuS.Text = Convert.ToString(i);
                res = i * insS;
                lbl_InsuAllS.Text = Convert.ToString(insu);
                lbl_InsuAllR.Text = (insu.ToString("N0"));/////////////////////////////////// 

                resP = res - insu;
                lbl_InsDbtS.Text = Convert.ToString(resP);
                lbl_InsDbtR.Text = (resP.ToString("N0"));/////////////////////////////////// 
            }
            catch
            {
                lbl_InsDbtS.Text = "خطا";
            }
            finally
            {
                con.GetConnection.Close();
            }
        }

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combo_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if((string.IsNullOrEmpty(bCal_Az.Text)) && (string.IsNullOrEmpty(bCal_Ta.Text)))
            {
                MessageBox.Show("ابتدا هر دو تاریخ ها را وارد کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                combo_Name.SelectedItem = null;
                this.Activate();
            }
            else if ((string.IsNullOrEmpty(bCal_Az.Text)) || (string.IsNullOrEmpty(bCal_Ta.Text)))
            {
                MessageBox.Show("ابتدا هر دو تاریخ ها را وارد کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                combo_Name.SelectedItem = null;
                this.Activate();
            }
            else
            {
                con = new Connection();
                con.GetConnection.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM parkban_property WHERE Name=@Name ", con.GetConnection);
                com.Parameters.AddWithValue("@Name", combo_Name.SelectedItem);
                SqlDataReader rd = com.ExecuteReader();
                if (rd.Read())
                {
                    lbl_ID.Text = Convert.ToString(rd["ID_Park"]);
                }
                rd.Close();
                con.GetConnection.Close();
                Fill_Imperest();
                Fill_Debt();
                Fill_ShargeDebt();
                Fill_Card();
                Fill_CardDbt();
                Fill_ShargeBuy();
                Fill_Transct();               
                Insurance();
            }
        }

        private void txt_Karane_TextChanged(object sender, EventArgs e)
        {
            if (Num(txt_Karane.Text))
            {                
                errorPro_Sal.Clear();
                if (txt_Karane.Text == "" || txt_Karane.Text == "0")
                    return;
                decimal price;
                price = decimal.Parse(txt_Karane.Text, System.Globalization.NumberStyles.Currency);
                lbl_KaraneR.Text = price.ToString("#,#");
                errorPro_Sal.Clear();
            }
            else
            {
                errorPro_Sal.SetError(txt_Karane, "عدد باشد");
            }
        }

        private void txt_Karane_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_Sal.SetError(tb, "فیلد پر شود");
            }

            if (!Num(txt_Karane.Text))
            {
                errorPro_Sal.SetError(txt_Karane, "عدد وارد شود");
            }    
        }

        private void txt_Tashviqi_TextChanged(object sender, EventArgs e)
        {
            if (Num(txt_Tashviqi.Text))
            {
                errorPro_Sal.Clear();
                if (txt_Tashviqi.Text == "" || txt_Tashviqi.Text == "0")
                    return;
                decimal price;
                price = decimal.Parse(txt_Tashviqi.Text, System.Globalization.NumberStyles.Currency);
                lbl_TashviqiR.Text = price.ToString("#,#");
                errorPro_Sal.Clear();
            }
            else
            {
                errorPro_Sal.SetError(txt_Tashviqi, "عدد باشد");
            }
        }

        private void txt_Tashviqi_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_Sal.SetError(tb, "فیلد پر شود");
            }

            if (!Num(txt_Tashviqi.Text))
            {
                errorPro_Sal.SetError(txt_Tashviqi, "عدد وارد شود");
            }    
        }      

        double ShP1 = 0, Shp2 = 0, Shp3 = 0, ShP = 0;
        private void txt_PyShrg_TextChanged(object sender, EventArgs e)
        {
            if (Num(txt_PyShrg.Text))
            {
                if (string.IsNullOrEmpty(txt_PyShrg.Text))
                {
                    lbl_ResSh.Text = "---";
                    lbl_ResShR.Text = "---";
                    errorPro_Sal.Clear();
                }
                else
                {
                    ShP1 = Convert.ToDouble(txt_PyShrg.Text);
                    Shp2 = Convert.ToDouble(lbl_ShPrcnt.Text);
                    ShP = Convert.ToDouble(lbl_AnyShS.Text);
                    Shp3 = ShP1 * Shp2 * ShP;
                    lbl_ResSh.Text = Convert.ToString(Shp3);
                    lbl_ResShR.Text = (Shp3.ToString("N0"));///////////////////////////////////  
                    errorPro_Sal.Clear();
                }
            }
            else
            {
                errorPro_Sal.SetError(txt_PyShrg, "عدد باشد");                
            }
        }

        double CaP1 = 0, CaP2 = 0, CaP3 = 0, Cap = 0;
        private void txt_PyCrd_TextChanged(object sender, EventArgs e)
        {
            if (Num(txt_PyCrd.Text))
            {
                if (string.IsNullOrEmpty(txt_PyCrd.Text))
                {
                    lbl_ResCa.Text = "---";
                    lbl_ResCaR.Text = "---";
                }
                else
                {
                    CaP1 = Convert.ToDouble(txt_PyCrd.Text);
                    CaP2 = Convert.ToDouble(lbl_CrdPrcnt.Text);
                    Cap = Convert.ToDouble(lbl_CrdFeeS.Text);
                    CaP3 = CaP1 * CaP2 * Cap;
                    lbl_ResCa.Text = Convert.ToString(CaP3);
                    lbl_ResCaR.Text = (CaP3.ToString("N0"));///////////////////////////////////  
                }
            }
            else
            {
                errorPro_Sal.SetError(txt_PyCrd, "عدد باشد");
            }
        }

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Insur_TextChanged(object sender, EventArgs e)
        {
            if (Num(txt_Insur.Text))
            {
                if ((string.IsNullOrEmpty(txt_Insur.Text)))
                {
                    lbl_TInsurK.Text = "---";
                    lbl_TInsurKR.Text = "---";
                    errorPro_Sal.Clear();
                }
                else
                {
                    k_insu = Convert.ToDouble(txt_Insur.Text);
                    lbl_TInsurK.Text = txt_Insur.Text;
                    lbl_TInsurKR.Text = (k_insu.ToString("N0"));/////////////////////////////////// 
                    errorPro_Sal.Clear();
                }
            }
            else
            {
                errorPro_Sal.SetError(txt_Insur, "عدد باشد");
            }
        }

        private void txt_Insur_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_Sal.SetError(tb, "فیلد پر شود");
            }

            if (!Num(txt_Insur.Text))
            {
                errorPro_Sal.SetError(txt_Insur, "عدد وارد شود");
            }    
        }
        
        private void txt_ImprD_TextChanged(object sender, EventArgs e)
        {
            if (Num(txt_ImprD.Text))
            {
                if ((string.IsNullOrEmpty(txt_ImprD.Text)))
                {
                    lbl_ImprstS.Text = "---";
                    lbl_ImprsR.Text = "---";
                    errorPro_Sal.Clear();
                }
                else
                {
                    k_impr = Convert.ToDouble(txt_ImprD.Text);
                    lbl_ImprstS.Text = txt_ImprD.Text;
                    lbl_ImprsR.Text = (k_impr.ToString("N0"));/////////////////////////////////// 
                    errorPro_Sal.Clear();
                }
            }
            else
            {
                errorPro_Sal.SetError(txt_ImprD, "عدد باشد");
            }
        }

        private void txt_ImprD_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_Sal.SetError(tb, "فیلد پر شود");
            }

            if (!Num(txt_ImprD.Text))
            {
                errorPro_Sal.SetError(txt_ImprD, "عدد وارد شود");
            }    
        }

        private void txt_CrdKsr_TextChanged(object sender, EventArgs e)
        {
            if (Num(txt_CrdKsr.Text))
            {
                if ((string.IsNullOrEmpty(txt_CrdKsr.Text)))
                {
                    lbl_CrdKS.Text = "---";
                    lbl_CrdKR.Text = "---";
                    errorPro_Sal.Clear();
                }
                else
                {
                    k_crd = Convert.ToDouble(txt_CrdKsr.Text);
                    lbl_CrdKS.Text = txt_CrdKsr.Text;
                    lbl_CrdKR.Text = (k_crd.ToString("N0"));/////////////////////////////////// 
                    errorPro_Sal.Clear();
                }
            }
            else
            {
                errorPro_Sal.SetError(txt_CrdKsr, "عدد باشد");
            }
        }

        private void txt_CrdKsr_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_Sal.SetError(tb, "فیلد پر شود");
            }

            if (!Num(txt_CrdKsr.Text))
            {
                errorPro_Sal.SetError(txt_CrdKsr, "عدد وارد شود");
            }    
        }

        private void txt_ShrgKsr_TextChanged(object sender, EventArgs e)
        {
            if (Num(txt_ShrgKsr.Text))
            {
                if ((string.IsNullOrEmpty(txt_ShrgKsr.Text)))
                {
                    lbl_ShrgKS.Text = "---";
                    lbl_ShrgKR.Text = "---";
                    errorPro_Sal.Clear();
                }
                else
                {
                    k_shr = Convert.ToDouble(txt_ShrgKsr.Text);
                    lbl_ShrgKS.Text = txt_ShrgKsr.Text;
                    lbl_ShrgKR.Text = (k_shr.ToString("N0"));/////////////////////////////////// 
                    errorPro_Sal.Clear();
                }
            }
            else
            {
                errorPro_Sal.SetError(txt_ShrgKsr, "عدد باشد");
            }
        }

        private void txt_ShrgKsr_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_Sal.SetError(tb, "فیلد پر شود");
            }

            if (!Num(txt_ShrgKsr.Text))
            {
                errorPro_Sal.SetError(txt_ShrgKsr, "عدد وارد شود");
            }    
        }

        private void txt_RigDbt_TextChanged(object sender, EventArgs e)
        {
            if (Num(txt_RigDbt.Text))
            {
                if ((string.IsNullOrEmpty(txt_RigDbt.Text)))
                {
                    lbl_DebtS.Text = "---";
                    lbl_DebtR.Text = "---";
                    errorPro_Sal.Clear();
                }
                else
                {
                    k_dbt = Convert.ToDouble(txt_RigDbt.Text);
                    lbl_DebtS.Text = txt_RigDbt.Text;
                    lbl_DebtR.Text = (k_dbt.ToString("N0"));/////////////////////////////////// 
                    errorPro_Sal.Clear();
                }
            }
            else
            {
                errorPro_Sal.SetError(txt_RigDbt, "عدد باشد");
            }
        }

        private void txt_RigDbt_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorPro_Sal.SetError(tb, "فیلد پر شود");
            }

            if (!Num(txt_RigDbt.Text))
            {
                errorPro_Sal.SetError(txt_RigDbt, "عدد وارد شود");
            }    
        }
        
        /////////////////////////////////////////////////////////////////////////////////////////////////
        double s_trns = 0, s_RSh = 0, s_RCa = 0, s_Tsh = 0, s_Kar = 0, s_sum = 0, s_minsal = 0, tax = 0, res_tax = 0, totals2 = 0, tot_Tax = 0;
        private void btn_CalSum_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_PyShrg.Text))
            {
                MessageBox.Show("نام را از لیست انتخاب کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
            else if ((string.IsNullOrEmpty(bCal_Az.Text)) && (string.IsNullOrEmpty(bCal_Ta.Text)))
            {
                MessageBox.Show("تاریخ ها را وارد کنید ", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
            else
            {
                 if ((string.IsNullOrEmpty(txt_Tashviqi.Text)) &&
                 (string.IsNullOrEmpty(txt_Karane.Text)))
                 {
                     errorPro_Sal.SetError(txt_Tashviqi, "عدد وارد کنید");
                     errorPro_Sal.SetError(txt_Karane, "عدد وارد کنید");
                 }
                 else if ((string.IsNullOrEmpty(txt_Tashviqi.Text)) ||
                     (string.IsNullOrEmpty(txt_Karane.Text)))
                 {
                     errorPro_Sal.SetError(txt_Tashviqi, "عدد وارد کنید");
                     errorPro_Sal.SetError(txt_Karane, "عدد وارد کنید");
                 }
                 else
                 {
                     s_trns = Convert.ToDouble(lbl_Trns.Text);
                     s_RSh = Convert.ToDouble(lbl_ResSh.Text);
                     s_RCa = Convert.ToDouble(lbl_ResCa.Text);
                     s_Tsh = Convert.ToDouble(txt_Tashviqi.Text);
                     s_Kar = Convert.ToDouble(txt_Karane.Text);
                     s_sum = s_trns + s_RSh + s_RCa + s_Tsh + s_Kar;
                     
                     lbl_Sum.Text = Convert.ToString(s_sum);
                     lbl_SumR.Text = (s_sum.ToString("N0"));///////////////////////////////////  
                    
                     s_minsal = Convert.ToDouble(lbl_MinSalS.Text);
                     btn_CalKasr.Enabled = true;
                     panel_Kasr.Enabled = true;
                     if (s_sum > s_minsal)
                     {
                         lbl_totalS2.Text = lbl_SumR.Text;
                         totals2 = Convert.ToDouble(lbl_totalS2.Text);
                         lbl_PR_Tax.Text = (totals2.ToString("N0"));///////////////////////////////////
                         tax = Convert.ToDouble(lbl_TaxS.Text);
                         
                         tot_Tax = (totals2 - s_minsal) * tax;
                         lbl_ResTax.Text = Convert.ToString(tot_Tax);
                         lbl_TaxR.Text = (tot_Tax.ToString("N0"));///////////////////////////////////
                     }        
                     else
                     {
                         lbl_ResTax.Text = "0";
                         res_tax = Convert.ToDouble(lbl_ResTax.Text);
                     }
                 }    
            }
        }

        double k_insu = 0 , k_impr=0, k_crd=0, k_shr=0, k_dbt=0, k_Sum=0 , SumAll=0;
        double e_insDbt=0, e_sumInsu=0, e_InsuS=0 , e_imprK=0 ,e_Tcrd=0 , e_TSh=0 , e_dbt=0;
        private void btn_CalKasr_Click(object sender, EventArgs e)
        {            
            if ((string.IsNullOrEmpty(txt_ImprD.Text)) &&
                (string.IsNullOrEmpty(txt_CrdKsr.Text))&&
                (string.IsNullOrEmpty(txt_ShrgKsr.Text))&&
                (string.IsNullOrEmpty(txt_Insur.Text))&&
                (string.IsNullOrEmpty(txt_RigDbt.Text))
                )
            {
                errorPro_Sal.SetError(txt_ImprD, "عدد وارد کنید");
                errorPro_Sal.SetError(txt_CrdKsr, "عدد وارد کنید");
                errorPro_Sal.SetError(txt_ShrgKsr, "عدد وارد کنید");
                errorPro_Sal.SetError(txt_Insur, "عدد وارد کنید");
                errorPro_Sal.SetError(txt_RigDbt, "عدد وارد کنید");
            }
            else if ((string.IsNullOrEmpty(txt_ImprD.Text)) ||
                (string.IsNullOrEmpty(txt_CrdKsr.Text)) ||
                (string.IsNullOrEmpty(txt_ShrgKsr.Text))||
                (string.IsNullOrEmpty(txt_Insur.Text))||
                (string.IsNullOrEmpty(txt_RigDbt.Text))
                )
            {
                errorPro_Sal.SetError(txt_ImprD, "عدد وارد کنید");
                errorPro_Sal.SetError(txt_CrdKsr, "عدد وارد کنید");
                errorPro_Sal.SetError(txt_ShrgKsr, "عدد وارد کنید");
                errorPro_Sal.SetError(txt_Insur, "عدد وارد کنید");
                errorPro_Sal.SetError(txt_RigDbt, "عدد وارد کنید");
            }

            else
            {
                errorPro_Sal.Clear();               
                e_InsuS = Convert.ToDouble(lbl_InsurS.Text);
                e_sumInsu = e_insDbt + e_InsuS;
                e_imprK=Convert.ToDouble(lbl_TImprstK.Text);
                e_Tcrd = Convert.ToDouble(lbl_TCrdK.Text);
                e_TSh = Convert.ToDouble(lbl_TShrgK.Text);
                e_dbt = Convert.ToDouble(lbl_TDebtK.Text);

                if (k_insu > e_sumInsu)
                {
                    MessageBox.Show("مقدار بیمه وارد شده از مجموع مقادیر بیمه کارکنان و بدهی بیمه بیشتر است", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    errorPro_Sal.SetError(txt_Insur, "بیشتر است");
                    this.Activate();
                }
                else if (k_impr > e_imprK)
                {
                    MessageBox.Show("مساعده وارد شده از مقدار مساعده دریافتی بیشتر است", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    errorPro_Sal.SetError(txt_ImprD, "بیشتر است");
                    this.Activate();
                }

                else if (k_crd > e_Tcrd)
                {
                    MessageBox.Show("مقدارکارت وارد شده ، از مقدار بدهی کارت بیشتر است", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    errorPro_Sal.SetError(txt_CrdKsr, "بیشتر است");
                    this.Activate();
                }
                else if (k_shr > e_TSh)
                {
                    MessageBox.Show("مقدارشارژ وارد شده از مقدار بدهی شارژ بیشتر است", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    errorPro_Sal.SetError(txt_ShrgKsr, "بیشتر است");
                    this.Activate();
                }

                else if (k_dbt > e_dbt)
                {
                    MessageBox.Show("مقدار بدهی وارد شده از مقدار بدهی بیشتر است", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    errorPro_Sal.SetError(txt_RigDbt, "بیشتر است");
                    this.Activate();
                }
                else
                {
                    k_Sum = k_insu + k_impr + k_crd + k_shr + k_dbt + tot_Tax;
                    lbl_Kasr.Text = Convert.ToString(k_Sum);
                    lbl_KasrR.Text = (k_Sum.ToString("N0"));/////////////////////////////////// 
                    SumAll = s_sum - k_Sum;
                    lbl_SumAllS.Text = Convert.ToString(SumAll);
                    lbl_SumAllR.Text = (SumAll.ToString("N0"));/////////////////////////////////// 
                    
                    btn_RegPyRoll.Enabled = true;
                }
            }            
        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Salar_RegCard()/// ثبت در جدول کارت
        {
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM pay_card", con.GetConnection);
                DataSet ds = new DataSet();
                da.Fill(ds, "id");
                DataTable tb = ds.Tables["id"];
                DataRow dr = tb.NewRow();

                double minus = 0, multi = 0;
                minus = (Convert.ToDouble(lbl_CrdKS.Text));
                multi = -minus;

                dr["_submit"] = lbl_Usr.Text;//1
                dr["_timsys"] = lbl_Tme.Text;//2          
                dr["_date"] = bCal_Ta.Text;//3                       
                dr["_nosalesC"] = "0";//4
                dr["_delivC"] = "0";//5            
                dr["_remainC"] = "0";//6         
                dr["_payabl"] = "0";//7
                dr["_paid"] = multi;//8
                dr["_debt"] = "0";//9
                dr["_desc"] = "ثبت هنگام پرداخت حقوق";//10
                dr["ID_Park"] = lbl_ID.Text;//11

                tb.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "id");
                //MessageBox.Show("در جدول کارت ذخیره شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                //this.Activate();
            }
            catch
            {
                MessageBox.Show("قادر به ذخیره سازی در جدول کارت نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
            finally
            {                              
                con.GetConnection.Close();                
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void Salary_UpCard()
        {
            double minus = 0, multi = 0;
            minus = (Convert.ToDouble(lbl_CrdKS.Text));
            multi = -minus;

            con = new Connection();
            con.GetConnection.Open();
            SqlCommand com = new SqlCommand("UPDATE Pay_Card SET ID_Park='" + Convert.ToInt32(lbl_ID.Text) + "' , _date='" + bCal_Ta.Text + "', " +
            "_nosalesC='" + "0" + "',_delivC='" + "0" + "',_paid='" + multi + "', _submit='" + lbl_Usr.Text + "', _timsys='" + lbl_Tme.Text + "', " +
            "_desc='" + "ثبت هنگام پرداخت حقوق" + "',_payabl='" + "0" + "',_debt='" + "0" + "'  WHERE _date=@_date AND _desc=@_desc ", con.GetConnection);
            com.Parameters.AddWithValue("@_date", bCal_Ta.Text);
            com.Parameters.AddWithValue("@_desc", "ثبت هنگام پرداخت حقوق");
            SqlTransaction tr = con.GetConnection.BeginTransaction();
            com.Transaction = tr;
            com.ExecuteNonQuery();
            com.Dispose();
            tr.Commit();            
            //MessageBox.Show(".ویرایش کارت با موفقیت انجام شد", "اعلام", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //this.Activate();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void Salary_RegSharge()/// ثبت در جدول شارژ
        {
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM pay_sharg", con.GetConnection);
                DataSet ds = new DataSet();
                da.Fill(ds, "id");
                DataTable tb = ds.Tables["id"];
                DataRow dr = tb.NewRow();

                double minus = 0, multi = 0, div = 0;

                minus = Convert.ToDouble(lbl_ShrgKS.Text);
                multi = -minus;
                div = multi / 1000;

                dr["ID_Park"] = lbl_ID.Text;//1
                dr["NoSharg_PS"] = "0";//2
                dr["paid_PS"] = "0";//3
                dr["debt_PS"] = div;//4
                dr["date_PS"] = bCal_Ta.Text;//5            
                dr["TmSys_Ps"] = lbl_Tme.Text;//6
                dr["UsSys_PS"] = lbl_Usr.Text;//7
                dr["Desc_PS"] = "ثبت هنگام پرداخت حقوق";//8

                tb.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "id");
                //MessageBox.Show("در جدول شارژ ذخیره شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                //this.Activate();
            }
            catch
            {
                MessageBox.Show("قادر به ذخیره سازی در جدول شارژ نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void Salary_UpSharge()
        {
            double minus = 0, multi = 0, div = 0;
            minus = Convert.ToDouble(lbl_ShrgKS.Text);
            multi = -minus;
            div = multi / 1000;

            con = new Connection();
            con.GetConnection.Open();
            SqlCommand com = new SqlCommand("UPDATE Pay_Sharg SET ID_Park='" + Convert.ToInt32(lbl_ID.Text) + "' , NoSharg_PS='" + "0" + "', " +
            "paid_PS='" + "0" + "',debt_PS='" + div + "',date_PS='" + bCal_Ta.Text + "' ,TmSys_Ps='" + lbl_Tme.Text + "',Desc_PS='" + "ثبت هنگام پرداخت حقوق" + "',UsSys_PS='" + lbl_Usr.Text + "'  WHERE date_PS=@date_PS AND Desc_PS=@Desc_PS ", con.GetConnection);
            com.Parameters.AddWithValue("@date_PS", bCal_Ta.Text);
            com.Parameters.AddWithValue("@Desc_PS", "ثبت هنگام پرداخت حقوق");
            SqlTransaction tr = con.GetConnection.BeginTransaction();
            com.Transaction = tr;
            com.ExecuteNonQuery();
            com.Dispose();        
            tr.Commit();
           
            //MessageBox.Show(".ویرایش شارژ با موفقیت انجام شد", "اعلام", MessageBoxButtons.OK, MessageBoxIcon.Information);            
            //this.Activate();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void Salary_RegImprest()////// ثبت در جدول مساعده
        {           
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Imprest", con.GetConnection);
                DataSet ds = new DataSet();
                da.Fill(ds, "id");
                DataTable tb = ds.Tables["id"];
                DataRow dr = tb.NewRow();

                double minus = 0, multi = 0;
                minus = (Convert.ToDouble(lbl_ImprstS.Text));
                multi = -minus;

                dr["ID_Park"] = lbl_ID.Text;//1
                dr["Impre_I"] = multi;//2
                dr["Fish_I"] = "0";//3           
                dr["Date_I"] = (bCal_Ta.Text);//4          
                dr["UsSys_I"] = (lbl_Usr.Text);//5
                dr["Dsc_I"] = "ثبت هنگام پرداخت حقوق";//6    

                tb.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "id");
                //MessageBox.Show("در جدول مساعده ذخیره شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                //this.Activate();
            }
            catch
            {
                MessageBox.Show("قادر به ذخیره سازی در جدول مساعده نمی باشد", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void Salary_UpImprest()
        {
            double minus = 0, multi = 0;
            minus = (Convert.ToDouble(lbl_ImprstS.Text));
            multi = -minus;

            con = new Connection();
            con.GetConnection.Open();
            SqlCommand com = new SqlCommand("UPDATE Imprest SET ID_Park='" + Convert.ToInt32(lbl_ID.Text) + "' , Date_I='" + bCal_Ta.Text + "', UsSys_I='" + lbl_Usr.Text + "'," +
            "Impre_I='" + multi + "',Fish_I='" + "0" + "',Dsc_I='" + "ثبت هنگام پرداخت حقوق" + "' WHERE Date_I=@Date_I AND Dsc_I=@Dsc_I ", con.GetConnection);
            com.Parameters.AddWithValue("@Date_I", bCal_Ta.Text);
            com.Parameters.AddWithValue("@Dsc_I", "ثبت هنگام پرداخت حقوق");
            SqlTransaction tr = con.GetConnection.BeginTransaction();
            com.Transaction = tr;
            com.ExecuteNonQuery();
            com.Dispose();
            tr.Commit();            
            //MessageBox.Show(".ویرایش مساعده با موفقیت انجام شد", "اعلام", MessageBoxButtons.OK, MessageBoxIcon.Information);            
            //this.Activate();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void Salary_RegDebt()/////ثبت در جدول بدهی لوازم
        {
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Debt", con.GetConnection);
                DataSet ds = new DataSet();
                da.Fill(ds, "id");
                DataTable tb = ds.Tables["id"];
                DataRow dr = tb.NewRow();

                double minus = 0, multi = 0;
                minus = (Convert.ToDouble(lbl_DebtS.Text));
                multi = -minus;

                dr["ID_Park"] = Convert.ToInt32(lbl_ID.Text);//1
                dr["DebtAm_D"] = multi;//2           
                dr["Date_D"] = (bCal_Ta.Text);//3            
                dr["UsSys_D"] = lbl_Usr.Text;//4
                dr["Dcs_D"] = "ثبت هنگام پرداخت حقوق";//5   

                tb.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "id");
                //MessageBox.Show("در جدول بدهی لوازم ذخیره شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                //this.Activate();
            }
            catch
            {
                MessageBox.Show("قادر به ذخیره سازی در جدول بدهی نمی باشد", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void Salary_UpDebt()
        {
            double minus = 0, multi = 0;
            minus = (Convert.ToDouble(lbl_DebtS.Text));
            multi = -minus;

            con = new Connection();
            con.GetConnection.Open();
            SqlCommand com = new SqlCommand("UPDATE Debt SET ID_Park='" + Convert.ToInt32(lbl_ID.Text) + "' , Date_D='" + bCal_Ta.Text + "', UsSys_D='" + lbl_Usr.Text + "'," +
            "DebtAm_D='" + multi + "', Dcs_D='" + "ثبت هنگام پرداخت حقوق" + "' WHERE Date_D=@Date_D AND Dcs_D=@Dcs_D ", con.GetConnection);
            com.Parameters.AddWithValue("@Date_D", bCal_Ta.Text);
            com.Parameters.AddWithValue("@Dcs_D", "ثبت هنگام پرداخت حقوق");
            SqlTransaction tr = con.GetConnection.BeginTransaction();
            com.Transaction = tr;
            com.ExecuteNonQuery();
            com.Dispose();
            tr.Commit();           
            //MessageBox.Show(".ویرایش بدهی با موفقیت انجام شد", "اعلام", MessageBoxButtons.OK, MessageBoxIcon.Information);           
            //this.Activate();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void Salary_RegInsurance()////ثبت در جدول بیمه
        {
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Insurance", con.GetConnection);
                DataSet ds = new DataSet();
                da.Fill(ds, "id");
                DataTable tb = ds.Tables["id"];
                DataRow dr = tb.NewRow();

                dr["ID_Park"] = Convert.ToInt32(lbl_ID.Text);//1
                dr["Insur"] = Convert.ToDouble(lbl_TInsurK.Text);//2
                dr["date_Az"] = (bCal_Az.Text);//3
                dr["date_Ta"] = (bCal_Ta.Text);//3
                dr["Usr_In"] = lbl_Usr.Text;//4  
                dr["Dsc_In"] = "Salary";//4  

                tb.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "id");
                //MessageBox.Show("در جدول بیمه ذخیره شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                //this.Activate();
            }
            catch
            {
                MessageBox.Show("قادر به ذخیره سازی در جدول بیمه نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void Salary_UpInsurance()
        {
            con = new Connection();
            con.GetConnection.Open();
            SqlCommand com = new SqlCommand("UPDATE Insurance SET ID_Park='" + Convert.ToInt32(lbl_ID.Text) + "' , Usr_In='" + lbl_Usr.Text + "'," +
            "Insur='" + Convert.ToDouble(lbl_TInsurK.Text) + "',date_Ta='" + bCal_Ta.Text + "',Dsc_In='" + "Salary" + "' ,date_Az='" + bCal_Az.Text + "' WHERE date_Az=@date_Az AND date_Ta=@date_Ta ", con.GetConnection);
            com.Parameters.AddWithValue("@date_Ta", bCal_Ta.Text);
            com.Parameters.AddWithValue("@date_Az", bCal_Az.Text); 
            SqlTransaction tr = con.GetConnection.BeginTransaction();
            com.Transaction = tr;
            com.ExecuteNonQuery();
            com.Dispose();
            tr.Commit();
            //MessageBox.Show(".ویرایش بدهی با موفقیت انجام شد", "اعلام", MessageBoxButtons.OK, MessageBoxIcon.Information);           
            //this.Activate();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void Salary_RegTransact()
        {
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Transact_Salary", con.GetConnection);
                DataSet ds = new DataSet();
                da.Fill(ds, "id");
                DataTable tb = ds.Tables["id"];
                DataRow dr = tb.NewRow();

                dr["ID_Park"] = Convert.ToInt32(lbl_ID.Text);//1
                if (lbl_IDTrns.Text == "-")
                {
                    dr["ID_Trns"] = "0";//2
                }
                else
                {
                    dr["ID_Trns"] = Convert.ToInt32(lbl_IDTrns.Text);//2
                }
                dr["Date_AzT"] = (bCal_Az.Text);//3
                dr["Date_TaT"] = bCal_Ta.Text;//4   
                dr["Trns_T"] =  Convert.ToDouble(lbl_Trns.Text);//5 
                dr["Usr_Sys"] = (lbl_Usr.Text);//6 
                dr["Dsc_TS"] = "Salary";//6 

                tb.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "id");
                //MessageBox.Show("در جدول تراکنش ذخیره شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                //this.Activate();
            }
            catch
            {
                MessageBox.Show("قادر به ذخیره سازی در جدول تراکنش نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }////ثبت درجدول تراکنش حقوق
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void Salary_UpTransact()
        {
            con = new Connection();
            con.GetConnection.Open();
            SqlCommand com = new SqlCommand("UPDATE Transact_Salary SET ID_Park='" + Convert.ToInt32(lbl_ID.Text) + "' , ID_Trns='" + Convert.ToInt32(lbl_IDTrns.Text) + "', Usr_Sys='" + lbl_Usr.Text + "'," +
            "Trns_T='" + Convert.ToDouble(lbl_Trns.Text) + "',Date_AzT='" + bCal_Ta.Text + "', Dsc_TS='" +"Salary" + "' ,Date_TaT='" + bCal_Az.Text + "' WHERE Date_AzT=@Date_AzT AND Date_TaT=@Date_TaT ", con.GetConnection);
            com.Parameters.AddWithValue("@Date_AzT", bCal_Az.Text);
            com.Parameters.AddWithValue("@Date_TaT", bCal_Ta.Text);
            SqlTransaction tr = con.GetConnection.BeginTransaction();
            com.Transaction = tr;
            com.ExecuteNonQuery();
            com.Dispose();
            tr.Commit();
            //MessageBox.Show(".ویرایش بدهی با موفقیت انجام شد", "اعلام", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //this.Activate();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void Salary_Register()
        {
            con = new Connection();
            try
            {
                con.GetConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Salary", con.GetConnection);
                DataSet ds = new DataSet();
                da.Fill(ds, "id");
                DataTable tb = ds.Tables["id"];
                DataRow dr = tb.NewRow();

                dr["ID_Park"] = Convert.ToInt32(lbl_ID.Text);//1                       
                dr["Date_Az"] = bCal_Az.Text;//2
                dr["Date_Ta"] = bCal_Ta.Text;//3     

                dr["Trns_Sal"] = Convert.ToDouble(lbl_Trns.Text);//4
                dr["NoShrg_Sal"] = Convert.ToDouble(lbl_ResSh.Text);//5
                dr["NoCrd_Sal"] = Convert.ToDouble(lbl_ResCa.Text);//6
                dr["Tashviq_Sal"] = Convert.ToDouble(txt_Tashviqi.Text);//7
                dr["Karane_Sal"] = Convert.ToDouble(txt_Karane.Text);//8
                dr["Sum_Sal"] = Convert.ToDouble(lbl_Sum.Text);//9

                dr["Tax_Sal"] = Convert.ToDouble(lbl_ResTax.Text);//10
                dr["Insu_Sal"] = Convert.ToDouble(lbl_TInsurK.Text);//11
                dr["Impr_Sal"] = Convert.ToDouble(lbl_ImprstS.Text);//12
                dr["CrdDbt_Sal"] = Convert.ToDouble(lbl_CrdKS.Text);//13
                dr["ShrgDbt_Sal"] = Convert.ToDouble(lbl_ShrgKS.Text);//14
                dr["Dbt_Sal"] = Convert.ToDouble(lbl_DebtS.Text);//15           
                dr["SumK_Sal"] = Convert.ToDouble(lbl_Kasr.Text);//16

                dr["SumAll_Sal"] = Convert.ToDouble(lbl_SumAllS.Text);//17
                dr["UsrSys_Sal"] = lbl_Usr.Text;//18
                dr["Tme_Sal"] = lbl_Tme.Text;//19

                tb.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "id");
                //MessageBox.Show("در جدول کارت ذخیره شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                //this.Activate();
            }
            catch
            {
                MessageBox.Show("قادر به ذخیره سازی در جدول حقوق نمی باشد", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
            }
            finally
            {
                con.GetConnection.Close();
            }
        }////ثبت در جدول حقوق
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void Salary_UpRegister()
        {
            con = new Connection();
            con.GetConnection.Open();
            SqlCommand com = new SqlCommand("UPDATE Salary SET ID_Park='" + Convert.ToInt32(lbl_ID.Text) + "' , Date_Az='" + bCal_Az.Text + "', Date_Ta='" + bCal_Ta.Text + "'," +
            " Trns_Sal='" + Convert.ToDouble(lbl_Trns.Text) + "',NoShrg_Sal='" + Convert.ToDouble(lbl_ResSh.Text) + "', NoCrd_Sal='" + Convert.ToDouble(lbl_ResCa.Text) + "', " +
            " Tashviq_Sal='" + Convert.ToDouble(txt_Tashviqi.Text) + "',Karane_Sal='" + Convert.ToDouble(txt_Karane.Text) + "', Sum_Sal='" + Convert.ToDouble(lbl_Sum.Text) + "', " +
            " Tax_Sal='" + Convert.ToDouble(lbl_ResTax.Text) + "',Insu_Sal='" + Convert.ToDouble(lbl_TInsurK.Text) + "', Impr_Sal='" + Convert.ToDouble(lbl_ImprstS.Text) + "', " +
            " CrdDbt_Sal='" + Convert.ToDouble(lbl_CrdKS.Text) + "',ShrgDbt_Sal='" + Convert.ToDouble(lbl_ShrgKS.Text) + "', Dbt_Sal='" + Convert.ToDouble(lbl_DebtS.Text) + "', " +
            " SumK_Sal='" + Convert.ToDouble(lbl_Kasr.Text) + "',SumAll_Sal='" + Convert.ToDouble(lbl_SumAllS.Text) + "', UsrSys_Sal='" + lbl_Usr.Text + "', " +
            " Tme_Sal='" + lbl_Tme.Text + "' WHERE Date_Az=@Date_Az AND Date_Ta=@Date_Ta ", con.GetConnection);
            
            com.Parameters.AddWithValue("@Date_Az", bCal_Az.Text);
            com.Parameters.AddWithValue("@Date_Ta", bCal_Ta.Text);
            SqlTransaction tr = con.GetConnection.BeginTransaction();
            com.Transaction = tr;
            com.ExecuteNonQuery();
            com.Dispose();
            tr.Commit();
            //MessageBox.Show(".ویرایش بدهی با موفقیت انجام شد", "اعلام", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //this.Activate();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_RegPyRoll_Click(object sender, EventArgs e)
        {
            con = new Connection();
            con.GetConnection.Open();
            SqlCommand com_c = new SqlCommand("SELECT * FROM Pay_Card WHERE _date=@_date AND _desc=@_desc AND ID_Park=@ID_Park", con.GetConnection);
            com_c.Parameters.AddWithValue("@ID_Park", lbl_ID.Text);
            com_c.Parameters.AddWithValue("@_date", bCal_Ta.Text);
            com_c.Parameters.AddWithValue("@_desc", "ثبت هنگام پرداخت حقوق");
            SqlDataReader rd_c = com_c.ExecuteReader();
            if (rd_c.Read())
            {
                Salary_UpCard();
            }
            else
            {
                rd_c.Close();
                Salar_RegCard();
            }
            con.GetConnection.Close();

            con = new Connection();
            con.GetConnection.Open();
            SqlCommand com_s = new SqlCommand("SELECT * FROM Pay_Sharg WHERE date_PS=@date_PS AND Desc_PS=@Desc_PS AND ID_Park=@ID_Park", con.GetConnection);
            com_s.Parameters.AddWithValue("@date_PS", bCal_Ta.Text);
            com_s.Parameters.AddWithValue("@ID_Park", lbl_ID.Text);
            com_s.Parameters.AddWithValue("@Desc_PS", "ثبت هنگام پرداخت حقوق");
            SqlDataReader rd_s = com_s.ExecuteReader();
            if (rd_s.Read())
            {
                Salary_UpSharge();
            }
            else
            {
                rd_s.Close();
                Salary_RegSharge();
            }

            con.GetConnection.Close();

            con = new Connection();
            con.GetConnection.Open();
            SqlCommand com_Im = new SqlCommand("SELECT * FROM Imprest WHERE Date_I=@Date_I AND Dsc_I=@Dsc_I AND ID_Park=@ID_Park", con.GetConnection);
            com_Im.Parameters.AddWithValue("@Date_I", bCal_Ta.Text);
            com_Im.Parameters.AddWithValue("@ID_Park", lbl_ID.Text);
            com_Im.Parameters.AddWithValue("@Dsc_I", "ثبت هنگام پرداخت حقوق");
            SqlDataReader rd_Im = com_Im.ExecuteReader();
            if (rd_Im.Read())
            {
                Salary_UpImprest();
            }
            else
            {
                rd_Im.Close();
                Salary_RegImprest();
            }

            con.GetConnection.Close();

            con = new Connection();
            con.GetConnection.Open();
            SqlCommand com_dbt = new SqlCommand("SELECT * FROM Debt WHERE Date_D=@Date_D AND Dcs_D=@Dcs_D AND ID_Park=@ID_Park", con.GetConnection);
            com_dbt.Parameters.AddWithValue("@Date_D", bCal_Ta.Text);
            com_dbt.Parameters.AddWithValue("@ID_Park", lbl_ID.Text);
            com_dbt.Parameters.AddWithValue("@Dcs_D", "ثبت هنگام پرداخت حقوق");
            SqlDataReader rd_dbt = com_dbt.ExecuteReader();
            if (rd_dbt.Read())
            {
                Salary_UpDebt();
            }
            else
            {
                rd_dbt.Close();
                Salary_RegDebt();
            }

            con.GetConnection.Close();

            con = new Connection();
            con.GetConnection.Open();
            SqlCommand com_ins = new SqlCommand("SELECT * FROM Insurance WHERE date_Az=@date_Az AND date_Ta=@date_Ta AND ID_Park=@ID_Park", con.GetConnection);
            com_ins.Parameters.AddWithValue("@date_Az", bCal_Az.Text);
            com_ins.Parameters.AddWithValue("@ID_Park", lbl_ID.Text);
            com_ins.Parameters.AddWithValue("@date_Ta", bCal_Ta.Text);
            SqlDataReader rd_ins = com_ins.ExecuteReader();
            if (rd_ins.Read())
            {
                Salary_UpInsurance();
            }
            else
            {
                rd_ins.Close();
                Salary_RegInsurance();
            }

            con.GetConnection.Close();

            con = new Connection();
            con.GetConnection.Open();
            SqlCommand com_tr = new SqlCommand("SELECT * FROM Transact_Salary WHERE Date_AzT=@Date_AzT AND Date_TaT=@Date_TaT AND ID_Park=@ID_Park", con.GetConnection);
            com_tr.Parameters.AddWithValue("@Date_AzT", bCal_Az.Text);
            com_tr.Parameters.AddWithValue("@ID_Park", lbl_ID.Text);
            com_tr.Parameters.AddWithValue("@Date_TaT", bCal_Ta.Text);
            SqlDataReader rd_tr = com_tr.ExecuteReader();
            if (rd_tr.Read())
            {
                Salary_UpTransact();
            }
            else
            {
                rd_tr.Close();
                Salary_RegTransact();
            }

            con.GetConnection.Close();

            con = new Connection();
            con.GetConnection.Open();
            SqlCommand com1 = new SqlCommand("SELECT * FROM Salary WHERE Date_Az=@Date_Az AND Date_Ta=@Date_Ta AND ID_Park=@ID_Park  ", con.GetConnection);
            com1.Parameters.AddWithValue("@Date_Az", bCal_Az.Text);
            com1.Parameters.AddWithValue("@ID_Park", lbl_ID.Text);
            com1.Parameters.AddWithValue("@Date_Ta", bCal_Ta.Text);
            SqlDataReader rd1 = com1.ExecuteReader();
            if (rd1.Read())
            {
                var result = MessageBox.Show("این تاریخ های حقوقی برای این پارکبان قبلا ثبت شده است آیا میخواهید جایگزین گردد؟", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                }
                if (DialogResult == DialogResult.Yes)
                {
                    Salary_UpRegister();
                    f1.RefSalary();
                    MessageBox.Show("در حقوق با موفقیت ویرایش شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                    this.Activate();
                }               
            }
            else
            {
                rd1.Close();
                Salary_Register();
                btn_Print.Enabled = true;
                f1.RefSalary();
                MessageBox.Show("در جدول حقوق با موفقیت ثبت شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                this.Activate();
                this.Activate();
            }
             con.GetConnection.Close();
        }
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static string SetTime;
        public static string SetbCal_Az;
        public static string SetbCal_Ta;
        public static string Setcombo_Name;

        public static string Setlbl_TrnsR;
        public static string Setlbl_ResShR;
        public static string Setlbl_ResCaR;

        public static string Setlbl_TashviqiR;
        public static string Setlbl_KaraneR;
        public static string Setlbl_SumR;

        public static string Setlbl_TaxR;
        public static string Setlbl_TInsurKR;
        public static string Setlbl_ImprsR;

        public static string Setlbl_CrdKR;
        public static string Setlbl_ShrgKR;
        public static string Setlbl_DebtR;

        public static string Setlbl_KasrR;
        public static string Setlbl_SumAllR;

        public static string Settxt_PyShrg;
        public static string Settxt_PyCrd;

        public static string Setlbl_TImprstKR;
        public static string Setlbl_TCrdKR;
        public static string Setlbl_TShargKR;
        public static string Setlbl_TDebtKR;
        public static string Setlbl_InsDbtR;
        public static string Setlbl_InsurR;

        private void btn_Print_Click(object sender, EventArgs e)
        {
            SetTime = lbl_Tme.Text;
            SetbCal_Az = bCal_Az.Text;
            SetbCal_Ta = bCal_Ta.Text;
            Setcombo_Name=combo_Name.Text;

            Setlbl_TrnsR = lbl_TrnsR.Text;
            Setlbl_ResShR = lbl_ResShR.Text;
            Setlbl_ResCaR = lbl_ResCaR.Text;
            Settxt_PyShrg = txt_PyShrg.Text;
            Settxt_PyCrd = txt_PyCrd.Text;

            Setlbl_TashviqiR = lbl_TashviqiR.Text;
            Setlbl_KaraneR = lbl_KaraneR.Text;
            Setlbl_SumR = lbl_SumR.Text;

            Setlbl_TaxR = lbl_TaxR.Text;
            Setlbl_TInsurKR = lbl_TInsurKR.Text;
            Setlbl_InsurR = lbl_InsurR.Text;
            Setlbl_InsDbtR = lbl_InsDbtR.Text;
            Setlbl_ImprsR = lbl_ImprsR.Text;
            Setlbl_TImprstKR = lbl_TImprstKR.Text;
            Setlbl_TCrdKR = lbl_TCrdKR.Text;
            Setlbl_TShargKR = lbl_TShargKR.Text;

            Setlbl_CrdKR = lbl_CrdKR.Text;
            Setlbl_ShrgKR = lbl_ShrgKR.Text;
            Setlbl_DebtR = lbl_DebtR.Text;
            Setlbl_TDebtKR = lbl_TDebtKR.Text;

            Setlbl_KasrR = lbl_KasrR.Text;
            Setlbl_SumAllR = lbl_SumAllR.Text;

            PrintSalary ps = new PrintSalary();
            ps.ShowDialog();
        }

        /////////////////////////////////////////////////////////////////
        private void Clear()
        {
            bCal_Az.Text = string.Empty;
            bCal_Ta.Text = string.Empty;
            combo_Name.SelectedItem = null;
            lbl_ID.Text = "-";
            lbl_IDTrns.Text = "-";
            lbl_Trns.Text = "---";
            lbl_TrnsR.Text = "---";
            txt_PyShrg.Text= string.Empty;
            lbl_ResSh.Text = "---";
            lbl_ResShR.Text = "---";
            txt_PyCrd.Text = string.Empty;
            lbl_ResCa.Text = "---";
            lbl_ResCaR.Text = "---";

            txt_Tashviqi.Text = string.Empty;
            lbl_TashviqiR.Text = "---";
            txt_Karane.Text = string.Empty;
            lbl_KaraneR.Text = "---";
            lbl_Sum.Text = "---";
            lbl_SumR.Text = "---";
            lbl_totalS2.Text = "---";

            lbl_PR_Tax.Text = "---";
            lbl_ResTax.Text = "---";
            lbl_TaxR.Text = "---";
            lbl_InsuAllS.Text = "---";
            lbl_InsuAllR.Text = "---";
            lbl_InsDbtS.Text = "---";

            lbl_InsDbtR .Text = "---";
            txt_Insur.Text = string.Empty;
            lbl_TInsurKR.Text = "---";
            lbl_TInsurK.Text = "---";
            lbl_NoInsuS.Text = "---";

            lbl_TImprstK.Text = "---";
            lbl_TImprstKR.Text = "---";
            txt_ImprD.Text = string.Empty;
            lbl_ImprstS.Text = "---";
            lbl_ImprsR.Text = "---";
            lbl_TCrdK.Text = "---";
            lbl_TCrdKR.Text = "---";
            txt_CrdKsr.Text = string.Empty;
            lbl_CrdKS.Text = "---";
            lbl_CrdKR.Text = "---";
            lbl_TShrgK.Text = "---";
            lbl_TShargKR.Text = "---";
            txt_ShrgKsr.Text = string.Empty;
            lbl_ShrgKS.Text = "---";
            lbl_ShrgKR.Text = "---";
            lbl_TDebtK.Text = "---";
            lbl_TDebtKR.Text = "---";
            txt_RigDbt.Text = string.Empty;
            lbl_DebtS.Text = "---";
            lbl_DebtR.Text = "---";
            lbl_Kasr.Text = "---";
            lbl_KasrR.Text = "---";
            lbl_SumAllS.Text = "---";
            lbl_SumAllR.Text = "---";

            btn_CalKasr.Enabled = false;
            btn_RegPyRoll.Enabled = false;
            panel_Kasr.Enabled = false;
            errorPro_Sal.Clear();
        }

        private void btn_Ref_Click(object sender, EventArgs e)
        {
            Clear();
        }

       

       

        



        
       

       

       
      


       

       

       




       




    }
}
