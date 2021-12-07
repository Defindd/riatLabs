using System;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace httpServer
{
    class Program
    {
            const string inputJSON = "{\"K\":10,\"Sums\":[1.01,2.02],\"Muls\":[1,4]}";
            static async Task Main()
            {
                JsonView correctJson = new JsonView(inputJSON);
                HttpListener listener = new HttpListener();
                listener.Prefixes.Add("http://127.0.0.1:8060/");
                listener.Start();
                Console.WriteLine("Ожидание подключений...");
                int responseCount = 0;
                var isOver = false; 
                while (!isOver)
                {
                    HttpListenerContext context = await listener.GetContextAsync();
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;

                    string responseString = "error";
                    if (request.RawUrl.ToLower().Contains("/ping"))
                    {
                        response.StatusCode = (int)HttpStatusCode.OK;
                        responseString = $"{HttpStatusCode.OK}";
                    }
                    if (request.RawUrl.ToLower().Contains("/getinputdata"))
                    {
                        responseString = inputJSON;
                    }
                    if (request.RawUrl.ToLower().Contains("/writeanswer"))
                    {
                        string answerString = request.QueryString["answer"];     
                        answerString = answerString.Remove(0,1);
                    answerString = answerString.Remove(answerString.Length - 1,1);
                    var json = JsonView.DeserializeOutput(answerString);
                        var correctoutput = JsonView.DeserializeOutput(correctJson.output);
                    Console.WriteLine(correctJson.output);

                        if (json.CompareTo(correctoutput) ==1)
                            responseString = "Answer is right";
                        else responseString = "Incorrect answer";
                    }


                    Console.WriteLine(responseString);
                    byte[] body = System.Text.Encoding.UTF8.GetBytes(responseString);
                    response.ContentLength64 = body.Length;
                   response.OutputStream.Write(body, 0, body.Length);
                    responseCount++;
                    if (responseCount > 10) isOver = !isOver;
                }
                listener.Stop();
                Console.WriteLine("Обработка подключений завершена");
                Console.Read();
            }
        }
    }

