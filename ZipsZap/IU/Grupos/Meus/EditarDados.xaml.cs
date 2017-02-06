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
    public partial class EditarDados : PhoneApplicationPage
    {
        public EditarDados()
        {
            InitializeComponent();
            textBoxDescricao.Text = (App.Current as App).DescricaoGrupoSelecionado;
        }

        private void buttonDeletar_Click(object sender, RoutedEventArgs e)
        {
            Modelo.Grupo.Deletar((App.Current as App).IdGrupoSelecionado);
            (App.Current as App).IdGrupoSelecionado = 0;
            (App.Current as App).DescricaoGrupoSelecionado = null;
            (App.Current as App).IdAdmGrupoSelecionado = 0;

            NavigationService.Navigate(new Uri("/Grupos/Inicial.xaml", UriKind.Relative));
        }

        private void buttonCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void buttonEditar_Click(object sender, RoutedEventArgs e)
        {
            Modelo.Grupo grupo = new Modelo.Grupo { Id = (App.Current as App).IdGrupoSelecionado, Descricao = textBoxDescricao.Text };
            Modelo.Grupo.Alterar(grupo);
            (App.Current as App).IdGrupoSelecionado = 0;
            (App.Current as App).DescricaoGrupoSelecionado = null;
            (App.Current as App).IdAdmGrupoSelecionado = 0;
            NavigationService.Navigate(new Uri("/Grupos/Inicial.xaml", UriKind.Relative));
        }
    }
}