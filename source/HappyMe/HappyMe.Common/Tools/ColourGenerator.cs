namespace HappyMe.Common.Tools
{
    using HappyMe.Common.Colors;

    public class ColourGenerator
    {
        private readonly IntensityGenerator intensityGenerator;
        private int index;

        public ColourGenerator()
        {
            this.intensityGenerator = new IntensityGenerator();
            this.index = 0;
        }

        public string NextColour()
        {
            string colour = string.Format(PatternGenerator.NextPattern(this.index), this.intensityGenerator.NextIntensity(this.index));
            this.index++;
            return colour;
        }
    }
}