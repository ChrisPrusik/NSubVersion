/*******************************************************************************

  NSubversion (C) 2010 Krzysztof Arkadiusz Prusik
  SVN, CVS tags in Your application, by the reflection SvnIdAttribute
  Latest version: http://NSubversion.codeplex.com/

  $Id: SvnDialog.cs 3180 2010-09-30 22:36:30Z unknown $

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
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace NSubversion
{
    /// <summary>
    /// Dialog with information about all Svn tags in your project
    /// </summary>
    [SvnId("$Id: SvnDialog.cs 3180 2010-09-30 22:36:30Z unknown $")]
    public partial class SvnDialog : Component
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SvnDialog"/> class.
        /// </summary>
        public SvnDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SvnDialog"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public SvnDialog(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private string titleText = "";
        /// <summary>
        /// Gets or sets the title text of dialog form.
        /// </summary>
        /// <value>The title text.</value>
        public string TitleText
        {
            get { return titleText; }
            set { titleText = value; }
        }

        private string headerText = "";
        /// <summary>
        /// Gets or sets the header text of dialog form (above DataView).
        /// </summary>
        /// <value>The header text.</value>
        public string HeaderText
        {
            get { return headerText; }
            set { headerText = value; }
        }

        /// <summary>
        /// Shows the dialog with information about all Svn tags in your project.
        /// If TitleText is empty, then display in title: 
        /// <i>Application.ProductName Application.ProductVersion (C) Application.CompanyName</i>
        /// (defined by [Assembly:...] attributes in Properties/AssemblyInfo.cs).
        /// If HeaderText is empty, then display in header:
        /// <i>SubVersion information about source code of your application</i>.
        /// </summary>
        public void ShowDialog()
        {
            using (SvnForm form = new SvnForm())
            {
                if (this.HeaderText != "")
                    form.HeaderText = this.HeaderText;
                if (this.TitleText != "")
                    form.Text = this.TitleText;
                form.ShowDialog();
            }
        }
    }
}