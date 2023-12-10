using ApiColegioPagos.Models;
using MAUI_Estudiantes.Services;

namespace MAUI_Estudiantes;

public partial class Menu : ContentPage
{
    private Estudiante ingreso;
    private readonly ApiService _ApiService;

    public Menu(Estudiante estudiante, ApiService apiservice)
	{
		InitializeComponent();
        ingreso= estudiante;
        _ApiService = apiservice;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Nombre.Text = ingreso.Est_nombre;
    }

    private async void OnClickListaPagos(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ListaPagos(ingreso, _ApiService));
    }

    private async void OnClickCuenta(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new Cuenta(ingreso,_ApiService));
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
        await Navigation.PushModalAsync(new PagoPension(ingreso, _ApiService));
    }
}