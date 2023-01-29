using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileArts.api.Domain.Services
{
    public interface IGenerateCodeService
    {
        public string generatCode(int KeyChars);
    }
}
