using ApiColegioPagos.Models;
using MAUI_Estudiantes.Services;
using System.Collections.ObjectModel;

namespace MAUI_Estudiantes;

public partial class ListaPagos : ContentPage
{
	private Estudiante ingreso;
    private readonly ApiService _APIService;
    public ListaPagos(Estudiante estudiante, ApiService apiService)
	{
		InitializeComponent();
		ingreso=estudiante;
        _APIService = apiService;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            List<Pago> listapagos = await _APIService.GetPagosEstudiante(ingreso.Est_id);
            var pagos = new ObservableCollection<Pago>(listapagos);
            pagosListView.ItemsSource = pagos;
        }
        catch(Exception ex) {
            await DisplayAlert("Error", "El estudiante no ha realizado ningún pago", "Ok");
            await Navigation.PopModalAsync();

        }

    }

    private async void OnClickRegresarMenu(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }
}