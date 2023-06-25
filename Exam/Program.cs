using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateDictionary();
        }

        public static void CreateDictionary()
        {

            Console.WriteLine($"Добро пожаловать в программу \"Cловари\"");
            Console.WriteLine("Создайте свой словарь");
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            Console.WriteLine($"Нажмите любую клавишу, чтобы продолжить");
            Console.ReadKey();
            Menu(dict);
        }

        public static void Menu(Dictionary<string, List<string>> dict) 
        { 
            while (true) 
            { 
                Console.Clear(); 
                Console.WriteLine($"Выберите пункт меню"); 
                Console.WriteLine("1 - Добавить слово и перевод"); 
                Console.WriteLine("2 - Удалить слово"); 
                Console.WriteLine("3 - Удалить перевод"); 
                Console.WriteLine("4 - Найти слово и перевод"); 
                Console.WriteLine("5 - Поменять слово"); 
                Console.WriteLine("6 - Поменять перевод"); 
                Console.WriteLine("7 - Вывести словарь в файл"); 
                Console.WriteLine("8 - Введите 0 для выхода из программы"); 

                ConsoleKeyInfo keyInfo; keyInfo = Console.ReadKey();
                
                if (keyInfo.Key >= ConsoleKey.D1 && keyInfo.Key <= ConsoleKey.D8) 
                { 
                    if (keyInfo.Key == ConsoleKey.D1) 
                    { 
                        Console.Clear(); addWord(dict); 
                    } 
                    else if (keyInfo.Key == ConsoleKey.D2) 
                    { 
                        Console.Clear(); 
                        delWord(dict); 
                    } 
                    else if (keyInfo.Key == ConsoleKey.D3) 
                    { 
                        Console.Clear(); 
                        delTranslation(dict); 
                    } 
                    else if (keyInfo.Key == ConsoleKey.D4) 
                    { 
                        Console.Clear(); 
                        searchWordAndTranslations(dict); 
                    } 
                    else if (keyInfo.Key == ConsoleKey.D5) 
                    { 
                        Console.Clear(); 
                        swapWord(dict); } 
                    else if (keyInfo.Key == ConsoleKey.D6) 
                    { 
                        Console.Clear(); 
                        swapTranslation(dict); 
                    } 
                    else if (keyInfo.Key == ConsoleKey.D7) 
                    { 
                        Console.Clear(); 
                        fileTransfer(dict); 
                    } 
                    else if (keyInfo.Key == ConsoleKey.D8) 
                    { 
                        Console.Clear(); 
                        Console.WriteLine($"Спасибо, что пользовались нашей программой. Заходите ещё!"); 
                        break; 
                    } 
                } else 
                { 
                    Console.WriteLine("Неверный ввод"); 
                } 
            } 
        }

        public static void addWord(Dictionary<string, List<string>> myDict)
        {
            while (true)
            {
                Console.WriteLine("Введите слово, которое хотите перевести.");
                Console.WriteLine("Для окончания ввода введите (exit)");
                string word = Console.ReadLine();

                if (word.ToLower() == "exit")
                {
                    break;
                }
                if (myDict.ContainsKey(word))
                {
                    Console.WriteLine("Такое слово уже существует.");
                    continue;
                }
                List<string> translations = new List<string>();
                while (true)
                {
                    Console.Write("Введите перевод слова (для окончания ввода переводов введите 'exit'): ");
                    string translation = Console.ReadLine();

                    if (translation.ToLower() == "exit")
                        break;

                    translations.Add(translation);
                }

                myDict.Add(word, translations);
            }
        }

        public static void delWord(Dictionary<string, List<string>> dict)
        {
            Console.WriteLine($"Введите слово, которое нужно удалить");
            string word = Console.ReadLine();
            if (dict.Keys.Contains(word))
            {
                dict.Remove(word);
            }
        }
        public static void delTranslation(Dictionary<string, List<string>> dict)
        {
            Console.WriteLine($"Введите перевод, который хотите удалить");
            string translation = Console.ReadLine();
            foreach (var item in dict)
            {
                if (item.Value.Count > 0)
                {
                    item.Value.RemoveAt(0);
                }
                else
                {
                    Console.WriteLine($"Нельзя удалить перевод, поскольку он 1");
                }
            }
        }

        public static void searchWordAndTranslations(Dictionary<string, List<string>> dict)
        {
            Console.WriteLine($"Введите искомое слово");
            string wordSearch = Console.ReadLine();
            if (dict.Keys.Contains(wordSearch))
            {
                int count = 1;
                Console.WriteLine($"Слово: {wordSearch} Перевод: ");
                foreach (var translations in dict[wordSearch])
                {
                    Console.WriteLine($"{count++} - {translations}");
                }
                Console.WriteLine($"Нажмите любую клавишу, чтобы продолжить");
                Console.ReadKey();
            }
        }

        public static void swapWord(Dictionary<string, List<string>> dict)
        {
            Console.WriteLine($"Введите слово, которое хотите поменять");
            string word = Console.ReadLine();
            List<string> list = new List<string>();
            list = dict[word];
            dict.Remove(word);
            Console.WriteLine($"Введите новое слово");
            string new_word = Console.ReadLine();
            dict.Add(new_word, list);
        }

        public static void swapTranslation(Dictionary<string, List<string>> dict)
        {
            Console.WriteLine($"Введите слово, чей перевод(ы) хотите поменять");
            string word = Console.ReadLine();
            List<string> list = new List<string>();


            while (true)
            {
                Console.WriteLine($"Введите перевод слова, который хотите поменять \'exit\' для выхода");
                string oldTrans = Console.ReadLine();
                if (oldTrans.ToLower() == "exit")
                {
                    break;
                }
                dict[word].Remove(oldTrans);
                Console.WriteLine($"Введите новый перевод ");
                dict[word].Add(Console.ReadLine().ToString());

            }
        }

        public static void fileTransfer(Dictionary<string, List<string>> dict)
        {
            Console.WriteLine($"Введите название словаря");
            string dictName = Console.ReadLine();
            using (var writer = new StreamWriter(dictName))
            {
                foreach (var blocks in dict)
                {
                    writer.WriteLine($"Слово: {blocks.Key}");
                    writer.WriteLine($"Переводы: ");
                    foreach (var translations in blocks.Value)
                    {
                        writer.WriteLine($"{translations}");
                    }
                }
            }
        }
        
    }
}
