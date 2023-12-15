using ApiColegioPagos.Models;
using MAUI_Estudiantes.Services;

namespace MAUI_Estudiantes;

public partial class DatoTarjeta : ContentPage
{
    private Estudiante ingreso;
    private readonly ApiService _APIService;
    private Global sistema;
    private Pago pagoActual;
    private Pension pension;
    public DatoTarjeta(Estudiante estudiante, ApiService apiService, Global global, Pago pago)
	{
		InitializeComponent();
        ingreso=estudiante;
        _APIService = apiService;
        sistema = global;
        pagoActual= pago;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        cuotaSistema.Text=sistema.Glo_valor.ToString();
        cuotaEstudiante.Text=pagoActual.Pag_cuota.ToString();
        pension = await _APIService.GetPension(pagoActual.Pension);
        int diferencia = sistema.Glo_valor - pagoActual.Pag_cuota;
        float total = 0;
        if (diferencia < 0)
        {
            diferencia=0;
        }
        if (pension != null)
        {
             total = pension.Pen_valor * diferencia;
        }
        montoPagar.Text=total.ToString();
        numeroCuota.Text=diferencia.ToString();
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

        if (numT.Length < 10)
        {
            await DisplayAlert("Error", "El número de tarjeta debe tener 16 dígitos", "Ok");
            return;
        }

        if(codigoT.Length < 3)
        {
            await DisplayAlert("Error", "El código de seguridad debe tener 3 dígitos", "Ok");
            return;
        }

        if(diferencia <= 0)
        {
            await DisplayAlert("Notificacion", "Usted está al día en el pago de las cuotas", "Ok");
            await Navigation.PopModalAsync();
        }else if (diferencia < cuotaP)
        {
            await DisplayAlert("Error", "No puede ingresar un número de cuota mayor a las pendientes", "Ok");
            return;
        }else if (cuotaP == 0)
        {
            await DisplayAlert("Error", "Ingrese un valor mayor a 0", "Ok");
            return;
        }
        else
        {
            bool confirmarPago = await DisplayAlert("Confirmación", "¿Desea proceder con el pago?", "Sí", "No");
            if (confirmarPago)
            {
                List<Pago> pagos = await _APIService.pagar(ingreso.Est_id, cuotaP);
                await DisplayAlert("Aprobado", "Se registró correctamente su transacción", "Ok");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Notificación", "Transacción rechazada", "Ok");
                await Navigation.PopModalAsync();
            }
        }
    }
}