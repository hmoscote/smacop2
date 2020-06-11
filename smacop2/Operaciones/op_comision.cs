using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using smacop2.Entity;
using System.Configuration;

namespace smacop2.Operaciones
{
    class op_comision
    {
        public string acc { get; set; }
        public string ope { get; set; }
        public string opn{ get; set; }
        public string vjs { get; set; }
        public decimal rec { get; set; }
        public decimal rec_km_f { get; set; }
        public decimal com { get; set; }

//        public static List<entradasmat> Gellresumen2()
//        {
////            string diai = op_sql.parametro1(@"SELECT c.`diainicial` FROM logicop.cohorte_tablas c  where tabla='entrada';");
//            string numd = op_sql.parametro1(@"SELECT c.`diafinal` FROM logicop.cohorte_tablas c where tabla='entrada';");
//            DateTime a, b;
//            b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día

//            if (b > DateTime.Now)
//            {
//                a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month - 1).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
//                b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
//            }
//            else
//            {
//                a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
//                b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month + 1).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
//            }
//            string sql = @"   SELECT
//                                `entradasmat`.`equ`,
//                                count(`entradasmat`.`nsis`) as viajes,
//                                sum(`entradasmat`.`matfle`) as total
//                                FROM `logicop`.`entradasmat`
//                                where anul=0
//                                 and fech between @fi and @ff  group by 1;";
//            List<entradasmat> list = new List<entradasmat>();
//            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
//            {
//                conn.Open();
//                MySqlCommand cmd = new MySqlCommand(sql, conn);
//                cmd.Parameters.AddWithValue("@fi", a);
//                cmd.Parameters.AddWithValue("@ff", b);
//                MySqlDataReader reader = cmd.ExecuteReader();
//                if (reader.HasRows == true)
//                {
//                    while (reader.Read())
//                        //list.Add(Loadresumen2(reader));
//                }
//            }
//            return list;
        //}
        public enum opciones
        {
            parametro_fecha,
            parametro_fecha_operador,
            parametro_acceso
        }
        public enum vista
        {
           maestro,
           detalle
        }
        public static List<op_comision> GellAllCargos(vista v ,opciones o, string acc)
        {
            string sql = null;
   

            if (o == opciones.parametro_fecha)
            {
                if (v == vista.maestro)
                    sql = @"SELECT 'SALIDAS' AS ACCESO,ope,  nomempl, count(nrec) as viajes,sum(rkm) as recorrido, sum(tifkm) as rec_km_fle, sum(tifkm*0.07) as comision FROM `logicop`.`salidasmat` join empleados on codempl=ope  where fech between @fi and @ff  group by 1,2  
                           union SELECT 'ENTRADAS' AS ACCESO,ope,  nomempl, count(nrec) as viajes,sum(rkm) as recorrido, sum(tifkm) as rec_km_fle, sum(tifkm*0.07) as comision FROM `logicop`.`entradasmat` join empleados on codempl=ope where fech between @fi and @ff group by 1,2 ;";
                if (v == vista.detalle)
                    sql = @"SELECT 'SALIDAS' AS ACCESO,ope,  nomempl, nrec as recibo,rkm as recorrido, tifkm as rec_km_fle, tifkm*0.07 as comision FROM `logicop`.`salidasmat` join empleados on codempl=ope where fech between @fi and @ff 
                           union SELECT 'ENTRADAS' AS ACCESO,ope,  nomempl, nrec as viajes,rkm as recorrido, tifkm as rec_km_fle, tifkm*0.07 as comision FROM `logicop`.`entradasmat` join empleados on codempl=ope where fech between @fi and @ff";
            }

            if (o == opciones.parametro_fecha_operador)
            {
                if (v == vista.maestro)
                    sql = @"SELECT 'SALIDAS' AS ACCESO, ope,  nomempl, count(nrec) as viajes,sum(rkm) as recorrido, sum(tifkm) as rec_km_fle, sum(tifkm*0.07) as comision FROM `logicop`.`salidasmat` join empleados on codempl=ope  where ope=@op  and fech between @fi and @ff  group by 1,2 
                           union SELECT 'ENTRADAS' AS ACCESO,ope,  nomempl, count(nrec) as viajes,sum(rkm) as recorrido, sum(tifkm) as rec_km_fle, sum(tifkm*0.07) as comision FROM `logicop`.`entradasmat` join empleados on codempl=ope where ope=@op and fech between @fi and @ff group by 1,2;";
                if (v == vista.detalle)
                    sql = @"SELECT 'SALIDAS' AS ACCESO, ope,  nomempl, nrec as recibo,rkm as recorrido, tifkm as rec_km_fle, tifkm*0.07 as comision FROM `logicop`.`salidasmat` join empleados on codempl=ope where ope=@op  and fech between @fi and @ff 
                           union SELECT 'ENTRADAS' AS ACCESO, ope,  nomempl, nrec as viajes,rkm as recorrido, tifkm as rec_km_fle, tifkm*0.07 as comision FROM `logicop`.`entradasmat` join empleados on codempl=ope where ope=@op  and fech between @fi and @ff";
            }

            if (o == opciones.parametro_acceso)
            {
                string tabla = null;

                if (acc == "ENTRADAS")
                    tabla = "`logicop`.`entradasmat`";
                else
                    tabla = "`logicop`.`salidasmat`";

                    sql = @"SELECT '" + acc + "' AS ACCESO, ope,  nomempl, nrec as viajes,rkm as recorrido, tifkm as rec_km_fle, tifkm*0.07 as comision FROM " + tabla + " join empleados on codempl=ope  where ope=@op  and fech between @fi and @ff order by 2,3";
            }


            List<op_comision> list = new List<op_comision>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@fi", op_var.a);
                cmd.Parameters.AddWithValue("@ff", op_var.b);
                cmd.Parameters.AddWithValue("@op", op_var.ope);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(LoadCargos(reader, v));
                }
            }
            return list;
        }


       //public string diai = op_sql.parametro1(@"SELECT c.`diainicial` FROM logicop.cohorte_tablas c  where tabla='entrada';");
       //public string numd = op_sql.parametro1(@"SELECT c.`diafinal` FROM logicop.cohorte_tablas c where tabla='entrada';");

