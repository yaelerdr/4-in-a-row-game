using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameUI
{
    public partial class FormGameBoard : Form
    {
        private int m_RowSize;
        private int m_ColumnSize;
        private Button[] m_ColumnsButtons;
        private Label[,] m_Label2DArray;
        private const int k_ButtonSize = 45;


        public FormGameBoard(int i_RowSize, int i_ColumnSize, string i_NamePlayer1, string i_NamePlayer2, int i_Player1Score, int i_Player2Score)
        {
            InitializeComponent();
            m_RowSize = i_RowSize;
            m_ColumnSize = i_ColumnSize;

            player1Label.Text = i_NamePlayer1 + ":";
            player2Label.Text = i_NamePlayer2 + ":";
            player1Score.Text = i_Player1Score.ToString();
            player2Score.Text = i_Player2Score.ToString();

            Height = (i_RowSize + 1) * (ButtonSize + 5) + 150;

            player1Label.Location = new Point(15, player1Label.Location.Y);
            player1Score.Location = new Point(player1Label.Right, player1Label.Location.Y);

            Width = Math.Max(i_ColumnSize * (ButtonSize + 5) + 30, player1Score.Right + player2Label.Width + player2Score.Width + 30);

            player2Label.Location = new Point(15, player1Label.Location.Y + 20);
            player2Score.Location = new Point(player2Label.Right, player1Label.Location.Y + 20);

            quit.Location = new Point(Width - (quit.Width) - 25, quit.Location.Y);
        }

        public int RowSize
        {
            get { return m_RowSize; }
        }

        public int ColumnSize
        {
            get { return m_ColumnSize; }
        }

        public string Player1Lable
        {
            set { player1Label.Text = value; }
        }

        public Label Player1Score
        {
            get { return player1Score; }
        }

        public Label Player2Score
        {
            get { return player2Score; }
        }

        public Label[,] Label2DArray
        {
            get { return m_Label2DArray; }
            set { m_Label2DArray = value; }
        }

        public Button[] ColumnsButtons
        {
            get { return m_ColumnsButtons; }
            set { m_ColumnsButtons = value; }
        }

        public int ButtonSize
        {
            get { return k_ButtonSize; }
        }

        public Button QuitButton
        {
            get { return quit; }
        }

        private void FormGameBoard_Load(object sender, EventArgs e)
        {

        }

        private void quit_Click(object sender, EventArgs e)
        {

        }
    }
}
