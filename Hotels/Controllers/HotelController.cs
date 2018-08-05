using Hotels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hotels.Controllers
{
    public class HotelController : ApiController
    {
        private static List<Hotel> _hotelList = new List<Hotel>()
        {
        new Hotel(){ Id = 1 , Name = "Hayatt Regency" , NumberOfAvailableRooms = 2 , Address = "Vimaan Nagar,Pune" , LocationCode = "PNQ"},
        new Hotel(){ Id = 2 , Name = "Novotel" , NumberOfAvailableRooms = 4 , Address = "Vimaan Nagar,Pune" , LocationCode = "PNQ"},
        new Hotel(){ Id = 3 , Name = "Royal Park 22" , NumberOfAvailableRooms = 1 , Address = "Sector 22,Chandigarh" , LocationCode = "CHD"},
        new Hotel(){ Id = 4 , Name = "Hotel Prime Sage Saket" , NumberOfAvailableRooms = 5 , Address = "Malviya Nagar, New Delhi" , LocationCode = "DEL"},
        };


        public ApiResponse CreateHotel(Hotel hotel)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                if (hotel != null)
                {
                    _hotelList.Add(hotel);
                    apiResponse.Hotels = _hotelList;
                    return new ApiResponse
                    {
                        Status = new Status()
                        {
                            ApiStatus = ApiStatus.Successful,
                            ErrorCode = 200,
                            ErrorMessage = "Hotel Successfully Added!"
                        }
                    };
                }
                else
                {
                    apiResponse.Hotels = null;
                    return new ApiResponse
                    {
                        Status = new Status()
                        {
                            ApiStatus = ApiStatus.Failed,
                            ErrorCode = 422,
                            ErrorMessage = "Invalid Data Sent"
                        }
                    };
                }
            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    Hotels = null,
                    Status = new Status
                    {
                        ApiStatus = ApiStatus.Failed,
                        ErrorCode = 500,
                        ErrorMessage = "Exception Occurred :" + exc.Message
                    }
                };
            }
            
        }

        public ApiResponse GetAllHotels()
        {
            try
            {
                return new ApiResponse()
                {
                    Hotels = _hotelList,
                    Status = new Status()
                    {
                        ApiStatus = ApiStatus.Successful,
                        ErrorCode = 200,
                        ErrorMessage = "OK"
                    }

                };
            }
            catch (Exception exc)
            {
                return new ApiResponse()
                {
                    Hotels = null,
                    Status = new Status()
                    {
                        ApiStatus = ApiStatus.Failed,
                        ErrorCode = 500,
                        ErrorMessage = "Internal Server Error"
                    }
                };
             }
        }
        public ApiResponse GetHotelById (int id)
        {
            var requiredHotel = _hotelList.Find(x => x.Id == id);
            if(requiredHotel == null)
            {
                return new ApiResponse()
                {
                    Hotels = null,
                    Status = new Status()
                    {
                        ApiStatus = ApiStatus.Failed,
                        ErrorCode = 404,
                        ErrorMessage = "Not Found"
                    }

                };

            }
            return new ApiResponse()
            {
                Hotels = new List<Hotel>() { requiredHotel },
                Status = new Status()
                {
                    ApiStatus = ApiStatus.Successful,
                    ErrorCode = 500,
                    ErrorMessage = "OK"
                }


            };
        }

        [HttpDelete]
        public ApiResponse RemoveHotelById(int Id)
        {
            try
            {
                var hotelToBeDeleted = _hotelList.Find(x => x.Id == Id);
                if (hotelToBeDeleted != null)
                {
                    _hotelList.Remove(hotelToBeDeleted);
                    return new ApiResponse
                    {
                        Hotels = _hotelList,
                        Status = new Status()
                        {
                            ApiStatus = ApiStatus.Successful,
                            ErrorCode = 200,
                           ErrorMessage = "Hotel Successfully Deleted"
                        }
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        Hotels = _hotelList,
                        Status = new Status()
                        {
                            ApiStatus = ApiStatus.Failed,
                            ErrorCode = 404,
                            ErrorMessage = "Hotel Not Found!"
                        }
                    };
                }
            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    Hotels = null,
                    Status = new Status
                    {
                        ApiStatus = ApiStatus.Failed,
                        ErrorCode = 500,
                        ErrorMessage = "Exception Occurred :" + exc.Message
                    }
                };
            }
        }


        [HttpPut]
        public ApiResponse BookHotelById(int Id, [FromBody] int NumberOfRoomsToBeBooked)
        {
            try
            {
                var hotelToBeBooked = _hotelList.Find(x => x.Id == Id);
                if (hotelToBeBooked != null && NumberOfRoomsToBeBooked > 0)
                {

                    if (hotelToBeBooked.NumberOfAvailableRooms >= NumberOfRoomsToBeBooked)
                    {
                        hotelToBeBooked.NumberOfAvailableRooms -= NumberOfRoomsToBeBooked;
                        return new ApiResponse
                        {
                            Hotels = _hotelList,
                            Status = new Status()
                            {
                                ApiStatus = ApiStatus.Successful,
                                ErrorCode = 200,
                                ErrorMessage = "Room Booked Successfully"
                            }
                        };

                    }
                    else
                    {
                        return new ApiResponse
                        {
                            Hotels = null,
                            Status = new Status()
                            {
                                ApiStatus = ApiStatus.Failed,
                                ErrorCode = 404,
                                ErrorMessage = "Rooms Not Available"
                            }
                        };
                    }
                }
                else
                {
                    return new ApiResponse
                    {
                        Hotels = null,
                        Status = new Status()
                        {
                            ApiStatus = ApiStatus.Failed,
                            ErrorCode = 404,
                            ErrorMessage = "Invalid Data Sent"
                        }
                    };
                }

            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    Hotels = null,
                    Status = new Status()
                    {
                        ApiStatus = ApiStatus.Failed,
                        ErrorCode = 500,
                        ErrorMessage = "Exception Occurred :" + exc.Message
                    }
                };
            }
        }

    }
}

