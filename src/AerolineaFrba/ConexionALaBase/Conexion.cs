using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace AerolineaFrba.ConexionALaBase
{
    class Conexion
    {
        public static SqlConnection conexxxxx = conectarBase();
       
        private static SqlCommand commandTran;
        static SqlConnection cone;
        public static SqlConnection conectarBase()
        {
            cone = new SqlConnection(AerolineaFrba.Properties.Settings.Default.GD2C2015ConnectionString);
            cone.Open();
            return cone;
        }
        public static SqlCommand getComando()
        {
             
            return conseguirComando();
        }
       public static SqlCommand conseguirComando()
        {
            SqlCommand comand = new SqlCommand();
            comand.Connection = conectarBase();
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
            comandoo.CommandTimeout = 21600;
            comandoo.ExecuteNonQuery();
            return;
        }

        
        

        public static void verificarConexion()
        {
            try { ConexionALaBase.Conexion.conectarBase(); }
            catch (SqlException exc)
            {
                MessageBox.Show("Error de conexion"); //verifica la contraseña del user gd
                MessageBox.Show(exc.Message);
            }
        }

        

    }
}
