using System;
using System.Diagnostics;
using System.IO;

namespace InventoryServices.Shared
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

    public class Helper
    {
        public static ApiResponse CreateResponse(string jsonData, int responseCode, string error)
        {
            ApiResponse response = new ApiResponse();
            response.data = jsonData;
            response.responseCode = responseCode;
            response.error = error;
            return response;
        }

        public static ApiResponse CreateDataResponse(object data)
        {
            ApiResponse response = new ApiResponse();
            response.data = data;
            response.responseCode = ApiResponse.Success;
            response.error = "";
            return response;
        }

        public static ApiResponse CreateNoDataResponse()
        {
            ApiResponse response = new ApiResponse();
            response.data = "";
            response.responseCode = ApiResponse.NoDataFound;
            response.error = "No Data Found";
            return response;
        }

        public static ApiResponse CreateErrorResponse(Exception ex)
        {
            ApiResponse response = new ApiResponse();
            response.data = "";
            response.responseCode = ApiResponse.Exception;
            response.error = Helper.GetException(ex).Message.Replace('"', '\'');
            try
            {
                Helper.LogError(response.error, ex);
            }
            catch (Exception e) {
                response.error += "\n" + e.Message.Replace('"', '\'');
            }
            return response;
        }

        private static Exception GetException(Exception ex)
        {
            if (ex.InnerException == null) return ex;
            else
            {
                return Helper.GetException(ex.InnerException);
            }
        }

        public static void LogError(string errorSummary, Exception ex)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\InventoryServicesLogs.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    Helper.WriteError(errorSummary, ex, sw);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    Helper.WriteError(errorSummary, ex, sw);
                }
            }
        }

        private static void WriteError(string errorMessage, Exception ex, StreamWriter sw)
        {
            sw.WriteLine("Error summary: " + errorMessage + " - " + DateTime.Now.ToLongDateString() + " " +DateTime.Now.ToLongTimeString());

            string errorDetails = Helper.GetErrorDetails(ex);
            sw.WriteLine("Error details:" + errorDetails);
        }

        public static string GetErrorDetails(Exception ex)
        {
            StackTrace st = new StackTrace(ex, true);
            StackFrame frame = st.GetFrame(st.FrameCount - 1); 

            string fileName = frame.GetFileName(); 

            string methodName = frame.GetMethod().Name; 

            int line = frame.GetFileLineNumber(); 

            int col = frame.GetFileColumnNumber();

            string errorDetails = "\r\n\tError in file: " + fileName + "\r\n\t" + "Method name: " + methodName + "\r\n\t" + "Error at line number : " + line.ToString() + "\r\n\t" + "At column: " + col.ToString() + "\r\n" ;
            return errorDetails;
        }
    }
}