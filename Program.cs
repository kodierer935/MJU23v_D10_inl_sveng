﻿using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        public static string Input(string input)
        {
            Console.Write(input);
            return Console.ReadLine();
        }

        static List<SweEngGloss> dictionary;
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            // TODO: felhantering för ogiltiga inmatningar
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }

        static void Main(string[] args)
        {
            //FIXME; ingen annan fil kan väljas || program får fel ifall man försöker ladda annan fil.
            //FIXME: programmet kan inte hantera fil som ej hittas.
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            
            do

            {
                try
                {
                        Input("> ");
                    string[] argument = Console.ReadLine().Split();
                    string command = argument[0];
                    // TODO: quit ska avsluta while loopen. (break;)
                    if (command == "quit")
                    {
                        Console.WriteLine("Goodbye!");
                        break;
                    }
                
                
                    else if (command == "load")
                    {
                        
                            if (argument.Length == 2)
                            {
                                try 
                                {
                                using (StreamReader sr = new StreamReader(argument[1]))
                                {
                                    dictionary = new List<SweEngGloss>(); // TODO: Tömma dictionary vid varje load
                                    string line = sr.ReadLine();
                                    while (line != null)
                                    {
                                        SweEngGloss gloss = new SweEngGloss(line);
                                        dictionary.Add(gloss);
                                        line = sr.ReadLine();
                                    }
                                }
                                }
                                catch (FileNotFoundException) { Console.WriteLine("THIS FILE NOT FOUND!"); }

                                
                            }





                            else if (argument.Length == 1)
                            {

                                using (StreamReader sr = new StreamReader(defaultFile))
                                {
                                    dictionary = new List<SweEngGloss>(); // TODO: Tömma dictionary vid varje load
                                    string line = sr.ReadLine();
                                    while (line != null)
                                    {
                                        SweEngGloss gloss = new SweEngGloss(line);
                                        dictionary.Add(gloss);
                                        line = sr.ReadLine();
                                    }
                                }


                            }
                    
                    
                    

                    }
                    // NYI: felhantering för filer som ej hittas

                    else if (command == "list")
                    {
                         //FIXME: felhantering för tom lista eller ingen vald fil
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                        }
                    
                    }
                    else if (command == "new")
                    {
                   
                        //FIXME: filhantering eller separat lista för nya ord utan att ladda fil. 
                        if (argument.Length == 3)
                        {
                            dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                        }
                        //FIXME: felhantering ifall endast ett ord är skrivet efter kommandot "new" t.ex. new sol.
                        else if (argument.Length == 1)
                        {
                            string swe = Input("Write word in Swedish: ");
                            string eng = Input("Write word in English: ");
                            dictionary.Add(new SweEngGloss(swe, eng));
                        }
                   
                    
                    }
                    else if (command == "delete")
                    {
                        if (argument.Length == 3)
                        {
                            int index = -1;
                            for (int i = 0; i < dictionary.Count; i++) {
                                SweEngGloss gloss = dictionary[i];
                                if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                                    index = i;
                            }
                            //FIXME: felhantering om ordet inte finns i ordlistan.
                            dictionary.RemoveAt(index);
                        }
                        else if (argument.Length == 1)
                        {
                            string del_swe = Input("Write word in Swedish: ");
                            string del_eng = Input("Write word in English: ");
                            int index = -1;
                            for (int i = 0; i < dictionary.Count; i++)
                            {
                                SweEngGloss gloss = dictionary[i];
                                if (gloss.word_swe == del_swe && gloss.word_eng == del_eng)
                                    index = i;
                            }
                            dictionary.RemoveAt(index);
                        }
                    }
                    else if (command == "translate")
                    {
                        string userInput;
                        if (argument.Length == 2)
                            userInput = argument[1];
                        if (argument.Length == 1)
                        {
                            userInput = Input("Write word to be translated: ");
                        }
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == userInput)
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == userInput)
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        } 
                     
                        //NYI: felhantering om ordet inte finns i ordlistan.
                    }
                    else
                    {
                        Console.WriteLine($"Unknown command: '{command}'");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            while (true);
            
            
        }
    }
}