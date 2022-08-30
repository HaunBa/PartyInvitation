namespace Data_Access.Interfaces
{
    public interface IRequestService
    {
        public Task<List<Request>> GetAllRequests();
        public Task<int> AddRequestToPerson(Request request, string hash);
    }
}
