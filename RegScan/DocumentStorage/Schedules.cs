using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Empty constructor. Used by Newtonsoft.JSON package for serializing and deserializing
        /// text to structs.
        /// </summary>
        public Schedules()
        {
        }

        /// <summary>
        /// Constructor from AccessionNumberObj. Break the sequence and schedule number out. 
        /// </summary>
        /// <param name="accNumb">Given accessionNumberObj</param>
        public Schedules(AccessionNumberObj accNumb)
        {
            _sequenceNumber = accNumb.Sequence;
            _scheduleNumber = accNumb.Schedule;
        }

        /// <summary>
        /// Constructor from a string. If the string matches the structure 
        ///    <EE>.<CCCC>
        /// where EE can be a 1 to 2 digits and CCCC can be 1 to 4 digits. The separator <.>
        /// can be any single character or not existent (0 to 1 characters). 
        /// </summary>
        /// <param name="seqSchText">String schedule value</param>
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

        /// <summary>
        /// Request the list of schedule values from DRS API. Update static field to match
        /// the new data.
        /// </summary>
        /// <exception cref="TypeAccessException">
        /// Thrown if the string returned from the API is null or empty
        /// </exception>
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
