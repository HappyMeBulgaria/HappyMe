namespace HappyMe.Web.Common.ActionResults.Models
{
    using System.Collections.Generic;

    using MoreDotNet.Extensions.Collections;

    internal class JsonResponse
    {
        public bool Success => this.ErrorMessages.IsNullOrEmpty();

        public object OriginalData { get; set; }

        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
