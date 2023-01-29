using MobileArts.api.Domain.Models;
using MobileArts.api.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileArts.api.Persistence.Utilities
{
    public  class GenerateCodeUtility : IGenerateCodeService
    {
        private char[] LettersArray;
        private char[] NumbersArray;
        private readonly GenerateCodeKeys _generateCodeKeys;
      
        public GenerateCodeUtility(IConfiguration Configuration)
        {
            _generateCodeKeys = Configuration.GetSection("GenerateCodeKeys").Get<GenerateCodeKeys>();
        }


        private string KeyLetters { get; set; }

        private string KeyNumbers { get; set; }

        private int KeyChars { get; set; }


        private string Generate()
        {
            int i_key;
            float Random1;
            Int16 arrIndex;
            StringBuilder sb = new StringBuilder();
            string RandomLetter; 

            LettersArray = KeyLetters.ToCharArray();
            NumbersArray = KeyNumbers.ToCharArray();

            for (i_key = 1; i_key <= KeyChars; i_key++)
            { 

                VBMath.Randomize();
                Random1 = VBMath.Rnd();
                arrIndex = -1; 

                if ((System.Convert.ToInt32(Random1 * 123)) % 2 == 0)
                { 

                    while (arrIndex < 0)
                        arrIndex = Convert.ToInt16(LettersArray.GetUpperBound(0)
                         * Random1);
                    RandomLetter = LettersArray[arrIndex].ToString(); 

                    if ((System.Convert.ToInt32(arrIndex * Random1 * 99)) % 2 != 0)
                    {
                        RandomLetter = LettersArray[arrIndex].ToString();
                        RandomLetter = RandomLetter.ToUpper();
                    }
                    sb.Append(RandomLetter);
                }
                else
                { 
                    while (arrIndex < 0)
                        arrIndex = Convert.ToInt16(NumbersArray.GetUpperBound(0)
                          * Random1);
                    sb.Append(NumbersArray[arrIndex]);
                }
            }
            return sb.ToString();
        }
        public string generatCode(int KeyChars)
        {
            //GenerateCodeUtility KeyGen;

            //KeyGen = new GenerateCodeUtility();

            this.KeyLetters = _generateCodeKeys.KeyLetters;
            this.KeyNumbers = _generateCodeKeys.KeyNumbers;
            this.KeyChars = KeyChars;

            return this.Generate().ToString();
        }
    }

}

