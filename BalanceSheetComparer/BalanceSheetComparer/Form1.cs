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
            ExcelOperation excel = new ExcelOperation();
            var firstFileExcelData = excel.ReadExcelFile(firstFilePath).Tables[0];
            var secondFileExcelData = excel.ReadExcelFile(secondFilePath).Tables[0];
            excel.CompareFiles(firstFileExcelData, secondFileExcelData);
            MessageBox.Show("Success");
        }
    }
}