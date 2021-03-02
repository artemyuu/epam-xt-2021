using System;
using System.Linq;
using System.Text;

namespace Task_1._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Averagers("Викентий хорошо отметил день рождения: покушал пиццу, посмотрел кино, пообщался со студентами в чате"));
            Console.WriteLine(Doubler("написать программу, которая", "описание") == "ннааппииссаать ппроограамму, коотоораая");
            Console.WriteLine(Lowercase("Антон хорошо начал утро: послушал Стинга, выпил кофе и посмотрел Звёздные Войны"));
            Console.WriteLine(Validator("я плохо учил русский язык. забываю начинать предложения с заглавной. хорошо, что можно написать программу!"));
        }

        public static int Averagers(string inputStr){
            int buff = 0;
            string[] words = new string(inputStr.Where(c => !char.IsPunctuation(c)).ToArray()).Split(' ');
            for(int i = 0; i < words.Length; i++){
                buff += words[i].Length;
            }
            return buff / words.Length; //Возвращает целочисленное значение
        }

        public static string Doubler(string inputStr, string doubler){
            StringBuilder resultStr = new StringBuilder();
            string doublerFiltered = new string(doubler.Distinct().ToArray());
            for(int i = 0; i < inputStr.Length; i++){
                for(int j = 0; j < doublerFiltered.Length; j++){
                    if(inputStr[i] == doublerFiltered[j]) resultStr.Append(inputStr[i]);
                }
                resultStr.Append(inputStr[i]);
            }
            return resultStr.ToString();
        }
        
        public static int Lowercase(string inputStr){
            int lowercaseCounter = 0;
            string[] words = new string(inputStr.Where(c => !char.IsPunctuation(c)).ToArray()).Split(' ');
            for(int i = 0; i < words.Length; i++){
                if(!Char.IsUpper(words[i][0])) lowercaseCounter++;
            }
            return lowercaseCounter;
        }

        public static string Validator(string inputString){
            StringBuilder resultStr = new StringBuilder(inputString);
            resultStr[0] = Char.ToUpper(resultStr[0]);
            for(int i = 1; i < resultStr.Length - 1; i++){
                if(resultStr[i] == '.' || resultStr[i] == '?' || resultStr[i] == '!') resultStr[i + 2] = Char.ToUpper(resultStr[i + 2]);
            }
            return resultStr.ToString();
        }
    }
}
