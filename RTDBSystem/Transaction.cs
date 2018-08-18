using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RTDBSystem
{
    public class Transaction
    {
        public string TransactionName { get; set; }
        public int ArrivalTime { get; set; }
        public int ExecutionTime { get; set; }
        public int ST { get; set; }
        public int Deadline { get; set; }
        public DataObject Data { get; set; }
        public bool isCommited { get; set; }
        public Color Tcolor { get; set; }
    }
}
