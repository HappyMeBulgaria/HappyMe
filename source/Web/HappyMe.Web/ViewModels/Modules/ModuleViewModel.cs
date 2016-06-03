namespace HappyMe.Web.ViewModels.Modules
{
    using System;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    public class ModuleViewModel : IMapFrom<Module>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageUrl => this.ImageData != null ?
            $"data:image/jpeg;base64,{Convert.ToBase64String(this.ImageData)}"
            : string.Empty;
    }
}