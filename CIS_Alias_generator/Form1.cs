using System;
using System.Linq;
using System.Windows.Forms;

namespace CIS_Alias_generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool abbreviationBehind = false;
        bool noAbbreviation = false;

        //on Generate button click
        private void btn_generate_Click(object sender, EventArgs e)
        {
            //get input
            string input = txt_input.Text.Trim();

            //split input into string values array
            string[] separated = input.Split(null);

            //create empty string array with length of separated array without company type abbreviation
            string[] nameSeparated = new string[separated.Length];

            //create a value to hold company name
            string companyName;

            //create a value to hold output value
            string output = "";

            //generate array with company types from 1st element of input
            string[] companyTypeGenerated = companyType(separated[0], separated[separated.Length-1]);

            //full native alias value
            string fullNativeAlias = "";

            //native alias value

            string shortNativeAlias = input;

            //native company name value
            string nativeCompanyName = "";

            //transliterated company name value
            string transliteratedCompanyName;

            //transliterated company type values
            string transliteratedShortCompanyType;
            string transliteratedFullCompanyType;

            //if abbreviation is behind, change order
            if (abbreviationBehind)
            {
                nameSeparated = separated.Take(separated.Count() - 1).ToArray();
                transliteratedShortCompanyType = transliteration(separated[separated.Length - 1]);
            }
            else
            {
                nameSeparated = separated.Skip(1).ToArray();
                transliteratedShortCompanyType = transliteration(separated[0]);
            }

            /* 
             * Transliterate each string value of input individually
             * !!! will be used for future functionality !!!
             */
            for (int i = 0; i < nameSeparated.Length; i++)
            {
                //transliterate a word and add it to a nameSeparated string array
                //value is not in use 08.08.2019
                //nameSeparated[i - 1] = transliteration(separated[i]);

                //creates full native alias for company name, without company type
                nativeCompanyName = nativeCompanyName + " " + nameSeparated[i];
            }

            //native alias for company name, without company type
            companyName = nativeCompanyName;

            //transliterate company name and type
            transliteratedCompanyName = transliteration(nativeCompanyName);
            transliteratedFullCompanyType = transliteration(companyTypeGenerated[0]);

            //native alias for company name, with full company type
            fullNativeAlias = companyTypeGenerated[0] + nativeCompanyName;

            //Transliterate short,full native alias and company name separetely
            string translitedShortNativeAlias = transliteration(shortNativeAlias);
            string translitedFullNativeAlias = transliteration(fullNativeAlias);
            string companyNameTranslited = transliteration(companyName);

            //if abbreviation is behind, change order of native alias
            if (abbreviationBehind)
            {
                shortNativeAlias = separated[separated.Length - 1] + nativeCompanyName;
            }

            //if no abbreviation, remove unused input 
            if (noAbbreviation)
            {
                output = input + System.Environment.NewLine +
                    transliteration(input);
            }
            else
            {
                //form output value with line breaks
                output = shortNativeAlias + System.Environment.NewLine +
                         fullNativeAlias + System.Environment.NewLine +
                         transliteratedShortCompanyType + transliteratedCompanyName + System.Environment.NewLine +
                         transliteratedFullCompanyType + transliteratedCompanyName + System.Environment.NewLine +
                         companyTypeGenerated[1] + transliteratedCompanyName + System.Environment.NewLine +
                         companyTypeGenerated[2] + transliteratedCompanyName + System.Environment.NewLine;
            }
            
            //set output value to output field
            txt_output.Text = output.ToUpper();
        }

        /*
         * Function to determine company type, based on 1st input value
         * returns an array of 3 values:
         * 1. Full native company type
         * 2. short english company type
         * 3. full english company type
         */ 
        private string[] companyType(string firstInput, string lastInput)
        {
            //create array of length 3 to return
            string[] companyType = new string[3];

            //value is second try
            bool isSecondAttempt = false;

            //send first check
            companyType = transliterateCompany(firstInput, isSecondAttempt);
            //if returned null -> first input is not abbreviation
            if (companyType==null)
            {
                isSecondAttempt = true;
                companyType = transliterateCompany(lastInput, isSecondAttempt);
            }

            //return array to main function
            return companyType;
        }
        //end of companyType function

        private string[] transliterateCompany(string input, bool isSecondAttempt)
        {
            //create array of length 3 to return
            string[] type = new string[3];

            bool isAbbreviation = true;

            //switch case to decode company type
            switch (input)
            {
                case "ОАО":
                case "оао":
                    type[0] = "ОТКРЫТОЕ АКЦИОНЕРНОЕ ОБЩЕСТВО";
                    type[1] = "OJSC";
                    type[2] = "OPEN JOINT STOCK COMPANY";
                    break;
                case "ЗАО":
                case "зао":
                    type[0] = "ЗАКРЫТОЕ АКЦИОНЕРНОЕ ОБЩЕСТВО";
                    type[1] = "CJSC";
                    type[2] = "CLOSED JOINT STOCK COMPANY";
                    break;
                case "ПАО":
                case "пао":
                    type[0] = "ПУБЛИЧНОЕ АКЦИОНЕРНОЕ ОБЩЕСТВО";
                    type[1] = "PJSC";
                    type[2] = "PUBLIC JOINT STOCK COMPANY";
                    break;
                case "ЧАО":
                case "чао":
                    type[0] = "ЧАСТНОЕ АКЦИОНЕРНОЕ ОБЩЕСТВО";
                    type[1] = "PJSC";
                    type[2] = "PRIVATE JOINT STOCK COMPANY";
                    break;
                case "АО":
                case "ао":
                    type[0] = "АКЦИОНЕРНОЕ ОБЩЕСТВО";
                    type[1] = "JSC";
                    type[2] = "JOINT STOCK COMPANY";
                    break;
                case "ООО":
                case "ооо":
                    type[0] = "ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ";
                    type[1] = "LLC";
                    type[2] = "LIMITED LIABILITY COMPANY";
                    break;
                case "МУП":
                case "муп":
                    type[0] = "МУНИЦИПАЛЬНОЕ УНИТАРНОЕ ПРЕДПРИЯТИЕ";
                    type[1] = "MUE";
                    type[2] = "MUNICIPAL UNITARY ENTERPRISE";
                    break;
                case "ФГУП":
                case "фгуп":
                    type[0] = "ФЕДЕРАЛЬНОЕ ГОСУДАРСТВЕННОЕ УНИТАРНОЕ ПРЕДПРИЯТИЕ";
                    type[1] = "FGUP";
                    type[2] = "FEDERAL STATE UNITARY ENTERPRISE";
                    break;

                // if company type is not in the list, transliterate input
                default:
                    type[0] = input;
                    type[1] = transliteration(input);
                    type[2] = type[1];
                    isAbbreviation = false;
                    break;
            }
            if (isAbbreviation && !isSecondAttempt)
            {
                noAbbreviation = false;
                abbreviationBehind = false;
                return type;
            }
            else if (!isAbbreviation && !isSecondAttempt)
            {
                return null;
            }
            else if (isAbbreviation && isSecondAttempt)
            {
                noAbbreviation = false;
                abbreviationBehind = true;
                return type;
            }
            else
            {
                abbreviationBehind = false;
                noAbbreviation = true;
                return type;
            }
            
        }

        /*
         * Function to transliterate russian input to english 
         * according to CIS guidelines
         * transforms string value to character array and transliterates each value individually
         */
        private string transliteration(string input)
        {
            // value to check if special rule needs to be applied
            bool previousVowel = true;

            //current character from input
            char current = ' ';

            //string value of transliterated character
            string english = "";

            //transliterated result (whole name)
            string result="";

            /* 
             * separates string input into character array
             */
            char[] separated = input.ToCharArray();

            /* 
            * transliterate each character in array
            */
            for (int i = 0; i < separated.Length; i++)
            {
                current = separated[i];
                switch (current)
                {
                    case 'А':
                    case 'а':
                        english = "A";
                        previousVowel = true;
                        break;
                    case 'Б':
                    case 'б':
                        english = "B";
                        previousVowel = false;
                        break;
                    case 'В':
                    case 'в':
                        english = "V";
                        previousVowel = false;
                        break;
                    case 'Г':
                    case 'г':
                        english = "G";
                        previousVowel = false;
                        break;
                    case 'Д':
                    case 'д':
                        english = "D";
                        previousVowel = false;
                        break;
                    case 'Е':
                    case 'е':
                    case 'Ё':
                    case 'ё':
                        if (previousVowel)
                        {
                            english = "YE";
                        }
                        else
                        {
                            english = "E";
                        } 
                        previousVowel = true;
                        break;
                    case 'Ж':
                    case 'ж':
                        english = "ZH";
                        previousVowel = false;
                        break;
                    case 'З':
                    case 'з':
                        english = "Z";
                        previousVowel = false;
                        break;
                    case 'И':
                    case 'и':
                        english = "I";
                        previousVowel = true;
                        break;
                    case 'Й':
                    case 'й':
                        previousVowel = true;
                        english = "Y";
                        break;
                    case 'К':
                    case 'к':
                        english = "K";
                        previousVowel = false;
                        break;
                    case 'Л':
                    case 'л':
                        english = "L";
                        previousVowel = false;
                        break;
                    case 'М':
                    case 'м':
                        english = "M";
                        previousVowel = false;
                        break;
                    case 'Н':
                    case 'н':
                        english = "N";
                        previousVowel = false;
                        break;
                    case 'О':
                    case 'о':
                        english = "O";
                        previousVowel = true;
                        break;
                    case 'П':
                    case 'п':
                        english = "P";
                        previousVowel = false;
                        break;
                    case 'Р':
                    case 'р':
                        english = "R";
                        previousVowel = false;
                        break;
                    case 'С':
                    case 'с':
                        english = "S";
                        previousVowel = false;
                        break;
                    case 'Т':
                    case 'т':
                        english = "T";
                        previousVowel = false;
                        break;
                    case 'У':
                    case 'у':
                        english = "U";
                        previousVowel = true;
                        break;
                    case 'Ф':
                    case 'ф':
                        english = "F";
                        previousVowel = false;
                        break;
                    case 'Х':
                    case 'х':
                        english = "KH";
                        previousVowel = false;
                        break;
                    case 'Ц':
                    case 'ц':
                        english = "TS";
                        previousVowel = false;
                        break;
                    case 'Ч':
                    case 'ч':
                        english = "CH";
                        previousVowel = false;
                        break;
                    case 'Ш':
                    case 'ш':
                        english = "SH";
                        previousVowel = false;
                        break;
                    case 'Щ':
                    case 'щ':
                        english = "SHCH";
                        previousVowel = false;
                        break;
                    case 'Ъ':
                    case 'ъ':
                        english = "";
                        previousVowel = true;
                        break;
                    case 'Ы':
                    case 'ы':
                        english = "Y";
                        previousVowel = false;
                        break;
                    case 'Ь':
                    case 'ь':
                        english = "";
                        previousVowel = true;
                        break;
                    case 'Э':
                    case 'э':
                        english = "E";
                        previousVowel = true;
                        break;
                    case 'Ю':
                    case 'ю':
                        english = "YU";
                        previousVowel = true;
                        break;
                    case 'Я':
                    case 'я':
                        previousVowel = true;
                        english = "YA";
                        break;
                    case ' ':
                        english = " ";
                        previousVowel = true;
                        break;
                    //if special character, transform it to string
                    default:
                        english = current.ToString();
                        break;
                }
                //add a generated string to result value
                result = result + english;
            }

            return result;
        }
        //end of transliteration function

    }
}
