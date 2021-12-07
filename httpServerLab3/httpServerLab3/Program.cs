using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web;
namespace httpServerLab3
{
    class Program
    {
        public static string inputToSerialize { get; set; }
        static async Task Main()
        {            
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:8030/");
            listener.Start();
            Console.WriteLine("Ожидание подключения...");
            bool isOver = false;
            string responseString = "error";
            while (!isOver)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                Console.WriteLine($"Получен запрос с командой:  {request.Url.AbsolutePath}");
                switch (request.Url.AbsolutePath.ToLower()) {
                    case "/ping":
                        {
                            response.StatusCode = (int)HttpStatusCode.OK;
                            responseString = $"{HttpStatusCode.OK}";
                            break;
                        }
                    case "/postinputdata":
                        {
                            using (var reader = new StreamReader(request.InputStream,
                                     request.ContentEncoding))
                            {
                                inputToSerialize = reader.ReadToEnd();
                            }
                            responseString = "Данные были успешно отправлены";
                            break;
                        }
                    case "/getanswer":
                        {
                            if (String.IsNullOrWhiteSpace(inputToSerialize))
                                break;
                            var jsonView = new JsonView(inputToSerialize);
                            responseString = jsonView.output;
                            break;
                        }

                    case "/stop":
                        {
                            
                            isOver = !isOver;
                            Console.WriteLine("Сервер был остановлен");
                            responseString = "Сервер был остановлен";
                            break;
                        }
                    default:
                        {
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            break;
                        }
                }
                byte[] body = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = body.Length;
                response.OutputStream.Write(body, 0, body.Length);

                response.Close();
            }
            listener.Stop();
        }
        }
    }

