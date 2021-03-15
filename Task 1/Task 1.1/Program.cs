using System;
using System.Collections.Generic;

namespace Task_1._1
{
    class Program
    {
        static void Main(string[] args)
        {   
            // Rectangle();
            // Triangle();
            // AnotherTriangle();
            XmasTree();
            // Console.WriteLine(SumOfNumbers(1000)); 
            // FontAdjustment(new Dictionary<string, bool>(3) {{"Bold", false}, {"Italic", false}, {"Underline", false}});
            // ArrayProcessing();
            // NoPositive();
            // NonNegativeSum();
            // TwoDArray();
        }
        public static double Rectangle(){
            Console.WriteLine("Enter width: ");
            Double.TryParse(Console.ReadLine(), out double a);
            Console.WriteLine("Enter height: ");
            Double.TryParse(Console.ReadLine(), out double b);
            if(a > 0 && b > 0){
                Console.Write("Sum: {0}", a*b+"\n");
                return a*b;
            }
            Console.WriteLine("Incorrect input");
            return 0;
        }
        public static void Triangle(){
            Console.WriteLine("Enter the number of lines: ");
            Int32.TryParse(Console.ReadLine(), out int N);
            if(N > 0){
                for(int i = 1; i < N+1; i++){
                    for(int j = i; j != 0; j--){
                        Console.Write('*');
                    }
                    Console.WriteLine();
                }
            }
        }
        public static void AnotherTriangle(){
            Console.WriteLine("Enter the number of lines: ");
            Int32.TryParse(Console.ReadLine(), out int N);
            if(N > 0){
                for(int i = 0; i < N; i++){
                    for(int j = 1; j < N*2; j++){
                        if(j > ((N*2-1)-(i*2+1))/2 && j <= (((N*2-1)+(i*2+1))/2)){
                            Console.Write('*');
                        }
                        else {
                            Console.Write(' ');
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
        public static void XmasTree(){
            Console.WriteLine("Enter the number of lines: ");
            Int32.TryParse(Console.ReadLine(), out int N);
            if(N > 0){
                for(int k = 1; k < N+1; k++){
                    for(int i = 0; i < k; i++){
                        for(int j = 1; j < N*2; j++){
                            if(j > ((N*2-1)-(i*2+1))/2 && j <= ((N*2-1)+(i*2+1))/2){
                                Console.Write('*');
                            }
                            else {
                                Console.Write(' ');
                            }
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
        public static int SumOfNumbers(int n){
            if(n > 0){
                n = n-1;
                int sumMultOfThree = 3*(n/3*(n/3+1)/2); 
                int sumMultOfFive = 5*(n/5*(n/5+1)/2);
                int repetitions = 15*(n/15*(n/15+1)/2);
                return sumMultOfThree + sumMultOfFive - repetitions;
            }
            return 0;
        }
        public static void FontAdjustment(Dictionary<string,bool> fontParams) { 
            while(true){
                List<string> activeParamsList = new List<string>();
                List<string> keyList = new List<string>(fontParams.Keys);
                foreach(KeyValuePair<string, bool> kvp in fontParams){
                    if(kvp.Value){
                        activeParamsList.Add(kvp.Key);
                    }
                }
                Console.WriteLine("Парметры надписи: " + (activeParamsList.Count == 0 ? "None" : String.Join(", ", activeParamsList.ToArray())));
                for(int i = 1; i < keyList.Count+1; i++){
                    Console.WriteLine("\t"+i+": " + keyList[i-1].ToLower());
                }
                Console.WriteLine("\t0: Выход");
                Int32.TryParse(Console.ReadLine(), out int choise);
                if(choise == 0) break;
                else if(choise > 0 && choise < 4) {
                    fontParams[keyList[choise-1]] = !fontParams[keyList[choise-1]];
                }
                else {
                    Console.WriteLine("Такой опции не существует");
                }
            }
        }

        public static void ArrayProcessing(){
            Random random = new Random();
            int[] array = new int[random.Next(5,20)];
            for(int i = 0; i < array.Length; i++){
                array[i] = random.Next(-50,50);
            }

            int max = array[0];
            int min = array[0];
            for(int i = 1; i < array.Length; i++){
                if(array[i] > max) max = array[i];
                if(array[i] < min) min = array[i];
            }

            Console.WriteLine("Initial array: [{0}]", string.Join(", ", array));
            Console.WriteLine("Max: {0}, Min: {1}", max, min);

            int tmp = 0;
            for (int i = 0; i < array.Length; i++) {
                for (int j = 0; j < array.Length - 1; j++) {
                    if (array[j] > array[j + 1]) {
                        tmp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = tmp;
                    }
                }
            }
            Console.WriteLine("Sorted array: [{0}]", string.Join(", ", array));
        }

        public static void NoPositive(){
            Random random = new Random();
            int[,,] array = new int[random.Next(5,20), random.Next(5,20), random.Next(5,20)];
            for (int i = 0; i < array.GetLength(0); i++){
                for (int j = 0; j < array.GetLength(1); j++){
                    for (int k = 0; k < array.GetLength(2); k++){
                            array[i, j, k] = random.Next(-50, 50);
                    }
                }
            }

            for (int i = 0; i < array.GetLength(0); i++){
                for (int j = 0; j < array.GetLength(1); j++){
                    for (int k = 0; k < array.GetLength(2); k++){
                            if(array[i, j, k] > 0) array[i, j, k] = 0;
                    }
                }
            }
        }

        public static int NonNegativeSum(){
            int sum = 0;
            Random random = new Random();
            int[] array = new int[random.Next(5,20)];
            for(int i = 0; i < array.Length; i++){
                array[i] = random.Next(-50,50);
                if(array[i] > 0) sum += array[i];
            }
            Console.WriteLine("Array: [{0}]", string.Join(", ", array));
            Console.WriteLine("Sum: {0}", sum);
            return sum;
        }

        public static int TwoDArray(){
            int sum = 0;
            Random random = new Random();
            int[,] array = new int[random.Next(2,5), random.Next(2,5)];
            for(int i = 0; i < array.GetLength(0); i++){
                for(int j = 0; j < array.GetLength(1); j++){
                    array[i,j] = random.Next(-50,50);
                    Console.Write(string.Format("{0} ", array[i, j]));
                    if( (i+j) %2 == 0){
                        sum+= array[i,j];
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Sum: {0}", sum);
            return sum;
        }
    }
}
