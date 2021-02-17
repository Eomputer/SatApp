using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SatApp.Clases
{
    //INotifyPropertyChanged: Notifica a los clientes que un valor de propiedad ha cambiado.
    //ObservableCollection: Representa una colección de datos dinámicos que proporciona notificaciones cuando se agregan o quitan elementos, o cuando se actualiza la lista completa.
    //OnPropertyChanged: Tiene lugar cuando cambia un valor de propiedad en la vista y modelo
    //CallerMemberName: Permite obtener el método o el nombre de propiedad a la cual fue invocada.

    public class DatosParte : INotifyPropertyChanged
    {
        public int N_Parte { get; set; }
        public DateTime Fecha { get; set; }
        public int CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Color { get; set; }
        public string Nombre_Comercial { get; set; }
        public string DireccionParte_ { get; set; }
        public string DireccionParte
        {
            get { return DireccionParte_; }
            set
            {
                DireccionParte_ = value;
                OnPropertyChanged(); //Tiene lugar cuando cambia un valor de propiedad en la vista y modelo
            }
        }
        public string Poblacion_ { get; set; }
        public string Poblacion
        {
            get { return Poblacion_; }
            set
            {
                Poblacion_ = value;
                OnPropertyChanged();
            }
        }
        public string Provincia_ { get; set; }
        public string Provincia
        {
            get { return Provincia_; }
            set
            {
                Provincia_ = value;
                OnPropertyChanged();
            }
        }

        public string CodigoPostal_ { get; set; }
        public string CodigoPostal
        {
            get { return CodigoPostal_; }
            set
            {
                CodigoPostal_ = value;
                OnPropertyChanged();
            }
        }

        public string AnomaliaParte_;
        public string AnomaliaParte
        {
            get { return AnomaliaParte_; }
            set
            {
                AnomaliaParte_ = value;
                OnPropertyChanged();
            }
        }
        public string TelefonoCliente_;
        public string TelefonoCliente
        {
            get { return TelefonoCliente_; }
            set
            {
                TelefonoCliente_ = value;
                OnPropertyChanged();
            }
        }
        public string Solucion_;
        public string Solucion
        {
            get { return Solucion_; }
            set
            {
                Solucion_ = value;
                OnPropertyChanged();
            }
        }
        public string Observaciones_;
        public string Observaciones
        {
            get { return Observaciones_; }
            set
            {
                Observaciones_ = value;
                OnPropertyChanged();
            }
        }
        public string FormaPago_;
        public string FormaPago
        {
            get { return FormaPago_; }
            set
            {
                FormaPago_ = value;
                OnPropertyChanged();
            }
        }

        public decimal Total_;
        public decimal Total
        {
            get { return Total_; }
            set
            {
                Total_ = value;
                OnPropertyChanged();
            }
        }

        public bool TieneRecargoEquivalencia_;
        public bool TieneRecargoEquivalencia
        {
            get { return TieneRecargoEquivalencia_; }
            set
            {
                TieneRecargoEquivalencia_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Base1_;
        public decimal Base1
        {
            get { return Base1_; }
            set
            {
                Base1_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Base2_;
        public decimal Base2
        {
            get { return Base2_; }
            set
            {
                Base2_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Base3_;
        public decimal Base3
        {
            get { return Base3_; }
            set
            {
                Base3_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Base4_;
        public decimal Base4
        {
            get { return Base4_; }
            set
            {
                Base4_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Base5_;
        public decimal Base5
        {
            get { return Base5_; }
            set
            {
                Base5_ = value;
                OnPropertyChanged();
            }
        }

        public decimal Base_Total_;
        public decimal Base_Total
        {
            get { return Base_Total_; }
            set
            {
                Base_Total_ = value;
                OnPropertyChanged();
            }
        }

        public decimal TantoIva1_;
        public decimal TantoIva1
        {
            get { return TantoIva1_; }
            set
            {
                TantoIva1_ = value;
                OnPropertyChanged();
            }
        }

        public decimal TantoIva2_;
        public decimal TantoIva2
        {
            get { return TantoIva2_; }
            set
            {
                TantoIva2_ = value;
                OnPropertyChanged();
            }
        }
        public decimal TantoIva3_;
        public decimal TantoIva3
        {
            get { return TantoIva3_; }
            set
            {
                TantoIva3_ = value;
                OnPropertyChanged();
            }
        }
        public decimal TantoIva4_;
        public decimal TantoIva4
        {
            get { return TantoIva4_; }
            set
            {
                TantoIva4_ = value;
                OnPropertyChanged();
            }
        }
        public decimal TantoIva5_;
        public decimal TantoIva5
        {
            get { return TantoIva5_; }
            set
            {
                TantoIva5_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Iva1_;
        public decimal Iva1
        {
            get { return Iva1_; }
            set
            {
                Iva1_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Iva2_ { get; set; }
        public decimal Iva2
        {
            get { return Iva2_; }
            set
            {
                Iva2_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Iva3_ { get; set; }
        public decimal Iva3
        {
            get { return Iva3_; }
            set
            {
                Iva3_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Iva4_ { get; set; }
        public decimal Iva4
        {
            get { return Iva4_; }
            set
            {
                Iva4_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Iva5_ { get; set; }
        public decimal Iva5
        {
            get { return Iva5_; }
            set
            {
                Iva5_ = value;
                OnPropertyChanged();
            }
        }

        public decimal Iva_Total_;
        public decimal Iva_Total
        {
            get { return Iva_Total_; }
            set
            {
                Iva_Total_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Tanto_Equivalencia1_ { get; set; }
        public decimal Tanto_Equivalencia1
        {
            get { return Tanto_Equivalencia1_; }
            set
            {
                Tanto_Equivalencia1_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Tanto_Equivalencia2_ { get; set; }
        public decimal Tanto_Equivalencia2
        {
            get { return Tanto_Equivalencia2_; }
            set
            {
                Tanto_Equivalencia2_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Tanto_Equivalencia3_ { get; set; }
        public decimal Tanto_Equivalencia3
        {
            get { return Tanto_Equivalencia3_; }
            set
            {
                Tanto_Equivalencia3_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Tanto_Equivalencia4_ { get; set; }
        public decimal Tanto_Equivalencia4
        {
            get { return Tanto_Equivalencia4_; }
            set
            {
                Tanto_Equivalencia4_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Tanto_Equivalencia5_ { get; set; }
        public decimal Tanto_Equivalencia5
        {
            get { return Tanto_Equivalencia5_; }
            set
            {
                Tanto_Equivalencia5_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Recargo_Equivalencia1_ { get; set; }
        public decimal Recargo_Equivalencia1
        {
            get { return Recargo_Equivalencia1_; }
            set
            {
                Recargo_Equivalencia1_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Recargo_Equivalencia2_ { get; set; }
        public decimal Recargo_Equivalencia2
        {
            get { return Recargo_Equivalencia2_; }
            set
            {
                Recargo_Equivalencia2_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Recargo_Equivalencia3_ { get; set; }
        public decimal Recargo_Equivalencia3
        {
            get { return Recargo_Equivalencia3_; }
            set
            {
                Recargo_Equivalencia3_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Recargo_Equivalencia4_ { get; set; }
        public decimal Recargo_Equivalencia4
        {
            get { return Recargo_Equivalencia4_; }
            set
            {
                Recargo_Equivalencia4_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Recargo_Equivalencia5_ { get; set; }
        public decimal Recargo_Equivalencia5
        {
            get { return Recargo_Equivalencia5_; }
            set
            {
                Recargo_Equivalencia5_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Recargo_Total_ { get; set; }
        public decimal Recargo_Total
        {
            get { return Recargo_Total_; }
            set
            {
                Recargo_Total_ = value;
                OnPropertyChanged();
            }
        }
        public decimal Descuento_Total_ { get; set; }
        public decimal Descuento_Total
        {
            get { return Descuento_Total_; }
            set
            {
                Descuento_Total_ = value;
                OnPropertyChanged();
            }
        }
        public bool Revisar_ { get; set; }
        public bool Revisar
        {
            get { return Revisar_; }
            set
            {
                Revisar_ = value;
                OnPropertyChanged();
            }
        }

        public bool Realizado_ { get; set; }
        public bool Realizado
        {
            get { return Realizado_; }
            set
            {
                Realizado_ = value;
                OnPropertyChanged();
            }
        }

        public string FechaEnvioApp { get; set; }


        //Representa el método que controlará al evento PropertyChanged que se provoqua cuando cambie una propiedad en un componente.
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DatosParte()
        {
        }
    }

}
