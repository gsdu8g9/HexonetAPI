using System;
using System.Collections.Generic;

namespace HexonetAPI.INI
{
    class INIFile
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="INIFile"/> class.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        public INIFile(string Contents)
            : base()
        {
            this.Contents = Contents;
        }

        public char SectionOpenChar = '[';
        public char SectionCloseChar = ']';
        public char NameValueDelimChar = '=';
        public char CommentChar = ';';
        public System.IO.FileInfo File;
        public List<Section> Sections = new List<Section>();


        #region "FindSection"

        /// <summary>
        /// Finds the first occurance of the specified <see cref="Section">Section</see>.
        /// </summary>
        /// <param name="SectionName"></param>
        /// <returns>The first matching <see cref="Section">Section</see>.</returns>
        /// <remarks>Matching is case-insensitive and is based on the <see cref="Section.name">name</see> property. If multiple matching <see cref="Section">Section</see>s exist, only the first occurance is returned.</remarks>
        /// <exception cref="SectionNotFoundException">Thrown if the specified <see cref="Section">Section</see> was not found.</exception>
        public Section FindSection(string SectionName)
        {
            foreach (Section oSection in Sections)
            {
                if (oSection.Name == null)
                {
                    if (SectionName == null) return oSection;
                }
                else
                {
                    if (oSection.Name.ToLower() == SectionName.ToLower()) return oSection;
                }
            }

            throw new SectionNotFoundException(SectionName);
        }

        #endregion

