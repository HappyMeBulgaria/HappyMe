namespace HappyMe.Web.Common.Extensions
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public static class FormFileExtentions
    {
        public static byte[] ToByteArray(this IFormFile file)
        {
            using (var fileStream = file.OpenReadStream())
            using (var target = new MemoryStream())
            {
                fileStream.CopyTo(target);
                return target.ToArray();
            }
        }
    }
}
