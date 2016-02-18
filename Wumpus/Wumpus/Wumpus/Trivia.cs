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
		public string[] Answers { get; private set; }
        // number from 1 to 4 showing which answer is correct
		public int CorrectAnswer { get; private set; }

		public Trivia() { }

		public Trivia(string Question, string[] Answers, int CorrectAnswer)
		{
			this.Question = Question;
            this.Answers = Answers;
			this.CorrectAnswer = CorrectAnswer;
		}

		public void RandomizeAnswers()
		{
			Random random = new Random();
            string correctAnswer = Answers[Answers.Length - 1];
            // Use Fisher-Yates shuffle to randomize trivia
			int position = Answers.Length;
			while (position > 1)
			{
				// Creates a random int from 0 to position, not including position to swap to
				// Decreases position by 1
				int swapPosition = random.Next(position--);
				string temp = Answers[position];
				Answers[position] = Answers[swapPosition];
				Answers[swapPosition] = temp;
			}
		}
	}
}
