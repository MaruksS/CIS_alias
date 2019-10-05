using System;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace CIS_Alias_generator
{
    public partial class Form1 : Form
    {
        //create dictionary array of transliteration symbol objects
        transliteration[] dictionary = new transliteration[100];


        public Form1()
        {
            InitializeComponent();

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\docs\transliteration.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            string lang = xlWorksheet.Name;


            for (int i = 2; i <= rowCount; i++)
            {
                dictionary[i] = new transliteration(Convert.ToString(xlRange.Cells[i, 1].Value2), Convert.ToString(xlRange.Cells[i, 2].Value2));
            }
        }

        private string transliterate(string input, int type)
        {

            string resultTotal = "";
            string english;

            if (type == 1)
            {
                //split input into string values array
                string[] separated = input.Split(null);

                /* 
                 * Transliterate each string value of input individually
                 */
                for (int i = 0; i < separated.Length; i++)
                {
                    if (i == 0)
                    {
                        english = transliterateWord(separated[i], true);
                        resultTotal = english + ",";
                    }
                    else
                    {
                        english = transliterateWord(separated[i], false);
                        resultTotal = resultTotal + english + " ";
                    }
                }
            }

            return resultTotal;
        }

        private string transliterateWord(string input, bool capitals)
        {
            string result = "";

            /* 
             * separates string input into character array
             */
            char[] separated = input.ToCharArray();
            char current;
            char original = ' ';
            string english = "";

            for (int i = 0; i < separated.Length; i++)
            {
                current = separated[i];

                for (int j = 1; j < dictionary.Length; j++)
                {
                    original = ' ';
                    if (dictionary[j] != null)
                    {
                        original = char.Parse(dictionary[j].getOriginal());
                        if (current == original)
                        {

                            english = dictionary[j].getTranslit();
                            if (i == 0)
                            {
                                english = FirstCharToUpper(english);
                            }
                            break;
                        }
                        else
                        {
                            english = current.ToString();

                        }
                    }
                }
                if (capitals && english != null)
                {
                    english = english.ToUpper();
                }
                result = result + english;
            }

            return result;
        }

        private void onBtnClick(object sender, EventArgs e)
        {
            string input = txt_input.Text.Trim();
            string result = "";

            if (chb_Person.Checked == false)
            {
                result = transliterate(input, 1);
            }

            label1.Text = result;

        }

        private string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        /* 
         * bool abbreviationBehind = false;
         * bool noAbbreviation = false;
         *
         * on Generate button click
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
            string[] companyTypeGenerated = companyType(separated[0], separated[separated.Length - 1]);

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
        /* 
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

        /*
            private string[] companyType(string firstInput, string lastInput)
            {
                //create array of length 3 to return
                string[] companyType = new string[3];

                //value is second try
                bool isSecondAttempt = false;

                //send first check
                companyType = transliterateCompany(firstInput, isSecondAttempt);
                //if returned null -> first input is not abbreviation
                if (companyType == null)
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
            */
    }

}
