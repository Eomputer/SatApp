using SatApp.Clases;
using SatApp.Modelos;
using SatApp.Repository;
using SatApp.ServiciosDB;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SatApp.VistasModelos
{
    public class SAT_VM : INotifyPropertyChanged
    {
        //INotifyPropertyChanged: Notifica a los clientes que un valor de propiedad ha cambiado.
        //ObservableCollection: Representa una colección de datos dinámicos que proporciona notificaciones cuando se agregan o quitan elementos, o cuando se actualiza la lista completa.
        //OnPropertyChanged: Tiene lugar cuando cambia un valor de propiedad.
        //CallerMemberName: Permite obtener el método o el nombre de propiedad a la cual fue invocada.

        private ObservableCollection<DatosParte> _listadoPartes = new ObservableCollection<DatosParte>();

        public ObservableCollection<DatosParte> ListadoPartes
        {
            get { return _listadoPartes; }
            set
            {
                _listadoPartes = value;
                OnPropertyChanged();
            }
        }

        //Este método devuelve los partes asignados al técnico.
        public SAT_VM()
        {
            RepositorySatApp database = new RepositorySatApp();

            List<SAT> ListadoSAT = database.GetPartesAbiertos<SAT>(Variables.CodigoPersonal);

            foreach (var sat in ListadoSAT)
            {
                DatosParte parte = database.CargarDatosDeParte(sat);

                ListadoPartes.Add(parte);
                Variables.DatosParte = null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}