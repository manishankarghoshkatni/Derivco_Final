using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryUi.Models
{
    public class ApiResponse
    {
        public object data;
        public int responseCode; // 0 - Success, 1 - No data Found, 2 - Exception
        public string error;

        public const int Success = 0;
        public const int NoDataFound = 1;
        public const int Exception = 2;
    }
}