namespace HappyMe.Web.Areas.Administration.Controllers.Base
{
    using HappyMe.Common.Mapping;
    using HappyMe.Data.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Common.Extensions;

    using Microsoft.AspNetCore.Identity;

    public abstract class MvcGridAdministrationCrudController<TEntity, TViewModel, TCreateModel, TEditModel> :
        MvcGridAdministrationReadAndDeleteController<TEntity, TViewModel>
        where TEntity : class, IEntity, new()
        where TViewModel : IMapFrom<TEntity>, new()
        where TCreateModel : IMapTo<TEntity>, new()
        where TEditModel : IMapFrom<TEntity>, IMapTo<TEntity>, new()
    {
        protected MvcGridAdministrationCrudController(
           IUsersDataService userData,
           IAdministrationService<TEntity> dataRepository,
           IMappingService mappingService,
           UserManager<User> userManager)
            : base(userData, dataRepository, mappingService, userManager)
        {
        }
        
        protected TEditModel GetEditModelData(params object[] id) =>
           this.MappingService.Map<TEditModel>(this.AdministrationService.Get(id));

        protected virtual TEntity BaseCreate(TCreateModel model)
        {
            TEntity entity = null;

            if (model != null && this.ModelState.IsValid)
            {
                entity = this.MappingService.Map<TEntity>(model);
                this.AdministrationService.Create(entity);
            }

            return entity;
        }

        protected virtual TEntity BaseUpdate(TEditModel model, params object[] id)
        {
            TEntity entity = null;

            if (model != null && this.ModelState.IsValid)
            {
                entity = this.AdministrationService.Get(id);
                this.MappingService.Map(model, entity);
                this.AdministrationService.Update(entity);
            }

            return entity;
        }
    }
}