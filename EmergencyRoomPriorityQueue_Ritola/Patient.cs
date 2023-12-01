using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyRoomPriorityQueue_Ritola
{
	class Patient
	{
		public string FirstName { get; }
		public string LastName { get; }
		public DateOnly Birthdate { get; }
		public int Priority { get; }

		public Patient(string firstName, string lastName, DateOnly birthdate, int priority)
		{
			FirstName = firstName;
			LastName = lastName;
			Birthdate = birthdate;
			Priority = priority;
		}

		public override string ToString()
		{
			return $"{LastName}, {FirstName}, {Birthdate.ToString("MM/dd/yyyy")}";
		}
		
	}
}
