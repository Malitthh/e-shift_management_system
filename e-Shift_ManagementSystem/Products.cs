using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e_Shift_ManagementSystem
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
            ShowProducts();
            countTransports();
            countProducts();
        }

        //Database Connection
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malit\OneDrive\Documents\e-ShiftDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Clear()
        {
            ProductNameTb.Text = "";
            ProductIDTb.Text = "";
            ProductSizeCb.SelectedIndex = -1;
            ProductDescriptionTb.Text = "";
        }
        private void ShowProducts()
        {
            Con.Open();
            string Query = "select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void countTransports()
        {
            Con.Open();
            string Query = "select count(*) from TransportTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            TNumLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();

        }
        private void countProducts()
        {
            Con.Open();
            string Query = "select count(*) from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            PLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();

        }

        //Save Products to the Database
        private void SaveBtn_Click(object sender, EventArgs e)
        {

            if (ProductNameTb.Text == "" || ProductIDTb.Text == "" || ProductSizeCb.SelectedIndex == -1 || ProductDescriptionTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ProductTbl (ProName,ProductCode,ProSize,ProDescription) values (@Pname,@Producid,@Psize,@PDes)", Con);
                    cmd.Parameters.AddWithValue("@Pname", ProductNameTb.Text);
                    cmd.Parameters.AddWithValue("@Producid", ProductIDTb.Text);
                    cmd.Parameters.AddWithValue("@Psize", ProductSizeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PDes", ProductDescriptionTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Logged");
                    Con.Close();
                    ShowProducts();
                    Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        //Application Exit Button
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Delete Products from the Database
        int Key = 0;
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Product");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from ProductTbl where Proid =@ProKey", Con);
                    cmd.Parameters.AddWithValue("@ProKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted");
                    Con.Close();
                    ShowProducts();
                    Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductNameTb.Text = ProductsDGV.SelectedRows[0].Cells[1].Value.ToString();
            ProductIDTb.Text = ProductsDGV.SelectedRows[0].Cells[2].Value.ToString();
            ProductSizeCb.SelectedItem = ProductsDGV.SelectedRows[0].Cells[3].Value.ToString();
            ProductDescriptionTb.Text = ProductsDGV.SelectedRows[0].Cells[4].Value.ToString();
            if(ProductNameTb.Text =="")
            {
                Key = 0;    
            }
            else
            {
                Key = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[0].Value.ToString());
               
            }
            
            
        }

        //Edit Products in the Database
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ProductNameTb.Text == "" || ProductIDTb.Text == "" || ProductSizeCb.SelectedIndex == -1 || ProductDescriptionTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update ProductTbl set ProductCode=@Producid ,ProName=@Pname,ProSize=@Psize,ProDescription=@PDes where Proid =@ProKey", Con);
                    cmd.Parameters.AddWithValue("@Pname", ProductNameTb.Text);
                    cmd.Parameters.AddWithValue("@Producid", ProductIDTb.Text);
                    cmd.Parameters.AddWithValue("@Psize", ProductSizeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PDes", ProductDescriptionTb.Text);
                    cmd.Parameters.AddWithValue("@ProKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Updated");
                    Con.Close();
                    ShowProducts();
                    Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Transport obj = new Transport();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Vehicles obj = new Vehicles();
            obj.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Users obj = new Users();
            obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void Products_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            this.Hide();
            Obj.Show();
        }
    }
}
