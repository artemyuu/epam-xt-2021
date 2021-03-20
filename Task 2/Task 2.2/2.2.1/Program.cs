using System;
using System.Collections.Generic;
using System.Threading;

namespace _2._2._1
{
    class Program
    {
        static void Main(string[] args)
        {	
					//for graphip use cmd chcp 65001
					Game game = new Game(new Map(30, 10));
					game.Start();
        }
    }
}

class GameObject{
	public char Sym{ get; set;}
	public int X{ get; set;}
	public int Y{ get; set;}
	
	public GameObject(int x, int y, char sym){
		X = x;
		Y = y;
		Sym = sym;
	}
}

class DynamicGameObject : GameObject{
	
	public DynamicGameObject(int x, int y, char sym): base(x, y, sym){}

	public void MoveRight(){
		this.X += 1;
	}
	public void MoveLeft(){
		this.X -= 1;
	}
	public void MoveUp(){
		this.Y -= 1;
	}
	public void MoveDown(){
		this.Y += 1;
	}

}

class Player : DynamicGameObject{
	public int Health{ get; set;}
	public int Coin{ get; set;}

	public Player(int x, int y, char sym): base(x, y, sym){
		Health = 3;
		Coin = 0;
	}
}

class Monster : DynamicGameObject{
	public Monster(int x, int y, char sym): base(x, y, sym){}
}


class StaticGameObject : GameObject{
	public StaticGameObject(int x, int y, char sym): base(x, y, sym){}
}

class Coin : StaticGameObject{
	public Coin(int x, int y, char sym): base(x, y, sym){}
}

class ConsoleDrawer{
	private Map _MAP;
	public ConsoleDrawer(Map map){
		_MAP = map;
	}

	public void ConsoleDrawHealthBar(){
		for(int i = 0; i < _MAP.Player.Health; i++){
			Console.Write("♡ ");
		}
		Console.WriteLine();
	}

	public void LogVictory(){
		Console.WriteLine("Y O U _ W I N");
	}
	public void LogLose(){
		Console.WriteLine("Y O U _ L O S E");
	}

	public void ConsoleDrawMap(){
		for(int y = 0; y < _MAP.Height; y++){
			for(int x = 0;  x < _MAP.Width; x++){
				bool findObj = false;
				for(int i = 0; i < _MAP.GameObjects.Count; i++){
						if(_MAP.GameObjects[i].X == x && _MAP.GameObjects[i].Y == y){
							Console.Write(_MAP.GameObjects[i].Sym);
							findObj = true;
						}
					}
					if(!findObj) Console.Write(' ');
				}
			Console.WriteLine();
		}
	}
}

class Map{
	private int _width;
	private int _height;
	private int _COUNT_OBS = 5;
	private List<GameObject> _GAME_OBJECTS = new List<GameObject>();
	private Player _PLAYER;

	private List<Monster> _MONSTERS = new List<Monster>();

	public Player Player{get => _PLAYER;}
	public List<Monster> Monsters{get => _MONSTERS;}
	public int Height{ get => _height;}
	public int Width{ get => _width;}
	public List<GameObject> GameObjects{ get => _GAME_OBJECTS;}

	public Map(int width, int height){
		_width = width;
		_height= height;
		RenderBorder();
		RenderObs();
		RenderPlayer();
		RenderCoin();
		RenderMonsters();
	}
	
  public void RenderBorder(){
    for(int y = 0; y < _height; y++){
			for(int x = 0;  x < _width; x++){
        if(y == 0 || y ==  _height-1) _GAME_OBJECTS.Add(new StaticGameObject(x, y, '█'));
        if((x == 0 || x == _width-1) && (y > 0 && y < _height-1)) _GAME_OBJECTS.Add(new StaticGameObject(x, y, '█'));
			}
		}
	}

	public void RenderPlayer(){
		_PLAYER = new Player(_width - 2 , _height - 2, '⍺');
		_GAME_OBJECTS.Add(_PLAYER);
	}

	public void RenderCoin(){
		_GAME_OBJECTS.Add(new Coin(1, 1,'⚬'));
		_GAME_OBJECTS.Add(new Coin(_width - 2, 1,'⚬'));
		_GAME_OBJECTS.Add(new Coin(1, _height - 2,'⚬'));
	}

	public void RenderMonsters(){
		Monster m1 = new Monster((_width / 2), 1,'ⶍ');
		Monster m2 = new Monster((_width / 2), _height-2,'ⶍ');
		_GAME_OBJECTS.Add(m1);
		_GAME_OBJECTS.Add(m2);
		_MONSTERS.Add(m1);
		_MONSTERS.Add(m2);
	}

