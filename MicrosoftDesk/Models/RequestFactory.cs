using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MicrosoftHelpDesk.Models
{
    public class RequestFactory
    {
        public static List<Request> requestData { get; set; }

        static RequestFactory()
        {
            requestData = new List<Request>{
                new Request{
                    Id = "1",
                    Name = "Doruk Korkmaz",
                    Priority = "Normal",
                    Location = "1st Floor",
                    Sublocation = "Mutfak",
                    Item = "Coffee Machine",
                    AccessiblePhone = "00905387675109",
                    Subject = "Kahve makinesi çalışmıyor",
                    Description = "Bu sabah mutfaktaki kahve makinesi kahve vermedi.",
                    },
                new Request{
                    Id = "2",
                    Name = "İbrahim Kıvanç",
                    Priority = "Emergency",
                    Location = "3rd Floor",
                    Sublocation = "Salon",
                    Item = "Lenovo Laptop",
                    AccessiblePhone = "00905306729060",
                    Subject = "Laptopum çalışmıyor",
                    Description = "Bilgisayarımı açmaya kalktığımda bios ekranında kalıyor.",
                    }
};
        }
    }
}
