using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WareHouse.Data;

namespace WareHouse.View
{
    public partial class AddProductView : Window
    {
        public AddProductView()
        {
            InitializeComponent();
        }
        public Product NewProduct
        {
            get 
            {
                return new Product()
                {
                    CodeId = int.Parse(this.CodeId.Text),
                    Name = this.Name.Text,
                    Price = int.Parse(this.Price.Text)
                };
            }
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsNumeric);
        }

        private void OnPasting(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsNumeric))
                e.CancelCommand();
        }
        private bool IsNumeric(char c)
        {
            return c >= '0' && c <= '9';
        }
    }
}
