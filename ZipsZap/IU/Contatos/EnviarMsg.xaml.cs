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
    public partial class EnviarMsg : PhoneApplicationPage
    {
        Modelo.Usuario usuario;
        public EnviarMsg()
        {
            CarregarDados();
            InitializeComponent();
            
        }

        public async void CarregarDados()
        {
            usuario = await Modelo.Usuario.Obter((App.Current as App).NomeUsuarioSelecionado);
            textBlockNomeSelecionado.Text = (App.Current as App).NomeUsuarioSelecionado;
        }
        private void buttonEnviar_Click(object sender, RoutedEventArgs e)
        {
            Modelo.Mensagem msg = new Modelo.Mensagem
            {
                Remetente = (App.Current as App).NomeUsuarioLogado,
                Uri = usuario.Uri,
                Conteudo = textBoxMsg.Text
            };
            Modelo.Mensagem.Enviar(msg);
        }

        private void buttonCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}