namespace Data_Access.Interfaces
{
    public interface IPersonService
    {
        /// <summary>
        /// Gets the Person asociated with the given hash
        /// </summary>
        /// <param name="hash">Hash identifing the User</param>
        /// <returns>Found Person or null if the person doesnt exist</returns>
        public Task<Person> GetPersonByHash(string hash);

        /// <summary>
        /// Checks if the User has a state other than 0
        /// </summary>
        /// <param name="hash">Hash identifing the User</param>
        /// <returns>true when the state is 0, false when the state is other than 0</returns>
        public Task<bool> IsPersonAllowed(string hash);

        /// <summary>
        /// Set the State of a User to 1 ( Coming )
        /// </summary>
        /// <param name="hash">Hash identifing the User</param>
        /// <returns>1 if succeded, -1 if user was not found</returns>
        public Task<int> SetPersonToComing(string hash);

        /// <summary>
        /// Set the State of a User to 2 ( Maybe Coming )
        /// </summary>
        /// <param name="hash">Hash identifing the User</param>
        /// <returns>1 if succeded, -1 if user was not found</returns>
        public Task<int> SetPersonToMaybe(string hash);

        /// <summary>
        /// Set the State of a User to 3 ( Not Coming )
        /// </summary>
        /// <param name="hash">Hash identifing the User</param>
        /// <returns>1 if succeded, -1 if user was not found</returns>
        public Task<int> SetPersonToDeclined(string hash);

        /// <summary>
        /// Gets: Total Invites / Remaining Invites / Coming / Maybe Coming / Not Coming
        /// </summary>
        /// <returns>List of Report Amounts</returns>
        public Task<List<int>> GetAllReports();
    }
}