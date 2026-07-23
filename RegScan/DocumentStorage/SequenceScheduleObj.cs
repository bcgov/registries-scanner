using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegScan
{
    public class SequenceScheduleObj
    {
        private int _sequenceNumber;
        private int _scheduleNumber;

        public int SequenceNumber
        {
            get { return _sequenceNumber; }
            set { _sequenceNumber = value; }
        }
        public string SequenceString
        {
            get { return _sequenceNumber.ToString(); }
        }
        public int ScheduleNumber
        {
            get { return _scheduleNumber; }
            set { _scheduleNumber = value; }
        }
        public string ScheduleString
        {
            get { return _scheduleNumber.ToString(); }
        }

        public SequenceScheduleObj(int sequenceNumber, int scheduleNumber)
        {
            this._sequenceNumber = sequenceNumber;
            this._scheduleNumber = scheduleNumber;
        }
    }
}
