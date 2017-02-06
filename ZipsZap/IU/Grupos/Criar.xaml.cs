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
    public partial class Criar : PhoneApplicationPage
    {
        public Criar()
        {
            InitializeComponent();
        }

        private void buttonCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void buttonCriar_Click(object sender, RoutedEventArgs e)
        {
            Modelo.Grupo grupo = new Modelo.Grupo { Descricao = textBoxNome.Text, IdAdm = (App.Current as App).IdUsuarioLogado };
            Modelo.Grupo.Criar(grupo);
            NavigationService.Navigate(new Uri("/Grupos/Inicial.xaml", UriKind.Relative));
        }
    }
}