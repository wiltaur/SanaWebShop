namespace SanaWebShop.Api.Business.Implements
{
    public interface IActionsCustomersBusiness
    {
        Task<int> Create(string name);
    }
}