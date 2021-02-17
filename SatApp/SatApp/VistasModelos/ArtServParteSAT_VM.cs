using SatApp.Modelos;
using SatApp.Repository;
using SatApp.ServiciosDB;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace SatApp.VistasModelos
{
    public class ArtServParteSAT_VM : INotifyPropertyChanged
    {
        readonly RepositorySatApp database = new RepositorySatApp();


        //Creamos los ObservableCollection que proporciona notificaciones cuando se agregan o quitan elementos, o cuando se actualiza la lista completa.
        private ObservableCollection<Articulos> _listaArticulos;
        private ObservableCollection<Servicios> _listaServicios;
        private ObservableCollection<Regimen_IVA> _listaRegimenIVA;
        private ObservableCollection<SAT_Lineas> _lineasSAT = new ObservableCollection<SAT_Lineas>();


        public ObservableCollection<SAT_Lineas> LineasSAT
        {
            get { return _lineasSAT; }
            set
            {
                _lineasSAT = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Articulos> ListaArticulos
        {
            get { return _listaArticulos; }
            set
            {
                _listaArticulos = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Servicios> ListaServicios
        {
            get { return _listaServicios; }
            set
            {
                _listaServicios = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Regimen_IVA> ListaRegimen_IVA
        {
            get { return _listaRegimenIVA; }
            set
            {
                _listaRegimenIVA = value;
                OnPropertyChanged();
            }
        }

        //En el constructor llamamos las lineas de cada parte.
        public ArtServParteSAT_VM()
        {
            try
            {
                LineasSAT = new ObservableCollection<SAT_Lineas>(database.GetAllLineasParte(Variables.DatosParte.N_Parte.ToString()));
            }
            catch (Exception ex)
            {
                throw ex;
            }               
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Ejecuta el Command al hacer click sobre el objeto.
        public Command<SAT_Lineas> AnadirLinea
        {
            get
            {
                return new Command<SAT_Lineas>((lineaParte) =>
                {
                    LineasSAT.Add(lineaParte);
                });
            }
        }

        public Command<SAT_Lineas> EliminarLinea
        {
            get
            {
                return new Command<SAT_Lineas>((lineaParte) =>
                {
                    LineasSAT.Remove(lineaParte);
                });
            }
        }

    }
}
