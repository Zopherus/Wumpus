using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wumpus
{
	class Trivia
	{
		private string question;
		private string answer1;
		private string answer2;
		private string answer3;
		private string answer4;
		private int answer;

		public Trivia() { }

		public Trivia(string q, string a1, string a2, string a3, string a4, int ans)
		{
			question = q;
			answer1 = a1;
			answer2 = a2;
			answer3 = a3;
			answer4 = a4;
            answer = ans;
		}

		public int Answer
		{
			get { return answer; }
			set { answer = value; }
		}

		public string Question
		{
			get { return question; }
			set { question = value; }
		}

		public string Answer1
		{
			get { return answer1; }
			set { answer1 = value; }
		}

		public string Answer2
		{
			get { return answer2; }
			set { answer2 = value; }
		}

		public string Answer3
		{
			get { return answer3; }
			set { answer3 = value; }
		}

		public string Answer4
		{
			get { return answer4; }
			set { answer4 = value; }
		}
		
	}
}
