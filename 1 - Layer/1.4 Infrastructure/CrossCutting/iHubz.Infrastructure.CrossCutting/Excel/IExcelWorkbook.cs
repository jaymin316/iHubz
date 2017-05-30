using System.Collections.Generic;
using System.IO;

namespace iHubz.Infrastructure.CrossCutting.Excel
{
    public interface IExcelWorkbook
    {
        IExcelWorksheet AddSheet(string name);
        IExcelWorksheet GetSheet(string name);

        void RemoveSheet(string name);
        void RemoveSheet(int index);

        List<IExcelWorksheet> Sheets { get; }

        MemoryStream SaveToStream();

        void CreateNamedRange(string name, string reference);

        void CopySheet(string sorceSheetName, IExcelWorkbook sourceSheet, string targetSheetName);
    }
}
