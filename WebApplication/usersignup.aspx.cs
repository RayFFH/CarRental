using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{

    public partial class usersignup : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        // sign up button click event
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkMemberExists())
            {

                Response.Write("<script>alert('Member Already Exist with this Member ID, try other ID');</script>");
            }
            else
            {
                signUpNewMember();
            }
        }

        // user defined method
        bool checkMemberExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from member_master_tb1 where member_id='" + TextBox8.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
        void signUpNewMember()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    // Log message to indicate successful database connection
                    Debug.WriteLine("Connected to the database successfully.");

                    SqlCommand cmd = new SqlCommand("INSERT INTO member_master_tb1 (full_name, contact_no, email, state, city, full_address, member_id, password, account_status) VALUES (@full_name, @contact_no, @email, @state, @city, @full_address, @member_id, @password, @account_status)", con);

                    cmd.Parameters.AddWithValue("@full_name", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@contact_no", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@city", TextBox6.Text.Trim());
                    cmd.Parameters.AddWithValue("@full_address", TextBox5.Text.Trim());
                    cmd.Parameters.AddWithValue("@member_id", TextBox8.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", TextBox9.Text.Trim());
                    cmd.Parameters.AddWithValue("@account_status", "pending");

                    cmd.ExecuteNonQuery();

                    // Log message to indicate successful sign-up
                    Debug.WriteLine("Sign Up Successful.");

                    // Display message in the browser to indicate successful sign-up
                    Response.Write("<script>alert('Sign Up Successful. Go to User Login to Login');</script>");
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                // Log the SQL exception details
                Debug.WriteLine("SQL Exception Details:");
                Debug.WriteLine($"Number: {sqlEx.Number}");
                Debug.WriteLine($"Message: {sqlEx.Message}");
                Debug.WriteLine($"State: {sqlEx.State}");
                Debug.WriteLine($"Procedure: {sqlEx.Procedure}");
                Debug.WriteLine($"LineNumber: {sqlEx.LineNumber}");
                Debug.WriteLine($"ErrorCode: {sqlEx.ErrorCode}");
                Debug.WriteLine($"StackTrace: {sqlEx.StackTrace}");

                // Display a user-friendly error message
                string errorMessage = "An error occurred during sign-up. Please contact the administrator for assistance. Details: " + sqlEx.Message;
                Response.Write("<script>alert('" + errorMessage + "');</script>");
            }
            catch (Exception ex)
            {
                // Log other exception details
                Debug.WriteLine("Exception Details:");
                Debug.WriteLine($"Type: {ex.GetType().Name}");
                Debug.WriteLine($"Message: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");

                // Display a user-friendly error message
                string errorMessage = "An unexpected error occurred during sign-up. Please contact the administrator for assistance. Details: " + ex.Message;
                Response.Write("<script>alert('" + errorMessage + "');</script>");
            }
        }

    }

    }




