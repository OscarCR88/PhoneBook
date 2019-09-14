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
                    var bindingList = new BindingList<PhoneBookEntry>(this.phoneBook.GetEntries());
                    //phoneBookEntryBindingSource.Clear();
                    phoneBookEntryBindingSource = new BindingSource( this.phoneBook.GetEntries(), null);

                    // Initialize the DataGridView.
                  
                    dataGridView1.DataSource = phoneBookEntryBindingSource;
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }
    }
}
