namespace iHubz.Application.MainModule.Company
{
    public static class CompanyConstants
    {
        public const int PAGE_SIZE = 10;
        public const string DEFAULT_VIEW_COMPANY_SORT_ORDER = "CompanyName";

        public struct CompanyDetails
        {
            // Row indexes
            public const int HEADER_ROWINDEX = 1;
            public const int DETAILS_ROWINDEX = 2;
            // Column indexes
            public const int COMPANY_NAME_COLINDEX = 0;
            public const int ADDRESS_LINE_1_COLINDEX = 1;
            public const int ADDRESS_LINE_2_COLINDEX = 2;
        }

    }
}
