﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sync.Net.Configuration;
using Sync.Net.UI.Utils;
using Sync.Net.UI.ViewModels;

namespace Sync.Net.UI.UnitTests
{
    [TestClass]
    public class MainWindowViewModelTests
    {
        private Mock<IWindowManager> _windowManager;
        private Mock<ISyncNetTask> _task;
        private SyncNetConfiguration _configuration;
        private Mock<ILogger> _logger;

        [TestInitialize]
        public void Initialize()
        {
            _windowManager = new Moq.Mock<IWindowManager>();
            _task = new Moq.Mock<ISyncNetTask>();
            _configuration = new SyncNetConfiguration();
            _logger = new Mock<ILogger>();
        }

        [TestMethod]
        public void ExitCommandShutDownApplication()
        {
            bool shutdown = false;

            _windowManager.Setup(x => x.ShutdownApplication()).Callback(() => shutdown = true);

            MainWindowViewModel model =
                new MainWindowViewModel(_windowManager.Object, _task.Object, _configuration, _logger.Object);
            model.ExitCommand.Execute(null);

            Assert.IsTrue(shutdown);
        }

        [TestMethod]
        public async Task SyncCommandStartsSync()
        {
            bool wasRun = false;
            _task.Setup(x => x.ProcessFiles()).Callback(() => wasRun = true);

            MainWindowViewModel model =
                new MainWindowViewModel(null, _task.Object, _configuration, _logger.Object);

            await model.SyncCommand.Execute();
            Assert.IsTrue(wasRun);
        }
    }
}
