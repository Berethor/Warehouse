using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WareHouse.Data;
using WareHouse.Model;
using WareHouse.Helpers.Converters;
using WareHouse.Helpers;

namespace WareHouse.ViewModel
{
    public class ReportViewModel : INotifyPropertyChanged
    {
        readonly DbInteractionModel model = new DbInteractionModel();
        private TypeValue _filter;
        public TypeValue Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                OnPropertyChanged("FilteredList");
            }
        }
        private DateTime _fromDate;
        public DateTime FromDate
        {
            get { return _fromDate; }
            set
            {
                _fromDate = value;
                OnPropertyChanged("FilteredList");
            }
        }
        private DateTime _toDate;
        public DateTime ToDate
        {
            get { return _toDate; }
            set
            {
                _toDate = value;
                OnPropertyChanged("FilteredList");
            }
        }
        public ObservableCollection<object> FilteredList
        {
            get
            {
                var list = new ObservableCollection<object>();
                switch(_filter)
                {
                    case (TypeValue.Accept):
                        var accept = model.Accept.Where(
                            a=>a.AcceptDate.CompareTo(FromDate) >= 0 &&
                            a.AcceptDate.CompareTo(ToDate) <= 0);
                        foreach (var product in accept)
                        {
                            list.Add(product);
                        }
                        break;
                    case (TypeValue.InStorage):
                        var inStorage = model.InStorages.Where(
                            a => a.InStorageDate.CompareTo(FromDate) >= 0 &&
                            a.InStorageDate.CompareTo(ToDate) <= 0);
                        foreach (var product in inStorage)
                        {
                            list.Add(product);
                        }
                        break;
                    case (TypeValue.Sales):
                        var sale = model.Sales.Where(
                            a => a.SaleDate.CompareTo(FromDate) >= 0 &&
                            a.SaleDate.CompareTo(ToDate) <= 0);
                        foreach (var product in sale)
                        {
                            list.Add(product);
                        }
                        break;
                    case (TypeValue.All):
                        list = model.AllProducts(FromDate,ToDate);
                        break;
                }
                return list;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
