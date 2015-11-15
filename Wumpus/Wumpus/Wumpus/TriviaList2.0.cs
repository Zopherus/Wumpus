using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wumpus
{
	class TriviaList
	{
		List<Trivia> trivias = new List<Trivia>();
		int triviaIndex = 0;
		string[] triviaStringArray = new string[4];
		string[] triviaHintArray = new string[2];

		Random rnd = new Random();

		int rightAnswer;

		//stubbData
		/*private void StubbInit()
		{
			Trivia t1 = new Trivia("What is 1 + 1?", "1", "4", "3", "2");
			trivias.Add(t1);

			Trivia t2 = new Trivia("What is 1 + 2?", "1", "2", "4", "3");
			trivias.Add(t2);

			Trivia t3 = new Trivia("What is 1 + 3?", "1", "2", "3", "4");
			trivias.Add(t3);

			Trivia t4 = new Trivia("What is 1 + 4?", "1", "2", "3", "5");
			trivias.Add(t4);

			Trivia t5 = new Trivia("What is 1 + 5?", "1", "2", "3", "6");
			trivias.Add(t5);
		}*/

		public TriviaList()
		{
			//read info from file
			ReadFromFile();
			//randomize trivia
			RandomizeTrivia();
		}

		private void ReadFromFile()
		{
			StreamReader sr = new StreamReader("Content/Text Files/TriviaList.txt");

			string input = sr.ReadLine();

			while (input != null)
			{
				string[] data = input.Split(',');

				Trivia t = new Trivia(data[0], data[1], data[2], data[3], data[4],int.Parse(data[5]));

				trivias.Add(t);

				input = sr.ReadLine();
			}
			sr.Close();
		}

		public void RandomizeTrivia()
		{
			int lastRnd = 0;

			for (int index = 0; index < trivias.Count; index++)
			{
				
				int rndTriv = rnd.Next(trivias.Count);

				Trivia t = trivias[lastRnd];
				trivias[lastRnd] = trivias[rndTriv];
				trivias[rndTriv] = t;

				lastRnd = rndTriv;
			}
		}

		public Trivia GetTrivia()
		{
			Trivia t = trivias[triviaIndex];

			triviaStringArray[0] = t.Answer1;
			triviaStringArray[1] = t.Answer2;
			triviaStringArray[2] = t.Answer3;
			triviaStringArray[3] = t.Answer4;
			
			triviaIndex = (triviaIndex < trivias.Count-1) ? triviaIndex+1 : 0;
			
			//Random rnd = new Random();
			int rndTriv = rnd.Next(3);

			string tR = triviaStringArray[3];
			triviaStringArray[3] = triviaStringArray[rndTriv];
			triviaStringArray[rndTriv] = tR;

			rightAnswer = rndTriv;

			t.Answer1 = triviaStringArray[0];
			t.Answer2 = triviaStringArray[1];
			t.Answer3 = triviaStringArray[2];
			t.Answer4 = triviaStringArray[3];
			t.Answer = rightAnswer + 1;

			return t;
		}

		public int GetAnswer()
		{
			return rightAnswer;
		}

		public string[] GetHint()
		{
			//Random rnd = new Random();
			int rndTriv = rnd.Next(trivias.Count);

			while (rndTriv <= triviaIndex) rndTriv = rnd.Next(trivias.Count);

			Trivia t = trivias[rndTriv];

			triviaHintArray[0] = t.Question;
			triviaHintArray[1] = t.Answer4;

			return triviaHintArray;
		}

	}
}
