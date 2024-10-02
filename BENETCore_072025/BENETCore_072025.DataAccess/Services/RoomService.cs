using BENETCore_072025.DataAccess.DO;
using BENETCore_072025.DataAccess.DTO;
using BENETCore_072025.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BENETCore_072025.DataAccess.Services
{
    public class RoomService : IRoomService

    {
        private readonly IunitOfWork _unitOfWork;
        public RoomService(IunitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ReturnReponse> AddRoom(RoomDTO room)
        {
            var response=new ReturnReponse();
            var roomAdd = new Rooms()
            {
                idRoomType = room.idroomType,
                NumberRoom = room.numberRoom,
                IsAvailable = room.isAvailable
            };
             await _unitOfWork.Repository<Rooms>().AddAsync(roomAdd);
             await _unitOfWork.SaveChangesAsync();
            response.MessageCode = 200;
            response.MessageReturn = "Thêm thành công";
            return response;
        }

        public async Task<ReturnReponse> DeleteRoom(int id)
        {
            var reponse = new ReturnReponse();
            await _unitOfWork.Repository<Rooms>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            reponse.MessageCode = 200;
            reponse.MessageReturn = "Xóa Phòng thành công";
            return reponse;
        }

        public Task<Rooms> GetRoomAsync(int roomId)
        {
            var room = _unitOfWork.Repository<Rooms>().GetByIdAsync(roomId);
            return room;
        }

        public async Task<List<Rooms>> GetRoomsAsync()
        {
            var list = new List<Rooms>();
            list = (List<Rooms>) await _unitOfWork.Repository<Rooms>().GetAllAsync();
            return list;
        }

        public async Task<List<Rooms>> GetRoomsAsyncByKeyWord(int keyword)
        {
            var list = new List<Rooms>();
            list = (List<Rooms>)  _unitOfWork.Repository<Rooms>().GetAllAsync().Result.Where(x=>x.idRoomType==keyword).ToList();
            return list;
        }

        public async Task<ReturnReponse> UpdateRoom(int id, RoomDTO roomDTO)
        {
            var reponse = new ReturnReponse();
            var room = await _unitOfWork.Repository<Rooms>().GetByIdAsync(id);
            room.NumberRoom = roomDTO.numberRoom;
            room.idRoomType = roomDTO.idroomType;
            room.IsAvailable = roomDTO.isAvailable;
         
            await _unitOfWork.Repository<Rooms>().UpdateAsync(room);
            reponse.MessageCode = 200;
            reponse.MessageReturn = "Cập nhật phòng thành công";
            return reponse;
        }
    }
}
