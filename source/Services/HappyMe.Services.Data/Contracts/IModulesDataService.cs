namespace HappyMe.Services.Data.Contracts
{
    using System.Collections.Generic;

    using HappyMe.Data.Models;
    using HappyMe.Services.Common;

    public interface IModulesDataService : IService
    {
        IEnumerable<Module> All();

        IEnumerable<Module> AllActive();

        IEnumerable<Module> AllPublic();

        Module GetById(int id);
    }
}
