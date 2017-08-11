using System.IO;
using Aspose.Cells;
using iHubz.Infrastructure.CrossCutting.Excel.DTO;

namespace iHubz.Infrastructure.CrossCutting.Excel.AsposeCells
{
    /// <summary>
    /// Aspose Excel workbook manager
    /// </summary>
    public sealed class AsposeExcelManager : IExcelManager
    {
        #region IExcelManager members

        /// <summary>
        /// Creates an Excel workbook using Aspose
        /// </summary>
        /// <returns></returns>
        public IExcelWorkbook CreateWorkbook()
        {
            string licPath = @"E:\00. Private\iHubz\2 - Library\Aspose.Cells.lic";

            Aspose.Cells.License lic = new Aspose.Cells.License();

            lic.SetLicense(licPath);
            //var license = new License();
            //license.SetLicense("Aspose.Cells.lic");
            var wb = new Workbook();
            var workbook = new AsposeExcelWorkbook(wb);
            return workbook;
        }

        /// <summary>
        /// Loads an Excel workbook using Aspose
        /// </summary>
        /// <returns></returns>
        public IExcelWorkbook CreateWorkbook(string fileName, byte[] fileData, bool csvAsText = false)
        {
            string licPath = @"E:\00. Private\iHubz\2 - Library\Aspose.Cells.lic";

            Aspose.Cells.License lic = new Aspose.Cells.License();

            lic.SetLicense(licPath);

            //var license = new License();
            //license.SetLicense("Aspose.Cells.lic");

            var loadOptions = new LoadOptions(GetLoadFormat(fileName));

            if (csvAsText &&
                (loadOptions.LoadFormat == LoadFormat.CSV || loadOptions.LoadFormat == LoadFormat.TabDelimited))
                loadOptions.ConvertNumericData = false;

            Workbook workBook;
            using (var ms = new MemoryStream(fileData))
            {
                workBook = new Workbook(ms, loadOptions) { FileName = fileName };
            }
            return new AsposeExcelWorkbook(workBook);
        }

        /// <summary>
        /// Copy excel websheet from the source to the target workbook
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IExcelWorkbook CopySheet(CopySheetDto param)
        {
            var xlsSource = param.SourceWorkbook;
            var xlsTarget = param.TargetWorkbook;

            foreach (var map in param.SheetNameMap)
            {
                var sourceSheetName = map.Key;
                var targetSheetName = map.Value;

                xlsTarget.CopySheet(sourceSheetName, xlsSource, targetSheetName);
            }

            return xlsTarget;
        }

        /// <summary>
        /// Is valid file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileData"></param>        
        /// <returns></returns>
        public bool IsValidFile(string fileName, byte[] fileData)
        {
            var isValid = false;
            /* Check whether the upload file is a valid excel workbook */
            try
            {
                var wb = ExcelManagerFactory.CreateExcelManager();
                wb.CreateWorkbook(fileName, fileData);
                isValid = true;
            }
            catch
            {
                isValid = false;
            }
            return isValid;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Get load format
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private LoadFormat GetLoadFormat(string fileName)
        {
            var loadFormat = LoadFormat.Auto;
            var ext = Path.GetExtension(fileName);
            if (!string.IsNullOrWhiteSpace(ext))
            {
                ext = ext.Trim().ToUpper();
                switch (ext)
                {
                    case ".CSV":
                        loadFormat = LoadFormat.CSV;
                        break;
                    case ".TXT":
                        loadFormat = LoadFormat.TabDelimited;
                        break;
                    case ".XLS":
                        loadFormat = LoadFormat.Excel97To2003;
                        break;
                    case ".XLSB":
                        loadFormat = LoadFormat.Xlsb;
                        break;
                    case ".XLSX":
                        loadFormat = LoadFormat.Xlsx;
                        break;
                }
            }
            return loadFormat;
        }

        #endregion
    }
}
