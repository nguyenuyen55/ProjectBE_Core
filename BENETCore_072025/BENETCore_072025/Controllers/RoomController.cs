using BENETCore_072025.DataAccess.DO;
using BENETCore_072025.DataAccess.DTO;
using BENETCore_072025.DataAccess.Services;
using BENETCore_072025.DataAccess.UnitOfWork;
using BENETCore_072025.Filter;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BENETCore_072025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        // GET: api/<RoomController>
        // POST api/<RoomController>
        private readonly IRoomService _roomService;
        private readonly IunitOfWork _unitOfWork;

        public RoomController(IRoomService roomService, IunitOfWork unitOfWork)
        {
            _roomService = roomService;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("getRoom")]
        public async Task<IActionResult> GetRoom(int id)
        {
            var reponse = new ReturnReponse();
            try
            {
                var room = await _roomService.GetRoomAsync(id);
                if (room == null)
                {
                    reponse.MessageCode = 404;
                    reponse.MessageReturn = "Không tìm thấy phòng";
                    return NotFound(reponse);
                }
                var RoomDTO = new RoomDTO()
                {
                    idroomType = room.idRoomType,
                    numberRoom = room.NumberRoom,
                    isAvailable = room.IsAvailable,
                };
                return Ok(RoomDTO);


            }
            catch (Exception ex)
            {
                reponse.MessageCode = 500;
                reponse.MessageReturn = "Lỗi dữ liệu " + ex.Message;
                return BadRequest(reponse);
            }

        }
        [HttpGet("searchByKeyTypeRoom")]
        public async Task<IActionResult> searchByKeyTypeRoom(int id)
        {
            var reponse = new ReturnReponse();
            try
            {
                var room = await _roomService.GetRoomsAsyncByKeyWord(id);

                if (room.Count > 0)
                {
                    var roomDTO = room.Select(x => new RoomDTO()
                    {
                        idroomType = x.idRoomType,
                        numberRoom = x.NumberRoom,
                        isAvailable = x.IsAvailable,
                    });
                    return Ok(roomDTO);
                }
                reponse.MessageCode = 404;
                reponse.MessageReturn = "Không dữ liệu nào được tìm thấy";
                return NotFound(reponse);

            }
            catch (Exception ex)
            {
                reponse.MessageCode = 500;
                reponse.MessageReturn = "Lỗi dữ liệu " + ex.Message;
                return BadRequest(reponse);
            }

        }
        [HttpGet("getAll")]
        [BeAuthorizeAttribute("GetAllRoom","VIEW")]
        public async Task<IActionResult> GetAllRoom()
        {
            var reponse = new ReturnReponse();
            try
            {
                var room = await _roomService.GetRoomsAsync();

                if (room.Count > 0)
                {
                    var roomDTO = room.Select(x => new RoomDTO()
                    {
                        idroomType = x.idRoomType,
                        numberRoom = x.NumberRoom,
                        isAvailable = x.IsAvailable,
                    });
                    return Ok(roomDTO);
                }
                reponse.MessageCode = 404;
                reponse.MessageReturn = "Không dữ liệu nào được tìm thấy";
                return NotFound(reponse);

            }
            catch (Exception ex)
            {
                reponse.MessageCode = 500;
                reponse.MessageReturn = "Lỗi dữ liệu " + ex.Message;
                return BadRequest(reponse);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoomDTO roomRequest)
        {

            var returnRespone = new ReturnReponse();

            try
            {
                var roomTypes = _unitOfWork.Repository<RoomTypes>().GetByIdAsync(roomRequest.idroomType).Result;
                if (roomTypes == null)
                {
                    returnRespone.MessageCode = 404;
                    returnRespone.MessageReturn = " không tìm thấy loại khách sạn";
                    return NotFound(returnRespone);
                }
                returnRespone = _roomService.AddRoom(roomRequest).Result;
                return Ok(returnRespone);
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi thêm dữ liệu " + ex.Message);
            }

        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(int id,[FromBody] RoomDTO roomRequest)
        {
            var returnRespone = new ReturnReponse();

            try
            { 
                var checkRoom= _unitOfWork.Repository<Rooms>().GetByIdAsync(id).Result;
                if (checkRoom == null)
                {
                    returnRespone.MessageCode = 404;
                    returnRespone.MessageReturn = " không tìm thấy phòng";
                    return NotFound(returnRespone);
                }
                var roomTypes = _unitOfWork.Repository<RoomTypes>().GetByIdAsync(roomRequest.idroomType).Result;
                if (roomTypes == null)
                {
                    returnRespone.MessageCode = 404;
                    returnRespone.MessageReturn = " không tìm thấy loại khách sạn";
                    return NotFound(returnRespone);
                }
                returnRespone = _roomService.UpdateRoom(id,roomRequest).Result;
                return Ok(returnRespone);
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi thêm dữ liệu " + ex.Message);
            }

        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var returnRespone = new ReturnReponse();

            try
            {
                var room = await _roomService.GetRoomAsync(id);
                if (room == null)
                {
                    returnRespone.MessageCode = 404;
                    returnRespone.MessageReturn = "Không tìm thấy phòng";
                    return NotFound(returnRespone);
                }
                await _roomService.DeleteRoom(id);
                returnRespone.MessageCode = 200;
                returnRespone.MessageReturn = "Xóa phòng thành công";
                return Ok(returnRespone);
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi xóa dữ liệu " + ex.Message);
            }

        }
    }
}
