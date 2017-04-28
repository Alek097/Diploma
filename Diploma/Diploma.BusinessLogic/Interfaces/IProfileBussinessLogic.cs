using Diploma.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Diploma.BusinessLogic.Interfaces
{
    public interface IProfileBussinessLogic
    {
        Task<ControllerResult> SendConfirmEditEmail(string userName, string newEmail);
        Task<ControllerResult<string>> EditEmail(string code, string newEmail, string userName);
    }
}
