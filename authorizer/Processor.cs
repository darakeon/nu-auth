using System;
using System.Collections.Generic;
using System.Linq;
using Authorizer.JsonObjects;
using Newtonsoft.Json;

namespace Authorizer
{
	class Processor
	{
		private Account account;

		private IList<Transaction> transactions
			= new List<Transaction>();

		public void Interpret(String json)
		{
			var obj = JsonConvert.DeserializeObject<Main>(json);

			if (obj == null)
				return;

			if (obj.Account != null)
				processAccount(obj);
			else if (obj.Transaction != null)
				processTransaction(obj);

			print(obj);
		}

		private void processAccount(Main main)
		{
			if (account != null)
			{
				main.AddViolation("account-already-initialized");
			}
			else
			{
				account = main.Account;
			}
		}

		private void processTransaction(Main main)
		{
			if (account == null)
			{
				main.AddViolation("account-not-initialized");
				return;
			}

			if (!account.ActiveCard)
			{
				main.AddViolation("card-not-active");
			}

			var transaction = main.Transaction;

			if (transaction.Amount > account.AvailableLimit)
			{
				main.AddViolation("insufficient-limit");
			}

			var last2Minutes = transactions.Where(
				t => t.Time >= transaction.Time.AddMinutes(-2)
			).ToList();

			if (last2Minutes.Count > 2)
			{
				main.AddViolation("high-frequency-small-interval");
			}

			var similar = last2Minutes.Any(
				t => t.Merchant == transaction.Merchant
					&& t.Amount == transaction.Amount
			);

			if (similar)
			{
				main.AddViolation("double-transaction");
			}

			if (main.NoViolations)
			{
				transactions.Add(transaction);
				account.AvailableLimit -= transaction.Amount;
			}
		}

		private void print(Main main)
		{
			var printed = new
			{
				account = (object)account ?? new {},
				violations = main.Violations,
			};

			var settings = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			};

			Console.WriteLine(
				JsonConvert.SerializeObject(
					printed,
					settings
				)
			);
		}
	}
}