using System;

namespace RegScan
{
    public class AccessionNumberObj
    {
        private int _sequenceNumber;
        private int _scheduleNumber;
        private int _boxNumber;

        public int Sequence { get { return _sequenceNumber; } }
        public string SequenceString { get { return _sequenceNumber.ToString().PadLeft(2, '0'); } }

        public int Schedule { get { return _scheduleNumber; } }
        public string ScheduleString { get { return _scheduleNumber.ToString().PadLeft(4, '0'); } }

        public int BoxNumber { get { return _boxNumber; } set { _boxNumber = value; } }
        public string BoxString {  get { return _boxNumber.ToString().PadLeft(4, '0'); } } 

        public string Text
        {
            get { return string.Concat( SequenceString, ScheduleString, BoxString ); }
        }

        public string TextDashes
        {
            get { return string.Concat(SequenceString, "-", ScheduleString, "-", BoxString); }
        }

        public AccessionNumberObj(int seq, int sch, int boxNumber)
        {
            this._sequenceNumber = seq;
            this._scheduleNumber = sch;
            this.BoxNumber = boxNumber;
        }

        /// <summary>
        /// Uses a regex string will match and group digits based on their placement. Because the
        /// API returns a string value we we have to assume that it may not be well formatted. 
        /// If accNumb does not match the regex pattern there will be issues. See more about the
        /// Regex pattern used in the "<remarks>" section
        /// </summary>
        /// <param name="accString">String to be parsed for individual numbers</param>
        /// <remarks>
        /// The Regex pattern @"^(\d{0,2}).?(\d{4}).?(\d{4})$" can be read as:
        ///   @ verbatim text string. Wont interoperate 'escape' characters
        ///   ^ matches the start of the string (limits time and effort from 'greedy' algorithm)
        ///   (\d{0,2}) Group 1 matches 0 to 2 digits
        ///   .? matches 0 or 1 of any character
        ///   (\d{4}) Group 2 matches exactly 4 characters
        ///   (\d{4}) Group 3 matches exactly 4 characters
        ///   $ matches the end of the string
        /// So the following strings will match and result in the following groups:
        ///   "11-2222-3333" -> group 1 = "11", group 2 = "2222", group 3 = "3333"
        ///   "11 2222 3333" -> group 1 = "11", group 2 = "2222", group 3 = "3333"
        ///   "1122223333"   -> group 1 = "11", group 2 = "2222", group 3 = "3333"
        ///   "122223333"    -> group 1 = "1",  group 2 = "2222", group 3 = "3333"
        /// </remarks>
        public AccessionNumberObj(string accString)
        {
            if (accString.Length >= 8)
            {
                string pattern = @"^(\d{0,2}).?(\d{4}).?(\d{4})$";

                var match = System.Text.RegularExpressions.Regex.Match(accString, pattern);

                // If the input matched our pattern we can parse the groups
                if (match.Success)
                {
                    this._sequenceNumber = int.Parse(match.Groups[1].Value);
                    this._scheduleNumber = int.Parse(match.Groups[2].Value);
                    this._boxNumber = int.Parse(match.Groups[3].Value);
                }
            }
        }

        /// <summary>
        /// Given two accession number objects return true if all fields are identical, false
        /// if any one field differs. 
        /// </summary>
        /// <param name="a">The AccessionNumberObj to compare</param>
        /// <param name="b">The AccessionNumberObj to compare against</param>
        /// <returns>True if all fields are identical, false otherwise</returns>
        public static bool CompareAccessionObsj(AccessionNumberObj a, AccessionNumberObj b)
        {
            bool match = a._sequenceNumber == b._sequenceNumber &&
                a._scheduleNumber == b._scheduleNumber &&
                a._boxNumber == b._boxNumber;

            return match;
        }

    }
}
