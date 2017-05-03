namespace HappyMe.Web.Common.Extensions
{
    using System.IO;
    using System.Web;

    public static class HttpPostedFileBaseExtentions
    {
        public static byte[] ToByteArray(this HttpPostedFileBase file)
        {
            using (var target = new MemoryStream())
            {
                file.InputStream.CopyTo(target);
                return target.ToArray();
            }
        }
    }
}
