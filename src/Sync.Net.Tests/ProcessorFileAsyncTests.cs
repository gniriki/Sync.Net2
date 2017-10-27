﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sync.Net.Tests
{
    [TestClass]
    public class ProcessorFileAsyncTests : ProcessorFullDirectoryTests
    {
        [TestMethod]
        public async Task UploadsFile()
        {
            await _syncNet.CopyFileAsync(_sourceDirectory.GetFile(_fileName));

            var files = _targetDirectory.GetFiles();
            Assert.AreEqual(_fileName, files.First().Name);
        }

        [TestMethod]
        public async Task UploadFromSubfolderDoesntCreateAFileInMainDirectory()
        {
            await _syncNet.CopyFileAsync(_sourceDirectory.GetDirectory(_subDirectoryName).GetFile(_subFileName));

            var files = _targetDirectory.GetFiles();

            Assert.IsTrue(!files.Any());
        }

        [TestMethod]
        public async Task UploadFromSubfolderCreatesASubDirectory()
        {
            await _syncNet.CopyFileAsync(_sourceDirectory.GetDirectory(_subDirectoryName).GetFile(_subFileName));

            var dirs = _targetDirectory.GetDirectories();

            Assert.IsTrue(dirs.Count() == 1);
            Assert.AreEqual(_subFileName, dirs.First().GetFiles().First().Name);
        }
    }
}