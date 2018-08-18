using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTDBSystem
{
    public static class Algorithm
    {
        /*
          a-	Wait techniques  with earliest deadline first for resovling the conflict
          b-	Wait Promote techniques with earliest deadline first for resovling the conflict
          c-	High Priority techniques  with earliest deadline first for resovling the conflict
          d-	Conditional Restart techniques  with earliest deadline first for resovling the conflict
          e-	Proposed model: it called Conditional Waiting High Priority technieque (CWHP) to resolve the conflict.
        */
        private static List<Transaction> LstTransactionWPEDLF {get;set;}
        private static Transaction GetTransactionCWHP(List<Transaction> LstTrans, List<DataObject> LstData, int clock)
        {
            var tran = LstTrans[0];
            //LstTrans.Sort((x, y) => x.Deadline.CompareTo(y.Deadline));
            foreach (var item in LstTrans)
            {
                if (item.ArrivalTime <= clock && item.Deadline <= tran.Deadline && !item.isCommited)
                    tran = item;
            }
            if (tran.Data.ifAvailable && !tran.Data.Holder.Equals(tran.TransactionName) && tran.ArrivalTime <= clock && !tran.isCommited)
            {
                tran.Data.ifAvailable = false;
                tran.Data.Holder = tran.TransactionName;

                return tran;
            }
            else
            {
                //int ST = tran.ST;
                foreach (var item in LstTrans)
                {
                    if (item.ArrivalTime <= clock && item.ExecutionTime <= tran.ST && !item.isCommited)
                        tran = item;
                    if (tran.isCommited)
                        tran = item;
                }
            }
            return tran;

            
        }
        private static Transaction GetTransactionCREDLF(List<Transaction> LstTrans, List<DataObject> LstData, int clock)
        {
            var tran = LstTrans[0];
            //LstTrans.Sort((x, y) => x.Deadline.CompareTo(y.Deadline));
            foreach (var item in LstTrans)
            {
                if (item.ArrivalTime <= clock && item.Deadline <= tran.Deadline && !item.isCommited)
                    tran = item;
            }
            if (tran.Data.ifAvailable && !tran.Data.Holder.Equals(tran.TransactionName) && tran.ArrivalTime <= clock && !tran.isCommited)
            {
                tran.Data.ifAvailable = false;
                tran.Data.Holder = tran.TransactionName;

                return tran;
            }
            else
            {
                //int ST = tran.ST;
                foreach (var item in LstTrans)
                {
                    if (item.ArrivalTime <= clock && item.ExecutionTime <= tran.ST && tran.Deadline<=item.Deadline && !item.isCommited)
                        tran = item;
                    if (tran.isCommited)
                        tran = item;
                }
            }
            return tran;
        }
        private static Transaction GetTransactionWPEDLF(List<Transaction> LstTrans, List<DataObject> LstData, int clock)
        {
            LstTransactionWPEDLF=new List<Transaction>();
            LstTransactionWPEDLF.Clear();
            foreach(var item in LstTrans)
            {
                if (!item.isCommited && item.ArrivalTime<=clock)
                    LstTransactionWPEDLF.Add(item);
            }
            LstTransactionWPEDLF.Sort((x, y) => x.Deadline.CompareTo(y.Deadline));
            return LstTransactionWPEDLF[0];
            
        }
        private static Transaction GetTransactionHPEDL(List<Transaction> LstTrans, List<DataObject> LstData, int clock)
        {
            LstTransactionWPEDLF = new List<Transaction>();
            LstTransactionWPEDLF.Clear();
            foreach (var item in LstTrans)
            {
                if (!item.isCommited && item.ArrivalTime <= clock)
                    LstTransactionWPEDLF.Add(item);
            }
            LstTransactionWPEDLF.Sort((x, y) => x.Deadline.CompareTo(y.Deadline));
            return LstTransactionWPEDLF[0];
        }
        private static Transaction GetErlierDeadline(List<Transaction> LstTrans, List<DataObject> LstData, int clock)
        {
            var tran=LstTrans[0];
            int deadline = tran.Deadline;
            foreach (var item in LstTrans)
            {
                if (item.ArrivalTime <= clock && item.Deadline <= deadline && !item.isCommited)
                    tran = item;
            }
            //return tran;
            if (tran.Data.ifAvailable && !tran.Data.Holder.Equals(tran.TransactionName))
            {
                foreach (var data in LstData)
                {
                    if (data.Name.Equals(tran.Data.Name))
                    {
                        data.ifAvailable = false;
                        data.Holder = tran.TransactionName;
                    }
                }
                return tran;
            }
            else 
            {
                int executionTime = tran.Deadline;
                foreach (var item in LstTrans)
                {
                    if (item.ArrivalTime <= clock  && item.ExecutionTime <= executionTime && !item.isCommited)
                        tran = item;
                    if (tran.isCommited)
                        tran = item;
                }
            }
            return tran;
        }
        
        public static bool ifAllCommited(List<Transaction> LstTrans)
        {
            int commeted = 0;
            foreach(var tran in LstTrans)
            {
                if (tran.isCommited)
                    commeted++;
            }
            if (commeted == LstTrans.Count)
                return true;
            else
                return false;
        }
        public static List<ExecutionTime> WEDLF(List<Transaction> LstTrans, List<DataObject> LstData, List<ExecutionTime> LstExtime)
        {
            try
            {
                int clock = 0;
                while(true)
                {
                    var tran = GetErlierDeadline(LstTrans, LstData, clock);
                    tran.ExecutionTime--;
                    if (tran.ExecutionTime == 0)
                    {
                        tran.isCommited = true;
                        tran.Data.ifAvailable = true;
                        
                    }
                    
                    LstExtime.Add(new ExecutionTime() { TransactionName = tran.TransactionName, TimeFrom = clock,Tcolor=tran.Tcolor });
                    clock++;
                    if (ifAllCommited(LstTrans))
                        return LstExtime;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return LstExtime;
            }
        }
        public static List<ExecutionTime> HPEDL(List<Transaction> LstTrans, List<DataObject> LstData, List<ExecutionTime> LstExtime)
        {
            try
            {
                int clock = 0;
                while (true)
                {
                    var tran = GetTransactionHPEDL(LstTrans, LstData, clock);
                    tran.ExecutionTime--;
                    if (tran.ExecutionTime == 0)
                    {
                        tran.isCommited = true;
                        tran.Data.ifAvailable = true;
                    }
                    LstExtime.Add(new ExecutionTime() { TransactionName = tran.TransactionName, TimeFrom = clock, Tcolor = tran.Tcolor });
                    clock++;
                    if (ifAllCommited(LstTrans))
                        return LstExtime;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return LstExtime;
            }
        }
        public static List<ExecutionTime> WPEDL(List<Transaction> LstTrans, List<DataObject> LstData, List<ExecutionTime> LstExtime)
        {
            try
            {
                int clock = 0;
                while (true)
                {
                    var tran = GetTransactionWPEDLF(LstTrans, LstData, clock);
                    tran.ExecutionTime--;
                    if (tran.ExecutionTime == 0)
                    {
                        tran.isCommited = true;
                        tran.Data.ifAvailable = true;
                    }
                    LstExtime.Add(new ExecutionTime() { TransactionName = tran.TransactionName, TimeFrom = clock, Tcolor = tran.Tcolor });
                    clock++;
                    if (ifAllCommited(LstTrans))
                        return LstExtime;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return LstExtime;
            }
        }
        public static List<ExecutionTime> CREDLF(List<Transaction> LstTrans, List<DataObject> LstData, List<ExecutionTime> LstExtime)
        {
            try
            {
                int clock = 0;
                while (true)
                {
                    var tran = GetTransactionCREDLF(LstTrans, LstData, clock);
                    tran.ExecutionTime--;
                    tran.ST--;
                    tran.ST = tran.ST < 0 ? 0 : tran.ST;
                    if (tran.ExecutionTime == 0)
                    {
                        tran.isCommited = true;
                        tran.Data.ifAvailable = true;
                    }
                    LstExtime.Add(new ExecutionTime() { TransactionName = tran.TransactionName, TimeFrom = clock, Tcolor = tran.Tcolor });
                    clock++;
                    if (ifAllCommited(LstTrans))
                        return LstExtime;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return LstExtime;
            }
        }
        public static List<ExecutionTime> CWHP(List<Transaction> LstTrans, List<DataObject> LstData, List<ExecutionTime> LstExtime)
        {
            try
            {
                int clock = 0;
                while (true)
                {
                    var tran = GetTransactionCWHP(LstTrans, LstData, clock);
                    tran.ExecutionTime--;
                    tran.ST--;
                    tran.ST = tran.ST < 0 ? 0 : tran.ST;
                    if (tran.ExecutionTime == 0)
                    {
                        tran.isCommited = true;
                        tran.Data.ifAvailable = true;
                    }
                    LstExtime.Add(new ExecutionTime() { TransactionName = tran.TransactionName, TimeFrom = clock, Tcolor = tran.Tcolor });
                    clock++;
                    if (ifAllCommited(LstTrans))
                        return LstExtime;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return LstExtime;
            }
        }
    }
}
