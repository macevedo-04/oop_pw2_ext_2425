using System;

namespace pw2.Guided_Practice
{
    public class DecimalToOctal : Conversion
    {
        public DecimalToOctal(string name, string definition) : base(name, definition, new DecimalInputValidator()) {}
        public override string Change(string input)
        {
            int number = Convert.ToInt32(input);

            if (number == 0) return "0";

            string octalString = "";

            while (number > 0){
                int remainder = number % 8;
                octalString = remainder + octalString;
                number /= 8;
            }
            return octalString;
        }
    }
}