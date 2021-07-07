using System;
using System.Collections.Generic;
using System.Linq;

namespace Authorizer.JsonObjects
{
	class Main
	{
		public Main()
		{
			Violations = new List<String>();
		}

		public Account Account { get; set; }
		public Transaction Transaction { get; set; }
		public List<String> Violations { get; set; }

		public Boolean NoViolations => !Violations.Any();

		public void AddViolation(String violation)
		{
			Violations.Add(violation);
		}
	}
}