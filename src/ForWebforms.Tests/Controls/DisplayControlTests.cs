using System;

namespace ExitStrategy.ForWebforms.Tests.Controls
{
    [Serializable]
    public class DisplayControlTests : MvcControlTests
    {
        protected override MvcControl CreateControl()
        {
            return new Display();
        }

        protected override string ExpectedContentForDateTime
        {
            get { return "This is a displaytemplate for 18/12/2014"; }
        }

        protected override string ExpectedContentForDateTimeNull
        {
            get { return "This is a displaytemplate for 01/01/2015"; }
        }
    }
}