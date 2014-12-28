using ExitStrategy.ForWebforms;
using System;
using System.Web.UI;

namespace ForWebforms.Tests.Mocks
{
    public class MockPage : Page
    {
        private MvcControl _control;

        public DateTime GetModel()
        {
            return new DateTime(2014, 12, 18);
        }

        public T GetControlUnderTest<T>()
            where T : MvcControl
        {
            return (T)_control;
        }

        public void SetControlUnderTest(MvcControl control)
        {
            Controls.Add(control);
            _control = control;
        }
    }
}