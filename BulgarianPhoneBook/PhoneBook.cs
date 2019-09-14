using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulgarianPhoneBook
{
    /// <summary>
    /// Represents a PhoneBook.
    /// </summary>
    public class PhoneBook
    {
        /// <summary>
        /// Holds a list of <see cref="PhoneBookEntry"/>.
        /// </summary>
        private List<PhoneBookEntry> PhoneList { get; set; }

        public PhoneBook()
        {
            this.PhoneList = new List<PhoneBookEntry>();
        }
        /// <summary>
        /// Loads a <see cref="PhoneBook"/> from a text file.
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadPhoneBook(String filePath)
        {
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            string[] lines = System.IO.File.ReadAllLines(@filePath);

            foreach (String line in lines)
            {
                var splitted = line.Split(',');
                var name = splitted[0];
                var number = splitted[1].Trim();

                AddEntry(name, number);

            }
        }

        /// <summary>
        /// Adds a new entry to the PhoneBook.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="number">Phone number.</param>
        public void AddEntry(String name, String number)
        {
            this.PhoneList.Add(new PhoneBookEntry(name, number));

        }

        /// <summary>
        /// Returns a list with all Phonebook entries.
        /// </summary>
        /// <returns></returns>
        public List<PhoneBookEntry> GetEntries()
        {
            return this.PhoneList;
        }
    }
}
