using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using IU.Resources;
using Microsoft.Phone.Notification;
using System.Text;

namespace IU
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string nomeCanal = "whatsCanal";
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private async void buttonPrincipal_Click(object sender, RoutedEventArgs e)
        {
            HttpNotificationChannel canalPush = HttpNotificationChannel.Find(nomeCanal);
            try
            {
                //Canal não foi criado
                if (canalPush == null)
                {
                    //Usuário já cadastrado
                    if (await Modelo.Usuario.Verificar(textBoxNome.Text) == textBoxNome.Text )
                    {
                        canalPush = new HttpNotificationChannel(nomeCanal);
                        canalPush.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(AtualizarUriCanal);
                        canalPush.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(CanalPush_ErrorOccurred);
                        canalPush.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);
                        canalPush.Open();
                        canalPush.BindToShellToast();
                        NavigationService.Navigate(new Uri("/Contatos/Inicial.xaml", UriKind.Relative));
                        Modelo.Usuario usuario = await Modelo.Usuario.Obter(textBoxNome.Text);
                        (App.Current as App).NomeUsuarioLogado = usuario.Nome;
                        (App.Current as App).IdUsuarioLogado = usuario.Id;
                    }
                    //Usuario sem cadastro
                    else
                    {
                        canalPush = new HttpNotificationChannel(nomeCanal);
                        canalPush.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(CriarUriCanal);
                        canalPush.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(CanalPush_ErrorOccurred);
                        canalPush.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);
                        canalPush.Open();
                        canalPush.BindToShellToast();
                        NavigationService.Navigate(new Uri("/Contatos/Inicial.xaml", UriKind.Relative));
                        Modelo.Usuario usuario = await Modelo.Usuario.Obter(textBoxNome.Text);
                        (App.Current as App).NomeUsuarioLogado = usuario.Nome;
                        (App.Current as App).IdUsuarioLogado = usuario.Id;
                    }
                    
                }
                //Canal já foi criado
                else
                {
                    if (await Modelo.Usuario.Verificar(textBoxNome.Text) == textBoxNome.Text)
                    {
                        canalPush.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(AtualizarUriCanal);
                        canalPush.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(CanalPush_ErrorOccurred);
                        canalPush.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);
                        NavigationService.Navigate(new Uri("/Contatos/Inicial.xaml", UriKind.Relative));
                        Modelo.Usuario usuario = await Modelo.Usuario.Obter(textBoxNome.Text);
                        (App.Current as App).NomeUsuarioLogado = usuario.Nome;
                        (App.Current as App).IdUsuarioLogado = usuario.Id;
                    }
                    else
                    {
                        canalPush.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(CriarUriCanal);
                        canalPush.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(CanalPush_ErrorOccurred);
                        canalPush.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);
                        NavigationService.Navigate(new Uri("/Contatos/Inicial.xaml", UriKind.Relative));
                        Modelo.Usuario usuario = await Modelo.Usuario.Obter(textBoxNome.Text);
                        (App.Current as App).NomeUsuarioLogado = usuario.Nome;
                        (App.Current as App).IdUsuarioLogado = usuario.Id;
                    }
                    
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        private void AtualizarUriCanal(object sender, NotificationChannelUriEventArgs e)
        {
            Dispatcher.BeginInvoke(
                () => {
                    System.Diagnostics.Debug.WriteLine(e.ChannelUri.ToString());
                    Modelo.Usuario u = new Modelo.Usuario
                    {
                        Nome = textBoxNome.Text,
                        Uri = e.ChannelUri.ToString()
                    };
                    Modelo.Usuario.AlterarUri(u);
                }
                );
        }

        private void CriarUriCanal(object sender, NotificationChannelUriEventArgs e)
        {
            Dispatcher.BeginInvoke(
                () => {
                    System.Diagnostics.Debug.WriteLine(e.ChannelUri.ToString());
                    Modelo.Usuario u = new Modelo.Usuario
                    {
                        Nome = textBoxNome.Text,
                        Uri = e.ChannelUri.ToString()
                    };
                    Modelo.Usuario.Cadastrar(u);
                }
                );
        }

        private void CanalPush_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
                MessageBox.Show(String.Format("A notificação {0} ocorreu o erro:  {1} ({2}) {3}",
                    e.ErrorType, e.Message, e.ErrorCode, e.ErrorAdditionalData))
                    );
        }

        private void PushChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            StringBuilder message = new StringBuilder();
            string relativeUri = string.Empty;

            message.AppendFormat("Mensagem Recebida às {0}:\n", DateTime.Now.ToShortTimeString());
            message.AppendFormat("{0} :", e.Collection["wp:Text1"]);
            message.AppendFormat(" '{0}'", e.Collection["wp:Text2"]);

            Dispatcher.BeginInvoke(() => MessageBox.Show(message.ToString()));
        }
    }
}