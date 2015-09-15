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
        static SqlConnection conexxxxx = conectarBase();
        private static SqlCommand comandoo = conseguirComando(); 

        public static SqlConnection conectarBase()
        {
            SqlConnection cone = new SqlConnection(AerolineaFrba.Properties.Settings.Default.GD1C2015ConnectionString);
            cone.Open();
            return cone;
        }
        public static SqlCommand getComando()
        {
            return comandoo;
        }
        public static SqlCommand conseguirComando()
        {
            SqlCommand comand = new SqlCommand();
            comand.Connection = conexxxxx;
            return comand;
        }

        public static SqlDataReader consultarBase(String unaQuery){
            comandoo.CommandText = unaQuery;
            return comandoo.ExecuteReader();
        }

        public static void ejecutarNonQuery(String unaQuery)
        {
            comandoo.CommandText = unaQuery;
            comandoo.ExecuteNonQuery();
            return;
        }

    }
}
