using System;
using System.IO;
using System.Text;
using Microsoft.Office.Interop.Excel;

using Range = Microsoft.Office.Interop.Excel.Range;

namespace ExcelToCsvConverter
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        private void Example()
        {
            string csvFilePath = @"C:\path\to\csv\file.csv";
            string excelFilePath = @"C:\path\to\excel\file.xlsx";
            Excel2Csv(excelFilePath, csvFilePath);
            Csv2Excel(csvFilePath, excelFilePath);
        }

        private static void Excel2Csv(string excelFilePath, string csvFilePath)
        {
            // 打开 Excel 文件
            Application excelApp = new Application();
            Workbook workbook = excelApp.Workbooks.Open(excelFilePath);

            // 选择第一个工作表
            Worksheet worksheet = workbook.Sheets[1];

            // 获取数据范围
            Range range = worksheet.UsedRange;
            int rowCount = range.Rows.Count;
            int columnCount = range.Columns.Count;

            // 创建 CSV 文件
            StreamWriter csvFile = new StreamWriter(csvFilePath, false, Encoding.Default);

            // 将 Excel 数据写入 CSV 文件
            for (int i = 1; i <= rowCount; i++)
            {
                StringBuilder line = new StringBuilder();
                for (int j = 1; j <= columnCount; j++)
                {
                    line.Append(Convert.ToString((range.Cells[i, j] as Range).Value2));
                    if (j != columnCount)
                    {
                        line.Append(",");
                    }
                }
                csvFile.WriteLine(line.ToString());
            }

            // 关闭文件
            csvFile.Close();
            workbook.Close();
            excelApp.Quit();
        }

        private static void Csv2Excel(string csvFilePath,string excelFilePath)
        {
            Application excel = new Application();
            Workbook workbook = excel.Workbooks.Add();
            Worksheet worksheet = workbook.ActiveSheet;

            // 读取CSV文件并将数据导入Excel工作表中
            string[] csvLines = File.ReadAllLines(csvFilePath);
            for (int i = 0; i < csvLines.Length; i++)
            {
                string[] fields = csvLines[i].Split(',');
                for (int j = 0; j < fields.Length; j++)
                {
                    worksheet.Cells[i + 1, j + 1] = fields[j];
                }
            }

            // 将工作簿保存为Excel文件
            workbook.SaveAs(excelFilePath, XlFileFormat.xlOpenXMLWorkbook);
            workbook.Close();
            excel.Quit();
        }
    }
}