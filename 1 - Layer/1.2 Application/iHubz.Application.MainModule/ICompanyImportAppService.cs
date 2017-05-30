/***************************************************************************************************************
* Author: Jaymin Patel
* Date: 15/03/2017  
* Purpose: Interface consumed by LanguageTranslationController to perform flow related operations
* 
* Date         Author      Change
* 15/03/2017   JP          Initial Version

* ***************************************************************************************************************/
namespace iHubz.Application.MainModule
{
    public interface ICompanyImportAppService
    {
        /// <summary>
        /// Import the language translation excel file coming in as byte stream, and save records appropriately to websheet translation tables
        /// </summary>
        /// <param name="actualFileName"></param>
        /// <param name="fileData"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        bool PerformImport(string actualFileName, byte[] fileData, string username);
    }
}
