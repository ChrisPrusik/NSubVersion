/*******************************************************************************

  NSubversion (C) 2010 Krzysztof Arkadiusz Prusik
  SVN, CVS tags in Your application, by the reflection SvnIdAttribute
  Latest version: http://NSubversion.codeplex.com/

  $Id: SvnRepository.cs 3216 2010-10-03 05:24:27Z unknown $

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
using System.Data;
using System.Reflection;

namespace NSubversion
{
    /// <summary>
    /// Central, static repository of all SvnTag items in project
    /// </summary>
    [SvnId("$Id: SvnRepository.cs 3216 2010-10-03 05:24:27Z unknown $")]
    public static class SvnRepository
    {
        private static List<SvnTag> items = new List<SvnTag>();
        /// <summary>
        /// List of all SvnTag items in project
        /// </summary>
        /// <example>
        /// <code>
        /// /* List all files with SvnIdAttribute tag in project */
        /// foreach(SvnTag item in SvnRepository.Items)
        ///     Console.WriteLine(item.FileName);
        /// </code>
        /// </example>
        /// <value>The items.</value>
        public static List<SvnTag> Items
        {
            get 
            {
                if (items.Count == 0)
                {
                    Dictionary<string, int> typePartial = new Dictionary<string, int>();
                    foreach (Assembly assemblyItem in AppDomain.CurrentDomain.GetAssemblies())
                        foreach (Type typeItem in assemblyItem.GetTypes())
                        {
                            SvnIdAttribute[] attributes = 
                                (SvnIdAttribute[])typeItem.GetCustomAttributes(
                                    typeof(SvnIdAttribute), false);
                            for (int index = 0; index < attributes.Length; index++)
                            {
                                SvnTag tag = new SvnTag(typeItem, index);
                                if (tag.Id != "")
                                    items.Add(tag);
                            }
                        }
                }
                return items; 
            }
        }

        /// <summary>
        /// Finds the name of the file in repository of SvnTag items in project.
        /// </summary>
        /// <example>
        /// <code>
        /// Console.WriteLine(SvnRepository.FindFileName("MyClass.cs").VersionNumber.ToString());
        /// </code>
        /// </example>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>SvnTag connected with the file</returns>
        public static SvnTag FindFileName(string fileName)
        {
            fileName = fileName.ToLower();
            foreach (SvnTag id in Items)
                if (fileName == id.FileName.ToLower())
                    return id;
            return null;
        }

        /// <summary>
        /// Convert SvnTag Items to array without header.
        /// </summary>
        /// <example>
        /// <code>
        /// MyClass.cs; 49273; 2010-07-22 07:07:29Z; Robert; MyAssembly; 1.0.1.3; neutral; null; frmMyClass
        /// MyClass.Designer.cs; 49271; 2010-07-22 06:07:29Z; Jan; MyAssembly; 1.0.1.3; neutral; null; frmMyClass
        /// </code>
        /// </example>
        /// <returns>array of string SvnTag fields separated by ;</returns>
        public static string[] ToArray()
        {
            if (Items != null)
            {
                string[] lines = new string[Items.Count];
                for(int i = 0; i < Items.Count; i++)
                {
                    lines[i] = Items[i].FileName + "; " +
                        Items[i].VersionNumber.ToString() + "; " +
                        Items[i].LastModified.ToString() + "; " +
                        Items[i].Author + "; " +
                        Items[i].AssemblyName + "; " +
                        Items[i].AssemblyVersion.ToString() + "; " +
                        items[i].AssemblyPublicKeyToken + "; " +
                        Items[i].AssemblyCulture + "; " +
                        Items[i].Type.Name;
                }
                return lines;
            }
            else
                return null;
        }

        /// <summary>
        /// Convert SvnTag Items to the text.
        /// </summary>
        /// <example>
        /// <code>
        /// MyClass.cs; 49273; 2010-07-22 07:07:29Z; Robert; MyAssembly; 1.0.1.3; neutral; null; frmMyClass
        /// MyClass.Designer.cs; 49271; 2010-07-22 06:07:29Z; Jan; MyAssembly; 1.0.1.3; neutral; null; frmMyClass
        /// </code>
        /// </example>
        /// <returns>ToArray() lines separated by linefeed (\r\n)</returns>
        public static string ToText()
        {
            if (Items != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string line in ToArray())
                    sb.AppendLine(line);
                return sb.ToString();

            }
            else
                return "";
        }

        /// <summary>
        /// Convert SvnTag Items to the DataTable object (and fill them). 
        /// </summary>
        /// <example>
        /// <code>
        /// DataTable myTable = SvnRepository.ToDataTable();
        /// foreach(DataRow myRow in myTable.Rows)
        ///     foreach(DataColumn myCol in myTable.Columns)
        ///         Console.WriteLine(myRow[myCol]);
        /// </code>
        /// </example>
        /// <returns>DataTable object</returns>
        public static DataTable ToDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FileName", typeof(string));
            dt.Columns.Add("VersionNumber", typeof(int));
            dt.Columns.Add("LastModified", typeof(DateTime));
            dt.Columns.Add("Author", typeof(string));
            dt.Columns.Add("AssemblyName", typeof(string));
            dt.Columns.Add("AssemblyVersion", typeof(string));
            dt.Columns.Add("AssemblyPublicKeyToken", typeof(string));
            dt.Columns.Add("AssemblyCulture", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            if (Items != null)
                foreach (SvnTag tag in Items)
                    dt.LoadDataRow(new object[] {tag.FileName, tag.VersionNumber,
                        tag.LastModified, tag.Author, tag.AssemblyName,
                        tag.AssemblyVersion.ToString(), tag.AssemblyPublicKeyToken, 
                        tag.AssemblyCulture, tag.Type.Name}, true);
            return dt;
        }
    }
}