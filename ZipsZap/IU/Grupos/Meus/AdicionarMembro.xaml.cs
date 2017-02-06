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
    public partial class AdicionarMembro : PhoneApplicationPage
    {
        public AdicionarMembro()
        {
            InitializeComponent();
        }

        private void buttonCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void buttonAdicionar_Click(object sender, RoutedEventArgs e)
        {
            if (await Modelo.Usuario.Verificar(textBoxNome.Text) == textBoxNome.Text)
            {
                Modelo.Usuario usuario = await Modelo.Usuario.Obter(textBoxNome.Text);
               
                if (usuario != null)
                {
                    Modelo.Membro novoMembro = new Modelo.Membro { Usuario_id = usuario.Id, Grupo_id = (App.Current as App).IdGrupoSelecionado, Usuario = usuario };
                    MessageBox.Show(novoMembro.Usuario_id.ToString());
                    Modelo.Membro.Adicionar(novoMembro);
                }
                else
                {
                    MessageBox.Show("Eu odeio Async");
                }

               
            }
            else
            {
                textBlockAviso.Visibility = Visibility.Visible;
            }
        }

    }
}