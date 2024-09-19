using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Runtime;

namespace endlessthoughts
{
    public partial class Home : System.Web.UI.Page
    {
        readonly String cstr = ConfigurationManager.ConnectionStrings["con"].ToString();
        DataSet1 ds = new DataSet1();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if (Convert.ToString(Session["langcode"]) == "1001")
                {
                    TextBox1.Text = "Branch Code";
                    TextBox2.Text = Convert.ToString(Session["branchcode"]);
                    TextBox3.Text = "Department Code";
                    TextBox4.Text = Convert.ToString(Session["departmentcode"]);
                    TextBox5.Text = "Sector Code";
                    TextBox6.Text = Convert.ToString(Session["sectorcode"]);
                    TextBox7.Text = "Subject Code";
                    TextBox8.Text = Convert.ToString(Session["subjectcode"]);
                    TextBox9.Text = "User Code";
                    TextBox10.Text = Convert.ToString(Session["usercode"]);
                    TextBox13.Text = "User Name";
                    TextBox14.Text = Convert.ToString(Session["fullname"]);
                    TextBox16.Text = "Branch Name";
                    TextBox18.Text = "Department Name";
                    TextBox20.Text = "Sector Name";
                    TextBox22.Text = "Subject Name";
                }
                if (Convert.ToString(Session["langcode"]) == "1002")
                {
                    TextBox1.Text = "كود الفرع";
                    TextBox2.Text = Convert.ToString(Session["branchcode"]);
                    TextBox3.Text = "كود قطاع";
                    TextBox4.Text = Convert.ToString(Session["departmentcode"]);
                    TextBox5.Text = "كود القسم";
                    TextBox6.Text = Convert.ToString(Session["sectorcode"]);
                    TextBox7.Text = "كود الماده";
                    TextBox8.Text = Convert.ToString(Session["subjectcode"]);
                    TextBox9.Text = "كود المستخدم";
                    TextBox10.Text = Convert.ToString(Session["usercode"]);
                    TextBox13.Text = "اسم المستخدم";
                    TextBox14.Text = Convert.ToString(Session["fullname"]);
                    TextBox16.Text = "اسم الفرع";
                    TextBox18.Text = "اسم القطاع";
                    TextBox20.Text = "اسم القسم";
                    TextBox22.Text = "اسم الماده";
                    Label1.Text = "اختر مهمه";
                    Label2.Text = "كود المهمه";
                }