//        public static List<op_comision> GellAllCargos1(int opc, opciones o, string ope, int mes, int ano,bool coh, string acc)
//        {
//            string diai = op_sql.parametro1(@"SELECT c.`diainicial` FROM logicop.cohorte_tablas c  where tabla='entrada';");
//            string numd = op_sql.parametro1(@"SELECT c.`diafinal` FROM logicop.cohorte_tablas c where tabla='entrada';");

//            string sql = null;
//            DateTime inicio, fin;



//            if (coh == false)
//            {
//                inicio = op_var.cohorte(1, mes + 1, ano);
//                fin = op_var.cohorte(1, mes + 2, ano).AddDays(-1);
//            }
//            else
//            {
//                inicio = op_var.cohorte(int.Parse(diai), mes , ano);
//                fin = inicio.AddDays(int.Parse(numd));
//            }
//            //    inicio = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(mes + 1).PadLeft(2, '0'), "/", ano));// pongo el 1 porque siempre es el primer día obvio
//            //    fin = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(mes + 2).PadLeft(2, '0'), "/", ano)).AddDays(-1); //resto un día al mes y con esto obtengo el ultimo día
//            //}
//            //else
//            //{

//            //    inicio = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(op_var.mes(mes) - 1).PadLeft(2, '0'), "/", ano));// pongo el 1 porque siempre es el primer día obvio
//            //    fin = inicio.AddDays(int.Parse(numd)); //resto un día al mes y con esto obtengo el ultimo dí            fin = inicio.AddDays(int.Parse(numd) - 1); //resto un día al mes y con esto obtengo el ultimo dí         
//            //}

//            if (o == opciones.parametro_fecha)
//            {
              
//                if (opc == 1)
//                    //maestros
//                    sql = @"SELECT 'SALIDAS' AS ACCESO,ope,  nomempl, count(nrec) as viajes,sum(rkm) as recorrido, sum(tifkm) as rec_km_fle, sum(tifkm*0.07) as comision FROM `logicop`.`salidasmat` join empleados on codempl=ope  where fech between @fi and @ff  group by 1,2 
//                           union SELECT 'ENTRADAS' AS ACCESO,ope,  nomempl, count(nrec) as viajes,sum(rkm) as recorrido, sum(tifkm) as rec_km_fle, sum(tifkm*0.07) as comision FROM `logicop`.`entradasmat` join empleados on codempl=ope where fech between @fi and @ff group by 1,2;";
//                else
//                    //detalles
//                    sql = @"SELECT 'SALIDAS' AS ACCESO,ope,  nomempl, nrec as recibo,rkm as recorrido, tifkm as rec_km_fle, tifkm*0.07 as comision FROM `logicop`.`salidasmat` join empleados on codempl=ope where fech between @fi and @ff 
//                           union SELECT 'ENTRADAS' AS ACCESO,ope,  nomempl, nrec as recibo,rkm as recorrido, tifkm as rec_km_fle, tifkm*0.07 as comision FROM `logicop`.`entradasmat` join empleados on codempl=ope where fech between @fi and @ff";
          
