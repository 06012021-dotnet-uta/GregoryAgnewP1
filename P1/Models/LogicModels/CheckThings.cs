using System.Linq;
using DatabaseModels;

namespace LogicModels
{
    /// <summary>
    /// Contains methods to check things in the database such as if a user already has an account or what their password is.
    /// </summary>
    public class CheckThings : ICheckThings
    {
        private readonly Context _context;

        public CheckThings(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Checks if the user already has an account in the database.
        /// </summary>
        /// <param name="email">Takes user input for email as string.</param>
        /// <returns>Returns true if user has an account</returns>
        public bool CheckUser(string email)
        {

            if (_context.Users.Where(x => (x.Email.ToUpper() == email.ToUpper())).ToList().Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the user's password in the database.
        /// </summary>
        /// <param name="password">Takes the user's input as a string.</param>
        /// <returns>Returns true if password matches database entry.</returns>
        public bool CheckPassword(string password)
        {

            if (_context.Users.Where(x => (x.Password == password)).ToList().Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
