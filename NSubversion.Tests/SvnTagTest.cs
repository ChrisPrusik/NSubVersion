/*******************************************************************************

  NSubversion (C) 2010 Krzysztof Arkadiusz Prusik
  SVN, CVS tags in Your application, by the reflection SvnIdAttribute
  Latest version: http://NSubversion.codeplex.com/

  $Id: SvnTagTest.cs 4263 2010-11-07 20:27:49Z unknown $

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
using System.Reflection;
using System.IO;
using NUnit.Framework;
using NSubversion;

namespace NSubversion.Tests
{
    [TestFixture]
    public class SvnTagTest
    {

        [Test]
        public void FindBeetweenTest()
        {
            SvnTag tag = new SvnTag("");
            Assert.AreEqual("Alek", tag.FindBeetween("1234Alek4321", "1234", "4321"));
            Assert.AreEqual("Alek", tag.FindBeetween("1234Alek4321", "4", "4"));
            Assert.AreEqual("Alek4321", tag.FindBeetween("1234Alek4321", "1234", ""));
            Assert.AreEqual("1234Alek", tag.FindBeetween("1234Alek4321", "", "4321"));
        }

        [Test]
        public void IdTest()
        {
            SvnTag tag = new SvnTag("$I" + "d: SvnTag.cs 48333 2010-07-13 05:54:53Z KAP $");
            Assert.AreEqual("SvnTag.cs", tag.FileName);
            Assert.AreEqual(48333, tag.VersionNumber);
            Assert.AreEqual(DateTime.Parse("2010-07-13 05:54:53Z"), tag.LastModified);
            Assert.AreEqual("KAP", tag.Author);

            tag = new SvnTag("$I" + "d$");
            Assert.AreEqual("", tag.FileName);
            Assert.AreEqual(0, tag.VersionNumber);
            Assert.AreEqual(DateTime.MinValue, tag.LastModified);
            Assert.AreEqual("", tag.Author);

            tag = new SvnTag("");
            Assert.AreEqual("", tag.FileName);
            Assert.AreEqual(0, tag.VersionNumber);
            Assert.AreEqual(DateTime.MinValue, tag.LastModified);
            Assert.AreEqual("", tag.Author);
        }

        [Test]
        public void AssemblyTest()
        {
            string path = "NSubversion.dll";
            if (File.Exists(path) == false)
                path = @"..\..\..\NSubversion\Bin\Debug\NSubversion.dll";
            Assert.IsTrue(File.Exists(path));
            Assembly assembly = Assembly.LoadFile(Path.GetFullPath(path));
            Assert.IsNotNull(assembly);
            Assert.AreEqual("NSubversion", assembly.GetName().Name);
            
            SvnTag tag = new SvnTag("$I" + "d: SvnTag.cs 48333 2010-07-13 05:54:53Z KAP $");
            tag = new SvnTag(tag, assembly.FullName);

            // Current Version may be different
            Version version = new Version(1, 0, 3846, 14773);
            string assemblyFullName = "NSubversion, Version=1.0.3846.14773, Culture=neutral, PublicKeyToken=null";
            Assert.AreEqual(tag.FindBeetween(assemblyFullName, "", "="),
                tag.FindBeetween(assembly.FullName, "", "="));
            Assert.AreEqual(tag.FindBeetween(assemblyFullName, "Culture=", ""),
                tag.FindBeetween(assembly.FullName, "Culture=", ""));
            Assert.AreEqual("NSubversion", tag.AssemblyName);
            Assert.IsTrue(version < tag.AssemblyVersion);
            Assert.AreEqual("null", tag.AssemblyPublicKeyToken);
            Assert.AreEqual("neutral", tag.AssemblyCulture);
        }

        [Test]
        public void TypeTest()
        {
            // Current Version may be different, please check this
            Version version = new Version(1, 0, 3846, 14773);
            SvnTag tag = new SvnTag(typeof(SvnTag), 0);

            Assert.AreEqual("SvnTag.cs", tag.FileName);
            Assert.IsTrue(tag.VersionNumber > 0);
            Assert.IsTrue(DateTime.Parse("2010-07-13 05:54:53Z") <= tag.LastModified);

            //CodePlex error: author in tag isn't correct
            Assert.AreEqual("unknown", tag.Author);

            Assert.AreEqual("NSubversion", tag.AssemblyName);
            Assert.IsTrue(version < tag.AssemblyVersion);
            Assert.AreEqual("null", tag.AssemblyPublicKeyToken);
            Assert.AreEqual("neutral", tag.AssemblyCulture);

            Assert.AreEqual(typeof(SvnTag), tag.Type);
            Assert.AreEqual("SvnTag", tag.Type.Name);
            Assert.AreEqual(tag.AssemblyFullName, tag.Type.Assembly.FullName);
        }
    }
}