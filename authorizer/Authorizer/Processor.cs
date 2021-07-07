using System;
using System.Collections.Generic;
using System.Linq;
using Authorizer.JsonObjects;
using Newtonsoft.Json;

namespace Authorizer
{
	class Processor
	{
		private readonly IList<String> input;
		private readonly IList<String> output;

		private Account account;
		private readonly IList<Transaction> transactions;

		public Processor(IList<String> input)
		{
			this.input = input;
			output = new List<String>();
			transactions = new List<Transaction>();
		}

		public IList<String> Interpret()
		{
			foreach (var line in input)
			{
				interpret(line);
			}

			return output.ToList();
		}

		private void interpret(String json)
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

			output.Add(
				JsonConvert.SerializeObject(
					printed,
					settings
				)
			);
		}
	}
}