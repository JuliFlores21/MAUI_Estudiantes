using MAUI_Estudiantes.Services;

namespace MAUI_Estudiantes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ApiService apiservice = new ApiService();
            MainPage = new Login(apiservice);
        }
    }
}