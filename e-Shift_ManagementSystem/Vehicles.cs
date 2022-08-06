using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace e_Shift_ManagementSystem
{
    public partial class Vehicles : Form
    {
        public Vehicles()
        {
            InitializeComponent();
            ShowVehicles();
            countVehicles();
        }

        //Database Connection
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
        private void Clear()
        {
            LPlateTb.Text = "";
            VTypeCb.SelectedIndex = -1;
            FTypeCb.SelectedIndex = -1;
            CSizeCb.SelectedIndex = -1;
            VYearCb.SelectedIndex = -1;
            VAvalCb.SelectedIndex = -1;
        }
        private void ShowVehicles()
        {
            Con.Open();
            string Query = "select * from VehicleTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            VehicleDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        //Save Vehicles to the Database
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (LPlateTb.Text == "" || VTypeCb.SelectedIndex == -1 || FTypeCb.SelectedIndex == -1 || CSizeCb.SelectedIndex == -1 || VYearCb.SelectedIndex == -1 || VAvalCb.SelectedIndex == -1) 
            {
                MessageBox.Show("Missing Information");
            }else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into VehicleTbl (VPlate,VType,VFuel,VSize,VYear,VAvalilability) values (@VP,@Vty,@VF,@VS,@VY,@VAval)", Con);
                    cmd.Parameters.AddWithValue("@VP", LPlateTb.Text);
                    cmd.Parameters.AddWithValue("@Vty", VTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VF", FTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VS", CSizeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VY", VYearCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VAval", VAvalCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Recorded");
                    Con.Close();
                    ShowVehicles();
                    Clear();

                }catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                
            }
        }

        //Delete Vehicles from the Database
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (LPlateTb.Text == "")
            {
                MessageBox.Show("Select a Vehicle");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from VehicleTbl where VPlate =@vplate", Con);
                    cmd.Parameters.AddWithValue("@vplate", LPlateTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Deleted");
                    Con.Close();
                    ShowVehicles();
                    Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void VehicleDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LPlateTb.Text = VehicleDGV.SelectedRows[0].Cells[0].Value.ToString();
            VTypeCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[1].Value.ToString();
            FTypeCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[2].Value.ToString();
            CSizeCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[3].Value.ToString();
            VYearCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[4].Value.ToString();
            VAvalCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[5].Value.ToString();
        }

        //Application Exit Button
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Edit Vehicles in the Database
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (LPlateTb.Text == "" || VTypeCb.SelectedIndex == -1 || FTypeCb.SelectedIndex == -1 || CSizeCb.SelectedIndex == -1 || VYearCb.SelectedIndex == -1 || VAvalCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update VehicleTbl set VType=@Vty,VFuel=@VF,VSize=@VS,VYear=@VY,VAvalilability=@VAval where VPlate=@VP", Con);
                    cmd.Parameters.AddWithValue("@VP", LPlateTb.Text);
                    cmd.Parameters.AddWithValue("@Vty", VTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VF", FTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VS", CSizeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VY", VYearCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VAval", VAvalCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Updated");
                    Con.Close();
                    ShowVehicles();
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
    }
}
