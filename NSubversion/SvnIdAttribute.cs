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
using System.IO;

namespace NSubversion
{
    /// <summary>
    /// Attribute which You can insert before all class, struct, enumeration, etc in your project
    /// </summary>
    /// <example>
    /// <code>
    /// [SvnId("$Id$")]
    /// class SvnIdAttribute
    /// {
    /// }
    /// </code>
    /// </example>
    [SvnId("$Id$")]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class SvnIdAttribute : Attribute
    {
        private SvnTag tag = null;
        /// <summary>
        /// Gets the SvnTag connected with with attribute.
        /// </summary>
        /// <value>The SvnTag object.</value>
        public SvnTag Tag
        {
            get { return tag; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SvnIdAttribute"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public SvnIdAttribute(string id)
        {
            this.tag = new SvnTag(id);
        }
    }
}