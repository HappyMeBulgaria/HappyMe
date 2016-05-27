﻿namespace HappyMe.Web.Areas.Administration.Controllers.Base
{
    using System.Collections.Generic;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Contracts;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;

    public abstract class MvcGridAdministrationController<TEntity, TViewModel, TCreateModel, TEditModel> :
        AdministrationController
        where TEntity : class, IEntity, new()
        where TViewModel : IMapFrom<TEntity>, new()
        where TCreateModel : IMapTo<TEntity>, new()
        where TEditModel : IMapFrom<TEntity>, IMapTo<TEntity>, new()
    {
        protected MvcGridAdministrationController(
           IUsersDataService userData,
           IAdministrationService<TEntity> dataRepository,
           IMappingService mappingService)
            : base(userData)
        {
            this.AdministrationService = dataRepository;
            this.MappingService = mappingService;
        }

    protected IAdministrationService<TEntity> AdministrationService { get; }

    protected IMappingService MappingService { get; }

    protected IEnumerable<TViewModel> GetData() =>
       this.MappingService.MapCollection<TViewModel>(this.AdministrationService.Read());

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

    protected void BaseDestroy(params object[] id)
    {
        this.AdministrationService.Delete(id);
    }
}
}