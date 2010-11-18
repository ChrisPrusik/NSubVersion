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
using System.Data;
using System.Windows.Forms;

namespace NSubversion
{
    /// <summary>
    /// Form executed by SvnDialog
    /// </summary>
    [SvnId("$Id$")]
    public partial class SvnForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SvnForm"/> class. 
        /// It's managed by <see cref="SvnDialog"/> class.
        /// </summary>
        public SvnForm()
        {
            InitializeComponent();
            Text = String.Format("{0} {1} (C) {2}",
                Application.ProductName, Application.ProductVersion, Application.CompanyName);

            DataTable dt = SvnRepository.ToDataTable();
            BindingSource bs = new BindingSource(dt, "");
            grvSvnRepository.DataSource = bs;
        }

        /// <summary>
        /// Gets or sets the header text on this form (above DataView).
        /// </summary>
        /// <value>The header text.</value>
        public string HeaderText
        {
            get { return lblHeaderText.Text; }
            set { lblHeaderText.Text = value; }
        }
    }
}
