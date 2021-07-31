using EnglStud.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EnglStud
{
    class TcpConnection
    {
        int port = 8005; // порт сервера
        string address = "127.0.0.1"; // адрес сервера
        public string message = null;
        public Response_Event Response = null;
        public ListWordsToClient wordsResponse = null;

        public TcpConnection() { }
        public TcpConnection(string message)
        {
            this.message = message;
        }

        public void SendToServer()
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);

                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);

                // получаем ответ
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт

                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                //Response = builder.ToString(); // {"message":null,"Id":1}

                string JsonString = builder.ToString();  // Json Magic

                //------------------------------Json Parse------------------------------------//

                var loginSchemaFrame = NJsonSchema.JsonSchema.FromType<Response_Event>();
                JSchema loginSchema = JSchema.Parse(loginSchemaFrame.ToJson().ToString());

                var wordsSchemaFrame = NJsonSchema.JsonSchema.FromType<ListWordsToClient>();
                JSchema wordsSchema = JSchema.Parse(wordsSchemaFrame.ToJson().ToString());


                if (JObject.Parse(JsonString).IsValid(wordsSchema)) // code 37
                {
                    ListWordsToClient restoredWords = new ListWordsToClient();
                    restoredWords = JsonConvert.DeserializeObject<ListWordsToClient>(JsonString);

                    wordsResponse = restoredWords;
                }
                else if (JObject.Parse(JsonString).IsValid(loginSchema))
                {
                    //Deserialize object
                    Response_Event restoredEvent = new Response_Event(); // object from client
                    restoredEvent = JsonConvert.DeserializeObject<Response_Event>(JsonString);

                    Response = restoredEvent;
                }
                

                // закрываем сокет-------------------------------------------------------------//
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}