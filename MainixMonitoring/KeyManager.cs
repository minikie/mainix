using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Akavache;

namespace MainixMonitoring
{
    public class KeyManager
    {
        public void set_key<T>(string key, T value)
        {
            BlobCache.UserAccount.InsertObject(key, value);
        }

        public async Task<T> get_key<T>(string key)
        {
            try
            {
                var v = await BlobCache.UserAccount.GetObject<T>(key);
                return v;

            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }

        }
    }
}
