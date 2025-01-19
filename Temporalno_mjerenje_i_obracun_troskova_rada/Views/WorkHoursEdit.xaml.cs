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
    /// Interaction logic for WorkHoursEdit.xaml
    /// </summary>
    public partial class WorkHoursEdit : Window
    {
        private readonly RadniSatiDTO _workHoursToEdit;
        private readonly ZaposlenikService _zaposlenikService;
        private readonly List<ZaposlenikDTO> _employees;

        public WorkHoursEdit(RadniSatiDTO workHours = null)
        {
            InitializeComponent();

            var dbContext = new DatabaseContext();
            var zaposlenikRepository = new ZaposlenikRepository(dbContext);
            _zaposlenikService = new ZaposlenikService(zaposlenikRepository);

            _employees = _zaposlenikService.GetAllZaposlenici();
            EmployeeComboBox.ItemsSource = _employees;
            EmployeeComboBox.DisplayMemberPath = "Ime";
            EmployeeComboBox.SelectedValuePath = "ZaposlenikId";

            if (workHours != null)
            {
                _workHoursToEdit = workHours;

                EmployeeComboBox.SelectedValue = _workHoursToEdit.ZaposlenikId;
                DatePicker.SelectedDate = _workHoursToEdit.Datum;
                HoursTextBox.Text = _workHoursToEdit.Sati.ToString();
            } else
            {
                _workHoursToEdit = new RadniSatiDTO();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select an employee.");
                return;
            }

            if (!decimal.TryParse(HoursTextBox.Text, out decimal hours) || hours < 0)
            {
                MessageBox.Show("Please enter a valid number of hours (>= 0).");
                return;
            }

            _workHoursToEdit.ZaposlenikId = (int)EmployeeComboBox.SelectedValue;
            _workHoursToEdit.Datum = DatePicker.SelectedDate ?? DateTime.Now;
            _workHoursToEdit.Sati = hours;

            try
            {
                var dbContext = new DatabaseContext();
                var repository = new RadniSatiRepository(dbContext);
                var service = new RadniSatiService(repository);

                if (_workHoursToEdit.RadniSatiId == 0)
                {
                    service.AddRadniSati(_workHoursToEdit);
                } else
                {
                    service.UpdateRadniSati(_workHoursToEdit);
                }

                MessageBox.Show("Work hours saved successfully!");
                DialogResult = true;
                Close();
            } catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving work hours: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
