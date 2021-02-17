using SatApp.Clases;
using SatApp.ServiciosDB;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SatApp.VistasModelos
{
    public class SolucionParteSAT_VM
    {
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

        public SolucionParteSAT_VM()
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
