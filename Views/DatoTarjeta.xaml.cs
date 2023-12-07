namespace MAUI_Estudiantes;

public partial class DatoTarjeta : ContentPage
{
	public DatoTarjeta()
	{
		InitializeComponent();
	}

    private async void OnClickRegresarMenu(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }
}