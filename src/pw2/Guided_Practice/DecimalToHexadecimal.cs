using System;

namespace pw2.Guided_Practice
{
    public class DecimalToHexadecimal : Conversion
    {
        public DecimalToHexadecimal(string name, string definition) : base(name, definition, new DecimalInputValidator()) {}        
        public override string Change(string input)
        {
            int number = Convert.ToInt32(input);
            
            char[] hexChars = "0123456789ABCDEF".ToCharArray();

            if (number == 0) return "0";

            string hexString = "";

            while (number > 0){
                int remainder = number % 16;
                hexString = hexChars[remainder] + hexString;
                number /= 16;
            }
            return hexString;
        }
    }
}