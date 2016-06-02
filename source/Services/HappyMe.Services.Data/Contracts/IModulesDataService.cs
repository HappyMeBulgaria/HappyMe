namespace HappyMe.Services.Data.Contracts
{
    using System.Collections.Generic;

    using HappyMe.Data.Models;

    public interface IModulesDataService
    {
        IEnumerable<Module> All();

        IEnumerable<Module> AllActive();

        IEnumerable<Module> AllPublic();

        Module GetById(int id);
    }
}
