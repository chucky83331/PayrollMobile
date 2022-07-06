﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using PayrollMobile;

namespace PayrollMobile
{
    public class ShiftInfo : INotifyPropertyChanged
    {
        private string workDate;
        private string shiftType;
        private string shiftTime;
        private string rate;
        private string diff;
        private string hrsWork;
        private string total;
        private string grandtotal;

        public string WorkDate
        {
            get { return workDate; }
            set
            {
                this.workDate = value;
                RaisePropertyChanged("WorkDate");
            }
        }
        public string ShiftType
        {
            get { return shiftType; }
            set
            {
                this.shiftType = value;
                RaisePropertyChanged("ShiftType");
            }
        }
        public string ShiftTime
        {
            get { return shiftTime; }
            set
            {
                this.shiftTime = value;
                RaisePropertyChanged("ShiftTime");
            }
        }
        public string Rate
        {
            get { return rate; }
            set
            {
                this.rate = value;
                RaisePropertyChanged("Rate");
            }
        }
        public string Diff
        {
            get { return diff; }
            set
            {
                this.diff = value;
                RaisePropertyChanged("Diff");
            }
        }
        public string HrsWork
        {
            get { return hrsWork; }
            set
            {
                this.hrsWork = value;
                RaisePropertyChanged("HrsWork");
            }
        }
        public string Total
        {
            get { return total; }
            set
            {
                this.total = value;
                RaisePropertyChanged("Total");
            }
        }
        public string GrandTotal
        {
            get { return grandtotal; }
            set
            {
                this.grandtotal = value;
                RaisePropertyChanged("GrandTotal");
            }
        }
        public ShiftInfo(string workDate, string shiftType, string shiftTime, string rate, string diff, string hrsWork, string total, string grandtotal)
        {
            this.WorkDate = workDate;
            this.ShiftType = shiftType;
            this.ShiftTime = shiftTime;
            this.Rate = rate;
            this.Diff = diff;
            this.HrsWork = hrsWork;
            this.Total = total;
            this.grandtotal = grandtotal;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
