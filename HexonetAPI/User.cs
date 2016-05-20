using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexonetAPI
{
    public class User
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            this.Uri = new Uri("https://coreapi.1api.net/api/call.cgi");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        public User(string entity, string userName, string password)
            : this()
        {
            this.Entity = entity;
            this.Username = userName;
            this.Password = password;
        }

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        public Uri Uri 
        { 
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public string Entity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get;
            set;
        }
    }
}
