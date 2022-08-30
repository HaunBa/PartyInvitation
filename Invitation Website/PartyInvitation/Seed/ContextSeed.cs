namespace PartyInvitation.Seed
{
    public class ContextSeed
    {
        public static void SeedUser(InvitationToolContext _db)
        {
            if (!_db.People.Any(x => x.PersonName == "Bastian"))
            {
                var user = new Person();

                user.PersonName = "Bastian";
                user.PersonNameHashed = user.PersonName.ToSha256();
                user.RequestRequests = new List<Request>();
                user.PersonRequestState = 0;

                _db.People.Add(user);
                _db.SaveChanges();
            }
        }
    }
}
