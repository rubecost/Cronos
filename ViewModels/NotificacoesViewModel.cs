using CommunityToolkit.Mvvm.ComponentModel;
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
    public class NotificacoesViewModel : ObservableObject
    {
        #region VARIAVEIS 
        private ObservableCollection<NotificacoesGeraisModel>? _notificacoes;
        private bool _SemInternet = false;
        #endregion
        #region CONSTRUTOR
        public NotificacoesViewModel()
        {
            _notificacoes = new ObservableCollection<NotificacoesGeraisModel>();
            InicializadorDadosFicticios();
        }
        #endregion
        #region OBJETOS
        public ObservableCollection<NotificacoesGeraisModel> Notificacoes
        {
            get => _notificacoes ??= new ObservableCollection<NotificacoesGeraisModel>();
            set => SetProperty(ref _notificacoes, value);
        }
        public bool SemInternet
        {
            get => _SemInternet;
            set => SetProperty(ref _SemInternet, value);
        }
        #endregion
        #region PROCESSOS
        public void InicializadorDadosFicticios()
        {
            Notificacoes = new ObservableCollection<NotificacoesGeraisModel>
            {
                new NotificacoesGeraisModel
                {
                    Data_Criacao = DateTime.Now,
                    Tipo = "Promoções",
                    Mensagem = "Essa é um exemplo de um texto que pode ser enviado como uma mensagem de promoção de algum plano ou upgrade para um plano melhor, no sistema de inspeções nr-13 idealle onde se pode gerenciar o processo de inspeções de sua industria online remotamente de qualquer lugar de seu desktop conectado a internet."
                },
                new NotificacoesGeraisModel
                {
                    Data_Criacao = DateTime.Now,
                    Tipo = "Promoções",
                    Mensagem = "Essa é um exemplo de um texto que pode ser enviado como uma mensagem de promoção de algum plano ou upgrade para um plano melhor, no sistema de inspeções nr-13 idealle onde se pode gerenciar o processo de inspeções de sua industria online remotamente de qualquer lugar de seu desktop conectado a internet."
                },
            };
        }
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
        #endregion
    }
}
