using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Player
    {
        private char m_Sign;
        private string m_Name;
        private int m_Score;
        private bool m_IsHumen;

        public Player(char i_Sign, string i_Name, bool i_IsHumen)
        {
            m_Sign = i_Sign;
            m_Name = i_Name;
            m_IsHumen = i_IsHumen;
            m_Score = 0;
        }

        public char Sign
        {
            get { return m_Sign; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public bool IsHumen
        {
            get { return m_IsHumen; }
            set { m_IsHumen = value; }
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