using System;
using System.Data;
using Npgsql;
using System.Security.Cryptography;
using System.Text;

namespace Capstone
{
    public partial class Login : System.Web.UI.Page
    {
        ////string connString = "Host=localhost;Port=5432;Password=123456;Database="PBC_CAPS";
        private readonly string con = "Server=localhost;Port=5432;User Id=postgres;Password=123456;Database=trashtrack";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        //protected void login_btn_Click(object sender, EventArgs e)
        //{
        //    string loginUsername = exampleInputEmail.Text;
        //    string loginPass = exampleInputPassword.Text;

        //    using (var db = new NpgsqlConnection(con))
        //    {
        //        db.Open();

        //        using (var cmd = db.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = "SELECT * FROM users WHERE username = @username";
        //            cmd.Parameters.AddWithValue("@username", loginUsername);

        //            using (NpgsqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    string storedHashedPassword = reader["password"].ToString();
        //                    string userRole = reader["role"].ToString();
        //                    int userId = Convert.ToInt32(reader["_id"]);
        //                    string userStatus = reader["status"].ToString();

        //                    if (PasswordHashMatches(loginPass, storedHashedPassword))
        //                    {
        //                        if (userStatus == "active")
        //                        {
        //                            if (userRole == "admin")
        //                            {
        //                                Session["admin_username"] = loginUsername;
        //                                Session["admin_password"] = storedHashedPassword;
        //                                Session["admin_id"] = userId;
        //                                Response.Redirect("Admin_Dashboard.aspx");
        //                            }
        //                            else if (userRole == "customer")
        //                            {
        //                                Session["customer_username"] = loginUsername;
        //                                Session["customer_password"] = storedHashedPassword;
        //                                Session["customer_id"] = userId;
        //                                Response.Redirect("Customer_Dashboard.aspx");
        //                            }
        //                            else if (userRole == "customer")
        //                            {
        //                                Session["customer_username"] = loginUsername;
        //                                Session["customer_password"] = storedHashedPassword;
        //                                Session["customer_id"] = userId;
        //                                Response.Redirect("Customer_Dashboard.aspx");
        //                            }

        //                        }
        //                        else if (userStatus == "suspended")
        //                        {
        //                            Response.Write("<script>alert('Your account is suspended!')</script>");
        //                        }
        //                        else
        //                        {
        //                            Response.Write("<script>alert('Invalid Username or Password!')</script>");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Response.Write("<script>alert('Invalid Username or Password!')</script>");
        //                    }
        //                }
        //                else
        //                {
        //                    Response.Write("<script>alert('Invalid Username or Password!')</script>");
        //                }
        //            }
        //        }

        //        db.Close();
        //    }
        //}

        protected void login_btn_Click(object sender, EventArgs e)
        {
            string loginEmail = exampleInputEmail.Text.Trim();
            string loginPass = exampleInputPassword.Text.Trim();

            int userId; // To hold the user ID after successful login
            string storedHashedPassword; // To store the hashed password from the database

            // Check if user is an Admin
            if (IsSAM(loginEmail, loginPass, out userId, out storedHashedPassword))
            {
                // Store necessary session data
                Session["sam_email"] = loginEmail;
                Session["sam_password"] = storedHashedPassword;
                Session["sam_id"] = userId;  // Store the user ID in the session

                // Redirect to Admin dashboard
                Response.Redirect("SAM_AccountMan.aspx");
                Response.Write("<script>alert('Login Successful!, WELCOME SUPER ACCOUNT MANAGER');</script>");
            }
            else if (IsAM(loginEmail, loginPass, out userId, out storedHashedPassword))
            {
                // Store necessary session data
                Session["am_email"] = loginEmail;
                Session["am_password"] = storedHashedPassword;
                Session["am_id"] = userId;  // Store the user ID in the session

                // Redirect to Admin dashboard
                Response.Redirect("AM_AccountMan.aspx");
                Response.Write("<script>alert('Login Successful!, WELCOME ACCOUNT MANAGER');</script>");
            }
            else if (IsBilling_Officer(loginEmail, loginPass))
            {
                Response.Write("<script>alert('Login Successful!, WELCOME BILLING OFFICER!');</script>");
            }
            else if (IsOperational_Dispatcher(loginEmail, loginPass))
            {
                Response.Write("<script>alert('Login Successful!, WELCOME OPERATIONAL DISPATCHER');</script>");
            }
            else
            {
                // Invalid login
                Response.Write("<script>alert('Login Failed. Invalid email or password.');</script>");
            }

            //else if(IsCustomer(loginUsername, loginPass))
            //{
            //    Response.Write("<script>alert('Login Successful!, WELCOME Contractual Customer!');</script>");
            //}

            //else if (IsHauler(loginUsername, loginPass))
            //{
            //    Response.Write("<script>alert('Login Successful!, WELCOME Hauler!');</script>");
            //}
            //else
            //{
            //    // Invalid login
            //    Response.Write("<script>alert('Login Failed. Invalid email or password.');</script>");
            //}
        }


