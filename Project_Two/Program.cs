//Advanced programming
//Project 2
//Aaron Perry

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Project_Two
{
	class Program
	{
		static void Main(string[] args)
		{

			//Finding file paths for where to read data from as well as where to create file and print data out to
			string filePath = Directory.GetCurrentDirectory();
			string backOne = Directory.GetParent(filePath).ToString();
			string backTwo = Directory.GetParent(backOne).ToString();
			string backThree = Directory.GetParent(backTwo).ToString();
			string csvFilePath = $@"{backThree}\Super_Bowl_Project.csv";
			string printPath = $@"{backThree}\Game_Stats";
			
			List<SuperBowl> gameRecords = new List<SuperBowl>();

			RecordGenerator(csvFilePath, ref gameRecords);
			StreamWriter writefile = new StreamWriter(printPath); //THIS IS THE INITIATION OF MY VARIABLE THAT IS EQUAL TO A STREAM WRITER TO THE ABOVE DECLARED FILE PATH
			writefile.AutoFlush = true;	// THIS IS THE AUTOFLUSH METHOD CALL ON MY STREAM WRITER TO OVERCOME THE BUFFER LIMIT. 
			WinnerGenerator(gameRecords, printPath, writefile);
			TopFiveGenerator(gameRecords, printPath, writefile);
			StateHosts(gameRecords, printPath, writefile);
			MVPList(gameRecords, printPath, writefile);
			CoachLose(gameRecords, printPath, writefile);
			CoachWin(gameRecords, printPath, writefile);
			TeamWin(gameRecords, printPath, writefile);
			TeamLose(gameRecords, printPath, writefile);
			BigPointDiff(gameRecords, printPath, writefile);
		}
		// create method to read from file and populate the super bowl stats into gameRecords List
		static void RecordGenerator(string csvFilePath, ref List<SuperBowl> gameRecords)
		{
			string[] myArray;//create an array for reading the data in initially. The array data type will help preserve the list of many items 
			FileStream myFile = new FileStream(csvFilePath, FileMode.Open, FileAccess.Read);
			StreamReader readFile = new StreamReader(myFile);
		

			while (!readFile.EndOfStream)
			{
				try
				{
					myArray = readFile.ReadLine().Split(',');
					gameRecords.Add(new SuperBowl(myArray[0], myArray[1], int.Parse(myArray[2]), myArray[3], myArray[4], myArray[5], int.Parse(myArray[6]), myArray[7], myArray[8], myArray[9], int.Parse(myArray[10]), myArray[11], myArray[12], myArray[13], myArray[14]));

				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}

			readFile.Close();
			myFile.Close();
		}

		static void WinnerGenerator(List<SuperBowl> gameRecords, string printPath, StreamWriter writefile)
		{
			//output list of Super Bowl winners
			
			
			writefile.WriteLine("*******Super Bowl Winners********");
			var Winners = from SuperBowl in gameRecords
						  select SuperBowl;
			foreach (SuperBowl record in Winners)
			{

				string output = String.Format("{0, -23} {1, -8} {2, -32} {3, -19} {4, -31} {5, -17}", record.winningTeam, record.Date, record.winningQB, record.winningCoach, record.MVP, record.PointDifference);
				writefile.WriteLine(output);
			}

		}
		static void TopFiveGenerator(List<SuperBowl> gameRecords, string printPath, StreamWriter writefile)
		{
			//ouput list fo top five attended super bowls
			
			
			writefile.WriteLine("*******Top 5 attended Super Bowls********");
			var topFive = (from SuperBowl in gameRecords
						   orderby SuperBowl.Attendance descending
						   select SuperBowl).Take(5);
			foreach (SuperBowl record in topFive)
			{
				string output = String.Format("{0, -8} {1, -23} {2, -23} {3, -16} {4, -15} {5, -17}", record.Date, record.winningTeam, record.losingTeam, record.gameCity, record.gameState, record.Stadium);
				writefile.WriteLine(output);
			}
		}

		static void StateHosts(List<SuperBowl> gameRecords, string printPath, StreamWriter writefile)
		{
			//output list of states that have hosted the most SB
			
			
			writefile.WriteLine("*******States that have hosted the most superbowls********");
			var topHosted = from SuperBowl in gameRecords
							group SuperBowl by SuperBowl.gameState into newGroup
							orderby newGroup.Count() descending
							select new { anotherGroup = newGroup.Key, Count = newGroup.Count() };
			var grab = topHosted.Take(5);
			foreach (var SuperBowl in grab)
			{

				writefile.WriteLine($"{SuperBowl.anotherGroup} have hosted the most superbowls with {SuperBowl.Count}. ");
			}
		}

		static void MVPList(List<SuperBowl> gameRecords, string printPath, StreamWriter writefile)
		{
			
			//output list of players who won the MVP more than twice
			writefile.WriteLine("*******Players who have won MVP more than twice********");
			var listMVP = from SuperBowl in gameRecords
						  group SuperBowl by SuperBowl.MVP into newGroup
						  orderby newGroup.Count() descending
						  select new { anotherGroup = newGroup.Key, Count = newGroup.Count() };
			var grab = listMVP.Take(5);
			foreach (var SuperBowl in grab)
			{
				string output = String.Format($"{SuperBowl.anotherGroup} has won MVP {SuperBowl.Count} times. ");
				writefile.WriteLine(output);
			}
		}
			
		static void CoachLose(List<SuperBowl> gameRecords, string printPath, StreamWriter writefile)
		{
			//Which coach has lost the most SB
			writefile.WriteLine("*******Coaches who have Lost the most Super Bowls********");
			var coachLoss = from SuperBowl in gameRecords
						  group SuperBowl by SuperBowl.losingCoach into newGroup
						  orderby newGroup.Count() descending
						  select new { anotherGroup = newGroup.Key, Count = newGroup.Count() };
			var grab = coachLoss.Take(1);
			foreach (var SuperBowl in grab)
			{
				string output = String.Format($"{SuperBowl.anotherGroup} as a coach has lost the Super Bowl {SuperBowl.Count} times. ");
				writefile.WriteLine(output);
			}
		}
			
		static void CoachWin(List<SuperBowl> gameRecords, string printPath, StreamWriter writefile)
		{
			//Which coach won the most SB
			writefile.WriteLine("*******Coaches who have Won the most Super Bowls********");
			var coachWon = from SuperBowl in gameRecords
						  group SuperBowl by SuperBowl.winningCoach into newGroup
						  orderby newGroup.Count() descending
						  select new { anotherGroup = newGroup.Key, Count = newGroup.Count() };
			var grab = coachWon.Take(1);
			foreach (var SuperBowl in grab)
			{
				string output = String.Format($"{SuperBowl.anotherGroup} as a coach has lost the Super Bowl {SuperBowl.Count} times. ");
				writefile.WriteLine(output);
			}
		}

		static void TeamWin(List<SuperBowl> gameRecords, string printPath, StreamWriter writefile)
		{
			//Which team won the most super bowls. 
			writefile.WriteLine("*******TEam that has Won the most Super Bowls********");
			var TeamWon = from SuperBowl in gameRecords
						  group SuperBowl by SuperBowl.winningTeam into newGroup
						  orderby newGroup.Count() descending
						  select new { anotherGroup = newGroup.Key, Count = newGroup.Count() };
			var grab = TeamWon.Take(1);
			foreach (var SuperBowl in grab)
			{
				string output = String.Format($"The {SuperBowl.anotherGroup} has won the Super Bowl {SuperBowl.Count} times. ");
				writefile.WriteLine(output);
			}
		}
		static void TeamLose(List<SuperBowl> gameRecords, string printPath, StreamWriter writefile)
		{
			//Which team(s) lost the most super bowls
			writefile.WriteLine("*******Team that has lost the most Super Bowls********");
			var TeamLoss = from SuperBowl in gameRecords
						   group SuperBowl by SuperBowl.losingTeam into newGroup
						   orderby newGroup.Count() descending
						   select new { anotherGroup = newGroup.Key, Count = newGroup.Count() };
			var grab = TeamLoss.Take(1);
			foreach (var SuperBowl in grab)
			{
				string output = String.Format($"The {SuperBowl.anotherGroup} has won the Super Bowl {SuperBowl.Count} times. ");
				writefile.WriteLine(output);
			}
		}
		static void BigPointDiff(List<SuperBowl> gameRecords, string printPath, StreamWriter writefile)
		{
			//Which super bowl had the biggest point difference
			writefile.WriteLine("*******Super Bowl that had the biggest point differnce********");
			var PDiff = (from SuperBowl in gameRecords
						   orderby SuperBowl.PointDifference descending
						   select SuperBowl).Take(1);
			foreach (SuperBowl record in PDiff)
			{
				string output = String.Format($" Super Bowl({record.SB}), {record.winningTeam} against {record.losingTeam} in {record.Date} had the largest point difference of {record.PointDifference}.");
				writefile.WriteLine(output);
			}
		}
		static void AverageAttendance(List<SuperBowl> gameRecords, string printPath, StreamWriter writefile)
		{
			//What is the average attendance of all the superbowls
			writefile.WriteLine("*******Super Bowl that had the biggest point differnce********");
			var Avg = from SuperBowl in gameRecords
					   group SuperBowl by SuperBowl.Attendance into newGroup
					   select new { anotherGroup = newGroup.Key,  aVerage = newGroup.Average() };
					  // (x => x.Attendance), } ;
		}
	}
}

