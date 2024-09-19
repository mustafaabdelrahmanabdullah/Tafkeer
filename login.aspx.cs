using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace endlessthoughts
{
    public partial class Login : System.Web.UI.Page
    {
        readonly String cstr = ConfigurationManager.ConnectionStrings["con"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
                Response.Cache.SetNoStore();

                Session["langcode"] = DropDownList1.SelectedValue;

                using (SqlConnection con = new SqlConnection(cstr))
                {
                    SqlCommand com = new SqlCommand("select chartname"+Convert.ToString(DropDownList1.SelectedValue) +" FROM [endlessthinking].[dbo].[mainchart] where chartnu like '1%' and relation > '1'", con);
                    con.Open();
                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                          DropDownList2.Items.Add(Convert.ToString(rdr[0]));
                    }
                    rdr.Close();
                    con.Close();
                    com.Dispose();
                }
                DropDownList1.Items.Add(string.Empty);
                DropDownList2.Items.Add(string.Empty);
                DropDownList1.SelectedIndex = DropDownList1.Items.Count -1;
                DropDownList2.SelectedIndex = DropDownList2.Items.Count - 1;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                if (!string.IsNullOrEmpty(DropDownList1.Text))
                {
                    Session["langcode"] = DropDownList1.SelectedValue;
                    DropDownList2.Items.Clear();
                    TextBox1.Text = string.Empty;
                    using (SqlConnection con = new SqlConnection(cstr))
                    {
                        SqlCommand com = new SqlCommand("select chartname"+Convert.ToString(Session["langcode"])+" FROM [endlessthinking].[dbo].[mainchart] where chartnu like '1%' and relation > 1", con);
                        con.Open();
                        SqlDataReader rdr = com.ExecuteReader();
                        while (rdr.Read())
                        {
                            DropDownList2.Items.Add(Convert.ToString(rdr[0]));
                        }
                        rdr.Close();
                        con.Close();
                        com.Dispose();
                    }
                    for (int u = 0; u < DropDownList1.Items.Count;u++)
                    {
                        if(DropDownList1.Items[u].Text == string.Empty)
                        {
                            DropDownList1.Items.RemoveAt(u);
                        }
                    }

                    if(DropDownList1.SelectedValue == "1002")
                    {
                        Label2.Text = "فرع";
                        Label3.Text = "اسم المستخدم";
                        Label4.Text = "كلمه المرور";
                    }
                    if(DropDownList1.SelectedValue == "1001")
                    {
                        Label2.Text = "Branch";
                        Label3.Text = "Username";
                        Label4.Text = "Password";
                    }
                }
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(cstr))
                {
                    SqlCommand com = new SqlCommand("select chartnu FROM [endlessthinking].[dbo].[mainchart] where chartname"+Convert.ToString(DropDownList1.SelectedValue)+" = N'"+DropDownList2.Text+"'", con);
                    con.Open();
                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        Session["branchcode"] = Convert.ToString(rdr[0]);
                    }
                    rdr.Close();
                    con.Close();
                    com.Dispose();
                }
                if (!string.IsNullOrEmpty(DropDownList2.Text))
                {
                    for (int u = 0; u < DropDownList2.Items.Count; u++)
                    {
                        if (DropDownList2.Items[u].Text == string.Empty)
                        {
                            DropDownList2.Items.RemoveAt(u);
                        }
                    }
                }
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (IsPostBack)
            {
                Label5.Visible = false;
                using (SqlConnection con = new SqlConnection(cstr))
                {
                    SqlCommand com = new SqlCommand("select chartname" + DropDownList1.SelectedValue + ",departmentcode,sectorcode,subjectcode,usercode,currentyear,available FROM [endlessthinking].[dbo].[employees] where username = @username and password = @password and branchcode = @branchcode", con);
                    com.Parameters.AddWithValue("@username", TextBox1.Text.Trim());
                    com.Parameters.AddWithValue("@password", TextBox2.Text.Trim());
                    com.Parameters.AddWithValue("@branchcode", Convert.ToInt64(Session["branchcode"]));
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if(dt.Rows.Count == 0)
                    {
                        Label5.BackColor = System.Drawing.Color.IndianRed;
                        Label5.Visible = true;
                        if (DropDownList1.SelectedValue == "1001")
                        {
                            Label5.Text = "Wrong Password or user name";
                        }
                        if (DropDownList1.SelectedValue == "1002")
                        {
                            Label5.Text = "اسم الدخول او كلمه المرور غير صحيحه";
                        }
                    }
                    if(dt.Rows.Count > 0)
                    {
                        Label5.Text = "Log In Successed ! Welcome " + Convert.ToString(dt.Rows[0][0]);
                        Label5.BackColor = System.Drawing.Color.ForestGreen;
                        Label5.Visible = true;
                        Session["fullname"] = Convert.ToString(dt.Rows[0][0]);
                        Session["departmentcode"] = Convert.ToString(dt.Rows[0][1]);
                        Session["sectorcode"] = Convert.ToString(dt.Rows[0][2]);
                        Session["subjectcode"] = Convert.ToString(dt.Rows[0][3]);
                        Session["usercode"] = Convert.ToString(dt.Rows[0][4]);
                        Session["currentyear"] = Convert.ToString(dt.Rows[0][5]);
                        Session["available"] = Convert.ToString(dt.Rows[0][6]);
                        Response.Redirect("Home.aspx", true);
                    }
                    con.Close();
                    com.Dispose();
                }
            }
        }
    }
}