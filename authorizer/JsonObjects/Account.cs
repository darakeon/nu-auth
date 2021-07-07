using System;
using Newtonsoft.Json;

namespace Authorizer.JsonObjects
{
	class Account
	{
		[JsonProperty("active-card")]
		public Boolean ActiveCard { get; set; }

		[JsonProperty("available-limit")]
		public Int32 AvailableLimit { get; set; }
	}
}