using System.Collections.Generic;
using System;

namespace Task_2._2
{
    class Program
    {
        static void Main(string[] args)
        {
            PaintApp app = new PaintApp();
            app.StartApp(); 
        }
    }

    abstract class Figure{
        private Point _startPoint;

        public Point StartPoint {get => _startPoint;} 
        public Figure(Point startPoint){
            _startPoint = startPoint;
        }

        public virtual void PrintFigure(){
            Console.WriteLine("Точка");
            Console.WriteLine("Расположение:");
            Console.WriteLine("X1:{0}", StartPoint.X);
            Console.WriteLine("Y1:{0}", StartPoint.X);
        }
    }
    abstract class AreaFigure : Figure{
        public double Area {get;} 

        public AreaFigure(Point startPoint):base(startPoint){}
    }

    class Point{
        private double _X;
        private double _Y;

        public double X {get => this._X;}
        public double Y {get => this._Y;}

        public Point(double x, double y){
            this._X = x;
            this._Y = y;
        }
    }

    class Line : Figure{
        private Point _endPoint;
        public Point EndPoint {get => this._endPoint;}
        public double Length {get => Math.Sqrt(Math.Pow((EndPoint.X - StartPoint.X),2) + Math.Pow((EndPoint.Y-StartPoint.Y),2));}

        public Line(Point startPoint, Point endPoint):base(startPoint){
            _endPoint = endPoint;
        } 
        public override void PrintFigure(){
            Console.WriteLine("Линия");
            Console.WriteLine("Расположение");
            Console.WriteLine("X1:{0}", StartPoint.X);
            Console.WriteLine("Y1:{0}", StartPoint.Y);
            Console.WriteLine("Длина:{0}", Length);
        }
    }

    class Rectangle : AreaFigure{
        private Point _endPoint;
        private double _horisontalLineLength;
        private double _verticalLineLength;

        public Point EndPoint {get => this._endPoint;}
        public double HorisontalLength {get => _horisontalLineLength;}
        public double VerticalLength {get => _verticalLineLength;}
        public double Diagonal {get => Math.Sqrt(Math.Pow((EndPoint.X - StartPoint.X),2) + Math.Pow((EndPoint.Y-StartPoint.Y),2));}
        public new double Area {get => _horisontalLineLength * _verticalLineLength;}

        public Rectangle(Point startPoint, Point endPoint): base(startPoint){
            _endPoint = endPoint;
            _horisontalLineLength = EndPoint.X - StartPoint.X;
            _verticalLineLength = EndPoint.Y - StartPoint.Y;
        } 

        public override void PrintFigure(){
            Console.WriteLine("Прямоугольник");
            Console.WriteLine("Расположение");
            Console.WriteLine("X1:{0}", StartPoint.X);
            Console.WriteLine("Y1:{0}", StartPoint.Y);
            Console.WriteLine("Площадь:{0}", Area);
        }
    }

    class Triangle : AreaFigure{
        private Point _heightPoint;
        private Line _baseLine;
        private Line _leftLine;
        private Line _rightLine;

        public Point HeightPoint {get => _heightPoint;}
        public Line BaseLine {get => _baseLine;}
        public new double Area {
            get {
                double halfPerimetr = this.Perimetr / 2;
                return Math.Sqrt(
                halfPerimetr * 
                (halfPerimetr - _baseLine.Length) * (halfPerimetr - _rightLine.Length) * (halfPerimetr - _leftLine.Length)
                );
            }
        }
        public double Perimetr {get => _baseLine.Length + _leftLine.Length + _rightLine.Length;}

        public Triangle(Point heightPoint, Line baseLine): base(heightPoint){
            _heightPoint = heightPoint;
            _baseLine = baseLine; 
            _leftLine = new Line(baseLine.StartPoint, _heightPoint);
            _rightLine = new Line(baseLine.EndPoint, _heightPoint);
        }
        public override void PrintFigure(){
            Console.WriteLine("Треугольник");
            Console.WriteLine("Расположение:");
            Console.WriteLine("X1:{0}", StartPoint.X);
            Console.WriteLine("Y1:{0}", StartPoint.Y);
            Console.WriteLine("Площадь:{0}", Area);
        }
    }

