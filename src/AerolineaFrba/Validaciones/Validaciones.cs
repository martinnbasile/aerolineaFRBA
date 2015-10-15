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

        public static bool validarMaskedTextBox(MaskedTextBox unMaskedTextBox, String unMensajeDeAlerta)
        {
            if (unMaskedTextBox.Text.Length == 0)
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
            SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select username from usuarios where Username='" + unUsername + "'");
            reader.Read();
            return reader.GetString(0);
        }
        public static bool validarCalendario(MonthCalendar unCalendario)
        {
            // unCalendario.SelectionRange.Start
            return true;
        }        
    }
}