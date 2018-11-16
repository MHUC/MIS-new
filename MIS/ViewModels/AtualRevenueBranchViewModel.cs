using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MIS.Models;

namespace MIS.ViewModels
{
    public class AtualRevenueBranchViewModel
    {
        public ActualRevenue Actual { get; set; }
        public IEnumerable<Branch> Branches { get; set; }

        public IEnumerable<ActualRevenue> ActualRevenue { get; set; }
    }
}