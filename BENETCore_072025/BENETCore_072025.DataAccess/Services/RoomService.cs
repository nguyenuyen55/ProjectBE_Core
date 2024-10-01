using BENETCore_072025.DataAccess.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BENETCore_072025.DataAccess.Services
{
    public class RoomService : IRoomService
    {
        public Task AddRoom(Rooms room)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRoom(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Rooms> GetRoomAsync(int roomId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Rooms>> GetRoomsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Rooms>> GetRoomsAsyncByKeyWord(string keyword)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRoom(int id, Rooms room)
        {
            throw new NotImplementedException();
        }
    }
}
