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
     
    class op_entradasmat
    {


        public static List<entradasmat> GellAllentradasmat()
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

            string sql = @"SELECT * FROM `logicop`.`lstentradasmat` where anul=0 and fech between @fi and @ff order by 1 ;";

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
                        list.Add(Loadentradasmat(reader));
                }
            }
            return list;
        }

        public static List<entradasmat> GellAllentradasmatver(DateTime a, DateTime b, string s,string c, int o)
        {

            string sql =null;

            if (o == 1)
                sql = @"SELECT * FROM `logicop`.`lstentradasmat`  where anul=0 and equ=@equ and fech between @fi and @ff order by 1 ;";
            if (o == 2)
                sql = @"SELECT * FROM `logicop`.`lstentradasmat`  where anul=0 and prov=@prov and fech between @fi and @ff order by 1 ;";
            if (o == 3)
                sql = @"SELECT * FROM `logicop`.`lstentradasmat`  where anul=0 and prov=@prov order by 1 ;";
            if (o == 4)
                sql = @"SELECT * FROM `logicop`.`lstentradasmat`  where anul=1 order by 1 ;";
            if (o == 5)
                sql = @"SELECT * FROM `logicop`.`lstentradasmat`  where anul=0 and prov=@prov and mat=@mat and fech between @fi and @ff order by 1 ;";

            List<entradasmat> list = new List<entradasmat>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                if (o == 2 || o == 3 || o == 5)
                    cmd.Parameters.AddWithValue("@prov", s);
                if (o == 1)
                    cmd.Parameters.AddWithValue("@equ", s);

                    cmd.Parameters.AddWithValue("@fi", a);
                    cmd.Parameters.AddWithValue("@ff", b);
                    cmd.Parameters.AddWithValue("@mat", c);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadentradasmat(reader));
                }
            }
            return list;
        }
        public static List<entradasmat> GellAllentradasmatcons(DateTime a, DateTime b )
        {

            string sql = @"SELECT * FROM `logicop`.`lstentradasmat` where anul=0 and fech between @fi and @ff order by 1 ;";
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
                        list.Add(Loadentradasmat(reader));
                }
            }
            return list;
        }
          public static List<entradasmat> Gellresumen3()
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
            string sql = @"    SELECT
                                m.nom as nom,
                                sum(volm) as total
                                FROM entradasmat e 
                                join materiales  m 
                                on e.mat=m.cod
                                where anul=0
                                and fech between @fi and @ff  group by 1 order by 2 desc;";
            
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
                        list.Add(Loadresumen3(reader));
                }
            }
            return list;
        }

        public static List<entradasmat> Gellresumen2()
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
                                `entradasmat`.`equ`,
                                count(`entradasmat`.`nsis`) as viajes,
                                sum(`entradasmat`.`tifkm`) as total
                                FROM `logicop`.`entradasmat`
                                where anul=0
                                and fech between @fi and @ff  group by 1 order by 2 desc;";
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
                        list.Add(Loadresumen2(reader));
                }
            }
            return list;
        }
        public static List<entradasmat> Gellresumen1()
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
            string sql = @" SELECT
                            (CASE ttra WHEN 'CR' THEN 'CREDITO'
                                      WHEN 'CP' THEN 'CONTADO PLANTA' 
                                      WHEN 'CO' THEN 'CONTADO OFICINA' 
                                      ELSE 'N/A' END) AS TRANSACCION,
                            sum(`entradasmat`.`matfle`) as total
                            FROM `logicop`.`entradasmat`
                            where anul=0
                            and fech between @fi and @ff  group by 1 order by 2 desc;";
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
                        list.Add(Loadresumen1(reader));
                }
            }
            return list;
        }

       
        private static entradasmat Loadresumen2(IDataReader reader)
        {
            //try
            //{
            decimal s;
            //cmd.Parameters.Add(new SqlParameter("@Symb", DBNull.Value.ToString()));
                entradasmat item = new entradasmat();
                item.rkm = Convert.ToInt32(reader["viajes"]);
                item.equ = Convert.ToString(reader["equ"]);
             
                s = reader["total"]==DBNull.Value ? 0: Convert.ToDecimal(reader["total"]);
                item.matfle = s;
                return item;
            //}
            //catch (Exception)
            //{
            
            //}
        }

        private static entradasmat Loadresumen1(IDataReader reader)
        {
            decimal s;
            entradasmat item = new entradasmat();
            item.ttra = Convert.ToString(reader["transaccion"]);
            s = reader["total"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total"]);
            item.matfle = s;
            return item;
        }
        private static entradasmat Loadresumen3(IDataReader reader)
        {
            decimal s;
            entradasmat item = new entradasmat();
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
        public static List<c_mat_dest> GellAllcmat2(DateTime a, DateTime b, string c)
        {

            List<c_mat_dest> list = new List<c_mat_dest>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand command;
                command = new MySqlCommand(@"SELECT
                        entradasmat.dcarg AS cod,
                        proyectos.nomproy AS nom,
                        materiales.nom as mat,
                        Sum(entradasmat.volm) AS vol
                        FROM
                        entradasmat
                        INNER JOIN proyectos ON proyectos.idproy = entradasmat.dcarg 
                        INNER JOIN materiales on materiales.cod=entradasmat.mat
                        where 
                        entradasmat.dcarg=@c and
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
                        entradasmat.prov AS cod,
                        proveedores.nom AS nom,
                        materiales.nom as mat,
                        Sum(entradasmat.volm) AS vol
                        FROM
                        entradasmat
                        INNER JOIN proveedores ON proveedores.cod = entradasmat.prov 
                        INNER JOIN materiales on materiales.cod=entradasmat.mat
                        where 
                        entradasmat.prov=@c and
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
        public static List<c_mat_dest>GellAllcmat(DateTime a, DateTime b, string c)
        {
        
            List<c_mat_dest> list = new List<c_mat_dest>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand command;
                command = new MySqlCommand(@"SELECT
                        entradasmat.prov AS cod,
                        proveedores.nom AS nom,
                        entradasmat.mat as mat,
                        Sum(entradasmat.volm) AS vol
                        FROM
                        entradasmat
                        INNER JOIN proveedores ON proveedores.cod = entradasmat.prov
                        where 
                        entradasmat.mat=@c and
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
                        list.Add( Loadcmat(reader));
                }
            }
            return list;
        }

        // devuleve un registro en particular
        public static entradasmat GellIdentradasmat(string c, int i)
        {
            entradasmat list = null;

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand command;
                if (i == 1)
                    command = new MySqlCommand("SELECT * FROM entradasmat WHERE nrec like @cod", conn);
                else if (i == 2)
                    command = new MySqlCommand("SELECT * FROM entradasmat WHERE nrec like @cod", conn);
                else
                    command = new MySqlCommand("SELECT * FROM entradasmat WHERE mat like @cod", conn);
                command.Parameters.AddWithValue("@cod", c);


                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list = Loadentradasmat(reader);
                }
            }
            return list;
        }
        public static List<entradasmat> GellIdsentradasmat(string c, DateTime a, DateTime b, int i)
        {
            List<entradasmat> list = new List<entradasmat>();

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand command;
                if (i == 1)
                    command = new MySqlCommand("SELECT * FROM lstentradasmat WHERE anul=0 and ttra like @cod and fech between @a and @b", conn);
                else if (i == 2)
                    command = new MySqlCommand("SELECT * FROM lstentradasmat WHERE anul=0 and equ like @cod and fech between @a and @b", conn);
                else
                    command = new MySqlCommand("SELECT * FROM lstentradasmat WHERE anul=0 and mat like @cod and fech between @a and @b", conn);

                command.Parameters.AddWithValue("@cod", c);
                command.Parameters.AddWithValue("@a", a);
                command.Parameters.AddWithValue("@b", b);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadentradasmat(reader));
                }
            }
            return list;
        }

        // Escribe los valores en la clase entradasmat
        private static entradasmat Loadentradasmat(IDataReader reader)
        {
            entradasmat item = new entradasmat();
            item.nsis = Convert.ToString(reader["nsis"]);
            item.nrec = Convert.ToString(reader["nrec"]);
            item.fech = Convert.ToDateTime(reader["fech"]);
            item.prov = Convert.ToString(reader["prov"]);
            item.mat = Convert.ToString(reader["mat"]);
            item.equ = Convert.ToString(reader["equ"]);
            item.ope = Convert.ToString(reader["ope"]);
            item.rkm = Convert.ToDecimal(reader["rkm"]);
            item.volm= Convert.ToDecimal(reader["volm"]);
            item.cosm = Convert.ToDecimal(reader["cosm"]);
            item.flem = Convert.ToDecimal(reader["flem"]);
            item.tifkm = Convert.ToDecimal(reader["tifkm"]);
            item.timat = Convert.ToDecimal(reader["timat"]);
            item.anul = Convert.ToInt32(reader["anul"]);
            item.matfle = Convert.ToDecimal(reader["matfle"]);
            item.ttra= Convert.ToString(reader["ttra"]);
            item.hini = Convert.ToString(reader["hini"]);
            item.hfin = Convert.ToString(reader["hfin"]);
            item.dcarg = Convert.ToString(reader["dcarg"]);
            //item.usu = Convert.ToString(reader["usu"]);
            return item;
        }


        // hace las operaciones correspondiente al procedimiento almacenado.
        public static int accion(entradasmat item, int op)
        {
            int rowsAffected = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();

                    MySqlCommand command = new MySqlCommand("SP_entradasmat", con);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramId = new MySqlParameter("msj", MySqlDbType.Int32);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    command.Parameters.AddWithValue("nsis1", item.nsis);
                    command.Parameters.AddWithValue("nrec1", item.nrec);
                    command.Parameters.AddWithValue("fech1", item.fech);
                    command.Parameters.AddWithValue("prov1", item.prov);
                    command.Parameters.AddWithValue("mat1", item.mat);
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
                    command.Parameters.AddWithValue("hini1", item.hini);
                    command.Parameters.AddWithValue("hfin1", item.hfin);
                    command.Parameters.AddWithValue("dcarg1", item.dcarg);
                    command.Parameters.AddWithValue("usu", item.usu);
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


        // devuleve un lista de registros de los entradasmat de forma servidor
        public static List<entradasmat> GellAllentradasmat2(string Id, int op)
        {
            List<entradasmat> list = new List<entradasmat>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    MySqlCommand command;
                    if (op == 1)
                        command = new MySqlCommand("SELECT * FROM entradasmat WHERE codeq like @cod", conn);
                    else if (op == 2)
                        command = new MySqlCommand("SELECT * FROM entradasmat WHERE nomeq like @cod", conn);
                    else if (op == 3)
                        command = new MySqlCommand("SELECT * FROM entradasmat WHERE tipeq = @cod", conn);
                    else
                        command = new MySqlCommand("SELECT * FROM entradasmat WHERE tipeq = @cod", conn);

                    command.Parameters.AddWithValue("cod", Id);

                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                            list.Add(Loadentradasmat(reader));
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

        public static List<entradasmat> GellAllentradasmat3(DateTime i, DateTime f, int op)
        {
            List<entradasmat> list = new List<entradasmat>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    MySqlCommand command=null;;
                    if (op == 1)
                        command = new MySqlCommand("SELECT * FROM lstentradasmat WHERE anul=0 and fech between @ini and @fin ", conn);
                    if (op == 2)
                        command = new MySqlCommand("SELECT * FROM lstentradasmat WHERE anul=0 and date(fech)= @ini", conn);
                  

                        command.Parameters.AddWithValue("@ini", i);
                        command.Parameters.AddWithValue("@fin", f);

                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows == true)
                        {
                            while (reader.Read())
                                list.Add(Loadentradasmat(reader));
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
        public static List<entradasmat> GellAllentradasmat4(DateTime i, DateTime f, string p,int op)
        {
            List<entradasmat> list = new List<entradasmat>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    MySqlCommand command = null; 
                    if (op == 1)
                        command = new MySqlCommand("SELECT * FROM lstentradasmat WHERE prov=@pro and anul=0 and fech between @ini and @fin ", conn);
                    //else if (op == 2)
                    //    command = new MySqlCommand("SELECT * FROM lstentradasmat WHERE date(fech1)= @ini", conn);


                    command.Parameters.AddWithValue("@ini", i);
                    command.Parameters.AddWithValue("@fin", f);
                    command.Parameters.AddWithValue("@pro", p);

                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                            list.Add(Loadentradasmat(reader));
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
        public static List<entradasmat> Gellresumenfechadet3(string mat, DateTime i, DateTime f, int opc)
        {

            string sql = null;
            if (opc == 1)
                sql = @"SELECT * FROM `logicop`.`lstentradasmat`
                                 where anul=0 and mat =@mat and
                                 fech between @ini and @fin;";
            if (opc == 2)
                sql = @"SELECT * FROM `logicop`.`lstentradasmat`
                                 where mat=@mat and anul=0 and
                                 date(fech)= @ini";

            List<entradasmat> list = new List<entradasmat>();
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
                        list.Add(Loadentradasmat(reader));
                }
            }
            return list;
        }


        public static List<entradasmat> Gellresumenfecha3(DateTime i, DateTime f, int opc)
        {
            string sql = null;
            if (opc == 1)
                sql = @"SELECT
                        m.nom as nom,
                        sum(volm) as total
                        FROM entradasmat e 
                        join materiales  m 
                        on e.mat=m.cod
                        where anul=0 and
                        fech between @ini and @fin
                        group by 1 order by 2 desc;";
            if (opc == 2)
                sql = @"SELECT
                        m.nom as nom,
                        sum(volm) as total
                        FROM entradasmat e 
                        join materiales  m 
                        on e.mat=m.cod
                        where anul=0 and
                        date(fech)= @ini
                        group by 1 order by 2 desc;";
            List<entradasmat> list = new List<entradasmat>();
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
        public static List<entradasmat> Gellresumenfecha33(string i)
        {
//            string sql = null;
          
//                sql = @"SELECT
//                        m.nom as nom,
//                        sum(matfle) as total
//                        FROM entradasmat e 
//                        join materiales  m 
//                        on e.mat=m.cod
//                        where anul=0 and
//                        date(fech)= @ini
//                        group by 1;";
            List<entradasmat> list = new List<entradasmat>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();

              
                MySqlCommand cmd = new MySqlCommand(i, conn);

                //cmd.Parameters.AddWithValue("@ini", );
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

         public static List<entradasmat> Gellresumenfechadet2(string veh, DateTime i, DateTime f, int opc)
        {

            string sql = null;
            if (opc == 1)
                sql = @"SELECT * FROM `logicop`.`lstentradasmat`
                                where anul=0 and equ=@equ and
                                fech between @ini and @fin;";
            if (opc == 2)
                sql = @"SELECT * FROM `logicop`.`lstentradasmat`
                                where equ=@equ and anul=0 and
                                date(fech)= @ini;";
            List<entradasmat> list = new List<entradasmat>();
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
                        list.Add(Loadentradasmat(reader));
                }
            }
            return list;
        }

        public static List<entradasmat> Gellresumenfecha2(DateTime i, DateTime f, int opc)
        {

            string sql = null;
            if(opc==1)
                sql= @"   SELECT
                                `entradasmat`.`equ`,
                                count(`entradasmat`.`nsis`) as viajes,
                                sum(`entradasmat`.`tifkm`) as total
                                FROM `logicop`.`entradasmat`
                                where anul=0 and
                                fech between @ini and @fin
                                group by 1 order by 2 desc;";
            if(opc==2)
                sql = @"   SELECT
                                `entradasmat`.`equ`,
                                count(`entradasmat`.`nsis`) as viajes,
                                sum(`entradasmat`.`tifkm`) as total
                                FROM `logicop`.`entradasmat`
                                where anul=0 and
                                date(fech)= @ini
                                group by 1 order by 2 desc;";
            List<entradasmat> list = new List<entradasmat>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ini",i);
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
        public static List<entradasmat> Gellresumenfechadet1(string ttra, DateTime i, DateTime f, int opc)
        {

            string sql = null;
            if (opc == 1)
                sql = @"SELECT * FROM `logicop`.`lstentradasmat`
                                 where anul=0 and ttra =@ttra and
                                 fech between @ini and @fin;";
            if (opc == 2)
                sql = @"SELECT * FROM `logicop`.`lstentradasmat`
                                 where ttra=@ttra and anul=0 and
                                 date(fech)= @ini";

            List<entradasmat> list = new List<entradasmat>();
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
                        list.Add(Loadentradasmat(reader));
                }
            }
            return list;
        }
        public static List<entradasmat> Gellresumenfecha1(DateTime i, DateTime f, int opc)
        {
             string sql = null;
            if(opc==1)
            sql= @" SELECT
                            (CASE ttra WHEN 'CR' THEN 'CREDITO'
                                      WHEN 'CP' THEN 'CONTADO PLANTA' 
                                      WHEN 'CO' THEN 'CONTADO OFICINA' 
                                      ELSE 'N/A' END) AS TRANSACCION,
                            sum(`entradasmat`.`matfle`) as total
                            FROM `logicop`.`entradasmat`
                            where anul=0 and
                            fech between @ini and @fin
                            group by 1 order by 2 desc;";
            if (opc == 2)
                sql = @" SELECT
                            (CASE ttra WHEN 'CR' THEN 'CREDITO'
                                      WHEN 'CP' THEN 'CONTADO PLANTA' 
                                      WHEN 'CO' THEN 'CONTADO OFICINA' 
                                      ELSE 'N/A' END) AS TRANSACCION,
                            sum(`entradasmat`.`matfle`) as total
                            FROM `logicop`.`entradasmat`
                            where anul=0 and
                            date(fech) = @ini
                            group by 1 order by 2 desc;";
            List<entradasmat> list = new List<entradasmat>();
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
    }
}
