namespace CrashPasswordSystem.UI
{
    public interface IDependencyContainer
    {
        T Resolve<T>();
    }
}