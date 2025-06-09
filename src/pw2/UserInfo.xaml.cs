namespace pw2;

public partial class UserInfoPage : ContentPage, IQueryAttributable
{
    private string currentUsername;
    public UserInfoPage()
    {
        InitializeComponent();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("username"))
            this.currentUsername = query["username"].ToString();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        try {
            string filePath = "files/users.csv";
            string opsFilePath = "files/operations.csv";

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
            Console.WriteLine("An I/O error occurred: " + ex.Message);
        }
        catch (Exception ex) {
            Console.WriteLine("An unexpected error occurred: " + ex.Message);
        }
    }

    private async void OnCalculatorClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(ConversorPage)}?username={currentUsername}");
    }

    private async void OnLogOutClicked(object sender, EventArgs e)
    {
        this.currentUsername = "";
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }

    private void OnExitClicked(object sender, EventArgs e)
    {
        Environment.Exit(0);
    }
}