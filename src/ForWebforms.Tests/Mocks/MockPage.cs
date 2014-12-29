using System;
using System.Web.UI;

namespace ExitStrategy.ForWebforms.Tests.Mocks
{
    public class MockPage : Page
    {
        public DateTime GetModel()
        {
            return new DateTime(2014, 12, 18);
        }
    }
}