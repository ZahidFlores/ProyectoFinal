using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Excel = Microsoft.Office.Interop.Excel;

namespace ProyectoFinal.Formularios
{
    public partial class FormPrincipal : Form {
        private int[,] matriz;
        private int renglon;
        private string wlinea;
        private int direccion;
        private bool espalreservada;
        private int estado;
        private string token, wsalida;
        private int posicion;
        private char caracter;
        private int columna;
        string nombreArchivo = "";
        string fechaformat = "";
        int id;
        string[] vectorPalabrasReservadas;
        string server = "Data Source = DESKTOP-39M6QEM\\SQLEXPRESS; Initial Catalog= SistemasProgramacion; Integrated Security = True ";
        SqlConnection conectar = new SqlConnection();
        string usuario;
        public FormPrincipal(string usuario)
        {

            InitializeComponent();
            this.usuario = usuario;

        }
        public DataTable Cargargcombo()
        {
            conectar.ConnectionString = server;
            conectar.Open();
            SqlDataAdapter da = new SqlDataAdapter("sp_cargarcombobox", conectar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            conectar.Close();
            conectar.Open();
            DateTime fechaactual= DateTime.Now;
            fechaformat =fechaactual.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "Select Nombre_Lenguaje from Lenguajes where Id_lenguaje=@Id_lenguaje";
            SqlCommand cmd = new SqlCommand(query, conectar);
            cmd.Parameters.AddWithValue("@Id_lenguaje", id.ToString());
            object resultado = cmd.ExecuteScalar();
            nombreArchivo = $"Output_{resultado}_{usuario}_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.txt";
            string rutaArchivo = Path.Combine(@"C:\Users\zaid_\Desktop\Outputs\", nombreArchivo);
            using (StreamWriter writer = new StreamWriter(rutaArchivo))
            {
                foreach (var item in ListSalida.Items)
                {
                    writer.WriteLine(item.ToString());
                }
            }
            conectar.Close();
            string idus = sacaridusuario(usuario);
            int a = Convert.ToInt32(idus);
            registro(a, id, fechaformat, nombreArchivo);
            ListEntrada.Items.Clear();
            ListSalida.Items.Clear();
            ListPreservadas.Items.Clear();
        }
        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Cargargcombo();
            comboBox1.DisplayMember = "Nombre_lenguaje";
            comboBox1.ValueMember = "Id_lenguaje";
            
        }
        private void BuscarTokens()
        {
            string apoyo;
            estado = 0;
            token = "";
            posicion = 1;
            while (posicion <= wlinea.Length)
            {
                apoyo = wlinea.Substring(posicion - 1, 1); // Extrae un carácter de wlinea
                caracter = apoyo.FirstOrDefault();
                CalcularColumna();
                estado = matriz[estado, columna];
                if (estado >= 100)
                {
                    if (token.Length > 0)
                    {
                        ReconoceTokens();
                    }
                    else if (token.Length == 0) // Únicamente caracteres especiales de un carácter
                    {
                        token = token + caracter;
                        ReconoceTokens();
                    }
                    estado = 0;
                    token = "";
                }
                else
                {
                    if (estado != 0) // Mientras sea diferente de 0, sigue agregando caracteres
                    {
                        token = token + caracter;
                    }
                }
                posicion++;
            }
            if (token.Length > 0)
            {
                caracter = ' ';
                CalcularColumna();
                estado = matriz[estado, columna];
                ReconoceTokens();
            }
        }
        private void CalcularColumna()
        {
            if (caracter >= 'A' && caracter <= 'Z' || caracter >= 'a' && caracter <= 'z')
            {
                columna = 0;
            }
            else if (caracter == ' ' || caracter == ' ')
            {
                columna = 2;
            }
            else if (caracter >= '0' && caracter <= '9')
            {
                columna = 1;
            }
            else if (caracter == '.')
            {
                columna = 3;
            }
            else if (caracter == '"')
            {
                columna = 4;
            }
            else if (caracter == '\'')
            {
                columna = 5;
            }
            else if (caracter == '+')
            {
                columna = 6;
            }
            else if (caracter == '-')
            {
                columna = 7;
            }
            else if (caracter == '*')
            {
                columna = 8;
            }
            else if (caracter == '/')
            {
                columna = 9;
            }
            else if (caracter == '<')
            {
                columna = 10;
            }
            else if (caracter == '>')
            {
                columna = 11;
            }
            else if (caracter == '=')
            {
                columna = 12;
            }
            else if (caracter == '_')
            {
                columna = 13;
            }
        }
        private void ReconoceTokens()
        {
            if (estado == 100)
            {
                espalreservada = false;
                BuscapalReservada();
                if (espalreservada)
                {
                    wsalida = token + "   PalReserv   " + direccion.ToString();
                }
                else
                {
                    //Buscaidentificadores();
                    wsalida = token + " Ident  ";
                }
                posicion = posicion - 1; // Regresa una posición requirió de un delimitador
            }

            if (estado == 101)
            {
                //BuscarEnteras();
                wsalida = token + " Cte. Enteras ";
                posicion = posicion - 1;
            }
            else if (estado == 102)
            {
                //BuscarReales();
                wsalida = token + " Cte. Real";
                posicion = posicion - 1;
            }

            if (estado == 105)
            {
                wsalida = token + " Car. Esp";
            }
            else if (estado == 106)
            {
                wsalida = token + " Car. Esp";
            }
            else if (estado == 107)
            {
                wsalida = token + " Car. Esp";
            }
            else if (estado == 108)
            {
                wsalida = token + " Car. Esp";
            }
            else if (estado == 109)
            {
                wsalida = token + " Car. Esp";
                posicion = posicion - 1;
            }
            else if (estado == 110)
            {
                token = token + caracter;
                wsalida = token + " Car. Esp";
            }
            else if (estado == 111)
            {
                wsalida = token + " Car. Esp";
                posicion = posicion - 1;
            }
            else if (estado == 112)
            {
                token = token + caracter;
                wsalida = token + " Car. Esp";
            }
            else if (estado == 113)
            {
                wsalida = token + " Car. Esp";
            }
            else if (estado == 104)
            {
                token = token + caracter;
                wsalida = token + " Comment";
            }

            if (estado == 103)
            {
                token = token + caracter;
                //uscarStrings()
                wsalida = token + " Cte. String ";
            }

            ListSalida.Items.Add(wsalida);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnAbrirarchivo_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string archivo = openFileDialog1.FileName;
            using (StreamReader fileReader = new StreamReader(archivo))
            {
                string stringReader;
                while ((stringReader = fileReader.ReadLine()) != null)
                {
                    ListEntrada.Items.Add(stringReader);
                }
            }
        }
        private void btnCompilar_Click(object sender, EventArgs e)
        {
            renglon = 0;
            while (renglon < ListEntrada.Items.Count)
            {
                ListEntrada.SelectedIndex = renglon;
                wlinea = ListEntrada.Text;
                BuscarTokens();
                renglon++;
            }
        }
        private void BuscapalReservada()
        {
                int renglon2 = 0;
                direccion = -1;
                while (!espalreservada && renglon2 < vectorPalabrasReservadas.Length)
                {
                    if (token.ToUpper() == vectorPalabrasReservadas[renglon2].ToUpper())
                    {
                        espalreservada = true;
                        direccion = renglon2;
                    }
                    renglon2 = renglon2 + 1;
                }
            
        }
        public void Visualbasic()
        {
            matriz = new int[10, 14];
            string rutaArchivo = @"C:\Users\zaid_\Desktop\Matrices de estados\" + id.ToString() + ".csv"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"C:\Users\zaid_\Desktop\Palabras Reservadas\Palabras_R_VisualBasic.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }
                vectorPalabrasReservadas = new string[37];
                string renglonvector;
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();


                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void Fortrain()
        {
            matriz = new int[10, 14];
            MessageBox.Show(id.ToString());
            string rutaArchivo = @"C:\Users\zaid_\Desktop\Matrices de estados\" + id.ToString() + ".csv"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"C:\Users\zaid_\Desktop\Palabras Reservadas\Palabras_R_Fortran.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }
   
                }
                
