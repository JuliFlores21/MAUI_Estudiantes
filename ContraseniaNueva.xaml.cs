namespace MAUI_Estudiantes;

public partial class ContraseniaNueva : ContentPage
{
	public ContraseniaNueva()
	{
		InitializeComponent();
	}

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}