namespace HappyMe.Common.Colors
{
    using System;

    public class IntensityValue
    {
        private IntensityValue childA;
        private IntensityValue childB;

        public IntensityValue(IntensityValue parent, int value, int level)
        {
            if (level > 7)
            {
                throw new Exception("There are no more colours left");
            }

            this.Value = value;
            this.Parent = parent;
            this.Level = level;
        }

        public int Level { get; set; }

        public int Value { get; set; }

        public IntensityValue Parent { get; set; }

        public IntensityValue ChildA => this.childA ?? (this.childA = new IntensityValue(this, this.Value - (1 << (7 - this.Level)), this.Level + 1));

        public IntensityValue ChildB => this.childB ?? (this.childB = new IntensityValue(this, this.Value + (1 << (7 - this.Level)), this.Level + 1));
    }
}