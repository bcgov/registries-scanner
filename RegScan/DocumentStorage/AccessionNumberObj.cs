using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegScan
{
    public class AccessionNumberObj
    {
        private SequenceScheduleObj _sequenceSchedule;
        private int _boxNumber;
        public SequenceScheduleObj SequenceSchedule { get { return _sequenceSchedule; } }
        public int BoxNumber { get { return _boxNumber; } set { _boxNumber = value; } }
        public string BoxNumberString {  get { return _boxNumber.ToString(); } } 

        public string AccessionNumberString
        {
            get
            {
                return string.Concat(
                    _sequenceSchedule.SequenceString,
                    _sequenceSchedule.ScheduleString,
                    BoxNumber
                    );
            }
        }

        public AccessionNumberObj(SequenceScheduleObj seqSch, int boxNumber)
        {
            this._sequenceSchedule = seqSch;
            this.BoxNumber = boxNumber;
        }

        public AccessionNumberObj(int sequenceNumber, int scheduleNumber, int boxNumber) : 
            this(new SequenceScheduleObj(sequenceNumber, scheduleNumber), boxNumber) { }

        public AccessionNumberObj(string accString)
        {
            if (accString.Length >= 8)
            {
                string pattern = @"^(\d{0,2}).?(\d{4}).?(\d{4})$";

                var match = System.Text.RegularExpressions.Regex.Match(accString, pattern);

                // If the input matched our pattern we can parse the groups
                if (match.Success)
                {
                    int seq = int.Parse(match.Groups[1].Value);
                    int sch = int.Parse(match.Groups[2].Value);
                    this._sequenceSchedule = new SequenceScheduleObj(seq, sch); ;
                    this.BoxNumber = int.Parse(match.Groups[3].Value);
                }
            }
        }

    }
}
