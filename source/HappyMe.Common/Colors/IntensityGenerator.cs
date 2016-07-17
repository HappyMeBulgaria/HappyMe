namespace HappyMe.Common.Colors
{
    public class IntensityGenerator
    {
        private IntensityValueWalker walker;
        private int current;

        public string NextIntensity(int index)
        {
            if (index == 0)
            {
                this.current = 255;
            }
            else if (index % 7 == 0)
            {
                if (this.walker == null)
                {
                    this.walker = new IntensityValueWalker();
                }
                else
                {
                    this.walker.MoveNext();
                }

                this.current = this.walker.Current.Value;
            }

            var currentText = this.current.ToString("X");
            if (currentText.Length == 1)
            {
                currentText = "0" + currentText;
            }

            return currentText;
        }
    }
}