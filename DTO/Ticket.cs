using System.Collections.Generic;
using System;

namespace DTO
{
	public class Ticket : Entity
	{
		public string Date { get; set; } = DateTime.Now.ToShortDateString();
		public Staff Staff { get; set; }
		public int TotalAmount { get; set; } = 0;
		public List<TicketDetails> Details { get; set; } = new List<TicketDetails>();
	}
}