using System.Collections.Generic;
using System.Linq;

namespace ContactManager.Models
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Phones { get; set; } = new List<string>();
        public string Address { get; set; } = "";

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Contact c))
                return false;

            return c.Address == this.Address && c.FirstName == this.FirstName &&
                c.LastName == this.LastName && c.Phones.SequenceEqual(this.Phones);

        }

        public override int GetHashCode()
        {
            return FirstName.GetHashCode() ^ LastName.GetHashCode() ^ 
                Address.GetHashCode() ^ Phones.GetHashCode();
        }
    }
}
