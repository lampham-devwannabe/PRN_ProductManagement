using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFApp.Model;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MyStoreContext _context;
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        public MainWindow()
        {
            InitializeComponent();
            _context = new MyStoreContext();
            DataContext = this;
            LoadCategoryList();
            LoadProductList();
        }
        public void LoadCategoryList()
        {
            try
            {
                var catList = _context.Categories.ToList();
                cboCategory.ItemsSource = catList;
                cboCategory.DisplayMemberPath = "CategoryName";
                cboCategory.SelectedValuePath = "CategoryId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of categories");
            }
        }
        public void LoadProductList()
        {
            try
            {
                Products.Clear();
                var productList = _context.Products.ToList();
                foreach (var item in productList)
                {
                    Products.Add(item);
                }
                dgData.ItemsSource = productList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of products");
            }
            finally
            {
                resetInput();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategoryList();
            LoadProductList();
        }
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = new Product();
                product.ProductName = txtProductName.Text;
                product.UnitPrice = decimal.Parse(txtPrice.Text);
                product.UnitsInStock = short.Parse(txtUnitsInStock.Text);
                if (int.TryParse(cboCategory.SelectedValue.ToString(), out int categoryId))
                {
                    product.CategoryId = categoryId;
                }
                _context.Products.Add(product);
                _context.SaveChanges();
                LoadProductList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem is Product selectedProduct)
            {
                txtProductID.Text = selectedProduct.ProductId.ToString();
                txtProductName.Text = selectedProduct.ProductName;
                txtPrice.Text = selectedProduct.UnitPrice.ToString();
                txtUnitsInStock.Text = selectedProduct.UnitsInStock.ToString();
                cboCategory.SelectedValue = selectedProduct.CategoryId;
            }
            else
            {
                resetInput();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProductID.Text.Length > 0)
                {
                    int productId = int.Parse(txtProductID.Text);
                    Product product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product != null)
                    {
                        product.ProductName = txtProductName.Text;
                        product.UnitPrice = Decimal.Parse(txtPrice.Text);
                        product.UnitsInStock = short.Parse(txtUnitsInStock.Text);
                        if (int.TryParse(cboCategory.SelectedValue.ToString(), out int categoryId))
                        {
                            product.CategoryId = categoryId;
                        }
                        _context.SaveChanges();
                        MessageBox.Show("Product updated successfully!");
                        LoadProductList(); // Refresh product list after update
                        resetInput(); // Reset the input fields after the update
                    }
                    else
                    {
                        MessageBox.Show("Product not found!");
                    }
                }
                else
                {
                    MessageBox.Show("You must select a Product!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProductID.Text.Length > 0)
                {
                    int productId = int.Parse(txtProductID.Text);
                    Product product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product != null)
                    {
                        _context.Products.Remove(product);
                        _context.SaveChanges();
                        MessageBox.Show("Product deleted successfully!");
                        LoadProductList();
                    }
                    else
                    {
                        MessageBox.Show("Product not found!");
                    }
                }
                else
                {
                    MessageBox.Show("You must select a Product!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void resetInput()
        {
            txtProductID.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtUnitsInStock.Text = string.Empty;
            cboCategory.SelectedValue = 0;
        }
    }
}