using System;
using System.Collections.Generic;
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
                textBox2.Clear();
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
            textBox3.Text = list[0].Name;
        }

        private void PrintList(List<PhoneBookEntry> list, Boolean isDialedList = false)
        {
            var headerText = isDialedList ? $"\r\nMOST DIALED NUMBERS\r\n" : $"\r\nSORTED NUMBER'S LIST\r\n";
            textBox1.AppendText(headerText);
            foreach (var item in list)
            {
                var text = isDialedList ? $"{item.Name} {item.PhoneNumber} was dialed {item.DialedTimes} times\r\n" : $"{item.Name} {item.PhoneNumber} \r\n";
                textBox1.AppendText(text);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var list = this.phoneBook.GetSortedList();
            PrintList(list);
        }

        private void DataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.dataGridView1.SelectedRows[0];
                textBox3.Text = row.Cells[0].Value.ToString();
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 0)
            {
                var dialedName = textBox3.Text;
                var item = this.phoneBook.GetByName(dialedName);
                textBox1.AppendText($"Dialing number: {item.Name} ({item.PhoneNumber}) ...\r\n");

                item.DialedTimes++;
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

        private void Button4_Click(object sender, EventArgs e)
        {
            var list = this.phoneBook.GetTop5DialedNumbers();
            PrintList(list, true);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    }
}
