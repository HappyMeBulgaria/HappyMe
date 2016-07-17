namespace HappyMe.Common.Colors
{
    public class IntensityValueWalker
    {
        public IntensityValueWalker()
        {
            this.Current = new IntensityValue(null, 1 << 7, 1);
        }

        public IntensityValue Current { get; set; }

        public void MoveNext()
        {
            if (this.Current.Parent == null)
            {
                this.Current = this.Current.ChildA;
            }
            else if (this.Current.Parent.ChildA == this.Current)
            {
                this.Current = this.Current.Parent.ChildB;
            }
            else
            {
                var levelsUp = 1;
                this.Current = this.Current.Parent;
                while (this.Current.Parent != null && this.Current == this.Current.Parent.ChildB)
                {
                    this.Current = this.Current.Parent;
                    levelsUp++;
                }

                if (this.Current.Parent != null)
                {
                    this.Current = this.Current.Parent.ChildB;
                }
                else
                {
                    levelsUp++;
                }

                for (var i = 0; i < levelsUp; i++)
                {
                    this.Current = this.Current.ChildA;
                }
            }
        }
    }
}