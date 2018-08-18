using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RTDBSystem
{
    public partial class FrmMain : Form
    {
        List<Transaction> LstTransactionWEDLF { get; set; }
        List<Transaction> LstTransactionCREDLF { get; set; }
        List<Transaction> LstTransactionHPEDL { get; set; }
        List<Transaction> LstTransactionCWHP { get; set; }

        List<DataObject> LstDataWEDLF { get; set; }
        List<DataObject> LstDataCREDLF { get; set; }
        List<DataObject> LstDataHPEDL { get; set; }
        List<DataObject> LstDataCWHP { get; set; }

        List<ExecutionTime> LstExTimeWEDLF { get; set; }
        List<ExecutionTime> LstExTimeCREDLF { get; set; }
        List<ExecutionTime> LstExTimeHPEDL { get; set; }
        List<ExecutionTime> LstExTimeCWHP { get; set; }


        private int Tcounter = 0;

        public FrmMain()
        {
            InitializeComponent();
        }
        private Transaction InitializeTransaction(int i)
        {
            //Transaction generator
            try
            {
                Transaction transaction = new Transaction();
                Random rnd = new Random();
                transaction.TransactionName = "T-" + i;
                transaction.ArrivalTime = i;
                transaction.ExecutionTime = rnd.Next(2, 6);
                transaction.Deadline = (transaction.ExecutionTime*2) + transaction.ArrivalTime ;
                
                transaction.isCommited = false;
                transaction.ST = transaction.Deadline - (transaction.ExecutionTime + transaction.ArrivalTime);
                transaction.Tcolor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                return transaction;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private DataObject InitializeData()
        {
            try
            {
                DataObject data = new DataObject();
                Random rnd = new Random();
                data.Name = ((Char)rnd.Next(65,69)).ToString();
                data.ifAvailable = false;
                return data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void btnTgenerator_Click(object sender, EventArgs e)
        {
            
            /*var data = InitializeData();
            LstData.Add(data);*/
            Random rnd = new Random();

            var transactionWEDLF = InitializeTransaction(Tcounter);
            transactionWEDLF.Data = LstDataWEDLF[rnd.Next(LstDataWEDLF.Count)];

            var transactionCREDLF = InitializeTransaction(Tcounter);
            transactionCREDLF.Data = LstDataCREDLF[rnd.Next(LstDataCREDLF.Count)];

            var transactionHPEDL = InitializeTransaction(Tcounter);
            transactionHPEDL.Data = LstDataHPEDL[rnd.Next(LstDataHPEDL.Count)];

            var transactionCWHP = InitializeTransaction(Tcounter);
            transactionCWHP.Data = LstDataCWHP[rnd.Next(LstDataCWHP.Count)];

            Tcounter++;

            LstTransactionWEDLF.Add(transactionWEDLF);
            LstTransactionCREDLF.Add(transactionCREDLF);
            LstTransactionHPEDL.Add(transactionHPEDL);
            LstTransactionCWHP.Add(transactionCWHP);

            dgvTransaction.Rows.Add(transactionWEDLF.TransactionName, transactionWEDLF.ArrivalTime, transactionWEDLF.ExecutionTime, transactionWEDLF.Deadline, transactionWEDLF.Data.Name);
            //dgvCWHP.Rows.Add(transaction1.TransactionName, transaction1.ArrivalTime, transaction1.ExecutionTime, transaction1.Deadline, transaction1.Data.Name);
            dgvTransaction.Rows[LstTransactionWEDLF.Count - 1].DefaultCellStyle.BackColor = transactionWEDLF.Tcolor;
            //dgvCWHP.Rows[LstTransactionWEDLF.Count - 1].DefaultCellStyle.BackColor = transaction1.Tcolor;

            dgvTransaction.ClearSelection();
            //dgvCWHP.ClearSelection();

            lblTno.Text = "No of Transaction= " + dgvTransaction.Rows.Count;
            
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            #region define list object
            LstDataWEDLF = new List<DataObject>();
            LstDataCREDLF = new List<DataObject>();
            LstDataHPEDL = new List<DataObject>();
            LstDataCWHP = new List<DataObject>();

            LstTransactionWEDLF = new List<Transaction>();
            LstTransactionCREDLF = new List<Transaction>();
            LstTransactionHPEDL = new List<Transaction>();
            LstTransactionCWHP = new List<Transaction>();

            LstExTimeWEDLF = new List<ExecutionTime>();
            LstExTimeCREDLF = new List<ExecutionTime>();
            LstExTimeHPEDL = new List<ExecutionTime>();
            LstExTimeCWHP = new List<ExecutionTime>();
            #endregion

            #region add data to list
            LstDataWEDLF.Add(new DataObject() { Name = "A", Holder = "", ifAvailable = true });
            LstDataWEDLF.Add(new DataObject() { Name = "B", Holder = "", ifAvailable = true });
            LstDataWEDLF.Add(new DataObject() { Name = "C", Holder = "", ifAvailable = true });
            LstDataWEDLF.Add(new DataObject() { Name = "D", Holder = "", ifAvailable = true });
            LstDataWEDLF.Add(new DataObject() { Name = "E", Holder = "", ifAvailable = true });

            LstDataCREDLF.Add(new DataObject() { Name = "A", Holder = "", ifAvailable = true });
            LstDataCREDLF.Add(new DataObject() { Name = "B", Holder = "", ifAvailable = true });
            LstDataCREDLF.Add(new DataObject() { Name = "C", Holder = "", ifAvailable = true });
            LstDataCREDLF.Add(new DataObject() { Name = "D", Holder = "", ifAvailable = true });
            LstDataCREDLF.Add(new DataObject() { Name = "E", Holder = "", ifAvailable = true });

            LstDataHPEDL.Add(new DataObject() { Name = "A", Holder = "", ifAvailable = true });
            LstDataHPEDL.Add(new DataObject() { Name = "B", Holder = "", ifAvailable = true });
            LstDataHPEDL.Add(new DataObject() { Name = "C", Holder = "", ifAvailable = true });
            LstDataHPEDL.Add(new DataObject() { Name = "D", Holder = "", ifAvailable = true });
            LstDataHPEDL.Add(new DataObject() { Name = "E", Holder = "", ifAvailable = true });

            LstDataCWHP.Add(new DataObject() { Name = "A", Holder = "", ifAvailable = true });
            LstDataCWHP.Add(new DataObject() { Name = "B", Holder = "", ifAvailable = true });
            LstDataCWHP.Add(new DataObject() { Name = "C", Holder = "", ifAvailable = true });
            LstDataCWHP.Add(new DataObject() { Name = "D", Holder = "", ifAvailable = true });
            LstDataCWHP.Add(new DataObject() { Name = "E", Holder = "", ifAvailable = true });
            #endregion

            
        }
        private void clear()
        {
            //LstData.Clear();
            LstTransactionWEDLF.Clear();
            LstTransactionCREDLF.Clear();
            LstTransactionHPEDL.Clear();
            LstTransactionCWHP.Clear();

            LstExTimeWEDLF.Clear();
            LstExTimeCREDLF.Clear();
            LstExTimeHPEDL.Clear();
            LstExTimeCWHP.Clear();

            Tcounter = 0;

            dgvTransaction.Rows.Clear();

            dgvChartWEDLF.Rows.Clear();
            dgvChartWEDLF.Columns.Clear();

            dgvChartCREDLF.Rows.Clear();
            dgvChartCREDLF.Columns.Clear();

            dgvChartHPEDL.Rows.Clear();
            dgvChartHPEDL.Columns.Clear();
            //dgvCWHP.Rows.Clear();
            dgvChartCWHP.Rows.Clear();
            dgvChartCWHP.Columns.Clear();

            lblCWHPsr.Text = "100";
            lblCREDLFsr.Text = "100";
            lblHPEDLFsr.Text = "100";
            lblWEDLFsr.Text = "100";

            rchCommitedCWHP.Clear();
            rchCommitedWEDLF.Clear();
            rchCommitedHPEDL.Clear();
            rchCommitedCREDLF.Clear();

            rchLogCWHP.Clear();
            rchLogCREDLF.Clear();
            rchLogHPEDL.Clear();
            rchLogWEDLF.Clear();

            chrtCWHP.Series.Clear();
            chrtWEDLF.Series.Clear();
            chrtHPEDL.Series.Clear();
            chrtCREDLF.Series.Clear();
            chrtSuccessRatio.Series.Clear();

            lblTno.Text = "No of Transaction= " + dgvTransaction.Rows.Count;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            #region WEDLF
            var logWEDLF = Algorithm.WEDLF(LstTransactionWEDLF, LstDataWEDLF, LstExTimeWEDLF);
            WriteLog(logWEDLF, rchLogWEDLF);
            WriteCommited(LstTransactionWEDLF, logWEDLF, rchCommitedWEDLF);
            drowChart(LstExTimeWEDLF,dgvChartWEDLF);
            lblWEDLFsr.Text =  calcSuccess(LstTransactionWEDLF, logWEDLF).ToString() ;
            #endregion

            #region CREDLF
            var logWPEDLF = Algorithm.CREDLF(LstTransactionCREDLF, LstDataCREDLF, LstExTimeCREDLF);
            WriteLog(logWPEDLF, rchLogCREDLF);
            WriteCommited(LstTransactionCREDLF, logWPEDLF, rchCommitedCREDLF);
            drowChart(LstExTimeCREDLF, dgvChartCREDLF);
            lblCREDLFsr.Text = calcSuccess(LstTransactionCREDLF, logWPEDLF).ToString();
            #endregion

            #region HPEDL
            var logHPEDL = Algorithm.HPEDL(LstTransactionHPEDL, LstDataHPEDL, LstExTimeHPEDL);
            WriteLog(logHPEDL, rchLogHPEDL);
            WriteCommited(LstTransactionHPEDL, logHPEDL, rchCommitedHPEDL);
            drowChart(LstExTimeHPEDL, dgvChartHPEDL);
            lblHPEDLFsr.Text = calcSuccess(LstTransactionHPEDL, logHPEDL).ToString();
            #endregion

            #region CWHP
            var logCWHP = Algorithm.CWHP(LstTransactionCWHP, LstDataCWHP, LstExTimeCWHP);
            WriteLog(logCWHP, rchLogCWHP);
            WriteCommited(LstTransactionCWHP, logCWHP, rchCommitedCWHP);
            drowChart(LstExTimeCWHP, dgvChartCWHP);
            lblCWHPsr.Text = calcSuccess(LstTransactionCWHP, logCWHP).ToString();
            #endregion


            ChartBar(LstExTimeWEDLF, chrtWEDLF);
            ChartBar(LstExTimeCREDLF, chrtCREDLF);
            ChartBar(LstExTimeCWHP, chrtCWHP);
            ChartBar(LstExTimeHPEDL, chrtHPEDL);
            ChartAll();
        }
        public void WriteLog(List<ExecutionTime> log,RichTextBox rchTxt)
        {
            rchTxt.Clear();
            foreach (var commit in log)
            {
                rchTxt.Text += "(" + commit.TransactionName + ")" + " - " + commit.TimeFrom + System.Environment.NewLine;
            }
        }
        public double calcSuccess(List<Transaction> LstTran, List<ExecutionTime> log)
        {
            double SR = 0;
            double TransactionNumber = LstTran.Count;
            double CommitedNumber = LstTran.Count;
            for (int i = 0; i < LstTran.Count; i++)
            {
                int maxDeadLine = 0;
                for (int j = 0; j < log.Count; j++)
                {
                    if (LstTran[i].TransactionName.Equals(log[j].TransactionName) && log[j].TimeFrom > maxDeadLine)
                        maxDeadLine = log[j].TimeFrom;
                }
                if (maxDeadLine > LstTran[i].Deadline)
                    CommitedNumber--;
            }
            SR = Math.Truncate(CommitedNumber / TransactionNumber * 100);

            return SR;
        }
        public void WriteCommited(List<Transaction>LstTran , List<ExecutionTime>log , RichTextBox rchCommited)
        {
            rchCommited.Clear();
            for (int i = 0; i < LstTran.Count; i++)
            {
                int maxDeadLine = 0;
                for (int j = 0; j < log.Count; j++)
                {
                    if (LstTran[i].TransactionName.Equals(log[j].TransactionName) && log[j].TimeFrom > maxDeadLine)
                        maxDeadLine = log[j].TimeFrom;
                }
                rchCommited.Text += "(" + LstTran[i].TransactionName + ") => " + maxDeadLine;
                if (maxDeadLine > LstTran[i].Deadline)
                    rchCommited.Text += "   (miss its deadline)" + System.Environment.NewLine;
                else
                    rchCommited.Text += System.Environment.NewLine;
            }
        }
        public void drowChart(List<ExecutionTime>LstTime,DataGridView dgv)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            foreach (var commit in LstTime)
            {
                var col1 = new DataGridViewTextBoxColumn();

                col1.HeaderText = commit.TimeFrom.ToString();
                col1.Name = commit.TransactionName;
                col1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                dgv.Columns.AddRange(new DataGridViewColumn[] { col1 });

                dgv.Rows[0].Cells[commit.TimeFrom].Value = (commit.TransactionName);
                dgv.Rows[0].Cells[commit.TimeFrom].Style.BackColor = commit.Tcolor;
            }
        }
        
        public void ChartBar(List<ExecutionTime> LstTime,Chart Mychrt)
        {
            Mychrt.Series.Clear();

            foreach (var commit in LstTime)
            {
                int i = 0;
                var series = new Series(commit.TransactionName);
                foreach (var ser in Mychrt.Series)
                {
                    if (ser.Name.Equals(series.Name))
                        i = 1;
                }
                if (i == 0)
                    Mychrt.Series.Add(series);

                //chrtLine.Series[commit.TransactionName].ChartType = SeriesChartType.Line;
                Mychrt.Series[commit.TransactionName].Color = commit.Tcolor;
                Mychrt.Series[commit.TransactionName].Points.AddXY(" ",commit.TimeFrom);
                i++;
            }
        }
        
        public void ChartAll()
        {
            chrtSuccessRatio.Series.Clear();
            
            var series = new Series("WEDLF");
            var series1 = new Series("CREDLF");
            var series2 = new Series("HPEDL");
            var series3 = new Series("CWHP");

            chrtSuccessRatio.Series.Add(series);
            chrtSuccessRatio.Series.Add(series1);
            chrtSuccessRatio.Series.Add(series2);
            chrtSuccessRatio.Series.Add(series3);

            chrtSuccessRatio.Series["WEDLF"].Points.AddXY(" ", int.Parse(lblWEDLFsr.Text));
            chrtSuccessRatio.Series["CREDLF"].Points.AddXY(" ", int.Parse(lblCREDLFsr.Text));
            chrtSuccessRatio.Series["HPEDL"].Points.AddXY(" ", int.Parse(lblHPEDLFsr.Text));
            chrtSuccessRatio.Series["CWHP"].Points.AddXY(" ", int.Parse(lblCWHPsr.Text));
        }

        
    }
}
