using System;
using System.Data;
using System.Drawing;
using Aspose.Cells;

namespace iHubz.Infrastructure.CrossCutting.Excel.AsposeCells
{
    public class AsposeExcelWorksheet : IExcelWorksheet
    {
        private readonly Worksheet _worksheet = null;

        public Worksheet Worksheet
        {
            get { return _worksheet; }
        }

        #region Constructors
        public AsposeExcelWorksheet(Worksheet ws)
        {
            if (ws == null)
                throw new NullReferenceException("ws");

            _worksheet = ws;
        }
        #endregion

        #region IExcelWorksheet Members

        /// <summary>
        /// Count number of column with data
        /// </summary>
        /// <returns></returns>
        public int CountDataColumn()
        {
            return _worksheet.Cells.MaxDataColumn + 1;
        }

        /// <summary>
        /// Count number of row with data
        /// </summary>
        /// <returns></returns>
        public int CountDataRow()
        {
            return _worksheet.Cells.MaxDataRow + 1;
        }

        /// <summary>
        /// Worksheet name
        /// </summary>
        public string Name
        {
            get
            {
                return _worksheet.Name;
            }
            set
            {
                // Excel worksheet names cannot be longer than 31 characters.
                _worksheet.Name = value.Length > 31 ? value.Substring(0, 31) : value;
            }
        }

        /// <summary>
        /// Get cell value
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        public object GetCellValue(int rowIndex, int colIndex)
        {
            return _worksheet.Cells[rowIndex, colIndex].Value;
        }

        /// <summary>
        /// Get an IExcelCell
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        public IExcelCell GetCell(int rowIndex, int colIndex)
        {
            return new AsposeExcelCell(_worksheet.Cells[rowIndex, colIndex]);
        }

        /// <summary>
        /// Set cell value
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="value"></param>
        public void SetCellValue(int rowIndex, int colIndex, object value)
        {
            _worksheet.Cells[rowIndex, colIndex].PutValue(value);
        }

        /// <summary>
        /// Set cell formula
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="formula"></param>
        public void SetCellFormula(int rowIndex, int colIndex, string formula)
        {
            string cellFormula = formula;
            if (!cellFormula.StartsWith("="))
                cellFormula = "=" + cellFormula;

            var cell = _worksheet.Cells[rowIndex, colIndex];
            cell.Formula = cellFormula;
        }

        /// <summary>
        /// Set cell style (no color)
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="size"></param>
        /// <param name="fontStyle"></param>
        public void SetCellStyle(int rowIndex, int colIndex, int? size, FontStyle fontStyle)
        {
            SetCellStyle(rowIndex, colIndex, size, fontStyle, Color.Transparent);
        }

        /// <summary>
        /// Set cell style (with color)
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="size"></param>
        /// <param name="fontStyle"></param>
        /// <param name="foregroundColor"></param>
        public void SetCellStyle(int rowIndex, int colIndex, int? size, FontStyle fontStyle, Color foregroundColor)
        {
            var cell = _worksheet.Cells[rowIndex, colIndex];
            var style = cell.GetStyle();

            if (foregroundColor != Color.Transparent)
            {
                style.Pattern = BackgroundType.Solid;
                style.ForegroundColor = foregroundColor;
            }

            style.Font.IsBold = ((fontStyle & FontStyle.Bold) == FontStyle.Bold);
            style.Font.IsItalic = ((fontStyle & FontStyle.Italic) == FontStyle.Italic);

            if (size != null)
                style.Font.Size = size.Value;

            cell.SetStyle(style);
        }

        /// <summary>
        /// Returns a DataTable with a maximum of maxRows
        /// </summary>
        /// <param name="maxRows"></param>
        /// <returns></returns>
        public DataTable CreateDataTable(int maxRows)
        {
            var maxCol = CountDataColumn();
            var maxRow = CountDataRow();
            if (maxRow > maxRows)
                maxRow = maxRows;

            var dt = new DataTable();

            #region Create the column headers

            for (var i = 1; i <= maxCol; i++)
            {
                dt.Columns.Add(ExcelHelper.GetExcelColumnName(i), typeof(string));
            }

            #endregion

            var currRow = -1;
            var dr = dt.NewRow();
            var addedRow = false;

            foreach (Cell c in _worksheet.Cells)
            {
                if (c.Type == CellValueType.IsNull)
                    continue;

                var colName = ExcelHelper.GetExcelColumnName(c.Column + 1);

                if (currRow == -1)
                {
                    addedRow = false;
                    dr = dt.NewRow();
                }
                else if (currRow != c.Row)
                {
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                }

                var value = c.Value;

                switch (c.Type)
                {
                    case CellValueType.IsNumeric:
                        var style = c.GetStyle();
                        if (style != null)
                        {
                            if (style.IsPercent)
                            {
                                value = decimal.Parse(c.Value.ToString()) * 100;
                            }
                        }
                        break;
                    case CellValueType.IsDateTime:
                        value = c.StringValue;
                        break;
                }

                dr[colName] = value;

                if (c.Row > maxRow)
                {
                    // Skip this row as it's not required
                    addedRow = true;
                    break;
                }

                currRow = c.Row;
            }
            if (!addedRow)
            {
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// Copy Excel Worksheet
        /// </summary>
        /// <param name="sourceExcelWorksheet"></param>
        public void CopyFrom(IExcelWorksheet sourceExcelWorksheet)
        {
            var aposeSource = sourceExcelWorksheet as AsposeExcelWorksheet;

            if (aposeSource != null) _worksheet.Copy(aposeSource.Worksheet);
        }

        /// <summary>
        /// Get column name from index
        /// </summary>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        public string GetColumnNameFromIndex(int colIndex)
        {
            return CellsHelper.ColumnIndexToName(colIndex);
        }

        #endregion        
    }
}
