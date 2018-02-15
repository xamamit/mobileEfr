
using System;
using System.Diagnostics;

namespace Acs.Common.Util
{
    [Flags]
    public enum ExceptionParts
    {
        Message = 0,
        StackTrace = 1,
        InnerExcp = 2,
        InnerExcpMessage = 3,
        InnerExcpStackTrace = 4
    }

    public static class DebugOutputUtil
    {
        private static string SeparatorLineExce  =    "********************************* EXCEPTION **********************************";
        private static string SeparatorLineInnerExce = "****************************** INNER EXCEPTION *******************************";
        private static string SeparatorLine =     "******************************************************************************";

        public static void WriteLineFullExceBlock(this Exception ex, bool addFlowerBox = true)
        {
            if (String.IsNullOrWhiteSpace(ex?.Message))
            {
                PrintDividerLine();
                var msg = $"In Acs.Common.Util.DebugExtension.WriteLineFullExceBlock(): " +
                          $"parameter ex was either null or empty.";

                return;
            }

            // Write the exception info first
            PrintDividerLine(1);
            PrintExecpLine();
            Debug.WriteLine($"EXCEPTION MESSAGE: {ex.Message}{Environment.NewLine}" +
                            $"EXCEPTION: {ex}{Environment.NewLine}");

            PrintDividerLine(1);
            Debug.WriteLine(String.Empty);

            if (String.IsNullOrWhiteSpace(ex.InnerException?.Message)) return;
            string innerMsg = ex.InnerException?.Message;

            // Write the inner Exception information
            PrintInnerExecLine();
            Debug.WriteLine($"INNER EXCEPTION MESSAGE: {ex.Message}{Environment.NewLine}" +
                            $"INNER EXCEPTION: {ex}{Environment.NewLine}");

            PrintDividerLine(1);
        }

        #region Print Separators
        
        private static void PrintExecpLine()
        {
            Debug.WriteLine(SeparatorLineExce);
        }

        private static void PrintInnerExecLine()
        {
            Debug.WriteLine(SeparatorLineInnerExce);
        }

        private static void PrintDividerLine(int lines = 1)
        {
            int count = lines >= 1 ? lines : 1;

            for (int i = 1; i <= count; i++)
            {
                Debug.WriteLine(SeparatorLine);
            }
        }
        #endregion Print Sepatators

        /// <summary>Determines if the <paramref name="ex"/> is null.</summary>
        /// <param name="ex">The <see cref="Exception"/> being tested for <c>null</c>.</param>
        /// <returns><c>truw</c> if <paramref name="ex"/> is <c>null</c>, otherwise <c>false</c>.</returns>
        public static bool ExceptionIsNull(this Exception ex)
        {
            return (null == ex?.Message);
        }


        /// <summary>Determines if the <c>StackTrace</c> of <paramref name="ex"/> is null.</summary>
        /// <param name="ex">The <see cref="Exception"/> for which the <see cref="Exception.StackTrace"/> 
        /// is being tested for <c>null</c>.</param>
        /// <returns><c>true</c> if the <see cref="Exception.StackTrace"/> of <paramref name="ex"/> is <c>null</c>, 
        /// otherwise <c>false</c>.</returns>
        public static bool StackTraceIsNull(this Exception ex)
        {
            return (null == ex?.StackTrace);
        }



        /// <summary>Determines if the <c>InnerException</c> of <paramref name="ex"/> is null.</summary>
        /// <param name="ex">The <see cref="Exception"/> for which the <see cref="Exception.InnerException"/> 
        /// is being tested for <c>null</c>.</param>
        /// <returns><c>true</c> if the <see cref="Exception.InnerException"/> of <paramref name="ex"/> is <c>null</c>, 
        /// otherwise <c>false</c>.</returns>
        public static bool InnerExceptionIsNull(this Exception ex)
        {        
            return (null == ex?.InnerException?.Message) ? true : false;
        }


        /// <summary>Determines if the <see cref="Exception.InnerException.StackTrace"/> of <paramref name="ex"/> is null.</summary>
        /// <param name="ex">The <see cref="Exception"/> for which the <see cref="Exception.InnerException.StackTrace"/> 
        /// is being tested for <c>null</c>.</param>
        /// <returns><c>true</c> if the <see cref="Exception.InnerException.StackTrace"/> of <paramref name="ex"/> is <c>null</c>, 
        /// otherwise <c>false</c>.</returns>
        public static bool InnerExceptionStackTraceIsNull(this Exception ex)
        {
            return (null == ex?.InnerException?.StackTrace) ? true : false;
        }


        /// <summary>Determines if the <see cref="Exception.InnerException.Message"/> of <paramref name="ex"/> is null.</summary>
        /// <param name="ex">The <see cref="Exception"/> for which the <see cref="Exception.InnerException.Message"/> 
        /// is being tested for <c>null</c>.</param>
        /// <returns><c>true</c> if the <see cref="Exception.InnerException.Message"/> of <paramref name="ex"/> is <c>null</c>, 
        /// otherwise <c>false</c>.</returns>
        public static bool InnerExceptionMessageIsNull(this Exception ex)
        {            
            return (null == ex?.InnerException?.Message) ? true : false;
        }
    }
}