namespace FileStoreToDbStore
{
    public interface IDbStore:IFiles
    {
        public void ConnectToDatabase(string name);
        public void InsertData();
    }

}