        //FOR IsSuperAccountManager
        private bool IsSAM(string email, string password, out int userId, out string storedHashedPassword)
        {
            userId = 0; // Initialize userId
            storedHashedPassword = null; // Initialize storedHashedPassword

            try
            {
                using (var db = new NpgsqlConnection(con))
                {
                    db.Open();

                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;

                        // Query to get acc_id and acc_password for the provided email
                        cmd.CommandText = "SELECT emp_id, emp_password FROM employee WHERE emp_email = @email AND role_id = 2 AND emp_status = 'Active'";
                        cmd.Parameters.AddWithValue("@email", email);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userId = reader.GetInt32(0); // Get acc_id
                                storedHashedPassword = reader.GetString(1); // Get acc_password (hashed)

                                // Hash the provided password
                                string hashedPassword = HashPassword(password);

                                // Compare the hashed password with the stored one
                                if (hashedPassword == storedHashedPassword)
                                {
                                    return true; // Login successful
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

            return false; // Login failed
        }




        //FOR IsAdmin
        private bool IsAM(string email, string password, out int userId, out string storedHashedPassword)
        {
            userId = 0; // Initialize userId
            storedHashedPassword = null; // Initialize storedHashedPassword

            try
            {
                using (var db = new NpgsqlConnection(con))
                {
                    db.Open();

                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;

                        // Query to get acc_id and acc_password for the provided email
                        cmd.CommandText = "SELECT acc_id, acc_password FROM account_manager WHERE acc_email = @email AND acc_status = 'Active'";
                        cmd.Parameters.AddWithValue("@email", email);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userId = reader.GetInt32(0); // Get acc_id
                                storedHashedPassword = reader.GetString(1); // Get acc_password (hashed)

                                // Hash the provided password
                                string hashedPassword = HashPassword(password);

                                // Compare the hashed password with the stored one
                                if (hashedPassword == storedHashedPassword)
                                {
                                    return true; // Login successful
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

            return false; // Login failed
        }



        //FOR IS BILLING OFFICER
        private bool IsBilling_Officer(string email, string password)
        {
            try
            {
                using (var db = new NpgsqlConnection(con))
                {
                    db.Open();

                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        // Retrieve salt and hashed password from the database
                        cmd.CommandText = "SELECT BO_PASSWORD FROM BILLING_OFFICER WHERE BO_EMAIL = @email AND BO_STATUS = 'Active'";
                        cmd.Parameters.AddWithValue("@email", email);

                        using (NpgsqlDataReader read = cmd.ExecuteReader())
                        {
                            if (read.Read())
                            {
                                string storedHash = read.GetString(0);

                                // Hash the provided password with the stored salt
                                string hashedPassword = HashPassword(password);

                                // Compare the hashed password
                                if (hashedPassword == storedHash)
                                {
                                    //Sample session
                                    //Session["id"] = read["bo_id"].ToString();
                                    //Session["name"] = read["bo_name"].ToString();

                                    //Response.Redirect("Operational_Dispatcher.aspx");
                                    Response.Write("<script>alert('Login Successful!, Welcome BILLING OFFICER!');</script>");
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Message " + ex.Message + "');</script>");
            }

            return false;
        }


        //FOR IS OPERATIONAL DISPATCHER
        private bool IsOperational_Dispatcher(string email, string password)
        {
            try
            {
                using (var db = new NpgsqlConnection(con))
                {
                    db.Open();

                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        // Retrieve salt and hashed password from the database
                        cmd.CommandText = "SELECT OP_PASSWORD FROM OPERATIONAL_DISPATCHER WHERE OP_EMAIL = @email AND OP_STATUS = 'Active'";
                        cmd.Parameters.AddWithValue("@email", email);

                        using (NpgsqlDataReader read = cmd.ExecuteReader())
                        {
                            if (read.Read())
                            {
                                //string storedSalt = read.GetString(0);
                                string storedHash = read.GetString(0);

                                // Hash the provided password with the stored salt
                                string hashedPassword = HashPassword(password);

                                // Compare the hashed password
                                if (hashedPassword == storedHash)
                                {
                                    //var op_id = read[""];
                                    //Sample session
                                    //Session["id"] = read["op_id"].ToString();
                                    //Session["name"] = read["op_name"].ToString();

                                    //Response.Redirect("Operational_Dispatcher.aspx");

                                    Response.Write("<script>alert('Login Successful!, WELCOME OPERATIONAL DISPATCHER');</script>");


                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Message " + ex.Message + "');</script>");
            }

            return false;
        }


        ////FOR IsHauler
        //private bool IsHauler(string email, string password)
        //{
        //    try
        //    {
        //        using (var db = new NpgsqlConnection(con))
        //        {
        //            db.Open();

        //            using (var cmd = db.CreateCommand())
        //            {
        //                cmd.CommandType = CommandType.Text;
        //                // Retrieve salt and hashed password from the database
        //                cmd.CommandText = "SELECT HAUL_SALT, HAUl_PASSWORD FROM HAULER WHERE HAUL_EMAIL = @email AND HAUL_STATUS = 'Active'";
        //                cmd.Parameters.AddWithValue("@email", email);

        //                using (NpgsqlDataReader read = cmd.ExecuteReader())
        //                {
        //                    if (read.Read())
        //                    {
        //                        string storedSalt = read.GetString(0);
        //                        string storedHash = read.GetString(1);

        //                        // Hash the provided password with the stored salt
        //                        string hashedPassword = HashPassword(password);

        //                        // Compare the hashed password
        //                        if (hashedPassword == storedHash)
        //                        {
        //                            Response.Write("<script>alert('Login Successful!, WELCOME HAULER!');</script>");
        //                            return true;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<script>alert('Message " + ex.Message + "');</script>");
        //    }

        //    return false;
        //}

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var saltedPassword = password;
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

                // Convert the hashed bytes to a hexadecimal string
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }

        //private bool PasswordHashMatches(string enteredPassword, string storedHashedPassword)
        //{
        //    using (SHA256 sha256 = SHA256.Create())
        //    {
        //        byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));

        //        StringBuilder stringBuilder = new StringBuilder();
        //        foreach (byte b in hashedBytes)
        //        {
        //            stringBuilder.Append(b.ToString("x2"));
        //        }

        //        return stringBuilder.ToString() == storedHashedPassword;
        //    }
        //}

        protected void exampleInputPassword_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
