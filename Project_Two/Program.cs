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

			QueryGenerator(gameRecords, printPath);


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

		static void QueryGenerator(List<SuperBowl> gameRecords, string printPath)
		{
			StreamWriter writefile = new StreamWriter(printPath);

			//output list of Super Bowl winners
			writefile.WriteLine("*******Super Bowl Winners********");
			var Winners = from SuperBowl in gameRecords
						  select SuperBowl;
			foreach(SuperBowl record in Winners)
			{
			
				string output = String.Format("{0, -23} {1, -8} {2, -32} {3, -19} {4, -31} {5, -17}", record.winningTeam, record.Date, record.winningQB, record.winningCoach, record.MVP, record.PointDifference);
				writefile.WriteLine(output);
			}


			//ouput list fo top five attended super bowls
			writefile.WriteLine("*******Top 5 attended Super Bowls********");
			var topFive = (from SuperBowl in gameRecords
						   orderby SuperBowl.Attendance descending
						   select SuperBowl).Take(5);
			foreach(SuperBowl record in topFive)
			{
				string output = String.Format("{0, -8} {1, -23} {2, -23} {3, -16} {4, -15} {5, -17}", record.Date, record.winningTeam, record.losingTeam, record.gameCity, record.gameState, record.Stadium);
				writefile.WriteLine(output);
			}

			//output list of states that have hosted the most SB
			writefile.WriteLine("*******States that have hosted the most superbowls********");
			var topHosted = from SuperBowl in gameRecords
							group SuperBowl by SuperBowl.gameState into newGroup
							orderby newGroup.Count() descending
							select new { anotherGroup = newGroup.Key, Count = newGroup.Count() };
			var grab = topHosted.Take(1);
			foreach (var SuperBowl in grab)
			{
				
				writefile.WriteLine($"{SuperBowl.anotherGroup} has hosted the most superbowls. ");
			}

			//output list of players who won the MVP more than twice
			writefile.WriteLine("*******Players who have won MVP more than twice********");
			var listMVP = from SuperBowl in gameRecords
						  group SuperBowl by SuperBowl.MVP into group1
						  orderby group1.Count() descending
						  where group1.Count() > 1
						  select group1;
			foreach (SuperBowl record in listMVP)
			{
				string output = String.Format("{0, -8} {1, -23} {2, -23} ", record.winningQB, record.winningTeam, record.losingTeam);
				writefile.WriteLine(output);
			}
			//Which coach has lost the most SB

			//Which coach won the most SB

			//Which team(s) lost the most super bowls

			//
		}
	}
}
