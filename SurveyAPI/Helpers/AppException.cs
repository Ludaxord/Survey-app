using System;
using System.Globalization;

namespace SurveyAPI.Helpers
{
    public class AppException: Exception
    {
        public AppException(): base() {}

        public AppException(string message) : base(message) {
        }

        public AppException(string message, params object[] arguments): base(String.Format(CultureInfo.CurrentCulture, message, arguments)) {

        }
    }
}
