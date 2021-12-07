using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace httpServerLab3
{
    class JsonView
    {
        public string output { get; set; }
        public JsonView(string jsonInput)
        {
            this.jsonString = jsonInput;
            var input = DeserializeJsonString();
            var output = Calculator.calculate(input);
            this.output = serializeOutput(output);
        }
        private string jsonString { get; set; }
        public Input DeserializeJsonString()
        {
            return JsonConvert.DeserializeObject<Input>(this.jsonString);
        }
        public static string serializeOutput(Output output)
        {
            return JsonConvert.SerializeObject(output);

        }
        public static Output DeserializeOutput(string output)
        {
            return JsonConvert.DeserializeObject<Output>(output);
        }
    }
}
