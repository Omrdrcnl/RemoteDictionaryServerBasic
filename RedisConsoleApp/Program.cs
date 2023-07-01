using StackExchange.Redis; // Redis bağlantısı için StackExchange.Redis kütüphanesini kullanıyoruz

class Program
{
    static void Main(string[] args)
    {
        string redisBaglanti = "localhost:6379";
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(redisBaglanti);
        IDatabase database = redis.GetDatabase();

        Dictionary<string, string> keyValue = new Dictionary<string, string>()
        {
            {"Name", "Ömer" },
            {"Surname", "DİRİCANLI" },
            {"Age", "29" }

        };
        foreach(var kv  in keyValue)
        {
            database.StringSet(kv.Key, kv.Value);
        }

        // Bir verileri arraya ekleme
        List<RedisValue> DonenDeger = new List<RedisValue>();
        foreach(var key in keyValue.Keys)
        {
            RedisValue value = database.StringGet(key);
            DonenDeger.Add(value);
        };
       //Ekrana Yazdırma

        for(int i = 0; i < keyValue.Count; i++)
        {
            Console.WriteLine("Keys değeri; " + keyValue.Keys.ElementAt(i) + "  Value Değeri: " + DonenDeger[i]) ;
        }

        foreach(var key in keyValue.Keys)
        {
            if (key.Contains("Age")) {
                database.KeyDelete(key);
                Console.WriteLine("Deger basarıyla silindi");
                break;
            }
            
        };

        DonenDeger.Clear();

        foreach(var key in keyValue.Keys)
        {
            RedisValue value = database.StringGet(key);
            DonenDeger.Add(value);
        }

        for (int i = 0; i < keyValue.Count; i++)
        {
            Console.WriteLine("Silme işleminden sonra Keys değeri; " + keyValue.Keys.ElementAt(i) + "       Silme işleminden sonra Value Değeri: " + DonenDeger[i]);
        }


       redis.Close();

       Console.ReadLine();
    }
}
