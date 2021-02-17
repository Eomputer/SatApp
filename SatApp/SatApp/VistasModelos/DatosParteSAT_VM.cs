using SatApp.Clases;
using SatApp.ServiciosDB;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SatApp.VistasModelos
{
    public class DatosParteSAT_VM : INotifyPropertyChanged
    {
        

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return IsRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        private DatosParte _datosParte;
        public DatosParte DatosParte
        {
            get { return _datosParte; }
            set
            {
                _datosParte = value;
                OnPropertyChanged();
            }
        }

        public DatosParteSAT_VM()
        {
            DatosParte = Variables.DatosParte;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

