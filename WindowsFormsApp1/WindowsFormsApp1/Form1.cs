using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class DatabaseWorkstation : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Workers.mdb;";
        public OleDbConnection myConnection;
        public DatabaseWorkstation()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            string query = "SELECT w_id, w_name, w_position, w_salary FROM Worker ORDER BY w_id";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString() + " ");
            }
            reader.Close();
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (textBoxSearch.Text == null || textBoxSearch.Text == String.Empty)
            {
                MessageBox.Show("Please input key number to string!");
            }
            else if (textBoxSearch.Text != String.Empty || textBoxSearch.Text != null)
            {
                string query = $"SELECT w_name, w_position, w_salary FROM Worker WHERE w_id = {textBoxSearch.Text}";
                OleDbCommand command = new OleDbCommand(query, myConnection);
                OleDbDataReader reader = command.ExecuteReader();
                listBox1.Items.Clear();
                while (reader.Read())
                {
                    listBox1.Items.Add(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " ");
                }
                reader.Close();
            }
            else
            {
                string error = "Sorry we cannot find index entry";
                listBox1.Items.Add(error);
            }
        }
        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            string query = "SELECT w_id, w_name, w_position, w_salary FROM Worker ORDER BY w_id";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString() + " ");
            }
            reader.Close();
        }
        private void buttonAddElement_Click(object sender, EventArgs e)
        {
            FormAdd af = new FormAdd();
            af.Owner = this;
            af.Show();
        }

        private void buttonDeleteElement_Click(object sender, EventArgs e)
        {
            FormDelete df = new FormDelete();
            df.Owner = this;
            df.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }
    }
}
