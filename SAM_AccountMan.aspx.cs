using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Security.Cryptography;
using System.Text;
using System.Data;

namespace Capstone
{
    public partial class Account_Manager_ManageAccount : System.Web.UI.Page
    {
        // Database Connection String
        private readonly string con = "Server=localhost;Port=5432;User Id=postgres;Password=123456;Database=trashtrack";
        protected void Page_Load(object sender, EventArgs e)
        {
            AccountManList();
        }



        protected void AccountManList()
        {
            using (var db = new NpgsqlConnection(con))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    // Modified the query to match the column names in the account_manager table
                    cmd.CommandText = "SELECT * FROM account_manager WHERE acc_status != 'Inactive' AND acc_id != @id ORDER BY acc_status";
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(Session["id"]));

                    DataTable admin_datatable = new DataTable();
                    NpgsqlDataAdapter admin_sda = new NpgsqlDataAdapter(cmd);
                    admin_sda.Fill(admin_datatable);

                    gridViewAccountMan.DataSource = admin_datatable;
                    gridViewAccountMan.DataBind();
                }

                db.Close();
            }
        }




        //// Registration of new admin
        //protected void register_Click(object sender, EventArgs e)
        //{
        //    // Get all the values fron the inputs
        //    string fname = adminfname.Value.Trim();
        //    string lname = adminlname.Value.Trim();
        //    string adminContact = admincontact.Value.Trim();
        //    string password = adminpass.Value.Trim();
        //    string salt = GenerateSalt();
        //    string hashedPass = HashPassword(password, salt);   // Hashed the inputted password

        //    string comfirmPass = comPass.Value;

        //    // Password confirmation
        //    if (comfirmPass != password)
        //    {

        //        adminfname.Value = string.Empty;
        //        adminlname.Value = string.Empty;
        //        admincontact.Value = string.Empty;
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
        //                "swal('Unmatched!', 'Unmatch Password!', 'error')", true);
        //        return;
        //    }

        //    // Check if the inputs are blank or not
        //    if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(adminContact) || string.IsNullOrEmpty(password))
        //    {
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
        //                "swal('Invalid!', 'Invalid Name or Password!', 'error')", true);
        //        return;
        //    }

        //    // Contact number validation
        //    if (!Regex.IsMatch(adminContact, "[0-9]{11}"))
        //    {
        //        // Display error message or handle validation failure
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
        //                "swal('Invalid!', 'Contact Number!', 'error')", true);
        //        return;
        //    }

        //    // Register and Store the new admin infos
        //    try
        //    {
        //        using (var db = new NpgsqlConnection(con))
        //        {
        //            db.Open();
        //            using (var cmd = db.CreateCommand())
        //            {
        //                cmd.CommandType = CommandType.Text;
        //                cmd.CommandText = "INSERT INTO ADMIN(ADMIN_FNAME, ADMIN_LNAME, ADMIN_CONTACT, ADMIN_PASS, SALT) " +
        //                    "VALUES (@fname, @lname, @contact, @pass, @salt)";
        //                cmd.Parameters.AddWithValue("@fname", fname);
        //                cmd.Parameters.AddWithValue("@lname", lname);
        //                cmd.Parameters.AddWithValue("@contact", adminContact);
        //                cmd.Parameters.AddWithValue("@pass", hashedPass);
        //                cmd.Parameters.AddWithValue("@salt", salt);

        //                var ctr = cmd.ExecuteNonQuery();
        //                if (ctr >= 1)
        //                {
        //                    AdminList();
        //                    adminfname.Value = string.Empty;
        //                    adminlname.Value = string.Empty;
        //                    admincontact.Value = string.Empty;
        //                    //Response.Write("<script>alert('New Employee account successfully registered!')</script>");
        //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
        //                        "swal('Account Regstered!', 'New admin account successfully registered!', 'success')", true);
        //                    // Delay the redirection for 2 seconds (2000 milliseconds)
        //                    ClientScript.RegisterStartupScript(this.GetType(), "redirect",
        //                        "setTimeout(function(){ window.location.href = '" + Request.Url.AbsoluteUri + "'; }, 2000);", true);
        //                }
        //                else
        //                {
        //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
        //                            "swal('Unsuccessfull!', 'No record registered!', 'error')", true);
        //                    // Delay the redirection for 2 seconds (2000 milliseconds)
        //                    ClientScript.RegisterStartupScript(this.GetType(), "redirect",
        //                        "setTimeout(function(){ window.location.href = '" + Request.Url.AbsoluteUri + "'; }, 2000);", true);
        //                }
        //            }

        //            db.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
        //                "swal('Unsuccessfull!', '" + ex.Message + "', 'error')", true);
        //    }
        //}



        // Generate Salt for password security
        protected string GenerateSalt()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var saltChars = new char[16];
            for (int i = 0; i < saltChars.Length; i++)
            {
                saltChars[i] = chars[random.Next(chars.Length)];
            }
            return new string(saltChars);
        }

        // Hashed the Password
        protected string HashPassword(string password, string salt)
        {
            // Combine the password and salt
            string combinedPassword = password + salt;

            // Choose the hash algorithm (SHA-256 or SHA-512)
            using (var sha256 = SHA256.Create())
            {
                // Convert the combined password string to a byte array
                byte[] bytes = Encoding.UTF8.GetBytes(combinedPassword);

                // Compute the hash value of the byte array
                byte[] hash = sha256.ComputeHash(bytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    result.Append(hash[i].ToString("x2"));
                }

                return result.ToString();
            }
        }


        protected void Suspendadmin_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int managerId = Convert.ToInt32(btn.CommandArgument);

            try
            {
                using (var db = new NpgsqlConnection(con))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE account_manager SET acc_status = 'Suspend' WHERE acc_id = @id";
                        cmd.Parameters.AddWithValue("@id", managerId);

                        var ctr = cmd.ExecuteNonQuery();
                        if (ctr >= 1)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                                "swal('Suspended!', 'Account Manager Suspended Successfully!', 'success')", true);
                            AccountManList();
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "swal('Unsuccessful!', '" + ex.Message + "', 'error')", true);
            }
        }



        // Unsuspension of the admin Action
        protected void Unsuspend_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int managerId = Convert.ToInt32(btn.CommandArgument);

            try
            {
                using (var db = new NpgsqlConnection(con))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE account_manager SET acc_status = 'Active' WHERE acc_id = @id";
                        cmd.Parameters.AddWithValue("@id", managerId);

                        var ctr = cmd.ExecuteNonQuery();
                        if (ctr >= 1)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                                "swal('Unsuspended!', 'Account Manager Unsuspended Successfully!', 'success')", true);
                            AccountManList();
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "swal('Unsuccessful!', '" + ex.Message + "', 'error')", true);
            }
        }


        // validate if the admin status is Suspend
        protected Boolean IsSuspended(string status)
        {
            return status == "Suspend";
        }

        // validate if the admin status is Unsuspend
        protected Boolean IsActive(string status)
        {
            return status == "Active";
        }

        // Deletion of the admin or update the status to Inactive if the admin is inactive anymore
        protected void Remove_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            int adminId = Convert.ToInt32(btn.CommandArgument);

            try
            {
                using (var db = new NpgsqlConnection(con))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE ADMIN SET ADMIN_STATUS = 'Inactive' WHERE ADMIN_ID = @id";
                        cmd.Parameters.AddWithValue("@id", adminId);

                        var ctr = cmd.ExecuteNonQuery();
                        if (ctr >= 1)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                                "swal('Account Removed!', 'Employee Account Removed Successfully!', 'success')", true);
                            AccountManList();
                        }
                    }

                    db.Close();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                        "swal('Unsuccessfull!', '" + ex.Message + "', 'error')", true);
            }
        }







    }
}