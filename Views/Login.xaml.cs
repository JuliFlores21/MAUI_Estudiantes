namespace MAUI_Estudiantes;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private async void OnLoginClicked(object sender, EventArgs e)
    {
		await Navigation.PushModalAsync(new Menu());
    }
}