using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wumpus
{
	class TriviaList
	{
		private List<Trivia> TriviaQuestions = new List<Trivia>();
		//Index of trivia to next be asked
		private int triviaIndex = 0;
		private string[] triviaHintArray = new string[2];

		Random random = new Random();

		public TriviaList()
		{
			//read info from file
			ReadFromFile();
			//randomize trivia
			RandomizeTrivia();
		}

		private void ReadFromFile()
		{
			StreamReader streamReader = new StreamReader("Content/Text Files/TriviaList.txt");

			string line;

			while (true)
			{
				line = streamReader.ReadLine();
				if (line == null)
					break;
				string[] data = line.Split(',');
				Trivia trivia = new Trivia(data[0], data[1], data[2], data[3], data[4], int.Parse(data[5]));
				TriviaQuestions.Add(trivia);
			}
			streamReader.Close();
		}

		public void RandomizeTrivia()
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

		public Trivia GetTrivia()
		{
			Trivia trivia = TriviaQuestions[triviaIndex];
			
			//Increase triviaIndex by 1, reset back to 0 if it hits end
			triviaIndex = (triviaIndex + 1 < TriviaQuestions.Count) ? triviaIndex + 1 : 0;

			trivia.RandomizeAnswers();

			return trivia;
		}

		public string[] GetHint()
		{
			int rndTriv = random.Next(TriviaQuestions.Count);

			while (rndTriv <= triviaIndex)
			{
				rndTriv = random.Next(TriviaQuestions.Count);
			}

			Trivia trivia = TriviaQuestions[rndTriv];

			triviaHintArray[0] = trivia.Question;
			triviaHintArray[1] = trivia.Answer4;

			return triviaHintArray;
		}

	}
}
