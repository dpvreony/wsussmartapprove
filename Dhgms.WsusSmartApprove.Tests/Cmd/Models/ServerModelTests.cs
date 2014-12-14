namespace Dhgms.WsusSmartApprove.Tests.Cmd.Models
{
    using System;

    using Xunit;

    public class ServerModelTests
    {
        public class ConnectMethod
        {
            [Fact]
            public void ThrowsArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() => SmartLib.Controller.Server.Connect(null));
            }
        }

        public class GetRootUpdateCategoriesMethod
        {
            [Fact]
            public void ThrowsArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() => SmartLib.Controller.Server.GetRootUpdateCategories(null));
            }
        }

        public class GetUpdateClassificationsMethod
        {
            [Fact]
            public void ThrowsArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() => SmartLib.Controller.Server.GetUpdateClassifications(null));
            }
        }

        public class GetRootTargetGroupMethod
        {
            [Fact]
            public void ThrowsArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() => SmartLib.Controller.Server.GetRootTargetGroup(null));
            }
        }
    }
}
