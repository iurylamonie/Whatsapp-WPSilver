using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;


namespace IU.Contatos
{
    public partial class Inicial : PhoneApplicationPage
    {
        List<Modelo.Usuario> usuario;
        public Inicial()
        {
            CarregarLista();
            InitializeComponent();
        }

        private async void buttonAtualizar_Click(object sender, RoutedEventArgs e)
        {
            usuario = await Modelo.Usuario.Listar();
            listBoxContatos.ItemsSource = usuario;
        }

        private void listBoxContatos_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            (App.Current as App).NomeUsuarioSelecionado = listBoxContatos.SelectedItem.ToString();
            NavigationService.Navigate(new Uri("/Contatos/EnviarMsg.xaml", UriKind.Relative));
        }

        private async void CarregarLista()
        {
            usuario = await Modelo.Usuario.Listar();
            listBoxContatos.ItemsSource = usuario;
        }

        private void buttonMeuDados_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Contatos/EditarDados.xaml", UriKind.Relative));
        }

        private void buttonGrupos_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Grupos/Inicial.xaml", UriKind.Relative));
        }

        private void buttonConversas_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Conversa.xaml", UriKind.Relative));
        }
    }
}