using System;

namespace Project_Two
{
	
	public class SuperBowl
	{
		public string Date { get; set; }
		public string SB { get; set; }
		public int Attendance { get; set; }
		public string winningQB { get; set; }
		public string winningCoach { get; set; }
		public string winningTeam { get; set; }
		public int winningPoints { get; set; }
		public string losingQB { get; set; }
		public string losingCoach { get; set; }
		public string losingTeam { get; set; }
		public int losingPoints { get; set; }
		public string MVP { get; set; }
		public string Stadium { get; set; }
		public string gameCity { get; set; }
		public string gameState { get; set; }
		public int PointDifference { get; set; }

		public SuperBowl(string Date, string SB, int Attendance, string winningQB, string winningCoach, string winningTeam, int winningPoints, string losingQB,
			string losingCoach, string losingTeam, int losingPoints, string MVP, string Stadium, string gameCity, string gameState)
		{
			this.Date = Date;
			this.SB = SB;
			this.Attendance = Attendance;
			this.winningQB = winningQB;
			this.winningCoach = winningCoach;
			this.winningTeam = winningTeam;
			this.winningPoints = winningPoints;
			this.losingQB = losingQB;
			this.losingCoach = losingCoach;
			this.losingTeam = losingTeam;
			this.losingPoints = losingPoints;
			this.MVP = MVP;
			this.Stadium = Stadium;
			this.gameCity = gameCity;
			this.gameState = gameState;
			this.PointDifference = winningPoints - losingPoints;

		}
	}
}
