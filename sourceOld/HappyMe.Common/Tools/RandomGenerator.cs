namespace HappyMe.Common.Tools
{
    using System;

    public sealed class RandomGenerator
    {
        private static readonly object SyncRoot = new object();

        private static volatile Random instance;

        private RandomGenerator()
        {
        }

        public static Random Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new Random();
                        }
                    }
                }

                return instance;
            }
        }
    }
}
