namespace Gestion_de_Tareas.Views
{
    public partial class LoginPage : ContentPage
    {
        // Credenciales quemadas
        private const string UsuarioValido = "admin";
        private const string PasswordValida = "1234";

        public LoginPage()
        {
            InitializeComponent();
            BtnLogin.Clicked += OnLoginClicked;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string usuario = EntryUsuario.Text?.Trim();
            string password = EntryPassword.Text?.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                LblError.Text = "⚠️ Por favor llena todos los campos";
                LblError.IsVisible = true;
                return;
            }

            if (usuario == UsuarioValido && password == PasswordValida)
            {
                LblError.IsVisible = false;
                // Navega al app principal y quita el Login del stack
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                LblError.Text = "❌ Usuario o contraseña incorrectos";
                LblError.IsVisible = true;
                EntryPassword.Text = string.Empty;
            }
        }
    }
}
