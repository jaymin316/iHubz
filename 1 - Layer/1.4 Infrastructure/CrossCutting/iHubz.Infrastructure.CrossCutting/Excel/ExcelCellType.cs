using System.ComponentModel;

namespace iHubz.Infrastructure.CrossCutting.Excel
{
    public enum ExcelCellType
    {
        [Description("Bool")]
        IsBool = 0,

        [DescriptionAttribute("Date")]
        IsDateTime = 1,

        [DescriptionAttribute("Error")]
        IsError = 2,

        [DescriptionAttribute("Null")]
        IsNull = 3,

        [DescriptionAttribute("Numeric")]
        IsNumeric = 4,

        [DescriptionAttribute("Text")]
        IsString = 5,

        [DescriptionAttribute("Unknown")]
        IsUnknown = 6,
    }
}
