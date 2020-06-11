using GemBox.Spreadsheet;
using GemBox.Spreadsheet.WinFormsUtilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBLA_Application
{
    class CommonFunctions
    {
        public static int numberNonActive;
        public static int numberActive;
        public static int exists = 2;
        public static int numberOwing;
        public static int amountOwedTotal;

        public static void exportToExcel(DataGridView dgv, string name, bool footer)
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            // Load Excel (XLSX) from a file.
            ExcelFile workbook = new ExcelFile();
            ExcelWorksheet worksheet = workbook.Worksheets.Add($"{name}");
            worksheet.Cells["A1"].Value = "";
            workbook.Save($"{name}.xlsx");
            var workbook1 = ExcelFile.Load($"{name}.xlsx");

            // Import DataGridView back to active worksheet.
            DataGridViewConverter.ImportFromDataGridView(
                workbook1.Worksheets.ActiveWorksheet,
                dgv,
                new ImportFromDataGridViewOptions() { ColumnHeaders = true });

            // Save Excel (XLSX) to a file.
            workbook1.Save($"{name}.xls");
            File.Delete($"{name}.xlsx");
            if (footer == true)
            {
                SheetHeaderFooter hfooter = workbook1.Worksheets.ActiveWorksheet.HeadersFooters;
                hfooter.DefaultPage.Footer.CenterSection.Content = $"Number of Non-Active Members: {numberNonActive},\nNumber of Active Members: {numberActive},\nNumber of Members Owing: {numberOwing},\nTotal Amount Owed: {amountOwedTotal}";
                workbook1.Save($"{name}.xls");
            }
        }

        public static int checkIfRowExists(string value)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source = FBLADatabase.sqlite;Version=3;");
            connection.Open();
            string connectionString = $"Select exists(Select 1 from Members where MemberNumber={value} limit 1)";
            SQLiteCommand command2 = new SQLiteCommand(connectionString, connection);
            exists = Convert.ToInt32(command2.ExecuteScalar());
            return exists;
        }
    }
}
