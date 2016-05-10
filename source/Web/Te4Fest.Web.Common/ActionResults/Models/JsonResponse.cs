namespace Te4Fest.Web.Common.ActionResults.Models
{
    using System.Collections.Generic;

    using MoreDotNet.Extentions.Common;

    internal class JsonResponse
    {
        public bool Success
        {
            get
            {
                return this.ErrorMessages.IsNullOrEmpty();
            }
        }

        public object OriginalData { get; set; }

        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
