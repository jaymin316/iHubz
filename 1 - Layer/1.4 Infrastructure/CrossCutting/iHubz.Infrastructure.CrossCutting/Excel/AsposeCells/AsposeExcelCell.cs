using System;
using Aspose.Cells;

namespace iHubz.Infrastructure.CrossCutting.Excel.AsposeCells
{
    public class AsposeExcelCell : IExcelCell
    {
        private readonly Cell _cell = null;

        #region Constructors

        public AsposeExcelCell(Cell cell)
        {
            if (cell == null)
                throw new NullReferenceException("cell");

            _cell = cell;
        }

        #endregion

        #region IExcelCell Properties

        public double DoubleValue
        {
            get
            {
                return _cell.DoubleValue;
            }
        }

        public DateTime DateTimeValue
        {
            get
            {
                return _cell.DateTimeValue;
            }
        }

        public bool BoolValue
        {
            get
            {
                return _cell.BoolValue;
            }
        }

        public string StringValue
        {
            get
            {
                return _cell.StringValue;
            }
        }

        public bool IsPercent
        {
            get
            {
                return _cell.GetStyle().IsPercent;
            }
        }

        public bool IsErrorValue
        {
            get
            {
                return _cell.IsErrorValue;
            }
        }

        public ExcelCellType Type
        {
            get
            {
                return (ExcelCellType)_cell.Type;
            }
        }

        public string Name
        {
            get
            {
                return _cell.Name;
            }
        }
        public int Row
        {
            get
            {
                return _cell.Row;
            }
        }
        public int Column
        {
            get
            {
                return _cell.Column;
            }
        }

        public object Value
        {
            get { return _cell.Value; }
        }

        #endregion
    }
}