                string renglonvector;
                vectorPalabrasReservadas = new string[30];
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();
       
                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void Pascal()
        {

            matriz = new int[13, 27];
            MessageBox.Show(id.ToString());
            string rutaArchivo = @"C:\Users\zaid_\Desktop\Matrices de estados\" + id.ToString() + ".csv"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"C:\Users\zaid_\Desktop\Palabras Reservadas\Palabras_R_Pascal.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }

                string renglonvector;
                vectorPalabrasReservadas=new string[45];
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();

                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void Basic()
        {
            matriz = new int[10, 16];
            MessageBox.Show(id.ToString());
            string rutaArchivo = @"C:\Users\zaid_\Desktop\Matrices de estados\" + id.ToString() + ".csv"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"C:\Users\zaid_\Desktop\Palabras Reservadas\Palabras_R_Basic.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }

                string renglonvector;
                vectorPalabrasReservadas = new string[30]; 
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();

                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void Cobol()
        {
            matriz = new int[12, 20];
            MessageBox.Show(id.ToString());
            string rutaArchivo = @"C:\Users\zaid_\Desktop\Matrices de estados\" + id.ToString() + ".csv"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"C:\Users\zaid_\Desktop\Palabras Reservadas\Palabras_R_Cobol.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }

                string renglonvector;
                vectorPalabrasReservadas=new string[27];
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();

                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void C()
        {
            matriz = new int[12, 20];
            MessageBox.Show(id.ToString());
            string rutaArchivo = @"C:\Users\zaid_\Desktop\Matrices de estados\" + id.ToString() + ".csv"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"C:\Users\zaid_\Desktop\Palabras Reservadas\Palabras_R_C.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }

