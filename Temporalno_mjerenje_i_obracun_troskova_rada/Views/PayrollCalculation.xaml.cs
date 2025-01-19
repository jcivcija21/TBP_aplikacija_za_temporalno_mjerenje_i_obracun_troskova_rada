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
    /// Interaction logic for PayrollCalculation.xaml
    /// </summary>
    public partial class PayrollCalculation : UserControl
    {
        private readonly ObracunPlaceService _obracunService;
        private readonly ZaposlenikService _zaposlenikService;
        private readonly DoprinosiService _doprinosiService;
        private readonly PoreziService _poreziService;
        private readonly RadniSatiService _radniSatiService; 
        private List<PorezDTO> _porez;
        private List<DoprinosiDTO> _doprinos;

        public PayrollCalculation()
        {
            InitializeComponent();
            var dbContext = new DatabaseContext();
            var zaposlenikRepository = new ZaposlenikRepository(dbContext);
            var obracunPlaceRepository = new ObracunPlaceRepository(dbContext);
            var doprinosiRepository = new DoprinosRepository(dbContext);
            var poreziRepository = new PoreziRepository(dbContext);
            var radniSatiRepository = new RadniSatiRepository(dbContext);

            _zaposlenikService = new ZaposlenikService(zaposlenikRepository);
            _obracunService = new ObracunPlaceService(obracunPlaceRepository);
            _doprinosiService = new DoprinosiService(doprinosiRepository);
            _poreziService = new PoreziService(poreziRepository);
            _radniSatiService = new RadniSatiService(radniSatiRepository);

            List<ZaposlenikDTO> zaposlenici = _zaposlenikService.GetAllZaposlenici();
            List<ZaposlenikDTO> filtriraniZaposlenici = zaposlenici.Where(z => z.Aktivan == true).ToList();


            EmployeeComboBox.ItemsSource = filtriraniZaposlenici;

            LoadTaxesAndContributions();
        }


        private void LoadTaxesAndContributions()
        {
            _porez = (List<PorezDTO>)_poreziService.GetAllPorezi();
            _doprinos = (List<DoprinosiDTO>)_doprinosiService.GetAllDoprinosi();

            foreach (var tax in _porez)
            {
                var taxCheckBox = new CheckBox
                {
                    Content = tax.Naziv,
                    Tag = tax.PorezId
                };
                TaxesCheckBoxPanel.Children.Add(taxCheckBox);
            }

            foreach (var contribution in _doprinos)
            {
                var contributionCheckBox = new CheckBox
                {
                    Content = contribution.Naziv,
                    Tag = contribution.DoprinosId
                };
                ContributionsCheckBoxPanel.Children.Add(contributionCheckBox);
            }
        }

        private List<int> GetSelectedContributions()
        {
            var selectedContributions = new List<int>();

            foreach (CheckBox checkBox in ContributionsCheckBoxPanel.Children)
            {
                if (checkBox.IsChecked == true)
                {
                    selectedContributions.Add((int)checkBox.Tag);
                }
            }

            return selectedContributions;
        }

        private List<int> GetSelectedTaxes()
        {
            var selectedTaxes = new List<int>();

            foreach (CheckBox checkBox in TaxesCheckBoxPanel.Children)
            {
                if (checkBox.IsChecked == true)
                {
                    selectedTaxes.Add((int)checkBox.Tag);
                }
            }

            return selectedTaxes;
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select an employee.");
                return;
            }

            var selectedEmployee = (ZaposlenikDTO)EmployeeComboBox.SelectedItem;

            var selectedContributions = GetSelectedContributions();
            var selectedTaxes = GetSelectedTaxes();

            if (selectedContributions.Count == 0)
            {
                MessageBox.Show("Please select at least one contribution.");
                return;
            }

            if (selectedTaxes.Count == 0)
            {
                MessageBox.Show("Please select at least one tax.");
                return;
            }

            decimal hourlyRate = 10;
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            decimal hoursWorked = _radniSatiService.GetTotalWorkHoursForEmployee(selectedEmployee.ZaposlenikId, currentMonth, currentYear);

            decimal grossSalary = hourlyRate * hoursWorked;

            decimal totalContributions = 0;
            decimal totalTaxes = 0;

            foreach (var contributionId in selectedContributions)
            {
                DoprinosiDTO contribution = _doprinos.FirstOrDefault(a => a.DoprinosId == contributionId);
                totalContributions += grossSalary * (contribution.Stopa / 100); 
            }

            foreach (var taxId in selectedTaxes)
            {
                PorezDTO tax = _porez.FirstOrDefault(a => a.PorezId == taxId);
                totalTaxes += grossSalary * (tax.Stopa / 100);
            }

            decimal netSalary = grossSalary - totalContributions - totalTaxes;

            GrossSalaryTextBlock.Text = grossSalary.ToString("F2");
            ContributionsTextBlock.Text = totalContributions.ToString("F2");
            TaxesTextBlock.Text = totalTaxes.ToString("F2");
            NetSalaryTextBlock.Text = netSalary.ToString("F2");


        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select an employee.");
                return;
            }

            if (!decimal.TryParse(GrossSalaryTextBlock.Text, out decimal grossSalary) ||
                !decimal.TryParse(ContributionsTextBlock.Text, out decimal contributions) ||
                !decimal.TryParse(TaxesTextBlock.Text, out decimal taxes) ||
                !decimal.TryParse(NetSalaryTextBlock.Text, out decimal netSalary))
            {
                MessageBox.Show("Please calculate payroll before saving.");
                return;
            }

            var payrollRecord = new ObracunPlaceDTO
            {
                ZaposlenikId = (int)EmployeeComboBox.SelectedValue,
                Bruto = grossSalary,
                Doprinosi = contributions,
                Porez = taxes,
                Neto = netSalary,
                DatumObracuna = DateTime.Now
            };

            _obracunService.AddObracun(payrollRecord);
            MessageBox.Show("Payroll record saved successfully.");
        }
    }
}
