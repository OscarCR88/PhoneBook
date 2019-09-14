using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
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
                textBox1.Text += ex.Message + "\r\n";
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
            phoneBookEntryBindingSource = new BindingSource(this.phoneBook.GetEntries(), null);
            dataGridView1.DataSource = phoneBookEntryBindingSource;
        }
    }
}
