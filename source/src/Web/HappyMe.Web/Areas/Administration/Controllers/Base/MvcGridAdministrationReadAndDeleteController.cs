namespace HappyMe.Web.Areas.Administration.Controllers.Base
{
    using System.Collections.Generic;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;

    using Microsoft.AspNetCore.Identity;

    public abstract class MvcGridAdministrationReadAndDeleteController<TEntity, TViewModel> : AdministrationController
        where TEntity : class, IEntity, new()
        where TViewModel : IMapFrom<TEntity>, new()
    {
        protected MvcGridAdministrationReadAndDeleteController(
            IUsersDataService userData,
            IAdministrationService<TEntity> dataRepository,
            IMappingService mappingService,
            UserManager<User> userManager)
            : base(userData, userManager)
        {
            this.AdministrationService = dataRepository;
            this.MappingService = mappingService;
        }

        protected IAdministrationService<TEntity> AdministrationService { get; }

        protected IMappingService MappingService { get; }

        protected IEnumerable<TViewModel> GetData() =>
           this.MappingService.MapCollection<TViewModel>(this.AdministrationService.Read());

        protected void BaseDestroy(params object[] id)
        {
            this.AdministrationService.Delete(id);
        }
    }
}
