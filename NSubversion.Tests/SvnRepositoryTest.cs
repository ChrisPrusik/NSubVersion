/*******************************************************************************

  NSubversion (C) 2010 Krzysztof Arkadiusz Prusik
  SVN, CVS tags in Your application, by the reflection SvnIdAttribute
  Latest version: http://NSubversion.codeplex.com/

  $Id: SvnRepositoryTest.cs 4257 2010-11-07 15:42:52Z unknown $

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
using System.Collections.Generic;
using NUnit.Framework;
using NSubversion;

namespace NSubversion.Tests
{
    
    [TestFixture]
    public class SvnRepositoryTest
    {

        [Test]
        public void ItemsTest()
        {
            long firstTime = Environment.TickCount;
            List<SvnTag> firstItems = SvnRepository.Items;
            firstTime = Environment.TickCount - firstTime;

            long secondTime = Environment.TickCount;
            List<SvnTag> secondItems = SvnRepository.Items;
            secondTime = Environment.TickCount - secondTime;

            Assert.IsTrue(secondTime <= firstTime);
            Assert.IsTrue(secondTime <= 100 || Math.Abs(firstTime - secondTime) > 100, 
                "Second reflection loading must be faster");

            Assert.IsTrue(firstItems.Count > 0);
            Assert.AreEqual(firstItems.Count, secondItems.Count);
            Assert.AreEqual(firstItems[0].Id, secondItems[0].Id);
            Assert.AreEqual(firstItems[0].AssemblyFullName, secondItems[0].AssemblyFullName);
            Assert.IsTrue(firstItems[0].Author != "");
            Assert.IsTrue(firstItems[0].FileName != "");

            foreach (SvnTag tag in firstItems)
            {
                Assert.IsTrue(tag.Id != "");
                Assert.IsTrue(tag.FileName != "");
                Assert.IsTrue(tag.LastModified > DateTime.MinValue);
                Assert.IsTrue(tag.VersionNumber > 0);
                Assert.IsTrue(tag.AssemblyFullName != "");
                Assert.IsTrue(tag.AssemblyName != "");
                Assert.IsTrue(tag.AssemblyPublicKeyToken != "");
                Assert.IsTrue(tag.AssemblyVersion > new Version(0, 0, 0, 0));
            }
        }

        [Test]
        public void FindFileNameTest()
        {
            Assert.IsNull(SvnRepository.FindFileName("FileNotExists.cs"));

            Assert.IsTrue(SvnRepository.FindFileName("SvnTag.cs").FileName == "SvnTag.cs");
            Assert.IsTrue(SvnRepository.FindFileName("SvnIdAttribute.cs").FileName == "SvnIdAttribute.cs");
            Assert.IsTrue(SvnRepository.FindFileName("SvnRepository.cs").FileName == "SvnRepository.cs");
        }

        [Test]
        public void AsLinesTest()
        {
            string[] lines = SvnRepository.ToArray();
            Assert.IsTrue(lines.Length > 0);
            Assert.AreEqual(lines.Length, SvnRepository.Items.Count);
            foreach (string line in lines)
                Assert.AreEqual(9, line.Split(';').Length);
        }

        [Test]
        public void AsTextTest()
        {
            string text = "";
            foreach (string line in SvnRepository.ToArray())
                text += line + "\r\n";
            Assert.AreEqual(text, SvnRepository.ToText());
        }

        [Test]
        public void AsDataTableTest()
        {
            Assert.AreEqual(SvnRepository.Items.Count, SvnRepository.ToDataTable().Rows.Count);
            Assert.AreEqual(9, SvnRepository.ToDataTable().Columns.Count);
        }
    }
}
