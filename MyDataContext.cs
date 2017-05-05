using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfApplication1
{
    public class MyDataContext : INotifyPropertyChanged
    {
        private int _start;

        public MyDataContext(int start)
        {
            _start = start;
        }

        IEnumerable<MyObject> _Data;
        public IEnumerable<MyObject> Data
        { 
            get
            {
                if (_Data == null)
                {
                    _Data = new ObservableCollection<MyObject>(from i in Enumerable.Range(_start, 50) select new MyObject() { ID = i , Name = String.Format("Name{0}", i) });
                }
                return _Data;
            }
        }

        ObservableCollection<MyObject> _SelectedItems;
        public ObservableCollection<MyObject> SelectedItems
        {
            get
            {
                if (_SelectedItems == null)
                {
                    _SelectedItems = new ObservableCollection<MyObject>();
                }
                return _SelectedItems;
            }
        }

        MyObject _SelectedItem;
        public MyObject SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;

                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        #region INotifyPropertyChanged Members

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