                string renglonvector;
                vectorPalabrasReservadas=new string[31];
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();

                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void VisualFoxpro()
        {
            matriz = new int[9, 19];
            MessageBox.Show(id.ToString());
            string rutaArchivo = @"C:\Users\zaid_\Desktop\Matrices de estados\" + id.ToString() + ".csv"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"C:\Users\zaid_\Desktop\Palabras Reservadas\Palabras_R_Visual_FoxPro.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }

                string renglonvector;
                vectorPalabrasReservadas = new string[20]; 
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();

                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void Clipper()
        {
            matriz = new int[12, 20];
            MessageBox.Show(id.ToString());
            string rutaArchivo = @"C:\Users\zaid_\Desktop\Matrices de estados\" + id.ToString() + ".csv"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"C:\Users\zaid_\Desktop\Palabras Reservadas\Palabras_R_Clipper.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }

                string renglonvector;
                vectorPalabrasReservadas = new string[74];
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();

                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void Dbase()
        {
            matriz = new int[16, 25];
            MessageBox.Show(id.ToString());
            string rutaArchivo = @"C:\Users\zaid_\Desktop\Matrices de estados\" + id.ToString() + ".csv"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"C:\Users\zaid_\Desktop\Palabras Reservadas\Palabras_R_Dbase.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }

                string renglonvector;
                vectorPalabrasReservadas = new string[37];
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();

                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void Java()
        {
            matriz = new int[12, 24];
            MessageBox.Show(id.ToString());
            string rutaArchivo = @"C:\Users\zaid_\Desktop\Matrices de estados\" + id.ToString() + ".csv"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"C:\Users\zaid_\Desktop\Palabras Reservadas\Palabras_R_Java.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }

