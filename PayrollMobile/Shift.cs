using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SQLite;

namespace  PayrollMobile
{
    public class Shift
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime WorkDate { get; set; }
        public string ShiftType { get; set; }
        public string ShiftTime { get; set; }
        public decimal Rate { get; set; }
        public decimal HrsWork { get; set; }
        public decimal Diff { get; set; }
        public decimal Total => ((Rate + Diff) * HrsWork);
        public decimal GrandTotal => (Total + Total);
    }
}