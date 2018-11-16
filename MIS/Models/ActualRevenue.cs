using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models
{
    public class ActualRevenue
    {

        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime WeekEndDate { get; set; }
        public Branch Branch { get; set; }
        public byte BranchId { get; set; }
        public DateTime CraetedDate { get; set; }
        public int CreatedBy { get; set; }
    }
}