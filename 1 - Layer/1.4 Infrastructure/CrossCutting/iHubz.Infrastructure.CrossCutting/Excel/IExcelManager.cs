using iHubz.Infrastructure.CrossCutting.Excel.DTO;

namespace iHubz.Infrastructure.CrossCutting.Excel
{
    /// <summary>
    /// Excel manager interface
    /// </summary>
    public interface IExcelManager
    {
        /// <summary>
        /// Create emtpy workbook
        /// </summary>
        /// <returns></returns>
        IExcelWorkbook CreateWorkbook();

        /// <summary>
        /// Creates a workbook from the parameters provided
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileData"></param>
        /// <param name="csvAsText"></param>
        /// <returns></returns>
        IExcelWorkbook CreateWorkbook(string fileName, byte[] fileData, bool csvAsText = false);

        /// <summary>
        /// Copy websheet from source workbook into the target
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        IExcelWorkbook CopySheet(CopySheetDto param);

        /// <summary>
        /// Is valid file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileData"></param>        
        /// <returns></returns>
        bool IsValidFile(string fileName, byte[] fileData);
    }
}
