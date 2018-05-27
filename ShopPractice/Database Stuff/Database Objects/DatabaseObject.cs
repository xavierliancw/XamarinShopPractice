using System;
using System.Threading.Tasks;
using ShopPractice.DatabaseStuff;

namespace DatabaseStuff
{
    public class DatabaseObject
    {
        public virtual async Task<int> DeleteFromDatabase()
        {
            return await TheDatabase.Connection.DeleteAsync(this);
        }

        public virtual async Task SaveToDatabase()
        {
            await TheDatabase.Connection.InsertAsync(this);
        }
    }
}
