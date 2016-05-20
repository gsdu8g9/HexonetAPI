using System;
using System.Collections.Generic;

namespace HexonetAPI.INI
{
    class Key
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Key"/> class.
        /// </summary>
        /// <param name="KeyName">Name of the key.</param>
        /// <param name="Value">The value.</param>
        public Key(string KeyName, string Value)
            : base()
        {

            this.Name = KeyName;
            this.Value = Value;
            IsComment = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Key"/> class.
        /// </summary>
        /// <param name="Comment">The comment.</param>
        public Key(string Comment)
            : base()
        {

            Value = Comment;
            IsComment = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Key"/> class.
        /// </summary>
        public Key()
            : base()
        {
        }

        public string Name;
        public string Value;
        public bool IsComment;

    }
}
