using System;

namespace Authorizer.JsonObjects
{
	class Transaction
	{
		public String Merchant { get; set; }
		public Int32 Amount { get; set; }
		public DateTime Time { get; set; }
	}
}