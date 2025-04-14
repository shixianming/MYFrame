namespace Game.Main
{
    public class Singleton<T> where T : class, new()
    {
        public static T Instance { get; private set; }

        public Singleton()
        {
            Instance = new T();
        }
    }
}