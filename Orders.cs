using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SquishyToys
{
    public partial class Orders : Form
    {
        Functions Con;
        public Orders()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Dpi;
            Con = new Functions();
            ShowOrders();
            GetCustomer();
            GetProducts();
        }

        private void GetCost()
        {
            string Query = "Select * from SquishyToysDBProducts where [Product Code] = {0}";
            Query = string.Format(Query, ProductCb.SelectedValue.ToString());
            foreach (DataRow dr in Con.GetData(Query).Rows)
            {
                decimal cost;
                if (decimal.TryParse(dr["Product Cost"].ToString(), out cost))
                {
                    ProdCostTextbox.Text = cost.ToString();
                }
                else
                {
                    MessageBox.Show("The product cost is not a valid floating point number.");
                }
            }
            // MessageBox.Show("Hello");
        }


        private void GetCustomer()
        {
            string Query = "Select * from SquishyToysDBCustomers";
            CustomerCb.DisplayMember = Con.GetData(Query).Columns["Customer Name"].ToString();
            CustomerCb.ValueMember = Con.GetData(Query).Columns["Customer Code"].ToString();
            CustomerCb.DataSource = Con.GetData(Query);

        }

        private void GetProducts() //SPareTbl with SPName and SPCOst
        {
            string Query = "Select * from SquishyToysDBProducts";
            ProductCb.DisplayMember = Con.GetData(Query).Columns["Product Name"].ToString();
            ProductCb.ValueMember = Con.GetData(Query).Columns["Product Code"].ToString();
            ProductCb.DataSource = Con.GetData(Query);

        }

        private void ShowOrders()
        {
            string Query = "Select * from .SquishyToysDBOrders";
            OrderData.DataSource = Con.GetData(Query);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
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


        private void SaveBtn_Click(object sender, EventArgs e)
        {
            decimal model;
            decimal cost;
            decimal repair;
            if (CustomerCb.SelectedIndex == -1 || string.IsNullOrWhiteSpace(PhoneTextbox.Text) || string.IsNullOrWhiteSpace(ProductTextbox.Text) || !decimal.TryParse(ModelTextbox.Text, out model) || ProductCb.SelectedIndex == -1 || !decimal.TryParse(ProdCostTextbox.Text, out cost) || !decimal.TryParse(RepairTextbox.Text, out repair))
                
            {
                MessageBox.Show("Oops, Missing Data",  "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    string ODate = OrderDateTextbox.Value.Date.ToString();
                    int Customer = Convert.ToInt32(CustomerCb.SelectedValue.ToString());
                    string CPhone = PhoneTextbox.Text;
                    cost = decimal.Parse(ProdCostTextbox.Text);
                    model = decimal.Parse(ModelTextbox.Text);
                    repair = decimal.Parse(RepairTextbox.Text);

                    string ProductName = ProductTextbox.Text;
                    //int ModelNumber = Convert.ToInt32(ModelTextbox.Text);
                    int ProductType = Convert.ToInt32(ProductCb.SelectedValue.ToString());
                    //int ProductCost = Convert.ToInt32(ProdCostTextbox.Text);
                    //int Total = Convert.ToInt32(TotalCostTextbox.Text);
                    decimal GrdTotal = cost + repair;
                    string Query = "Insert into SquishyToysDBOrders values('{0}',{1},'{2}','{3}',{4},'{5}',{6},{7})";
                    Query = string.Format(Query,ODate, Customer, CPhone, ProductName, model, ProductType, cost, GrdTotal);
                    Con.SetData(Query);
                    MessageBox.Show("Order Added", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    ShowOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }



        private void UpdateBtn_Click(object sender, EventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)

            {
                MessageBox.Show("Oops, Select an Order", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                   
                    string Query = "Delete from SquishyToysDBOrders where [Order Code] = {0}";
                    Query = string.Format(Query, Key);
                    Con.SetData(Query);
                    MessageBox.Show("Order Deleted", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    ShowOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int Key = 0;

      

        private void Orders_Load(object sender, EventArgs e)
        {
            btnCustomers.Cursor = Cursors.Hand;
            btnOrders.Cursor = Cursors.Hand;
            btnProducts.Cursor = Cursors.Hand;
            btnExit.Cursor = Cursors.Hand;
            btnClose.Cursor = Cursors.Hand;
        }

        private void ProductCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCost();
        }

        private void OrderData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Key = Convert.ToInt32(OrderData.SelectedRows[0].Cells[0].Value.ToString());
        }

      


        //Customers
        private void customerPage_Click(object sender, EventArgs e)
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
            btnProducts.Cursor = Cursors.Hand;
        }

        private void btnProducts_MouseLeave(object sender, EventArgs e)
        {
            btnProducts.Cursor = Cursors.Default;
        }

        //Orders
        private void btnOrders_Click(object sender, EventArgs e)
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
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
