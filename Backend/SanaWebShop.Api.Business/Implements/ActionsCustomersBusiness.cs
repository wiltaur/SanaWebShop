using Microsoft.Extensions.Configuration;
using SanaWebShop.Api.Models.Entities;
using SanaWebShop.Api.Repository.Interfaces;
using System.Runtime.ExceptionServices;

namespace SanaWebShop.Api.Business.Implements
{
    public class ActionsCustomersBusiness : IActionsCustomersBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActionsCustomersBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Create(string name)
        {
            int result = 0;
            try
            {
                Customer cust = new()
                {
                    Name = name.Trim().ToUpper()
                };
                _unitOfWork.Customers.Add(cust);
                await _unitOfWork.Customers.Save();
                result = cust.Id;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            return result;
        }
    }
}