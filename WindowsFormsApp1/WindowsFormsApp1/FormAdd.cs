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
    public partial class FormAdd : Form
    {
        public FormAdd()
        {
            InitializeComponent();
        }

        private void buttonAddElement_Click(object sender, EventArgs e)
        {
            DatabaseWorkstation main = this.Owner as DatabaseWorkstation;
            if (textBoxName.Text == null || textBoxName.Text == String.Empty && textBoxStatus.Text == null || textBoxStatus.Text == String.Empty && textBoxSalary.Text == null || textBoxSalary.Text == String.Empty)
            {
                MessageBox.Show("Please enter data to fields!");
            }
            else
            {
                string query = $"INSERT INTO Worker (w_name, w_position, w_salary) VALUES ('{textBoxName.Text}', '{textBoxStatus.Text}', {textBoxSalary.Text})";
                OleDbCommand command = new OleDbCommand(query, main.myConnection);
                command.ExecuteNonQuery();
                main.buttonSelectAll_Click(sender, e);
                Close();
            }
        }

        private void buttonCloseForm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            char str = e.KeyChar;
            if (!Char.IsLetter(str))
            {
                e.Handled = true;
            }
        }

        private void textBoxStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            char str = e.KeyChar;
            if (!Char.IsLetter(str))
            {
                e.Handled = true;
            }
        }
    }
}
