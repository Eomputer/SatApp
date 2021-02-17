using SatApp.Modelos;
using SatApp.ServiciosDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SatApp.Repository
{
    public class Funciones
    {
        public Type[] ObtenerModelos()
        {
            try
            {
                //Obtengo la Lista completa de las Entidades. Así aprovechamos trabajar con la Reflexión, para crear dinámicamente una instancia de un tipo.
                Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "SatApp.Modelos");
                return typelist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Retorna el nombre del objeto dentro del NameSpace donde se encuentran los Modelos.
        private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            try
            {
                return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CalcularParte(int numeroParte)
        {
            RepositorySatApp database = new RepositorySatApp();

            SAT parte = new SAT();
            var lineas = database.GetAllLineasParte(Variables.DatosParte.N_Parte.ToString());
            decimal descuento = 0;

            parte.Base1 = 0;
            parte.Base2 = 0;
            parte.Base3 = 0;
            parte.Base4 = 0;
            parte.Base5 = 0;

            parte.TantoIva1 = 0;
            parte.TantoIva2 = 0;
            parte.TantoIva3 = 0;
            parte.TantoIva4 = 0;
            parte.TantoIva5 = 0;

            parte.Iva1 = 0;
            parte.Iva2 = 0;
            parte.Iva3 = 0;
            parte.Iva4 = 0;
            parte.Iva5 = 0;

            parte.Tanto_Equivalencia1 = 0;
            parte.Tanto_Equivalencia2 = 0;
            parte.Tanto_Equivalencia3 = 0;
            parte.Tanto_Equivalencia4 = 0;
            parte.Tanto_Equivalencia5 = 0;

            parte.Recargo_Equivalencia1 = 0;
            parte.Recargo_Equivalencia2 = 0;
            parte.Recargo_Equivalencia3 = 0;
            parte.Recargo_Equivalencia4 = 0;
            parte.Recargo_Equivalencia5 = 0;


            foreach (var item in lineas)  //foreach (var item in Variables.lineasParte)
            {
                if (item.DTO != 0)
                {
                    //Jesus:Indica que quitemos el descuento ya que no hace nada.26/05/2020
                    //descuento = Math.Round(descuento + (item.Precio * item.DTO / 100), 2);
                    //En principio le pasamos 0 a la variable.
                    descuento = 0;
                    item.Importe = Math.Round(item.Importe, 2); 
                }

                if (parte.TantoIva1 == item.Iva)
                    parte.Base1 = Math.Round(parte.Base1 + item.Importe, 2);
                else if (parte.TantoIva2 == item.Iva)
                    parte.Base2 = Math.Round(parte.Base2 + item.Importe, 2);
                else if (parte.TantoIva3 == item.Iva)
                    parte.Base3 = Math.Round(parte.Base3 + item.Importe, 2);
                else if (parte.TantoIva4 == item.Iva)
                    parte.Base4 = Math.Round(parte.Base4 + item.Importe, 2);
                else if (parte.TantoIva5 == item.Iva)
                    parte.Base5 = Math.Round(parte.Base5 + item.Importe, 2);
                else
                {

                    if ((parte.TantoIva1 == 0) && (parte.Base1 == 0))
                    {
                        parte.TantoIva1 = item.Iva;
                        parte.Base1 = Math.Round(parte.Base1 + item.Importe, 2);
                    }
                    else if ((parte.TantoIva2 == 0) && (parte.Base2 == 0))
                    {
                        parte.TantoIva2 = item.Iva;
                        parte.Base2 = Math.Round(parte.Base2 + item.Importe, 2);
                    }
                    else if ((parte.TantoIva3 == 0) && (parte.Base3 == 0))
                    {
                        parte.TantoIva3 = item.Iva;
                        parte.Base3 = Math.Round(parte.Base3 + item.Importe, 2);
                    }
                    else if ((parte.TantoIva4 == 0) && (parte.Base4 == 0))
                    {
                        parte.TantoIva4 = item.Iva;
                        parte.Base4 = Math.Round(parte.Base4 + item.Importe, 2);
                    }
                    else if ((parte.TantoIva5 == 0) && (parte.Base5 == 0))
                    {
                        parte.TantoIva5 = item.Iva;
                        parte.Base5 = Math.Round(parte.Base5 + item.Importe, 2);
                    }
                }
            }

            parte.Iva1 = Math.Round(parte.Base1 * parte.TantoIva1 / 100, 2);
            parte.Iva2 = Math.Round(parte.Base2 * parte.TantoIva2 / 100, 2);
            parte.Iva3 = Math.Round(parte.Base3 * parte.TantoIva3 / 100, 2);
            parte.Iva4 = Math.Round(parte.Base4 * parte.TantoIva4 / 100, 2);
            parte.Iva5 = Math.Round(parte.Base5 * parte.TantoIva5 / 100, 2);

            if (Variables.DatosParte.TieneRecargoEquivalencia == true)
            {
                if (parte.TantoIva1 != 0)
                {
                    parte.Tanto_Equivalencia1 = ObtenerRecargoEquivalencia(parte.TantoIva1);
                    parte.Recargo_Equivalencia1 = Math.Round(parte.Base1 * parte.Tanto_Equivalencia1 / 100, 2);
                }

                if (parte.TantoIva2 != 0)
                {
                    parte.Tanto_Equivalencia2 = ObtenerRecargoEquivalencia(parte.TantoIva2);
                    parte.Recargo_Equivalencia2 = Math.Round(parte.Base2 * parte.Tanto_Equivalencia2 / 100, 2);
                }

                if (parte.TantoIva3 != 0)
                {
                    parte.Tanto_Equivalencia3 = ObtenerRecargoEquivalencia(parte.TantoIva3);
                    parte.Recargo_Equivalencia3 = Math.Round(parte.Base3 * parte.Tanto_Equivalencia3 / 100, 2);
                }

                if (parte.TantoIva4 != 0)
                {
                    parte.Tanto_Equivalencia4 = ObtenerRecargoEquivalencia(parte.TantoIva4);
                    parte.Recargo_Equivalencia4 = Math.Round(parte.Base4 * parte.Tanto_Equivalencia4 / 100, 2);
                }

                if (parte.TantoIva5 != 0)
                {
                    parte.Tanto_Equivalencia5 = ObtenerRecargoEquivalencia(parte.TantoIva5);
                    parte.Recargo_Equivalencia5 = Math.Round(parte.Base5 * parte.Tanto_Equivalencia5 / 100, 2);
                }

            }

            parte.Base_Total = parte.Base1 + parte.Base2 + parte.Base3 + parte.Base4 + parte.Base5;
            parte.Iva_Total = parte.Iva1 + parte.Iva2 + parte.Iva3 + parte.Iva4 + parte.Iva5;
            parte.Recargo_Total = parte.Recargo_Equivalencia1 + parte.Recargo_Equivalencia2 + parte.Recargo_Equivalencia3 + parte.Recargo_Equivalencia4 + parte.Recargo_Equivalencia5;

            parte.Total = parte.Base_Total + parte.Iva_Total + parte.Recargo_Total;

            SAT parteFinal = database.Get<SAT>(numeroParte);

            parteFinal.Base1 = parte.Base1;
            parteFinal.Base2 = parte.Base2;
            parteFinal.Base3 = parte.Base3;
            parteFinal.Base4 = parte.Base4;
            parteFinal.Base5 = parte.Base5;

            parteFinal.TantoIva1 = parte.TantoIva1;
            parteFinal.TantoIva2 = parte.TantoIva2;
            parteFinal.TantoIva3 = parte.TantoIva3;
            parteFinal.TantoIva4 = parte.TantoIva4;
            parteFinal.TantoIva5 = parte.TantoIva5;

            parteFinal.Iva1 = parte.Iva1;
            parteFinal.Iva2 = parte.Iva2;
            parteFinal.Iva3 = parte.Iva3;
            parteFinal.Iva4 = parte.Iva4;
            parteFinal.Iva5 = parte.Iva5;

            parteFinal.Tanto_Equivalencia1 = parte.Tanto_Equivalencia1;
            parteFinal.Tanto_Equivalencia2 = parte.Tanto_Equivalencia2;
            parteFinal.Tanto_Equivalencia3 = parte.Tanto_Equivalencia3;
            parteFinal.Tanto_Equivalencia4 = parte.Tanto_Equivalencia4;
            parteFinal.Tanto_Equivalencia5 = parte.Tanto_Equivalencia5;

            parteFinal.Recargo_Equivalencia1 = parte.Recargo_Equivalencia1;
            parteFinal.Recargo_Equivalencia2 = parte.Recargo_Equivalencia2;
            parteFinal.Recargo_Equivalencia3 = parte.Recargo_Equivalencia3;
            parteFinal.Recargo_Equivalencia4 = parte.Recargo_Equivalencia4;
            parteFinal.Recargo_Equivalencia5 = parte.Recargo_Equivalencia5;

            parteFinal.Iva_Total = parte.Iva_Total;
            parteFinal.Base_Total = parte.Base_Total;
            parteFinal.Total = parte.Total;

            Variables.DatosParte.Base1 = parteFinal.Base1;
            Variables.DatosParte.Base2 = parteFinal.Base2;
            Variables.DatosParte.Base3 = parteFinal.Base3;
            Variables.DatosParte.Base4 = parteFinal.Base4;
            Variables.DatosParte.Base5 = parteFinal.Base5;

            Variables.DatosParte.TantoIva1 = parteFinal.TantoIva1;
            Variables.DatosParte.TantoIva2 = parteFinal.TantoIva2;
            Variables.DatosParte.TantoIva3 = parteFinal.TantoIva3;
            Variables.DatosParte.TantoIva4 = parteFinal.TantoIva4;
            Variables.DatosParte.TantoIva5 = parteFinal.TantoIva5;

            Variables.DatosParte.Iva1 = parteFinal.Iva1;
            Variables.DatosParte.Iva2 = parteFinal.Iva2;
            Variables.DatosParte.Iva3 = parteFinal.Iva3;
            Variables.DatosParte.Iva4 = parteFinal.Iva4;
            Variables.DatosParte.Iva5 = parteFinal.Iva5;

            Variables.DatosParte.Tanto_Equivalencia1 = parteFinal.Tanto_Equivalencia1;
            Variables.DatosParte.Tanto_Equivalencia2 = parteFinal.Tanto_Equivalencia2;
            Variables.DatosParte.Tanto_Equivalencia3 = parteFinal.Tanto_Equivalencia3;
            Variables.DatosParte.Tanto_Equivalencia4 = parteFinal.Tanto_Equivalencia4;
            Variables.DatosParte.Tanto_Equivalencia5 = parteFinal.Tanto_Equivalencia5;

            Variables.DatosParte.Recargo_Equivalencia1 = parteFinal.Recargo_Equivalencia1;
            Variables.DatosParte.Recargo_Equivalencia2 = parteFinal.Recargo_Equivalencia2;
            Variables.DatosParte.Recargo_Equivalencia3 = parteFinal.Recargo_Equivalencia3;
            Variables.DatosParte.Recargo_Equivalencia4 = parteFinal.Recargo_Equivalencia4;
            Variables.DatosParte.Recargo_Equivalencia5 = parteFinal.Recargo_Equivalencia5;

            Variables.DatosParte.Iva_Total = parteFinal.Iva_Total;
            Variables.DatosParte.Total = parteFinal.Total;
            Variables.DatosParte.Base_Total = parteFinal.Base_Total;
            Variables.DatosParte.Descuento_Total = descuento;
        }

        public decimal ObtenerRecargoEquivalencia(decimal tipoDeIVA)
        {
            RepositorySatApp database = new RepositorySatApp();
            List<Iva> ivas = database.GetAll<Iva>();

            decimal tipoRecargo = 0;
            foreach (var item in ivas)
            {
                if (item.Porcentaje == tipoDeIVA)
                {
                    tipoRecargo = item.Recargo;
                    break;
                }
            }
            return tipoRecargo;
        }


    }
}
