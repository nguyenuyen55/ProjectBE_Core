using BENETCore_072025.DataAccess.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BENETCore_072025.DataAccess.Services
{
   public interface IRoomService
    {
        Task<List<Rooms>> GetRoomsAsync();
        Task<Rooms> GetRoomAsync(int roomId);

        Task AddRoom(Rooms room);
        Task UpdateRoom(int id,Rooms room);
        Task DeleteRoom(int id);
        Task<List<Rooms>> GetRoomsAsyncByKeyWord(string keyword);

    }
}
