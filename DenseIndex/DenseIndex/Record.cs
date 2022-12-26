using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenseIndex
{
    public class Record
    {
        public long key;
        public string? name;
        public string? surname;
        public string? phoneNumber;
        public bool deleted;

        public Record(long _key, string? _name, string? _surname, string? _phoneNumber, bool _deleted)
        {
            key = _key;
            name = _name;
            surname = _surname;
            phoneNumber = _phoneNumber;
            deleted = _deleted;
        }

        public override string ToString()
        {
            return key.ToString() + " " + name + " " + surname + " " + phoneNumber + " " + deleted.ToString();
        }
    }

}
