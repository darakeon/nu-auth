using System;
using Newtonsoft.Json;

namespace Authorizer.JsonObjects
{
	class Transaction
	{
		[JsonProperty("merchant")]
		public String Merchant { get; set; }

		[JsonProperty("amount")]
		public Int32 Amount { get; set; }

		[JsonProperty("time")]
		public DateTime Time { get; set; }
	}
}