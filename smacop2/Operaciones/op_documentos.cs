using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smacop2.Entity;

namespace smacop2.Operaciones
{
       public static class op_documentos
    {
      
            public static List<documento> documentos()
            {
                List<documento> lista = new List<documento>()
                {
                    new documento("CC", "CEDULA DE CIUDADANIA"),
                    new documento("RU", "RUT"),
                    new documento("NI", "NIT"),
                    new documento("LM", "LIBRETA MILITAR"),
                    new documento("PS", "PASAPORTE")
                };

                return lista;
            }

            public static List<documento> transaccion()
            {
                List<documento> lista = new List<documento>()
                {
                    new documento("CO", "CONTADO OFICINA"),
                    new documento("CP", "CONTADO PLANTA"),
                    new documento("CR", "CREDITO"),
                    
                };

                return lista;
            }
            public static List<documento> documentos1()
            {
                List<documento> lista = new List<documento>()
                {
                    new documento("C", "CONSORCIO"),
                    new documento("I", "INTERNO"),
                    new documento("E", "EXTERNO"),
                };

                return lista;
            }

            public static List<documento> linea()
            {
                List<documento> lista = new List<documento>()
                {
                    new documento("1", "AMARILLA"),
                    new documento("2", "BLANCA"),
                     new documento("3", "LIVIANOS"),
                };

                return lista;
            }
            public static List<documento> tipo_produccion()
            {
                List<documento> lista = new List<documento>()
                {
                    new documento("EXT", "EXTERNO"),
                    new documento("CMP", "COMPUESTO"),
                    new documento("PRO", "PRODUCIDO"),
                };

                return lista;
            }
            public static List<documento> servicios()
            {
                List<documento> lista = new List<documento>()
                {
                    new documento("01", "CANTERA"),
                    new documento("02", "COMBUSTIBLE"),
                    new documento("03", "TALLER DE EQUIPOS"),
                    new documento("04", "LLANTERIA"),
                    new documento("05", "ALQUILER EQUIPOS"),
                    new documento("06", "VENTA DE EQUIPOS"),
                    new documento("07", "VENTA DE RESPUESTOS"),
                    new documento("08", "VENTA DE CRUDO"),
                    new documento("09", "REPARACION DE EQUIPOS"),
                    new documento("10", "TRANSPORTE"),
                   
                };

                return lista;
            }
            public static List<documento> ventas(string opc)
            {
                List<documento> lista = null;
                if (opc == "CO")
                {
                    lista = new List<documento>()
                    {
                    new documento("01", "VENTA EN OFICINA CON TRANSPORTE"),
                    new documento("02", "VENTA EN OFICINA SIN TRANSPORTE"),
            
                   
                    };
                    return lista;
                }
                if (opc == "CP")
                {
                    lista = new List<documento>()
                {
                   
                    new documento("03", "VENTA EN PLANTA CON TRANSPORTE"),
                    new documento("04", "VENTA EN PLANTA SIN TRANSPORTE"),
                   
                };
                    return lista;
                }
                if (opc == "CR")
                {
                    lista = new List<documento>()
                {
                    new documento("01", "VENTA EN OFICINA CON TRANSPORTE"),
                    new documento("02", "VENTA EN OFICINA SIN TRANSPORTE"),
                    new documento("03", "VENTA EN PLANTA CON TRANSPORTE"),
                    new documento("04", "VENTA EN PLANTA SIN TRANSPORTE"),
                   
                };
                  
                }
                return lista;
            }

         



               
            
    }
}
