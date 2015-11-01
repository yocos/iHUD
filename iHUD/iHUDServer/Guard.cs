using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Guard
{
    public static class Argument
    {
        public static void IsNotNull(object value, string argName)
        {
            if (value == null) throw new ArgumentNullException(argName);
        }
    }
}

