namespace MAUI_Estudiantes;

public partial class Cuenta : ContentPage
{
	public Cuenta()
	{
		InitializeComponent();
	}

    private async void OnClickRegresar(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }

    private async void OnClickNuevaContrasenia(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ContraseniaNueva());
    }
}