namespace MAUI_Estudiantes;

public partial class ListaPagos : ContentPage
{
	public ListaPagos()
	{
		InitializeComponent();
	}

    private async void OnClickRegresarMenu(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }
}