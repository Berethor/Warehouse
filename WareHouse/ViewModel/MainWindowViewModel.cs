using System;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

using WareHouse.Data;
using WareHouse.View;
using WareHouse.Model;
using WareHouse.Helpers;

namespace WareHouse.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        readonly DbInteractionModel model = new DbInteractionModel();

        #region Properties
        public ObservableCollection<Accept> Accepts => model.Accept;
        public ObservableCollection<InStorage> InStorages => model.InStorages;
        public ObservableCollection<Sale> Sales => model.Sales;

        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                    (_addCommand = new RelayCommand(_ =>
                    {
                        var addProduct = new AddProductView();

                        if (addProduct.ShowDialog() == true)
                        {
                            model.TryAddToDb(addProduct.NewProduct);
                        }
                    }));
            }
        }
        private RelayCommand _reportWindowCommand;
        public RelayCommand ReportWindowCommand
        {
            get
            {
                return _reportWindowCommand ??
                    (_reportWindowCommand = new RelayCommand(_ =>
                    {
                        ReportView reportView = new ReportView();
                        reportView.Show();
                    }));
            }
        }
        private RelayCommand _sellCommand;
        public RelayCommand SellCommand
        {
            get
            {
                return _sellCommand ??
                    (_sellCommand = new RelayCommand(a =>
                    {
                        model.SellProduct((InStorage)a);
                    }));
            }
        }
        #endregion

        #region Public Methods
        public MainWindowViewModel()
        {
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
