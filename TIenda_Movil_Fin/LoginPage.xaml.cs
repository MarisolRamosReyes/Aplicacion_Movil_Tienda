namespace TIenda_Movil_Fin;

public partial class LoginPage : ContentPage
{
    // Credenciales fijas
    private const string FixedUsername = "admin";
    private const string FixedPassword = "12345";

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string enteredUsername = UserNameEntry.Text;
        string enteredPassword = PasswordEntry.Text; 

        if (enteredUsername == FixedUsername && enteredPassword == FixedPassword)
        {
            UserNameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            await Navigation.PushAsync(new HomePage());
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contraseņa incorrectos", "OK");
        }
    }
}