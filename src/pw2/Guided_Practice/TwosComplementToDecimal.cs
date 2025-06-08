using System;

namespace pw2.Guided_Practice
{
    public class TwosComplementToDecimal : Conversion
    {
        public TwosComplementToDecimal(string name, string definition) : base(name, definition, new BinaryInputValidator()) {}
        public override string Change(string input)
        {
            int number = Convert.ToInt32(input);

            //Get the length of the string and check the sign 
            int n = input.Length;
            bool isNegative = input[0] == '1';

            int result = 0;

            if (!isNegative){ //If it's positive
                for (int i = 0; i < n; i++){
                    int bit = input[i] - '0';
                    result += bit * (int)Math.Pow(2, n - i - 1); //We calculate normally with powers of 2 for each bit
                }
            } else { //If it's negative
                char[] inverted = new char[n];
                for (int i = 0; i < n; i++)
                    inverted[i] = input[i] == '0' ? '1' : '0'; //We flip the bits
                
                for (int i = n - 1; i >= 0; i--){ //We add 1
                    if (inverted[i] == '0'){ //We set the first 0 to 1
                        inverted[i] = '1';
                        break;
                    } else
                        inverted[i] = '0';
                }
                for (int i = 0; i < n; i++){
                    int bit = inverted[i] - '0';
                    result += bit * (int)Math.Pow(2, n - i - 1); //We calculate normally with powers of 2 for each bit
                }
                result *= -1; //We negate the result 
            }
            return result.ToString(); //We convert the result to string
        }
    }
}