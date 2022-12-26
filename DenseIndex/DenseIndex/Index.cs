using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenseIndex
{
    public class Index
    {
        public long key;
        public int number;

        public Index(long _key, int _number)
        {
            key = _key;
            number = _number;
        }

        public override string ToString()
        {
            return key.ToString() + " " + number.ToString();
        }
    }


}
