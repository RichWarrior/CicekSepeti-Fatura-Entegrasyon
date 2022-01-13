
using Core.Utilities;
using StackExchange.Redis;

namespace Service.Repositories
{
    public class BaseRepository
    {
        public DataContext Context { get; private set; }

        private ConnectionMultiplexer _cache { get; set; }

        public BaseRepository(DataContext Context)
        {
            this.Context = Context;
        }

        protected ConnectionMultiplexer Cache
        {
            get
            {
                if(_cache == null)
                {
                    var connectionInfo = ConnectionInfo.Instance;
                    _cache = ConnectionMultiplexer.Connect(connectionInfo.RedisConnectionString);
                }
                return _cache;
            }
        }
    }
}
