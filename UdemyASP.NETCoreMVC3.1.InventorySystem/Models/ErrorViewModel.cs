using System;

namespace UdemyASP.NETCoreMVC3._1.InventorySystem.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
