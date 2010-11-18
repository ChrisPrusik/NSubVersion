/*******************************************************************************

  NSubversion (C) 2010 Krzysztof Arkadiusz Prusik
  SVN, CVS tags in Your application, by the reflection SvnIdAttribute
  Latest version: http://NSubversion.codeplex.com/

  $Id$

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

*******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace NSubversion
{
    /// <summary>
    /// Information about Svn Id tag and connected type with assembly
    /// </summary>
    /// <seealso cref="SvnIdAttribute"/>
    [SvnId("$Id$")]
    public class SvnTag
    {
        private string id = "";
        /// <summary>
        /// Gets the full id tag.
        /// </summary>
        /// <example>
        /// <code>$Id$</code>
        /// </example>
        /// <value>The id.</value>
        public string Id 
        { 
            get 
            {
                if (id.Length > 4 && id.Substring(0, 4) == "$Id:" && 
                    id.Substring(id.Length - 1) == "$" && 
                    id.Split(' ').Length == 7)
                    return id;
                else
                    return "";
            } 
        }

        /// <summary>
        /// Gets the name of the file from id tag.
        /// </summary>
        /// <example>
        /// <code>SvnTag.cs /* from $Id$ */</code>
        /// </example>
        /// <value>The name of the file.</value>
        public string FileName 
        { 
            get 
            {
                try
                {
                    return Id.Split(' ')[1];
                }
                catch
                {
                    return "";
                }
            } 
        }

        /// <summary>
        /// Gets the version number from id tag.
        /// </summary>
        /// <example>
        /// <code>49182 /* from $Id$ */</code>
        /// </example>
        /// <value>The version number.</value>
        public int VersionNumber
        {
            get
            {
                try
                {
                    return int.Parse(Id.Split(' ')[2]);
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the last modified from id tag.
        /// </summary>
        /// <example>
        /// <code>2010-07-21 17:11:44Z /* from $Id$ */</code>
        /// </example>
        /// <value>The last modified.</value>
        public DateTime LastModified
        {
            get
            {
                try
                {
                    return DateTime.Parse(Id.Split(' ')[3] + " " + 
                        Id.Split(' ')[4]);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
        }

        /// <summary>
        /// Gets the author from id tag.
        /// </summary>
        /// <example>
        /// <code>unknown /* from $Id$ */</code>
        /// </example>
        /// <value>The author.</value>
        public string Author
        {
            get
            {
                try
                {
                    return Id.Split(' ')[5];
                }
                catch
                {
                    return "";
                }
            }
        }

        private string assemblyFullName = "";
        /// <summary>
        /// Gets the full name of the assembly where is SvnIdAttribute.
        /// </summary>
        /// <example>
        /// <code>PKSoft.SubVersion, Version=1.0.3846.14773, Culture=neutral, PublicKeyToken=null</code>
        /// </example>
        /// <value>The full name of the assembly.</value>
        public string AssemblyFullName
        {
            get
            {
                return assemblyFullName;
            }
        }

        /// <summary>
        /// Gets the name of the assembly where is SvnIdAttribute.
        /// </summary>
        /// <example>
        /// <code>PKSoft.SubVersion /* from PKSoft.SubVersion, Version=1.0.3846.14773, Culture=neutral, PublicKeyToken=null */</code>
        /// </example>
        /// <value>The name of the assembly.</value>
        public string AssemblyName
        {
            get
            {
                try
                {
                    return FindBeetween(assemblyFullName, "", ",");
                }
                catch
                {
                    
                    return "";
                }
            }
        }

        /// <summary>
        /// Gets the assembly version where is SvnIdAttribute.
        /// </summary>
        /// <example>
        /// <code>1.0.3846.14773 /* from PKSoft.SubVersion, Version=1.0.3846.14773, Culture=neutral, PublicKeyToken=null */</code>
        /// </example>
        /// <value>The assembly version.</value>
        public Version AssemblyVersion
        {
            get
            {
                try
                {
                    return new Version(FindBeetween(assemblyFullName, 
                        "Version=", ","));
                }
                catch
                {

                    return new Version();
                }
            }
        }

        /// <summary>
        /// Gets the assembly culture where is SvnIdAttribute.
        /// </summary>
        /// <example>
        /// <code>neutral /* from PKSoft.SubVersion, Version=1.0.3846.14773, Culture=neutral, PublicKeyToken=null */</code>
        /// </example>
        /// <value>The assembly culture.</value>
        public string AssemblyCulture
        {
            get
            {
                try
                {
                    return FindBeetween(assemblyFullName, "Culture=", ",");
                }
                catch
                {

                    return "";
                }
            }
        }

        /// <summary>
        /// Gets the assembly public key token where is SvnIdAttribute.
        /// </summary>
        /// <example>
        /// <code>null /* from PKSoft.SubVersion, Version=1.0.3846.14773, Culture=neutral, PublicKeyToken=null */</code>
        /// </example>
        /// <value>The assembly public key token.</value>
        public string AssemblyPublicKeyToken
        {
            get
            {
                try
                {
                    return FindBeetween(assemblyFullName, 
                        "PublicKeyToken=", ",");
                }
                catch
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// Finds the the text beetween before and after (helper method).
        /// </summary>
        /// <param name="text">The searched text.</param>
        /// <param name="before">The text before.</param>
        /// <param name="after">The text after.</param>
        /// <returns>The text beetween before and after</returns>
        /// <example>
        /// <code>
        /// string s1 = FindBeetween("123456", "2", "4"); /* Result: 3 */
        /// string s2 = FindBeetween("123456", "", "4"); /* Result: 123 */
        /// string s3 = FindBeetween("123456", "2", ""); /* Result: 3456 */
        /// </code>
        /// </example>
        public string FindBeetween(string text, string before, string after)
        {
            int begin = 0;
            if (text != "")
            {
                if (before != "")
                {
                    begin = text.IndexOf(before, Math.Min(begin, 
                        text.Length - 1));
                    if (begin >= 0)
                        begin = begin + before.Length;
                    else
                        begin = text.Length;
                }
            }

            int end = text.Length;
            if (after != "")
            {
                end = text.IndexOf(after, Math.Min(begin, text.Length - 1));
                if (end < 0)
                    end = text.Length;
            }
            string result = text.Substring(begin, end - begin);
            begin += result.Length;
            return result;
        }

        private Type objectType = null;
        /// <summary>
        /// Gets the type connected with SvnIdAttribute.
        /// </summary>
        /// <value>The type.</value>
        public Type Type
        {
            get
            {
                return objectType;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SvnTag"/> class.
        /// </summary>
        /// <example>
        /// <code>
        /// var tag = new SvnTag("$Id$");
        /// </code>
        /// </example>
        /// <param name="text">The Svn Id tag.</param>
        public SvnTag(string text)
        {
            this.id = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SvnTag"/> class.
        /// </summary>
        /// <example>
        /// <code>
        /// var tag = new SvnTag(
        ///     "$Id$",
        ///     "PKSoft.SubVersion, Version=1.0.3846.14773, Culture=neutral, PublicKeyToken=null");
        /// </code>
        /// </example>
        /// <param name="id">The Svn id tag.</param>
        /// <param name="assemblyFullName">Full name of the assembly.</param>
        public SvnTag(SvnTag id, string assemblyFullName)
        {
            this.id = id.Id;
            this.assemblyFullName = assemblyFullName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SvnTag"/> class.
        /// </summary>
        /// <example>
        /// <code>
        /// var tag1 = new SvnTag(MainForm, 0); /* MainForm.cs */
        /// var tag2 = new SvnTag(MainForm, 1); /* MainForm.Designer.cs */
        /// </code>
        /// </example>
        /// <param name="type">The type connected with the SvnTag class.</param>
        /// <param name="partialIndex">The partial index of source code.</param>
        public SvnTag(Type type, int partialIndex)
        {
            object[] attributes = type.GetCustomAttributes(typeof(SvnIdAttribute), false);
            if (attributes.Length > partialIndex)
                this.id = ((SvnIdAttribute)attributes[partialIndex]).Tag.Id;
            this.assemblyFullName = type.Assembly.FullName;
            this.objectType = type;
        }
    }
}