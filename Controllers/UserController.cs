using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ONeilSoft.RSSQLAPI;
using ONeilSoft.RSSQLAPI.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RSSQLRestAPICore.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private static IEnumerable<User> SqlQuery(string query, string server, string usercode, string param1 )
        {
            try
            {
                // Plain Text Credentials :D :D
                string ConnectionString = @"Data Source=srrssql01;Initial Catalog=RSSQLDB_LAB;User ID=RSSQLAPI;Password=Grace123$;";
                using var connection = new SqlConnection(ConnectionString);

                //await connection.OpenAsync()
                try
                {
                    var results = connection.Query<User>(query, new { Param1 = param1 });
                    return results;
                }
                catch
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        // GET: /<UserController>
        [HttpGet]
        public IEnumerable<User> GetUser()
        {

            string query = @" SELECT u.UserCode,u.FirstName,u.LastName, u.AddressEmail as email,CASE WHEN u.UserCode = f.UserCode THEN null else f.UserCode END as functionModel,s.ItemSecurityCode, t.UserTypeDesc as UserType, CAST(u.UserStatusID as bit) UserActive, u.UserID as ID
	                        FROM RSUSER u
	                        LEFT JOIN RSUSER f ON u.FunctionModelID = f.UserID
	                        LEFT JOIN RSSECURITY s ON u.ItemSecurityID = s.ItemSecurityID
	                        LEFT JOIN RSUSERTYPE t ON t.UserTypeID = u.UserTypeID
                            WHERE u.AddressEmail = ISNULL(@Param1,u.AddressEmail) AND u.LastName = 'SSO' ";

            var result = SqlQuery(query, "blah", "blah", "mcho@grace.com.au");
            return result;
        }

        // GET /<UserController>/email
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound,Type = typeof(User))]
        [HttpGet("{email}")]
        public ActionResult GetUser(string email)
        {

            string query = @" SELECT u.UserCode,u.FirstName,u.LastName, u.AddressEmail as email,CASE WHEN u.UserCode = f.UserCode THEN null else f.UserCode END as functionModel,s.ItemSecurityCode, t.UserTypeDesc as UserType, CAST(u.UserStatusID as bit) UserActive, u.UserID as ID
	                        FROM RSUSER u
	                        LEFT JOIN RSUSER f ON u.FunctionModelID = f.UserID
	                        LEFT JOIN RSSECURITY s ON u.ItemSecurityID = s.ItemSecurityID
	                        LEFT JOIN RSUSERTYPE t ON t.UserTypeID = u.UserTypeID
                            WHERE u.AddressEmail = ISNULL(@Param1,u.AddressEmail) AND u.LastName = 'SSO' ";

            var result = SqlQuery(query, "blah", "blah", email);
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST /<UserController>
        [HttpPost]
        public ActionResult CreateUser([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user?.Email))
            {
                return BadRequest();
            }
            
            var client = APIClient.Login("RHO-MC", "------", Guid.Parse("1F28BE82-7A38-4FDC-AF88-3816AFB1BB1E"));
            user.UserCode = user.AccountCode + "-" + user.FirstName.Substring(0, 1) + user.LastName.Substring(0, 1);
            user.Password = "Grace123$";//System.Web.Security.Membership.GeneratePassword(30, 5);

            var webUser = new RSUSER(true)
            {
                Code = user.UserCode,
                //model.AccountCode + "-" + model.FirstName.Substring(0, 1) 
                TypeID = UserTypeValues.WebUser,
                LastName = "SSO", //+ user.Email.Substring(0,Math.Min(26,user.Email.Length)) ,   //SS0-mcho@grace.com.au
                FirstName = user.FirstName + ' ' + user.LastName, //Michael Cho
                ItemSecurityID = 1,                                 //Lowest Security
                // You can optionally set the function access model user.
                // The referenced user must be a "Model" user
                FunctionModelID = 1007,
                EmailAddress = user.Email,
                PasswordChangeRequired = true
            };

            try
            {
                client.UserInsert(webUser, user.Password);
                return CreatedAtAction(nameof(GetUser), new { email = webUser.EmailAddress }, webUser);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT /<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User User)
        {
        }

        // DELETE /<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(User User)
        {
        }
    }
}
