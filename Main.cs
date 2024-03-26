using System;
using System.IO;
using Newtonsoft.Json;

namespace JsonTool
{
    /*
    public class JsonTool
    {
        //Example:
        public static JsonRw<TestClass> Json;
        public static readonly string ConifgPath = "test.json";
        public static void InitConfig()
        {
            var initobj = new TestClass { 
                Number = 8848,
                Text= "HelloWorld!"
            };
            var json = new JsonRw<TestClass>(ConifgPath , initobj);
            Json = json;
        }
    }
    */
    public class JsonRw<T> where T : new()
    {
        public T ConfigObj { get; private set; }
        public string ConfigPath { get; private set; }
        public JsonRw(string path, T initialConfig)//有初始类
        {
            ConfigPath = Path.Combine(AppContext.BaseDirectory, path);
            ConfigObj = initialConfig;
            ReadConfig();
        }
        public JsonRw(string path)//无初始类
        {
            ConfigPath = Path.Combine(AppContext.BaseDirectory, path);
            ConfigObj = new();
            ReadConfig();
        }
        public bool Exists()
        {
            return File.Exists(ConfigPath);
        }
        public void ReadConfig()
        {
            try
            {
                if (!File.Exists(ConfigPath))
                {
                    //
                }
                else
                {
                    using (StreamReader file = File.OpenText(ConfigPath))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        ConfigObj = (T)serializer.Deserialize(file, typeof(T));
                    }
                }
                WriteConfig();
            }
            catch
            {
            }
        }
        public void WriteConfig()
        {
            FileStream fileStream = new FileStream(ConfigPath, FileMode.Create, FileAccess.Write, FileShare.Write);
            string value = JsonConvert.SerializeObject(ConfigObj, Formatting.Indented);
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.Write(value);
            }
        }
    }
}
