using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTest.ViewModels
{
    public class FlightViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "پر کردن این فیلد اجباریست")]
        public int fromCityId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "پر کردن این فیلد اجباریست")]
        public int toCityId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "پر کردن این فیلد اجباریست")]
        public int airlineId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "پر کردن این فیلد اجباریست")]
        public int airplaneId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "پر کردن این فیلد اجباریست")]
        public DateTime departureTime { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "پر کردن این فیلد اجباریست")]
        public DateTime arrivalTime { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "پر کردن این فیلد اجباریست")]
        public int classTypeId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "پر کردن این فیلد اجباریست")]
        public string flightNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "پر کردن این فیلد اجباریست")]
        [Range(0,50,ErrorMessage ="این مقدار برای بار مجاز نیست")]
        public int allowCargoWeight { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "پر کردن این فیلد اجباریست")]
        [Range(0,300,ErrorMessage ="این مقدار برای ظرفیت مجاز نیست")]
        public int capacity { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "پر کردن این فیلد اجباریست")]
        [Range(0,999999999,ErrorMessage ="قیمت غیر مجاز")]
        public int price { get; set; }
        [Range(1,10,ErrorMessage ="تعداد مسافر قابل قبول نیست")]
        public int count { get; set; }
    }
}
