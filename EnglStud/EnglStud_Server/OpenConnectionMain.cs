using EnglStud_Server.Entities;
using EnglStud_Server.Json_Classes;
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
                    
                    var clientSchemaFrame = NJsonSchema.JsonSchema.FromType<Response_Event>();
                    JSchema clientSchema = JSchema.Parse(clientSchemaFrame.ToJson().ToString());

                    var wordSchemaFrame = NJsonSchema.JsonSchema.FromType<Word>();
                    JSchema wordSchema = JSchema.Parse(wordSchemaFrame.ToJson().ToString());

                    if (JObject.Parse(JsonString).IsValid(loginSchema))
                    {
                        //Deserialize object
                        User restoredUser = new User(); // object from client
                        restoredUser = JsonConvert.DeserializeObject<User>(JsonString);

                        Response_Event @event;

                        using (DbMainContext db = new DbMainContext())
                        {
                            if (restoredUser.type == 0) // add to db (Sign Up)
                            {
                                User user = db.Users.FirstOrDefault(p => p.Login == restoredUser.Login);
                                if (user == null)
                                {
                                    db.Users.Add(restoredUser);
                                    db.SaveChanges();

                                    @event = new Response_Event(user.Id, "ok");
                                    Response(System.Text.Json.JsonSerializer.Serialize(@event)); // Response User Id

                                    Console.WriteLine("objects was added... Login: " + restoredUser.Login);
                                }
                                else                                
                                    Response("err");
                            }
                            else if (restoredUser.type == 1) // select (Sign In)
                            {
                                User user = db.Users.FirstOrDefault(p => p.Login == restoredUser.Login);
                                if (user != null)
                                {
                                    @event = new Response_Event(user.Id, "ok");
                                    Response(System.Text.Json.JsonSerializer.Serialize(@event));
                                }
                                else                                
                                    Response("err");
                            }
                        }
                    }

                    if (JObject.Parse(JsonString).IsValid(clientSchema))
                    {
                        Response_Event @event = new Response_Event();
                        @event = JsonConvert.DeserializeObject<Response_Event>(JsonString);

                        using (DbMainContext db = new DbMainContext())
                        {
                            if (@event.Id != 0)
                            {
                                // to do request word list
                            }
                            else if (@event.message != null)
                            {

                            }
                        }
                    }

                    if (JObject.Parse(JsonString).IsValid(wordSchema))
                    {
                        Word words = new Word();
                        words = JsonConvert.DeserializeObject<Word>(JsonString);

                        using (DbMainContext db = new DbMainContext())
                        {
                            if (words.IdKnowWords != 0 && words.IdWordUnStudy == 0)
                            {
                                db.Words.Add(words);
                                db.SaveChanges();
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
