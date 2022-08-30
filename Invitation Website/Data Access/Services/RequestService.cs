namespace Data_Access.Services
{
    public class RequestService : IRequestService
    {
        private readonly InvitationToolContext _db;
        private readonly IPersonService _personService;

        public RequestService(InvitationToolContext db, IPersonService personService)
        {
            _db = db;
            _personService = personService;
        }

        public async Task<int> AddRequestToPerson(Request request, string hash)
        {
            var person = await _personService.GetPersonByHash(hash);
            if (person == null) return -1;
            person.RequestRequests.Add(request);
            _db.Update(person);
            _db.SaveChanges();
            return 0;
        }

        public async Task<List<Request>> GetAllRequests()
        {
            return await _db.Requests.ToListAsync();
        }
    }
}
