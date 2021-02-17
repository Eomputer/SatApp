using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SatApp.Base
{
    //Lo que va a ser este método es obtener el nombre de la propiedad desde el cual llamemos 
    //este métdo para no tener  que estarlo mandando desde cada una de estas propiedades 
    //PropertyChangedEventArgs: Este evento recibe lo que es el nombre de la propiedad desde 
    //la cual se va a notificar a la interfaz grafica que ha ocurrido el cambio.
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
