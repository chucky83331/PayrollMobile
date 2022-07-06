using System;
using System.Collections.Generic;
using PayrollMobile.Service;

namespace PayrollMobile.ViewModel
{
    public class ShiftViewModel
    {
       public List<Shift> Shifts { get; set; } // Note s end of Shift
        public ShiftViewModel()
        {
            Shifts = new ShiftService().GetShiftsList();
        }
    }
}
