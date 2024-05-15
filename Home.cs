using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SquishyToys
{
    public partial class Home : Form
    {
        //SESSION 1
        public Home()
        {
            InitializeComponent();
        }

        //Database Connection
        SqlConnection con = new SqlConnection();
        int ID;
        int rowID;
        private void Home_Load(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["BiscuitDBConnection"].ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select * from TablePhoneBook";
            //Executes the SQL command
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //Holds the content of the database table as a Dataset
            DataSet ds = new DataSet();

            sda.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["BiscuitDBConnection"].ToString();
                SqlCommand cmd = new SqlCommand("Insert into TablePhoneBook (PhoneNumber, FullName,Email,_Address,_Description) Values ('" + textBoxPhoneNumber.Text + "', '" + textBoxFullName.Text + "','" + textBoxEmail.Text + "', '" + textBoxAddress.Text + "', '" + textBoxDescription.Text + "')", con);
                cmd.Connection = con;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

               MessageBox.Show("Hurrah! You have successfully added the record to the database for the first time.", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                Home_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Saved " + ex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["BiscuitDBConnection"].ToString();
                SqlCommand cmd = new SqlCommand("Update TablePhoneBook set PhoneNumber='" + textBoxPhoneNumber.Text + "',FullName='" + textBoxFullName.Text + "',Email='" + textBoxEmail.Text + "',_Address='" + textBoxAddress.Text + "',_Description='" + textBoxDescription.Text + "' where id= '" + ID + "'", con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("You have successfully updated the selected record in the database.", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                Home_Load(null, null);


            }
            catch (Exception ex)
            {

                MessageBox.Show("You have successfully updated the selected record to the database.", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["BiscuitDBConnection"].ToString();
                SqlCommand cmd = new SqlCommand("Delete from TablePhoneBook where id= '" + ID + "'", con);
                cmd.Connection = con;


                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                Home_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("You have successfully deleted the selected record from the database.", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBoxPhoneNumber.Clear();
            textBoxFullName.Clear();
            textBoxEmail.Clear();
            textBoxAddress.Clear();
            textBoxDescription.Clear();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

   
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            ///////
            ////SESSION 2
            ///////


            //ID = The value is the selected id in the dgv
            //int ID = 23;
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    DataGridViewRow selectedRow = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                    ID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                }

                con.ConnectionString = ConfigurationManager.ConnectionStrings["BiscuitDBConnection"].ToString();
                SqlCommand cmd = new SqlCommand("Select * from TablePhoneBook where id= '" + ID + "'");
                cmd.Connection = con;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                rowID = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                textBoxPhoneNumber.Text = ds.Tables[0].Rows[0][1].ToString();
                textBoxFullName.Text = ds.Tables[0].Rows[0][2].ToString();
                textBoxEmail.Text = ds.Tables[0].Rows[0][3].ToString();
                textBoxAddress.Text = ds.Tables[0].Rows[0][4].ToString();
                textBoxDescription.Text = ds.Tables[0].Rows[0][5].ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
           // dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
