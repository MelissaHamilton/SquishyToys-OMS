using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SquishyToys
{

    public partial class Customers : Form
    {
        Functions Con;
        public Customers()
        {
            InitializeComponent();
            Con = new Functions();
            ShowCustomers();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnCustomers.Cursor = Cursors.Hand;
            btnOrders.Cursor = Cursors.Hand;
            btnProducts.Cursor = Cursors.Hand;
            btnExit.Cursor = Cursors.Hand;
            btnClose.Cursor = Cursors.Hand;
        }

        private void ShowCustomers()
        {
            string Query = "Select * from SquishyToysDBCustomers";
            CustData.DataSource = Con.GetData(Query);
        }

        //DataGrid
        int Key = 0;
        private void CustData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustNameTextbox.Text = CustData.SelectedRows[0].Cells[1].Value.ToString();
            CustPhoneTextbox.Text = CustData.SelectedRows[0].Cells[2].Value.ToString();
            CustAddressTextbox.Text = CustData.SelectedRows[0].Cells[3].Value.ToString();
            if (CustNameTextbox.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CustData.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CustNameTextbox.Text == "" || CustPhoneTextbox.Text == "" || CustAddressTextbox.Text == "")
            {
                MessageBox.Show("Oops, Missing Data", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    string CName = CustNameTextbox.Text;
                    string CPhone = CustPhoneTextbox.Text;
                    string CAddress = CustAddressTextbox.Text;
                    string Query = "Insert into SquishyToysDBCustomers values('{0}','{1}','{2}')";
                    Query = string.Format(Query, CName, CPhone, CAddress);
                    Con.SetData(Query);
                    MessageBox.Show("Customer Added", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    ShowCustomers();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.Cursor = Cursors.Hand;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.Cursor = Cursors.Default;
        }




        private void UpdateBtn_Click(object sender, EventArgs e)
        {

            if (CustNameTextbox.Text == "" || CustPhoneTextbox.Text == "" || CustAddressTextbox.Text == "")
            {
                MessageBox.Show("Oops, Missing Data", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    string CName = CustNameTextbox.Text;
                    string CPhone = CustPhoneTextbox.Text;
                    string CAddress = CustAddressTextbox.Text;
                    string Query = "Update SquishyToysDBCustomers set [Customer Name] = '{0}',[Customer Phone] = '{1}',[Customer Address] = '{2}' where [Customer Code] = {3}";
                    Query = string.Format(Query, CName, CPhone, CAddress, Key);
                    Con.SetData(Query);
                    MessageBox.Show("Customer Added", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    ShowCustomers();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Clear()
        {
            CustNameTextbox.Text = "";
            CustPhoneTextbox.Text = "";
            CustAddressTextbox.Text = "";
            Key = 0;
        }
        private void DeleteBtn_Click(object sender, EventArgs e)
        {

            if (Key == 0)
            {
                MessageBox.Show("Oops, select a Customer", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    string Query = "delete from SquishyToysDBCustomers where [Customer Code] = {0}";
                    Query = string.Format(Query, Key);
                    Con.SetData(Query);
                    MessageBox.Show("Customer Deleted", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    ShowCustomers();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Customers
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            Obj.Show();
        }
        
        private void btnCustomers_MouseEnter(object sender, EventArgs e)
        {
            btnCustomers.Cursor = Cursors.Hand;
        }

        private void btnCustomers_MouseLeave(object sender, EventArgs e)
        {
            btnCustomers.Cursor = Cursors.Default;
        }


        //Products
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Products Obj = new Products();
            Obj.Show();
        }

        private void btnProducts_MouseEnter(object sender, EventArgs e)
        {
            btnProducts.Cursor = Cursors.Hand;
        }

        private void btnProducts_MouseLeave(object sender, EventArgs e)
        {
            btnProducts.Cursor = Cursors.Default;
        }


        //Orders
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Orders Obj = new Orders();
            Obj.Show();
        }

        private void btnOrders_MouseEnter(object sender, EventArgs e)
        {
            btnOrders.Cursor = Cursors.Hand;
        }

        private void btnOrders_MouseLeave(object sender, EventArgs e)
        {
            btnOrders.Cursor = Cursors.Default;
        }


        //Exit
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
