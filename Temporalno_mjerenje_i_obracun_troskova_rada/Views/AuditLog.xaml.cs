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
    /// Interaction logic for AuditLog.xaml
    /// </summary>
    public partial class AuditLog : UserControl
    {
        private readonly ZaposlenikService _zaposlenikService;
        private readonly PovijestObracunaService _auditLogService;
        private List<ZaposlenikDTO> _employees;
        private ZaposlenikDTO _selectedEmployee;
        private List<PovijestObracunaDTO> _auditLogs;

        public AuditLog()
        {
            InitializeComponent();

            var dbContext = new DatabaseContext();
            var zaposlenikRepository = new ZaposlenikRepository(dbContext);
            var auditLogRepository = new PovijestObracunaRepository(dbContext);

            _zaposlenikService = new ZaposlenikService(zaposlenikRepository);
            _auditLogService = new PovijestObracunaService(auditLogRepository);

            LoadEmployees();
        }

        private void LoadEmployees()
        {
            _employees = _zaposlenikService.GetAllZaposlenici();
            EmployeeDataGrid.ItemsSource = _employees;
        }

        private void EmployeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedEmployee = EmployeeDataGrid.SelectedItem as ZaposlenikDTO;

            if (_selectedEmployee != null)
            {
                LoadAuditLogs(_selectedEmployee);
            }
        }
        private void LoadAuditLogs(ZaposlenikDTO selectedEmployee)
        {
            _auditLogs = _auditLogService.GetLogsForEmployee(selectedEmployee.ZaposlenikId);
            LogsDataGrid.ItemsSource = _auditLogs;
        }
}
}
