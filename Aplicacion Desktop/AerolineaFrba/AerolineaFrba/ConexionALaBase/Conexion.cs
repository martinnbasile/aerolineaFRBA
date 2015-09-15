using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace AerolineaFrba.ConexionALaBase
{
    class Conexion
    {
        static System.Data.SqlClient.SqlConnection con = conectarBase();
        static SqlCommand com = comando();

        public static SqlConnection conectarBase()
        {
            //string fechaDelSistema = Properties.Settings.Default.fechaDelSistema;
            SqlConnection cone = new SqlConnection(AerolineaFrba.Properties.Settings.Default.GD1C2015ConnectionString);
            cone.Open();
            return cone;
        }
        public static SqlCommand getComando()
        {
            return com;
        }
        public static SqlCommand comando()
        {
            SqlCommand comand = new SqlCommand();
            comand.Connection = con;
            return comand;
        }
        
    }
}
