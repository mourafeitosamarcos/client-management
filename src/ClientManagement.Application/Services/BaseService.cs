using ClientManagement.Application.Interfaces;

namespace ClientManagement.Application.Services
{
    public class BaseService
    {
        public readonly IUnitOfWork _unitOfWork;
        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
