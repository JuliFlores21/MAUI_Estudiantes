using ApiColegioPagos.Models;
using MAUI_Estudiantes.Services;

namespace MAUI_Estudiantes;

public partial class PagoPension : ContentPage
{
    private Estudiante ingreso;
    private readonly ApiService _APIService;
    public PagoPension(Estudiante estudiante)
	{
		InitializeComponent();
        ingreso=estudiante;
	}

    private async void OnClickRegresarMenu(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }

    private async void OnClickPagarPension(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new DatoTarjeta());
    }
}