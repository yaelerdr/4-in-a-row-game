using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalDLL = Ex02.ConsoleUtils;

// $G$ RUL-004 (-10) Wrong zip name format / folder name format.
// $G$ RUL-005 (-10) Wrong zip folder structure, the zip file should contain a single folder.

namespace GameLogic
{
    public class UserInterface
    {
        public const int k_MinRange = 4;
        public const int k_MaxRange = 8;
        public const char k_SignOfPlayer1 = 'X';
        public const char k_SignOfPlayer2 = 'O';

        public static void Main(string[] args)
        {
            Game game = SetUpGameByUsersInputs();
            game.Round();
            Console.ReadLine();
        }

        public static Game SetUpGameByUsersInputs()
        {
            Console.WriteLine("Welcome to Connect4!!\n");
            Board board = SetUpBoardByUsersInputs();
            Player player1 = SetUpPlayerByUsersInputs(k_SignOfPlayer1);
            Player player2;

            if (AgainstComputer())
            {
                player2 = new Player(k_SignOfPlayer2, "Computer", false);
            }

            else
            {
                player2 = SetUpPlayerByUsersInputs(k_SignOfPlayer2);

            }

            return new Game(player1, player2, board);
        }

        public static Board SetUpBoardByUsersInputs()
        {
            int rows = 0;
            int columns = 0;
            // $G$ NTT-999 (-5) You should have used Environment.NewLine instead of "\n".
            Console.WriteLine("Please choose board size:\n(minimal size is {0}x{0}, maximal size is {1}x{1})\n\nFirst, choose rows size:", k_MinRange, k_MaxRange);
            string input = Console.ReadLine();

            while (!IsValidRange(input, ref rows, k_MinRange, k_MaxRange))
            {
                Console.WriteLine("Rows size needs to be a number between {0}-{1}, please re-enter rows size:", k_MinRange, k_MaxRange);
                input = Console.ReadLine();
            }

            Console.WriteLine("\nGreat, next please choose columns size:");
            // $G$ DSN-004 (-5) Redundant code duplication, should be divded for methods.
            input = Console.ReadLine();

            while (!IsValidRange(input, ref columns, k_MinRange, k_MaxRange))
            {
                Console.WriteLine("Column size needs to be a number between {0}-{1}, please re-enter columns size:", k_MinRange, k_MaxRange);
                input = Console.ReadLine();
            }

            return new Board(rows, columns);
        }

        public enum eGameMode
        {
            AgainstComputer = 0,
            WithAnoterPlayer = 1
        }

        public static bool AgainstComputer()
        {
            int gameMode = -1;
            bool againstComputer = false;
            string input;

            Console.WriteLine("\nChoose game mode:\n0.To play against the computer.\n1.To set up another player.");
            input = Console.ReadLine();

            while (!IsValidRange(input, ref gameMode, (int)eGameMode.AgainstComputer, (int)eGameMode.WithAnoterPlayer))
            {
                Console.WriteLine("You can only enter '0' to play against the computer or '1' to set up another player, try again:");
                input = Console.ReadLine();
            }

            if ((eGameMode)gameMode == eGameMode.AgainstComputer)
            {
                againstComputer = true;
            }

            return againstComputer;
        }

        public static bool IsValidRange(string i_Input, ref int io_InputAsInt, int i_RangeStart, int i_RangeEnd)
        {
            bool IsValidRange = int.TryParse(i_Input, out io_InputAsInt);

            if (IsValidRange)
            {
                if (io_InputAsInt > i_RangeEnd || io_InputAsInt < i_RangeStart)
                {
                    IsValidRange = false;
                }

            }

            return IsValidRange;
        }

        // $G$ CSS-999 (-3) Private methods should start with an lowercase letter.
        private static Player SetUpPlayerByUsersInputs(char i_PlayerSign)
        {
            string playerName;

            Console.WriteLine("\nPlease enter Player's name:\n(Their sign will be: {0})", i_PlayerSign);
            playerName = Console.ReadLine();

            return new Player(i_PlayerSign, playerName, true);
        }

