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
    public partial class Listar : PhoneApplicationPage
    {
        public Listar()
        {
            InitializeComponent();
            CarregarLista();
        }

        private void buttonDeletar_Click(object sender, RoutedEventArgs e)
        {
            Modelo.Membro membro = new Modelo.Membro { Grupo_id = (App.Current as App).IdGrupoSelecionado, Usuario_id = (listBoxMembros.SelectedItem as Modelo.Membro).Usuario_id };
            Modelo.Membro.Deletar(membro);
            NavigationService.Navigate(new Uri("/Grupos/Meus/EnviarMsg.xaml", UriKind.Relative));
        }

        private void buttonAtualizar_Click(object sender, RoutedEventArgs e)
        {
            CarregarLista();
        }

        private void buttonAdicionar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Grupos/Meus/AdicionarMembro.xaml", UriKind.Relative));
        }
        private async void CarregarLista()
        {
            List<Modelo.Membro> membros = await Modelo.Membro.Listar((App.Current as App).IdGrupoSelecionado);
            listBoxMembros.ItemsSource = membros;
        }

        private void listBoxMembros_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            
        }
    }
}