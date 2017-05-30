using System;

namespace iHubz.Infrastructure.CrossCutting.Excel
{
    public interface IExcelCell
    {
        DateTime DateTimeValue { get; }
        double DoubleValue { get; }
        bool BoolValue { get; }
        bool IsErrorValue { get; }
        string StringValue { get; }
        bool IsPercent { get; }
        ExcelCellType Type { get; }
        string Name { get; }
        int Row { get; }
        int Column { get; }
        object Value { get; }
    }
}
