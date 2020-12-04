using System;
using System.Timers;
using System.Collections.Generic;
using DTO;

namespace API.Components
{
	public static class ActiveCustomer
	{
		private static Dictionary<Person, Timer> tracker = new Dictionary<Person, Timer>();

		public static void trackCustomer(Person customer)
		{
			Timer timer = new Timer(5 * 60 * 1000);

			timer.AutoReset = false;
			timer.Elapsed += (Object o, ElapsedEventArgs e) => untrackCustomer(customer);
			tracker.Add(customer, timer);
			timer.Start();
		}

		public static void resetInactiveTimeOut(Person customer)
		{
			Timer timer = tracker[customer];

			timer.Stop();
			timer.Start();
		}

		public static void untrackCustomer(Person customer)
		{
			Timer timer = tracker[customer];

			timer.Stop();
			timer.Dispose();
			tracker.Remove(customer);
		}
	}
}