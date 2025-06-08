using System;

namespace pw2.Guided_Practice
{
    public class DecimalInputValidator : InputValidator
    {
        public override void Validate(string input)
        {
            for (int i = 0; i < input.Length; i++) {
                if (input[i] == '-' && i > 0)
                    throw new FormatException("Bad format: input is not a valid integer.");
                else if (!char.IsDigit(input[i]))
                    throw new FormatException("Bad format: input is not a valid integer.");
            }
        }
    }   
}