namespace HappyMe.Web.Common.ActionResults.Models
{
    using System.Collections.Generic;
    using System.Linq;

    internal class JsonResponse
    {
        public bool Success => this.ErrorMessages == null || this.ErrorMessages.Any();

        public object OriginalData { get; set; }

        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
