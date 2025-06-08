using Microsoft.Maui.Controls;
namespace pw2;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();

        UsernameEntry.Text = "";
        PasswordEntry.Text = "";
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        string filePath = "files/users.csv";
        bool found = false;

        if (string.IsNullOrEmpty(UsernameEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text))
        {
            await DisplayAlert("Login Failed", "Please enter both username and password.", "OK");
            return;
        }

        try
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    string[] fields = line.Split(';');
                    if (fields[1] == UsernameEntry.Text && fields[3] == PasswordEntry.Text)
                        found = true;
                }
            }
            else
            {
                await DisplayAlert("File Missing", "User data file not found.", "OK");
                return;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred while accessing user data:\n{ex.Message}", "OK");
            return;
        }

        if (found)
            await Shell.Current.GoToAsync($"{nameof(ConversorPage)}?username={UsernameEntry.Text}");
        else
            await DisplayAlert("Login Failed", "Account does not exist or password is incorrect.", "OK");
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }

    private async void OnForgotPasswordClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PasswordRecoveryPage));
    }

    private void OnExitClicked(object sender, EventArgs e)
    {
        Environment.Exit(0);
    }
}