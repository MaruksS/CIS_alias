using System;
using System.Windows.Forms;

namespace CIS_Alias_generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Generate button
        private void btn_generate_Click(object sender, EventArgs e)
        {
            string input = txt_input.Text;
            string[] separated = input.Split(null);
            string[] nameSeparated = new string[separated.Length - 1];

            string companyName;
            string output = "";
            string[] companyTypeGenerated = companyType(separated[0]);

            string fullNativeAlias = "";
            string shortNativeAlias = input;

            for (int i = 1; i < separated.Length; i++)
            {
                nameSeparated[i - 1] = transliteration(separated[i]);
                fullNativeAlias = fullNativeAlias + " " + separated[i];
            }
            companyName = fullNativeAlias;
            fullNativeAlias = companyTypeGenerated[0] + fullNativeAlias;


            string translitedShortNativeAlias = transliteration(shortNativeAlias);
            string translitedFullNativeAlias = transliteration(fullNativeAlias);
            string companyNameTranslited = transliteration(companyName);

            output = shortNativeAlias + System.Environment.NewLine +
                     fullNativeAlias + System.Environment.NewLine +
                     translitedShortNativeAlias + System.Environment.NewLine +
                     translitedFullNativeAlias + System.Environment.NewLine +
                     companyTypeGenerated[1] + companyNameTranslited + System.Environment.NewLine +
                     companyTypeGenerated[2] + companyNameTranslited + System.Environment.NewLine;

            txt_output.Text = output.ToUpper();
        }

        private string[] companyType(string input)
        {
            string[] type = new string[3];

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
                default:
                    type[0] = input;
                    type[1] = transliteration(input);
                    type[2] = type[1];
                    break;
            }
            return type;
        } 

        private string transliteration(string input)
        {
            bool previousVowel = true;
            char current = ' ';
            string english = "";
            string result="";

            char[] separated = input.ToCharArray();
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
                    default:
                        english = "_";
                        break;
                }
                result = result + english;
            }

            return result;
        }
    }
}