    class Square : AreaFigure{
        private double _side; 
        public double Side {get => _side;}
        public new double Area {get => _side * _side;}
        public Square(Point startPoint, double side): base(startPoint){
            _side = side;
        }

        public override void PrintFigure(){
            Console.WriteLine("Квадрат");
            Console.WriteLine("Расположение:");
            Console.WriteLine("X1:{0}", StartPoint.X);
            Console.WriteLine("Y1:{0}", StartPoint.Y);
            Console.WriteLine("Площадь:{0}", Area);
        }
    }

    class Circum : Figure{
        private double _radius;
        public double Radius {get => _radius;} 
        public double CircumLength {get => 2 * Math.PI * this.Radius;} 

        public Circum(Point centerPoint, double radius): base(centerPoint){
            _radius = radius;
        } 

        public override void PrintFigure(){
            Console.WriteLine("Окружность");
            Console.WriteLine("Расположение:");
            Console.WriteLine("X1:{0}", StartPoint.X);
            Console.WriteLine("Y1:{0}", StartPoint.Y);
            Console.WriteLine("Длина окружности:{0}", CircumLength);
        }
    }

    class Circle : AreaFigure{
        private Circum _circum;
        public double Radius {get => _circum.Radius;} 
        public double CircumLength {get => 2 * Math.PI * _circum.Radius;} 
        public new double Area {get => Math.PI * Math.Pow(_circum.Radius,2);}

        public Circle(Point centerPoint, double radius) : base(centerPoint){
            _circum = new Circum(centerPoint, radius);
        } 

        public override void PrintFigure(){
            Console.WriteLine("Круг");
            Console.WriteLine("Расположение:");
            Console.WriteLine("X1:{0}", StartPoint.X);
            Console.WriteLine("Y1:{0}", StartPoint.Y);
            Console.WriteLine("Площадь:{0}", Area);
        }
    }

    class Ring : AreaFigure{
        private Circle _innerCircle;
        private Circle _outerCircle;

        public new double Area {get => _outerCircle.Area - _innerCircle.Area;}
        public double InnerRadius {get => _innerCircle.Radius;}
        public double OuterRadius {get => _outerCircle.Radius;}
        public double CircumsLength {get => _innerCircle.CircumLength + _outerCircle.CircumLength;}

        public Ring(Point startPoint, double innerRadius, double outerRadius): base(startPoint){
            _innerCircle = new Circle(startPoint, innerRadius);
            _outerCircle = new Circle(startPoint, outerRadius);
        }     
    }

    class User{
        private List<Figure> _createdFiguresByUser;
        private string _userName;

        public List<Figure> CreatedFiguresByUser {get => _createdFiguresByUser;}
        public string UserName {get => _userName;}
        
        public User(List<Figure> createdFiguresByUser, string userName){
            _createdFiguresByUser = createdFiguresByUser;
            _userName = userName;
        }

        public void AddFigure(Figure figure){
            _createdFiguresByUser.Add(figure);
        }
    }

    class PaintApp{
        private List<Figure> _CREATED_FIGURES = new List<Figure>();
        private List<User> _CREATED_USERS = new List<User>();
        private User _currentUser;
        private string[] _MAIN_MENU_TEXT = {
            "Выбирите действие:",
            "1.Добавить фигуру",
            "2.Вывести фигуры",
            "3.Очистить холст",
            "4.Выход"
        };
        private string[] _USER_MENU_TEXT = {
            "Выбирите действие:",
            "1.Добавить пользователя",
            "2.Выбрать пользователя",
            "3.Очистить список пользователей",
            "4.Выход"
        };
        private string[] _TYPES_OF_FIGURES_MENU_TEXT = {
            "Выбирите тип фигуры:",
            "1.Добавить квадрат",
            "2.Добавить окружность",
            "3.Добавить кольцо",
            "4.Добавить круг",
            "5.Добавить прямоугольник",
            "6.Добавить линию",
            "7.Добавить треугольник",
            "8.Выход"
        };
        private Action[] _MAIN_MENU_METHODS;
        private Action[] _ADD_FIGURES_METHODS;
        private Action[] _USER_MENU_METHODS;

