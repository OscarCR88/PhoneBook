using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
            this.PhoneList.Clear();
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            string[] lines = System.IO.File.ReadAllLines(@filePath);

            foreach (String line in lines)
            {
                var splitted = line.Split(',');
                if (splitted.Length == 2)
                {
                    var name = splitted[0];
                    var number = splitted[1].Trim();

                    if (ValidatePhoneNumber(number))
                    {
                        AddEntry(name, number);
                    }
                }

            }
        }

        /// <summary>
        /// Returns top 5 dailed numbers from the list. 
        /// </summary>
        /// <param name="sort">Sort direction.</param>
        /// <returns></returns>
        public List<PhoneBookEntry> GetTop5DialedNumbers(SortOrder sort = SortOrder.Descending)
        {
            if (sort == SortOrder.Ascending)
            {
                return this.PhoneList.OrderBy(n => n.DialedTimes).Take(5).ToList();
            }
            return this.PhoneList.OrderByDescending(n => n.DialedTimes).Take(5).ToList();
        }
        
        /// <summary>
        /// Returns a sorted list. 
        /// </summary>
        /// <param name="sort">Sort direction.</param>
        /// <returns></returns>
        public List<PhoneBookEntry> GetSortedList(SortOrder sort = SortOrder.Ascending)
        {
            if (sort == SortOrder.Ascending)
            {
                return this.PhoneList.OrderBy(n => n.Name).ToList();
            }
            return this.PhoneList.OrderByDescending(n => n.Name).ToList();
        }

        /// <summary>
        /// Returns a <see cref="PhoneBookEntry"/> object, if exists. Case sensitive.
        /// </summary>
        /// <param name="name">Name to remove.</param>
        /// <returns></returns>
        public PhoneBookEntry GetByName(String name)
        {
            return this.PhoneList.FirstOrDefault(n => n.Name == name);
        }

        /// <summary>
        /// Removes the first occurrence, if any of a given name. Case sensitive.
        /// </summary>
        /// <param name="name">Name to remove.</param>
        public void DeletePairByName(String name)
        {
            var item = GetByName(name);
            if (item != null)
            {
                this.PhoneList.Remove(item);
            }
            else
            {
                throw new Exception($"Name: {name} was not found on this PhoneBook. Be careful with uppercase/lowercase");
            }

        }

        /// <summary>
        /// Validates the phone number to be a Bulgarian one.
        /// </summary>
        /// <returns></returns>
        private Boolean ValidatePhoneNumber(String number)
        {
            string pattern = @"^(\+359|0|00359)(87|88|89)([2-9])([\d]{6}$)";

            Match result = Regex.Match(number, pattern);

            return result.Success;
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
