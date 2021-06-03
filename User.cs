using System;

namespace RSSQLRestAPICore
{
    public class User
    {
        /// <summary>   
        /// RSSQL User Code
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>   
        /// Account Code Prefix for Non-SSO Accounts
        /// </summary>
        public string AccountCode { get; set; }

        /// <summary>   
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>   
        /// First Name or Full Name for SSO Accounts
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>   
        /// Last name or SSO tag
        /// </summary>
        public string LastName { get; set; }

        /// <summary>   
        /// Email Address or SSO UPN
        /// </summary>
        public string Email { get; set; }

        /// <summary>   
        /// Function Access ID
        /// </summary>
        public string FunctionModel { get; set; }

        /// <summary>   
        /// Item Security (0-99)
        /// </summary>
        public string ItemSecurityCode { get; set; }

        /// <summary>   
        /// User Type
        /// </summary>
        public string UserType { get; set; }

        /// <summary>   
        /// User Status
        /// </summary>
        public bool? UserActive { get; set; }

        /// <summary>   
        /// GUID
        /// </summary>
        public int Id { get; set; }
    }
}
