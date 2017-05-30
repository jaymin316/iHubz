using System.Data;
using System.Drawing;

namespace iHubz.Infrastructure.CrossCutting.Excel
{
    public interface IExcelWorksheet
    {
        /// <summary>
        /// Worksheet name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Count number of column with data
        /// </summary>
        /// <returns></returns>
        int CountDataColumn();

        /// <summary>
        /// Count number of row with data
        /// </summary>
        /// <returns></returns>
        int CountDataRow();

        /// <summary>
        /// Get cell value
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        object GetCellValue(int rowIndex, int colIndex);

        /// <summary>
        /// Get cell value
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        IExcelCell GetCell(int rowIndex, int colIndex);

        /// <summary>
        /// Set cell value
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="value"></param>
        void SetCellValue(int rowIndex, int colIndex, object value);

        /// <summary>
        /// Set cell formula
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="formula"></param>
        void SetCellFormula(int rowIndex, int colIndex, string formula);

        /// <summary>
        /// Set cell style (no color)
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="size"></param>
        /// <param name="fontStyle"></param>
        void SetCellStyle(int rowIndex, int colIndex, int? size, FontStyle fontStyle);

        /// <summary>
        /// Set cell style (with color)
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="size"></param>
        /// <param name="fontStyle"></param>
        /// <param name="foregroundColor"></param>
        void SetCellStyle(int rowIndex, int colIndex, int? size, FontStyle fontStyle, Color foregroundColor);

        /// <summary>
        /// Returns a DataTable with a maximum of maxRows
        /// </summary>
        /// <param name="maxRows"></param>
        /// <returns></returns>
        DataTable CreateDataTable(int maxRows);

        void CopyFrom(IExcelWorksheet sourceExcelWorksheet);

        /// <summary>
        /// Get column name from index
        /// </summary>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        string GetColumnNameFromIndex(int colIndex);
    }
}
