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
    public partial class FormDelete : Form
    {
        public FormDelete()
        {
            InitializeComponent();
        }
        private void buttonDeleteElement_Click(object sender, EventArgs e)
        {
            DatabaseWorkstation main = this.Owner as DatabaseWorkstation;
            DialogResult dr = MessageBox.Show("Delete this field?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Cancel)
            {
                var ee = new CancelEventArgs();
                ee.Cancel = true;
            }   
            else if (dr == DialogResult.OK)
            {
                if (textBoxName.Text == null || textBoxName.Text == String.Empty)
                {
                    MessageBox.Show("Please input key number to string!");
                }
                else
                {
                    string query = $"DELETE FROM Worker WHERE w_id = {textBoxName.Text}";
                    OleDbCommand command = new OleDbCommand(query, main.myConnection);
                    command.ExecuteNonQuery();
                    main.buttonSelectAll_Click(sender, e);
                    Close();
                }
            }
            
        }
        private void buttonCloseForm_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
