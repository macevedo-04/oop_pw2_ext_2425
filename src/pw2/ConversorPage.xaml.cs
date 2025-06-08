using Microsoft.Maui.Controls;
using pw2.Guided_Practice;
namespace pw2;

public partial class ConversorPage : ContentPage, IQueryAttributable
{
    private string currentUsername;
    public ConversorPage()
    {
        InitializeComponent();
        AssignButtonEvents();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("username"))
            this.currentUsername= query["username"].ToString();
    }

    private void AssignButtonEvents()
    {
        Button0.Clicked += ButtonNumber_Clicked;
        Button1.Clicked += ButtonNumber_Clicked;
        Button2.Clicked += ButtonNumber_Clicked;
        Button3.Clicked += ButtonNumber_Clicked;
        Button4.Clicked += ButtonNumber_Clicked;
        Button5.Clicked += ButtonNumber_Clicked;
        Button6.Clicked += ButtonNumber_Clicked;
        Button7.Clicked += ButtonNumber_Clicked;
        Button8.Clicked += ButtonNumber_Clicked;
        Button9.Clicked += ButtonNumber_Clicked;

        ButtonA.Clicked += ButtonNumber_Clicked;
        ButtonB.Clicked += ButtonNumber_Clicked;
        ButtonC.Clicked += ButtonNumber_Clicked;
        ButtonD.Clicked += ButtonNumber_Clicked;
        ButtonE.Clicked += ButtonNumber_Clicked;
        ButtonF.Clicked += ButtonNumber_Clicked;

        ButtonAC.Clicked += ButtonAC_Clicked;
        ButtonSign.Clicked += ButtonSign_Clicked;

        ButtonDecToBin.Clicked += ButtonDecToBin_Clicked;
        ButtonDecToTwoComp.Clicked += ButtonDecToTwoComp_Clicked;
        ButtonDecToOct.Clicked += ButtonDecToOct_Clicked;
        ButtonDecToHex.Clicked += ButtonDecToHex_Clicked;
        ButtonBinToDec.Clicked += ButtonBinToDec_Clicked;
        ButtonTwoCompToDec.Clicked += ButtonTwoCompToDec_Clicked;
        ButtonOctToDec.Clicked += ButtonOctToDec_Clicked;
        ButtonHexToDec.Clicked += ButtonHexToDec_Clicked;
    }

    private void IncrementNumOperations()
    {
        string filePath = "files/users.csv";
        if (File.Exists(filePath)) {
            string[] lines = File.ReadAllLines(filePath);
            bool found = false;

            for (int i = 0; i < lines.Length; i++) {
                string[] fields = lines[i].Split(';');
                if (fields[1] == this.currentUsername) {
                int newNumOp = Convert.ToInt32(fields[4]);
                newNumOp++;
                fields[4] = newNumOp.ToString();
                lines[i] = string.Join(";", fields);
                found = true;}
            }

            if (found)
                File.WriteAllLines(filePath, lines);
        }
    }

    private void ButtonNumber_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        InputEntry.Text += button.Text;
    }

    private void ButtonAC_Clicked(object sender, EventArgs e)
    {
        InputEntry.Text = "";
        OutputLabel.Text = "";
    }

    private void ButtonSign_Clicked(object sender, EventArgs e)
    {
        if (InputEntry.Text != "") {
            if (InputEntry.Text.StartsWith("-"))
                InputEntry.Text = InputEntry.Text.Substring(1);
            else
                InputEntry.Text = "-" + InputEntry.Text;
        }
    }

    private async void ButtonDecToBin_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(InputEntry.Text)) {
            OutputLabel.Text = "";
            try {
                int bits = 0;
                string bitsInput;
                do {
                    bitsInput = await DisplayPromptAsync("Bits", "How many bits should I use?", "OK", "Cancel", "Enter number of bits", 3, Keyboard.Numeric);
                    if (bitsInput == null)
                        return;
                } while (bitsInput == "" || !int.TryParse(bitsInput, out bits) || bits <= 0);
                DecimalToBinary converter = new DecimalToBinary("Binary", "Decimal to Binary");
                converter.Validate(InputEntry.Text);
                string binaryNumber = converter.Change(InputEntry.Text, bits);
                OutputLabel.Text = binaryNumber;
                IncrementNumOperations();
            }
            catch (FormatException ex) {
                InputEntry.Text = "";
                await DisplayAlert("ERROR", $"{ex.Message}", "OK");
            }
            catch (Exception ex) {
                InputEntry.Text = "";
                await DisplayAlert("ERROR", $"{ex.Message}", "OK");
            }
        }
        else 
            OutputLabel.Text = "The entry cannot be empty.";
    }

    private async void ButtonDecToTwoComp_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(InputEntry.Text)) {
            OutputLabel.Text = "";
            try {
                int bits = 0;
                string bitsInput;
                do {
                    bitsInput = await DisplayPromptAsync("Bits", "How many bits should I use?", "OK", "Cancel", "Enter number of bits", 3, Keyboard.Numeric);
                    if (bitsInput == null)
                        return;
                } while (bitsInput == "" || !int.TryParse(bitsInput, out bits) || bits <= 0);
                DecimalToTwosComplement converter = new DecimalToTwosComplement("Two's Complement", "Decimal to Two's Complement");
                converter.Validate(InputEntry.Text);
                string twoCompNumber = converter.Change(InputEntry.Text, bits);
                OutputLabel.Text = twoCompNumber;
                IncrementNumOperations();
            }
            catch (FormatException ex) {
                InputEntry.Text = "";
                await DisplayAlert("ERROR", $"{ex.Message}", "OK");
            }
            catch (Exception ex) {
                InputEntry.Text = "";
                await DisplayAlert("ERROR", $"{ex.Message}", "OK");
            }
        }
        else 
            OutputLabel.Text = "The entry cannot be empty.";
    }

    private async void ButtonDecToOct_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(InputEntry.Text)) {
            OutputLabel.Text = "";
            try {
                DecimalToOctal converter = new DecimalToOctal("Octal", "Decimal to Octal");
                converter.Validate(InputEntry.Text);
                string octalNumber = converter.Change(InputEntry.Text);
                OutputLabel.Text = octalNumber;
                IncrementNumOperations();
            }
            catch (FormatException ex) {
                InputEntry.Text = "";
                await DisplayAlert("ERROR", $"{ex.Message}", "OK");
            }
            catch (Exception ex) {
                InputEntry.Text = "";
                await DisplayAlert("ERROR", $"{ex.Message}", "OK");
            }
        }
        else 
            OutputLabel.Text = "The entry cannot be empty.";
    }

    private async void ButtonTwoCompToDec_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(InputEntry.Text)) {
            OutputLabel.Text = "";
            try {
                TwosComplementToDecimal converter = new TwosComplementToDecimal("Decimal", "Two's Complement to Decimal");
                converter.Validate(InputEntry.Text);
                string decimalNumber = converter.Change(InputEntry.Text);
                OutputLabel.Text = decimalNumber;
                IncrementNumOperations();
            }
            catch (FormatException ex) {
                InputEntry.Text = "";
                await DisplayAlert("ERROR", $"{ex.Message}", "OK");
            }
            catch (Exception ex) {
                InputEntry.Text = "";
                await DisplayAlert("ERROR", $"{ex.Message}", "OK");
            }
        }
        else 
            OutputLabel.Text = "The entry cannot be empty.";
    }

    private async void ButtonDecToHex_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(InputEntry.Text)) {
            OutputLabel.Text = "";
            try {
                DecimalToHexadecimal converter = new DecimalToHexadecimal("Hexadecimal", "Decimal to Hexadecimal");
                converter.Validate(InputEntry.Text);
                string hexNumber = converter.Change(InputEntry.Text);
                OutputLabel.Text = hexNumber;
                IncrementNumOperations();
            }
            catch (FormatException ex) {
                InputEntry.Text = "";
                await DisplayAlert("ERROR", $"{ex.Message}", "OK");
            }
            catch (Exception ex) {
                InputEntry.Text = "";
                await DisplayAlert("ERROR", $"{ex.Message}", "OK");
            }
        }
        else 
            OutputLabel.Text = "The entry cannot be empty.";
    }

    private async void ButtonBinToDec_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(InputEntry.Text)) {
            OutputLabel.Text = "";
            try {
                BinaryToDecimal converter = new BinaryToDecimal("Decimal", "Binary to Decimal");
                converter.Validate(InputEntry.Text);
                string decimalNumber = converter.Change(InputEntry.Text);
                OutputLabel.Text = decimalNumber;
                IncrementNumOperations();
            }
            catch (FormatException ex) {
                InputEntry.Text = "";
                await DisplayAlert("ERROR", $"{ex.Message}", "OK");
            }
            catch (Exception ex) {
                InputEntry.Text = "";
                await DisplayAlert("ERROR", $"{ex.Message}", "OK");
            }
        }
        else 
            OutputLabel.Text = "The entry cannot be empty.";
    }

    private async void ButtonOctToDec_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(InputEntry.Text)) {
            OutputLabel.Text = "";
            try {
                OctalToDecimal converter = new OctalToDecimal("Decimal", "Octal to Decimal");
                converter.Validate(InputEntry.Text);
                string decimalNumber = converter.Change(InputEntry.Text);
                OutputLabel.Text = decimalNumber;
                IncrementNumOperations();
            }
            catch (FormatException ex) {
                InputEntry.Text = "";
                await DisplayAlert("ERROR", $"{ex.Message}", "OK");
            }
            catch (Exception ex) {
                InputEntry.Text = "";
                await DisplayAlert("ERROR", $"{ex.Message}", "OK");
            }
        }
        else 
            OutputLabel.Text = "The entry cannot be empty.";
    }

    private async void ButtonHexToDec_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(InputEntry.Text)) {
            OutputLabel.Text = "";
            try {
                HexadecimalToDecimal converter = new HexadecimalToDecimal("Decimal", "Hexadecimal to Decimal");
                converter.Validate(InputEntry.Text);
                string decimalNumber = converter.Change(InputEntry.Text);
                OutputLabel.Text = decimalNumber;
                IncrementNumOperations();
            }
            catch (FormatException ex) {
                InputEntry.Text = "";
                await DisplayAlert("ERROR", $"{ex.Message}", "OK");
            }
            catch (Exception ex) {
                InputEntry.Text = "";
                await DisplayAlert("ERROR", $"{ex.Message}", "OK");
            }
        }
        else 
            OutputLabel.Text = "The entry cannot be empty.";
    }

    private async void OnOperationsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(UserInfoPage)}?username={this.currentUsername}");
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