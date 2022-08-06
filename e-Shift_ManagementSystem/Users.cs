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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            ShowUsers();
            BestCustomer();


        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Database Connection
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malit\OneDrive\Documents\e-ShiftDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Clear()
        {
            UserNameTb.Text = "";
            UserPhoneTb.Text = "";
            PasswordTb.Text = "";
        }
        private void ShowUsers()
        {
            Con.Open();
            string Query = "select * from UsersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
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


        //Save User to the Database
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (UserNameTb.Text == "" || UserPhoneTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into UsersTbl (UName,UPhone,UPassword,UJoinDate) values (@Uname,@Uphone,@Upass,@Ujoin)", Con);
                    cmd.Parameters.AddWithValue("@Uname", UserNameTb.Text);
                    cmd.Parameters.AddWithValue("@Uphone", UserPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@Upass", PasswordTb.Text);
                    cmd.Parameters.AddWithValue("@Ujoin", JoinDate.Value.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Logged");
                    Con.Close();
                    ShowUsers();
                    Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        //Delete User from the Database
        int Key = 0;   
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a User");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from UsersTbl where UId =@UKey", Con);
                    cmd.Parameters.AddWithValue("@UKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Removed");
                    Con.Close();
                    ShowUsers();
                    Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        

        //Edit User in the Database
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (UserNameTb.Text == "" || UserPhoneTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update UsersTbl set UName=@Uname,UPhone=@Uphone,UPassword=@Upass,UJoinDate=@Ujoin where UId =@UKey", Con);
                    cmd.Parameters.AddWithValue("@Uname", UserNameTb.Text);
                    cmd.Parameters.AddWithValue("@Uphone", UserPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@Upass", PasswordTb.Text);
                    cmd.Parameters.AddWithValue("@Ujoin", JoinDate.Value.ToString());
                    cmd.Parameters.AddWithValue("@UKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated");
                    Con.Close();
                    ShowUsers();
                    Clear();
                                                                                
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UserNameTb.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            UserPhoneTb.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
            PasswordTb.Text = UserDGV.SelectedRows[0].Cells[3].Value.ToString();
            JoinDate.Text = UserDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (UserNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(UserDGV.SelectedRows[0].Cells[0].Value.ToString());
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Products obj = new Products();
            obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            this.Hide();
            Obj.Show();
        }
    }
                                                    
}
