using System;
using System.Collections.Generic;

namespace Task_3._1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите N");
            Int32.TryParse(Console.ReadLine(), out int N);
            Console.WriteLine("Введите, какой по счету человек будет вычеркнут каждый раунд:");
            Int32.TryParse(Console.ReadLine(), out int eachN);
            WeakestLink weakestLink = new WeakestLink(N, eachN);
            weakestLink.StartGame();
        }
    }

    class WeakestLink{
        private List<Person> _personList;
        private int _eachN;

        public WeakestLink(int N, int eachN){
            _personList = new List<Person>(N);
            _eachN = eachN;
            FillPersonsList();
        }

        public void FillPersonsList(){
            for(int i = 0; i < _personList.Capacity; i++){
                _personList.Add(new Person("Name " + (i+1)));
            }   
        }

        public void StartGame(){
            _personList = RecursiveDelete(_personList, new List<Person>());
            Console.Write("Остались: ");
            for(int i = 0; i < _personList.Count; i++){
                Console.Write(_personList[i].Name+" ");
            }
            Console.WriteLine("игроки");
        }

        public List<Person> RecursiveDelete(List<Person> startList, List<Person> finalList, int index = 1){
            string indexDeletedPersons = "";
            if(startList.Count < _eachN){
                Console.WriteLine("Больше никого не вычеркнуть. Игра окончена");
                return startList;
            } 
            for(int i = 0; i < startList.Count; i++, index++){
                if(index % _eachN != 0){
                    finalList.Add(startList[i]);
                } 
                else indexDeletedPersons += $"{startList[i].Name} ";
            }
            Console.WriteLine("Были удалены: " + indexDeletedPersons + "игроки");
            return RecursiveDelete(finalList, new List<Person>(), index);
        }
    }

    class Person{
        public string Name {get; private set;}
        public Person(string name){
            Name = name;
        }
    }

}
