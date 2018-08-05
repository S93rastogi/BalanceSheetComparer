using System;
using System.Windows.Forms;

namespace BalanceSheetComparer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void generateReport_Click(object sender, EventArgs e)
        {
            var firstFilePath = firstBalanceSheet.Text;
            var secondFilePath = secondBalanceSheeet.Text;
            try
            {
                ExcelOperation excel =
                        new ExcelOperation(firstFilePath, secondFilePath);
                excel.CompareAndGenearteResultFile();
                MessageBox.Show("Success");
            }
            catch (Exception)
            {
                MessageBox.Show("Failed");
            }
        }
    }
}