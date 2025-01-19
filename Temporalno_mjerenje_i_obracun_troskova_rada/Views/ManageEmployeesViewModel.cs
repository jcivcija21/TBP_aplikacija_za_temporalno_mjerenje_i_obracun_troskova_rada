using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Temporalno_mjerenje_i_obracun_troskova_rada.Data;
using Temporalno_mjerenje_i_obracun_troskova_rada.DTOs;
using Temporalno_mjerenje_i_obracun_troskova_rada.Services;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Views
{
    public class ManageEmployeesViewModel
    {
        private readonly ZaposlenikService _zaposlenikService;
        public ObservableCollection<ZaposlenikDTO> Employees { get; set; }
        public ZaposlenikDTO SelectedEmployee;
        public string SearchQuery { get; set; }
        public string SelectedFilter { get; set; }

        public ICommand AddEmployeeCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeactivateCommand { get; set; }

        public ManageEmployeesViewModel()
        {
            var dbContext = new DatabaseContext();
            var repository = new ZaposlenikRepository(dbContext);
            _zaposlenikService = new ZaposlenikService(repository);
            LoadEmployees();
            AddEmployeeCommand = new RelayCommand(AddEmployee);
            EditCommand = new RelayCommand(EditEmployee);
            DeactivateCommand = new RelayCommand(DeactivateEmployee);
        }
        private void LoadEmployees()
        {
            var zaposlenici = _zaposlenikService.GetAllZaposlenici();
            Employees = new ObservableCollection<ZaposlenikDTO>(zaposlenici);
        }

        private void AddEmployee(object parameter)
        {
        
        }
        private void EditEmployee(object parameter) 
        { 
            
        }
        private void DeactivateEmployee(object parameter) 
        {
            if (SelectedEmployee != null)
            {
                if(SelectedEmployee.Aktivan == true)
                {
                    SelectedEmployee.Aktivan = false;
                } else
                {
                    SelectedEmployee.Aktivan = true;
                }

                _zaposlenikService.UpdateZaposlenik(SelectedEmployee);

                LoadEmployees();
            } else
            {
                MessageBox.Show("Please select an employee to deactivate.");
            }

        }
    }
}
