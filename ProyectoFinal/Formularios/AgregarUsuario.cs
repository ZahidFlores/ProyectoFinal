using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class AgregarUsuario : Form
    {
        string server = "Data Source = DESKTOP-39M6QEM\\SQLEXPRESS; Initial Catalog= SistemasProgramacion; Integrated Security = True ";
        SqlConnection conectar = new SqlConnection();
        
        public AgregarUsuario()
        {
            InitializeComponent();
        }

        private void AgregarUsuario_Load(object sender, EventArgs e)
        {

        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertir la contraseña en un arreglo de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convertir el arreglo de bytes a una cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtUser.Text== null || txtNombre.Text== null || txtApPaterno.Text == null || txtApMaterno.Text == null || txtTelefono.Text== null || txtPassword.Text == null)
            {
                MessageBox.Show("Favor de llenar todos los campos");
            }
            else {
                string password = txtPassword.Text;
                string hashedpassword = HashPassword(password);
                MessageBox.Show(hashedpassword);
                conectar.ConnectionString = server;
            conectar.Open();
            SqlCommand cmd = new SqlCommand("sp_agregarusuario1", conectar);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id_Usuario", txtIdUser.Text);
            cmd.Parameters.AddWithValue("@Usuario", txtUser.Text);
            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
            cmd.Parameters.AddWithValue("@ApPaterno", txtApPaterno.Text);
            cmd.Parameters.AddWithValue("@ApMaterno", txtApMaterno.Text);
            cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
            cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
            cmd.Parameters.AddWithValue("@contra", hashedpassword.ToString());
            try
            {
                MessageBox.Show("Usuario agregado correctamente");
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
            conectar.Close();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            Login form1 = new Login();
            form1.Show();
               
        }
    }
}
