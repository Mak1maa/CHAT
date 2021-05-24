using System.ServiceModel;

/*
 * Своеобразный контейнер, где хранится:
 * ID текущего пользователя и его Имя
 * И поле OperationContext - содержит информацию о подключении клиента к нашему сервису, 
 *          чтобы мы потом, когда нам нужно было, могли к тому клиенту который уже подключался, 
 *          обратиться со стороны нашего сервиса.
*/

namespace Chat
{
    public class ServerUser
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public OperationContext operationContext { get; set; }
    }
}