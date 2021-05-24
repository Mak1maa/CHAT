using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

/**
 * Реализвация интерфейса IServiceChat
**/

namespace Chat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        // Лист пользователей
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1; // Генерация id-шников

        public int Connect(string name)
        {
            // Создание пользователя
            ServerUser user = new ServerUser()
            {
                ID = nextId,
                Name = name,
                operationContext = OperationContext.Current
            };
            nextId++;

            SendMsg(": " + user.Name + " вошёл в чат.", 0);

            users.Add(user);

            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if(user != null)
            {
                users.Remove(user);
                SendMsg(": " + user.Name + " вышел из чата.", 0);
            }
        }

        public void SendMsg(string msg, int id)
        {
            foreach(var item in users)
            {
                // Ответ от сервера всем пользователям
                string answer = DateTime.Now.ToShortTimeString();

                var user = users.FirstOrDefault(i => i.ID == id);
                if (user != null)
                {
                    answer += ": " + user.Name + " ";
                }

                answer += msg;

                // Отправка сообщения текущему пользователю, с которым мы работаем в цикле foreach
                item.operationContext.GetCallbackChannel<IServiceChatCallBack>().MsgCallBack(answer);
            }
        }
    }
}