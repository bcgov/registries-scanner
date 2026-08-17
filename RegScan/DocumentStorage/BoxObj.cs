using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegScan
{
    public class BoxObj
    {
        public DateTime DEFAULTDATE = new DateTime(1900, 1, 2);
        private int _boxID;
        private int _boxNumber;
        private DateTimeOffset _openedDate;
        private DateTimeOffset _closedDate;
        private bool _closedSet = false;
        private bool _closed = false;
        private int _pageCount;
        private int _sequenceNumber;
        private int _scheduleNumber;

        [JsonProperty("boxId")]
        public int BoxId { get { return _boxID; } set { _boxID = value; } }
        [JsonProperty("boxNumber")]
        public int BoxNumber { get { return _boxNumber; } set { _boxNumber = value; } }
        [JsonProperty("openedDate")]
        public DateTimeOffset OpenedDate { 
            get { return _openedDate; } set { _openedDate = value;  } 
        }
        public string OpenedDateString
        {
            get { return _openedDate.ToString("MMM dd, yyyy"); }
        }
        [JsonProperty("closedDate")]
        public DateTimeOffset ClosedDate { 
            get { return _closedDate; } 
            set { 
                _closedDate = value;
                SetClosed();
            } 
        }
        public string ClosedDateString
        {
            get
            {
                if (!_closedSet)
                    SetClosed();
                return _closed? _closedDate.ToString("MMM dd, yyyy") : "N/A";
            }
        }
        public bool IsClosed { get {
                if (!_closedSet)
                    SetClosed();
                return _closed; 
            } }
        public string Status { get { return _closed ? "Closed" : "Open"; } }
        [JsonProperty("pageCount")]
        public int PageCount { get { return _pageCount; } set { _pageCount = value; } }
        [JsonProperty("scheduleNumber")]
        public int ScheduleNumber { 
            get { return _scheduleNumber; } set { _scheduleNumber = value; }
        }
        [JsonProperty("sequenceNumber")]
        public int SequenceNumber { 
            get { return _sequenceNumber; } set { _sequenceNumber = value; }
        }
        public string AccessionNumber
        {
            get { 
                return String.Concat(
                    _sequenceNumber.ToString(), _scheduleNumber.ToString(), _boxNumber.ToString());
            }
        }
        public string AccessionNumberDashes
        {
            get
            {
                return String.Concat(_sequenceNumber.ToString(), "-", 
                    _scheduleNumber.ToString(), "-", _boxNumber.ToString());
            }
        }

        /// <summary>
        /// Sets the closed status for the box. This is based off the boxes _closedDate
        /// </summary>
        /// <remarks>
        /// There are 2 conditions that must be met for a box to be considered closed:
        ///     - The closed date is not null
        ///     - The closed date is greater (more recent) than the default date
        /// Both must be true for the box to be closed.
        /// </remarks>
        public void SetClosed()
        {
            _closed = (_closedDate != null && _closedDate > DEFAULTDATE);
            _closedSet = true;
        }


    }

}
