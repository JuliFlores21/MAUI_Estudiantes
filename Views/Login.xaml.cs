using ApiColegioPagos.Models;
using MAUI_Estudiantes.Services;
using Newtonsoft.Json;

namespace MAUI_Estudiantes;

public partial class Login : ContentPage
{
	private readonly ApiService _APIService;
	public Login(ApiService apiservice)
	{
		InitializeComponent();
		_APIService = apiservice;
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        id.Text = string.Empty;
        contrasenia.Text = string.Empty;
        try
        {
            string storedJson = SecureStorage.GetAsync("estudiante").Result;

            if (storedJson != null)
            {
                Estudiante estudiante = JsonConvert.DeserializeObject<Estudiante>(storedJson);
                bool ingreso = await _APIService.Login(estudiante);
                if (ingreso)
                {
                    await Navigation.PushModalAsync(new Menu(estudiante, _APIService));
                }
                else
                {
                    SecureStorage.Remove("estudiante");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string contra = contrasenia.Text;

        if (string.IsNullOrEmpty(id.Text) || string.IsNullOrEmpty(contrasenia.Text))
        {
            await DisplayAlert("Error", "Llene todos los campos", "Ok");
            return;
        }

        if (!int.TryParse(id.Text.Trim(), out int ID))
        {
            await DisplayAlert("Error", "Seleccione un ID válido", "Ok");
            return;
        }

        try
        {
            Estudiante existe = await _APIService.GetEstudiante(ID);
            existe.contrasenia=contrasenia.Text;
            bool ingreso = await _APIService.Login(existe);
             if (!ingreso)
            {
                await DisplayAlert("Error", "Contraseña Incorrecta", "Ok");
                contrasenia.Text = "";
            }
            else
            {
                string estudiante = JsonConvert.SerializeObject(existe);
                SecureStorage.SetAsync("estudiante", estudiante);
                await Navigation.PushModalAsync(new Menu(existe,_APIService));
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "El usuario no existe", "Ok");
        }
    }
}