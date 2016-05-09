namespace Te4Fest.Web.Common.Extensions
{
    using System.Web.Mvc;

    using Te4Fest.Common.Constants;

    public static class TempDataExtentions
    {
        public static void AddDangerMessage(this TempDataDictionary tempData, string message)
        {
            tempData.Add(GlobalConstants.DangerMessage, message);
        }

        public static void AddInfoMessage(this TempDataDictionary tempData, string message)
        {
            tempData.Add(GlobalConstants.InfoMessage, message);
        }

        public static void AddSuccessMessage(this TempDataDictionary tempData, string message)
        {
            tempData.Add(GlobalConstants.SuccessMessage, message);
        }

        public static void AddWarningMessage(this TempDataDictionary tempData, string message)
        {
            tempData.Add(GlobalConstants.WariningMessage, message);
        }
    }
}
