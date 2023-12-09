using ApiColegioPagos.Models;
using MAUI_Estudiantes.Services;

namespace MAUI_Estudiantes;

public partial class Menu : ContentPage
{
    private Estudiante ingreso;

    public Menu(Estudiante estudiante)
	{
		InitializeComponent();
        ingreso= estudiante;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Nombre.Text = ingreso.Est_nombre;
    }

    private async void OnClickListaPagos(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ListaPagos());
    }

    private async void OnClickCuenta(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new Cuenta(ingreso));
    }

    private async void OnClickSalir(object sender, EventArgs e)
    {
        bool confirmacionCerrarSesion = await DisplayAlert("Confirmación", "¿Desea cerrar sesión?", "Sí", "No");
        if (confirmacionCerrarSesion)
        {
            await Navigation.PopModalAsync();
        }     
    }

    private async void OnClickPagoPension(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new PagoPension(ingreso));
    }
}