
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e_Shift_ManagementSystem
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            countVehicles();
            countUsers();
            countCustomers();
            countTransports();
            countProducts();
            sumAmount();
            BestCustomer();
            sumIncome();
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
        private void countUsers()
        {
            Con.Open();
            string Query = "select count(*) from UsersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            UNumLbl.Text = dt.Rows[0][0].ToString();
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

        private void countProducts()
        {
            Con.Open();
            string Query = "select count(*) from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            PNumLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();

        }
        private void sumAmount()
        {
            Con.Open();
            string Query = "select Sum(Amount) from TransportTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            INumLbl.Text = "LKR"+dt.Rows[0][0].ToString();
            Con.Close();

        }
        private void BestCustomer()
        {
            Con.Open();
            string InnerQuery = "select Max(Amount) from TransportTbl";
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter(InnerQuery, Con);
            sda1.Fill(dt1);
            string Query = "select CustName from TransportTbl where Amount = '"+ dt1.Rows[0][0].ToString() + "' ";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BestCustomerLbl.Text =dt.Rows[0][0].ToString();
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

        private void Dashboard_Load(object sender, EventArgs e)
        {

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

        private void PdfCreateBtn_Click(object sender, EventArgs e)
        {

        }

        /*private void ExportToPDF()
        {
            string deviceInfo = ""; //for setting width,height etc. using defaults
            string[] streamIds;
            Warning[] warnings;

            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            ReportViewer viewer = new Microsoft.Reporting.WinForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = "Report1.rdlc";
            viewer.LocalReport.DataSources.Add(new ReportDataSource("Transportpdf",GetTransportData()));
            viewer.RefreshReport();
            var bytes = viewer.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamIds, out warnings);
            string filename = @"G:\new\transportdata.pdf";
            File.WriteAllBytes(filename, bytes);
            System.Diagnostics.Process.Start(filename);
        }
        private List<Transportpdf> GetTransportData()
        {
            return new List<Transportpdf>
            {
                new Transportpdf{CustomerName="Hasitha",Vehicle="CP 001",Products="Table",PickupDate="2022-07-25",DeliveryDate="2022-07-25",PickupLocation="CMB",Destination="Kandy",Weight="10",Amount="1000"},
            };
        }*/
    }
}
