namespace HappyMe.Services.Common.Colors
{
    using System.Collections.Generic;

    using HappyMe.Common.Tools;
    using HappyMe.Services.Common.Colors.Contracts;

    public class ColorProviderService : IColorProviderService
    {
        public string GetRandomColorAsString()
        {
            var colorGenerator = new ColourGenerator();
            return colorGenerator.NextColour();
        }

        public IEnumerable<string> GetRandomColorsAsStrings(int totalNumber)
        {
            var result = new List<string>(totalNumber);

            for (var i = 0; i < totalNumber; i++)
            {
                result.Add(this.GetRandomColorAsString());
            }

            return result;
        }
    }
}
