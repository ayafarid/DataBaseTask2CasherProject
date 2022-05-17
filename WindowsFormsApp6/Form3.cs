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
    public partial class Form3 : Form
    {
        SqlConnection con;
        SqlDataAdapter dad;
        DataSet ds = new DataSet();
        public Form3()
        {
            InitializeComponent();
            button1.Enabled = false;
            button1.Select();
            try
            {
                string s = "Data Source = DESKTOP-1BPFL5P\\SQLEXPRESS;Initial Catalog= master;Integrated Security=True;";
                con = new SqlConnection(s);
                con.Open();
                SqlCommand c = new SqlCommand("create database Categories", con);
                c.ExecuteNonQuery();
                con.ChangeDatabase("Categories");
                SqlCommand c1 = new SqlCommand("create table category([categoryID][int]primary key,[categoryName][varchar](50),[categoryDesrip][varchar](50))", con); 
                c1.ExecuteNonQuery();
            }
            catch
            {
                con.ChangeDatabase("Categories");
                dad = new SqlDataAdapter("select * from category", con);
                dad.Fill(ds);
            }
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            this.Hide();
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
                string descrip = textBox3.Text;
                string scommand = "insert into category(categoryID,categoryName,categoryDesrip) values(" + id + ",'" + name + "','" + descrip +"')";
                SqlCommand com = new SqlCommand(scommand, con);
                com.ExecuteNonQuery();
                ds.Clear();
                dad = new SqlDataAdapter("select * from category", con);
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
                    if (int.Parse(textBox1.Text) == Convert.ToInt32(r["categoryID"]))
                    {
                        r["categoryID"] = int.Parse(textBox1.Text);
                        r["categoryName"] = textBox2.Text;
                        r["categoryDesrip"] = textBox3.Text;
                    }
                }
                dad.Fill(ds);
                dad.UpdateCommand = new SqlCommandBuilder(dad).GetUpdateCommand();
                dad.Update(ds);
                ds.Clear();
                dad = new SqlDataAdapter("select * from category", con);
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
                string sql = "Delete from category where categoryID=" + Convert.ToInt32(textBox1.Text);
                SqlCommand com = new SqlCommand(sql, con);
                com.ExecuteNonQuery();
                dad.Fill(ds);
                dad.UpdateCommand = new SqlCommandBuilder(dad).GetUpdateCommand();
                dad.Update(ds);
                ds.Clear();
                dad = new SqlDataAdapter("select * from category", con);
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
