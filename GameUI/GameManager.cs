using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLogic;

namespace GameUI
{
    public class GameManager
    {
        private FormGameBoard m_GameBoard;
        private Game m_Game;
        private int m_UserNextMoveCol;
        private bool m_UserNextMoveWasUsed = true;

        public GameManager(Game i_Game)
        {
            m_Game = i_Game;
            InitializeNewGame();
        }

        public Game Game
        {
            get { return m_Game; }
        }

        public FormGameBoard GameBoard
        {
            get { return m_GameBoard; }
            set { m_GameBoard = value; }
        }

        public int NextMoveCol
        {
            get { return m_UserNextMoveCol; }
            set { m_UserNextMoveCol = value; }
        }

        public bool NextMoveWasUsed
        {
            get { return m_UserNextMoveWasUsed; }
            set { m_UserNextMoveWasUsed = value; }
        }

        public void InitializeNewGame()
        {
            GameBoard = new FormGameBoard(Game.Board.RowSize, Game.Board.ColumnSize, Game.Player1.Name, Game.Player2.Name, 0, 0);
            drawBoard();
            StartRound();
        }

        public void StartRound()
        {
            GameBoard.ShowDialog();
        }

        private void continueRound()
        {
            executeMove(NextMoveCol);
            handleAfterMove();
        }

        private void handleAfterMove()
        {
            switch (Game.State)
            {
                case Game.eGameState.Continue:
                    {
                        if (!Game.CurrentPlayer.IsHumen)
                        {
                            executeMove(Game.ComputersMove());
                            handleAfterMove();
                        }

                        break;
                    }

                case Game.eGameState.Exit:
                    {
                        closeGame();
                        break;
                    }

                case Game.eGameState.Win:
                    {
                        Game.ChangePlayer();
                        popUpHandle();
                        break;
                    }

                case Game.eGameState.Draw:
                case Game.eGameState.Resign:

                    {
                        popUpHandle();
                        break;
                    }

            }
        }

        private void drawBoard()
        {
            int horizontalLocation = 10;
            int verticalLocation = 65; 
            Button button;
            Label label;

            GameBoard.ColumnsButtons = new Button[Game.Board.ColumnSize];
            GameBoard.Label2DArray = new Label[Game.Board.RowSize, Game.Board.ColumnSize];

            for (int col = 0; col < Game.Board.ColumnSize; col++)
            {
                GameBoard.ColumnsButtons[col] = new Button();
                button = GameBoard.ColumnsButtons[col];
                button.Text = (col + 1).ToString();
                button.Name = (col).ToString();
                button.Size = new Size(GameBoard.ButtonSize, GameBoard.ButtonSize);
                button.Location = new Point(horizontalLocation, verticalLocation);
                GameBoard.Controls.Add(button);
                button.Click += on_Button_Click;
                horizontalLocation += (GameBoard.ButtonSize + 5);
            }

            verticalLocation += (GameBoard.ButtonSize + 5);
            horizontalLocation = 10;

            for (int row = 0; row < Game.Board.RowSize ; row++)
            {
                for (int col = 0; col < Game.Board.ColumnSize; col++)
                {
                    GameBoard.Label2DArray[row, col] = new Label();
                    label = GameBoard.Label2DArray[row, col];
                    GameBoard.Controls.Add(label); //check if needed
                    label.Size = new Size(GameBoard.ButtonSize, GameBoard.ButtonSize);
                    label.Location = new Point(horizontalLocation, verticalLocation);
                    label.BorderStyle = BorderStyle.FixedSingle;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    horizontalLocation += (GameBoard.ButtonSize + 5);
                }

                verticalLocation += (GameBoard.ButtonSize + 5);
                horizontalLocation = 10;
            }

            GameBoard.QuitButton.Name = "Quit";
            GameBoard.QuitButton.Click += on_Button_Click;
        }

        private void on_Button_Click(object sender, EventArgs e)   //----> TODO
        {
            Button chosenButton = sender as Button;

            if (chosenButton.Name == "Quit")
            {
                NextMoveCol = -1;
                Game.State = Game.eGameState.Resign;
            }

            else if(chosenButton.Name == "CloseButton")
            {
                NextMoveCol = -2;
                Game.State = Game.eGameState.Exit;
                closeGame();
            }

            else
            {
                NextMoveCol = int.Parse(chosenButton.Name);
            }
            NextMoveWasUsed = false;
            continueRound();
        }


        private void clearBoard()
        {
            foreach (Label label in GameBoard.Label2DArray)
            {
                label.Text = string.Empty;
            }

        }

        private void executeMove(int i_NextMove) 
        {
            Game.State = executeMoveInGameLogic(i_NextMove, out int rowToInsert);

            if (Game.State != Game.eGameState.Exit && Game.State != Game.eGameState.Resign)
            {
                executeMoveInUI(i_NextMove, rowToInsert);
            }

            if (Game.State == Game.eGameState.Continue && Game.Board.IsFullColumn(i_NextMove))
            {
                GameBoard.ColumnsButtons[i_NextMove].Enabled = false;
            }

        }

        private Game.eGameState executeMoveInGameLogic(int i_NextMove, out int o_RowToInsert)
        {
            Game.eGameState gameState = Game.UpdateNextMove(i_NextMove, out o_RowToInsert);
            return gameState;
        }

        private void executeMoveInUI(int i_NextMove, int i_RowToInsert)
        {
            GameBoard.Label2DArray[i_RowToInsert,i_NextMove].Text = Game.CurrentPlayer.Sign.ToString();
            Game.ChangePlayer();
        }

        private void popUpHandle() 
        {
            DialogResult answer;
            string headline;
            string message;

            getMessageBoxInfoByGameState(out headline, out message);
            message += "\n\nAnother Round?";

            answer = MessageBox.Show(message, headline, MessageBoxButtons.YesNo);
            wantToPlayAnotherRound(answer);

        }

        private void wantToPlayAnotherRound(DialogResult i_MessageBoxRes)
        {
            if (i_MessageBoxRes == DialogResult.Yes)
            {
                resetGame();
            }

            else
            {
                closeGame();
            }

        }

        private void resetGame()
        {
            Game.UpdateScore();
            Game.RestGame();
            clearBoard();
            GameBoard.Player1Score.Text = Game.Player1.Score.ToString();
            GameBoard.Player2Score.Text = Game.Player2.Score.ToString();
            resetBottons();
        }

        private void resetBottons()
        {
            foreach (Button tempButton in GameBoard.ColumnsButtons)
            {
                tempButton.Enabled = true;
            }

        }

        private void closeGame()
        {
            MessageBox.Show(@"Bye Bye for now :)");
            GameBoard.Close();
            Application.Exit();
        }

        private void getMessageBoxInfoByGameState(out string o_Headline, out string o_Message)
        {
            o_Headline = "";
            o_Message = "";

            switch (Game.State)
            {
                case Game.eGameState.Resign:
                    {
                        o_Headline = "Resign!";
                        o_Message = string.Format("{0} has quit the game!", Game.CurrentPlayer.Name);
                        break;
                    }

                case Game.eGameState.Draw:
                    {
                        o_Headline = "Tie!";
                        o_Message = "It's a tie !";
                        break;
                    }

                case Game.eGameState.Win:
                    {
                        o_Headline = "Winner!";
                        o_Message = string.Format("The winner is: {0}!!", Game.CurrentPlayer.Name);
                        break;
                    }

            }
        }
    }
}