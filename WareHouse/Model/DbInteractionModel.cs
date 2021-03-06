﻿using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.ComponentModel;

using WareHouse.Data;

namespace WareHouse.Model
{
    public class DbInteractionModel : INotifyPropertyChanged
    {
        private WarehouseContext _warehouseContext = new WarehouseContext();

        #region Properties
        public ObservableCollection<Accept> Accept
        {
            get
            {
                _warehouseContext.Accepts.Load();
                _warehouseContext.Products.Load();
                return _warehouseContext.Accepts.Local;
            }
        }
        public ObservableCollection<InStorage> InStorages
        {
            get
            {
                _warehouseContext.InStorages.Load();
                _warehouseContext.Products.Load();
                return _warehouseContext.InStorages.Local;
            }
        }
        public ObservableCollection<Sale> Sales
        {
            get
            {
                _warehouseContext.Sales.Load();
                _warehouseContext.Products.Load();
                return _warehouseContext.Sales.Local;
            }
        }
        #endregion

        #region Public Methods
        public ObservableCollection<object> AllProducts(DateTime FromDate, DateTime ToDate)
        {
            _warehouseContext.InStorages.Load();
            _warehouseContext.Sales.Load();
            _warehouseContext.Accepts.Load();
            _warehouseContext.Products.Load();
            var result = new ObservableCollection<object>();

            foreach (var product in _warehouseContext.Accepts.Local
                .Where(a => a.AcceptDate.CompareTo(FromDate) >= 0 &&
                            a.AcceptDate.CompareTo(ToDate) <= 0))
            {
                result.Add(product);
            }

            foreach (var product in _warehouseContext.InStorages.Local
                .Where(a => a.InStorageDate.CompareTo(FromDate) >= 0 &&
                            a.InStorageDate.CompareTo(ToDate) <= 0))
            {
                result.Add(product);
            }

            foreach (var product in _warehouseContext.Sales.Local
                .Where(a => a.SaleDate.CompareTo(FromDate) >= 0 &&
                            a.SaleDate.CompareTo(ToDate) <= 0))
            {
                result.Add(product);
            }

            return result;
        }
        public bool TryAddToDb(Product product)
        {
            try
            {
                if(_warehouseContext.Products
                    .Where(a=> a.CodeId == product.CodeId).Any())
                {
                    throw new Exception("Уже существует такой товар");
                }

                _warehouseContext.Products.Add(product);
                _warehouseContext.Accepts.Add(new Accept()
                {
                    Product = product,
                    AcceptDate = DateTime.Now,
                    Id = _warehouseContext.Accepts.Count()
                });
                _warehouseContext.InStorages.Add(new InStorage()
                {
                    Product = product,
                    AcceptDate = DateTime.Now,
                    InStorageDate = DateTime.Now.AddDays(1),
                    Id = _warehouseContext.InStorages.Count()
                });
                _warehouseContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SellProduct(InStorage product)
        {
            try
            {
                _warehouseContext.Sales.Add(new Sale(product, _warehouseContext.Sales.Local.Count));
                _warehouseContext.InStorages.Remove(product);
                _warehouseContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
