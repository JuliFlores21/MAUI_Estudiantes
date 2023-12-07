namespace MAUI_Estudiantes;

public partial class PagoPension : ContentPage
{
	public PagoPension()
	{
		InitializeComponent();
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