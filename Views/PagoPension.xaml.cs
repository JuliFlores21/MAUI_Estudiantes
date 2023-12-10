using ApiColegioPagos.Models;
using MAUI_Estudiantes.Services;

namespace MAUI_Estudiantes;

public partial class PagoPension : ContentPage
{
    private Estudiante ingreso;
    private readonly ApiService _APIService;
    private Global sistema;
    private Pago pagoActual;
    public PagoPension(Estudiante estudiante, ApiService apiService)
	{
		InitializeComponent();
        ingreso=estudiante;
        _APIService= apiService;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        sistema = await _APIService.obtenerCuota();
        List<Pago> pagosEstudiante = await _APIService.GetPagosEstudiante(ingreso.Est_id);
        pagoActual = pagosEstudiante.Last();
        cuotaSistema.Text = sistema.Glo_valor.ToString();
        cuotaEstudiante.Text=pagoActual.Pag_cuota.ToString();
        
    }

    private async void OnClickRegresarMenu(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }

    private async void OnClickPagarPension(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new DatoTarjeta(ingreso,_APIService,sistema,pagoActual));
    }
}