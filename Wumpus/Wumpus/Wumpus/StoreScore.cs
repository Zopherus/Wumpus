using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wumpus
{
	class StoreScore
	{
		string name;
		int score;

		public StoreScore(string n, int s)
		{
			name = n;
			score = s;
		}

		public string Name
		{
			set { name = value; }
			get { return name; }
		}

		public int Score
		{
			set { score = value; }
			get { return score; }
		}

        public override string ToString()
        {
            return name + ": " + score.ToString();
        }
	}
}
