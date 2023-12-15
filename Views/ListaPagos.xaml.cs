using ApiColegioPagos.Models;
using MAUI_Estudiantes.Services;
using MAUI_Estudiantes.ViewModel;
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
            List<Pension> listaPension = new List<Pension>();
            foreach(Pago pago in listapagos)
            {
                Pension p = await _APIService.GetPension(pago.Pension);
                if (p != null)
                {
                    listaPension.Add(p);
                }
            }
            List<PagoPensionViewModel> listaCombinada = new List<PagoPensionViewModel>();
            foreach (Pago pago in listapagos)
            {
                Pension pension = listaPension.FirstOrDefault(p => p.Pen_id == pago.Pension);
                if (pension != null)
                {
                    PagoPensionViewModel viewModel = new PagoPensionViewModel
                    {
                        Pago = pago,
                        Pension = pension
                    };
                    listaCombinada.Add(viewModel);
                }
            }
            var pagos = new ObservableCollection<PagoPensionViewModel>(listaCombinada);
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