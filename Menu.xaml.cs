namespace MAUI_Estudiantes;

public partial class Menu : ContentPage
{
	public Menu()
	{
		InitializeComponent();
	}

    private async void OnClickListaPagos(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ListaPagos());
    }

    private async void OnClickCuenta(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new Cuenta());
    }

    private async void OnClickSalir(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void OnClickPagoPension(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new PagoPension());
    }
}