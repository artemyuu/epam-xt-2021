using System;

namespace Task_2._1{
    class Program{
        static void Main(String[] args){
            char[] arr = {'H','e','l','l','o',' ','w','o','r','l','d','!'};
            char[] arr1 = {'H','e','l','l'};
            MyString str = new MyString(arr);
            MyString str1 = new MyString(new char[]{'E','P','A','M'});
            str1.Concat(str1);
            str1.Print();
            str1.Reverse();
            str1.Print();
            Console.WriteLine(str.Equal(arr));
            Console.WriteLine(str1.Find('E'));
        }
    }

    class MyString{
        private char[] _data = {'\0'};

        public char this[int i]{
            get { return _data[i]; }
            set { this._data[i] = value; }
        }

        public int Length{
            get {
                int i = -1;
                do {
                    i++;
                } while(_data[i] != '\0');
                return i;
            }
        }

        public MyString(){}

        public MyString(char[] str){
            char[] buff = new char[str.Length+1];
            for(int i = 0; i < buff.Length; i++){
                if(i == buff.Length-1) buff[i] = '\0';
                else buff[i] = str[i];
            }
            _data = buff;
        }

        public void Print(){
            Console.Write(_data);
        }

        public bool Equal(MyString str){
            if(str.Length != this.Length) return true;
            for(int i = 0; i < str.Length; i++){
                if(str[i] != _data[i]) return true;
            }
            return false;
        }

        public int Equal(char[] str){
            if(str.Length != this.Length) return 0;
            for(int i = 0; i < str.Length; i++){
                if(str[i] != _data[i]) return 0;
            }
            return 1;
        }

        public void Concat(char[] str){
            char[] buff = new char[str.Length + this.Length + 1];
            for(int i = 0; i < buff.Length; i++){
                if(i == buff.Length - 1){
                    buff[i] = '\0';
                    break;
                }
                if(i < this.Length){
                    buff[i] = _data[i];
                }
                else {
                    buff[i] = str[i-this.Length];
                }
            }
            _data = buff;
        }

        public void Concat(MyString str){
            char[] buff = new char[str.Length + this.Length + 1];
            for(int i = 0; i < buff.Length; i++){
                if(i == buff.Length - 1){
                    buff[i] = '\0';
                    break;
                }
                if(i < this.Length){
                    buff[i] = _data[i];
                }
                else {
                    buff[i] = str[i-Length];
                }
            }
            _data = buff;
        }

        public int Find(char sym){
            for(int i = 0; i < this.Length; i++){
                if(_data[i] == sym) return i;
            }
            return -1;
        }

        public void Reverse(){
            char[] buff = new char[this.Length + 1];
            for(int i = this.Length-1, j = 0; i > -1; i--, j++){
                buff[j] = this._data[i];
            }
            buff[this.Length] = '\0';
            this._data = buff;
        }

        public char[] ToArray(){
            return _data;    
        }

        public override string ToString(){
            return new string(_data);
        }
    }
}
