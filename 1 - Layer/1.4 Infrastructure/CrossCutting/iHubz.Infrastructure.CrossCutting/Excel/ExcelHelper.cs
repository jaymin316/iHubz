using System;

namespace iHubz.Infrastructure.CrossCutting.Excel
{
    public class ExcelHelper
    {
        /// <summary>
        /// Returns an Excel type column name (A,B,C, etc...) based on the columnIndex
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public static string GetExcelColumnName(int columnIndex)
        {
            var dividend = columnIndex;
            var columnName = string.Empty;

            while (dividend > 0)
            {
                var modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (dividend - modulo) / 26;
            }
            return columnName;
        }

        /// <summary>
        /// Returns the integer index of a Excel column name
        /// </summary>
        /// <param name="columnLetter"></param>
        /// <returns></returns>
        public static int GetExcelIntColVal(string columnLetter)
        {
            if (string.IsNullOrEmpty(columnLetter))
                throw new ArgumentNullException("columnLetter");

            columnLetter = columnLetter.ToUpperInvariant();
            var sum = 0;

            for (var i = 0; i < columnLetter.Length; i++)
            {
                sum *= 26;
                sum += (columnLetter[i] - 'A' + 1);
            }
            return sum - 1; // 0 based index!
        }

        public static int GetMaxColumnIndex(string columns)
        {
            var maxColumnIndex = -1;
            var cols = columns.Split(',');
            foreach (var col in cols)
            {
                var colIndex = GetExcelIntColVal(col);
                if (colIndex > maxColumnIndex)
                    maxColumnIndex = colIndex;
            }
            return maxColumnIndex + 1; // Add one to give the actual count as it's zero based
        }

        /// <summary>
        /// Returns the Excell cell reference based on indexes
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public static string GetExcelCellName(int columnIndex, int rowIndex)
        {
            //both row & column is zero based
            var rowPosition = (rowIndex + 1).ToString();
            return GetExcelColumnName(columnIndex + 1) + rowPosition;
        }
    }
}
