using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Game
    {
        public delegate int NextMoveEventHandler();

        // $G$ CSS-999 (-2) this members should be readonly.
        private Player m_Player1;
        private Player m_Player2;
        private Player m_CurrentPlayer;
        private Board m_Board;
        private eGameState m_State;
        private Move m_LastMove;
        private Random m_Random;

        private event NextMoveEventHandler NextMoveWasUpdated;



        public struct Move
        {
            private int m_Row;
            private int m_Column;

            public Move(int row, int column)
            {
                m_Row = row;
                m_Column = column;
            }

            public int row
            {
                get { return m_Row; }
                set { row = value; }
            }

            public int column
            {
                get { return m_Column; }
                set { column = value; }
            }
        }
        public Player Player1
        {
            get { return m_Player1; }
        }

        public Player Player2
        {
            get { return m_Player2; }
        }

        public Player CurrentPlayer
        {
            get {return m_CurrentPlayer; }
            set { m_CurrentPlayer = value; }
        }

        public Board Board
        {
            get { return m_Board; }
        }

        public Random Random
        {
            get { return m_Random; }
        }

        public eGameState State
        {
            get { return m_State; }
            set { m_State = value; }
        }

        public Move LastMove
        {
            get { return m_LastMove; }
            set { m_LastMove = value;
            }
        }

        public Game(Player player1, Player player2, Board board)
        {
            m_Player1 = player1;
            m_Player2 = player2;
            m_CurrentPlayer = m_Player1;
            m_Board = board;
            m_Random = new Random();
            m_State = eGameState.Continue;
        }

        public enum eGameState
        {
            FullColumn,
            Resign,
            Draw,
            Win,
            Continue,
            Exit
        }

        public void OnNextMove()
        {
            NextMoveWasUpdated.Invoke();
        }

        public void Round()
        {
            int nextMove;

            while (State == eGameState.Continue)
            {
                // $G$ DSN-002 (-5) You should not make UI calls from your logic classes. 
                UserInterface.PrintGameState(this);
                nextMove = UserInterface.GetPlayersMove(this);
                State = updateNextMove(nextMove);
                OnNextMove();

            }



            if (State == eGameState.FullColumn)
            {
                nextMove = UserInterface.NextMoveIsFullColumn(this);
                State = updateNextMove(nextMove);
                Round();
            }

            else
            {
                UpdateScore();
                UserInterface.PrintGameState(this);

                if (State == eGameState.Exit)
                {
                    Environment.Exit(0);
                }

                else
                {
                    UserInterface.WantToPlayAnotherRound(this);
                }

            }
        }

        public void ChangePlayer()
        {
            if (CurrentPlayer == Player1)
            {
                CurrentPlayer = Player2;
            }

            else
            {
                CurrentPlayer = Player1;
            }

        }

        public void RestGame()
        {
            Board.ClearBoard();
            State = eGameState.Continue;
            Round();
        }

        // $G$ CSS-028 (-3) A method should not include more than one return statement.  ---> DONE   V
        private eGameState updateNextMove(int i_Column)
        {
            int row;
            eGameState gameState = eGameState.Continue;

            if (i_Column == -1)
            {
                gameState = eGameState.Resign;
            }

            else if (Board.IsFullColumn(i_Column))
            {
                gameState = eGameState.FullColumn;
            }

            else
            {
                row = Board.UpdateBoardAccordingToColumn(i_Column, CurrentPlayer.Sign);
                LastMove = new Move(row, i_Column);

                if (isWinAccordingToLastMove())
                {
                    gameState = eGameState.Win;
                }

                else if (Board.IsFullBoard())
                {
                    gameState = eGameState.Draw;
                }

                if (gameState == eGameState.Continue)
                {
                    ChangePlayer();
                }
            }

            return gameState;
        }

        public void UpdateScore()
        {
            switch (State)
            {
                case eGameState.Resign:
                    ChangePlayer();
                    CurrentPlayer.Score++;
                    ChangePlayer();
                    break;

                case eGameState.Draw:
                    Player1.Score++;
                    Player2.Score++;
                    ChangePlayer();
                    break;

                case eGameState.Win:
                    CurrentPlayer.Score++;
                    break;
            }
        }

        // $G$ NTT-007 (-7) There's no need to re-instantiate the Random instance each time it is used.    ---> DONE    V
        public int ComputersMove()
        {
            int temp = Random.Next(0, Board.AvailableColumns.Count);
            int columnToInsert = Board.AvailableColumns[temp];

            return columnToInsert;
        }

        // $G$ DSN-999 (-2) This method should be private.     ---> DONE    V
        private bool are4ConnectedInArray(char[] i_ArrayToCheck, char i_Sign)
        {
            int count = 0;
            int index = i_ArrayToCheck.Length - 1;

            while ((count < 4) && (index >= 0))
            {
                if (i_ArrayToCheck[index] == i_Sign)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }

                index--;
            }

            return (count == 4);
        }

        private bool isWinAccordingToLastMove()
        {
            bool are4Connected = are4ConnectedInArray(Board.GetMainDiagonal(LastMove.row, LastMove.column), CurrentPlayer.Sign) ||
                                 are4ConnectedInArray(Board.GetAntiDiagonal(LastMove.row, LastMove.column), CurrentPlayer.Sign) ||
                                 are4ConnectedInArray(Board.GetRow(LastMove.row), CurrentPlayer.Sign) ||
                                 are4ConnectedInArray(Board.GetColumn(LastMove.column), CurrentPlayer.Sign);

            return are4Connected;
        }

    }
}