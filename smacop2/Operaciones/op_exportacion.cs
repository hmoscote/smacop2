
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Collections;

namespace smacop2.Operaciones
{
    class op_exportacion
    {
        public static void opcion(DataGridView d,string filtros,int opcexportacion, string nom)
        {
              SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = filtros;
             //saveFileDialog1.Filter = "html (*.html)|*.html";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.FileName = nom;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ArrayList titulos = new ArrayList();
                    DataTable datosTabla = new DataTable();
                    //Especificar rutal del archivo con extencion de HTML.
                    op_formatos OF = new op_formatos(saveFileDialog1.FileName.ToString());

                    //obtenemos los titulos del grid y creamos las columnas de la tabla
                    foreach (DataGridViewColumn item in d.Columns)
                    {
                        titulos.Add(item.HeaderText);
                        datosTabla.Columns.Add();
                    }
                    //se crean los renglones de la tabla
                    foreach (DataGridViewRow item in d.Rows)
                    {
                        DataRow rowx = datosTabla.NewRow();
                        datosTabla.Rows.Add(rowx);
                    }
                    //se pasan los datos del dataGridView a la tabla
                    foreach (DataGridViewColumn item in d.Columns)
                    {
                        foreach (DataGridViewRow itemx in d.Rows)
                        {
                            datosTabla.Rows[itemx.Index][item.Index] = d[item.Index, itemx.Index].Value;
                        }
                    }
                    if (opcexportacion==1)
                         OF.ExportCSV(titulos, datosTabla);
                    else
                        OF.Export(titulos, datosTabla);
                    
                    Process.Start(OF.xpath);
                    MessageBox.Show("Procceso Completo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
