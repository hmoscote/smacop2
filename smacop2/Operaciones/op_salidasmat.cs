using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using smacop2.Entity;
using smacop2.Operaciones;
using System.Runtime.InteropServices;

namespace smacop2.Operaciones
{
    class op_salidasmat
    {
        
        public static List<salidasmat> GellAllsalidasmat()
        {
           string diai= op_sql.parametro1(@"SELECT c.`diainicial` FROM logicop.cohorte_tablas c  where tabla='entrada';");
           string numd= op_sql.parametro1(@"SELECT c.`diafinal` FROM logicop.cohorte_tablas c where tabla='entrada';");
           DateTime a, b;
           b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year)).AddDays(1);//resto un día al mes y con esto obtengo el ultimo día

           if (b > DateTime.Now)
           {
               a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month - 1).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
               b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
           }
           else
           {
               a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
               b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month + 1).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
           }
           string sql = @"SELECT * FROM `logicop`.`lstsalidasmat` where anul=0 and fech between @fi and @ff order by 1 ;";
            List<salidasmat> list = new List<salidasmat>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@fi", a);
                cmd.Parameters.AddWithValue("@ff", b);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadsalidasmat(reader));
                }
            }
            return list;
        }

        public static List<salidasmat> GellAllsalidasmatver(DateTime a, DateTime b, string s, string m, int o)
        {
            string sql=null;
           if (o==1)
             sql = @"SELECT * FROM `logicop`.`lstsalidasmat`  where anul=0 and equ=@equ and fech between @fi and @ff order by 1 ;";
           if (o == 2)
               sql = @"SELECT * FROM `logicop`.`lstsalidasmat`  where anul=0 and ccos=@ccos and fech between @fi and @ff order by 1 ;";
           if (o == 3)
               sql = @"SELECT * FROM `logicop`.`lstsalidasmat`  where anul=0 and ccos=@ccos order by 1 ;";
           if (o == 4)
               sql = @"SELECT * FROM `logicop`.`lstsalidasmat`  where anul=1 order by 1 ;";
           if (o == 5)
               sql = @"SELECT * FROM `logicop`.`lstsalidasmat`  where anul=0 and ccos=@ccos and mat=@mat and fech between @fi and @ff order by 1 ;";
           if (o == 6)
               sql = @"SELECT * FROM `logicop`.`lstsalidasmat`  where anul=0 and prov=@ccos and mat=@mat and fech between @fi and @ff order by 1 ;";
            List<salidasmat> list = new List<salidasmat>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                if (o==3)
                    cmd.Parameters.AddWithValue("@ccos", s);
                if (o == 1)
                {
                    cmd.Parameters.AddWithValue("@equ", s);
                    cmd.Parameters.AddWithValue("@fi", a);
                    cmd.Parameters.AddWithValue("@ff", b);
                }
                if (o == 2 || o == 5 || o == 6)
                {
                    cmd.Parameters.AddWithValue("@ccos", s);
                    cmd.Parameters.AddWithValue("@fi", a);
                    cmd.Parameters.AddWithValue("@ff", b);
                }
                if (o == 5 || o == 6)
                    cmd.Parameters.AddWithValue("@mat", m);

                  
               
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadsalidasmat(reader));
                }
            }
            return list;
        }
        public static List<entradasmat> GellAllsalidasmatcons(DateTime a, DateTime b)
        {

            string sql = @"SELECT * FROM `logicop`.`lstsalidasmat` where anul=0 and fech between @fi and @ff order by 1 ;";
            List<entradasmat> list = new List<entradasmat>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@fi", a);
                cmd.Parameters.AddWithValue("@ff", b);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadsalidasmat(reader));
                }
            }
            return list;
        }
        public static List<salidasmat> Gellresumen3()
        {
           string diai= op_sql.parametro1(@"SELECT c.`diainicial` FROM logicop.cohorte_tablas c  where tabla='entrada';");
           string numd= op_sql.parametro1(@"SELECT c.`diafinal` FROM logicop.cohorte_tablas c where tabla='entrada';");
           DateTime a, b;
           b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year)).AddDays(1);//resto un día al mes y con esto obtengo el ultimo día

           if (b > DateTime.Now)
           {
               a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month - 1).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
               b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
           }
           else
           {
               a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
               b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month + 1).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
           }
            string sql = @"     SELECT
                                m.nom as nom,
                                sum(e.cant) as total
                                FROM salidasmat e 
                                join materiales  m 
                                on e.mat=m.cod
                                where anul=0 and fech between @fi and @ff  
                                group by 1  order by 2 desc;";
            List<salidasmat> list = new List<salidasmat>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                   cmd.Parameters.AddWithValue("@fi", a);
                cmd.Parameters.AddWithValue("@ff", b);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadresumen3(reader));
                }
            }
            return list;
        }

        public static List<salidasmat> Gellresumen2()
        {
            string diai = op_sql.parametro1(@"SELECT c.`diainicial` FROM logicop.cohorte_tablas c  where tabla='entrada';");
            string numd = op_sql.parametro1(@"SELECT c.`diafinal` FROM logicop.cohorte_tablas c where tabla='entrada';");
            DateTime a, b;
            b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year)).AddDays(1);//resto un día al mes y con esto obtengo el ultimo día

            if (b > DateTime.Now)
            {
                a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month - 1).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
                b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
            }
            else
            {
                a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
                b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month + 1).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
            }
            string sql = @"   SELECT
                                `salidasmat`.`equ`,
                                count(`salidasmat`.`nsis`) as viajes,
                                sum(`salidasmat`.`tifkm`) as total
                                FROM `logicop`.`salidasmat`
                                where anul=0 and fech between @fi and @ff
                                group by 1  order by 2 desc;";
            List<salidasmat> list = new List<salidasmat>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@fi", a);
                cmd.Parameters.AddWithValue("@ff", b);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadresumen2(reader));
                }
            }
            return list;
        }
        public static List<salidasmat> Gellresumen1()
        {
            string diai = op_sql.parametro1(@"SELECT c.`diainicial` FROM logicop.cohorte_tablas c  where tabla='entrada';");
            string numd = op_sql.parametro1(@"SELECT c.`diafinal` FROM logicop.cohorte_tablas c where tabla='entrada';");
           DateTime a, b;
           b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year)).AddDays(1);//resto un día al mes y con esto obtengo el ultimo día

            if (b > DateTime.Now)
            {
                a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month - 1).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
                b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
            }
            else
            {
                a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
                b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month+1).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
            }
    
            string sql = @" SELECT
                            (CASE ttra WHEN 'CR' THEN 'CREDITO'
                                       WHEN 'CP' THEN 'CONTADO PLANTA' 
                                       WHEN 'CO' THEN 'CONTADO OFICINA' 
                                       ELSE 'N/A' END) AS TRANSACCION,
                            sum(`salidasmat`.`matflet`) as total
                            FROM `logicop`.`salidasmat`
                            where anul=0 and fech between @fi and @ff  
                            group by 1  order by 2 desc;";
            List<salidasmat> list = new List<salidasmat>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@fi", a);
                cmd.Parameters.AddWithValue("@ff", b);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadresumen1(reader));
                }
            }
            return list;
        }


        private static salidasmat Loadresumen2(IDataReader reader)
        {
            decimal s;
            salidasmat item = new salidasmat();
            item.rkm = Convert.ToInt32(reader["viajes"]);
            item.equ = Convert.ToString(reader["equ"]);
            s = reader["total"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total"]);
            item.matfle = s;
            return item;
        }

        private static consulta_03 Loadresumenall(IDataReader reader)
        {
            decimal s,m,n;
            consulta_03 item = new consulta_03();
            item.eq= Convert.ToString(reader["codeq"]);
            s = reader["ent"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["ent"]);
            m = reader["sal"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["sal"]);
           n = reader["total"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total"]);
           item.me = s;
           item.ms = m;
            item.total = n;
          
            return item;
        }


        private static salidasmat Loadresumen1(IDataReader reader)
        {
            decimal s;
            salidasmat item = new salidasmat();
            item.ttra = Convert.ToString(reader["transaccion"]);
           
            s = reader["total"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total"]);
            item.matfle = s;
            return item;
        }
        private static salidasmat Loadresumen3(IDataReader reader)
        {
            decimal s;
            salidasmat item = new salidasmat();
            item.ttra = Convert.ToString(reader["nom"]);
           
            s = reader["total"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total"]);
            item.matfle = s;
            return item;
        }

        private static c_mat_dest Loadcmat(IDataReader reader)
        {

            //decimal s;
            ////cmd.Parameters.Add(new SqlParameter("@Symb", DBNull.Value.ToString()));
            c_mat_dest item = new c_mat_dest();
            item.cod = Convert.ToString(reader["cod"]);
            item.des = Convert.ToString(reader["nom"]);
            item.mat = Convert.ToString(reader["mat"]);
            item.vol = Convert.ToDecimal(reader["vol"]);

            //s = reader["total"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total"]);
            //item.vol = s;
            return item;

        }
        public static List<c_mat_dest> GellAllcmat(DateTime a, DateTime b, string c)
        {

            List<c_mat_dest> list = new List<c_mat_dest>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand command;
                command = new MySqlCommand(@"SELECT
                        salidasmat.ccos AS cod,
                        proyectos.nomproy AS nom,
                        salidasmat.mat as mat,
                        Sum(salidasmat.cant) AS vol
                        FROM
                        salidasmat
                        INNER JOIN proyectos ON proyectos.idproy = salidasmat.ccos
                        where 
                        salidasmat.mat=@c and
                        fech BETWEEN @a and @b and 
                        anul=0
                        group by 1,2,3", conn);
                command.Parameters.AddWithValue("@a", a);
                command.Parameters.AddWithValue("@b", b);
                command.Parameters.AddWithValue("@c", c);


                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadcmat(reader));
                }
            }
            return list;
        }
        public static List<c_mat_dest> GellAllcmat2(DateTime a, DateTime b, string c)
        {

            List<c_mat_dest> list = new List<c_mat_dest>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand command;
                command = new MySqlCommand(@"SELECT
                        salidasmat.ccos AS cod,
                        proyectos.nomproy AS nom,
                        materiales.nom as mat,
                        Sum(salidasmat.cant) AS vol
                        FROM
                        salidasmat
                        INNER JOIN proyectos ON proyectos.idproy = salidasmat.ccos 
                        INNER JOIN materiales on materiales.cod=salidasmat.mat
                        where 
                        salidasmat.ccos=@c and
                        fech BETWEEN @a and @b and 
                        anul=0
                        group by 1,2,3", conn);
                command.Parameters.AddWithValue("@a", a);
                command.Parameters.AddWithValue("@b", b);
                command.Parameters.AddWithValue("@c", c);


                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadcmat(reader));
                }
            }
            return list;
        }
        public static List<c_mat_dest> GellAllcmat1(DateTime a, DateTime b, string c)
        {

            List<c_mat_dest> list = new List<c_mat_dest>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand command;
                command = new MySqlCommand(@"SELECT
                        salidasmat.prov AS cod,
                        proveedores.nom AS nom,
                        materiales.nom as mat,
                        Sum(salidasmat.cant) AS vol
                        FROM
                        salidasmat
                        INNER JOIN proveedores ON proveedores.cod = salidasmat.prov 
                        INNER JOIN materiales on materiales.cod=salidasmat.mat
                        where 
                        salidasmat.prov=@c and
                        fech BETWEEN @a and @b and 
                        anul=0
                        group by 1,2,3", conn);
                command.Parameters.AddWithValue("@a", a);
                command.Parameters.AddWithValue("@b", b);
                command.Parameters.AddWithValue("@c", c);


                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadcmat(reader));
                }
            }
            return list;
        }

        // devuleve un registro en particular
        public static salidasmat GellIdsalidasmat(string c, int i)
        {
            salidasmat list = null;

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand command;
                if (i == 1)
                    command = new MySqlCommand("SELECT * FROM salidasmat WHERE nrec like @cod", conn);
                else if (i == 2)
                    command = new MySqlCommand("SELECT * FROM salidasmat WHERE nrec like @cod", conn);
                else
                    command = new MySqlCommand("SELECT * FROM salidasmat WHERE mat like @cod", conn);
                command.Parameters.AddWithValue("@cod", c);


                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list = Loadsalidasmat(reader);
                }
            }
            return list;
        }
        public static List<salidasmat> GellIdssalidasmat(string c, DateTime a, DateTime b,int i)
        {
            List<salidasmat> list = new List<salidasmat>();

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand command;
                if (i == 1)
                    command = new MySqlCommand("SELECT * FROM lstsalidasmat WHERE anul=0 and ttra = @cod and fech between @a and @b", conn);
                else if (i == 2)
                    command = new MySqlCommand("SELECT * FROM lstsalidasmat WHERE anul=0 and fech between @a and @b AND equ = @cod", conn);
                else
                    command = new MySqlCommand("SELECT * FROM lstsalidasmat WHERE anul=0 and fech between @a and @b AND mat = @cod", conn);
                command.Parameters.AddWithValue("@cod", c);
                command.Parameters.AddWithValue("@a", a);
                command.Parameters.AddWithValue("@b", b);

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadsalidasmat(reader));
                }
            }
            return list;
        }

        // Escribe los valores en la clase salidasmat
        private static salidasmat Loadsalidasmat(IDataReader reader)
        {
            salidasmat item = new salidasmat();
            item.nsis = Convert.ToString(reader["nsis"]);
            item.nrec = Convert.ToString(reader["nrec"]);
            item.fech = Convert.ToDateTime(reader["fech"]);
            item.prov = Convert.ToString(reader["prov"]);//contratista
            item.mat = Convert.ToString(reader["mat"]);
            item.opcd = Convert.ToInt32(reader["occo"]);
            item.ccos = Convert.ToString(reader["ccos"]);
            item.lug = Convert.ToString(reader["lug"]);
            item.tven = Convert.ToString(reader["tpven"]);
            item.equ = Convert.ToString(reader["equ"]);
            item.ope = Convert.ToString(reader["ope"]);
            item.cosm = Convert.ToDecimal(reader["cosm"]);
            item.flem = Convert.ToDecimal(reader["flem"]);
            item.tifkm = Convert.ToDecimal(reader["tifkm"]);
            item.timat = Convert.ToDecimal(reader["timat"]);
            item.anul = Convert.ToInt32(reader["anul"]);
            item.matfle = Convert.ToDecimal(reader["matfle"]);
            item.ttra = Convert.ToString(reader["ttra"]);
            item.cant = Convert.ToDecimal(reader["cant"]);
            item.timatt = Convert.ToDecimal(reader["timatt"]);
            item.hfin = Convert.ToString(reader["hsal"]);
            item.opcdt = Convert.ToInt32(reader["occot"]);
            item.ccost = Convert.ToString(reader["ccost"]);
            item.cont = Convert.ToString(reader["cont"]);
            item.flemt = Convert.ToDecimal(reader["flemt"]);
            item.tifkmt = Convert.ToDecimal(reader["tifkmt"]);
            item.matflet = Convert.ToDecimal(reader["matflet"]);
            item.rkm = Convert.ToDecimal(reader["rkm"]);
            return item;
        }


        // hace las operaciones correspondiente al procedimiento almacenado.
        public static int accion(salidasmat item, int op)
        {
            int rowsAffected = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();

                    MySqlCommand command = new MySqlCommand("SP_salidasmat", con);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramId = new MySqlParameter("msj", MySqlDbType.Int32);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    command.Parameters.AddWithValue("nsis1", item.nsis);
                    command.Parameters.AddWithValue("nrec1", item.nrec);
                    command.Parameters.AddWithValue("fech1", item.fech);
                    command.Parameters.AddWithValue("cont1", item.cont);
                    command.Parameters.AddWithValue("mat1", item.mat);
                    command.Parameters.AddWithValue("occo1", item.opcd);
                    command.Parameters.AddWithValue("ccos1", item.ccos);
                    command.Parameters.AddWithValue("lug1", item.lug);
                    command.Parameters.AddWithValue("tpven1", item.tven);
                    command.Parameters.AddWithValue("cant1", item.cant);
                    command.Parameters.AddWithValue("equ1", item.equ);
                    command.Parameters.AddWithValue("ope1", item.ope);
                    command.Parameters.AddWithValue("rkm1", item.rkm);
                    command.Parameters.AddWithValue("volm1", item.volm);
                    command.Parameters.AddWithValue("cosm1", item.cosm);
                    command.Parameters.AddWithValue("flem1", item.flem);
                    command.Parameters.AddWithValue("tifkm1", item.tifkm);
                    command.Parameters.AddWithValue("timat1", item.timat);
                    command.Parameters.AddWithValue("anul1", item.anul);
                    command.Parameters.AddWithValue("matfle1", item.matfle);
                    command.Parameters.AddWithValue("ttra1", item.ttra);
                    command.Parameters.AddWithValue("ccost1", item.ccost);
                    command.Parameters.AddWithValue("occot1", item.opcdt);
                    command.Parameters.AddWithValue("prov1", item.prov);
                    command.Parameters.AddWithValue("hsal1", item.hfin);
                    command.Parameters.AddWithValue("flemt1", item.flemt);
                    command.Parameters.AddWithValue("tifkmt1", item.tifkmt);
                    command.Parameters.AddWithValue("timatt1", item.timatt);
                    command.Parameters.AddWithValue("usu", item.usu);
                    command.Parameters.AddWithValue("matflet1", item.matflet);
       
                    command.Parameters.AddWithValue("opc", op);

                    command.ExecuteNonQuery();
                    rowsAffected = int.Parse(command.Parameters["msj"].Value.ToString());

                   
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex1)
            {
                MessageBox.Show("Error: " + ex1.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return rowsAffected;
        }


        // devuleve un lista de registros de los salidasmat de forma servidor
        public static List<salidasmat> GellAllsalidasmat2(string Id, int op)
        {
            List<salidasmat> list = new List<salidasmat>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    MySqlCommand command;
                    if (op == 1)
                        command = new MySqlCommand("SELECT * FROM salidasmat WHERE codeq like @cod", conn);
                    else if (op == 2)
                        command = new MySqlCommand("SELECT * FROM salidasmat WHERE nomeq like @cod", conn);
                    else
                        command = new MySqlCommand("SELECT * FROM salidasmat WHERE tipeq = @cod", conn);

                    command.Parameters.AddWithValue("cod", Id);

                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                            list.Add(Loadsalidasmat(reader));
                    }
                    else
                    {
                        MessageBox.Show("No existe información concerniente al parámetro", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return list;

        }
        public static List<salidasmat> GellAllsalidasmat3(DateTime i, DateTime f, int op)
        {
            List<salidasmat> list = new List<salidasmat>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    MySqlCommand command = null; ;
                    if (op == 1)
                        command = new MySqlCommand("SELECT * FROM lstsalidasmat WHERE anul=0 and fech between @ini and @fin ", conn);
                    else if (op == 2)
                        command = new MySqlCommand("SELECT * FROM lstsalidasmat WHERE anul=0 and fech= @ini", conn);


                    command.Parameters.AddWithValue("@ini", i);
                    command.Parameters.AddWithValue("@fin", f);

                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                            list.Add(Loadsalidasmat(reader));
                    }
                    else
                    {
                        MessageBox.Show("No existe información concerniente al parámetro", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }

            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return list;

        }
        public static List<salidasmat> Gellresumenfechadet3(string mat, DateTime i, DateTime f, int opc)
        {

            string sql = null;
            if (opc == 1)
                sql = @"SELECT * FROM `logicop`.`lstsalidasmat`
                                where anul=0 and mat =@mat and
                                fech between @ini and @fin;";
            if (opc == 2)
                sql = @"SELECT * FROM `logicop`.`lstsalidasmat`
                                where mat=@mat and anul=0 and
                                date(fech)= @ini";

            List<salidasmat> list = new List<salidasmat>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@mat", mat);
                cmd.Parameters.AddWithValue("@ini", i);
                cmd.Parameters.AddWithValue("@fin", f);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadsalidasmat(reader));
                }
            }
            return list;
        }

        public static List<salidasmat> Gellresumenfecha3(DateTime i, DateTime f, int opc)
        {
            string sql = null;
            if (opc == 1)
                sql = @"SELECT
                        m.nom as nom,
                        sum(e.cant) as total
                        FROM salidasmat e 
                        join materiales  m 
                        on e.mat=m.cod
                        where anul=0 and
                        fech between @ini and @fin
                        group by 1 order by 2 desc;";
            if (opc == 2)
                sql = @"SELECT
                        m.nom as nom,
                        sum(e.cant) as total
                        FROM salidasmat e 
                        join materiales  m 
                        on e.mat=m.cod
                        where anul=0 and
                        date(fech)= @ini
                        group by 1 order by 2 desc;";
            List<salidasmat> list = new List<salidasmat>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ini", i);
                cmd.Parameters.AddWithValue("@fin", f);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadresumen3(reader));
                }
            }
            return list;
        }
     

        public static string fecha_mysql(string j)
        {
            string a, m, d = null;
            a = j.Substring(6, 4);
            m = j.Substring(3, 2);
            d = j.Substring(0, 2);
            return string.Concat(a, "-", m, "-", d);

        }

        public static List<salidasmat> Gellresumenfechadet2(string veh, DateTime i, DateTime f, int opc)
        {

            string sql = null;
            if (opc == 1)
                sql = @"SELECT * FROM `logicop`.`lstsalidasmat`
                                where anul=0 and equ=@equ and 
                                fech between @ini and @fin;";
            if (opc == 2)
                sql = @"SELECT * FROM `logicop`.`lstsalidasmat`
                                where equ=@equ and anul=0 and
                                date(fech)= @ini;";
            List<salidasmat> list = new List<salidasmat>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@equ", veh);
                cmd.Parameters.AddWithValue("@ini", i);
                cmd.Parameters.AddWithValue("@fin", f);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadsalidasmat(reader));
                }
            }
            return list;
        }

        public static List<salidasmat> Gellresumenfecha2(DateTime i, DateTime f, int opc)
        {

            string sql = null;
            if (opc == 1)
                sql = @"   SELECT
                                `salidasmat`.`equ`,
                                count(`salidasmat`.`nsis`) as viajes,
                                sum(`salidasmat`.`tifkm`) as total
                                FROM `logicop`.`salidasmat`
                                where anul=0 and
                                fech between @ini and @fin
                                group by 1 order by 3 desc;";
            if (opc == 2)
                sql = @"   SELECT
                                `salidasmat`.`equ`,
                                count(`salidasmat`.`nsis`) as viajes,
                                sum(`salidasmat`.`tifkm`) as total
                                FROM `logicop`.`salidasmat`
                                where anul=0 and
                                date(fech)= @ini
                                group by 1 order by 3 desc;";
            List<salidasmat> list = new List<salidasmat>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ini", i);
                cmd.Parameters.AddWithValue("@fin", f);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadresumen2(reader));
                }
            }
            return list;
        }


        public static List<salidasmat> Gellresumenfechadet1(string ttra, DateTime i, DateTime f, int opc)
        {

            string sql = null;
            if (opc == 1)
                sql = @"SELECT * FROM `logicop`.`lstsalidasmat`
                                 where anul=0 and ttra =@ttra and 
                                 fech between @ini and @fin;";
            if (opc == 2)
                sql = @"SELECT * FROM `logicop`.`lstsalidasmat`
                                 where ttra=@ttra and anul=0 and
                                 date(fech)= @ini";

            List<salidasmat> list = new List<salidasmat>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ttra", ttra);
                cmd.Parameters.AddWithValue("@ini", i);
                cmd.Parameters.AddWithValue("@fin", f);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadsalidasmat(reader));
                }
            }
            return list;
        }

        public static List<salidasmat> Gellresumenfecha1(DateTime i, DateTime f, int opc)
        {
            string sql = null;
            if (opc == 1)
                sql = @" SELECT
                            (CASE ttra WHEN 'CR' THEN 'CREDITO'
                                      WHEN 'CP' THEN 'CONTADO PLANTA' 
                                      WHEN 'CO' THEN 'CONTADO OFICINA' 
                                      ELSE 'N/A' END) AS TRANSACCION,
                            sum(`salidasmat`.`matflet`) as total
                            FROM `logicop`.`salidasmat`
                            where anul=0 and
                            fech between @ini and @fin
                            group by 1 order by 2 desc;";
            if (opc == 2)
                sql = @" SELECT
                            (CASE ttra WHEN 'CR' THEN 'CREDITO'
                                      WHEN 'CP' THEN 'CONTADO PLANTA' 
                                      WHEN 'CO' THEN 'CONTADO OFICINA' 
                                      ELSE 'N/A' END) AS TRANSACCION,
                            sum(`salidasmat`.`matflet`) as total
                            FROM `logicop`.`salidasmat`
                            where anul=0 and
                            date(fech) = @ini
                            group by 1 order by 2 desc;";
            List<salidasmat> list = new List<salidasmat>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ini", i);
                cmd.Parameters.AddWithValue("@fin", f);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadresumen1(reader));
                }
            }
            return list;
        }


        public static List<salidasmat> GellAllsalidasmat4(DateTime i, DateTime f, string o ,int op)
        {
            List<salidasmat> list = new List<salidasmat>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    MySqlCommand command = null; ;
                    if (op == 1)
                        command = new MySqlCommand("SELECT * FROM lstsalidasmat WHERE ccos=@cob and anul=0 and fech between @ini and @fin ", conn);
                    else if (op == 2)
                        command = new MySqlCommand("SELECT * FROM lstsalidasmat WHERE cont=@cob and anul=0 and fech between @ini and @fin ", conn);
                    else if (op == 3)
                        command = new MySqlCommand("SELECT * FROM lstsalidasmat WHERE prov=@cob and anul=0 and fech between @ini and @fin ", conn);


                    command.Parameters.AddWithValue("@ini", i);
                    command.Parameters.AddWithValue("@fin", f);
                    command.Parameters.AddWithValue("@cob",o);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                            list.Add(Loadsalidasmat(reader));
                    }
                    else
                    {
                        MessageBox.Show("No existe información concerniente al parámetro", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }

            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return list;

        }
        public enum ac
        {
            externo,
            interno
        }

        public static List<consulta_03> Gellresumenall(ac o)
        {
            string sql = null;
            if (o == ac.interno)

                sql = @"SELECT * from tmp_liq_eq ORDER BY 1";
            else
                sql = @"SELECT * from tmp_liq_eq_ext ORDER BY 1";
            List<consulta_03> list = new List<consulta_03>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadresumenall(reader));
                }
            }
            return list;
        }
    }
}