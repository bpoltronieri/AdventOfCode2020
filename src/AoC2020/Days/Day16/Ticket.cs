using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Days.Day16Utils
{
    class Ticket
    {
        public int[] Values { get; set; }

        public Ticket(int[] values)
        {
            Values = values;
        }

        // Looks for ticket values that don't fit any of the valid ranges in 
        // the given ticketFields list. Returns the ticket error rate:
        // the sum of those values.
        public int GetErrorRate(List<TicketField> ticketFields)
        {
            return Values
                .Where(v => InvalidValue(v, ticketFields))
                .Sum();
        }

        private bool InvalidValue(int v, List<TicketField> ticketFields)
        {
            return ticketFields.All(f => !f.ValidValue(v));
        }
        
        public bool IsInvalid(List<TicketField> ticketFields)
        {
            return Values.Any(v => InvalidValue(v, ticketFields));
        }
    }
}