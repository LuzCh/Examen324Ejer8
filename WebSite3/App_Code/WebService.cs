using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public int editarUsuario(String usuario, String nom, String direc, String depa, int telf)
    {
        try
        {
            using (MySqlConnection con = new MySqlConnection("Server=localhost;Database=luzbd;User=luzgod;Password=12345;"))
            {
                con.Open();
                using (MySqlCommand com = new MySqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "UPDATE `usuarios` SET `nombre`=@nom,`direccion`=@direc,`departamento`=@depa,`telefono`=@telf WHERE nombre_usuario=@usuario";
                    com.Parameters.AddWithValue("@nom", nom);
                    com.Parameters.AddWithValue("@direc", direc);
                    com.Parameters.AddWithValue("@depa", depa);
                    com.Parameters.AddWithValue("@telf", telf);
                    com.Parameters.AddWithValue("@usuario", usuario);
                    com.ExecuteNonQuery();
                }
            }
            return 1;
        }
        catch (MySqlException ex)
        {
            string errorMessage = "Error MySQL al modificalr al usuario:" + ex.Message;
            Console.WriteLine(errorMessage);
            return -1; 
        }
    }


    [WebMethod]
    public int agregarUsuario(String usuario, String contra, String rol, int ci, String nom, String direc, String depa, int telf)
    {
        try
        {
            using (MySqlConnection con = new MySqlConnection("Server=localhost;Database=luzbd;User=luzgod;Password=12345;"))
            {
                con.Open();
                using (MySqlCommand com = new MySqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "INSERT INTO usuarios VALUES (NULL, @usuario, @contra, @rol, @ci, @nom, @direccion, @depa, @telf)";
                    com.Parameters.AddWithValue("@usuario", usuario);
                    com.Parameters.AddWithValue("@contra", contra);
                    com.Parameters.AddWithValue("@rol", rol);
                    com.Parameters.AddWithValue("@ci", ci);
                    com.Parameters.AddWithValue("@nom", nom);
                    com.Parameters.AddWithValue("@direccion", direc);
                    com.Parameters.AddWithValue("@depa", depa);
                    com.Parameters.AddWithValue("@telf", telf);
                    com.ExecuteNonQuery();
                }
            }
            return 1; 
        }
        catch (MySqlException ex)
        {
            string errorMessage = "Error MySQL al agregar usuario: " + ex.Message;
            Console.WriteLine(errorMessage);
            return -1;
        }
    }

    [WebMethod]
    public int eliminarUsuario(int id, String usuario, String contra)
    {
        try
        {
            using (MySqlConnection con = new MySqlConnection("Server=localhost;Database=luzbd;User=luzgod;Password=12345;"))
            {
                con.Open();
                using (MySqlCommand com = new MySqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "UPDATE usuarios SET nombre_usuario='ELIMINADO' WHERE nombre_usuario=@usuario and contrasena_usuario=@contra";
                    com.Parameters.AddWithValue("@usuario", usuario);
                    com.Parameters.AddWithValue("@contra", contra);
                    com.ExecuteNonQuery();
                    com.CommandText = "UPDATE cuentas SET estado_cuenta='CONGELADA' WHERE id_usuario=@id";
                    com.Parameters.AddWithValue("@id", id);
                    com.ExecuteNonQuery();
                }
            }
            return 1;
        }
        catch (MySqlException ex)
        {
            string errorMessage = "Error MySQL al eliminar usuario: " + ex.Message;
            Console.WriteLine(errorMessage);
            return -1;
        }
    }

}