  public void RenderObs(){
		Random rnd = new Random();
		for(int i = 0; i < _COUNT_OBS; i++){
			int x = rnd.Next(3, _width-2);
			int y = rnd.Next(1, _height-1);
			_GAME_OBJECTS.Add(new StaticGameObject(x,y,'█'));
		}
  }
}

class Game{
	private Map _MAP;
	private ConsoleDrawer cd;

	public Game(Map map){
			_MAP = map;
			cd = new ConsoleDrawer(_MAP);
	}

	public void Start(){
		GameLoop();
	}

	public void CoinCollision(){
		for(int i = 0; i < _MAP.GameObjects.Count; i++){
			if((_MAP.Player.X == _MAP.GameObjects[i].X) && (_MAP.Player.Y == _MAP.GameObjects[i].Y) && (_MAP.GameObjects[i] is Coin)){
				_MAP.Player.Coin += 1;
				_MAP.GameObjects.RemoveAt(i);
				break;
			}
		}
	}

	public void MonsterCollision(){
		for(int i = 0; i < _MAP.Monsters.Count; i++){
			if((_MAP.Player.X == _MAP.Monsters[i].X) && (_MAP.Player.Y == _MAP.Monsters[i].Y)){
				_MAP.Player.Health -= 1;
				break;
			}
		}
	}

	private bool[] FreeCells(GameObject gameObject){
		bool[] blockPositions = {true, true, true, true};
			for(int i = 0; i < _MAP.GameObjects.Count; i++){
			if(_MAP.GameObjects[i].Y == gameObject.Y - 1 && _MAP.GameObjects[i].X == gameObject.X && !(_MAP.GameObjects[i] is Coin) && !(_MAP.GameObjects[i] is DynamicGameObject)) blockPositions[0] = false;
			if(_MAP.GameObjects[i].X == gameObject.X - 1 && _MAP.GameObjects[i].Y == gameObject.Y && !(_MAP.GameObjects[i] is Coin) && !(_MAP.GameObjects[i] is DynamicGameObject)) blockPositions[1] = false;
			if(_MAP.GameObjects[i].Y == gameObject.Y + 1 && _MAP.GameObjects[i].X == gameObject.X && !(_MAP.GameObjects[i] is Coin) && !(_MAP.GameObjects[i] is DynamicGameObject)) blockPositions[2] = false;
			if(_MAP.GameObjects[i].X == gameObject.X + 1 && _MAP.GameObjects[i].Y == gameObject.Y && !(_MAP.GameObjects[i] is Coin) && !(_MAP.GameObjects[i] is DynamicGameObject)) blockPositions[3] = false;
		}
		return blockPositions;
	}

	public void MonstersMove(){
		Random rnd = new Random();
		for(int i = 0; i < _MAP.Monsters.Count; i++){
			bool changedPosition = false;
			bool[] blockPositions = FreeCells(_MAP.Monsters[i]);
			while(!changedPosition){
				int pos = rnd.Next(0, blockPositions.Length);
				if(blockPositions[pos] == true){
					changedPosition = true;
					switch(pos){
						case 0:
							_MAP.Monsters[i].MoveUp();
							break;
						case 1:
							_MAP.Monsters[i].MoveLeft();
							break;
						case 2:
							_MAP.Monsters[i].MoveDown();
							break;
						case 3:
							_MAP.Monsters[i].MoveRight();
							break;
					}
					MonsterCollision();
				}
			}
		}
	}

	public void PlayerController(ConsoleKeyInfo cki){
		bool[] freeCells = FreeCells(_MAP.Player);
		switch (cki.Key)
		{
			case ConsoleKey.W:
				if(freeCells[0]){
					_MAP.Player.MoveUp();
				}
				break;
			case ConsoleKey.A:
				if(freeCells[1]){
					_MAP.Player.MoveLeft();
				}
				break;
			case ConsoleKey.S:
				if(freeCells[2]){
					_MAP.Player.MoveDown();
				}
				break;
			case ConsoleKey.D:
				if(freeCells[3]){
					_MAP.Player.MoveRight();
				}
				break;
				
			default: break;
		}
		CoinCollision();
		MonsterCollision();
	}
	public void GameLoop(){
	ConsoleKeyInfo cki;
	do {
		while(Console.KeyAvailable == false){
			cd.ConsoleDrawMap();
			cd.ConsoleDrawHealthBar();
			MonstersMove();
			Thread.Sleep(600);
			Console.Clear();
		}
		cki = Console.ReadKey(true);
		PlayerController(cki);
	} while(_MAP.Player.Coin < 3 && _MAP.Player.Health > 0);

	if(_MAP.Player.Health <= 0) cd.LogLose();
	if(_MAP.Player.Coin == 3) cd.LogVictory();
	}
}
