using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;




namespace SquishyToys
{
    public partial class Registration : Form

    {
      

        
        public Registration()
        {
            InitializeComponent();
        }


        private static string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }



        SqlConnection con = new SqlConnection();
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login logObj = new Login();
            logObj.Show();
            this.Hide();
        }


        //Parameters in SQL Query
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BiscuitDBConnection"].ToString()))
                {
                    con.Open();
                    string hashedPassword = HashPassword(textBoxPassword.Text);
                    SqlCommand cmd = new SqlCommand("INSERT INTO UserRegistration (FirstName, LastName, UserName, Password) VALUES (@FirstName, @LastName, @UserName, @Password)", con);

                    cmd.Parameters.AddWithValue("@FirstName", textBoxFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", textBoxLastName.Text);
                    cmd.Parameters.AddWithValue("@UserName", textBoxUserName.Text);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("You have successfully registered a new user.", "Registration Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    this.Hide(); // Hide the registration form
                    Login logObj = new Login();
                    logObj.FormClosed += (s, args) => this.Close(); // Ensure the registration form closes when the login form is closed
                    logObj.Show();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Failed to register user. Please check your network connection and try again.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            // Set the cursor for the close button
            btnClose.Cursor = Cursors.Hand;

            // Tooltips for password requirements or other fields
            ToolTip passwordTooltip = new ToolTip();
            passwordTooltip.SetToolTip(textBoxPassword, "Password must be at least 14 characters, include a number, and a special character.");

         
        }

    }
}
