using System;
using System.Collections;
using System.Collections.Generic;

namespace Task_3._2
{
    class Program{
        static void Main(string[] args){
            List<int> test = new List<int>{0,1,2,3,4,5,6,7};
            DynamicArray<int> da = new DynamicArray<int>(test);
            foreach(var item in da){
                Console.WriteLine(item);
            }
        }
    }
    class DynamicArray<T> : IEnumerable<T>, IEnumerable{
        private T[] _data;

        public T this[int i]{
            get {
                if(i > -1 && i < Length) return _data[i]; 
                else throw new ArgumentOutOfRangeException();
            }
            set { 
                if(i > -1 && i < Length) this._data[i] = value;
                else throw new ArgumentOutOfRangeException();
            }
        }
        public int Length{get; private set;} = 0;
        public int Capacity{get; private set;}

        public DynamicArray(){
            Capacity = 8;
            _data = new T[Capacity];
        }

        public DynamicArray(int capacity){
            if(capacity > 0){
                Capacity = capacity;
                _data = new T[Capacity];
            }
            else throw new ArgumentException("Capacity must be greather than 0");
        }

        public DynamicArray(IEnumerable<T> collection){
            Capacity = 8;
            _data = new T[Capacity];
            foreach (var item in collection){
                Add(item);
            }
        }

        public void Add(T element){
            if(Length == Capacity){
                Resize(Capacity * 2);
            }
            Length += 1;
            _data[Length - 1] = element;
        }

        public void AddRange(IEnumerable<T> collection){
            int countCollectionItems = 0;
            foreach (var item in collection){
                countCollectionItems++;
            }
            if(countCollectionItems + Length > Capacity){
                int powOfTwoForCapacity = (int)Math.Ceiling(Math.Log2(countCollectionItems + Length));
                int newCapacity = (int)Math.Pow(2, powOfTwoForCapacity);
                Resize(newCapacity);
            }
            foreach(var item in collection){
                Add(item);
            }
        }

        public bool Remove(int index){
            try{
                if(index >= 0 && index < Length){
                    T[] buff = new T[Capacity];
                    for(int i = 0, j = 0; i < Length; i++){
                        if(i != index) {
                            buff[j] = _data[i];
                            j++;
                        }
                    }
                    Length--;
                    _data = buff;
                    return true;
                }
                throw new ArgumentException("Remove error: Incorrect index");
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool Insert(int index, T item){
            try{
                if(index >= 0 && index < Length){
                    if(Length + 1 > Capacity) Resize(Capacity+1);
                    T[] buff = new T[Capacity];
                    for(int i = 0, j = 0; i < Length; i++){
                        if(i != index) {
                            buff[j] = _data[i];
                            j++;
                        }
                        else {
                            buff[j] = item;
                            buff[j+1] = _data[i];
                            j+=2;
                        }
                    }
                    Length++;
                    _data = buff;
                    return true;
                }
                throw new ArgumentException("Insert error: Incorrect index");
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        private void Resize(int newCapacity){
            Capacity = newCapacity;
            T[] buff = new T[Capacity];
            for(int i = 0; i < Length; i++){
                buff[i] = _data[i];
            }
            _data = buff;
        }

        public virtual IEnumerator<T> GetEnumerator(){
            for (int i = 0; i < Length; i++){
                yield return _data[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();
    }
}
