using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QM_ContextMenuHotkeys
{

    //In progress.  A bit of a hack right now.

    /// <summary>
    /// Converts array types of enums.  The Array type must have an Add(T) function
    /// Original code from https://blog.bitscry.com/2017/09/06/single-or-array-enum-json-converter/
    /// </summary>
    /// <typeparam name="ListT"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class JsonArrayEnumConverter<ListT, T> : JsonConverter
        where ListT : IEnumerable<T>, new()
        where T : struct, Enum
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(ListT));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            MethodInfo addFunction = typeof(ListT).GetMethod("Add");
            ListT list = new ListT();

            if (token.Type == JTokenType.Array)
            {
                token.ToObject<List<string>>().ForEach(x => addFunction.Invoke(list, new object[] { (T)Enum.Parse(typeof(T), x) }));
                return list;
            }

            addFunction.Invoke(list, new object[] { (T)Enum.Parse(typeof(T), token.ToObject<string>()) });
            return list;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //if (value.GetType() == typeof(List<T>))
            if (value.GetType() == typeof(ListT))
            {
                ListT list = (ListT)value;
                value = list.Select(x => x.ToString()).ToList();
            }
            else if (value.GetType() == typeof(T))
            {

                value = value.ToString();
            }

            serializer.Serialize(writer, value);
        }

        public override bool CanWrite
        {
            get { return true; }
        }
    }
}
