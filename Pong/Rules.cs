using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class Rules
    {

        private Player p1;
        private Player p2;
        private int winningScore;
        private RenderWindow window;
        public Rules(Player p1, Player p2, int winningScore,RenderWindow window)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.winningScore = winningScore;
            this.window = window;
        }

        public void addPointToPlayer(int id)
        {
            if (id == 0)
            {
                p1.AddPoint();
            }
            else if (id == 1)
            {
                p2.AddPoint();
            }
            Player winner = checkForWin();

            if (winner != null)
            {
                Console.WriteLine("Player " + winner.Playerid + " hat gewonnen.");
                window.Close();
            }
        }
        public Player checkForWin()
        {
            if (p1.Score >= winningScore)
            {
                return p1;
            }
            if (p2.Score >= winningScore)
            {
                return p2;
            }
            return null;
        }
    }
}
