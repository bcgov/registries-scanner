using Newtonsoft.Json;
using PdfSharp.Pdf.Content.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegScan.DocumentStorage
{
    public class Schedules
    {
        private int _sequenceNumber;
        private int _scheduleNumber;
        private static List<Schedules> _scheduleList;

        [JsonProperty("sequenceNumber")]
        public int Sequence { get { return _sequenceNumber; } set { _sequenceNumber = value; } }
        [JsonIgnore]
        public string SequenceString { get { return _sequenceNumber.ToString().PadLeft(2, '0'); } }

        [JsonProperty("scheduleNumber")]
        public int Schedule { get { return _scheduleNumber; } set { _scheduleNumber = value; } }
        [JsonIgnore]
        public string ScheduleString { get { return _scheduleNumber.ToString().PadLeft(4, '0'); } }

        [JsonIgnore]
        public string PrettyText { get { return string.Concat(SequenceString, "-", ScheduleString); } }
        [JsonIgnore]
        public static List<Schedules> ScheduleList { get { return _scheduleList; } }

        public Schedules()
        {
        }

        public Schedules(AccessionNumberObj accNumb)
        {
            _sequenceNumber = accNumb.Sequence;
            _scheduleNumber = accNumb.Schedule;
        }

        public Schedules(string seqSchText)
        {
            if (seqSchText.Length >= 7)
            {
                string pattern = @"^(\d{0,2}).?(\d{4})$";

                var match = System.Text.RegularExpressions.Regex.Match(seqSchText, pattern);

                // If the input matched our pattern we can parse the groups
                if (match.Success)
                {
                    this._sequenceNumber = int.Parse(match.Groups[1].Value);
                    this._scheduleNumber = int.Parse(match.Groups[2].Value);
                }
            }
        }

        public static void RefreshList()
        {
            string newSchedules = ScheduleAPI.GetSchedules();

            if (string.IsNullOrEmpty(newSchedules))
            {
                throw new TypeAccessException("API returned null or empty string.");
            }

            // update the JSON payload to an iterable list and set as _scheduleList
            _scheduleList = JsonConvert.DeserializeObject<List<Schedules>>(newSchedules);
        }
    }
}
