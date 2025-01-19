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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Temporalno_mjerenje_i_obracun_troskova_rada.Data;
using Temporalno_mjerenje_i_obracun_troskova_rada.DTOs;
using Temporalno_mjerenje_i_obracun_troskova_rada.Services;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Views
{
    /// <summary>
    /// Interaction logic for ManageEmployees.xaml
    /// </summary>
    public partial class ManageEmployees : UserControl
    {
        private readonly ZaposlenikService _zaposlenikService;
        private List<ZaposlenikDTO> _employees;
        private ZaposlenikDTO _selectedEmployee;

        public ManageEmployees()
        {
            InitializeComponent();

            var dbContext = new DatabaseContext();
            var repository = new ZaposlenikRepository(dbContext);
            _zaposlenikService = new ZaposlenikService(repository);
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            _employees = _zaposlenikService.GetAllZaposlenici();
            EmployeesDataGrid.ItemsSource = _employees;
        }

        private void EmployeesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedEmployee = EmployeesDataGrid.SelectedItem as ZaposlenikDTO;
        }

        private void EditEmployee(object sender, RoutedEventArgs e)
        {
            if (_selectedEmployee != null)
            {
                var editWindow = new EmployeeEditWindow(_selectedEmployee);
                bool? result = editWindow.ShowDialog();

                if (result == true)
                {
                    LoadEmployees();
                }
            } else
            {
                MessageBox.Show("Please select an employee to edit.");
            }
        }

        private void DeactivateEmployee(object sender, RoutedEventArgs e)
        {
            if (_selectedEmployee != null)
            {
                _selectedEmployee.Aktivan = !_selectedEmployee.Aktivan;

                _zaposlenikService.UpdateZaposlenik(_selectedEmployee);

                LoadEmployees();
            } else
            {
                MessageBox.Show("Please select an employee to deactivate.");
            }
        }
        private void AddEmployee(object sender, RoutedEventArgs e)
        {

            var editWindow = new EmployeeEditWindow(_selectedEmployee);
            bool? result = editWindow.ShowDialog();

            if (result == true)
            {
                LoadEmployees();
            }
           
        }
    }
}
