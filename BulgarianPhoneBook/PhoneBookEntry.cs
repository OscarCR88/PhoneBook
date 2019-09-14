using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulgarianPhoneBook
{
    /// <summary>
    /// Represents a Name/Phone pair.
    /// </summary>
    public class PhoneBookEntry
    {
        /// <summary>
        /// Phone's user name.
        /// </summary>
        private String Name { get; set; }

        /// <summary>
        /// Phone Number.
        /// </summary>
        private String PhoneNumber { get; set; }

        /// <summary>
        /// Creates an object of type <see cref="PhoneBookEntry"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phoneNumber"></param>
        public PhoneBookEntry(String name, String phoneNumber)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("Name must not be null or empty!");
            if (String.IsNullOrEmpty(phoneNumber)) throw new ArgumentNullException("Phone Number must not be null or empty!");

            this.Name = name;
            this.PhoneNumber = PhoneNumber;
        }
    }
}
