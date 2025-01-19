using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Views
{
    public partial class MainViewModel : INotifyPropertyChanged
    {
        private object _currentView;

        public event PropertyChangedEventHandler PropertyChanged;


        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand ChangeViewCommand { get; }

        public MainViewModel()
        {
            CurrentView = new ManageEmployees();

            ChangeViewCommand = new RelayCommand(ChangeView);
        }

        private void ChangeView(object parameter)
        {
            switch (parameter?.ToString())
            {
                case "ManageEmployees":
                    CurrentView = new ManageEmployees();
                    break;
                case "ManagePositions":
                    CurrentView = new ManagePositions();
                    break;
                case "RecordWorkHours":
                    CurrentView = new RecordWorkHours();
                    break;
                case "PayrollCalculation":
                    CurrentView = new PayrollCalculation();
                    break;
                case "AuditLog":
                    CurrentView = new AuditLog();
                    break;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
