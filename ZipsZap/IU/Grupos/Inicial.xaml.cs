using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace IU.Grupos
{
    public partial class Inicial : PhoneApplicationPage
    {
        List<Modelo.Grupo> grupos;
        public Inicial()
        {
            CarregarTabelas();
            InitializeComponent();
        }

        private async void CarregarTabelas()
        {
            grupos = await Modelo.Grupo.Listar((App.Current as App).IdUsuarioLogado);
            listBoxGrupos.ItemsSource = grupos;
        }

        private void buttonAtualizar_Click(object sender, RoutedEventArgs e)
        {
            CarregarTabelas();
        }

        private void buttonContatos_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Contatos/Inicial.xaml", UriKind.Relative));
        }

        private void buttonCriarGrupo_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Grupos/Criar.xaml", UriKind.Relative));
        }

        private void listBoxGrupos_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            (App.Current as App).DescricaoGrupoSelecionado = (listBoxGrupos.SelectedItem as Modelo.Grupo).Descricao;
            (App.Current as App).IdGrupoSelecionado = (listBoxGrupos.SelectedItem as Modelo.Grupo).Id;
            (App.Current as App).IdAdmGrupoSelecionado = (listBoxGrupos.SelectedItem as Modelo.Grupo).IdAdm;
            
            
            if ((App.Current as App).IdAdmGrupoSelecionado == (App.Current as App).IdUsuarioLogado)
            {
                NavigationService.Navigate(new Uri("/Grupos/Meus/EnviarMsg.xaml", UriKind.Relative));
            }
            else
            {
                NavigationService.Navigate(new Uri("/Grupos/EnviarMsg.xaml", UriKind.Relative));
            }
        }

        private void buttonConversas_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Conversa.xaml", UriKind.Relative));
        }
    }
}