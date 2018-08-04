using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.IO;
using System.Linq;

namespace BalanceSheetComparer
{
    class ExcelOperation
    {
        private const int debitColumnIndex = 2;
        private const int creditColumnIndex = 3;

        private string GetConnectionString(string filePath)
        {
            var databaseProperties = new Dictionary<string, string>();

            // XLSX - Excel 2007, 2010, 2012, 2013
            databaseProperties["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
            databaseProperties["Extended Properties"] = "'Excel 12.0 XML; HDR=NO; IMEX=1;'";
            databaseProperties["Data Source"] = filePath;

            #region XLS - Excel 2003 and Older
            // XLS - Excel 2003 and Older
            //props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
            //props["Extended Properties"] = "Excel 8.0";
            //props["Data Source"] = "C:\\MyExcel.xls";
            #endregion XLS - Excel 2003 and Older

            var connectionString = new StringBuilder();
            foreach (KeyValuePair<string, string> prop in databaseProperties)
            {
                connectionString.Append(prop.Key);
                connectionString.Append('=');
                connectionString.Append(prop.Value);
                connectionString.Append(';');
            }
            return connectionString.ToString();
        }

        private string WriteFileConnectionString(string filePath)
        {
            var databaseProperties = new Dictionary<string, string>();

            // XLSX - Excel 2007, 2010, 2012, 2013
            databaseProperties["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
            databaseProperties["Extended Properties"] = "'Excel 12.0 XML; HDR=NO;'";
            databaseProperties["Data Source"] = filePath;

            var connectionString = new StringBuilder();
            foreach (KeyValuePair<string, string> prop in databaseProperties)
            {
                connectionString.Append(prop.Key);
                connectionString.Append('=');
                connectionString.Append(prop.Value);
                connectionString.Append(';');
            }
            return connectionString.ToString();
        }

        public DataSet ReadExcelFile(string filePath)
        {
            var dataSet = new DataSet();
            var connectionString = GetConnectionString(filePath);
            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                var command = new OleDbCommand();
                command.Connection = connection;

                // Get all Sheets in Excel File
                DataTable dtSheet = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                // Loop through all Sheets to get data
                foreach (DataRow dataRow in dtSheet.Rows)
                {
                    var sheetName = dataRow["TABLE_NAME"].ToString();
                    if (!sheetName.EndsWith("$"))
                        continue;

                    // Get all rows from the Sheet
                    command.CommandText = "SELECT * FROM [" + sheetName + "]";

                    var dataTable = new DataTable();
                    dataTable.TableName = sheetName;

                    var dataAdapter = new OleDbDataAdapter(command);
                    dataAdapter.Fill(dataTable);
                    dataSet.Tables.Add(dataTable);
                }
                command = null;
                connection.Close();
            }
            return dataSet;
        }

        public void CompareFiles(
            DataTable firstFileExcelData,
            DataTable secondFileExcelData)
        {
            var firstFileDebits = AllDebits(firstFileExcelData.Rows);
            var firstFileCredits = AllCredits(firstFileExcelData.Rows);

            var secondFileDebits = AllDebits(secondFileExcelData.Rows);
            var secondFileCredits = AllCredits(secondFileExcelData.Rows);

            var queries = GenerateQueryForUnMatchedData(
                firstFileDebits,
                secondFileDebits,
                firstFileCredits,
                secondFileCredits);

            CompareAndWriteFile(queries);
        }

        private List<double> AllDebits(DataRowCollection dataRowCollection)
        {
            var debits = new List<double>();
            foreach (DataRow item in dataRowCollection)
            {
                if (item.ItemArray.Length - 1 < debitColumnIndex)
                    continue;

                if (item.ItemArray[debitColumnIndex].ToString() == string.Empty)
                    continue;

                try
                {
                    debits.Add(Convert.ToDouble(item.ItemArray[debitColumnIndex]));
                }
                catch
                {
                    continue;
                }
            }
            return debits;
        }

        private List<double> AllCredits(DataRowCollection dataRowCollection)
        {
            var credits = new List<double>();
            foreach (DataRow item in dataRowCollection)
            {
                if (item.ItemArray.Length - 1 < creditColumnIndex)
                    continue;

                if (item.ItemArray[creditColumnIndex] != null)
                {
                    try
                    {
                        credits.Add(Convert.ToDouble(item.ItemArray[creditColumnIndex]));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            return credits;
        }

        private void CompareAndWriteFile(List<string> queries)
        {
            var writeFilePath = System.Windows.Forms.Application.StartupPath + "\\ComparedResults.xlsx";
            var connectionString = WriteFileConnectionString(writeFilePath);
            if (File.Exists(writeFilePath))
                File.Delete(writeFilePath);

            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                var command = new OleDbCommand();
                command.Connection = connection;

                command.CommandText = "CREATE TABLE [table1] (Information VARCHAR, results NUMBER);";
                command.ExecuteNonQuery();

                foreach (var query in queries)
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        private List<string> GenerateQueryForUnMatchedData(
            List<double> debits1,
            List<double> debits2,
            List<double> credits1,
            List<double> credits2)
        {
            var query1 = GenerateQueryForUnMatchedData(debits1, debits2);
            var query2 = GenerateQueryForUnMatchedData(debits2, debits1);
            var query3 = GenerateQueryForUnMatchedData(credits1, credits2);
            var query4 = GenerateQueryForUnMatchedData(credits2, credits1);
            return query1.Concat(query2).Concat(query3).Concat(query4).ToList();
        }

        private List<string> GenerateQueryForUnMatchedData(
            List<double> collection1,
            List<double> collection2)
        {
            var unMatchedData = collection1.Except(collection2);
            return unMatchedData.Select(x => "INSERT INTO [table1] VALUES ('DebitsCredits', " + x + ");").ToList();
        }
    }
}