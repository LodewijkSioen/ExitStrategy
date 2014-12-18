using System;
using System.Web.UI;

namespace ForWebforms.Tests
{
    public class MockPage : Page
    {
        public DateTime GetModel()
        {
            return new DateTime(2014, 12, 18);
        }
    }
}