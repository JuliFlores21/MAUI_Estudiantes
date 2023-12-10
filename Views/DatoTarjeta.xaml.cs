using ApiColegioPagos.Models;
using MAUI_Estudiantes.Services;

namespace MAUI_Estudiantes;

public partial class DatoTarjeta : ContentPage
{
    private Estudiante ingreso;
    private readonly ApiService _APIService;
    private Global sistema;
    private Pago pagoActual;
    public DatoTarjeta(Estudiante estudiante, ApiService apiService, Global global, Pago pago)
	{
		InitializeComponent();
        ingreso=estudiante;
        _APIService = apiService;
        sistema = global;
        pagoActual= pago;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        cuotaSistema.Text=sistema.Glo_valor.ToString();
        cuotaEstudiante.Text=pagoActual.Pag_cuota.ToString();
    }

    private async void OnClickRegresarMenu(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }

    private async void OnClickPagarCuota(object sender, EventArgs e)
    {
        string numT = numeroTarjeta.Text, nombreT=nombreTarjeta.Text, fechaT=fechaTarjeta.Text,
            codigoT=codigoTarjeta.Text;
        int cuotaSistema = sistema.Glo_valor, cuotaEstudiante = pagoActual.Pag_cuota;
        int diferencia = cuotaSistema - cuotaEstudiante;
        if(string.IsNullOrEmpty(numT) || string.IsNullOrEmpty(nombreT) || 
            string.IsNullOrEmpty(fechaT) || string.IsNullOrEmpty(codigoT))
        {
            await DisplayAlert("Error", "Llene todos los campos", "Ok");
            return;
        }

        if (!int.TryParse(cuotaPagar.Text, out int cuotaP))
        {
            await DisplayAlert("Error", "Seleccione un valor para la cuota", "Ok");
            return;
        }

        if (numT.Length < 16)
        {
            await DisplayAlert("Error", "El n�mero de tarjeta debe tener 16 d�gitos", "Ok");
            return;
        }

        if(codigoT.Length < 3)
        {
            await DisplayAlert("Error", "El c�digo de seguridad debe tener 3 d�gitos", "Ok");
            return;
        }

        if(diferencia == 0)
        {
            await DisplayAlert("Notificacion", "Usted est� al d�a en el pago de las cuotas", "Ok");
            await Navigation.PopModalAsync();
        }else if (diferencia < cuotaP)
        {
            await DisplayAlert("Error", "No puede ingresar una cuota mayor a la del sistema", "Ok");
            return;
        }else if (cuotaP == 0)
        {
            await DisplayAlert("Error", "Ingrese un valor mayor a 0", "Ok");
            return;
        }
        else
        {
            List<Pago> pagos = await _APIService.pagar(ingreso.Est_id, cuotaP);
            await DisplayAlert("Aprobado", "Se registr� correctamente su transacci�n", "Ok");
            await Navigation.PopModalAsync();
        }

    }
}