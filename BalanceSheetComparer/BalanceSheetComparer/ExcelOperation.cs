using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Runtime.InteropServices;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace BalanceSheetComparer
{
    class ExcelOperation
    {
        private string GetConnectionString()
        {
            var databaseProperties = new Dictionary<string, string>();

            // XLSX - Excel 2007, 2010, 2012, 2013
            databaseProperties["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
            databaseProperties["Extended Properties"] = "Excel 12.0 XML";
            databaseProperties["Data Source"] = "D:\\Bank Statement Dr Side.xlsx";

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

        public DataSet ReadExcelFile()
        {
            var dataSet = new DataSet();
            var connectionString = GetConnectionString();
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
    }
}
