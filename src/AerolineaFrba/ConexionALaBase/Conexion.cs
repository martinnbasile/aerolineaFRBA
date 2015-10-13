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
        SqlConnection conexxxxx = conectarBase();
       

        public static SqlConnection conectarBase()
        {
            SqlConnection cone = new SqlConnection(AerolineaFrba.Properties.Settings.Default.GD2C2015ConnectionString);
            cone.Open();
            return cone;
        }
        public static SqlCommand getComando()
        {
            SqlCommand comandoo = conseguirComando(); 
            return comandoo;
        }
        public static SqlCommand conseguirComando()
        {
            SqlCommand comand = new SqlCommand();
            SqlConnection conexxxxx = conectarBase();
            comand.Connection = conexxxxx;
            return comand;
        }

        public static SqlDataReader consultarBase(String unaQuery){
            SqlCommand comandoo = conseguirComando(); 
            comandoo.CommandText = unaQuery;
            return comandoo.ExecuteReader();
        }

        public static void ejecutarNonQuery(String unaQuery)
        {
            SqlCommand comandoo = conseguirComando(); 
            comandoo.CommandText = unaQuery;
            comandoo.ExecuteNonQuery();
            return;
        }

    }
}
