using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace IU.Grupos.Meus
{
    public partial class EnviarMsg : PhoneApplicationPage
    {
        public EnviarMsg()
        {
            InitializeComponent();
            buttonDescrcao.Content = (App.Current as App).DescricaoGrupoSelecionado.ToString();
        }

        private void buttonCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void buttonEnviar_Click(object sender, RoutedEventArgs e)
        {
            Modelo.Mensagem msg = new Modelo.Mensagem
            {
                Remetente = "(" + (App.Current as App).DescricaoGrupoSelecionado + ") " + (App.Current as App).NomeUsuarioLogado,
                Conteudo = textBoxMsg.Text
            };

            Modelo.Grupo.Enviar((App.Current as App).IdGrupoSelecionado, msg);
        }

        private void textBlockDescricao_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Grupos/Meus/Listar.xaml", UriKind.Relative));
        }

        private void buttonAdicionar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Grupos/Meus/Adicionar.xaml", UriKind.Relative));
        }

        private void buttonAlterar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Grupos/Meus/EditarDados.xaml", UriKind.Relative));
        }
    }
}