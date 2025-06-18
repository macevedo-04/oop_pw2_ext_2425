namespace pw2;

public partial class UserInfoPage : ContentPage, IQueryAttributable
{
    private string currentUsername;
    public UserInfoPage()
    {
        InitializeComponent();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query) //Sets the current username from the query parameters
    {
        if (query.ContainsKey("username"))
            this.currentUsername = query["username"].ToString();
    }

    protected async override void OnAppearing() // Loads user information and operations from operations.csv when the page appears
    {
        base.OnAppearing();

        try {
            string dir = Path.Combine(FileSystem.AppDataDirectory, "files");
            Directory.CreateDirectory(dir);
            string filePath = Path.Combine(dir, "users.csv");
            string opsFilePath = Path.Combine(dir, "operations.csv");

            List<string> userOps = new List<string>();

            if (File.Exists(opsFilePath)) {
                string[] lines = File.ReadAllLines(opsFilePath);

                foreach (string line in lines) {
                    string[] fields = line.Split(';');

                    if (fields.Length == 5) {
                        if (fields[0] == currentUsername && fields[2] != "0") {
                            string formatted = " • " + fields[3] + ": " + fields[1] + " → " + fields[4] + " (bits: " + fields[2] + ")";
                            userOps.Add(formatted);
                        }
                        else if (fields[0] == currentUsername) {
                            string formatted = " • " + fields[3] + ": " + fields[1] + " → " + fields[4];
                            userOps.Add(formatted);
                        }
                    }
                }
            }

            OperationsListView.ItemsSource = userOps;

            if (File.Exists(filePath)) {
                string[] lines = File.ReadAllLines(filePath);
                bool userFound = false;

                foreach (string line in lines) {
                    string[] fields = line.Split(';');

                    if (!userFound && fields.Length == 5 && fields[1] == currentUsername) {
                        NameLabel.Text = "Name: " + fields[0];
                        UsernameLabel.Text = "Username: " + fields[1];
                        EmailLabel.Text = "Email: " + fields[2];
                        PasswordLabel.Text = "Password: " + fields[3];
                        NumOperationsLabel.Text = "Number of operations: " + fields[4];

                        userFound = true;
                    }
                }
            }
        }
        catch (IOException ex) {
            await DisplayAlert("Error", $"An I/O error occurred while accessing user data: {ex.Message}", "OK");
        }
        catch (Exception ex) {
            await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
        }
    }

    private async void OnCalculatorClicked(object sender, EventArgs e) //Navigates to the ConversorPage with the current username as a query parameter
    {
        await Shell.Current.GoToAsync($"{nameof(ConversorPage)}?username={currentUsername}");
    }

    private async void OnLogOutClicked(object sender, EventArgs e) //Logs the user out by clearing the current username and navigating to the LoginPage
    {
        this.currentUsername = "";
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }

    private void OnExitClicked(object sender, EventArgs e) //Exits the application
    {
        Environment.Exit(0);
    }
}