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
using System.Net;

namespace endlessthoughts
{
    public partial class Recordreport : System.Web.UI.Page
    {
        readonly String cstr = ConfigurationManager.ConnectionStrings["con"].ToString();

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

                await GetEgyptTime();
                DropDownList1.Items.Add(string.Empty);
                using (SqlConnection con = new SqlConnection(cstr))
                {
                    SqlCommand getnames = new SqlCommand("select chartname" + Convert.ToString(Session["langcode"]) + " FROM [endlessthinking].[dbo].[" + Convert.ToString(Session["tablename"]) + "] where sectorcode >= '3005' and branchcode = '" + Convert.ToInt64(Session["branchcode"]) + "'", con);
                    con.Open();
                    SqlDataReader rdr = getnames.ExecuteReader();
                    while (rdr.Read())
                    {
                        DropDownList1.Items.Add(Convert.ToString(rdr[0]));
                    }
                    rdr.Close();
                    getnames.Dispose();
                    con.Close();
                }

                DropDownList1.SelectedIndexChanged += DropDownList1_SelectedIndexChanged;
                ImageButton1.Click += ImageButton1_Click;
                ImageButton2.Click += ImageButton2_Click;
            }

            if(IsPostBack)
            {
                if (Session["datetimenow"] != null)
                {
                    DateTime fetchedDate = Convert.ToDateTime(Session["datetimenow"]);

                    // Set the current date as the fetched date
                    Calendar1.TodaysDate = fetchedDate;

                    // Set the selected date to highlight the fetched date
                    Calendar1.SelectedDate = fetchedDate;
                }
            }
        }

        private void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DropDownList1.Text))
            {
                using (SqlConnection con = new SqlConnection(cstr))
                {
                    SqlCommand getnames = new SqlCommand("select usercode FROM [endlessthinking].[dbo].["+Convert.ToString(Session["tablename"])+"] where chartname" + Convert.ToString(Session["langcode"]) + " = N'" + DropDownList1.Text + "'", con);
                    con.Open();
                    SqlDataReader rdr = getnames.ExecuteReader();
                    while (rdr.Read())
                    {
                        TextBox1.Text = Convert.ToString(rdr[0]);
                    }
                    rdr.Close();
                    getnames.Dispose();
                    con.Close();
                }
                Getrecnu();
            }
        }

        private void Getrecnu()
        {
            if (IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(cstr))
                {
                    SqlCommand getrecnu = new SqlCommand("select max(recnu) FROM [endlessthinking].[dbo].[codata] where departmentcode = @departmentcode and branchcode = @branchcode and currentyear = @currentyear", con);
                    getrecnu.Parameters.AddWithValue("@departmentcode", Convert.ToInt64(Session["owndepartment"]));
                    getrecnu.Parameters.AddWithValue("@branchcode", Convert.ToInt64(Session["branchcode"]));
                    getrecnu.Parameters.AddWithValue("@currentyear", Convert.ToInt64(Session["currentyear"]));
                    con.Open();
                    SqlDataReader getrecnurdr = getrecnu.ExecuteReader();
                    if (getrecnurdr.HasRows) // Check if any rows are returned
                    {
                        TextBox4.ReadOnly = false;

                        while (getrecnurdr.Read())
                        {
                            if (getrecnurdr.IsDBNull(0))
                            {
                                TextBox4.Text = "1";
                            }
                            else
                            {
                                Int64 currdr = Convert.ToInt64(getrecnurdr[0]) + 1;
                                TextBox4.Text = Convert.ToString(currdr);
                            }
                        }
                        TextBox4.ReadOnly = true;

                    }
                    else
                    {
                        Label5.Text = "No records found for given conditions.";
                    }
                    getrecnurdr.Close();
                    con.Close();
                }
            }
        }


        private static readonly HttpClient client = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(60) // Set the timeout once at the beginning
        };

        public async Task GetEgyptTime()
        {
            string worldTimeApiUrl = "https://worldtimeapi.org/api/timezone/Africa/Cairo";
            string timeZoneDbUrl = "http://api.timezonedb.com/v2.1/get-time-zone?key=YOUR_TIMEZONEDB_API_KEY&format=json&by=zone&zone=Africa/Cairo";
            string timeApiUrl = "https://timeapi.io/api/Time/current/zone?timeZone=Africa/Cairo";

            int retryCount = 3;
            TimeSpan delayBetweenRetries = TimeSpan.FromSeconds(5); // Delay between retries

            // Try WorldTimeAPI
            if (await TryGetTimeFromApi(worldTimeApiUrl, retryCount, delayBetweenRetries)) return;

            // Try TimeZoneDB
            if (await TryGetTimeFromApi(timeZoneDbUrl, retryCount, delayBetweenRetries, "TimeZoneDB")) return;

            // Try TimeAPI.io
            if (await TryGetTimeFromApi(timeApiUrl, retryCount, delayBetweenRetries)) return;

            Label5.Text = "Error: Failed to retrieve Egypt time after multiple attempts with all APIs.";
            Label5.BackColor = System.Drawing.Color.Red;
        }

        private async Task<bool> TryGetTimeFromApi(string apiUrl, int retryCount, TimeSpan delayBetweenRetries, string apiName = "WorldTimeAPI")
        {
            for (int attempt = 1; attempt <= retryCount; attempt++)
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl); // Reuse the same HttpClient instance
                    response.EnsureSuccessStatusCode();

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);

                    // Extract datetime based on API
                    DateTime egyptDateTime;
                    if (apiName == "TimeZoneDB")
                    {
                        // TimeZoneDB API returns timestamp in seconds
                        int timestamp = data.timestamp;
                        egyptDateTime = DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
                    }
                    else
                    {
                        // Both WorldTimeAPI and TimeAPI.io return a datetime field
                        egyptDateTime = data.datetime ?? data.dateTime;
                    }

                    Session["datetimenow"] = egyptDateTime;
                    TextBox3.Text = egyptDateTime.ToString("yyyy-MM-dd");
                    return true; // Exit once successful
                }
                catch (HttpRequestException ex) when (attempt < retryCount)
                {
                    Label5.Text = $"Error during request from {apiName}: " + ex.Message;
                    await Task.Delay(delayBetweenRetries);
                }
                catch (TaskCanceledException ex) when (attempt < retryCount)
                {
                    Label5.Text = $"{apiName} request timed out: " + ex.Message;
                    await Task.Delay(delayBetweenRetries);
                }
                catch (Exception ex)
                {
                    Label5.Text = $"Unexpected error from {apiName}: " + ex.Message;
                    Label5.BackColor = System.Drawing.Color.Red;
                    return false; // Exit in case of an unexpected error
                }
            }

            return false; // If all retries failed, return false
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            DropDownList1.Items.Add(string.Empty);
            DropDownList1.Text = string.Empty;
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, GetType(), "ResetControls", "resetControls();", true);
            FileUpload1.Enabled = true;
            ImageButton1.Enabled = true;
            Button1.Enabled = true;
            TextBox2.Enabled = true;
            ImageButton2.Enabled = false;
            Label5.Text = "Statue";
            Label5.BackColor = System.Drawing.Color.WhiteSmoke;
        }

        protected async void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Getrecnu();

            await GetEgyptTime();

            Byte[] imageBytes = null;

            if (FileUpload1.HasFile)
            {
                using (BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream))
                {
                    imageBytes = br.ReadBytes((int)FileUpload1.PostedFile.InputStream.Length);
                }
            }

            try
            {
                using (SqlConnection con = new SqlConnection(cstr))
                {
                    SqlCommand savereport = new SqlCommand("insert into [endlessthinking].[dbo].[codata] (branchcode, departmentcode, sector, dept, chartnu, comment, recnu, recdate, date, usercode, atcode, currentyear, img) values (@branchcode, @departmentcode, @sector, @dept, @chartnu, @comment, @recnu, @recdate, @date, @usercode, @atcode, @currentyear, @img)", con);
                    savereport.Parameters.AddWithValue("@branchcode", Convert.ToInt64(Session["branchcode"]));
                    savereport.Parameters.AddWithValue("@departmentcode", Convert.ToInt64(Session["owndepartment"]));
                    savereport.Parameters.AddWithValue("@sector", Convert.ToInt64(Session["sectorcode"]));
                    savereport.Parameters.AddWithValue("@dept", 1);
                    savereport.Parameters.AddWithValue("@chartnu", Convert.ToInt64(Session["chartnu"]));
                    savereport.Parameters.AddWithValue("@comment", TextBox2.Text.Trim());
                    savereport.Parameters.AddWithValue("@recnu", Convert.ToInt64(TextBox4.Text));
                    savereport.Parameters.AddWithValue("@recdate", Convert.ToDateTime(Session["datetimenow"]));
                    savereport.Parameters.AddWithValue("@date", Calendar1.SelectedDate);
                    savereport.Parameters.AddWithValue("@usercode", Convert.ToInt64(Session["usercode"]));
                    savereport.Parameters.AddWithValue("@atcode", Convert.ToInt64(TextBox1.Text));
                    savereport.Parameters.AddWithValue("@currentyear", Convert.ToInt64(Session["currentyear"]));
                    savereport.Parameters.AddWithValue("@img", imageBytes ?? (object)DBNull.Value);
                    con.Open();
                    savereport.ExecuteNonQuery();
                    con.Close();
                }

                FileUpload1.Enabled = false;
                ImageButton1.Enabled = false;
                Button1.Enabled = false;
                TextBox2.Enabled = false;
                ImageButton2.Enabled = true;
                Label5.Text = "All data has been saved successfully";
                Label5.BackColor = System.Drawing.Color.LimeGreen;
            }
            catch (Exception ex)
            {
                Label5.Text = "Error: " + ex.Message;
                Label5.BackColor = System.Drawing.Color.Red;
            }
        }
    }
}