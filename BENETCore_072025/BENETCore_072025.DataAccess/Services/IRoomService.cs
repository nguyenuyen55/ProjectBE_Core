using BENETCore_072025.DataAccess.DO;
using BENETCore_072025.DataAccess.DTO;
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

        Task<ReturnReponse> AddRoom(RoomDTO room);
        Task<ReturnReponse> UpdateRoom(int id, RoomDTO roomDTO);
        Task<ReturnReponse> DeleteRoom(int id);
        Task<List<Rooms>> GetRoomsAsyncByKeyWord(int keyword);

    }
}
