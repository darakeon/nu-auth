using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authorizer.JsonObjects
{
	class Main
	{
		public Main()
		{
			Violations = new List<String>();
		}

		[JsonProperty("account")]
		public Account Account { get; set; }

		[JsonProperty("transaction")]
		public Transaction Transaction { get; set; }

		[JsonProperty("violations")]
		public List<String> Violations { get; set; }
	}
}