        public static int GetPlayersMove(Game i_Game)
        {
            int playersMove = -1;
            bool isValid;
            string input;

            if (i_Game.CurrentPlayer.IsHumen)
            {
                input = Console.ReadLine();


                // $G$ CSS-999 (-2) You should use constant vars and not constant values.
                if (input == "Q")
                {
                    return playersMove;
                }

                isValid = IsValidRange(input, ref playersMove, 1, i_Game.Board.ColumnSize);

                while (!isValid)
                {
                    Console.WriteLine("Your next move needs to be a valid column number, or you can enter 'Q' to resign)");
                    input = Console.ReadLine();

                    if (input == "Q")
                    {
                        return playersMove;
                    }

                    isValid = IsValidRange(input, ref playersMove, 1, i_Game.Board.ColumnSize);
                }

                playersMove--;
            }

            else
            {
                playersMove = i_Game.ComputersMove();
            }

            return playersMove;
        }

        public static void PrintGameState(Game i_Game)
        {
            string stateOutput = "";

            ExternalDLL.Screen.Clear();
            Console.WriteLine("Player 1 is:  {0} \t their sign is:  {1}", i_Game.Player1.Name, i_Game.Player1.Sign);
            Console.WriteLine("Player 2 is:  {0} \t their sign is:  {1}", i_Game.Player2.Name, i_Game.Player2.Sign);

            if (i_Game.State == Game.eGameState.Continue)
            {
                PrintContinueState(i_Game);
            }

            else
            {
                switch (i_Game.State)
                {
                    case Game.eGameState.Resign:
                        stateOutput = string.Format("This game ended with {0}'s resign.", i_Game.CurrentPlayer.Name);
                        break;

                    case Game.eGameState.Draw:
                        stateOutput = string.Format("This game ended with a draw.");
                        break;

                    case Game.eGameState.Win:
                        stateOutput = string.Format("!!!!! The winner is: {0} !!!!!", i_Game.CurrentPlayer.Name);
                        break;
                }

                PrintEndGameState(i_Game, stateOutput);
            }

        }

        public static void PrintContinueState(Game i_Game)
        {
            Console.WriteLine("This is {0}'s turn:\n", i_Game.CurrentPlayer.Name);

            if (i_Game.CurrentPlayer.IsHumen)
            {
                Console.WriteLine("What's your next move? \n(choose your move by entering column number, or enter 'Q' to resign)\n");
            }

            PrintBoard(i_Game.Board);
        }

        public static void PrintEndGameState(Game i_Game, string i_Output)
        {
            PrintBoard(i_Game.Board);
            Console.WriteLine(i_Output);
            Console.WriteLine("Player 1 score: {0}\nPlayer 2 score: {1}", i_Game.Player1.Score, i_Game.Player2.Score);
            WantToPlayAnotherRound(i_Game);
        }

        public static int NextMoveIsFullColumn(Game i_Game)
        {
            Console.Write("This column is full, please enter a different move:\n(choose your move by entering column number, or enter 'Q' to resign)\n");
            return GetPlayersMove(i_Game);
        }

        public static void WantToPlayAnotherRound(Game i_Game)
        {
            string input;

            Console.WriteLine("If you want to play another round, please enter 'Y'\n(Any other key to exit)");
            input = Console.ReadLine();

            if (input == "Y")
            {
                i_Game.RestGame();
            }

            else
            {
                i_Game.State = Game.eGameState.Exit;
                Console.WriteLine("Bye bye");
                Console.ReadLine();
            }

        }
        // $G$ DSN-999 (-5) You should override ToString in Board class
        public static void PrintBoard(Board i_board)
        {
            StringBuilder board = new StringBuilder();
            string columnsNumberToAppend = "";
            string rowToAppend = "";
            string rowPlusHyphens;
            string hyphens = "";

            for (int i = 1; i <= i_board.ColumnSize; i++)
            {
                columnsNumberToAppend += string.Format("  {0} ", i.ToString());
            }

            board.Append(columnsNumberToAppend);
            board.Append('\n');

            for (int i = 0; i < i_board.RowSize; i++)
            {
                for (int j = 0; j < i_board.ColumnSize; j++)
                {
                    rowToAppend += string.Format("| {0} ", i_board.Matrix[i, j]);
                    hyphens += "====";
                }

                rowPlusHyphens = string.Format("{0}|\n{1}=\n", rowToAppend, hyphens);
                board.Append(rowPlusHyphens);
                rowToAppend = "";
                hyphens = "";
            }

            Console.Write(board);
        }

    }

}

