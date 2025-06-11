using System.Text.RegularExpressions;
namespace pw2;

public partial class PasswordRecoveryPage : ContentPage
{
    public PasswordRecoveryPage()
    {
        InitializeComponent();
    }

    private async void OnRecoverClicked(object sender, EventArgs e) //Changes the password for the user if the username matches an entry in the users.csv file
    {
        string filePath = "files/users.csv";
        bool found = false;

        if (string.IsNullOrEmpty(UsernameEntry.Text) || string.IsNullOrEmpty(NewPasswordEntry.Text) || string.IsNullOrEmpty(ConfirmPasswordEntry.Text)) {
            await DisplayAlert("Error", "Please fill all fields.", "OK");
            return;
        }

        if (NewPasswordEntry.Text != ConfirmPasswordEntry.Text) {
            await DisplayAlert("Error", "Passwords do not match.", "OK");
            return;
        }

        string passwordError = GetPasswordFormatError(NewPasswordEntry.Text);
        if (passwordError != null) {
            await DisplayAlert("Error", passwordError, "OK");
            return;
        }

        try {
            if (File.Exists(filePath)) {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; i++) {
                    string[] fields = lines[i].Split(';');
                    if (fields[1] == UsernameEntry.Text) {
                        if (fields[3] == NewPasswordEntry.Text) {
                            await DisplayAlert("Warning", "The new password matches the current password for this account.", "OK");
                            return;
                        }
                        fields[3] = NewPasswordEntry.Text;
                        lines[i] = string.Join(";", fields);
                        found = true;
                    }
                }

                if (found) {
                    File.WriteAllLines(filePath, lines);
                    await DisplayAlert("Success", "Password changed successfully.", "OK");
                }
                else
                    await DisplayAlert("Error", "Username not found.", "OK");
            }
            else
                await DisplayAlert("Error", "User data file not found.", "OK");
        }
        catch (IOException ex) {
            await DisplayAlert("Error", $"An I/O error occurred: {ex.Message}", "OK");
        }
        catch (Exception ex) {
            await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
        }
    }

    private string GetPasswordFormatError(string password) // Validates the password format
    {
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasNumber = new Regex(@"[0-9]+");
        var hasSpecialChar = new Regex(@"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]+");

        if (password.Length < 8 || !hasUpperChar.IsMatch(password) || !hasLowerChar.IsMatch(password) || !hasNumber.IsMatch(password) || !hasSpecialChar.IsMatch(password))
            return "Password must be at least 8 characters and contain upper, lower, number, and special character.";

        return null;
    }

    private async void OnBackClicked(object sender, EventArgs e) //Navigates back to the LoginPage
    {
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }    

    private void OnExitClicked(object sender, EventArgs e) //Exits the application
    {
        Environment.Exit(0);
    }
}