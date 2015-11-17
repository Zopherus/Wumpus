using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wumpus
{
	//Inherit from IComparable in order to sort scores correctly
	class Score : IComparable<Score>
	{
		public string Name { get; set; }
		public int Points { get; set; }

		public Score(string Name, int Points)
		{
			this.Name = Name;
			this.Points = Points;
		}

        public override string ToString()
        {
            return Name + ": " + Points.ToString();
        }

		public int CompareTo(Score other)
		{
			if (other == null)
				return 1;
			return other.Points.CompareTo(this.Points);
		}
	}
}
