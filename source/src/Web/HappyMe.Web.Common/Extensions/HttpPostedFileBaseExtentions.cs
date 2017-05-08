namespace HappyMe.Web.Common.Extensions
{
    using System.IO;


    // TODO: There is no HttpPostedFileBase in asp.net core. We must user IFormFile: http://stackoverflow.com/questions/29836342/mvc-6-httppostedfilebase
    public static class HttpPostedFileBaseExtentions
    {
        //public static byte[] ToByteArray(this HttpPostedFileBase file)
        //{
        //    using (var target = new MemoryStream())
        //    {
        //        file.InputStream.CopyTo(target);
        //        return target.ToArray();
        //    }
        //}
    }
}
