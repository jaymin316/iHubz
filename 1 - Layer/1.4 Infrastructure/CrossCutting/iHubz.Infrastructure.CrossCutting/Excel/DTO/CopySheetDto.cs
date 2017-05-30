using System.Collections.Generic;

namespace iHubz.Infrastructure.CrossCutting.Excel.DTO
{
    public class CopySheetDto
    {
        // Source
        public IExcelWorkbook SourceWorkbook { get; set; }
        public string SourceFileName { get; set; }

        // Target
        public IExcelWorkbook TargetWorkbook { get; set; }
        public string TargetFileName { get; set; }


        // Mapping between source and target sheet names
        public Dictionary<string, string> SheetNameMap { get; set; }
    }
}
