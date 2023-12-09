using ApiColegioPagos.Models;
using MAUI_Estudiantes.Services;

namespace MAUI_Estudiantes;

public partial class ContraseniaNueva : ContentPage
{
    private Estudiante ingreso;
    private readonly ApiService _APIService;

    public ContraseniaNueva(Estudiante estudiante)
	{
		InitializeComponent();
        ingreso=estudiante;
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
        if(contr1.Equals("") || contr2.Equals(""))
        {
            await DisplayAlert("Error", "Llene todos los campos", "Ok");
        }else if (contr1.Equals(contr2))
        {
            bool contraseniaNueva = await _APIService.CambioContrasenia(ingreso, contr1);
            if(contraseniaNueva)
            {
                await DisplayAlert("Aprobado", "Su contraseña se cambió con éxito", "Ok");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Hubo un error en el sistema, intente nuevamente", "Ok");
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