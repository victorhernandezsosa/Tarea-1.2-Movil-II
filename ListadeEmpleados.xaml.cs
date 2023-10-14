using System.Collections.ObjectModel;
using Tarea_1._2.Controller;

namespace Tarea_1._2;


public partial class ListadeEmpleados : ContentPage
{
    EmpleBase db;
    ObservableCollection<Empleado> empleados;

    public ListadeEmpleados()
	{
		InitializeComponent();

        db = new EmpleBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "empleados.db3"));
        empleados = new ObservableCollection<Empleado>();
        listaemple.ItemsSource = empleados;

        listaemple.ItemTapped += async (sender, e) =>
        {
            if (e.Item is Empleado empleado)
            {
                bool confirmarBorrado = await DisplayAlert("Confirmación", $"¿Desea eliminar a {empleado.completo}?", "Sí", "No");

                if (confirmarBorrado)
                {
                    await db.DeleteEmpleado(empleado.Id);

                    await refrescar();
                }
            }

            ((ListView)sender).SelectedItem = null;
        };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await refrescar();
    }

    private async Task refrescar()
    {
        var empleadosbase = await db.GetEmpleadoAsync();

        empleados.Clear();

        foreach(var empleado in empleadosbase)
        {
            empleados.Add(empleado);
        }
    }
}