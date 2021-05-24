using System.ServiceModel;

/**
 * Представляет собой описание того, что может делать наш сервис
**/

namespace Chat
{
    [ServiceContract(CallbackContract = typeof(IServiceChatCallBack))]
    public interface IServiceChat 
    {
        // Подключение к сервису
        [OperationContract]
        int Connect(string name);

        // Вызов, когда клиент покидает чат
        [OperationContract]
        void Disconnect(int id);

        // Отправка сообщений
        [OperationContract(IsOneWay = true)]
        void SendMsg(string msg, int id);
    }

    // Вызов действия со стороны сервера
    public interface IServiceChatCallBack
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallBack(string msg);
    }
}