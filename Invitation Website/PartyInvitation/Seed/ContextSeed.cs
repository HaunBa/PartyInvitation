namespace PartyInvitation.Seed
{
    public class ContextSeed
    {
        public static void SeedUser(InvitationToolContext _db)
        {
            List<string> Names = new();

            Names.Add("Haunschmied Bastian");
            Names.Add("Grabmeir Marcel");
            Names.Add("Leutgöb Fabian");
            Names.Add("Märzinger Selina");
            Names.Add("Pointner Emelie");
            Names.Add("Müller Stefan");
            Names.Add("Tuzi Elena");

            foreach (var item in Names)
            {
                if (!_db.People.Any(x => x.PersonName == item))
                {
                    Person user = new();

                    user.PersonName = item;
                    user.PersonNameHashed = user.PersonName.ToSha256();
                    user.RequestRequests = new List<Request>();
                    user.PersonRequestState = 0;

                    _db.People.Add(user);
                    _db.SaveChanges();
                }
            }
        }

        public static void ClearAllDbData(InvitationToolContext _db)
        {
            foreach (var item in _db.Requests)
            {
                _db.Requests.Remove(item);
            }

            foreach (var item in _db.People)
            {
                _db.People.Remove(item);
            }

            _db.SaveChanges();
        }
    }
}
