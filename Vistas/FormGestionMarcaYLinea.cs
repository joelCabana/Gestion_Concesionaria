using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClasesBase;

namespace Vistas
{
    public partial class FormGestionMarcaYLinea : Form
    {
        public FormGestionMarcaYLinea()
        {
            InitializeComponent();
        }

        private void FormGestionMarcaYLinea_Load(object sender, EventArgs e)
        {
            actualizarDataGridMarca();
            actualizarDataGridLinea();
            carga_combo_marca();

            btnModificarLinea.Enabled = false;
            btnEliminarLinea.Enabled = false;
            btnModificarMarca.Enabled = false;
            btnEiminarMarca.Enabled = false;
        }

        private void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            if (txtMarca.Text == null || txtMarca.Text == "")
            {
                MessageBox.Show("No puede haber campos vacios");
            }
            else {
                TrabajarVehiculo.insertar_marca(txtMarca.Text);
                actualizarDataGridMarca();
                txtMarca.Text = null;
            }
         
            
        }

        private void carga_combo_marca()
        {
            cmbMarca.DisplayMember = "mar_nombre";
            cmbMarca.ValueMember = "mar_id";
            cmbMarca.DataSource = TrabajarVehiculo.listar_marca();
            cmbMarca.SelectedIndex = -1;
        }

        public void actualizarDataGridMarca(){
            dgvMarcaVehiculo.DataSource = TrabajarVehiculo.listar_marcaTable();
        }

        public void actualizarDataGridLinea() {
            dgvLinea.DataSource = TrabajarVehiculo.listar_lineaTable();
        }

        private void btnEiminarMarca_Click(object sender, EventArgs e)
        {

            var confirmResult = MessageBox.Show("¿Seguro que quieres Eliminar?",
                                    "¿Eliminar?",
                                    MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvMarcaVehiculo.CurrentRow.Cells["Id"].Value);
                TrabajarVehiculo.eliminar_marca(id);
                actualizarDataGridMarca();
            }
        }

        private void btnModificarMarca_Click(object sender, EventArgs e)
        {
            if (txtMarca.Text == null || txtMarca.Text == "" || cmbMarca.SelectedIndex == -1)
            {
                MessageBox.Show("No puede haber campos vacios");
            }
            else
            {

                var confirmResult = MessageBox.Show("¿Seguro que quieres Modificar?",
                                     "¿Modificar?",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvMarcaVehiculo.CurrentRow.Cells["Id"].Value);
                    string nombre = txtMarca.Text;
                    TrabajarVehiculo.actualizar_marca(id, nombre);
                    actualizarDataGridMarca();
                }

            }


           

        }

        private void dgvMarcaVehiculo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMarca.Text = Convert.ToString(dgvMarcaVehiculo.CurrentRow.Cells["Nombre"].Value);
            btnModificarMarca.Enabled = true;
            btnEiminarMarca.Enabled = true;
        }

        private void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            if (txtLinea.Text == null || txtLinea.Text == "")
            {
                MessageBox.Show("No puede haber campos vacios");


            }
            else {
                TrabajarVehiculo.insertar_linea(txtLinea.Text, Convert.ToInt32(cmbMarca.SelectedValue));
                actualizarDataGridLinea();
                limpiarCamposLinea();
            }
            
         
        }

        private void btnEliminarLinea_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("¿Seguro que quieres Eliminar?",
                                       "¿Eliminar?",
                                       MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvLinea.CurrentRow.Cells["Id"].Value);
                TrabajarVehiculo.eliminar_linea(id);
                actualizarDataGridLinea();

                limpiarCamposLinea();
            }
            else {
                limpiarCamposLinea();
            }

           
        }


        private void limpiarCamposLinea(){
        txtLinea.Text = null;
                cmbMarca.SelectedIndex = -1;
        }

        private void btnModificarLinea_Click(object sender, EventArgs e)
        {

            if (txtLinea.Text == null || txtLinea.Text == "")
            {
                MessageBox.Show("No puede haber campos vacios");
            }
            else
            {

                var confirmResult = MessageBox.Show("¿Seguro que quieres Modificar?",
                                     "¿Modificar?",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvLinea.CurrentRow.Cells["Id"].Value);
                    string nombre = txtLinea.Text;
                    int idMarca = Convert.ToInt32(cmbMarca.SelectedValue);
                    TrabajarVehiculo.actualizar_Linea(id, nombre, idMarca);
                    actualizarDataGridLinea();


                    btnModificarLinea.Enabled = false;
                    limpiarCamposLinea();
                }
                else {
                    limpiarCamposLinea();
                }

            }


           
        }

        private void dgvLinea_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtLinea.Text = Convert.ToString(dgvLinea.CurrentRow.Cells["Linea"].Value);
            cmbMarca.SelectedIndex = Convert.ToInt32(dgvLinea.CurrentRow.Cells["Id Marca"].Value) - 1;
            btnModificarLinea.Enabled = true;
            btnEliminarLinea.Enabled = true;
        }

  

    }
}
