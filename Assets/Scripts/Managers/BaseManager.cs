namespace Managers
{
    public abstract class BaseManager
    {
        protected MainManager Manager => MainManager.Instance;
    }
}

