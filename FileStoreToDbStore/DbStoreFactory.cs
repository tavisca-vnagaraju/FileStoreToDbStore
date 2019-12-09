using System;

namespace FileStoreToDbStore
{
    public class DbStoreFactory
    {
        public static IDbStore GetDbStore(string storeName)
        {
            switch (storeName.ToLower())
            {
                case "mongodb":
                    return new MongoDbStore();
                case "mysql":
                    return new MySqlDbStore();
                default:
                    throw new FormatException();
            }
        }
    }
}