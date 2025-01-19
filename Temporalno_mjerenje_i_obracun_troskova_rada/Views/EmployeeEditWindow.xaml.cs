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
using Temporalno_mjerenje_i_obracun_troskova_rada.Data;
using Temporalno_mjerenje_i_obracun_troskova_rada.DTOs;
using Temporalno_mjerenje_i_obracun_troskova_rada.Services;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Views
{
    /// <summary>
    /// Interaction logic for EmployeeEditWindow.xaml
    /// </summary>
    public partial class EmployeeEditWindow : Window
    {
        private ZaposlenikDTO _employeeToEdit;

        public EmployeeEditWindow(ZaposlenikDTO employee = null)
        {
            InitializeComponent();

            if (employee != null)
            {
                _employeeToEdit = employee;
                NameTextBox.Text = _employeeToEdit.Ime;
                LastNameTextBox.Text = _employeeToEdit.Prezime;
                OibTextBox.Text = _employeeToEdit.Oib;
                EmploymentDatePicker.SelectedDate = _employeeToEdit.DatumZaposlenja;
                ActiveCheckBox.IsChecked = _employeeToEdit.Aktivan;
            } else
            {
                _employeeToEdit = new ZaposlenikDTO();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _employeeToEdit.Ime = NameTextBox.Text;
            _employeeToEdit.Prezime = LastNameTextBox.Text;
            _employeeToEdit.Oib = OibTextBox.Text;
            _employeeToEdit.DatumZaposlenja = EmploymentDatePicker.SelectedDate ?? DateTime.Now;
            _employeeToEdit.Aktivan = ActiveCheckBox.IsChecked ?? false;

            if (_employeeToEdit.ZaposlenikId == 0)
            {
                var service = new ZaposlenikService(new ZaposlenikRepository(new DatabaseContext()));
                service.AddZaposlenik(_employeeToEdit);
            } else
            {
                var service = new ZaposlenikService(new ZaposlenikRepository(new DatabaseContext()));
                service.UpdateZaposlenik(_employeeToEdit);
            }

            this.DialogResult = true;
        }
    }
}
