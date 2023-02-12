using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class FileDataHandler
{
    public class InputJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject json = new JObject();
            InputScheme input = (InputScheme)value;

            if (input is KeyboardInput)
            {
                KeyboardInput keyboard = (KeyboardInput)input;
                json.Add("Type", "KeyboardInput");
            }
            else if (input is MouseInput)
            {
                MouseInput mouseInput = (MouseInput)input;
                json.Add("Type", "MouseInput");
            }
            else if (input is SwipeInput)
            {
                SwipeInput swipeInput = (SwipeInput)input;
                json.Add("Type", "SwipeInput");
            }
            else if (input is TouchInput)
            {
                TouchInput touchInput = (TouchInput)input;
                json.Add("Type", "TouchInput");
            }

            json.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject json = JObject.Load(reader);
            string type = (string)json["Type"];

            if (type == "KeyboardInput")
            {
                KeyboardInput keyboardInput = new KeyboardInput();
                return keyboardInput;
            }
            else if (type == "MouseInput")
            {
                MouseInput mouseInput = new MouseInput();
                return mouseInput;
            }
            else if (type == "SwipeInput")
            {
                SwipeInput swipeInput = new SwipeInput();
                return swipeInput;
            }
            else if (type == "TouchInput")
            {
                TouchInput touchInput = new TouchInput();
                return touchInput;
            }

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InputScheme);
        }
    }

}
