using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegScan
{
    public class BoxObj
    {
        public DateTime DEFAULTDATE = new DateTime(1900, 1, 2);
        private int _boxID;
        private int _boxNumber;
        private int _maxBatchID;
        private DateTimeOffset _openedDate;
        private DateTimeOffset _closedDate;
        private bool _closedSet = false;
        private bool _closed = false;
        private int _pageCount;
        private int _sequenceNumber;
        private int _scheduleNumber;

        private AccessionNumberObj _accessionNumber;

        private static ICollection<BoxObj> _boxes;

        [JsonIgnore]
        public static ICollection<BoxObj> Boxes { get { return _boxes; } }

        [JsonProperty("boxId")]
        public int BoxId { get { return _boxID; } set { _boxID = value; } }
        [JsonProperty("boxNumber")]
        public int BoxNumber { get { return _boxNumber; } set { _boxNumber = value; } }
        [JsonProperty("openedDate")]
        public DateTimeOffset OpenedDate { 
            get { return _openedDate; } set { _openedDate = value;  } 
        }
        [JsonIgnore]
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
        [JsonIgnore]
        public string ClosedDateString
        {
            get
            {
                if (!_closedSet)
                    SetClosed();
                return _closed? _closedDate.ToString("MMM dd, yyyy") : "N/A";
            }
        }
        [JsonIgnore]
        public bool IsClosed { get {
                if (!_closedSet)
                    SetClosed();
                return _closed; 
            } }
        [JsonIgnore]
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
        /// <summary>
        ///  The max batchID is used to determine if a box should be updated or not
        /// </summary>
        [JsonIgnore]
        public int MaxBatchID { get { return _maxBatchID; } set { _maxBatchID = value; } }

        [JsonIgnore]
        public string InfoString
        {
            get
            {
                return string.Concat("\tAccession Number:\t" + this.AccessionNumber.TextDashes +
                    "\n\tOpened on:\t\t" + this.OpenedDateString + "\t\n" +
                    "\tClosed on:\t\t" + this.ClosedDateString + "\t\n" +
                    "\tTotal number of pages:\t" + this.PageCount.ToString());

            }
        }

        [JsonIgnore]
        public string AccessionNumberTextDashes
        {
            get {
                return AccessionNumber != null ? AccessionNumber.TextDashes : string.Empty; }
        }

        [JsonIgnore]
        public AccessionNumberObj AccessionNumber
        {
            get { 
                UpdateAccessionNumber();
                return _accessionNumber;
            }
            set {
                UpdateAccessionNumber();
            }
        }

        public BoxObj()
        {
            // Ensure the Accession Number is set when the box is created
            //AccessionNumber = new AccessionNumberObj(_sequenceNumber, _scheduleNumber, _boxNumber);
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateAccessionNumber()
        {
            _accessionNumber = new AccessionNumberObj(
                _sequenceNumber, _scheduleNumber, _boxNumber);
        }
        
        /// <summary>
        /// Used to undo any information set while trying to close the box. This should only
        /// be used in the case that a box was attempted to be closed but encountered an error.
        /// </summary>
        public void ReopenBox()
        {
            _closedDate = DEFAULTDATE;
            _closedSet = false;
            _closed = false;
        }

        /// <summary>
        /// Used to request the list of all box records stored in the DRS API. The result is
        /// then used to populate the static class parameter _boxes.
        /// </summary>
        /// <exception cref="TypeAccessException">Thrown when the response is null or empty </exception>
        public static void RefreshBoxList()
        {
            string boxList = BoxAPI.GetAllBoxes();

            if (string.IsNullOrEmpty(boxList))
            {
                throw new TypeAccessException("API returned null or empty string.");
            }

            // update the JSON payload to an iterable list and set as _boxes
            _boxes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BoxObj>>(boxList);

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

        /// <summary>
        /// Find a particular box in the list and update the fields to match. 
        /// </summary>
        /// <param name="updatedBox">BoxObject with updated parameters</param>
        public static void UpdateBoxInList(BoxObj updatedBox)
        {
            // Get a reference to the box in the list
            BoxObj boxInList = _boxes.FirstOrDefault(x => x.BoxId == updatedBox.BoxId);
            if (boxInList != null)
            {
                boxInList = updatedBox;
            }
        }

        /// <summary>
        /// Creates a new <see cref="BoxObj"/> pre-populated with the default values taken from
        /// an existing box. Only the user-editable accession number fields (sequence, schedule,
        /// and box number) are copied so the resulting object can be reviewed and modified before
        /// it is submitted to the DRS API.
        /// </summary>
        /// <remarks>
        /// The new box is deliberately left without a <see cref="BoxId"/> (it is assigned by the
        /// API on creation) and is initialized as an open box with a zero page count and an
        /// opened date of the current day.
        /// </remarks>
        /// <param name="source">The box whose default values should be used as a starting point.</param>
        /// <returns>A new, unsaved <see cref="BoxObj"/> populated with default values.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="source"/> is <c>null</c>.
        /// </exception>
        public static BoxObj CreateFromDefaults(BoxObj source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return new BoxObj
            {
                SequenceNumber = source.SequenceNumber,
                ScheduleNumber = source.ScheduleNumber,
                BoxNumber = source.BoxNumber + 1, 
                OpenedDate = DateTimeOffset.Now,
                PageCount = 0
            };
        }

        /// <summary>
        /// Adds a newly created box to the in-memory <see cref="Boxes"/> collection so the
        /// application reflects the new record without requiring a full refresh from the API.
        /// </summary>
        /// <param name="newBox">The box to add to the list.</param>
        public static void AddBoxToList(BoxObj newBox)
        {
            if (newBox == null)
                return;

            if (_boxes == null)
                _boxes = new List<BoxObj>();

            _boxes.Add(newBox);
        }

    }

}