                string renglonvector;
                vectorPalabrasReservadas = new string[50]; 
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();

                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void registro(int usuario,int id, string fechaformat, string nombreArchvo)
        {
            conectar.Open();
            SqlCommand cmd = new SqlCommand("guardarregistro", conectar);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id_Usuario", usuario);
            cmd.Parameters.AddWithValue("@Id_lenguaje", id);
            cmd.Parameters.AddWithValue("@fecha_hora", fechaformat);
            cmd.Parameters.AddWithValue("@nombre_archivo", nombreArchivo);
            try
            {
                MessageBox.Show("Registro agregado correctamente");
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
            conectar.Close();


        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {   
            Login frm1 = new Login();
            conectar.Close();
            frm1.Show();
            this.Close();
        }
        public string sacaridusuario(string usuario)
        {
            string idusuario;
            string query = "Select Id_Usuario from Usuarios where Usuario=@Usuario";
            conectar.Open();
            SqlCommand cmd = new SqlCommand(query, conectar);
            cmd.Parameters.AddWithValue("@Usuario", usuario);
            object result = cmd.ExecuteScalar();
            idusuario = result.ToString();
            conectar.Close();
            return idusuario;
          
        }
        public int palabrasreserv()
        {
            int ids=comboBox1.SelectedIndex+1;
            return ids;
        }
        private void btnRegistros_Click(object sender, EventArgs e)
        {
            Reportes frmreportes = new Reportes();  
            frmreportes.Show();
        }
        private void btnsaveexcel_Click(object sender, EventArgs e)
        {
            conectar.Close();
            conectar.Open();
            DateTime fechaactual = DateTime.Now;
            fechaformat = fechaactual.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "Select Nombre_Lenguaje from Lenguajes where Id_lenguaje=@Id_lenguaje";
            SqlCommand cmd = new SqlCommand(query, conectar);
            cmd.Parameters.AddWithValue("@Id_lenguaje", id.ToString());
            object resultado = cmd.ExecuteScalar();
            Excel.Application excelapp = new Excel.Application();
            excelapp.Visible = false;
            Excel.Workbook workbook = excelapp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet as Excel.Worksheet;
            nombreArchivo = $"Output_{resultado}_{usuario}_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.xlsx";
            string rutaArchivo = Path.Combine(@"C:\Users\zaid_\Desktop\Outputs\", nombreArchivo);
            for (int i = 0; i < ListSalida.Items.Count; i++)
            {
                string item = ListSalida.Items[i].ToString();
                worksheet.Cells[i + 1, 1] = item; // Guarda cada elemento en una celda de la columna A
            }
            workbook.SaveAs(rutaArchivo);
            workbook.Close();
            excelapp.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelapp);
            conectar.Close();
            string idus = sacaridusuario(usuario);
            int a = Convert.ToInt32(idus);
            registro(a, id, fechaformat, nombreArchivo);
            ListEntrada.Items.Clear();
            ListSalida.Items.Clear();
            ListPreservadas.Items.Clear();
        }

        private void btnsavecsv_Click(object sender, EventArgs e)
        {
            conectar.Close();
            conectar.Open();
            DateTime fechaactual = DateTime.Now;
            fechaformat = fechaactual.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "Select Nombre_Lenguaje from Lenguajes where Id_lenguaje=@Id_lenguaje";
            SqlCommand cmd = new SqlCommand(query, conectar);
            cmd.Parameters.AddWithValue("@Id_lenguaje", id.ToString());
            object resultado = cmd.ExecuteScalar();
            nombreArchivo = $"Output_{resultado}_{usuario}_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.csv";
            string rutaArchivo = Path.Combine(@"C:\Users\zaid_\Desktop\Outputs\", nombreArchivo);
            using (StreamWriter writer = new StreamWriter(rutaArchivo))
            {
                foreach (var item in ListSalida.Items)
                {
                    writer.WriteLine(item.ToString());
                }
            }
            conectar.Close();
            string idus = sacaridusuario(usuario);
            int a = Convert.ToInt32(idus);
            registro(a, id, fechaformat, nombreArchivo);
            ListEntrada.Items.Clear();
            ListSalida.Items.Clear();
            ListPreservadas.Items.Clear();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            ListPreservadas.Items.Clear();
            id = 0;
            id = palabrasreserv();
      
            if (comboBox1.Text == "Visual Basic")
            {
                Visualbasic();
            }
            else if (comboBox1.Text == "Fortran")
            {
                Fortrain();
            }
            else if (comboBox1.Text == "Pascal")
            {
                Pascal();
            }
            else if (comboBox1.Text == "Basic")
            {
                Basic();
            }
            else if (comboBox1.Text == "Cobol")
            {
                Cobol();
            }
            else if (comboBox1.Text == "C")
            {
                C();
            }
            else if (comboBox1.Text == "Visual Foxpro")
            {
                VisualFoxpro();
            }
            else if (comboBox1.Text == "Clipper")
            {
                Clipper();
            }
            else if (comboBox1.Text == "Dbase")
            {
                Dbase();
            }
            else if (comboBox1.Text == "Java")
            {
                Java();
            }
            
        }
    }
 }


