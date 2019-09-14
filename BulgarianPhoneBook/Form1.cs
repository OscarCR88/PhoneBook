using System;
using System.Security;
using System.Windows.Forms;

namespace BulgarianPhoneBook
{
    public partial class Form1 : Form
    {
        private PhoneBook phoneBook { get; set; }

        public Form1()
        {
            InitializeComponent();
            this.phoneBook = new PhoneBook();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.phoneBook.LoadPhoneBook(openFileDialog1.FileName);
                    InitializeDGV();
                    textBox1.Text = textBox2.Text = "";
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                var name = textBox2.Text;
                this.phoneBook.DeletePairByName(name);
                InitializeDGV();
            }
            catch (Exception ex)
            {
                textBox1.AppendText(ex.Message + "\r\n");
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void InitializeDGV()
        {
            var list = this.phoneBook.GetEntries();
            phoneBookEntryBindingSource = new BindingSource(list, null);
            dataGridView1.DataSource = phoneBookEntryBindingSource;
            textBox3.Text = list[0].PhoneNumber;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var list = this.phoneBook.GetSortedList();

            foreach (var item in list)
            {
                textBox1.AppendText($"{item.Name} {item.PhoneNumber} \r\n");
            }
        }

        private void DataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.dataGridView1.SelectedRows[0];
                textBox3.Text = row.Cells[1].Value.ToString();
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 0)
            {
                textBox1.AppendText($"Dialing number: {textBox3.Text} ...\r\n");
            }
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 0)
            {
                button3.Enabled = button4.Enabled = button5.Enabled = true;
            }
            else
            {
                button3.Enabled = button4.Enabled = button5.Enabled = false;
            }
        }
    }
}
