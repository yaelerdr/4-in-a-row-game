using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Player
    {
        // $G$ CSS-999 (-3) this members should be readonly. (except score) ---> DONE   V
        private char r_Sign;
        private string r_Name;
        private int m_Score;
        private bool r_IsHumen;

        public Player(char i_Sign, string i_Name, bool i_IsHumen)
        {
            r_Sign = i_Sign;
            r_Name = i_Name;
            r_IsHumen = i_IsHumen;
            m_Score = 0;
        }

        public char Sign
        {
            get { return r_Sign; }
        }

        public string Name
        {
            get { return r_Name; }
        }

        public bool IsHumen
        {
            get { return r_IsHumen; }
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public enum PlayerGameChar
        {
            X = 1,
            O = 2
        }

    }
}