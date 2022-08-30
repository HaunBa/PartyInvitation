namespace Data_Access.Services
{
    public class PersonService : IPersonService
    {

        private readonly InvitationToolContext _db;

        public PersonService(InvitationToolContext invitationToolContext)
        {
            _db = invitationToolContext;
        }

        public async Task<Person> GetPersonByHash(string hash)
        {
            var person = await _db.People.FirstOrDefaultAsync(x => x.PersonNameHashed == hash);
            if (person == null) return null;
            return person;
        }

        public async Task<bool> IsPersonAllowed(string hash)
        {
            var person = await GetPersonByHash(hash);
            if(person == null) return false;
            if (person.PersonRequestState != 0) return false;
            return true;
        }

        public async Task<int> SetPersonToComing(string hash)
        {
            var person = await GetPersonByHash(hash);
            if (!await IsPersonAllowed(hash)) return -1;

            person.PersonRequestState = 1;
            _db.People.Update(person);
            await _db.SaveChangesAsync();

            return 1;
        }

        public async Task<int> SetPersonToMaybe(string hash)
        {
            var person = await GetPersonByHash(hash);
            if (!await IsPersonAllowed(hash)) return -1;

            person.PersonRequestState = 2;
            _db.People.Update(person);
            await _db.SaveChangesAsync();

            return 1;
        }

        public async Task<int> SetPersonToDeclined(string hash)
        {
            var person = await GetPersonByHash(hash);
            if (!await IsPersonAllowed(hash)) return -1;

            person.PersonRequestState = 3;
            _db.People.Update(person);
            await _db.SaveChangesAsync();

            return 1;
        }

        public async Task<List<int>> GetAllReports()
        {
            List<int> result = new();

            result.Add(_db.People.Count());

            var openReq = from p in _db.People
                          where p.PersonRequestState == 0
                          select p;

            var accepted = from p in _db.People
                           where p.PersonRequestState == 1
                           select p;

            var maybe = from p in _db.People
                       where p.PersonRequestState == 2
                       select p;

            var declined = from p in _db.People
                       where p.PersonRequestState == 3
                       select p;

            result.Add(openReq.Count());
            result.Add(accepted.Count());
            result.Add(maybe.Count());
            result.Add(declined.Count());

            return result;
        }
    }
}
