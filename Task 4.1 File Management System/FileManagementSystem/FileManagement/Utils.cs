using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    public static class Utils
    {
        public static int GetHash(byte[] data)
        {
            unchecked
            {
                return data.Aggregate(0, (current, b) => (current * 31) ^ b);
            }
        }
    }
}
