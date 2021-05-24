using System.Windows;
using System.Windows.Input;
using ChatClient.ServiceChat;

namespace ChatClient
{
    public partial class MainWindow : Window, IServiceChatCallback
    {
        // Подключён ли на данный момент клиент к серверу
        bool isConnected = false;
        ServiceChatClient client; // Объект нашего хоста, чтобы взаимодействовать с его методами
        int ID;

        public MainWindow()
        {
            InitializeComponent();
        }

        void ConnectUser()
        {
            if (!isConnected)
            {
                // Создание и выделение памяти под наш объект ServiceChatClient
                client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false; // Блокировка возможности изменить имя пользователя
                bConDiscon.Content = "Отсоединиться";
                isConnected = true;
            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                tbUserName.IsEnabled = true; // Возможности изменить имя пользователя
                bConDiscon.Content = "Присоединиться";
                isConnected = false;
            }
        }

        // Обработка событий при нажатии на кнопку
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser(); // Отсоединение пользователя, если подключён
            }
            else
            {
                ConnectUser(); // Присоединение пользователя, если не подключён
            }
        }

        public void MsgCallBack(string msg)
        {
            lbChat.Items.Add(msg); // Добавление сообщения в listbox
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]); // Скролл к последнему сообщению
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser(); // Отсоединение пользователя при закрытии приложения 
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(client != null)
                {
                    client.SendMsg(tbMessage.Text, ID);
                    tbMessage.Text = string.Empty;
                }
            }
        }
    }
}