using System;

namespace Task_3._3._1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] testArray = new int[]{1,1,2,3,4,5};
            testArray.ForEach( (x) => x+1 );

            testArray.ForEach( (x) => {
                System.Console.WriteLine(x);
                return x;
            });

            Console.WriteLine("SumOfAll: " + testArray.SumOfAll());
            Console.WriteLine("Average: " + testArray.Average());
            Console.WriteLine("MostFrequentElement: " + testArray.MostFrequentElement());
        }
    }

    static class ArrayExtension{

        public static void ForEach(this int[] array, Func<int, int> func){
            if(func != null){
                for(int i = 0; i < array.Length; i++){
                    array[i] = func(array[i]);
                }
            }
        }

        public static int SumOfAll(this int[] array){
            int accum = 0;
            for(int i = 0; i < array.Length; i++){
                accum += array[i];
            }
            return accum;
        }

        public static double Average(this int[] array) => array.SumOfAll() / array.Length;

        public static int MostFrequentElement(this int[] array){
            int num = array[0];
            int maxFrq = 1;
            for(int i = 0; i < array.Length - 1; i++){
                int frq = 1;
                for(int j = i + 1; j < array.Length; j++){
                    if(array[i] == array[j]) frq++;
                }
                if (frq > maxFrq){
                    maxFrq = frq;
                    num = array[i];
                }
            }
            return num;
        }
    }
}