//            }

//            if (o == opciones.parametro_fecha_operador)
//            {
//                if (opc == 1)
//                    //maestros
//                    sql = @"SELECT 'SALIDAS' AS ACCESO, ope,  nomempl, count(nrec) as viajes,sum(rkm) as recorrido, sum(tifkm) as rec_km_fle, sum(tifkm*0.07) as comision FROM `logicop`.`salidasmat` join empleados on codempl=ope  where ope=@op  and fech between @fi and @ff  group by 1,2 
//                           union SELECT 'ENTRADAS' AS ACCESO,ope,  nomempl, count(nrec) as viajes,sum(rkm) as recorrido, sum(tifkm) as rec_km_fle, sum(tifkm*0.07) as comision FROM `logicop`.`entradasmat` join empleados on codempl=ope where ope=@op and fech between @fi and @ff group by 1,2;";
//                else
//                    //detalles
//                    sql = @"SELECT 'SALIDAS' AS ACCESO, ope,  nomempl, nrec as recibo,rkm as recorrido, tifkm as rec_km_fle, tifkm*0.07 as comision FROM `logicop`.`salidasmat` join empleados on codempl=ope where ope=@op  and fech between @fi and @ff 
//                           union SELECT 'ENTRADAS' AS ACCESO, ope,  nomempl, nrec as recibo,rkm as recorrido, tifkm as rec_km_fle, tifkm*0.07 as comision FROM `logicop`.`entradasmat` join empleados on codempl=ope where ope=@op  and fech between @fi and @ff";
//            }

//            if (o == opciones.parametro_acceso)
//            {
//                string tabla = null;

//                if (acc == "ENTRADAS")
//                    tabla = "`logicop`.`entradasmat`";
//                else
//                    tabla = "`logicop`.`salidasmat`";

//                if (opc == 1)
//                    sql = @"SELECT '" + acc + "' AS ACCESO, ope,  nomempl, nrec as viajes,rkm as recorrido, tifkm as rec_km_fle, tifkm*0.07 as comision FROM " + tabla + " join empleados on codempl=ope  where ope=@op  and fech between @fi and @ff";
//            }

//            List<op_comision> list = new List<op_comision>();
//            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
//            {
//                conn.Open();
//                MySqlCommand cmd = new MySqlCommand(sql, conn);
//                cmd.Parameters.AddWithValue("@fi", inicio);
//                cmd.Parameters.AddWithValue("@ff", fin);
//                cmd.Parameters.AddWithValue("@op", ope);

//                MySqlDataReader reader = cmd.ExecuteReader();
//                if (reader.HasRows == true)
//                {
//                    while (reader.Read())
//                        list.Add(LoadCargos(reader, v));
//                }
//            }
//            return list;
//        }
        private static op_comision LoadCargos(IDataReader reader, vista v)
        {
            op_comision item = new op_comision();
            item.acc = Convert.ToString(reader["ACCESO"]);
            item.ope = Convert.ToString(reader["ope"]);
            item.opn = Convert.ToString(reader["nomempl"]);
            item.vjs = Convert.ToString(reader["viajes"]);
            item.rec = Convert.ToDecimal(reader["recorrido"]);
            item.rec_km_f = Convert.ToDecimal(reader["rec_km_fle"]);
            item.com = Convert.ToDecimal(reader["comision"]);
            return item;
        }

        //private static op_comision LoadCargos1(IDataReader reader, vista v)
        //{
        //    op_comision item = new op_comision();
        //    item.acc = Convert.ToString(reader["ACCESO"]);
        //    item.ope = Convert.ToString(reader["ope"]);
        //    item.opn = Convert.ToString(reader["nomempl"]);
        //    item.vjs = Convert.TO(reader["viajes"]);
        //    item.rec = Convert.ToDecimal(reader["recorrido"]);
        //    item.rec_km_f = Convert.ToDecimal(reader["rec_km_fle"]);
        //    item.com = Convert.ToDecimal(reader["comision"]);
        //    return item;
        //}
    }
}
