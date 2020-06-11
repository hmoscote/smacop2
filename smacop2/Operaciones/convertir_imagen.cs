using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace smacop2.Operaciones
{
    class convertir_imagen
    {
        public static byte[] Image2Bytes(Image pImagen)
        {
            byte[] mImage = null;
            try
            {
                if (pImagen != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pImagen.Save(ms, pImagen.RawFormat);
                        mImage = ms.GetBuffer();
                        ms.Close();
                    }
                }
                else { mImage = null; }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return mImage;
        }

        public static Image Bytes2Image(byte[] bytes)
        {
            if (bytes == null) return null;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Bitmap bm = null;
                try
                {
                    bm = new Bitmap(ms);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return bm;
            }
        }

        //public static Image ObtenerImagenNoDisponible()
        //{
        //    Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
        //    //Stream file = assembly.ge("Resources.NoDisponible.jpg");
        //    //return Image.FromStream(file);
        //}

    }
}

