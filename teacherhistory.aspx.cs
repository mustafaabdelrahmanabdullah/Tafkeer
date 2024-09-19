using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Reporting.WebForms;

namespace endlessthoughts
{
    public partial class personsdatareport : System.Web.UI.Page
    {
        readonly String cstr = ConfigurationManager.ConnectionStrings["con"].ToString();
        DataSet1 ds = new DataSet1();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DropDownList1.Items.Add(string.Empty);

                using (SqlConnection con = new SqlConnection(cstr))
                {
                    SqlCommand getcolumns = new SqlCommand("SELECT chartname"+Convert.ToString(Session["langcode"])+ " FROM [endlessthinking].[dbo].[employees] WHERE branchcode = '"+Convert.ToInt64(Session["branchcode"]) +"'", con);
                    con.Open();
                    SqlDataReader rdr = getcolumns.ExecuteReader();
                    while(rdr.Read())
                    {
                        DropDownList1.Items.Add(Convert.ToString(rdr[0]));
                    }
                    rdr.Close();
                    con.Close();
                    getcolumns.Dispose();
                }
                DropDownList1.SelectedIndexChanged += DropDownList1_SelectedIndexChanged;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DropDownList1.Text))
            {
                using (SqlConnection con = new SqlConnection(cstr))
                {
                    SqlCommand getusercode = new SqlCommand("select usercode FROM [endlessthinking].[dbo].[employees] where chartname"+Convert.ToString(Session["langcode"])+" = N'"+DropDownList1.Text+"' and branchcode = '"+Convert.ToInt64(Session["branchcode"])+"'", con);
                    con.Open();
                    SqlDataReader getusercoderdr = getusercode.ExecuteReader();
                    while(getusercoderdr.Read())
                    {
                        TextBox1.Text = Convert.ToString(getusercoderdr[0]);
                    }
                    getusercoderdr.Close();

                    SqlCommand getreportbody = new SqlCommand("select * FROM [endlessthinking].[dbo].[employees] where usercode = '" + Convert.ToInt64(TextBox1.Text) + "' order by id", con);
                    SqlDataAdapter getreportbodyad = new SqlDataAdapter(getreportbody);
                    getreportbodyad.Fill(ds.Tables["availabledata"]);

                    ds.Tables["hrmovements"].Rows.Add();
                    ds.Tables["hrmovements"].Rows[0][0] = Convert.ToString(Session["langcode"]);

                    SqlCommand gethrmovements = new SqlCommand("select sum(dept),sum(credit) FROM [endlessthinking].[dbo].[codata] where usercode = '" + Convert.ToInt64(TextBox1.Text) + "' and branchcode = '"+Convert.ToInt64(Session["branchcode"]) +"' and currentyear = '"+Convert.ToInt64(Session["currentyear"])+ "' and chartnu = '2004002'", con);
                    SqlDataReader gethrmovementsrdr = gethrmovements.ExecuteReader();
                    while(gethrmovementsrdr.Read())
                    {
                        decimal val = 0;
                        decimal val2 = 0;

                        if(!gethrmovementsrdr.IsDBNull(0))
                        {
                            val = Convert.ToDecimal(gethrmovementsrdr[0]);
                        }
                        if (!gethrmovementsrdr.IsDBNull(1))
                        {
                            val2 = Convert.ToDecimal(gethrmovementsrdr[1]);
                        }
                        decimal res = val - val2;
                        ds.Tables["hrmovements"].Rows[0][1] = res.ToString("0.00");
                    }
                    gethrmovementsrdr.Close();

                    SqlCommand permis = new SqlCommand("select sum(dept),sum(credit) FROM [endlessthinking].[dbo].[codata] where usercode = '" + Convert.ToInt64(TextBox1.Text) + "' and branchcode = '" + Convert.ToInt64(Session["branchcode"]) + "' and currentyear = '" + Convert.ToInt64(Session["currentyear"]) + "' and chartnu = '2004008'", con);
                    SqlDataReader pirmsrdr = permis.ExecuteReader();
                    while (pirmsrdr.Read())
                    {
                        decimal val = 0;
                        decimal val2 = 0;

                        if (!pirmsrdr.IsDBNull(0))
                        {
                            val = Convert.ToDecimal(pirmsrdr[0]);
                        }
                        if (!pirmsrdr.IsDBNull(1))
                        {
                            val2 = Convert.ToDecimal(pirmsrdr[1]);
                        }
                        decimal res = val - val2;
                        ds.Tables["hrmovements"].Rows[0][2] = res.ToString("0.00");
                    }
                    pirmsrdr.Close();

                    SqlCommand absence = new SqlCommand("select sum(dept),sum(credit) FROM [endlessthinking].[dbo].[codata] where usercode = '" + Convert.ToInt64(TextBox1.Text) + "' and branchcode = '" + Convert.ToInt64(Session["branchcode"]) + "' and currentyear = '" + Convert.ToInt64(Session["currentyear"]) + "' and chartnu = '2004003'", con);
                    SqlDataReader absencerdr = absence.ExecuteReader();
                    while (absencerdr.Read())
                    {
                        decimal val = 0;
                        decimal val2 = 0;

                        if (!absencerdr.IsDBNull(0))
                        {
                            val = Convert.ToDecimal(absencerdr[0]);
                        }
                        if (!absencerdr.IsDBNull(1))
                        {
                            val2 = Convert.ToDecimal(absencerdr[1]);
                        }
                        decimal res = val - val2;
                        ds.Tables["hrmovements"].Rows[0][3] = res.ToString("0.00");
                    }
                    absencerdr.Close();

                    SqlCommand session = new SqlCommand("select sum(dept),sum(credit) FROM [endlessthinking].[dbo].[codata] where usercode = '" + Convert.ToInt64(TextBox1.Text) + "' and branchcode = '" + Convert.ToInt64(Session["branchcode"]) + "' and currentyear = '" + Convert.ToInt64(Session["currentyear"]) + "' and chartnu = '2004004001'", con);
                    SqlDataReader sessionrdr = session.ExecuteReader();
                    while (sessionrdr.Read())
                    {
                        decimal val = 0;
                        decimal val2 = 0;

                        if (!sessionrdr.IsDBNull(0))
                        {
                            val = Convert.ToDecimal(sessionrdr[0]);
                        }
                        if (!sessionrdr.IsDBNull(1))
                        {
                            val2 = Convert.ToDecimal(sessionrdr[1]);
                        }
                        decimal res = val - val2;
                        ds.Tables["hrmovements"].Rows[0][4] = res.ToString("0.00");
                    }
                    sessionrdr.Close();

                    SqlCommand duty = new SqlCommand("select sum(dept),sum(credit) FROM [endlessthinking].[dbo].[codata] where usercode = '" + Convert.ToInt64(TextBox1.Text) + "' and branchcode = '" + Convert.ToInt64(Session["branchcode"]) + "' and currentyear = '" + Convert.ToInt64(Session["currentyear"]) + "' and chartnu = '2004004002'", con);
                    SqlDataReader dutyrdr = duty.ExecuteReader();
                    while (dutyrdr.Read())
                    {
                        decimal val = 0;
                        decimal val2 = 0;

                        if (!dutyrdr.IsDBNull(0))
                        {
                            val = Convert.ToDecimal(dutyrdr[0]);
                        }
                        if (!dutyrdr.IsDBNull(1))
                        {
                            val2 = Convert.ToDecimal(dutyrdr[1]);
                        }
                        decimal res = val - val2;
                        ds.Tables["hrmovements"].Rows[0][5] = res.ToString("0.00");
                    }
                    dutyrdr.Close();

                    SqlCommand inside = new SqlCommand("select sum(dept),sum(credit) FROM [endlessthinking].[dbo].[codata] where usercode = '" + Convert.ToInt64(TextBox1.Text) + "' and branchcode = '" + Convert.ToInt64(Session["branchcode"]) + "' and currentyear = '" + Convert.ToInt64(Session["currentyear"]) + "' and chartnu = '2004004003'", con);
                    SqlDataReader insiderdr = inside.ExecuteReader();
                    while (insiderdr.Read())
                    {
                        decimal val = 0;
                        decimal val2 = 0;

                        if (!insiderdr.IsDBNull(0))
                        {
                            val = Convert.ToDecimal(insiderdr[0]);
                        }
                        if (!insiderdr.IsDBNull(1))
                        {
                            val2 = Convert.ToDecimal(insiderdr[1]);
                        }
                        decimal res = val - val2;
                        ds.Tables["hrmovements"].Rows[0][6] = res.ToString("0.00");
                    }
                    insiderdr.Close();

                    SqlCommand aca = new SqlCommand("select sum(dept),sum(credit) FROM [endlessthinking].[dbo].[codata] where usercode = '" + Convert.ToInt64(TextBox1.Text) + "' and branchcode = '" + Convert.ToInt64(Session["branchcode"]) + "' and currentyear = '" + Convert.ToInt64(Session["currentyear"]) + "' and chartnu = '2004005001'", con);
                    SqlDataReader acardr = aca.ExecuteReader();
                    while (acardr.Read())
                    {
                        decimal val = 0;
                        decimal val2 = 0;

                        if (!acardr.IsDBNull(0))
                        {
                            val = Convert.ToDecimal(acardr[0]);
                        }
                        if (!acardr.IsDBNull(1))
                        {
                            val2 = Convert.ToDecimal(acardr[1]);
                        }
                        decimal res = val - val2;
                        ds.Tables["hrmovements"].Rows[0][7] = res.ToString("0.00");
                    }
                    acardr.Close();

                    SqlCommand classvisit = new SqlCommand("select sum(dept),sum(credit) FROM [endlessthinking].[dbo].[codata] where usercode = '" + Convert.ToInt64(TextBox1.Text) + "' and branchcode = '" + Convert.ToInt64(Session["branchcode"]) + "' and currentyear = '" + Convert.ToInt64(Session["currentyear"]) + "' and chartnu = '2004005002'", con);
                    SqlDataReader classvisitrdr = classvisit.ExecuteReader();
                    while (classvisitrdr.Read())
                    {
                        decimal val = 0;
                        decimal val2 = 0;

                        if (!classvisitrdr.IsDBNull(0))
                        {
                            val = Convert.ToDecimal(classvisitrdr[0]);
                        }
                        if (!classvisitrdr.IsDBNull(1))
                        {
                            val2 = Convert.ToDecimal(classvisitrdr[1]);
                        }
                        decimal res = val - val2;
                        ds.Tables["hrmovements"].Rows[0][8] = res.ToString("0.00");
                    }
                    classvisitrdr.Close();

                    SqlCommand endterm = new SqlCommand("select sum(dept),sum(credit) FROM [endlessthinking].[dbo].[codata] where usercode = '" + Convert.ToInt64(TextBox1.Text) + "' and branchcode = '" + Convert.ToInt64(Session["branchcode"]) + "' and currentyear = '" + Convert.ToInt64(Session["currentyear"]) + "' and chartnu = '2004005003'", con);
                    SqlDataReader endrdr = endterm.ExecuteReader();
                    while (endrdr.Read())
                    {
                        decimal val = 0;
                        decimal val2 = 0;

                        if (!endrdr.IsDBNull(0))
                        {
                            val = Convert.ToDecimal(endrdr[0]);
                        }
                        if (!endrdr.IsDBNull(1))
                        {
                            val2 = Convert.ToDecimal(endrdr[1]);
                        }
                        decimal res = val - val2;
                        ds.Tables["hrmovements"].Rows[0][9] = res.ToString("0.00");
                    }
                    endrdr.Close();

                    SqlCommand comp = new SqlCommand("select sum(dept),sum(credit) FROM [endlessthinking].[dbo].[codata] where usercode = '" + Convert.ToInt64(TextBox1.Text) + "' and branchcode = '" + Convert.ToInt64(Session["branchcode"]) + "' and currentyear = '" + Convert.ToInt64(Session["currentyear"]) + "' and chartnu = '2004005004'", con);
                    SqlDataReader comprdr = comp.ExecuteReader();
                    while (comprdr.Read())
                    {
                        decimal val = 0;
                        decimal val2 = 0;

                        if (!comprdr.IsDBNull(0))
                        {
                            val = Convert.ToDecimal(comprdr[0]);
                        }
                        if (!comprdr.IsDBNull(1))
                        {
                            val2 = Convert.ToDecimal(comprdr[1]);
                        }
                        decimal res = val - val2;
                        ds.Tables["hrmovements"].Rows[0][10] = res.ToString("0.00");
                    }
                    comprdr.Close();

                    SqlCommand train = new SqlCommand("select sum(dept),sum(credit) FROM [endlessthinking].[dbo].[codata] where usercode = '" + Convert.ToInt64(TextBox1.Text) + "' and branchcode = '" + Convert.ToInt64(Session["branchcode"]) + "' and currentyear = '" + Convert.ToInt64(Session["currentyear"]) + "' and chartnu = '2004006'", con);
                    SqlDataReader trainrdr = train.ExecuteReader();
                    while (trainrdr.Read())
                    {
                        decimal val = 0;
                        decimal val2 = 0;

                        if (!trainrdr.IsDBNull(0))
                        {
                            val = Convert.ToDecimal(trainrdr[0]);
                        }
                        if (!trainrdr.IsDBNull(1))
                        {
                            val2 = Convert.ToDecimal(trainrdr[1]);
                        }
                        decimal res = val - val2;
                        ds.Tables["hrmovements"].Rows[0][11] = res.ToString("0.00");
                    }
                    trainrdr.Close();

                    con.Close();

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/employeehistory.rdlc");

                    ReportDataSource rds = new ReportDataSource("availabledata", ds.Tables["availabledata"]);
                    ReportDataSource rds2 = new ReportDataSource("hrmovements", ds.Tables["hrmovements"]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.DataSources.Add(rds2);
                    ReportViewer1.LocalReport.Refresh();
                }

                for (int u = 0; u < DropDownList1.Items.Count; u ++)
                {
                    if(string.IsNullOrEmpty(DropDownList1.Items[u].Text))
                    {
                        DropDownList1.Items.RemoveAt(u);
                    }
                }
            }
        }
    }
}