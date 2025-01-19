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
    /// Interaction logic for ManagePositions.xaml
    /// </summary>
    public partial class ManagePositions : UserControl
    {
        private readonly PozicijaService _pozicijaService;
        private List<PozicijaDTO> _positions;
        private PozicijaDTO _selectedPosition;

        public ManagePositions()
        {
            InitializeComponent();

            var dbContext = new DatabaseContext();
            var repository = new PozicijaRepository(dbContext);
            _pozicijaService = new PozicijaService(repository);
            LoadPositions();
        }

        private void LoadPositions()
        {
            _positions = _pozicijaService.GetAllPozicije();
            PositionsDataGrid.ItemsSource = _positions;
        }

        private void PositionsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedPosition = PositionsDataGrid.SelectedItem as PozicijaDTO;
        }

        private void EditPosition(object sender, RoutedEventArgs e)
        {
            if (_selectedPosition != null)
            {
                var editWindow = new PositionEditWindow(_selectedPosition);
                bool? result = editWindow.ShowDialog();

                if (result == true)
                {
                    LoadPositions();
                }
            } else
            {
                MessageBox.Show("Please select a position to edit.");
            }
        }

        private void DeletePosition(object sender, RoutedEventArgs e)
        {
            if (_selectedPosition != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete this position?", "Confirm Deletion", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    _pozicijaService.DeletePozicija(_selectedPosition.PozicijaId);
                    LoadPositions();
                }
            } else
            {
                MessageBox.Show("Please select a position to delete.");
            }
        }

        private void AddPosition(object sender, RoutedEventArgs e)
        {
            var editWindow = new PositionEditWindow(null);
            bool? result = editWindow.ShowDialog();

            if (result == true)
            {
                LoadPositions();
            }
        }
    }
}
