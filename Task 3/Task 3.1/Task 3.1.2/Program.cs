using System;
using System.Linq;
using System.Collections.Generic;

namespace Task_3._1._2
{
    class Program{
        static void Main(string[] args)
        {
            //Amy normally hated Monday mornings, but this year was different. Kamal was in her art class and she liked Kamal. She was waiting outside the classroom when her friend Tara arrived.
            TextAnalysis TA = new TextAnalysis();
            TA.StartApp();
        }
    }

    class TextAnalysis{
        private Dictionary<string, int> _wordsAndCount;
        private Dictionary<string, int> _topRareWords;
        private Dictionary<string, int> _topFrequentWords;
        private int _countAllWords;
        private ConsoleMenu _startMenu;
        private ConsoleMenu _menuAfterAnalyze;

        private const double _UNIQUENESS_PERCENT = 0.15f;
        public TextAnalysis(){
            _startMenu = new ConsoleMenu(
                new string[]{
                    "1.Analyze",
                    "2.Exit",
                },
                new Action[]{
                    () => Analyze(),
                }
            );

            _menuAfterAnalyze = new ConsoleMenu(
                new string[]{
                    "1.Show top frequent words",
                    "2.Show top rare words",
                    "3.Show word-count",
                    "4.Back",
                },
                new Action[]{
                    () => PrintTopFrequent(),
                    () => PrintTopRare(),
                    () => PrintWordsCount(),
                }
            );
        }
        private void GetWordsCount(){
            _wordsAndCount = new Dictionary<string, int>();
            string[] words;
            
            do{
                Console.WriteLine("Enter text:");
                string inputText = Console.ReadLine();
                words = new string(inputText.Where(c => !char.IsPunctuation(c)).ToArray()).ToLower().Split(' ');
                if(words.Length > 5) break;
                else Console.WriteLine("The text is too short to analyze");
            }while(true);

            _countAllWords = words.Length;

            for(int i = 0; i < _countAllWords; i++){
                int count = 0;
                for(int j = i; j < _countAllWords; j++){
                    if(words[i] == words[j]) count++;
                }
                if(!_wordsAndCount.ContainsKey(words[i])) _wordsAndCount.Add(words[i], count);
            }
        }

        private void Analyze(){
            GetWordsCount();
            Console.Clear();
            CalculateTextRating();
            TopWords();
            _menuAfterAnalyze.Listener();
        }

        private void TopWords(){
            _topFrequentWords = new Dictionary<string, int>();
            _topRareWords = new Dictionary<string, int>();
            int freqCounter = 0;
            int rareCounter = 0;
            int uniquenessPercent = (int)Math.Round(_countAllWords * _UNIQUENESS_PERCENT)/2;

            foreach (KeyValuePair<string, int> keyValue in _wordsAndCount){
                if(freqCounter + rareCounter == 6) break;
                if(keyValue.Value >= uniquenessPercent && freqCounter != 3){
                    _topFrequentWords.Add(keyValue.Key, keyValue.Value);
                    freqCounter++;
                }
                if(keyValue.Value < uniquenessPercent && rareCounter != 3) {
                    _topRareWords.Add(keyValue.Key, keyValue.Value);
                    rareCounter++;
                }
            }
        }

        private void PrintWordsCount(){
           Console.Clear();
           PrintDictionary(_wordsAndCount);
        }

        private void PrintTopFrequent(){
           Console.Clear();
           PrintDictionary(_topFrequentWords);
        }

        private void PrintTopRare(){
           Console.Clear();
           PrintDictionary(_topRareWords);
        }

        private void PrintDictionary(Dictionary<string, int> dictionary){
            foreach (KeyValuePair<string, int> keyValue in dictionary){
                Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
            }
        }

        private void CalculateTextRating(){
            int uniquenessPercent = (int)Math.Round(_countAllWords * _UNIQUENESS_PERCENT);
            int badMark = 0;
            foreach (int i in _wordsAndCount.Values){
                if(i > uniquenessPercent) badMark++;
            }
            if(badMark > 1) Console.WriteLine("Monotonous text ");
            else Console.WriteLine("Varied text");
        }

        public void StartApp(){
            _startMenu.Listener();
        }
    }

    class ConsoleMenu{
        public string[] MenuText {get; private set;}
        public Action[] MenuMethods {get; private set;}

        public ConsoleMenu(string[] menuText, Action[] menuMethods){
            MenuText = menuText;
            MenuMethods = menuMethods;
        }

        public void Listener(){
            int userChoice;
            do{
                PrintMenu();
                Int32.TryParse(Console.ReadLine(), out userChoice);
                Console.Clear();
                if(userChoice == MenuMethods.Length+1) break;
                if(userChoice > 0 && userChoice < MenuMethods.Length+1){
                    MenuMethods[userChoice-1].Invoke();
                }
                else Console.WriteLine("Invalid option selected");
            }while(true);
        }

        private void PrintMenu(){
            for(int i = 0; i < MenuText.Length; i++){
                Console.WriteLine(MenuText[i]);
            }
        }
    }
}
