namespace HappyMe.Services.Common.Colors.Contracts
{
    using System.Collections.Generic;

    public interface IColorProviderService : IService
    {
        string GetRandomColorAsString();

        IEnumerable<string> GetRandomColorsAsStrings(int totalNumber);
    }
}
