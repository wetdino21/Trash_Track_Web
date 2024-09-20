using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Text.RegularExpressions;
using Npgsql;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Text.Json;
using System.IO;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Xml.Linq;

namespace Capstone
{
    public partial class SAM_AccountSettings : System.Web.UI.Page
    {
        // Database Connection String
        private readonly string con = "Server=localhost;Port=5432;User Id=postgres;Password=123456;Database=trashtrack";

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the hashed bytes to a hexadecimal string
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    stringBuilder.Append(hashedBytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }

        protected void changepassword_Click(object sender, EventArgs e)
        {
            // Get admin ID from session
            int samId = (int)Session["sam_id"];


            //// Store necessary session data
            //Session["sam_email"] = loginEmail;
            //Session["sam_password"] = storedHashedPassword;
            //Session["sam_id"] = userId;  // Store the user ID in the session


            string currentPass = currentpassword.Text.Trim();
            string newPass = changetxt.Text.Trim();

            // Hash the entered current password
            string hashedCurrentPassword = HashPassword(currentPass);

            using (var db = new NpgsqlConnection(con))
            {
                db.Open();

                // Query to get the current password for the user
                string selectQuery = "SELECT acc_password FROM account_manager WHERE acc_id = @adminId";
                using (var cmdSelect = new NpgsqlCommand(selectQuery, db))
                {
                    cmdSelect.Parameters.AddWithValue("@adminId", samId);

                    var storedPassword = cmdSelect.ExecuteScalar()?.ToString();

                    // Check if the current password matches the stored password
                    if (storedPassword == hashedCurrentPassword)
                    {
                        // Passwords match, update the password
                        string hashedNewPassword = HashPassword(newPass);

                        // Query to update the password
                        string updateQuery = "UPDATE account_manager SET acc_password = @newPassword WHERE acc_id = @adminId";
                        using (var cmdUpdate = new NpgsqlCommand(updateQuery, db))
                        {
                            cmdUpdate.Parameters.AddWithValue("@newPassword", hashedNewPassword);
                            cmdUpdate.Parameters.AddWithValue("@adminId", samId);

                            int rowsAffected = cmdUpdate.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Password updated successfully
                                Response.Write("<script>alert('Password changed successfully!')</script>");
                            }
                            else
                            {
                                // Error updating the password
                                Response.Write("<script>alert('Error: Unable to update password!')</script>");
                            }
                        }
                    }
                    else
                    {
                        // Current password does not match the stored password
                        Response.Write("<script>alert('Error: Incorrect current password!')</script>");
                    }
                }
            }
        }

    }
}