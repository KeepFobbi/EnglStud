using EnglStud_Server.Entities;
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

namespace EnglStud_Server
{
    class OpenConnection
    {
        Socket handler;
        StringBuilder builder;
        byte[] data;
        static int port = 8005; // порт для приема входящих запросов
        public OpenConnection()
        {
            // получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);

                // начинаем прослушивание
                listenSocket.Listen(10);

                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    handler = listenSocket.Accept();
                    // получаем сообщение
                    builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    data = new byte[256]; // буфер для получаемых данных

                    do // byte -> string
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    string JsonString = builder.ToString();  // Json Magic
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + JsonString);

                    var loginSchemaFrame = NJsonSchema.JsonSchema.FromType<User>();
                    JSchema loginSchema = JSchema.Parse(loginSchemaFrame.ToJson().ToString());

                    if (JObject.Parse(JsonString).IsValid(loginSchema)) //Sign Up
                    {
                        //Deserialize object
                        User restoredUser = new User();
                        restoredUser = JsonConvert.DeserializeObject<User>(JsonString);

                        DbMainContext db = new DbMainContext();

                        // to do
                        //user user = new user("test4", "test4", "test4@ukr.net"); // create a user object
                        //user user2 = new user(1, "test", "test", "test@ukr.net");

                        //db.users.add(user); // add user object into db
                        ////db.users.add(user2);
                        //db.savechanges(); // save db
                        //console.writeline("objects was added...");

                        //console.readline();

                        //var users = db.users;
                        //console.writeline("object list");
                        //foreach (user u in users)
                        //{
                        //    console.writeline("{0}.{1} - {2} - {3}", u.id, u.login, u.password, u.email);
                        //}
                        //console.writeline(user2.gettype());

                        Console.WriteLine(restoredUser.Login);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Response(string message = "ваше сообщение доставлено")
        {
            // отправляем ответ            
            data = Encoding.Unicode.GetBytes(message);
            handler.Send(data);
            // закрываем сокет
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }
    }
}
