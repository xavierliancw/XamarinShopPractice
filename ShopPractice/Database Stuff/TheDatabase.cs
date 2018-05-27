using DatabaseStuff;
using SQLite;
using Xamarin.Forms;
namespace ShopPractice.DatabaseStuff
{
    public interface INativeBridgeToDB
    {
        string GetThePath(string filename);
    }

    /// <summary>
    /// Automatically creates all database tables and is the system-wide access point into the database.
    /// </summary>
    public static class TheDatabase
    {
        private static SQLiteAsyncConnection _connection;

        /// <summary>
        /// A query-able connection to the database.
        /// </summary>
        /// <value>The connection to the database.</value>
        public static SQLiteAsyncConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    //Use the DependencyService and the NativeBridgeToDB to get a device's path to the DB because it's platform specific code
                    _connection = new SQLiteAsyncConnection(
                        DependencyService.Get<INativeBridgeToDB>().GetThePath("TheDatabase.db3") //Magic string because this should never change.
                    );
                    //Create the tables
                    _connection.CreateTablesAsync<ChatBubbleDO, ChatParticipantDO, ConversationDO>().Wait();
                }
                return _connection;
            }
        }
    }
}
