using System;
using System.Collections.Generic;

namespace HexonetAPI.INI
{
    class SectionNotFoundException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionNotFoundException"/> class.
        /// </summary>
        /// <param name="SectionName">Name of the section.</param>
        public SectionNotFoundException(string SectionName)
            : base("The section '" + SectionName + "' was not found.")
        {
        }
    }
}
