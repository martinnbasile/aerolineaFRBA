using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AerolineaFrba.ConexionALaBase
{
    class CargadorDeEstructuras
    {
       
        public static void cargarComboBox(ComboBox unCombo,SqlDataReader reader)
        {
            while (reader.Read())
            {
                unCombo.Items.Add(reader.GetSqlString(0));
            }
            reader.Dispose();
        }
    }
}
