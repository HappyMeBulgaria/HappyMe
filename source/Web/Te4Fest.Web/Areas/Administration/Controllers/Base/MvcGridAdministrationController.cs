namespace Te4Fest.Web.Areas.Administration.Controllers.Base
{
    using System.Collections.Generic;

    using Te4Fest.Common.Mapping;
    using Te4Fest.Data.Contracts;
    using Te4Fest.Services.Administration.Contracts;
    using Te4Fest.Services.Common.Mapping.Contracts;

    public abstract class MvcGridAdministrationController<TEntity, TViewModel, TCreateModel, TEditModel> : 
        AdministrationController
        where TEntity : class, IEntity, new()
        where TViewModel : IMapFrom<TEntity>, new()
        where TCreateModel : IMapTo<TEntity>, new()
        where TEditModel : IMapFrom<TEntity>, IMapTo<TEntity>, new()
    {
        protected MvcGridAdministrationController(
           IAdministrationService<TEntity> dataRepository,
           IMappingService mappingService)
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

        protected virtual void BeforeCreateAndUpdate(TViewModel viewModel)
        {
        }

        protected virtual void AfterCreateAndUpdate(TViewModel viewModel)
        {
        }

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