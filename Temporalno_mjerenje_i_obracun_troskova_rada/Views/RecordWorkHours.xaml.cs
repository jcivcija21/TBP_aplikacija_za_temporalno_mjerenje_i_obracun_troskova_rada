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
    /// Interaction logic for RecordWorkHours.xaml
    /// </summary>
    public partial class RecordWorkHours : UserControl
    {
        private readonly RadniSatiService _radniSatiService;
        private List<RadniSatiDTO> _workHours;
        private RadniSatiDTO _selectedWorkHour;

        public RecordWorkHours()
        {
            InitializeComponent();

            var dbContext = new DatabaseContext();
            var repository = new RadniSatiRepository(dbContext);
            _radniSatiService = new RadniSatiService(repository);
            LoadWorkHours();
        }

        private void LoadWorkHours()
        {
            _workHours = _radniSatiService.GetAllRadniSati();
            WorkHoursDataGrid.ItemsSource = _workHours;
        }

        private void WorkHoursDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedWorkHour = WorkHoursDataGrid.SelectedItem as RadniSatiDTO;
        }

        private void AddWorkHours_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new WorkHoursEdit();
            bool? result = editWindow.ShowDialog();

            if (result == true)
                LoadWorkHours();
        }

        private void EditWorkHours_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedWorkHour != null)
            {
                var editWindow = new WorkHoursEdit(_selectedWorkHour);
                bool? result = editWindow.ShowDialog();

                if (result == true)
                    LoadWorkHours();
            } else
            {
                MessageBox.Show("Please select work hours to edit.");
            }
        }

        private void DeleteWorkHours_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedWorkHour != null)
            {
                _radniSatiService.DeleteRadniSati(_selectedWorkHour.RadniSatiId);
                LoadWorkHours();
            } else
            {
                MessageBox.Show("Please select work hours to delete.");
            }
        }
    }
}
