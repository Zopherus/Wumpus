using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wumpus
{
	static class TriviaList
	{
		private static List<Trivia> TriviaQuestions = new List<Trivia>();
		//Index of trivia to next be asked
		private static int triviaIndex = 0;
		private static string[] triviaHintArray = new string[2];

		private static Random random = new Random();

		public static void InitializeTriviaList()
		{
			//read info from file
			ReadFromFile();
			//randomize trivia
			RandomizeTrivia();
		}

		private static void ReadFromFile()
		{
			StreamReader streamReader = new StreamReader("Content/Text Files/TriviaList.txt");

			string line;

			while (true)
			{
				line = streamReader.ReadLine();
				if (line == null)
					break;
				string[] data = line.Split(',');
                string question = data[0];
                int position = 1;
                List<string> answers = new List<string>();
                while (position <= data.Length - 2)
                {
                    answers.Add(data[position]);
                    position++;
                }
                int correctAnswer = int.Parse(data[data.Length - 1]);
				Trivia trivia = new Trivia(data[0], answers.ToArray(), correctAnswer);
				TriviaQuestions.Add(trivia);
			}
			streamReader.Close();
		}

		public static void RandomizeTrivia()
		{
			// Uses Fisher-Yates shuffle to randomize the trivia questions
			int position = TriviaQuestions.Count;
			while (position > 1)
			{
				//creates a random int from 0 to position, not including position to swap to
				//decreases position by 1
				int swapPosition = random.Next(position--);
				Trivia temp = TriviaQuestions[position];
				TriviaQuestions[position] = TriviaQuestions[swapPosition];
				TriviaQuestions[swapPosition] = temp;
			}
		}

		public static Trivia GetTrivia()
		{
			Trivia trivia = TriviaQuestions[triviaIndex];
			
			//Increase triviaIndex by 1, reset back to 0 if it hits end
			triviaIndex = (triviaIndex + 1 < TriviaQuestions.Count) ? triviaIndex + 1 : 0;

			trivia.RandomizeAnswers();

			return trivia;
		}

		public static string[] GetHint()
		{
			int rndTriv = random.Next(TriviaQuestions.Count);

			while (rndTriv <= triviaIndex)
			{
				rndTriv = random.Next(TriviaQuestions.Count);
			}

			Trivia trivia = TriviaQuestions[rndTriv];

			triviaHintArray[0] = trivia.Question;
			//triviaHintArray[1] = trivia.Answer4;

			return triviaHintArray;
		}

	}
}
