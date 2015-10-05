using System;
using System.Collections.Generic;
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
    }
}