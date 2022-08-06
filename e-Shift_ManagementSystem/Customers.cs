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
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            ShowCustomers();
            countTransports();
            
        }

        //Database Connection
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malit\OneDrive\Documents\e-ShiftDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Clear()
        {
            CustomerNameTb.Text = "";
            CustomerEmailTb.Text = "";
            CustomerPhoneTb.Text = "";
            CustomerAddressTb.Text = "";
            GenderCb.SelectedIndex = -1;
            RatingCb.SelectedIndex = -1;
        }
        private void ShowCustomers()
        {
            Con.Open();
            string Query = "select * from CustomersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
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

        int Rate;
        private void GetStars()
        {
            Rate = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[7].Value.ToString());
            RateLbl.Text = "" + Rate;
            if(Rate == 1 || Rate == 2)
            {
                LevelLbl.Text = "OK";
                LevelLbl.ForeColor = Color.Orange;
            }else if(Rate == 3 || Rate == 4)
            {
                LevelLbl.Text = "Good";
                LevelLbl.ForeColor = Color.DeepSkyBlue;
            }
            else
            {
                LevelLbl.Text = "Excellent";
                LevelLbl.ForeColor = Color.LightSkyBlue;
            }

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //Save Customers to the Database
        private void button1_Click(object sender, EventArgs e)
        {
            if (CustomerNameTb.Text == "" || CustomerEmailTb.Text == "" || CustomerPhoneTb.Text == "" || CustomerAddressTb.Text == "" || GenderCb.SelectedIndex == -1 || RatingCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomersTbl (CusName,CusPhone,CusAddress,CusDob,CusJoinDate,CusGender,CusRating,Cusmail) values (@Cname,@Cphone,@Caddress,@Cdob,@Cjoin,@Cgen,@Crate,@Cmail)", Con);
                    cmd.Parameters.AddWithValue("@Cname", CustomerNameTb.Text);
                    cmd.Parameters.AddWithValue("@Cphone", CustomerPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@Caddress", CustomerAddressTb.Text);
                    cmd.Parameters.AddWithValue("@Cdob", DOB.Value.ToString());
                    cmd.Parameters.AddWithValue("@Cjoin", DOJ.Value.ToString());
                    cmd.Parameters.AddWithValue("@Cgen", GenderCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Crate", RatingCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Cmail", CustomerEmailTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Logged");
                    Con.Close();
                    ShowCustomers();
                    Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Delete Customers from the Database
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Customer");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from CustomersTbl where Cusid =@CKey", Con);
                    cmd.Parameters.AddWithValue("@CKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted");
                    Con.Close();
                    ShowCustomers();
                    Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CustomerNameTb.Text == "" || CustomerEmailTb.Text == "" || CustomerPhoneTb.Text == "" || CustomerAddressTb.Text == "" || GenderCb.SelectedIndex == -1 || RatingCb.SelectedIndex == -1)
            {
                MessageBox.Show("Select a Customer");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update CustomersTbl set CusName=@Cname,CusPhone=@Cphone,CusAddress=@Caddress,CusDob=@Cdob,CusJoinDate=@Cjoin,CusGender=@Cgen,CusRating=@Crate,Cusmail=@Cmail where Cusid =@CKey", Con);
                    cmd.Parameters.AddWithValue("@Cname", CustomerNameTb.Text);
                    cmd.Parameters.AddWithValue("@Cphone", CustomerPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@Caddress", CustomerAddressTb.Text);
                    cmd.Parameters.AddWithValue("@Cdob", DOB.Value.ToString());
                    cmd.Parameters.AddWithValue("@Cjoin", DOJ.Value.ToString());
                    cmd.Parameters.AddWithValue("@Cgen", GenderCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Crate", RatingCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Cmail", CustomerEmailTb.Text);
                    cmd.Parameters.AddWithValue("@CKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated");
                    Con.Close();
                    ShowCustomers();
                    Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        int Key = 0;
        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustomerNameTb.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();           
            CustomerPhoneTb.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            CustomerAddressTb.Text = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();
            DOB.Text = CustomerDGV.SelectedRows[0].Cells[4].Value.ToString();
            DOJ.Text = CustomerDGV.SelectedRows[0].Cells[5].Value.ToString();
            GenderCb.Text = CustomerDGV.SelectedRows[0].Cells[6].Value.ToString();
            RatingCb.Text = CustomerDGV.SelectedRows[0].Cells[7].Value.ToString();
            CustomerEmailTb.Text = CustomerDGV.SelectedRows[0].Cells[8].Value.ToString();

            if (CustomerNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());
                GetStars();
            }
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

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            CustomerDashboard obj = new CustomerDashboard();
            obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Customers_Load(object sender, EventArgs e)
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