        public PaintApp(){
            _MAIN_MENU_METHODS = new Action[] {
                () => AddFigure(),
                () => PrintFigures(),
                () => ClearCanvas(),
            };

            _USER_MENU_METHODS = new Action[] {
                () => AddUser(),
                () => СhooseUser(),
                () => ClearUsers(),
            };

            _ADD_FIGURES_METHODS = new Action[] {
                () => AddSquare(),
                () => AddCircle(),
                () => AddRing(),
                () => AddCircle(),
                () => AddRectangle(),
                () => AddLine(),
                () => AddTriangle(),
            };
        }
        public void StartApp(){
            menuController(_USER_MENU_TEXT, _USER_MENU_METHODS);
        }
        private void menuController(string[] textArray, Action[] METHODsArray){
            do{
                PrintArray(textArray);
                Int32.TryParse(Console.ReadLine(),out int userChoice);
                userChoice--;
                if(userChoice > -1 && userChoice <= METHODsArray.Length){
                    if(userChoice == METHODsArray.Length) break;
                    METHODsArray[userChoice].Invoke();
                }
                else Console.WriteLine("Выбрано несуществующее действие");
            }while(true);
        }
        
        private void AddFigure(){
            menuController(this._TYPES_OF_FIGURES_MENU_TEXT, _ADD_FIGURES_METHODS);
        }

