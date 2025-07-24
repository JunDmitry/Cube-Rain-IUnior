namespace Assets.Scripts
{
    public abstract class Pool
    {
        protected readonly int InitialSize;

        protected Pool(int initialSize)
        {
            InitialSize = initialSize;
        }
    }
}