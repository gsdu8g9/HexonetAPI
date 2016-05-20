using System;
using System.Collections.Generic;

namespace HexonetAPI.INI
{
    class KeyNotFoundException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyNotFoundException"/> class.
        /// </summary>
        /// <param name="KeyName">Name of the key.</param>
        public KeyNotFoundException(string KeyName)
            : base("The key '" + KeyName + "' was not found.")
        {
        }
    }
}
