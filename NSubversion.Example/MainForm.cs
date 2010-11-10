/*******************************************************************************

  NSubversion (C) 2010 Krzysztof Arkadiusz Prusik
  SVN, CVS tags in Your application, by the reflection SvnIdAttribute
  Latest version: http://NSubversion.codeplex.com/

  $Id: MainForm.cs 3180 2010-09-30 22:36:30Z unknown $

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
using System.Windows.Forms;

namespace NSubversion.Example
{
    [SvnId("$Id: MainForm.cs 3180 2010-09-30 22:36:30Z unknown $")]
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnShowSubVersionInfoAsText_Click(object sender, EventArgs e)
        {
            MessageBox.Show(SvnRepository.ToText());
        }

        private void btnShowSnvRepositoryDialog_Click(object sender, EventArgs e)
        {
            svnDialog1.HeaderText = "List of all svn $I" + "d$ tags in current project";
            svnDialog1.ShowDialog();
        }
    }
}
