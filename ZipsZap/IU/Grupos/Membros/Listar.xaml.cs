using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace IU.Grupos.Membros
{
    public partial class Listar : PhoneApplicationPage
    {
        public Listar()
        {
            InitializeComponent();
            CarregarLista();
        }
        private async void CarregarLista()
        {
            List<Modelo.Membro> membros = await Modelo.Membro.Listar((App.Current as App).IdGrupoSelecionado);
            listBoxMembros.ItemsSource = membros;
        }

        private void buttonAtualizar_Click(object sender, RoutedEventArgs e)
        {
            CarregarLista();
        }
    }
}