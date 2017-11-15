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
            public const int COMPANY_NAME_COLINDEX = 2;
            public const int CONTACT_PERSON_COLINDEX = 4;
            public const int PRODUCTS_MANUFACTURE_COLINDEX = 5;
            public const int CATEGORY_COLINDEX = 7;
            public const int SUB_CATEGORY_COLINDEX = 8;
            public const int ADDRESS_LINE_1_COLINDEX = 9;
            public const int ADDRESS_LINE_2_COLINDEX = 10;
            public const int ADDRESS_LINE_3_COLINDEX = 11;
            public const int MOBILE_COLINDEX = 16;
            public const int OFFICE_NUMBER_COLINDEX = 17;
            public const int FAX_COLINDEX = 18;
            public const int EMAIL_1_COLINDEX = 19;
            public const int EMAIL_2_COLINDEX = 20;
            public const int WEBSITE_COLINDEX = 22;
            public const int CITY_COLINDEX = 23;
            public const int PINCODE_COLINDEX = 24;
            public const int STATE_COLINDEX = 25;
            public const int COUNTRY_COLINDEX = 26;
            public const int CERTIFICATION_COLINDEX = 27;
        }

    }
}
