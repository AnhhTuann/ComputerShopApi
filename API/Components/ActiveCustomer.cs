using System;
using System.Timers;
using System.Collections.Generic;
using DTO;

namespace API.Components
{
	public static class ActiveCustomer
	{
		private static Dictionary<int, Timer> tracker = new Dictionary<int, Timer>();

		public static void trackCustomer(int id)
		{
			Timer timer = new Timer(5 * 60 * 1000);

			timer.AutoReset = false;
			timer.Elapsed += (Object o, ElapsedEventArgs e) => untrackCustomer(id);
			tracker.Add(id, timer);
			timer.Start();
		}

		public static void resetInactiveTimeOut(int id)
		{
			Timer timer = tracker[id];

			timer.Stop();
			timer.Start();
		}

		public static void untrackCustomer(int id)
		{
			Timer timer = tracker[id];

			timer.Stop();
			timer.Dispose();
			tracker.Remove(id);
		}

		public static bool contain(int id)
		{
			return tracker.ContainsKey(id);
		}
	}
}