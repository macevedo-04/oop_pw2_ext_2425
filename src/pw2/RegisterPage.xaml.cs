using Microsoft.Maui.Controls;
using System.Text.RegularExpressions;
namespace pw2;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e) //Registers the user if the username does not already exist in the users.csv file and all fields are valid
    {
        string dir = Path.Combine(FileSystem.AppDataDirectory, "files");
        Directory.CreateDirectory(dir);
        string filePath = Path.Combine(dir, "users.csv");
        bool correct = false;

        if (!TermsCheckBox.IsChecked) {
            await DisplayAlert("Register Failed", "You must accept the terms and conditions.", "OK");
            return;
        }

        try {
            if (File.Exists(filePath)) {
                foreach (string line in File.ReadAllLines(filePath)) {
                    string[] fields = line.Split(',');
                    if (fields.Length > 1 && fields[1] == UsernameEntry.Text) {
                        await DisplayAlert("Register Failed", "Username already exists.", "OK");
                        return;
                    }
                }
            }
        }
        catch (IOException ex) {
            await DisplayAlert("Error", $"An I/O error occurred reading user data: {ex.Message}", "OK");
            return;
        }
        catch (Exception ex) {
            await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            return;
        }

        string validationError = GetValidationError();
        if (validationError == null) {
            User user = new User(NameEntry.Text, UsernameEntry.Text, EmailEntry.Text, PasswordEntry.Text);

            try {
                user.SaveToFile();
                correct = true;
                await DisplayAlert("Success", "Account created!", "OK");
            }
            catch (IOException ex) {
                await DisplayAlert("Error", $"Failed to save user data: {ex.Message}", "OK");
                return;
            }
            catch (Exception ex) {
                await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
                return;
            }
        }
        else
            await DisplayAlert("Register Failed", validationError, "OK");

        if (correct)
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }

    private string GetValidationError() // Validates the input fields for the registration form
    {
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasNumber = new Regex(@"[0-9]+");
        var hasSpecialChar = new Regex(@"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]+");
        var validEmail = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        if (string.IsNullOrEmpty(NameEntry.Text) || string.IsNullOrEmpty(UsernameEntry.Text) || string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text) || string.IsNullOrEmpty(ConfirmPasswordEntry.Text))
            return "Please fill all fields.";
        if (NameEntry.Text == UsernameEntry.Text)
            return "Name and Username must be different.";
        if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
            return "Passwords do not match.";
        if (!validEmail.IsMatch(EmailEntry.Text))
            return "Invalid email address.";
        if (PasswordEntry.Text.Length < 8 || !hasUpperChar.IsMatch(PasswordEntry.Text) || !hasLowerChar.IsMatch(PasswordEntry.Text) || !hasNumber.IsMatch(PasswordEntry.Text) || !hasSpecialChar.IsMatch(PasswordEntry.Text))
            return "Password must be at least 8 characters and contain upper, lower, number, and special character.";

        return null;
    }

    private void OnExitClicked(object sender, EventArgs e) // Exits the application
    {
        Environment.Exit(0);
    }
}