using SatApp.Clases;
using SatApp.Modelos;
using SatApp.ServiciosDB;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SatApp.VistasModelos
{
    public class FormaPago_VM : INotifyPropertyChanged
    {
        //Datos para forma de pago.        

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

        private ObservableCollection<Forma_Pago> _formasPago;
        public ObservableCollection<Forma_Pago> FormasPago
        {
            get { return _formasPago; }
            set
            {
                _formasPago = value;
                OnPropertyChanged();
            }
        }


        public FormaPago_VM()
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
