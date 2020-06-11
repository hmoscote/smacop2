using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smacop2.Entity
{
    public class combos
    {
        public string cod { get; set; }
        public string nom { get; set; }

    }
  
    public class consulta_01
    {
        public string nombre { get; set; }
        public decimal cantidad{ get; set; }

    }

    public class usu
    {
        public string nombre { get; set; }
        public bool act { get; set; }

    }
     public class consulta_02
    {
        public string ccos { get; set; }
        public int nrec{ get; set; }
        public decimal matf{ get; set; }
        public decimal matft{ get; set; }
    }

     public class consulta_03
     {
         public string eq { get; set; }
         public decimal me { get; set; }
         public decimal ms { get; set; }
         public decimal total { get; set; }
     }


    public class tipo_equipos
    {
        public string cod { get; set; }
        public string nom { get; set; }
        public string tip { get; set; }

    }

    public class materiales: tipo_equipos
    {
    }

    public class documento: combos
    {
        public documento(string id, string desc)
        {
            this.cod = id;
            this.nom = desc;
        }
    }

    public class combos2 : combos
    {
        public string cod1 { get; set; }
    }
  
    public class empleados : combos
    {
        public byte[] foto { get; set; }
        public string cargo { get; set; }
        public string tipid { get; set; }
        public string dir { get; set; }
        public string tel { get; set; }
        public string cel { get; set; }
        public string email { get; set; }
        public bool act { get; set; }

        public string NombreCompleto
        {
            get
            {
                return String.Format("{0} - {1}", this.cod, this.nom);
            }
        }

    }

    public class clientes:empleados {}

    public class contratistas : empleados {}

   

    public class proveedores : empleados
    {
        public string dpto { get; set; }
        public string mun { get; set; }
        public string cont { get; set; }
    }

    public class cargos : combos
    {
        public int niv { get; set; }
        public decimal sal { get; set; }
        public string NombreCompleto
        {
            get
            {
                return String.Format("{0} - {1}", this.cod, this.nom);
            }
        }
    }

    public class conf
    {
        public string tabla { get; set; }
        public string campo { get; set; }
        public string valor { get; set; }
        public bool cargar { get; set; }
    }

    public class equipos
    {
        public string cod { get; set; }
        public string des { get; set; }
        public string mod { get; set; }
        public string pot { get; set; }
        public string cap { get; set; }
        public string cmb { get; set; }
        public string ser { get; set; }
        public string teq { get; set; }
        public string pla { get; set; }
        public decimal hms { get; set; }
        public decimal kms { get; set; }
        public string ope { get; set; }
        public decimal vho { get; set; }
        public decimal csh { get; set; }
        public decimal ccg { get; set; }
    }

    public class proyectos
    {
        public string idproy { get; set; }
        public string nomproy { get; set; }
        public string desproy { get; set; }
        public string tipproy { get; set; }
        public string contproy { get; set; }
        public string dirproy { get; set; }
        public string respproy { get; set; }
        public bool actproy { get; set; }
    }

    public class entradasmat
    {
        public string usu { get; set; }
        public string nsis { get; set; }
        public string nrec { get; set; }
        public DateTime fech { get; set; }
        public string prov { get; set; }
        public string mat { get; set; }
        public string equ { get; set; }
        public string ope { get; set; }
        public decimal rkm { get; set; }
        public decimal volm{ get; set; }
        public decimal cosm { get; set; }
        public decimal flem{ get; set; }
        public decimal tifkm { get; set; }
        public decimal timat { get; set; }
        public decimal matfle { get; set; }
        public string ttra { get; set; }
        public string  hini { get; set; }
        public string  hfin { get; set; }
        public string  dcarg { get; set; }
      
        public decimal Tifkm
        {
            get
            {
                return  rkm * volm * flem;
            }
        }
        public decimal Timat
        {
            get
            {
                return volm * cosm;
            }
        }

        public decimal Matfle
        {
            get
            {
                return Timat + Tifkm;
            }
        }
        public int anul { get; set; }
    }
    public class salidasmat: entradasmat
    {
        public string ccos { get; set; }
        public string tven { get; set; }
        public string lug { get; set; }
        public int opcd { get; set; }
        public decimal cant { get; set; }
      
        public int opcdt { get; set; }
        public string ccost { get; set; }
        public string cont { get; set; }
    
        public decimal flemt { get; set; }
        public decimal tifkmt { get; set; }
        public decimal timatt { get; set; }
        public decimal matflet { get; set; }
    
        //liquidacion

        public decimal Tifkm1
        {
            get
            {return rkm * cant * flem;}
        }
        public decimal Timat1
        {
            get
            {return cant * cosm;}
        }

        public decimal Matfle1
        {
            get
            { return Timat1 + Tifkm1;}
        }

     // facturacion
        public decimal Tifkmt1
        {
            get
            { return rkm * cant * flemt; }
        }
      

        public decimal Matflet1
        {
            get
            {return Timat1 + Tifkmt1;}
        }


    }
    public class rango
    {
        public string mat { get; set; }
        public int rini { get; set; }
        public int rfin { get; set; }
        public decimal precio { get; set; }
    }
    public class rangofle
    {
        public string cod { get; set; }
        public decimal vini { get; set; }
        public decimal vfin { get; set; }
        public decimal precio { get; set; }
    }

    public class cant_material
    {
        public string cod { get; set; }
        public string mat { get; set; }
        public string cnt { get; set; }
        public decimal costo { get; set; }
    }

    public class c_mat_dest
    {
        public string cod { get; set; }
        public string des { get; set; }
        public string mat { get; set; }
        public decimal vol { get; set; }
    }
   
   
}
