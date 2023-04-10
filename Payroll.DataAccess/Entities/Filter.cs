using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.DataAccess
{
    /// <summary>
    /// where <key> <operator> '<value>' e.g name = 'sayeed'
    /// </summary>
    public class Filter
    {
        // how to enforce the string op must be a string from Operator class??
        public Filter(string key, string op, string value)
        {
            Key = key;
            Operator = op;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
        public string Operator { get; set; }
    }
}
