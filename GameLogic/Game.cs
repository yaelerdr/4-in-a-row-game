using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Game
    {
        private Player m_Player1;
        private Player m_Player2;
        private Player m_CurrentPlayer;
        private Board m_Board;
        private eGameState m_State;
        private Move m_LastMove;
        private Random m_Random;


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
            get { return m_CurrentPlayer; }
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
            set
            {
                m_LastMove = value;
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
            CurrentPlayer = Player1;
        }

        public eGameState UpdateNextMove(int i_Column, out int io_Row)
        {

            io_Row = -1;
            eGameState eGameState = eGameState.Continue;

            if (i_Column == -1)
            {
                return eGameState.Resign;
            }

            if (Board.IsFullColumn(i_Column))
            {
                return eGameState.FullColumn;
            }

            io_Row = Board.UpdateBoardAccordingToColumn(i_Column, CurrentPlayer.Sign);
            LastMove = new Move(io_Row, i_Column);

            if (isWinAccordingToLastMove())
            {
                eGameState = eGameState.Win;
            }

            else if (Board.IsFullBoard())
            {
                eGameState = eGameState.Draw;
            }

            return eGameState;
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

        public int ComputersMove()
        {
            int temp = Random.Next(0, Board.AvailableColumns.Count);
            int columnToInsert = Board.AvailableColumns[temp];

            return columnToInsert;
        }

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