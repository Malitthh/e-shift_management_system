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
    public partial class Transport : Form
    {
        public Transport()
        {
            InitializeComponent();
            GetCustomers();
            GetVehicles();
            ShowTransport();
            GetProducts();
            countTransports();
            sumIncome();
        }

        //Database Connection
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malit\OneDrive\Documents\e-ShiftDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void GetCustomers()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from CustomersTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CusName", typeof(string));
            dt.Load(rdr);
            CusCb.ValueMember = "CusName";
            CusCb.DataSource = dt;
            Con.Close();
        }
        private void GetVehicles()
        {
            string IsBooked = "Yes";
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from VehicleTbl where VAvalilability='"+ IsBooked + "'", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("VPlate", typeof(string));
            dt.Load(rdr);
            VehiCb.ValueMember = "VPlate";
            VehiCb.DataSource = dt;
            Con.Close();
        }
        private void GetProducts()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from ProductTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("ProName", typeof(string));
            dt.Load(rdr);
            ProductCb.ValueMember = "ProName";
            ProductCb.DataSource = dt;
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
        private void sumIncome()
        {
            Con.Open();
            string Query = "select Sum(Amount) from TransportTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SumIncomeLbl.Text = "LKR" + dt.Rows[0][0].ToString();
            Con.Close();

        }
        private void ShowTransport()
        {
            Con.Open();
            string Query = "select * from TransportTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TransportDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Clear()
        {
            CusCb.SelectedIndex = -1;
            VehiCb.SelectedIndex = -1;
            PickupTb.Text = "";
            DestinationTb.Text = "";
            WeightTb.Text = "";
            ProductCb.SelectedIndex = -1;
            AmountTb.Text = "";
        }
        private void UpdateVehicle()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update VehicleTbl set VAvalilability=@VAval where VPlate=@VP", Con);
                cmd.Parameters.AddWithValue("@VP", VehiCb.SelectedValue.ToString());
                
                cmd.Parameters.AddWithValue("@VAval", "No");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Vehicle Updated");
                Con.Close();

                Clear();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (CusCb.SelectedIndex == -1 || VehiCb.SelectedIndex == -1 || PickupTb.Text == "" || DestinationTb.Text == "" || WeightTb.Text == "" || ProductCb.SelectedIndex == -1 || AmountTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into TransportTbl (CustName,Vehicle,Products,PickupDate,DeliveryDate,PickupLocation,Destination,Weight,Amount) values (@Cname,@Tvehi,@Tprod,@Pdate,@Ddate,@Ploc,@Desti,@Pweig,@Amn)", Con);
                    cmd.Parameters.AddWithValue("@Cname", CusCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Tvehi", VehiCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Tprod", ProductCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Pdate", PickDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Ddate", DeliveryDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Ploc", PickupTb.Text);                    
                    cmd.Parameters.AddWithValue("@Desti", DestinationTb.Text);
                    cmd.Parameters.AddWithValue("@Pweig", WeightTb.Text);
                    cmd.Parameters.AddWithValue("@Amn", AmountTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Transport Created");
                    Con.Close();
                    ShowTransport();
                    UpdateVehicle();
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Vehicles obj = new Vehicles();
            obj.Show();
            this.Hide();
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Products obj = new Products();
            obj.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Users obj = new Users();
            obj.Show();
            this.Hide();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            this.Hide();
            Obj.Show();
        }

        private void PickupTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CusCb.SelectedIndex == -1 || VehiCb.SelectedIndex == -1 || PickupTb.Text == "" || DestinationTb.Text == "" || WeightTb.Text == "" || ProductCb.SelectedIndex == -1 || AmountTb.Text == "")
            {
                MessageBox.Show("Select a Transport");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update TransportTbl set CustName=@Cname,Vehicle=@Tvehi,Products=@Tprod,PickupDate=@Pdate,DeliveryDate=@Ddate,PickupLocation=@Ploc,Destination=@Desti,Weight=@Pweig,Amount=@Amn where Transportid =@TKey ", Con);
                    cmd.Parameters.AddWithValue("@Cname", CusCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Tvehi", VehiCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Tprod", ProductCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Pdate", PickDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Ddate", DeliveryDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Ploc", PickupTb.Text);
                    cmd.Parameters.AddWithValue("@Desti", DestinationTb.Text);
                    cmd.Parameters.AddWithValue("@Pweig", WeightTb.Text);
                    cmd.Parameters.AddWithValue("@Amn", AmountTb.Text);
                    cmd.Parameters.AddWithValue("@TKey",Key);                  
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Transport Updated");
                    Con.Close();
                    ShowTransport();
                    UpdateVehicle();
                    Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Transport");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from TransportTbl where Transportid =@TKey", Con);
                    cmd.Parameters.AddWithValue("@TKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Transport Deleted");
                    Con.Close();
                    ShowTransport();
                    Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        int Key = 0;
        private void TransportDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CusCb.SelectedItem = TransportDGV.SelectedRows[0].Cells[1].Value.ToString();
            VehiCb.SelectedItem = TransportDGV.SelectedRows[0].Cells[2].Value.ToString();
            ProductCb.SelectedItem = TransportDGV.SelectedRows[0].Cells[3].Value.ToString();
            PickDate.Text = TransportDGV.SelectedRows[0].Cells[4].Value.ToString();
            DeliveryDate.Text = TransportDGV.SelectedRows[0].Cells[5].Value.ToString();
            PickupTb.Text = TransportDGV.SelectedRows[0].Cells[6].Value.ToString();
            DestinationTb.Text = TransportDGV.SelectedRows[0].Cells[7].Value.ToString();
            WeightTb.Text = TransportDGV.SelectedRows[0].Cells[8].Value.ToString();
            AmountTb.Text = TransportDGV.SelectedRows[0].Cells[9].Value.ToString();
            if (DeliveryDate.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(TransportDGV.SelectedRows[0].Cells[0].Value.ToString());

            }
        }
    }
}