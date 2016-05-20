using System;
using System.Collections.Generic;

namespace HexonetAPI.INI
{
    class FileNotSpecifiedException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FileNotSpecifiedException"/> class.
        /// </summary>
        public FileNotSpecifiedException()
            : base("A file was not specified for reading or writing. The field 'File' is set to 'Nothing'.")
        {
        }
    }
}
