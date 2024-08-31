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
        string connString = "Host=localhost;Port=5432;Password=123456;Database=\"pbc_capstone\"";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void login_btn_Click(object sender, EventArgs e)
        {
            string loginUsername = exampleInputEmail.Text;
            string loginPass = exampleInputPassword.Text;

            using (var db = new NpgsqlConnection(connString))
            {
                db.Open();

                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM users WHERE username = @username";
                    cmd.Parameters.AddWithValue("@username", loginUsername);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHashedPassword = reader["password"].ToString();
                            string userRole = reader["role"].ToString();
                            int userId = Convert.ToInt32(reader["_id"]);
                            string userStatus = reader["status"].ToString();

                            if (PasswordHashMatches(loginPass, storedHashedPassword))
                            {
                                if (userStatus == "active")
                                {
                                    if (userRole == "admin")
                                    {
                                        Session["admin_username"] = loginUsername;
                                        Session["admin_password"] = storedHashedPassword;
                                        Session["admin_id"] = userId;
                                        Response.Redirect("Admin_Dashboard.aspx");
                                    }
                                    else if (userRole == "customer")
                                    {
                                        Session["customer_username"] = loginUsername;
                                        Session["customer_password"] = storedHashedPassword;
                                        Session["customer_id"] = userId;
                                        Response.Redirect("Customer_Dashboard.aspx");
                                    }
                                    else if (userRole == "customer")
                                    {
                                        Session["customer_username"] = loginUsername;
                                        Session["customer_password"] = storedHashedPassword;
                                        Session["customer_id"] = userId;
                                        Response.Redirect("Customer_Dashboard.aspx");
                                    }

                                }
                                else if (userStatus == "suspended")
                                {
                                    Response.Write("<script>alert('Your account is suspended!')</script>");
                                }
                                else
                                {
                                    Response.Write("<script>alert('Invalid Username or Password!')</script>");
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid Username or Password!')</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Invalid Username or Password!')</script>");
                        }
                    }
                }

                db.Close();
            }
        }

        private bool PasswordHashMatches(string enteredPassword, string storedHashedPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));

                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }

                return stringBuilder.ToString() == storedHashedPassword;
            }
        }

        protected void exampleInputPassword_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
