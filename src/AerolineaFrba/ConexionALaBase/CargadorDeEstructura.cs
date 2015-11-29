using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace AerolineaFrba.ConexionALaBase
{
    class CargadorDeEstructuras
    {
        public static void cargarDataGrid(DataGridView unDataGrid ,String unaQuery){
            DataTable dataTable = new DataTable();
            SqlCommand command = new SqlCommand(unaQuery, Conexion.conexxxxx);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);
            unDataGrid.DataSource = dataTable;
            unDataGrid.AllowUserToAddRows = false;
            unDataGrid.ReadOnly = true;
            unDataGrid.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        }

        public static void cargarDataGrid(SqlCommand command, DataGridView unDataGrid, String unaQuery)
        {
            DataTable dataTable = new DataTable();
            command.CommandText = unaQuery;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);
            unDataGrid.DataSource = dataTable;
            unDataGrid.AllowUserToAddRows = false;
            unDataGrid.ReadOnly = true;
            unDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public static void cargarComboBox(ComboBox unCombo,SqlDataReader reader)
        {
            while (reader.Read())
            {
                unCombo.Items.Add(reader.GetSqlString(0));
            }
            reader.Dispose();
        }
        public static void cargarListBox(ListBox unListBox, SqlDataReader reader)
        {
            while (reader.Read())
            {
                unListBox.Items.Add(reader.GetSqlString(0));
            }
            reader.Dispose();
        }
    }
}
