using System;
using System.Linq;
using System.Windows.Forms;


namespace CIS_Alias_generator
{
    public partial class Form1 : Form
    {
        //create dictionary array of transliteration symbol objects
        transliteration[] dictionary = new transliteration[100];
        ExceptionRules[] rules = new ExceptionRules[20];
        string[] indicators = new string[50];
        string[,] allCompanyAbbreviations = new string[20, 20];

        string[] companyAliases = new string[20];


        public Form1()
        {
            InitializeComponent();
            string line;
            string mode = "";
            int count=0;

            // Read the file and save rules
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"C:\docs\config.txt");
            while ((line = file.ReadLine()) != null)
            {
                if (line=="[alphabet]")
                {
                    mode = "alphabet";
                    count = 0;
                }
                if (line == "[rules]")
                {
                    mode = "rules";
                    count = 0;
                }
                if (line == "[company]")
                {
                    mode = "company";
                    count = 0;
                }
                if (line == "[type1]")
                {
                    mode = "type1";
                    count = 0;
                }

                //add each letter with transliteration to the dictionary
                if (mode == "alphabet")
                {
                    string[] letters = line.Split('-');
                    if (letters.Length > 1)
                    {
                        dictionary[count] = new transliteration(letters[0], letters[1]);
                    }
                    else
                    {
                        dictionary[count] = new transliteration(letters[0], "");
                    }
                    count++;
                }

                //TODO: Rule handling
                if (mode == "rules")
                {
                    string[] transliterationParameters = line.Split(':');
                    if (transliterationParameters.Length==4)
                    {
                        rules[count] = new ExceptionRules(transliterationParameters[0], transliterationParameters[1], transliterationParameters[2], transliterationParameters[3]);
                    }
                }
                if (mode == "type1")
                {
                    string symbol = line;
                    indicators[count] = symbol;
                    count++;
                }

                //add each company abbreviation to the abbreviation list
                if (mode == "company")
                {
                    string[] companyArray = line.Split('~');
                    for (int i = 0; i < companyArray.Length; i++)
                    {
                        allCompanyAbbreviations[count,i] = companyArray[i];
                    }
                    count++;
                }
            }
            file.Close();
        }


        private string transliterate(string input, int type)
        {
            // final output string
            string resultTotal = "";

            companyAliases = new string[20];

            //temporary value to store translation 
            string english;
            string englishResult="";
            string nativeResult="";

            int row = 0;

            //abbreviation indicators
            bool abbreviationBehind = false;
            bool abbreviationFront = false;
            bool noAbbreviation = false;

            //split input into string values array
            string[] separated = input.Split(null);

            /* 
            * if type person
            */
            if (type == 1)
            {
                //Transliterate each string value of input individually
                for (int i = 0; i < separated.Length; i++)
                {
                    //if 1st character, make upper case
                    if (i == 0)
                    {
                        english = transliterateWord(separated[i], true);
                        englishResult = english + ",";
                        nativeResult = separated[i].ToUpper() + ",";
                    }
                    else
                    {
                        english = transliterateWord(separated[i], false);
                        englishResult = englishResult + english + " ";
                        nativeResult = nativeResult + FirstCharToUpper(separated[i]) + " ";
                    }
                }//End of for: Transliterate each string value of input individually
                resultTotal = nativeResult + Environment.NewLine + englishResult;
            }

            /* 
            * if type entity
            */
            else
            {
                //Transliterate each string value of input individually
                for (int i = 0; i < separated.Length; i++)
                {
                    // if 1st element of input
                    if (i == 0)
                    {
                        //Find first and last word of input
                        string lastWord = separated[separated.Length-1];
                        string firstWord = separated[0];
                        row = 0;

                        //check if last/first word of input is inside abbreviation list
                        for (int j = 0; j < allCompanyAbbreviations.GetLength(0); j++)
                        {
                            //if abbreaviation is first word
                            if (allCompanyAbbreviations[j, 0] == firstWord | allCompanyAbbreviations[j, 1] == firstWord)
                            {
                                abbreviationFront = true;
                                row = j;
                                break;
                            }
                            //if abbreaviation is last word
                            else if (allCompanyAbbreviations[j, 0] == lastWord | allCompanyAbbreviations[j, 1] == lastWord)
                            {
                                abbreviationBehind = true;
                                row = j;
                                break;
                            }
                        }

                        // add abbreviation if found
                        if (abbreviationBehind | abbreviationFront)
                        {
                            // add all abbreviation translations to alias
                            for (int j = 1; j < allCompanyAbbreviations.GetLength(0); j++)
                            {
                                int x = j - 1;
                                if (allCompanyAbbreviations[row, j] != null)
                                {
                                    companyAliases[x] = allCompanyAbbreviations[row, j] + " ";
                                }
                            }

                            // if abbreviation was behind name, put it upfront and add 1st word
                            if (abbreviationBehind)
                            {
                                addToCompanyName(separated[i], noAbbreviation, row);
                            }
                        }

                        //if no abbreviation found, translate
                        else
                        {
                            companyAliases[0] = "";
                            companyAliases[1] = "";
                            noAbbreviation = true;
                            addToCompanyName(separated[i], noAbbreviation, row);  
                        }
                    } //end of (if 1st element of input)

                    //if 2nd> element, transliterate and add to alias
                    else
                    {
                        if (abbreviationBehind && i == separated.Length-1) break;

                        else
                        {
                            addToCompanyName(separated[i], noAbbreviation, row);
                        }
                    }
                } //End of for: Transliterate each string value of input individually

                //after translating all aliases add them to result string
                for (int i = 0; i < companyAliases.Length; i++)
                {
                    if (companyAliases[i] !=null)
                    {
                        resultTotal = resultTotal + companyAliases[i] + Environment.NewLine;
                    }
                }


            } //End of (if entity)
            return resultTotal;
        }

