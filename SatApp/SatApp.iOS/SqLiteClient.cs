using SatApp.Dependencies;
using SQLite;
using System;
using System.IO;

namespace SatApp.iOS
{
    public class SqLiteClient : IDataBase
    {
        public SQLiteConnection GetConnection()
        {
            try
            {
                string bbddfile = "dbapp.db3";
                //Con esto estoy accediendo al folder especial personal de cada dispositivo. Ademas de que se crea la base de datos.
                string rutadocumentos = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string path = Path.Combine(rutadocumentos, bbddfile);
                SQLiteConnection cn = new SQLiteConnection(path);

                return cn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}