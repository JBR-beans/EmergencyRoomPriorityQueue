// Juliea Ritola
// IT113
// NOTES: 
// BEHAVIORS NOT IMPLIMENTED AND WHY:
namespace EmergencyRoomPriorityQueue_Ritola
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.IO;

	class Program
	{
		static PriorityQueue<Patient, int> ERQueue = new PriorityQueue<Patient, int>();
		static void Main()
		{
			LoadPatientsFromFile();

			char choice;
			do
			{
				Console.WriteLine("(A)dd Patient  (P)rocess Current Patient  (L)ist All in Queue  (Q)uit");
				choice = char.ToUpper(Console.ReadKey().KeyChar);
				Console.WriteLine();

				switch (choice)
				{
					case 'A':
						AddPatient();
						break;
					case 'P':
						ProcessPatient();
						break;
					case 'L':
						ListPatients();
						break;
					case 'Q':
						Console.WriteLine("Quitting the program...");
						break;
					default:
						Console.WriteLine("Invalid choice. Please try again.");
						break;
				}

			} while (choice != 'Q');
		}

		static void LoadPatientsFromFile()
		{
			try
			{
				string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Patients.csv");
				if (File.Exists(filePath))
				{
					string[] lines = File.ReadAllLines(filePath);

					if (lines.Length > 0 && lines[0] == "FirstName,LastName,DOB,Priority")
					{
						lines = lines.Skip(1).ToArray();
					}
					foreach (string line in lines)
					{
						string[] parts = line.Split(',');

						if (parts.Length == 4)
						{
							string firstName = parts[0].Trim();
							string lastName = parts[1].Trim();

							DateOnly birthdate = DateOnly.Parse(parts[2].Trim());
							int priority = int.Parse(parts[3].Trim());

							ERQueue.Enqueue(new Patient(firstName, lastName, birthdate, priority), priority);
							
						}
					}
				}
				else
				{
					Console.WriteLine("Patients.csv file not found.");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error loading patients: {ex.Message}");
			}
		}

		static void AddPatient()
		{
			Console.Write("Enter First Name: ");
			string firstName = Console.ReadLine();

			Console.Write("Enter Last Name: ");
			string lastName = Console.ReadLine();

			Console.Write("Enter Birthdate (MM/dd/yyyy): ");
			if (DateOnly.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly birthdate))
			{
				int priority = 5;
				if (birthdate.Year > DateTime.Now.Year - 21 || birthdate.Year < DateTime.Now.Year - 65)
				{
					priority = 1;
				}

				Patient newPatient = new Patient(firstName, lastName, birthdate, priority);
				ERQueue.Enqueue(newPatient, priority);

				Console.WriteLine($"Patient {newPatient} added to the queue.");
			}
			else
			{
				Console.WriteLine("Invalid date format. Please try again.");
			}
		}

		static void ProcessPatient()
		{
			if (ERQueue.TryDequeue(out var patient, out var priority))
			{
				Console.WriteLine($"Processing patient with priority {priority}: {patient}");
			}
			else
			{
				Console.WriteLine("Queue is empty. No patients to process.");
			}
		}

		static void ListPatients()
		{
			Console.WriteLine("Patients in Queue:");
			foreach (var patient in ERQueue.UnorderedItems)
			{
				Console.WriteLine(patient);
			}
		}

	}

}