        //translate company name
        private void addToCompanyName(string text, bool noAbbreviation, int row)
        {
            //check if alias contains native symbols
            bool isNative = false;

            //how many alias outputs are neccesary
            int times = 2;

            //if no abbreviation found add 2 aliases (native and transliterated)
            if(noAbbreviation)
            {
                companyAliases[0] = companyAliases[0] + text.ToUpper() + " ";
                companyAliases[1] = companyAliases[1] + transliterateWord(text, true) + " ";
            }

            //if abbreviation found
            else
            {
                //nr of loops is equal to nr of abbreviation translations
                times = companyAliases.Length;

                //loop through each alias
                for (int j = 0; j < times; j++)
                {
                    //set default as non-native
                    isNative = false;

                    if (companyAliases[j] != null)
                    {
                        //convert word to char array and check if it contains native symbols
                        char[] aliasCharacterArray = companyAliases[j].ToCharArray();
                        for (int i = 0; i < aliasCharacterArray.Length; i++)
                        {
                            if (checkDictionary(aliasCharacterArray[i]))
                            {
                                isNative = true;
                                break;
                            }
                        }
                        //if contains native symbols - do not transliterate
                        if (isNative)
                        {
                            companyAliases[j] = companyAliases[j] + text.ToUpper() + " ";
                        }
                        //if no native symbols found - transliterate
                        else
                        {
                            companyAliases[j] = companyAliases[j] + transliterateWord(text, true) + " ";
                        }
                    }
                }//end of loop through each alias
            }//end of if abbreviation found
        }

        /* 
        * translate single word
        */
        private string transliterateWord(string input, bool capitals)
        {
            // transliterated final word
            string result = "";

            /* 
             * separates string input into character array
             */
            char[] separated = input.ToCharArray();

            // temporary variable to hold currently viewed character
            char current;

            // temporary variable to hold original character (from object)
            char original = ' ';

            // temporary variable to hold special symbol, to which special rules will apply (from object)
            char specialSymbol = '%';

            // temporary variable to hold special symbol replacement (from object)
            string replacement= "";

            //TODO: apply dynamic rule handling
            if (rules[0] != null)
            {
                specialSymbol = rules[0].getSymbol().ToCharArray()[0];
                replacement = rules[0].getReplacement();
            }

            //if special symbol (for the rule handling)
            bool special = false;

            // temporary variable to hold english transliteration
            string english = "";

            //loop through each character of a word
            for (int i = 0; i < separated.Length; i++)
            {
                //select current character
                current = separated[i];

                //apply rule handling on character
                if (special & current == specialSymbol || (i == 0 && indicators.Contains(" ") & current == specialSymbol))
                {
                    english = replacement;
                    if (i == 0)
                    {
                        english = FirstCharToUpper(english);
                    }
                }
                // if special rules doesn't apply for character
                else
                {

                    //loop through dictionary to find current character
                    for (int j = 1; j < dictionary.Length; j++)
                    {
                        original = ' ';
                        if (dictionary[j] != null)
                        {
                            original = char.Parse(dictionary[j].getOriginal());

                            //if current character is found in dictionary, transliterate
                            if (current == original)
                            {
                                english = dictionary[j].getTranslit();
                                //if first character of the word, make it upper case
                                if (i == 0) english = FirstCharToUpper(english);
                                //break outside dictionary loop
                                break;
                            }
                            //if current character is not found in dictionary, keep it
                            else
                            {
                                english = current.ToString();
                            }
                        }
                    }
                    //check if current character is an indicator of special rule
                    special = checkSpecial(current);
                }

                //if capitals required, transform
                if (capitals && english != null)
                {
                    english = english.ToUpper();
                }
                //add transliteration to result
                result = result + english;
            }//end of loop through each character of a word

            //return word
            return result;
        }

        /* 
         * Check if current character falls under special rule
         */
        private bool checkSpecial(char current)
        {
            char indicator;
            bool special = false;

            for (int x = 0; x < indicators.Length; x++)
            {
                if (indicators[x] != null)
                {
                    indicator = indicators[x].ToCharArray()[0];
                    if (indicator == current)
                    {
                        special = true;
                        break;
                    }
                    else special = false;
                }
            }

            return special;
        }

        /* 
         * Check if current character falls under special rule
         */
        private bool checkDictionary(char current)
        {
            char indicator;
            bool special = false;
            
            for (int x = 0; x < dictionary.Length; x++)
            {
                
                if (dictionary[x] != null)
                {
                    indicator = dictionary[x].getOriginal().ToCharArray()[0];
                    if (indicator == current)
                    {
                        special = true;
                        break;
                    }
                    else special = false;
                }
            }
            return special;
        }

        /* 
         * Trigger button activity
         */
        private void onBtnClick(object sender, EventArgs e)
        {
            string input = txt_input.Text.Trim();
            string result = "";

            if (chb_Person.Checked == false)
            {
                result = transliterate(input, 1);
            }else
            {
                result = transliterate(input, 2);
            }

            txt_output.Text = result;

        }

        /* 
         * Change first character of string to UPPER CASE
         */
        private string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }
    }

}
