using System.Text.RegularExpressions;

namespace pw2
{
    public class User
    {
        private string name;
        private string username;
        private string email;
        private string password;
        private int numOperations;

        public User(string name, string username, string email, string password)
        {
            this.name = name;
            this.username = username;
            this.email = email;
            this.password = password;
            numOperations = 0;
        }

        public void SaveToFile(string filePath) //Saves the user information to the specified file path in CSV format
        {
            try {
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine($"{this.name};{this.username};{this.email};{this.password};{this.numOperations}");
                }
            }
            catch (IOException ex) {
                Console.WriteLine("An I/O error occurred while saving to file: " + ex.Message);
            }
            catch (Exception ex) {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}