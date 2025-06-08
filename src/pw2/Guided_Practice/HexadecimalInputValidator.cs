using System;

namespace pw2.Guided_Practice
{
    public class HexadecimalInputValidator : InputValidator
    {
        public override void Validate(string input)
        {
            for (int i = 0; i < input.Length; i++) {
                char c = input[i];
                bool isDigit = c >= '0' && c <= '9';
                bool isUpperHex = c >= 'A' && c <= 'F';
                bool isLowerHex = c >= 'a' && c <= 'f';

                if (!isDigit && !isUpperHex && !isLowerHex)
                    throw new FormatException("Bad format: input is not a valid hexadecimal number.");
            }
        }
    }
}