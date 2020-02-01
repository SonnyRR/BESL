namespace BESL.Common
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    // https://github.com/SonnyRR/DB-Advanced/blob/master/Exams/07APR2019/Cinema/DataProcessor/KotsevExamHelper.cs
    public class JsonHelper
    {
        /// <summary>
        /// Serializes the object(s) to json.
        /// </summary>
        /// <returns>JSON string</returns>
        /// <param name="object">Object to serialize.</param>
        /// <param name="ignoreNullValues">If set to <c>true</c> ignore null values.</param>
        /// <param name="indentJson">If set to <c>true</c> indent json.</param>
        /// <param name="shouldUseCamelCase">If set to <c>true</c> should use camel case.</param>
        public static string SerializeObjectToJson
            (object @object, bool ignoreNullValues = false, bool indentJson = false, bool shouldUseCamelCase = false)
        {

            var serializerSettings = new JsonSerializerSettings();

            if (ignoreNullValues)
            {
                serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            }

            if (indentJson == false)
            {
                serializerSettings.Formatting = Formatting.None;
            }
            else
            {
                serializerSettings.Formatting = Formatting.Indented;
            }

            if (shouldUseCamelCase)
            {
                serializerSettings.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            }


            string json = JsonConvert.SerializeObject(@object, serializerSettings);

            return json;
        }

        /// <summary>
        /// Deserializes the object(s) from json.
        /// </summary>
        /// <returns>Object(s) from json.</returns>
        /// <param name="jsonInput">Json input.</param>
        /// <typeparam name="T">Type of desired object</typeparam>
        public static T DeserializeObjectFromJson<T>(string jsonInput, string dateFormatString = "dd/MM/yyyy")
        {
            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.DateFormatString = dateFormatString;

            var deserializedObject = JsonConvert.DeserializeObject<T>(jsonInput, jsonSettings);

            return deserializedObject;
        }
    }
}
