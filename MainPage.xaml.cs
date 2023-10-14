using Tarea_1._2.Controller;

namespace Tarea_1._2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Guardar_Emple(object sender, EventArgs e)
        {
            Empleado empleado = new Empleado
            {
                Nombres = txtNombre.Text,
                Apellidos = txtApellido.Text,
                FechaNacimiento = fechanac.Date,
                Correo = txtCorreo.Text
            };

            if(empleado.ValidarCorreo() )
            {
                var db = new EmpleBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "empleados.db3"));
                await db.GuardarEmpleado(empleado);
                DisplayAlert("Exito", "El empleado se ha registrado con exito", "OK");

                txtNombre.Text = string.Empty;
                txtApellido.Text = string.Empty;
                fechanac.Date = DateTime.Now;
                txtCorreo.Text = string.Empty;

            }
            else
            {
                DisplayAlert("Error", "Correo electrónico inválido.", "OK");
            }
        }

        private async void ListEmple(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListadeEmpleados());
        }
    }
}