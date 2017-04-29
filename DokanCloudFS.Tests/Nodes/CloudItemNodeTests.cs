﻿/*
The MIT License(MIT)

Copyright(c) 2015 IgorSoft

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IgorSoft.CloudFS.Interface.IO;
using IgorSoft.DokanCloudFS.Nodes;

namespace IgorSoft.DokanCloudFS.Tests.Nodes
{
    [TestClass]
    public sealed partial class CloudItemNodeTests
    {
        private Fixture fixture;

        [TestInitialize]
        public void Initialize()
        {
            fixture = Fixture.Initialize();
        }

        [TestMethod, TestCategory(nameof(TestCategories.Offline))]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CloudItemNode_CreateNew_WhereContractIsUnknownType_Throws()
        {
            var contract = fixture.TestItem;

            var sut = new CloudDirectoryNode(new DirectoryInfoContract("*", "*", DateTimeOffset.FromFileTime(0), DateTimeOffset.FromFileTime(0)), fixture.Drive);
            sut.CreateNew(contract);
        }

        [TestMethod, TestCategory(nameof(TestCategories.Offline))]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CloudItemNode_Move_WhereNewNameIsEmpty_Throws()
        {
            var contract = fixture.TestItem;

            var sut = fixture.GetItem(contract);
            sut.Move(string.Empty, new CloudDirectoryNode(fixture.TargetDirectory, fixture.Drive));
        }

        [TestMethod, TestCategory(nameof(TestCategories.Offline))]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CloudItemNode_Move_WhereDestinationDirectoryIsNull_Throws()
        {
            var contract = fixture.TestItem;

            var sut = fixture.GetItem(contract);
            sut.Move("MovedItem", null);
        }

        [TestMethod, TestCategory(nameof(TestCategories.Offline))]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CloudItemNode_Move_WhereDestinationDriveIsDifferent_Throws()
        {
            var contract = fixture.TestItem;

            var sut = fixture.GetItem(contract);
            sut.Move("MovedItem", new CloudDirectoryNode(fixture.TargetDirectory, fixture.OtherDrive));
        }

        [TestMethod, TestCategory(nameof(TestCategories.Offline))]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CloudItemNode_Move_WhereParentIsNull_Throws()
        {
            var contract = fixture.TestItem;

            var sut = fixture.GetItem(contract);
            sut.Move("MovedItem", new CloudDirectoryNode(fixture.TargetDirectory, fixture.Drive));
        }

        [TestMethod, TestCategory(nameof(TestCategories.Offline))]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CloudItemNode_ResolveContract_WhereCloudItemNodeIsResolved_Throws()
        {
            var contract = fixture.TestFile;

            var sut = fixture.GetFile(contract);
            sut.ResolveContract(contract);
        }

        [TestMethod, TestCategory(nameof(TestCategories.Offline))]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CloudItemNode_ResolveContract_WhereContractIsMismatched_Throws()
        {
            var proxyContract = fixture.MismatchedProxyTestFile;
            var contract = fixture.TestFile;

            var sut = fixture.GetFile(proxyContract);
            sut.ResolveContract(contract);
        }
    }
}
