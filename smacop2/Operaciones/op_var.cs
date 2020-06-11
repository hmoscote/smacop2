using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace smacop2.Operaciones
{
    class op_var
    {

        public static string cod;
        public static string nom;
        public static Int32 opc;

        public static bool boton1;
        public static bool boton2;
        public static bool boton3;
        public static bool boton4;
        public static void botones(bool a, Button nuevo)
        {
            if (a == true)
                nuevo.SendToBack();
            else
                nuevo.BringToFront();
        }

        public static DateTime cohorte(int dia, int mes, int ano)
        {
            string fecha;
            string d = dia.ToString("00");
            string m = mes.ToString("00");
            string a = ano.ToString("0000");
            fecha = string.Concat(d, "/", m, "/", a);
            return DateTime.Parse(fecha);
        }

        ///reporte entre fecha
        public static DateTime a;
        public static DateTime b;
        public static string c;

        /// <summary>
        /// reporte de viajes
        /// </summary>
        public static int aa;
        public static int yy;
        //public static int mm;
        public static bool forma;
        public static string ope;
        //public static DateTime at;
        //public static DateTime bt;
      

        //public static bool opc1;
        // cantera
        public static string tmp_cantera;

        public static string usu;

        //public static string convertir_hora(int a)
        //{

        //    return 
        //}
        //public static Int32 opc1;
        public static int mes(int q)
        {
            switch (q)
            {

                case 0:
                    q = 1;
                    break;
                case 1:
                    q = 2;
                    break;
                case 2:
                    q = 3;
                    break;
                case 3:
                    q = 4;
                    break;
                case 4:
                    q = 5;
                    break;
                case 5:
                    q = 6;
                    break;
                case 6:
                    q = 7;
                    break;
                case 7:
                    q = 8;
                    break;
                case 8:
                    q = 9;
                    break;
                case 9:
                    q = 10;
                    break;
                case 10:
                    q = 11;
                    break;
                case 11:
                    q = 12;
                    break;




            }
            if (q < 0) q = 12;

            return q + 1;
        }
    }
}
