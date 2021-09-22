using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YooniK.Services.Client.Common;

namespace YooniK.Services.Client.Tests.Common
{
    public class UtilsTests
    {
        [Test]
        public void GetRequestUri_BaseUriNullParameters_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Utils.GetRequestUri(null, null, null));
        }
    }
}
