using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AoC2020.Days.Day16Utils;

namespace AoC2020.Days
{
    public class Day16 : IDay
    {
        private Ticket MyTicket;
        private List<Ticket> NearbyTickets;
        private List<TicketField> TicketFields;

        public Day16(string file)
        {
            NearbyTickets = new List<Ticket>();
            TicketFields = new List<TicketField>();
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
            string[] input = File.ReadAllLines(file);
            var line = 0;
            while (input[line].Length > 0) // parse the different field requirements
            {
                var splitLine = input[line].Split(": ");
                var fieldName = splitLine[0];
                var ranges = splitLine[1].Split(" or ");

                var range1 = ranges[0]
                    .Split('-')
                    .Select(x => int.Parse(x))
                    .ToArray(); 

                var range2 = ranges[1]
                    .Split('-')
                    .Select(x => int.Parse(x))
                    .ToArray();  

                var field = new TicketField(fieldName, range1, range2);
                TicketFields.Add(field);
               
                line += 1;
            }
            line += 2; // parse my ticket values
            var myValues = input[line]
                .Split(',')
                .Select(x => int.Parse(x))
                .ToArray();
            MyTicket = new Ticket(myValues);

            line += 3;
            while (line < input.Length) // parse nearby ticket values
            {
            var values = input[line]
                .Split(',')
                .Select(x => int.Parse(x))
                .ToArray();
            var ticket = new Ticket(values);
            NearbyTickets.Add(ticket);

            line += 1;
            }
        }     

        public string PartOne()
        {
            return NearbyTickets
                .Select(t => t.GetErrorRate(TicketFields))
                .Sum()
                .ToString();
        }

        // Part Two uses valid tickets, including my own, to figure
        // out which ticket values correspond to which field.
        // Returns product of all values in my ticket
        // with field beginning with "departure"
        public string PartTwo()
        {
            NearbyTickets.RemoveAll(t => t.IsInvalid(TicketFields));

            var IDToPossibleFields = InitPossibleFieldsDict();
            var done = false;

            if (!MyTicket.IsInvalid(TicketFields)) // problem didn't explicitly say my ticket is valid
                done = RuleOutPossibleFields(MyTicket, IDToPossibleFields);

            foreach (var ticket in NearbyTickets)
            {
                done = done || RuleOutPossibleFields(ticket, IDToPossibleFields);
                if (done) break;
            }
            if (!done) throw new InvalidOperationException();

            Int64 result = 1;
            for (var i = 0; i < MyTicket.Values.Count(); i++)
            {
                if (IDToPossibleFields[i][0].Split()[0] == "departure")
                    result *= MyTicket.Values[i];
            }
            return result.ToString();
        }

        // Uses values in given ticket to rule out possible fields for each index.
        // Returns true if we're down to a single possible mapping.
        private bool RuleOutPossibleFields(Ticket ticket, Dictionary<int, List<string>> IDToPossibleFields)
        {
            for (var i = 0; i < ticket.Values.Count(); i++)
            {
                var value = ticket.Values[i];
                foreach (var field in TicketFields)
                {
                    if (!field.ValidValue(value))
                        RemoveFieldFromID(IDToPossibleFields, field.Name, i);
                }
            }
            return IDToPossibleFields.Values.All(p => p.Count() == 1);
        }

        private void RemoveFieldFromID(Dictionary<int, List<string>> IDToPossibleFields, string fieldName, int i)
        {
            var removed = IDToPossibleFields[i].Remove(fieldName);
            if (removed && IDToPossibleFields[i].Count() == 0)
                throw new InvalidOperationException();
            if (removed && IDToPossibleFields[i].Count() == 1)
                RemoveFieldFromOtherIDs(IDToPossibleFields, i);
        }

        // Called when we have found which field maps to the given index. We then remove
        // that field from the lists of possible fields for all the other indices.
        private void RemoveFieldFromOtherIDs(Dictionary<int, List<string>> IDToPossibleFields, int i)
        {
            var fieldName = IDToPossibleFields[i][0];
            for (var j = 0; j < IDToPossibleFields.Count(); j++)
            {
                if (j == i) continue;
                RemoveFieldFromID(IDToPossibleFields, fieldName, j);
            }
        }

        // initialises dict mapping field indicesto list of possible field names
        private Dictionary<int, List<string>> InitPossibleFieldsDict()
        {
            var dict = new Dictionary<int, List<string>>();
            var allNames = TicketFields
                .Select(f => f.Name)
                .ToList();

            for (var i = 0; i < allNames.Count; i++)
            {   
                var allNamesCopy = new List<string>(allNames);
                dict.Add(i, allNamesCopy);
            }

            return dict;
        }
    }
}