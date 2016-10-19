namespace BingRestServices
{
    public class KeyType
    {
        protected KeyType() { }

        protected KeyType(string key)
        {
            Key = key;
        }

        public string Key { get; private set; }

        public static T Create<T>(string key) where T : KeyType, new()
        {
            return  new T { Key = key };
        }
    }
}