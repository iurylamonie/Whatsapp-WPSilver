using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace IU
{
    public partial class Conversa : PhoneApplicationPage
    {
        public Conversa()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var dic = NavigationContext.QueryString;
            
            if (dic.ContainsKey("Rem")) listBoxConversas.Items.Add(dic["Rem"]);
            if (dic.ContainsKey("Cont")) listBoxConversas.Items.Add(dic["Cont"]);
        }

        private void buttonGrupos_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Grupos/Inicial.xaml", UriKind.Relative));
        }

        private void buttonContatos_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Contatos/Inicial.xaml", UriKind.Relative));
        }

        private void buttonAtualizar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}