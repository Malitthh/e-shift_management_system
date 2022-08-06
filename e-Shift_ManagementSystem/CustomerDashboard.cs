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
    public partial class CustomerDashboard : Form
    {
        public CustomerDashboard()
        {
            InitializeComponent();
            countVehicles();
            countCustomers();
            countTransports();
            BestCustomer();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malit\OneDrive\Documents\e-ShiftDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void countVehicles()
        {
            Con.Open();
            string Query = "select count(*) from VehicleTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            VNumLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();

        }
        private void countCustomers()
        {
            Con.Open();
            string Query = "select count(*) from CustomersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CNumLbl.Text = dt.Rows[0][0].ToString();
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
        private void BestCustomer()
        {
            Con.Open();
            string InnerQuery = "select Max(Amount) from TransportTbl";
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter(InnerQuery, Con);
            sda1.Fill(dt1);
            string Query = "select CustName from TransportTbl where Amount = '" + dt1.Rows[0][0].ToString() + "' ";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BestCustomerLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();

        }
        private void CustomerDashboard_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            this.Hide();
            Obj.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            this.Hide();
            Obj.Show();
        }
    }
}