        #region "FindKey"
        /// <summary>
        /// Finds the first occurance of the specified <see cref="Key">Key</see> in only the first occurance of the specified <see cref="Section">Section</see>.
        /// </summary>
        /// <param name="SectionName">The <see cref="Section">Section</see> the <see cref="Key">Key</see> should be searched under.</param>
        /// <param name="KeyName">The <see cref="Key">Key</see> to search for.</param>
        /// <returns>The first matching <see cref="Key">Key</see>.</returns>
        /// <remarks>Matching is case-insensitive and is based on the section's <see cref="Section.name">name</see> and the key's<see cref="Key.name"> name</see> property. If multiple matching <see cref="Section">Section</see>s exist, only the first occurance is searched. If multiple matching <see cref="Key">Key</see>s exist, only the first occurance is returned.</remarks>
        /// <exception cref="SectionNotFoundException">Thrown if the specified <see cref="Section">Section</see> was not found.</exception>
        /// <exception cref="KeyNotFoundException">Thrown is the specified <see cref="Key">Key</see> was not found in the first matching <see cref="Section">Section</see>.</exception>
        public Key FindKey(string SectionName, string KeyName)
        {
            Section oSection = FindSection(SectionName);

            if (oSection != null)
            {
                return oSection.FindKey(KeyName);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region "GetValue"
        /// <summary>
        /// Returns the <see cref="Key.Value">value</see> of a <see cref="key">Key</see> from a specified file.
        /// </summary>
        /// <param name="SectionName">The <see cref="Section">Section</see> the <see cref="key">Key</see> should be searched under.</param>
        /// <param name="KeyName">The <see cref="key">Key</see> to return the <see cref="Key.Value">value</see> for.</param>
        /// <returns>The key's value.</returns>
        /// <remarks>If multiple matching <see cref="Section">Section</see>s exist, only the first occurance is searched. If multiple matching <see cref="key">Key</see>s exist, only the first occurance is returned.</remarks>
        /// <exception cref="SectionNotFoundException">Thrown if the specified <see cref="Section">Section</see> was not found.</exception>
        /// <exception cref="KeyNotFoundException">Thrown is the specified <see cref="key">Key</see> was not found in the first matching <see cref="Section">Section</see>.</exception>
        public string GetValue(string SectionName, string KeyName)
        {
            Key oKey = FindKey(SectionName, KeyName);

            if (oKey != null)
            {
                return oKey.Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the <see cref="Key.Value">value</see> of a <see cref="key">Key</see>.
        /// </summary>
        /// <param name="SectionName">The <see cref="Section">Section</see> the <see cref="key">Key</see> should be searched under.</param>
        /// <param name="KeyName">The <see cref="key">Key</see> to return the <see cref="Key.Value">value</see> for.</param>
        /// <param name="FileName">The file to return the <see cref="key">Key</see>'s <see cref="Key.Value">value</see> from.</param>
        /// <returns>The <see cref="key">Key</see>'s <see cref="Key.Value">value</see>.</returns>
        /// <remarks>If multiple matching <see cref="Section">Section</see>s exist, only the first occurance is searched. If multiple matching <see cref="key">Key</see>s exist, only the first occurance is returned.</remarks>
        /// <exception cref="SectionNotFoundException">Thrown if the specified <see cref="Section">Section</see> was not found.</exception>
        /// <exception cref="KeyNotFoundException">Thrown is the specified <see cref="key">Key</see> was not found in the first matching <see cref="Section">Section</see>.</exception>
        public static string GetValue(string SectionName, string KeyName, string FileName)
        {
            INIFile INI = new INIFile(FileName);

            return INI.GetValue(SectionName, KeyName);
        }
        #endregion


        #region "Contents"
        /// <summary>
        /// A <see cref="string">String</see> that contains the current INI file.
        /// </summary>
        /// <remarks>This property processes the contents of the file upon each call. To increas performance, only call this property when needed and buffer cache the contents otherwise.</remarks>
        public string Contents
        {
            get
            {
                System.Text.StringBuilder sbBuffer = new System.Text.StringBuilder();

                foreach (Section oSection in Sections)
                {
                    if (oSection.Name != null)
                    {
                        sbBuffer.AppendLine(SectionOpenChar + oSection.Name + SectionCloseChar);
                    }

                    foreach (Key oKey in oSection.Keys)
                    {
                        if (oKey.IsComment)
                        {
                            sbBuffer.AppendLine(CommentChar + oKey.Value);
                        }
                        else
                        {
                            sbBuffer.AppendLine(oKey.Name + NameValueDelimChar + oKey.Value);
                        }
                    }
                    sbBuffer.AppendLine();
                }
                return sbBuffer.ToString();
            }
            set
            {
                //clear out all the sections first
                Sections.Clear();

                if (string.IsNullOrEmpty(value)) return;

                using (System.IO.StringReader srBuffer = new System.IO.StringReader(value))
                {
                    string sLine = srBuffer.ReadLine().Trim();
                    string sTrimmedLine = sLine.Trim();

                    Section oCurrentSection = null;

                    do
                    {
                        sTrimmedLine = sLine.Trim();

                        if (sTrimmedLine.Length > 0)
                        {
                            switch (sTrimmedLine.Substring(0, 1))
                            {
                                case "[":
                                    if (sTrimmedLine.Contains(SectionCloseChar.ToString()))
                                    {
                                        oCurrentSection = new Section(sTrimmedLine.Substring(1, sTrimmedLine.IndexOf(SectionCloseChar) - 1));
                                    }
                                    else
                                    {
                                        oCurrentSection = new Section(sTrimmedLine.Substring(1, sTrimmedLine.Length - 1));
                                    }
                                    this.Sections.Add(oCurrentSection);
                                    break;
                                case "]":
                                    if (oCurrentSection == null)
                                    {
                                        oCurrentSection = new Section();
                                        this.Sections.Add(oCurrentSection);
                                    }

                                    oCurrentSection.Keys.Add(new Key(sTrimmedLine.Substring(1)));
                                    break;
                                default:
                                    string sKeyName = null;
                                    string sKeyValue = null;

                                    if (sTrimmedLine.Contains(NameValueDelimChar.ToString()))
                                    {
                                        sKeyName = sTrimmedLine.Substring(0, sTrimmedLine.IndexOf(NameValueDelimChar));
                                        sKeyValue = sTrimmedLine.Substring(sKeyName.Length + 1);
                                    }
                                    else
                                    {
                                        sKeyName = sTrimmedLine;
                                        sKeyValue = null;
                                    }

                                    if (oCurrentSection == null)
                                    {
                                        oCurrentSection = new Section(null);
                                        this.Sections.Add(oCurrentSection);
                                    }

                                    oCurrentSection.Keys.Add(new Key(sKeyName, sKeyValue));
                                    break;
                            }
                        }

                        sLine = srBuffer.ReadLine();
                    }
                    while (!(sLine == null));

                    srBuffer.Close();
                }
            }
        }

        #endregion

    }
}
