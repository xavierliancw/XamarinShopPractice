using System;
using System.Threading.Tasks;
using ShopPractice.DatabaseStuff;

namespace ShopPractice
{
    public class DatabaseObject
    {
        public virtual async Task<int> DeleteFromDatabase()
        {
            return await TheDatabase.Connection.DeleteAsync(this);
        }

        public async Task SaveToDatabase()
        {
            await TheDatabase.Connection.InsertAsync(this);
        }
    }
}