                using (SqlConnection con = new SqlConnection(cstr))
                {
                    DropDownList1.Items.Add(string.Empty);

                    SqlCommand com = new SqlCommand("select task,owndepartment FROM [endlessthinking].[dbo].[posistiontask] where departmentcode = '" + Convert.ToInt64(Session["departmentcode"]) + "' and branchcode = '" + Convert.ToInt64(Session["branchcode"]) + "' and position = '" + Convert.ToInt64(Session["sectorcode"]) + "' and active = '1'", con);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    da.Fill(ds.Tables["maindrop1"]);
                    com.Dispose();

                    if (ds.Tables["maindrop1"].Rows.Count > 0)
                    {
                        for (int u = 0; u < ds.Tables["maindrop1"].Rows.Count; u++)
                        {
                            SqlCommand com2 = new SqlCommand("select chartname" + Convert.ToString(Session["langcode"]) + " FROM [endlessthinking].[dbo].[chart" + Convert.ToString(ds.Tables["maindrop1"].Rows[u][1]) + "] where chartnu = '" + Convert.ToInt64(ds.Tables["maindrop1"].Rows[u][0]) + "'", con);
                            SqlDataReader rdr2 = com2.ExecuteReader();
                            while (rdr2.Read())
                            {
                                DropDownList1.Items.Add(Convert.ToString(rdr2[0]));
                            }
                            rdr2.Close();
                        }
                    }

                    SqlCommand getbranchname = new SqlCommand("select chartname" + Convert.ToString(Session["langcode"]) + " FROM [endlessthinking].[dbo].[mainchart] where chartnu = '" + Convert.ToInt64(TextBox2.Text) + "'", con);
                    SqlDataReader getbranchnamerdr = getbranchname.ExecuteReader();
                    while (getbranchnamerdr.Read())
                    {
                        TextBox17.Text = Convert.ToString(getbranchnamerdr[0]);
                    }
                    getbranchnamerdr.Close();
                    getbranchname.Dispose();

                    SqlCommand departmentname = new SqlCommand("select chartname" + Convert.ToString(Session["langcode"]) + " FROM [endlessthinking].[dbo].[mainchart] where chartnu = '" + Convert.ToInt64(TextBox4.Text) + "'", con);
                    SqlDataReader departmentnamerdr = departmentname.ExecuteReader();
                    while (departmentnamerdr.Read())
                    {
                        TextBox19.Text = Convert.ToString(departmentnamerdr[0]);
                    }
                    departmentnamerdr.Close();
                    departmentname.Dispose();

                    SqlCommand sectorname = new SqlCommand("select chartname" + Convert.ToString(Session["langcode"]) + " FROM [endlessthinking].[dbo].[mainchart] where chartnu = '" + Convert.ToInt64(TextBox6.Text) + "'", con);
                    SqlDataReader sectornamerdr = sectorname.ExecuteReader();
                    while (sectornamerdr.Read())
                    {
                        TextBox21.Text = Convert.ToString(sectornamerdr[0]);
                    }
                    sectornamerdr.Close();
                    sectorname.Dispose();

                    if (!string.IsNullOrEmpty(TextBox8.Text))
                    {
                        SqlCommand subjectname = new SqlCommand("select chartname" + Convert.ToString(Session["langcode"]) + " FROM [endlessthinking].[dbo].[mainchart] where chartnu = '" + Convert.ToInt64(TextBox8.Text) + "'", con);
                        SqlDataReader subjectnamerdr = subjectname.ExecuteReader();
                        while (subjectnamerdr.Read())
                        {
                            TextBox23.Text = Convert.ToString(subjectnamerdr[0]);
                        }
                        sectornamerdr.Close();
                        subjectname.Dispose();
                    }
                    con.Close();
                }
                DropDownList1.SelectedIndexChanged += new EventHandler(DropDownList1_SelectedIndexChanged);
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DropDownList1.Text))
            {
                using (SqlConnection con = new SqlConnection(cstr))
                {                   
                    con.Open();
                    SqlCommand comds = new SqlCommand("select task,owndepartment FROM [endlessthinking].[dbo].[posistiontask] where departmentcode = '" + Convert.ToInt64(Session["departmentcode"]) + "' and branchcode = '" + Convert.ToInt64(Session["branchcode"]) + "' and position = '" + Convert.ToInt64(Session["sectorcode"]) + "' and active = '1'", con);
                    SqlDataAdapter dads = new SqlDataAdapter(comds);
                    dads.Fill(ds.Tables["maindrop1"]);
                    comds.Dispose();

                    //get jobdiscription
                    SqlCommand com2 = new SqlCommand("select chartnu FROM [endlessthinking].[dbo].[chart" + Convert.ToString(ds.Tables["maindrop1"].Rows[DropDownList1.SelectedIndex -1][1]) + "] where chartname" + Convert.ToString(Session["langcode"]) + " = N'" + DropDownList1.Text + "'", con);
                    Int64 departcode = 0;
                    SqlDataReader rdr2 = com2.ExecuteReader();
                    while (rdr2.Read())
                    {
                        departcode = Convert.ToInt64(rdr2[0]);
                        Session["chartnu"] = Convert.ToInt64(rdr2[0]);
                        Session["owndepartment"] = ds.Tables["maindrop1"].Rows[DropDownList1.SelectedIndex-1][1];
                    }
                    rdr2.Close();

                    SqlCommand com = new SqlCommand("select reportname,task,tablename FROM [endlessthinking].[dbo].[posistiontask] where task = '" + departcode + "'", con);
                    SqlDataReader rdr = com.ExecuteReader();
                    DropDownList1.Items.Add(string.Empty);
                    while (rdr.Read())
                    {
                        iframeWebpage.Attributes["src"] = "~/" + Convert.ToString(rdr[0]) + ".aspx";
                        TextBox11.Text = Convert.ToString(rdr[1]);
                        Session["tablename"] = Convert.ToString(rdr[2]);
                    }
                    rdr.Close();
                    com.Dispose();
                    con.Close();
                    UpdatePanel1.Update();

                    for (int u = 0; u < DropDownList1.Items.Count; u++)
                    {
                        if (DropDownList1.Items[u].Text == string.Empty)
                        {
                            DropDownList1.Items.RemoveAt(u);
                        }
                    }
                }
            }
        }
    }
}