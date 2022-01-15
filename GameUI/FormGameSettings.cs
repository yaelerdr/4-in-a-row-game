using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLogic;

namespace GameUI
{
    public partial class FormGameSettings : Form
    {
        private Player m_Player1; //= new Player('X', "", true);
        private Player m_Player2; //= new Player('O', "", false);
        private Board m_Board;

        public Player Player1
        {
            get { return m_Player1; }
            set { m_Player1 = value; }
        }

        public Player Player2
        {
            get { return m_Player2; }
            set { m_Player2 = value; }
        }

        public Board Board
        {
            get { return m_Board; }
            set { m_Board = value; }
        }

        public FormGameSettings()
        {
            InitializeComponent();
            m_Player1 = new Player('X', textBoxNamePlayer1.Text, true);
            m_Player2 = new Player('O', "Computer", false);
            Board = new Board((int)numericUpDownRows.Value, (int)numericUpDownCols.Value);
        }

        private void FormGameSettings_Load(object sender, EventArgs e)
        {

        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            Board = new Board(Board.RowSize, Board.ColumnSize);
            Player2 = new Player('O', textBoxNamePlayer2.Text, checkBoxPlayer2.Checked);
            GameManager newGameManager = new GameManager(new Game(Player1, Player2, Board));
        }

        private void textBoxNamePlayer1_TextChanged(object sender, EventArgs e)
        {
            Player1.Name = textBoxNamePlayer1.Text;
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPlayer2.Checked)
            {
                Player2.IsHumen = true;
                textBoxNamePlayer2.Enabled = true;
                textBoxNamePlayer2.Text = string.Empty;
            }

            else
            {
                Player2.IsHumen = false;
                textBoxNamePlayer2.Enabled = false;
                textBoxNamePlayer2.Text = "Computer";
            }

        }

        private void textBoxNamePlayer2_TextChanged(object sender, EventArgs e)
        {
            Player2.Name = textBoxNamePlayer2.Text;
        }

        private void numericUpDownRows_ValueChanged(object sender, EventArgs e)
        {
            Board.RowSize = (int)numericUpDownRows.Value;
        }

        private void numericUpDownCols_ValueChanged(object sender, EventArgs e)
        {
            Board.ColumnSize = (int)numericUpDownCols.Value;
        }

    }
}
