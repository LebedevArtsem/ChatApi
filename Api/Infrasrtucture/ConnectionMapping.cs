using System.Collections.Generic;

namespace Api.Infrasrtucture
{
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connections = new Dictionary<T, HashSet<string>>();

        public HashSet<string> GetConnection(T key)
        {
            lock(_connections)
            {
                var connection = _connections[key];
                return connection;
            }
        }

        public void AddConnection(T key,string connectionId) 
        {
            lock (_connections)
            {
                HashSet<string> connection;
                if (!_connections.TryGetValue(key,out connection))
                {
                    connection = new HashSet<string>();
                    _connections.Add(key, connection);
                }

                lock(connection)
                {
                    connection.Add(connectionId);
                }
            }
        }

        public void RemoveConnection(T key,string connectionId) 
        {
            lock (_connections)
            {
                HashSet<string> connection;
                if (!_connections.TryGetValue(key,out connection))
                {
                    return;
                }

                lock(connection)
                {
                    connection.Remove(connectionId);

                    if (connection.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }
}
