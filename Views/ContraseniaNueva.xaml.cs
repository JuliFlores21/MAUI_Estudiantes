using ApiColegioPagos.Models;
using MAUI_Estudiantes.Services;

namespace MAUI_Estudiantes;

public partial class ContraseniaNueva : ContentPage
{
    private Estudiante ingreso;
    private readonly ApiService _APIService;

    public ContraseniaNueva(Estudiante estudiante, ApiService apiservice)
	{
		InitializeComponent();
        ingreso=estudiante;
        _APIService= apiservice;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Contrasenia.Text= ingreso.contrasenia.ToString();
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        string contr1 = Contr1.Text;
        string contr2 = Contr2.Text;
        string actual = Contrasenia.Text;
        const int longitudMinima = 8;
        
        if (string.IsNullOrWhiteSpace(contr1) || string.IsNullOrWhiteSpace(contr2))
        {
            await DisplayAlert("Error", "Llene todos los campos", "Ok");
        }
        else if (contr1.Length < longitudMinima || contr2.Length < longitudMinima)
        {
            await DisplayAlert("Error", $"La longitud mínima de la contraseña es {longitudMinima} caracteres", "Ok");
        }
        else if (contr1.Equals(contr2))
        {
            if (contr1.Equals(actual))
            {
                await DisplayAlert("Error", "Ingrese una nueva contraseña", "Ok");
                return;
            }
            try
            {
                bool contraseniaNueva = await _APIService.CambioContrasenia(ingreso, contr1);

                if (contraseniaNueva)
                {
                    ingreso.contrasenia = contr1;
                    await DisplayAlert("Aprobado", "Su contraseña se cambió con éxito", "Ok");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un error en el sistema, intente nuevamente", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Problemas con servidor", "Ok");
            }            
        }
        else
        {
            await DisplayAlert("Error", "Las contraseñas no coinciden", "Ok");
        }
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}