using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SquishyToys.Utilities;




namespace SquishyToys
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRegisterLoginForm_Click(object sender, EventArgs e)
        {
            Registration regObj = new Registration();
            regObj.Show();
            this.Hide();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Properties.Settings.Default.RememberUserName;
            txtUsername.Text = Properties.Settings.Default.RememberUserName ? Properties.Settings.Default.UserName : "";

            ToolTip passwordTooltip = new ToolTip();
            passwordTooltip.SetToolTip(txtPassword, "Password must be at least 14 characters, include a number, and a special character.");
        }

      

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BiscuitDBConnection"].ToString()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Password FROM UserRegistration WHERE UserName = @UserName", con);
                    cmd.Parameters.AddWithValue("@UserName", txtUsername.Text.Trim());

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader["Password"].ToString();
                            try
                            {
                                if (AuthenticationHelper.ValidatePassword(txtPassword.Text, storedHash))
                                {
                                    MessageBox.Show("Login successful.", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                   
                                    // Hide the current Login form
                                    this.Hide();
                                    // Create and show the Orders form
                                    Orders ordersForm = new Orders();
                                    ordersForm.FormClosed += (s, args) => this.Close(); // Close this form when Orders form is closed
                                    ordersForm.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception ex)
                            {
                                // Handle specific exceptions related to password validation if any
                                MessageBox.Show("An error occurred during password validation. Please contact support.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                // Log the exception details for further analysis
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error connecting to the database: " + ex.Message, "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Optionally log this exception to a file or logging service
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Log general exceptions for debugging and maintenance
            }
        }

        private void pictureBoxClose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration regObj = new Registration();
            regObj.Show();
            this.Hide();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // Optional: handle password visibility toggle
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            Properties.Settings.Default.UserName = checkBox1.Checked ? txtUsername.Text : "";
            Properties.Settings.Default.RememberUserName = checkBox1.Checked;
            Properties.Settings.Default.Save();
        }
    }
}