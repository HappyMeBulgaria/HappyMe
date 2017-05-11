using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HappyMe.Web.Common.Extensions
{
    using HappyMe.Common.Constants;

    public static class TempDataExtentions
    {
        public static void AddDangerMessage(this ITempDataDictionary tempData, string message)
        {
            tempData.Add(GlobalConstants.DangerMessage, message);
        }

        public static void AddInfoMessage(this ITempDataDictionary tempData, string message)
        {
            tempData.Add(GlobalConstants.InfoMessage, message);
        }

        public static void AddSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            tempData.Add(GlobalConstants.SuccessMessage, message);
        }

        public static void AddWarningMessage(this ITempDataDictionary tempData, string message)
        {
            tempData.Add(GlobalConstants.WariningMessage, message);
        }
    }
}
