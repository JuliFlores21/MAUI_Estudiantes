using ApiColegioPagos.Models;
using MAUI_Estudiantes.Services;

namespace MAUI_Estudiantes;

public partial class Cuenta : ContentPage
{
    private Estudiante ingreso;
    private readonly ApiService _ApiService;
	public Cuenta(Estudiante estudiante, ApiService apiservice)
	{
		InitializeComponent();
        ingreso = estudiante;
        _ApiService = apiservice;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Nombre.Text= ingreso.Est_nombre.ToString();
        ID.Text = ingreso.Est_id.ToString();
        Cedula.Text= ingreso.Est_cedula.ToString();
        Direccion.Text=ingreso.Est_direccion.ToString();
    }

    private async void OnClickRegresar(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }

    private async void OnClickNuevaContrasenia(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ContraseniaNueva(ingreso, _ApiService));
    }
}