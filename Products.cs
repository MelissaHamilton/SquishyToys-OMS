using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Core;

namespace SquishyToys
{

    public partial class Products : Form
    {
        Functions Con;
        public Products()
        {
            InitializeComponent();
            Con = new Functions();
            ShowProducts();
        }

        private void ShowProducts()
        {
            string Query = "Select * from SquishyToysDBProducts";
            ProductData.DataSource = Con.GetData(Query);
        }

        private void Products_Load(object sender, EventArgs e)
        {
            btnCustomers.Cursor = Cursors.Hand;
            btnOrders.Cursor = Cursors.Hand;
            btnProducts.Cursor = Cursors.Hand;
            btnExit.Cursor = Cursors.Hand;
            btnClose.Cursor = Cursors.Hand;
        }

        //DataGrid
        int Key = 0;
        private void ProductData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductNameTextbox.Text = ProductData.SelectedRows[0].Cells[1].Value.ToString();
            ProductCostTextbox.Text = ProductData.SelectedRows[0].Cells[2].Value.ToString();
            if (ProductNameTextbox.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ProductData.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            float cost;
            if (string.IsNullOrWhiteSpace(ProductNameTextbox.Text) || !float.TryParse(ProductCostTextbox.Text, out cost))
            {
                MessageBox.Show("Oops, missing data", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    string PName = ProductNameTextbox.Text;
                    cost = float.Parse(ProductCostTextbox.Text);


                    string Query = "Insert into SquishyToysDBProducts values('{0}',{1})";
                    Query = string.Format(Query, PName, cost);
                    Con.SetData(Query);
                    MessageBox.Show("Product Added", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    ShowProducts();
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
            ProductNameTextbox.Text = "";
            ProductCostTextbox.Text = "";
            Key = 0;
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





        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProductNameTextbox.Text) || !float.TryParse(ProductCostTextbox.Text, out float cost))
            {
                MessageBox.Show("Oops, missing data", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string PName = ProductNameTextbox.Text;
                string Query = "Delete SquishyToysDBProducts where [Product Code] = {0}";
                Query = string.Format(Query, Key);
                Con.SetData(Query);
                MessageBox.Show("Product Deleted", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                ShowProducts();
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProductNameTextbox.Text) || !float.TryParse(ProductCostTextbox.Text, out float cost))
            {
                MessageBox.Show("Oops, missing data", "Warning",MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string PName = ProductNameTextbox.Text;
                string Query = "Update SquishyToysDBProducts set [Product Name] = '{0}',[Product Cost] = {1} where [Product Code] = {2}";
                Query = string.Format(Query, PName, cost, Key);
                Con.SetData(Query);
                MessageBox.Show("Product Updated", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                ShowProducts();
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        //Customers
        private void btnCustomers_Click(object sender, EventArgs e)
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
        private void btnProducts_Click(object sender, EventArgs e)
        {
            Products Obj = new Products();
            Obj.Show();
        }

        private void btnProducts_MouseEnter(object sender, EventArgs e)
        {
            btnCustomers.Cursor = Cursors.Hand;
        }

        private void btnProducts_MouseLeave(object sender, EventArgs e)
        {
            btnCustomers.Cursor = Cursors.Default;
        }



        //Orders
        private void btnOrders_Click(object sender, EventArgs e)
        {
            Orders Obj = new Orders();
            Obj.Show();
        }

        private void btnOrders_MouseEnter(object sender, EventArgs e)
        {
            btnCustomers.Cursor = Cursors.Hand;
        }

        private void btnOrders_MouseLeave(object sender, EventArgs e)
        {
            btnCustomers.Cursor = Cursors.Default;
        }


        //Exit
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      


    }



}
