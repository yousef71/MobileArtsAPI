using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileArts.api.Domain.Services
{
    public interface IMessageService
    {
        public string Send(string MobileNbr, string Message, string lang);
    }
}
