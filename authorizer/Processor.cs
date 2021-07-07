using System;
using Authorizer.JsonObjects;
using Newtonsoft.Json;

namespace Authorizer
{
	class Processor
	{
		public void Interpret(String json)
		{
			var obj = JsonConvert.DeserializeObject<Main>(json);

			if (obj.Account != null)
				processAccount(obj.Account);
			else if (obj.Transaction != null)
				processTransaction(obj.Transaction);

			print(obj);
		}

		private void processAccount(Account account)
		{
			Console.WriteLine("AC: {0}", account.ActiveCard);
			Console.WriteLine("AL: {0}", account.AvailableLimit);
		}

		private void processTransaction(Transaction transaction)
		{
			Console.WriteLine("M: {0}", transaction.Merchant);
			Console.WriteLine("A: {0}", transaction.Amount);
			Console.WriteLine("T: {0}", transaction.Time);
		}

		private void print(Main main)
		{
			Console.WriteLine(
				JsonConvert.SerializeObject(
					main,
					new JsonSerializerSettings
					{
						NullValueHandling = NullValueHandling.Ignore
					}
				)
			);
		}
	}
}