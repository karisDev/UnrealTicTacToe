using System;

namespace TicTacToe
{
    public class frontend
    {
        public static bool introduction()
        {
            bool terminate = false;
            switch( arrow_control(new[] {"New try", "Exit"}, 1) )
            {
                case 1:
                    backend.field_refresh();
                    int game_status = 1; // 1 - in progress, 2 - bot wins, 3 - human wins, 4 - draw
                    while( game_status == 1 )
                    {
                        
                        backend.bot_move();
                        field_display();
                        game_status = backend.game_status_check(backend.field_arr);
                        if(game_status == 1)
                        {
                            user_move();
                        }
                        
                        /* ТЕСТ ЕСЛИ ЧЕЛОВЕК ХОДИТ ПЕРВЫЙ (сработал)
                        field_display();
                        user_move();
                        game_status = backend.game_status_check(backend.field_arr);
                        if(game_status == 1)
                        {
                            backend.bot_move();
                        }
                        */
                    }
                    switch( game_status )
                    {
                        case 2:
                            field_display();
                            Console.WriteLine("Unbelievable! You've lost!");
                            Console.ReadLine();
                            break;
                        case 4:
                            field_display();
                            Console.WriteLine("You didn't win. Neither I won. But I'll get you next time!");
                            Console.ReadLine();
                            break;
                        default:
                            Console.WriteLine("If you see this message - something is wrong");
                            Console.ReadLine();
                            break;
                    }
                    break;
                case 2:
                    terminate = true;
                    Console.Clear();
                    cool_ASCII(2);
                    break;
            }
            return terminate;
        }
        static int arrow_control(string[] options_arr, int ascii) // про вторую опцию: она выводит лого если нужно. 0 если не требуется.
        {
            ConsoleKeyInfo key;
            int number_of_options = options_arr.Length;
            int user_option = 1;
            bool enter_pressed = false;
            while(!enter_pressed)
            {
                Console.Clear();
                if(ascii != 0) { cool_ASCII(ascii); }
                Console.WriteLine("\n\nPress enter or use arrows to navigate:\n");

                for(int i = 1; i < number_of_options + 1; i++)
                {
                    if(user_option == i) { Console.WriteLine("-->  " + options_arr[i - 1].ToUpper()); }
                    else { Console.WriteLine(options_arr[i - 1]); }
                }

                key = Console.ReadKey(); // распознаем кнопку,  или изменяем user_options в зависимости от нее.
                switch(key.Key)
                {
                    case ConsoleKey.Enter:
                        enter_pressed = true;
                        break;

                    case ConsoleKey.UpArrow:
                        if(user_option == 1) { user_option = number_of_options; }
                        else{ user_option--; }
                        break;

                    case ConsoleKey.DownArrow:
                        if(user_option == number_of_options) { user_option = 1; }
                        else{ user_option++; }
                        break;

                    default:
                        break;
                }
            }
            return user_option;
        }
        static void cool_ASCII(int ascii)
        {
            // 1 - UNREAL TIC TAC TOE 2 - SEE YOU LATER
            switch(ascii)
            {
                case 1:
                    Console.WriteLine(@"          ) (              (            (                                            )");
                    Console.WriteLine(@"       ( /( )\ )     (     )\ )    *   ))\ )  (      *   )   (       (      *   ) ( /(");
                    Console.WriteLine(@"    (  )\()|()/((    )\   (()/(  ` )  /(()/(  )\   ` )  /(   )\      )\   ` )  /( )\()) (");
                    Console.WriteLine(@"    )\((_)\ /(_))\((((_)(  /(_))  ( )(_))(_)|((_)   ( )(_)|(((_)(  (((_)   ( )(_)|(_)\  )\");
                    Console.WriteLine(@" _ ((_)_((_|_))((_))\ _ )\(_))   (_(_()|_)) )\___  (_(_()) )\ _ )\ )\___  (_(_())  ((_)((_)");
                    Console.WriteLine(@"| | | | \| | _ \ __(_)_\(_) |    |_   _|_ _((/ __| |_   _| (_)_\(_|(/ __| |_   _| / _ \| __|");
                    Console.WriteLine(@"| |_| | .` |   / _| / _ \ | |__    | |  | | | (__    | |    / _ \  | (__    | |  | (_) | _|");
                    Console.WriteLine(@" \___/|_|\_|_|_\___/_/ \_\|____|   |_| |___| \___|   |_|   /_/ \_\  \___|   |_|   \___/|___|");
                    Console.WriteLine();
                    break;
                
                case 2:
                    Console.WriteLine(" (                 )   )          (                      (");
                    Console.WriteLine(@" )\ )           ( /(( /(          )\ )   (      *   )    )\ )");
                    Console.WriteLine(@"(()/((   (      )\())\())    (   (()/(   )\   ` )  /((  (()/(");
                    Console.WriteLine(@" /(_))\  )\    ((_)((_)\     )\   /(_)|(((_)(  ( )(_))\  /(_))"); 
                    Console.WriteLine(@"(_))((_)((_)  __ ((_)((_) _ ((_) (_))  )\ _ )\(_(_()|(_)(_))");
                    Console.WriteLine(@"/ __| __| __| \ \ / / _ \| | | | | |   (_)_\(_)_   _| __| _ \");
                    Console.WriteLine(@"\__ \ _|| _|   \ V / (_) | |_| | | |__  / _ \   | | | _||   /");
                    Console.WriteLine(@"|___/___|___|   |_| \___/ \___/  |____|/_/ \_\  |_| |___|_|_\");
                    Console.WriteLine();
                    break;
                default:
                    Console.WriteLine("cool_ASCII(?)");
                    break;
            }
        }  
        static void field_display()
        {
            Console.Clear();
            Console.WriteLine("Bot: X\nYou: O");
            Console.WriteLine("     |     |");
            for(int i = 0; i < 9; i+=3)
            {
                Console.WriteLine("  {0}  |  {1}  |  {2}", backend.field_arr[i], backend.field_arr[i + 1], backend.field_arr[i + 2]);
                Console.WriteLine("_____|_____|_____ ");
                Console.WriteLine("     |     |");
            }
        }
        static void user_move()
        {
            int user_cell;
            bool if_correct_cell = false;
            Console.WriteLine("Choose a cell");
            while(!if_correct_cell)
            {
                if(Int32.TryParse(Console.ReadLine(), out user_cell))
                {
                    if(0 < user_cell && user_cell < 10 && backend.field_arr[user_cell - 1] == user_cell.ToString())
                    {
                        if_correct_cell = true;
                        backend.field_arr[user_cell - 1] = "O";
                        return;
                    }
                }
                Console.WriteLine("Wrong number");
            }
        }
    }
    public class backend
    {
        public static string[] field_arr;
        public static void field_refresh()
        {
            field_arr = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" }; // "-" ничего нет, X крестик, O нолик
        }
        public static int game_status_check(string[] arr)
        {
            for(int i = 0; i < 9; i += 3) // проверка строк
            {
                if(arr[i] == arr[i + 1] && arr[i + 1] == arr[i + 2])
                {
                    if(arr[i] == "X")
                    {
                        return 2;
                    }
                    else
                    {
                        return 3;
                    }
                }
            }
            for(int i = 0; i < 3; i++) // проверка столбцов
            {
                if(arr[i] == arr[i + 3] && arr[i + 3] == arr[i + 6])
                {
                    if(arr[i] == "X")
                    {
                        return 2;
                    }
                    else
                    {
                        return 3;
                    }
                }
            }
            // проверка диагоналей
            if((arr[0] == arr[4] && arr[4] == arr[8]) || (arr[2] == arr[4] && arr[4] == arr[6]))
            {
                if(arr[4] == "X")
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
            for(int i = 0; i < 9; i++) // проверка на пустые клетки
            {
                if(arr[i] == (i + 1).ToString())
                {
                    return 1;
                }
            }
            return 4;
        }    
        public static void bot_move()
        {
            int best_score = -10000000;
            int current_score = 0;
            int best_move = 0;
            for(int i = 0; i < 9; i++)
            {
                if(field_arr[i] == (i + 1).ToString())
                {
                    field_arr[i] = "X";
                    current_score = minimax(field_arr, false);
                    field_arr[i] = (i + 1).ToString();
                    Console.WriteLine("Current AI score is: " + current_score);
                    if(current_score > best_score)
                    {
                        best_score = current_score;
                        best_move = i;
                    }
                }
            }
            Console.ReadLine();
            field_arr[best_move] = "X";
        }
        
        static int minimax(string[] arr, bool if_ai_turn) // принимает игровое поле и чей ход. Возврат исхода либо 1 либо 0 либо -1, где 1 - позитивный ход, 0 - нейтральный, -1 - отрицательный
        {
            int best_score;
            int current_score;
            if(game_status_check(arr) != 1) // проверка является ли последним ходом
            {
                if(game_status_check(arr) == 3) // O победил
                {
                    return -1;
                }
                else if(game_status_check(arr) == 2) // X победил
                {
                    return 1;
                }
                else // ничья
                {
                    return 0;
                }
            }
            if(if_ai_turn) // ход ИИ - положительный
            {
                best_score = -1000000;
                current_score = 0;
                for(int i = 0; i < 9; i ++)
                {
                    if(arr[i] == (i + 1).ToString())
                    {
                        arr[i] = "X";
                        current_score = minimax(arr, false);
                        arr[i] = (i + 1).ToString();
                        best_score = Math.Max(current_score, best_score);
                    }
                }
                return best_score;
            }
            else // ход Человека - отрицательный
            {
                best_score = 10000000;
                current_score = 0;
                for(int i = 0; i < 9; i ++)
                {
                    if(arr[i] == (i + 1).ToString())
                    {
                        arr[i] = "O";
                        current_score = minimax(arr, true);
                        arr[i] = (i + 1).ToString();
                        best_score = Math.Min(current_score, best_score);
                    }
                }
                return best_score;
            }
        }
    }
    class simplify_main
    {
        static void Main(string[] args)
        {
            bool terminate = false;
            while(!terminate)
            {
                terminate = frontend.introduction();
            }
        }
    }
}
