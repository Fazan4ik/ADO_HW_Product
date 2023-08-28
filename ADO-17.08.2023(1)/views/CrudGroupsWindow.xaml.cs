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

namespace ADO_17._08._2023_1_.views
{
    /// <summary>
    /// Interaction logic for CrudGroupsWindow.xaml
    /// </summary>
    public partial class CrudGroupsWindow : Window
    {
        public ProductGroup? ProductGroup { get; set; }
        private string name;
        private string description;
        private string picture;

        public CrudGroupsWindow(ProductGroup productGroup)
        {
            InitializeComponent();

            ProductGroup = productGroup;
            name = ProductGroup.Name; 
            description = ProductGroup.Description;
            picture = ProductGroup.Picture;

            DataContext = ProductGroup;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ProductGroup = null;    
            Close();
        }

        private bool DataValidation()
        {
            List<string> allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".svg", ".webp" };
            if (ProductGroup is not null)
            {
                return !String.IsNullOrEmpty(ProductGroup.Name) &&
                       !String.IsNullOrEmpty(ProductGroup.Description) &&
                       !String.IsNullOrEmpty(ProductGroup.Picture) &&
                       (allowedExtensions.Any(extension => ProductGroup.Picture.EndsWith(extension)));
            }
            return false;
        }

        private bool CheckChangedData()
        {
            if (ProductGroup is not null)
                return ProductGroup.Name != name || ProductGroup.Description != description || ProductGroup.Picture != picture;
            return false;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                if (DataValidation() && CheckChangedData())
                {
                    if (!SaveButton.IsEnabled) SaveButton.IsEnabled = true;
                }
                else
                {
                    if (SaveButton.IsEnabled) SaveButton.IsEnabled = false;
                }
            }
        }
    }
}
