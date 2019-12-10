using System;

namespace FileStoreToDbStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Started!");
            //mysql or mongodb
            IDbStore dbStore = DbStoreFactory.GetDbStore("mongodb");
            dbStore.ReadFile("/PointOfInterestCoordinatesList.txt");
            dbStore.ConnectToDatabase("PointOfInterest");
            dbStore.InsertData();
            Console.WriteLine("Done!!");
            Console.ReadKey(true);
        }
    }
}
