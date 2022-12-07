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
            bool result = int.TryParse(textBoxSearch.Text, out int tempint);  
            //if (tempstring == null || tempstring == String.Empty)
            //{
            //    MessageBox.Show("Please input key number to string!");
            //}
            //if (textBoxSearch.Text != String.Empty || textBoxSearch.Text != null)
            if (result)
            {
                string tempstring = tempint.ToString();
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
                MessageBox.Show("Sorry we cannot find index entry");
            }
        }
        internal void buttonSelectAll_Click(object sender, EventArgs e)
        {
            textBoxSearch.Text = String.Empty;
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
            textBoxSearch.Text = String.Empty;
            FormAdd af = new FormAdd();
            af.Owner = this;
            af.Show();
        }

        private void buttonDeleteElement_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                FormDelete df = new FormDelete();
                df.Owner = this;
                df.Show();
            }
            else
            {
                DialogResult dr = MessageBox.Show("Delete this field?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Cancel)
                {
                    var ee = new CancelEventArgs();
                    ee.Cancel = true;
                }
                else if (dr == DialogResult.OK)
                {
                    var id = listBox1.Items[listBox1.SelectedIndex].ToString().Split()[0];
                    string query = $"DELETE FROM Worker WHERE w_id = {Convert.ToUInt32(id)}";
                    OleDbCommand command = new OleDbCommand(query, myConnection);
                    command.ExecuteNonQuery();
                    buttonSelectAll_Click(sender, e);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }

        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var el = listBox1.Items[listBox1.SelectedIndex].ToString().Split();
                textBoxSearch.Text = el[1] + " " + el[2] + " " + el[3];
                ActiveControl = this.textBoxSearch;
            }
            else
            {
                MessageBox.Show("You`ve clicked on empty field!");
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {   
            if (listBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Enter the element for changing!");
                }
            else
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to change this field?", "Change", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Cancel)
                {
                    var ee = new CancelEventArgs();
                    ee.Cancel = true;
                }
                else if (dr == DialogResult.OK)
                {
                    //var id = listBox1.Items[listBox1.SelectedIndex].ToString().Split()[0];
                    //string query = $"UPDATE FROM Worker WHERE w_id = {Convert.ToUInt32(id)}";
                    //OleDbCommand command = new OleDbCommand(query, myConnection);
                    //command.ExecuteNonQuery();
                    //buttonSelectAll_Click(sender, e);
                    string[] words = textBoxSearch.Text.Split();
                    var el = listBox1.Items[listBox1.SelectedIndex].ToString().Split();
                    //string query = $"UPDATE Worker SET w_name = '" + el[1] + "',w_position='" + el[2] + "',w_salary =" + el[3] + "  WHERE w_id = " + el[0];
                    string query = $"UPDATE Worker SET w_name = '{words[0]}',w_position='{words[1]}',w_salary ={words[2]}  WHERE w_id = " + el[0];
                    OleDbCommand command = new OleDbCommand(query, myConnection);
                    command.ExecuteNonQuery();
                    buttonSelectAll_Click(sender, e);
                }

            }
        }
    }
}
