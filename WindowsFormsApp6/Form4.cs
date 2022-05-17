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
    public partial class Form4 : Form
    {
        SqlConnection con;
        SqlDataAdapter dad;
        DataSet ds = new DataSet();
        public Form4()
        {
            InitializeComponent();
            button2.Enabled = false;
            button2.Select();
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = new SqlConnection("Data Source=DESKTOP-1BPFL5P\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;");
                conn.Open();
                conn.ChangeDatabase("Categories");
                string query = "select (categoryName) from category";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("categoryName", typeof(string));
                dt.Load(dr);
                comboBox1.ValueMember = "categoryName";
                comboBox1.DataSource = dt;

                comboBox2.ValueMember = "categoryName";
                comboBox2.DataSource = dt;
                conn.Close();

            }
            catch (Exception exe)
            {
                MessageBox.Show("Error "+exe);
            }
            try
            {
                string s = "Data Source = DESKTOP-1BPFL5P\\SQLEXPRESS;Initial Catalog= master;Integrated Security=True;";
                con = new SqlConnection(s);
                con.Open();
                SqlCommand c = new SqlCommand("create database afarid", con);
                c.ExecuteNonQuery();
                con.ChangeDatabase("afarid");
                string scom = "create table product([productID][int]primary key,[productName][varchar](50),[productQuantity][int],[productPrice][int],[productCatogry][varchar](50))";
                SqlCommand c1 = new SqlCommand(scom, con);
                c1.ExecuteNonQuery();
            }
            catch
            {
                con.ChangeDatabase("afarid");
                dad = new SqlDataAdapter("select * from product", con);
                dad.Fill(ds);
            }
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBox1.Text);
                string name = textBox2.Text;
                int qunatity = int.Parse(textBox3.Text);
                int price = int.Parse(textBox4.Text);
                string catogry = comboBox1.Text;
                string scommand = "insert into product(productID,productName,productQuantity,productPrice,productCatogry) values(" + id + ",'" + name + "'," + qunatity + ","+price+",'"+catogry+")";
                SqlCommand com = new SqlCommand(scommand, con);
                com.ExecuteNonQuery();
                ds.Clear();
                dad = new SqlDataAdapter("select * from product", con);
                dad.Fill(ds);
                MessageBox.Show("Addetion successed:)");

            }
            catch (Exception exe)
            {
                MessageBox.Show("There was an error " + exe);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (int.Parse(textBox1.Text) == Convert.ToInt32(r["productID"]))
                    {
                        r["productID"] = int.Parse(textBox1.Text);
                        r["productName"] = textBox2.Text;
                        r["productQuantity"] = int.Parse(textBox3.Text);
                        r["productPrice"] = int.Parse(textBox4.Text);
                        r["productCatogry"] = comboBox1.Text;
                    }
                }
                dad.Fill(ds);
                dad.UpdateCommand = new SqlCommandBuilder(dad).GetUpdateCommand();
                dad.Update(ds);
                ds.Clear();
                dad = new SqlDataAdapter("select * from product", con);
                dad.Fill(ds);
                MessageBox.Show("Update successed");
            }
            catch (Exception exe)
            {
                MessageBox.Show("There were an error " + exe);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Delete from category where productID=" + Convert.ToInt32(textBox1.Text);
                SqlCommand com = new SqlCommand(sql, con);
                com.ExecuteNonQuery();
                dad.Fill(ds);
                dad.UpdateCommand = new SqlCommandBuilder(dad).GetUpdateCommand();
                dad.Update(ds);
                ds.Clear();
                dad = new SqlDataAdapter("select * from product", con);
                dad.Fill(ds);
                MessageBox.Show("Delete Suceeded");
            }
            catch (Exception exe)
            {
                MessageBox.Show("There were an error " + exe);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
    }
}
