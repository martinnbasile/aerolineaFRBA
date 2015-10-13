using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Validaciones
{
    class Validaciones
    {
        public static bool validarTextBox(TextBox unTextBox, String unMensajeDeAlerta)
        {
            if (unTextBox.Text.Length == 0)
            {
                MessageBox.Show(unMensajeDeAlerta);
                return false;
            }
            return true;
        }

        public static bool validarComboBox(ComboBox unComboBox, String unMensajeDeAlerta)
        {
            if (unComboBox.SelectedIndex == -1)
            {
                MessageBox.Show(unMensajeDeAlerta);
                return false;
            }
            return true;
        }
        
        public static bool validarListBox(ListBox unListBox, String unMensajeDeAlerta)
        {
            if (unListBox.SelectedIndex < 0)
            {
                MessageBox.Show(unMensajeDeAlerta);
                return false;
            }
            return true;
        }

        public static String validarUsername(String unUsername)
        {
            SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select count (*) from usuarios where Username='" + unUsername + "'");
            reader.Read();
            Int32 vecesQueAparece = reader.GetInt32(0);
            if (vecesQueAparece == 1) { return unUsername; }
            else
            {
                throw new Exception();
            }
        }
    }
}