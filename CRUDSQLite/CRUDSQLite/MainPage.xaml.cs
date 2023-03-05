using CRUDSQLite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CRUDSQLite
{
    public partial class MainPage : ContentPage
    {
        bool LimpiarCampos = false;
        public MainPage()
        {
            InitializeComponent();
            LlenarDatos();
        }

        private async void btnRegistar_Clicked(object sender, EventArgs e)
        {
            
                if (ValidarDatos())
                {
                Usuario usuario = new Usuario
                {
                    Nombre = txtNombre.Text,
                    Estado = txtEstado.Text,
                    Lectura = txtLectura.Text,
                    LecturaAnterior = txtLecturaAnterior.Text,
                    Ruta = txtRuta.Text,
                    Secuencia = txtSecuencia.Text
                 
                };
                LimpiarCampos = true;
                await App.SQLiteDB.GuardarUsuarioAsync(usuario);

                if (LimpiarCampos)
                {
                    LimpiarControles();
                    await DisplayAlert("Registro", "Se ha guardado con exito el estado", "Ok");
                    LlenarDatos();
                }

                
                }
                else
                {
                    await DisplayAlert("Advertencia", "Ingrese todos los datos", "OK");
                }
            
            
        }
        public async void LlenarDatos()
        {
            var usuarioLista = await App.SQLiteDB.GetusuarioAsync();

            if (usuarioLista != null)
            {
                lstUsuarios.ItemsSource = usuarioLista;
            }
        }
        public bool ValidarDatos()
        {
            bool respuesta;

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
           
          else  if (string.IsNullOrEmpty(txtEstado.Text))
            {
                respuesta = false;
            }
           
           else if (string.IsNullOrEmpty(txtLectura.Text))
            {
                respuesta = false;
            }
            
           else if (string.IsNullOrEmpty(txtLecturaAnterior.Text))
            {
                respuesta = false;
            }
          
            
           else if (string.IsNullOrEmpty(txtRuta.Text))
            {
                respuesta = false;
            }
            
           else if (string.IsNullOrEmpty(txtSecuencia.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }

            return respuesta;
        }

        private  async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdUsuario.Text))
            {
                Usuario usuario = new Usuario()
                {
                    Id = Convert.ToInt32(txtIdUsuario.Text),
                    Nombre = txtNombre.Text,
                    Estado = txtEstado.Text,
                    Lectura = txtLectura.Text,
                    LecturaAnterior = txtLecturaAnterior.Text,
                    Ruta = txtRuta.Text,
                    Secuencia = txtSecuencia.Text
                };

                await App.SQLiteDB.GuardarUsuarioAsync(usuario);
            }
            await DisplayAlert("Registro", "Se ha actualizado con exito el registro", "Ok");

            LimpiarControles();

            txtIdUsuario.IsVisible = false;
            btnActualizar.IsVisible = false;
            btnRegistar.IsVisible = true;
            LlenarDatos();
        }

        private async void lstUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Usuario)e.SelectedItem;
            btnRegistar.IsVisible = false;
            txtIdUsuario.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;

            if (string.IsNullOrEmpty(obj.Id.ToString()))
            {
                var usuario = await App.SQLiteDB.GetUsuarioById(obj.Id);
                if (usuario !=null)
                {

                    txtIdUsuario.Text = usuario.Id.ToString();
                    txtNombre.Text = usuario.Nombre;
                    txtEstado.Text = usuario.Estado;
                    txtLectura.Text = usuario.Lectura;
                    txtLecturaAnterior.Text = usuario.LecturaAnterior;
                    txtRuta.Text = usuario.Ruta;
                    txtSecuencia.Text = usuario.Secuencia;



                }
            }
        }

        private  async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var usuario = await App.SQLiteDB.GetUsuarioById(Convert.ToInt32(txtIdUsuario.Text));

            if (usuario != null)
            {
                await App.SQLiteDB.DeleteUsuarioAsync(usuario);
                await DisplayAlert("Estado", "Se ha eliminado con éxito", "Ok");

                LimpiarControles();
                LlenarDatos();

                txtIdUsuario.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                btnRegistar.IsVisible = true;
            }
        }

        public void LimpiarControles()
        {
            txtIdUsuario.Text = "";
            txtNombre.Text = "";
            txtEstado.Text = "";
            txtLectura.Text = "";
            txtLecturaAnterior.Text = "";
            txtRuta.Text = "";
            txtSecuencia.Text = "";

        }
    }
}
