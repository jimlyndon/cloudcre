using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Cloudcre.Test.DataLoader.Marshallers
{
    public abstract class CsvMarshaller
    {
        protected abstract void Marshall(DataTable table);

        public void Load(string csvPath)
        {
            Marshall(ParseCsv(csvPath));
        }

        private DataTable ParseCsv(string path)
        {
            if (!File.Exists(path))
                return null;

            string full = Path.GetFullPath(path);
            string file = Path.GetFileName(full);
            string dir = Path.GetDirectoryName(full);

            //create the "database" connection string 
            string connString = "Provider=Microsoft.Jet.OLEDB.4.0;"
              + "Data Source=\"" + dir + "\\\";"
              + "Extended Properties=\"text;HDR=Yes;FMT=Delimited\"";

            //create the database query
            string query = "SELECT * FROM " + file;

            //create a DataTable to hold the query results
            var dTable = new DataTable();

            //create an OleDbDataAdapter to execute the query
            var dAdapter = new OleDbDataAdapter(query, connString);

            try
            {
                //fill the DataTable
                dAdapter.Fill(dTable);
            }
            catch (InvalidOperationException /*e*/)
            {
            }

            dAdapter.Dispose();

            return dTable;
        }
    }

    public static class DataTableExtensions
    {
        public static T GetRowValue<T>(this DataTable dt, string columname, int rowindex)
        {
            //Type typeParameterType = typeof(T);
            //if (typeParameterType == typeof(decimal))
            //{
            //    decimal value;
            //    string field = dt.Rows[rowindex].Field<string>(columname);
            //    return decimal.TryParse(field, out value) ? value : (decimal?)null;
            //}


            if (dt.Columns.Contains(columname))
            {
                return dt.Rows[rowindex].Field<T>(columname);
            }
            return default(T);
        }
    }
}
