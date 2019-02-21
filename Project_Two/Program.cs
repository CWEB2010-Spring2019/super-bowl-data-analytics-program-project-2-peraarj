using System;

namespace Project_Two
{
    class Program
    {
		public class Record {
			DateTime Date { get; set; }
			string SB { get; set; }
			int Attendance { get; set; }
			string winningQB { get; set; }
			string winningCoach { get; set; }
			string winningTeam { get; set; }
			int winningPoints { get; set; }
			string losingQB { get; set; }
			string losingCoach { get; set; }
			string losingteam { get; set; }
			int losingPoints { get; set; }
			string MVP { get; set; }
			string Stadium { get; set; }
			string gameCity { get; set; }
			string gameState { get; set; }
		}
	


		static void Main(string[] args)
		{
			/**Your application should allow the end user to pass a file path for output 
			* or guide them through generating the file.
			**/
			Console.WriteLine("Hello world. This is a commit test");

		}
	}


	
}