        private void AddUser(){
            Console.WriteLine("Введите имя новго пользователя");
            string userName = Console.ReadLine();
            _CREATED_USERS.Add(new User(new List<Figure>(), userName));
            Console.WriteLine("Пользователь успешно создан");
        }
        private void ClearUsers(){
            _CREATED_USERS.Clear();
        }
        private void СhooseUser(){
            while(true){
                Console.WriteLine("Введите имя существющего пользователя");
                string userName = Console.ReadLine();
                for(int i = 0; i < _CREATED_USERS.Count; i++){
                    if(_CREATED_USERS[i].UserName == userName){
                        _currentUser = _CREATED_USERS[i];
                        menuController(_MAIN_MENU_TEXT, _MAIN_MENU_METHODS);
                        return;
                    }
                }
                Console.WriteLine("Не существует такого пользователя");
            }
        }
        private void AddSquare(){
            Console.WriteLine("Введите координаты(x,y) левой верхней точки");
            Console.WriteLine("X: ");
            Double.TryParse(Console.ReadLine(),out double startX);
            Console.WriteLine("Y: ");
            Double.TryParse(Console.ReadLine(),out double startY);
            Console.WriteLine("Введите длину стороны");
            Double.TryParse(Console.ReadLine(),out double side);
            _currentUser.AddFigure(new Square(new Point(startX, startY), side));
            Console.WriteLine("Квадрат успешно добавлен!");
        }
        private void AddRing(){
            Console.WriteLine("Введите центра(x,y) круга");
            Console.WriteLine("X: ");
            Double.TryParse(Console.ReadLine(),out double startX);
            Console.WriteLine("Y: ");
            Double.TryParse(Console.ReadLine(),out double startY);
            double innerRadius;
            double outerRadius;
            while(true){
                Console.WriteLine("Введите длину внутреннего радиуса");
                Double.TryParse(Console.ReadLine(),out innerRadius);
                Console.WriteLine("Введите длину внешнего радиуса");
                Double.TryParse(Console.ReadLine(),out outerRadius);
                if(innerRadius < outerRadius) break;
                else Console.WriteLine("Внутренний радиус не может быть меньше внешнего!");
            }
            _currentUser.AddFigure(new Ring(new Point(startX, startY), innerRadius, outerRadius));
            Console.WriteLine("Кольцо успешно добавлено!");
        }
        private void AddCircle(){
            Console.WriteLine("Введите координаты центра(x,y) круга");
            Console.WriteLine("X: ");
            Double.TryParse(Console.ReadLine(),out double startX);
            Console.WriteLine("Y: ");
            Double.TryParse(Console.ReadLine(),out double startY);
            Console.WriteLine("Введите радиус");
            Double.TryParse(Console.ReadLine(),out double radius);
            _currentUser.AddFigure(new Circle(new Point(startX,startY), radius));
            Console.WriteLine("Круг успешно добавлен!");
        }
        private void AddCircum(){
            Console.WriteLine("Введите координаты центра(x,y) окружности");
            Console.WriteLine("X: ");
            Double.TryParse(Console.ReadLine(),out double startX);
            Console.WriteLine("Y: ");
            Double.TryParse(Console.ReadLine(),out double startY);
            Console.WriteLine("Введите радиус");
            Double.TryParse(Console.ReadLine(),out double radius);
            _currentUser.AddFigure(new Circle(new Point(startX,startY), radius));
            Console.WriteLine("Окружность успешно добавлена!");
        }
        private void AddRectangle(){
            Console.WriteLine("Введите координаты(x,y) левой верхней точки");
            Console.WriteLine("X: ");
            Double.TryParse(Console.ReadLine(),out double startX);
            Console.WriteLine("Y: ");
            Double.TryParse(Console.ReadLine(),out double startY);
            Console.WriteLine("Введите длину стороны");
            Double.TryParse(Console.ReadLine(),out double side);
            _currentUser.AddFigure(new Square(new Point(startX, startY), side));
            Console.WriteLine("Прямоугольник успешно добавлен!");
        }
        private void AddLine(){
            Console.WriteLine("Введите координаты(x,y) точки начала линии");
            Console.WriteLine("X: ");
            Double.TryParse(Console.ReadLine(),out double startX);
            Console.WriteLine("Y: ");
            Double.TryParse(Console.ReadLine(),out double startY);
            Console.WriteLine("Введите координаты(x,y) точки конца линии");
            Console.WriteLine("X: ");
            Double.TryParse(Console.ReadLine(),out double endX);
            Console.WriteLine("Y: ");
            Double.TryParse(Console.ReadLine(),out double endY);
            _currentUser.AddFigure(new Line(new Point(startX, startY), new Point(endX, endY)));
            Console.WriteLine("Линия успешно добавлена!");
        }
        private void AddTriangle(){
            Console.WriteLine("Введите координаты(x,y) точки начала линии основания");
            Console.WriteLine("X: ");
            Double.TryParse(Console.ReadLine(),out double startX);
            Console.WriteLine("Y: ");
            Double.TryParse(Console.ReadLine(),out double startY);
            Console.WriteLine("Введите координаты(x,y) точки конца линии основания");
            Console.WriteLine("X: ");
            Double.TryParse(Console.ReadLine(),out double endX);
            Console.WriteLine("Y: ");
            Double.TryParse(Console.ReadLine(),out double endY);
            Console.WriteLine("Введите координаты(x,y) точки высоты треугольника");
            Console.WriteLine("X: ");
            Double.TryParse(Console.ReadLine(),out double heightX);
            Console.WriteLine("Y: ");
            Double.TryParse(Console.ReadLine(),out double heightY);
            _currentUser.AddFigure(new Triangle(new Point(heightX, heightY),new Line(new Point(startX, startY), new Point(endX, endY))));
            Console.WriteLine("Треугольник успешно добавлен!");
        }
        private void ClearCanvas(){
             _currentUser.CreatedFiguresByUser.Clear();
        }
        private void PrintArray(string[] array){
            for(int i = 0; i < array.Length; i++){
                Console.WriteLine(array[i]); 
            }
        }
        private void PrintFigures(){
            if(_currentUser.CreatedFiguresByUser.Count == 0){
                Console.WriteLine("Не существует созданных фигур");
                return;
            }
            for(int i = 0; i < _currentUser.CreatedFiguresByUser.Count; i++){
                Console.WriteLine(new string('-', 20)); 
                _currentUser.CreatedFiguresByUser[i].PrintFigure();
                Console.WriteLine(new string('-', 20));  
            }
        }
    }
}