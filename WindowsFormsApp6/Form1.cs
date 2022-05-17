using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Data;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataAdapter dad;
        DataSet ds=new DataSet();

        public Form1()
        {
            InitializeComponent();
            try
            {
                string s = "Data Source = DESKTOP-1BPFL5P\\SQLEXPRESS;Initial Catalog= master;Integrated Security=True;";
                con = new SqlConnection(s);
                con.Open();
                SqlCommand c = new SqlCommand("create database db9", con);
                c.ExecuteNonQuery();
                con.ChangeDatabase("db9");
                SqlCommand c1 = new SqlCommand("create table login([name][varchar](50)primary key,[pass][varchar](50),[accsess][varchar](6))", con);
                c1.ExecuteNonQuery();
            }
            catch
            {
                con.ChangeDatabase("db9");
                dad = new SqlDataAdapter("select * from login", con);
                dad.Fill(ds, "login");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand c = new SqlCommand("insert into login(name,pass,accsess) values ('" + textBox1.Text + "','" + textBox2.Text + "','"+comboBox1.Text+"')", con);
            c.ExecuteNonQuery();
            MessageBox.Show("Data added succesfully");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand c = new SqlCommand("select * from login where name='" + textBox1.Text + "' and pass='" + textBox2.Text + "' and accsess='"+comboBox1.Text+"'", con);
            SqlDataReader d = c.ExecuteReader();
            if (d.Read())
            {
                Form3 f = new Form3();
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("incorrect username or password");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Admin");
            comboBox1.Items.Add("Seller");
        }
    }
}
