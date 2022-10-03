 using System; 
 using System.Threading; 
 using System.Collections; 
 using System.Numerics; 
 using System.Windows.Input; 
 class Game 
 { 
     Map map; 
     Snake snake; 
     Utils utils = new Utils(); 
     int Score = 0; 
     public static void Main() 
     { 
         Game game = new Game(); 
  
         game.StartUp(); 
         game.GameUpdate(); 
     } 

     public void StartUp() 
     { 
         map = new Map(); 
         snake = new Snake(); 
  
         map.Init(); 
         map.Clear(); 
  
         snake.Init(); 
         map.CheckForApple(); 
     } 
     public void GameLose() 
     { 
         Console.WriteLine("You Lost!"); 
         Console.WriteLine("Score: " + Score.ToString()); 
         System.Environment.Exit(0); 
     } 
     public void GameUpdate() 
     { 

          
         if(Console.KeyAvailable == true) 
         { 
             ChangeDirection(); 
         } 
         if(snake.CheckForLoss() || map.GetVal(snake.headPosition + snake.Direction) == 1) 
         { 
             GameLose(); 
         } 
         snake.Move(); 
         if (!map.CheckForApple()) 
         { 
             Score++; 
             snake.Length += 1; 
         } 
         map.Clear(); 
         map.DrawApple(); 
         map.DrawSnake(snake.Positions); 
         map.Render(); 

  
         Thread.Sleep(1000); 
         GameUpdate(); 
     } 
     public void ChangeDirection() 
     { 
         var input = Console.ReadKey(false); 
         switch (input.Key) 
         { 
             case ConsoleKey.UpArrow: 
                 snake.Direction = new Vector2(0,-1); 
                 break; 
             case ConsoleKey.DownArrow: 
                 snake.Direction = new Vector2(0, 1); 
                 break; 
             case ConsoleKey.LeftArrow: 
 

                snake.Direction = new Vector2(-1, 0); 
                 break; 
             case ConsoleKey.RightArrow: 
                 snake.Direction = new Vector2(1, 0); 
                 break; 
         } 
  
     } 
 } 
 class Snake 
 { 
     public Vector2 headPosition; 
     public List<Vector2> Positions; 
  
     // Snake Info 
     public int Length = 1; 
     public Vector2 Direction = new Vector2(0,

 1); 
  
     public void Init() 
     { 
         headPosition = new Vector2(5f,5f); 
         Positions = new List<Vector2>(); 
         Positions.Add(headPosition); 
     } 
     public void MaxCheck() 
     { 
         if(Positions.Count > Length) 
         { 
             Positions.RemoveAt(0); 
         } 
     } 
     public bool CheckForLoss() 
     { 
         Vector2 pos = headPosition + Direction; 
         if(pos.X < 1 || pos.X > 10 || pos.Y < 1 ||

 pos.Y > 10) 
         { 
             return true; 
         } 
         return false; 
     } 
     public void Move() 
     { 
         headPosition += Direction; 
         Positions.Add(headPosition); 
         MaxCheck(); 
     } 
 } 
 class Map 
 { 
     public int[,] Game; 
     public Vector2 applepos; 
     private Utils utils = new Utils(); 
     public void Init() 
     { 
         Game = new int[11,11]; 

     } 
     public void Clear() 
     { 
         for(var i = 0; i < 11; i++) 
         { 
             for(var e = 0; e < 11; e++) 
             { 
                 Game[i,e] = 0; 
             } 
         } 
     } 
     public void DrawSnake(List<Vector2> positions) 
     { 
         foreach(Vector2 pos in positions) 
         { 
             SetVal(pos, 1); 
         } 
     } 
     public void DrawApple() 

     { 
         SetVal(applepos, 2); 
     } 
     public void Render() 
     { 
         Console.Clear(); 
         for (var i = 0; i < 11; i++) 
         { 
             for(var e = 0; e < 11; e++) 
             { 
                 if (e > 0 && i > 0) 
                 { 
                     Console.Write(Game[e,i]); 
                     Console.Write(" "); 
                 } 
             } 
             Console.WriteLine(); 
         } 
     } 
     public int GetVal(Vector2 pos) 
     { 

         return Game[(int)pos.X, (int)pos.Y]; 
     } 
     public void SetVal(Vector2 pos, int val) 
     { 
         Game[(int)pos.X, (int)pos.Y] = val; 
     } 
     public bool CheckForApple() 
     { 
         if (!utils.ValueExists(Game, 11, 11, 2)) 
         { 
             CreateApple(); 
             return false; 
         } 
         return true; 
     } 
     public void CreateApple() 
     { 
         bool SpotFound = false; 
         Random random = new Random(); 
         while (!SpotFound) 
         { 

             Vector2 randpos = new Vector2(random.Next(1, 10), random.Next(1, 10)); 
             if (GetVal(randpos) == 0) 
             { 
                 applepos = randpos; 
                 SpotFound = true; 
             } 
         } 
     } 
 } 
 class Utils 
 { 
     public bool ValueExists(int[,] arr, int height, int width, int value) 
     { 
         for(var i = 0; i < height; i++) 
         { 
             for(var e = 0; e < width; e++) 
             { 

                 if(arr[i,e] == value) 
                 { 
                     return true; 
                 } 
             } 
         } 
         return false; 
     } 
 }
