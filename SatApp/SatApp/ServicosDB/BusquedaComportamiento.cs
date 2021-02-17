using System;
using Xamarin.Forms;

namespace SatApp.ServiciosDB
{
    class BusquedaComportamiento : Behavior<SearchBar>
    {
        //Esta clase nos va a permitir buscar Articulos para asignar lineas al parte para enviarlos a la Central.
        protected override void OnAttachedTo(SearchBar bindable)
        {
            try
            {
                base.OnAttachedTo(bindable);
                bindable.TextChanged += Bindable_TextChanged;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        protected override void OnDetachingFrom(SearchBar bindable)
        {
            try
            {
                base.OnDetachingFrom(bindable);
                bindable.TextChanged -= Bindable_TextChanged;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ((SearchBar)sender).SearchCommand?.Execute(e.NewTextValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
