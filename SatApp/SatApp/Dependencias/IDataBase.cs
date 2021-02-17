namespace SatApp.Dependencies
{
    public interface IDataBase
    {
        //Esto nos devolvera la colección a una plataforma o a otra es decir IOS o Android.
        SQLite.SQLiteConnection GetConnection();
    }
}
