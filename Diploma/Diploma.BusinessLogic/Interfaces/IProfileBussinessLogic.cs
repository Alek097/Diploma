using Diploma.Core;
using System.Threading.Tasks;
using Diploma.Core.ViewModels;

namespace Diploma.BusinessLogic.Interfaces
{
    public interface IProfileBussinessLogic
    {
        Task<ControllerResult> SendConfirmEditEmail(string userName, string newEmail);
        Task<ControllerResult<string>> EditEmail(string code, string newEmail, string userName);
        Task<ControllerResult<AddressViewModel>> AddAddress(AddressViewModel address, string userName);
        Task<ControllerResult> DeleteAddress(string id, string name);
        Task<ControllerResult<AddressViewModel>> EditAddress(string name, AddressViewModel address);
    }
}
