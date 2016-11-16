using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSManDataAccess
{
    class ClearanceLevel
    {
        public int ClearanceLevelId { get; set; }

        public int PriorityNumber { get; set; }

        public int Name { get; set; }

        public int Description { get; set; }

        public int DepartmentId { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsViewable { get; set; }

        public bool IsEditable { get; set; }

        public int FunctionsId { get; set; }

        public int UserId { get; set; }
    }
}
