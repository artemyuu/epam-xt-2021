using System;

namespace Task_3._3._2
{
    class Program{
        static void Main(string[] args){
            string russianString = "Привет";
            Console.WriteLine(russianString);
            Console.WriteLine("IsRussian: " + russianString.IsRussian()); 
            Console.WriteLine("IsEnglish: " + russianString.IsEnglish()); 
            Console.WriteLine("IsNumber: " + russianString.IsNumber()); 
            Console.WriteLine("IsMixed: " + russianString.IsMixed()); 
            Console.WriteLine();

            string englishString = "Hello";
            Console.WriteLine(englishString);
            Console.WriteLine("IsRussian: " + englishString.IsRussian()); 
            Console.WriteLine("IsEnglish: " + englishString.IsEnglish()); 
            Console.WriteLine("IsNumber: " + englishString.IsNumber()); 
            Console.WriteLine("IsMixed: " + englishString.IsMixed()); 
            Console.WriteLine();

            string numberString = "123";
            Console.WriteLine(numberString);
            Console.WriteLine("IsRussian: " + numberString.IsRussian()); 
            Console.WriteLine("IsEnglish: " + numberString.IsEnglish()); 
            Console.WriteLine("IsNumber: " + numberString.IsNumber()); 
            Console.WriteLine("IsMixed: " + numberString.IsMixed()); 
            Console.WriteLine();

            string mixedString = "123abcабв";
            Console.WriteLine(mixedString);
            Console.WriteLine("IsRussian: " + mixedString.IsRussian()); 
            Console.WriteLine("IsEnglish: " + mixedString.IsEnglish()); 
            Console.WriteLine("IsNumber: " + mixedString.IsNumber()); 
            Console.WriteLine("IsMixed: " + mixedString.IsMixed()); 
        }
    }

    static class StringExtension{
        private static char startCharCyrillic = 'Ѐ';
        private static char endCharCyrillic = 'ӿ';
        private static char startCharLatin = 'A';
        private static char endCharLatin = 'Z';
        private static char startCharNumber = '0';
        private static char endCharNumber = '9';

        public static bool IsRussian(this string str){
            for(int i = 0; i < str.Length; i++){
                if(!CheckCharDiapason(str[i], startCharCyrillic, endCharCyrillic)) return false;
            }
            return true;
        }
        public static bool IsEnglish(this string str){;
            for(int i = 0; i < str.Length; i++){
                if(!CheckCharDiapason(Char.ToUpper(str[i]), startCharLatin, endCharLatin)) return false;
            }
            return true;
        }
        public static bool IsNumber(this string str){
            for(int i = 0; i < str.Length; i++){
                if(!CheckCharDiapason(str[i], startCharNumber, endCharNumber)) return false;
            }
            return true;
        }

        public static bool IsMixed(this string str){
            bool CyrChar = false;
            bool LatChar = false;
            bool NumChar = false;
            
            if(str.IsRussian()) return false;
            if(str.IsNumber()) return false;
            if(str.IsEnglish()) return false;

            for(int i = 0; i < str.Length; i++){
                if(CheckCharDiapason(str[i], startCharCyrillic, endCharCyrillic)) CyrChar = true;
                if(CheckCharDiapason(Char.ToUpper(str[i]), startCharLatin, endCharLatin)) LatChar = true;
                if(CheckCharDiapason(str[i], startCharNumber, endCharNumber)) NumChar = true;
            }

            if(CyrChar && LatChar && NumChar == false) return false;
            return true;

        }
        private static bool CheckCharDiapason(char ch, char startChar, char endChar){
            if(!(ch >= startChar && ch <= endChar)) return false;
            return true;
        }
    }
}
