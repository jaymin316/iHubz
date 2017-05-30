
namespace iHubz.Infrastructure.CrossCutting.Excel
{
    public static class ExcelManagerFactory
    {
        #region Members

        static IExcelManagerFactory _currentExcelManagerFactory = null;

        #endregion

        #region Public Static Methods

        public static void SetCurrent(IExcelManagerFactory excelManagerFactory)
        {
            _currentExcelManagerFactory = excelManagerFactory;
        }

        public static IExcelManager CreateExcelManager()
        {
            return _currentExcelManagerFactory.Create();
        }

        #endregion
    }
}
