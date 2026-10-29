using Oxide.Core.Plugins;
using Microsoft.Data.Sqlite;

namespace Oxide.Ext.SQLite
{
    public sealed class Connection
    {
        internal string ConnectionString { get; set; }
        internal bool ConnectionPersistent { get; set; }
        internal SqliteConnection Con { get; set; }
        internal Plugin Plugin { get; set; }
        public long LastInsertRowId { get; internal set; }

        public Connection(string connection, bool persistent)
        {
            ConnectionString = connection;
            ConnectionPersistent = persistent;
        }
    }
}
