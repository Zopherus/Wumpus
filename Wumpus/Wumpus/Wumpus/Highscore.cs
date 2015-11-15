using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wumpus
{
	class Highscore
	{
		static List<StoreScore> highscore = new List<StoreScore>();

		public static void addScore(string n, int s)
		{
			StoreScore score = new StoreScore(n, s);
			highscore.Add(score);

			highscore.Sort(new scoreComparer());

			WriteToFile();
		}

		public static List<StoreScore> GetScore()
		{
            highscore.Clear();
            ReadFromFile();
			return highscore;
		}

		private static void ReadFromFile()
		{
			try
			{
                
				StreamReader sr = new StreamReader("Content/Text Files/HighScores.txt");

				string input = sr.ReadLine();

				while (input != null)
				{
					string[] data = input.Split(',');
					StoreScore s = new StoreScore(data[0], int.Parse(data[1]));

					highscore.Add(s);

					input = sr.ReadLine();
				}
				sr.Close();
			}
			catch
			{
				//No Current Scores in File
                highscore.Add(new StoreScore("No scores to display", 0));
			}
		}

		private static void WriteToFile()
		{
            StreamWriter sw = new StreamWriter("Content/Text Files/HighScores.txt");

            foreach (StoreScore s in highscore)
            {
                string output = s.Name + "," + s.Score.ToString();
                sw.WriteLine(output);
            }

			sw.Close();
		}
	}
	class scoreComparer : Comparer<StoreScore>
	{
		public override int Compare(StoreScore x, StoreScore y)
		{
			return y.Score.CompareTo(x.Score);
		}
	}
}
