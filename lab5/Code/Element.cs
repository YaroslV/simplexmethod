using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5.Code
{
    class Element : INotifyPropertyChanged
    {
        private double _p1;
        public string RowName { get; set; }

        public double P1
        {
            get { return _p1; }
            set
            {
                _p1 = value;
                var pc = PropertyChanged;
                if (pc != null)
                    pc(this, new PropertyChangedEventArgs("P1"));
            }
        }
        public double P2 { get; set; }
        public double P3 { get; set; }
        public double Norm { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
