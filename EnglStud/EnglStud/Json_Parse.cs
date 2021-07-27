using EnglStud.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglStud
{
    class Json_Parse
    {
        public Response_Event Json_Handler(string JsonString)
        {
            var loginSchemaFrame = NJsonSchema.JsonSchema.FromType<Response_Event>();
            JSchema loginSchema = JSchema.Parse(loginSchemaFrame.ToJson().ToString());

            var wordsSchemaFrame = NJsonSchema.JsonSchema.FromType<WordsToServer>();
            JSchema wordsSchema = JSchema.Parse(wordsSchemaFrame.ToJson().ToString());



            if (JObject.Parse(JsonString).IsValid(loginSchema))
            {
                //Deserialize object
                Response_Event restoredEvent = new Response_Event(); // object from client
                restoredEvent = JsonConvert.DeserializeObject<Response_Event>(JsonString);

                return restoredEvent;
            }
            else if (JObject.Parse(JsonString).IsValid(wordsSchema)) // code 37
            {
                WordsToServer restoredWords = new WordsToServer();
                restoredWords = JsonConvert.DeserializeObject<WordsToServer>(JsonString);

                return restoredWords;
            }

            return null;
        }
    }
}
