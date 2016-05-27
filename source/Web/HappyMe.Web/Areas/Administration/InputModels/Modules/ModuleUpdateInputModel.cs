namespace HappyMe.Web.Areas.Administration.InputModels.Modules
{
    using System.Web.Mvc;

    public class ModuleUpdateInputModel : ModuleCreateInputModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
    }
}