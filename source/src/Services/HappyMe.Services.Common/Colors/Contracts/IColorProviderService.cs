using System.Collections.Generic;

namespace HappyMe.Services.Common.Colors.Contracts
{
    public interface IColorProviderService : IService
    {
        string GetRandomColorAsString();

        IEnumerable<string> GetRandomColorsAsStrings(int totalNumber);
    }
}
