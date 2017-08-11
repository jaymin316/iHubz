namespace iHubz.Infrastructure.CrossCutting.Excel.AsposeCells
{
    public sealed class AsposeExcelManagerFactory : IExcelManagerFactory
    {
        public IExcelManager Create()
        {
            return new AsposeExcelManager();
        }
    }
}
