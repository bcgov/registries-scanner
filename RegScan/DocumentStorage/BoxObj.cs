using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegScan
{
    public class BoxObj
    {
        private static List<BoxObj> _boxes;

        private int _boxID;
        private DateTime _openedDate;
        private DateTime _closedDate;
        private int _pageCount;
        private AccessionNumberObj _accessionNumber;
        

        public static List<BoxObj> Boxes { get { return _boxes; } }
        public int BoxId { get { return _boxID; } set { _boxID = value; } }
        public int BoxNumber { get { return _accessionNumber.BoxNumber; } }
        public DateTime OpendedDate { get { return _openedDate; } set { _openedDate = value; } }
        public DateTime ClosedDate { get { return _closedDate; } set { _closedDate = value; } }
        public int PageCount { get { return _pageCount; } set { _pageCount = value; } }
        public AccessionNumberObj AccessionNumber { get { return _accessionNumber; } }

        public void LoadBoxes()
        {
            _boxes = new List<BoxObj>();

        }
    }

}
