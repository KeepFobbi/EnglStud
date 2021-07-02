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

                    if (JObject.Parse(JsonString).IsValid(loginSchema))
                    {
                        //Deserialize object
                        User restoredUser = new User(); // object from client
                        restoredUser = JsonConvert.DeserializeObject<User>(JsonString);

                        using (DbMainContext db = new DbMainContext())
                        {
                            if (restoredUser.type == 0) // add to db (Sign Up)
                            {
                                User user = db.Users.FirstOrDefault(p => p.Login == restoredUser.Login);
                                if (user == null)
                                {
                                    db.Users.Add(restoredUser);
                                    db.SaveChanges();

                                    Response("ok");
                                    Console.WriteLine("objects was added... Login: " + restoredUser.Login);
                                }
                                else                                
                                    Response("err");
                            }
                            else if (restoredUser.type == 1) // select (Sign In)
                            {
                                //var users_TryAuth = (from user in db.Users where user.Login == restoredUser.Login select user).ToList();
                                User user = db.Users.FirstOrDefault(p => p.Login == restoredUser.Login);
                                if (user != null)
                                    Response("ok");
                                else                                
                                    Response("err");
                            }
                        }

                        
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
