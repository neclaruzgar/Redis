using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis
{
    public class RedisClient
    {
        //Paketteki Client'ı tanımlama
        private static Lazy<IRedisClientsManager> ClientManager { get; set; }
        public RedisClient()
        {
            ClientManager = InitializeRedisClient();//Redise bağlanılacak metod yazılıyor
        }
        private Lazy<IRedisClientsManager> InitializeRedisClient() //Lazy<IRedisClientsManager> tipinde döndürmeli
        {
            //Redise bağlanmak için şifre port gibi gerekli bilgiler yazılıyor
            var connetionString = "redis://clientid:necla@localhost:6379?ssl=false&db=0"; // şifre-localhostta bağlanıyorum-ssl kullanığ kullanmadığım bilgisi
            var result = new Lazy<IRedisClientsManager>(() => new BasicRedisClientManager(connetionString) //Redise bağlantı yapılacak kısım seçilir || Lazy<IRedisClientsManager> tipinde döndürecek
            {
                ConnectTimeout = -1,
                SocketReceiveTimeout=-1 //Veriyi çekerken bekleme süresi
            });

            return result;
        }
        public List<string> GetAllKeys()
        {
            using (var redisClient=ClientManager.Value.GetClient())
            {
                var keys = redisClient.GetAllKeys();
                return keys;
            }
        }
        public void Set(string key, object data, int ttl ) //İçeriye veri yazmakla yükümlü bir metot || ttl ne kadar süre hayatta kalacağı bilgisi
        {
            using (var redisClient = ClientManager.Value.GetClient())
            {
                var redis = redisClient.As<object>(); //redisClient object tipinde
                redis.SetValue(key, data); //İçerisine key ve value alıcak. Keyin adı ne olacak ve bunun içerisini neyle dolduralacağı yazılır 
                redis.ExpireEntryAt(key, DateTime.Now.AddMinutes(ttl)); //Şuandan itibaren ttl için verilecek dakika kadar hayatta kalacak
            }
        }
        public T Get<T>(string key)
        {
            using(var redisClient = ClientManager.Value.GetClient())
            {
                var typedRedisClient = redisClient.As<T>(); //T tipinde herhangi bir tip
                return typedRedisClient.GetValue(key);
            }
        }
    }
}
