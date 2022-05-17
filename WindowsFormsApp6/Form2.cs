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


namespace WindowsFormsApp6
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlDataAdapter dad;
        DataSet ds = new DataSet();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button3.Select();
            try
            {
                string s = "Data Source = DESKTOP-1BPFL5P\\SQLEXPRESS;Initial Catalog= master;Integrated Security=True;";
                con = new SqlConnection(s);
                con.Open();
                SqlCommand c = new SqlCommand("create database DataBase9", con);
                c.ExecuteNonQuery();
                con.ChangeDatabase("DataBase9");
                SqlCommand c1 = new SqlCommand("create table aya([sellerId][int]primary key,[sellerName][varchar](50),[sellerAge][int],[sellerPhone][varchar](10),[sellerPassword][varchar](10))", con);
                c1.ExecuteNonQuery();
            }
            catch
            {
                con.ChangeDatabase("DataBase9");
                dad = new SqlDataAdapter("select * from aya", con);
                dad.Fill(ds);
            }
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
            this.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.Show();
            this.Hide();
        }
        private void button3_Click(object sender, EventArgs e)
        {
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBox1.Text);
                string name = textBox2.Text;
                int age = int.Parse(textBox3.Text);
                string phone = textBox4.Text;
                string pass = textBox5.Text;
                string scommand = "insert into aya(sellerId,sellerName,sellerAge,sellerPhone,sellerPassword) values(" + id + ",'" + name + "'," + age + ",'" + phone + "','" + pass + "')";
                SqlCommand com = new SqlCommand(scommand, con);
                com.ExecuteNonQuery();
                ds.Clear();
                dad = new SqlDataAdapter("select * from aya", con);
                dad.Fill(ds);
                MessageBox.Show("Addetion successed:)");

            }
            catch (Exception exe)
            {
                MessageBox.Show("There was an error " + exe);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (int.Parse(textBox1.Text) == Convert.ToInt32(r["sellerId"]))
                    {
                        r["sellerName"] = textBox2.Text;
                        r["sellerAge"] = int.Parse(textBox3.Text);
                        r["sellerPhone"] = textBox4.Text;
                        r["sellerPassword"] = textBox5.Text;
                    }
                }
                dad.Fill(ds);
                dad.UpdateCommand = new SqlCommandBuilder(dad).GetUpdateCommand();
                dad.Update(ds);
                ds.Clear();
                dad = new SqlDataAdapter("select * from aya", con);
                dad.Fill(ds);
                MessageBox.Show("Update successed");
            }
            catch (Exception exe)
            {
                MessageBox.Show("There were an error " + exe);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Delete from aya where sellerId=" + Convert.ToInt32(textBox1.Text);
                SqlCommand com = new SqlCommand(sql, con);
                com.ExecuteNonQuery();
                dad.Fill(ds);
                dad.UpdateCommand = new SqlCommandBuilder(dad).GetUpdateCommand();
                dad.Update(ds);
                ds.Clear();
                dad = new SqlDataAdapter("select * from aya", con);
                dad.Fill(ds);
                MessageBox.Show("Delete Suceeded");
            }
            catch (Exception exe)
            {
                MessageBox.Show("There were an error " + exe);
            }
        }
    }
}