﻿using System.Collections.Generic;
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
    }
}
