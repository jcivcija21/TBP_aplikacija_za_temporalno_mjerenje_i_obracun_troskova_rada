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
    /// Interaction logic for PositionEditWindow.xaml
    /// </summary>
    public partial class PositionEditWindow : Window
    {
        private PozicijaDTO _positionToEdit;

        public PositionEditWindow(PozicijaDTO position = null)
        {
            InitializeComponent();

            if (position != null)
            {
                _positionToEdit = position;
                PositionNameTextBox.Text = _positionToEdit.Naziv;
                PositionDescriptionTextBox.Text = _positionToEdit.Opis;
                HourlyRateTextBox.Text = _positionToEdit.OsnovnaSatnica.ToString();
            } else
            {
                _positionToEdit = new PozicijaDTO();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _positionToEdit.Naziv = PositionNameTextBox.Text;
            _positionToEdit.Opis = PositionDescriptionTextBox.Text;
            _positionToEdit.OsnovnaSatnica = decimal.TryParse(HourlyRateTextBox.Text, out decimal hourlyRate) ? hourlyRate : 0;

            var service = new PozicijaService(new PozicijaRepository(new DatabaseContext()));

            if (_positionToEdit.PozicijaId == 0)
            {
                service.AddPozicije(_positionToEdit);
            } else
            {
                service.UpdatePozicije(_positionToEdit);
            }

            this.DialogResult = true;
        }
    }
}
