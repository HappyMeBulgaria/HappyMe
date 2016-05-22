namespace Te4Fest.Web.Areas.Administration.Controllers.Base
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Te4Fest.Common.Mapping;
    using Te4Fest.Data.Contracts;
    using Te4Fest.Services.Administration.Contracts;
    using Te4Fest.Services.Common.Mapping.Contracts;

    public abstract class MvcGridAdministrationController<TEntity, TViewModel> : AdministrationController
        where TEntity : class, IEntity, new()
        where TViewModel : IMapFrom<TEntity>, IMapTo<TEntity>, IIdentifiable<int>, new()
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

        protected virtual void BeforeCreateAndUpdate(TViewModel viewModel)
        {
        }

        protected virtual void AfterCreateAndUpdate(TViewModel viewModel)
        {
        }

        protected virtual TEntity BaseCreate(TViewModel viewModel)
        {
            TEntity entity = null;

            if (viewModel != null && this.ModelState.IsValid)
            {
                entity = this.MappingService.Map<TEntity>(viewModel);
                this.AdministrationService.Create(entity);
            }

            return entity;
        }

        protected virtual TEntity BaseUpdate(TViewModel viewModel)
        {
            TEntity entity = null;

            if (viewModel != null && this.ModelState.IsValid)
            {
                entity = this.AdministrationService.Get(viewModel.Id);
                this.MappingService.Map(viewModel, entity);
                this.AdministrationService.Update(entity);
            }

            return entity;
        }

        protected ActionResult BaseDestroy(TViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                this.AdministrationService.Delete(viewModel.Id);
            }

            return this.GridOperation(viewModel);
        }

        protected JsonResult GridOperation(TViewModel model) => 
            this.Json(new[] 
            {
                model 
            });
    }
}