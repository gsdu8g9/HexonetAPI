using System;
using System.Collections.Generic;

namespace HexonetAPI.INI
{

    #region "Class Section"
    
    /// <summary>
    /// Contains an individual section from an INI file.
    /// </summary>
    class Section
    {

        #region "Constructor"
        /// <summary>
        /// Initializes a new instance of the <see cref="Section">Section</see> class.
        /// </summary>
        public Section()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Section">Section</see> class with a specified name.
        /// </summary>
        /// <param name="SectionName">The name of the section.</param>
        public Section(string SectionName)
            : base()
        {
            this.Name = SectionName;
        }
        #endregion

        /// <summary>
        /// A <see cref="List(Of Key)">List(Of Key)</see> collection containing the keys of the section.
        /// </summary>
        public List<Key> Keys = new List<Key>();

        /// <summary>
        /// The name of the section.
        /// </summary>
        /// <remarks>A value of <c>Nothing</c> indicates a set of keys and comments at the top of the file before the first section.</remarks>
        public string Name;

        #region "FindKey"
        /// <summary>
        /// Finds a key by name.
        /// </summary>
        /// <param name="KeyName"></param>
        /// <returns>A reference to a matching key class within this section.</returns>
        /// <remarks>Always returns only the first match.</remarks>
        /// <exception cref="KeyNotFoundException">Thrown is the specified key was not found in the first matching section.</exception>
        public Key FindKey(string KeyName)
        {
            
            foreach ( Key oKey in Keys) {
                if (oKey.Name != null) {
                    if (oKey.Name.ToLower() == KeyName.ToLower()) return oKey; 
                }
            }
            
            throw new KeyNotFoundException(KeyName);
        }
        #endregion
    }

    #endregion
}