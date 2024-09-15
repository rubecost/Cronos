using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Cronos.Models;
using Cronos.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cronos.ViewModels
{
	public class CriarContaViewModel : ObservableObject
	{
        #region VARIAVEIS
        private PlanosModel _selectedPlan;
        private PlanosModel _selectedGlobalPlan;
        private bool _ExpandAccordPlanos = false;
        private bool _SemInternet = false;
        #endregion

        #region CONSTRUTOR
        public CriarContaViewModel()
        {
            _selectedPlan = new PlanosModel();
            _selectedGlobalPlan = _selectedPlan;
        }
        #endregion

        #region OBJETOS
        public PlanosModel SelectedPlan
        {
            get => _selectedPlan;
            set => SetProperty(ref _selectedPlan, value);
        }
        public PlanosModel SelectedGlobalPlan
        {
            get => _selectedGlobalPlan;
            set => SetProperty(ref _selectedGlobalPlan, value);
        }
        public bool ExpandAccordPlanos
        {
            get => _ExpandAccordPlanos;
            set => SetProperty(ref _ExpandAccordPlanos, value);
        }
        public ObservableCollection<PlanosModel> Plans { get; } = new ObservableCollection<PlanosModel>();
        public bool SemInternet
        {
            get => _SemInternet;
            set => SetProperty(ref _SemInternet, value);
        }
        #endregion

        #region PROCESSOS

        private void SelectPlan(PlanosModel plan)
        {
            if (plan != null)
            {
                Plans.Remove(plan);
                Plans.Insert(0, plan);
                SelectedPlan = plan;
                SelectedGlobalPlan = plan; // Atualiza a variável global com o plano selecionado
                ExpandAccordPlanos = false;
            }
        }

        //public async Task LoadPlansAsync()
        //{

        //    using var httpClient = new HttpClient();
        //    var response = await httpClient.GetStringAsync("https://localhost:7037/api/Plano/nomes");
        //    var planNames = System.Text.Json.JsonSerializer.Deserialize<List<string>>(response);

        //    Plans.Clear();

        //    if (planNames != null)
        //    {
        //        foreach (var nome in planNames)
        //        {
        //            Plans.Add(new PlanosModel { Nome = nome });
        //        }
        //        if (Plans.Count > 0)
        //        {
        //            SelectedPlan = Plans[0];
        //            SelectedGlobalPlan = Plans[0]; // Define o primeiro plano como o global inicialmente
        //        }
        //    }
        //}
        private void AvisoSemInternet()
        {
            if (VerificaInternetService.VerificaInternet())
            {
                SemInternet = !SemInternet;
            }
        }
        #endregion

        #region COMANDOS
        public ICommand Btn_AvisoSemInternet => new Command(AvisoSemInternet);

        public IRelayCommand<PlanosModel> SelectPlanCommand => new RelayCommand<PlanosModel>(SelectPlan);
        //public IAsyncRelayCommand LoadPlansCommand => new AsyncRelayCommand(LoadPlansAsync);
        #endregion
    }
}
