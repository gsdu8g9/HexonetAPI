using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace HexonetAPI
{
    public class Connection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        public Connection()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="params">The @params.</param>
        public Connection(User user)
            : this(user.Uri, user.Entity, user.Username, user.Password)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="URL">The URL.</param>
        /// <param name="Entity">The entity.</param>
        /// <param name="Login">The login.</param>
        /// <param name="Password">The password.</param>
        public Connection(Uri url, string entity, string userName, string password)
            : this()
        {
            this.Uri = url;
            this.Entity = entity;
            this.Username = userName;
            this.Password = password;
        }

        /// <summary>
        /// Requests the raw data.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public string RequestRawData(Command command)
        {
            string sCommand = "";
            string postData = "";

            foreach (var key in command.Keys)
            {
                string value;
                if (command.TryGetValue(key, out value))
                {
                    sCommand += string.Format("{0}={1}\n", key, value);
                }
            }

            postData += string.Format("s_entity={0}&", this.Entity);
            postData += string.Format("s_login={0}&", this.Username);
            postData += string.Format("s_pw={0}&", this.Password);
            postData += "s_command=" + sCommand;

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.Uri);
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentLength = byteArray.Length;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            Stream dataStream = httpWebRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
          
            using (WebResponse response = httpWebRequest.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Requests the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public Response Request(Command command)
        {
            string request = RequestRawData(command);
            return new Response(request);
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
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public Uri Uri
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
        internal string Password
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public string Role
        {
            get;
            set;
        }

    }
}
