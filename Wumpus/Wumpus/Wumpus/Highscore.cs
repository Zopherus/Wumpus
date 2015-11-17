using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wumpus
{
	class Highscore
	{
		static List<Score> HighscoreList = new List<Score>();

		public static void addScore(string Name, int Score)
		{
			Score score = new Score(Name, Score);
			HighscoreList.Add(score);

			HighscoreList.Sort(new scoreComparer());

			WriteToFile();
		}

		public static List<Score> GetScore()
		{
            HighscoreList.Clear();
            ReadFromFile();
			return HighscoreList;
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
					Score s = new Score(data[0], int.Parse(data[1]));

					HighscoreList.Add(s);

					input = sr.ReadLine();
				}
				sr.Close();
			}
			catch
			{
				//No Current Scores in File
                HighscoreList.Add(new Score("No scores to display", 0));
			}
		}

		private static void WriteToFile()
		{
            StreamWriter sw = new StreamWriter("Content/Text Files/HighScores.txt");

            foreach (Score score in HighscoreList)
            {
                string output = score.Name + "," + score.Points.ToString();
                sw.WriteLine(output);
            }

			sw.Close();
		}
	}
	class scoreComparer : Comparer<Score>
	{
		public override int Compare(Score x, Score y)
		{
			return y.Points.CompareTo(x.Points);
		}
	}
}
