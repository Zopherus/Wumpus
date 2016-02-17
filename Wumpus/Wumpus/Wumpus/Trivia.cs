using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wumpus
{
	class Trivia
	{
		public string Question { get; private set; }
		public string Answer1 { get; private set; }
		public string Answer2 { get; private set; }
		public string Answer3 { get; private set; }
		public string Answer4 { get; private set; }
        // number from 1 to 4 showing which answer is correct
		public int CorrectAnswer { get; private set; }

		public Trivia() { }

		public Trivia(string Question, string Answer1, string Answer2, string Answer3, string Answer4, int CorrectAnswer)
		{
			this.Question = Question;
			this.Answer1 = Answer1;
			this.Answer2 = Answer2;
			this.Answer3 = Answer3;
			this.Answer4 = Answer4;
			this.CorrectAnswer = CorrectAnswer;
		}

		public void RandomizeAnswers()
		{
			Random random = new Random();
			string[] answers = { Answer1, Answer2, Answer3, Answer4 };
            string correctAnswer = answers[CorrectAnswer - 1];
            // Use Fisher-Yates shuffle to randomize trivia
			int position = answers.Length;
			while (position > 1)
			{
				// Creates a random int from 0 to position, not including position to swap to
				// Decreases position by 1
				int swapPosition = random.Next(position--);
				string temp = answers[position];
				answers[position] = answers[swapPosition];
				answers[swapPosition] = temp;
			}
			// Reset the answers to the randomized answers
			Answer1 = answers[0];
			Answer2 = answers[1];
			Answer3 = answers[2];
			Answer4 = answers[3];
		}
	}
}
