using ApiColegioPagos.Models;

namespace MAUI_Estudiantes;

public partial class Cuenta : ContentPage
{
    private Estudiante ingreso;
	public Cuenta(Estudiante estudiante)
	{
		InitializeComponent();
        ingreso = estudiante;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ID.Text = ingreso.Est_id.ToString();
        Cedula.Text= ingreso.Est_cedula.ToString();
        Direccion.Text=ingreso.Est_direccion.ToString();
    }

    private async void OnClickRegresar(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }

    private async void OnClickNuevaContrasenia(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ContraseniaNueva(ingreso));
    }
}