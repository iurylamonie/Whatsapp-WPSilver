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
    public partial class EditarDados : PhoneApplicationPage
    {
        public EditarDados()
        {
            InitializeComponent();
            textBoxNome.Text = (App.Current as App).NomeUsuarioLogado;
        }

        private void buttonCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void buttonEditar_Click(object sender, RoutedEventArgs e)
        {
            Modelo.Usuario usuario = new Modelo.Usuario { Nome = textBoxNome.Text };
            Modelo.Usuario.AlterarNome((App.Current as App).NomeUsuarioLogado, usuario);
            (App.Current as App).NomeUsuarioLogado = textBoxNome.Text;
            NavigationService.Navigate(new Uri("/Contatos/Inicial.xaml", UriKind.Relative));
        }

        private void buttonDeletar_Click(object sender, RoutedEventArgs e)
        {
            Modelo.Usuario.Deletar((App.Current as App).NomeUsuarioLogado);
            (App.Current as App).NomeUsuarioLogado = null;
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}