using SubKuchV2.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.DTO_s
{
    public class DashboardDto
    {
        public List<TodayOrderDto> Todayorder { get; set; }
        public List<TodayRiderRequest> TodayRiderRequesT { get; set; }
        public string CompletedOrders { get; set; }
        public string RejectedOrders { get; set; }
        public string CancelOrders { get; set; }
        public string PickedOrders { get; set; }
        public string TodaySale { get; set; }
        public string TodayProfit { get; set; }
        public string Total_CompletedOrders { get; set; }
        public string Total_RejectedOrders { get; set; }
        public string Total_CancelOrders { get; set; }
        public string Total_PickedOrders { get; set; }
        public string AllTotalSale { get; set; }
        public string OLDsALE { get; set; }
        public string AllTotalProfit { get; set; }
        public string NotClearOrders_ByRider { get; set; }
        public string TodayVendersAmount { get; set; }

        public string TotalVendersAmount { get; set; }
    }
}
