namespace Uno.Runtime.Services
{
    public interface IFactory<out T>
    {
        T Create();
    }

    public class GenericFactory<T> : IFactory<T> where T : new()
    {
        public T Create()
        {
            return new T();
        }
    }

    public interface IFactory<out T, in TArg>
    {
        T Create(TArg arg);
    }
    
    public interface IFactory<out T, in TArg1, in TArg2>
    {
        T Create(TArg1 arg1, TArg2 arg2);
    }
}