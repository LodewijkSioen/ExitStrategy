using System;

namespace ExitStrategy.ForWebforms.Tests.Controls
{
    [Serializable]
    public class EditorControlTests : MvcControlTests
    {
        protected override MvcControl CreateControl()
        {
            return new Editor();
        }

        protected override string ExpectedContentForDateTime
        {
            get { return "This is an editortemplate for 18/12/2014"; }
        }

        protected override string ExpectedContentForDateTimeNull
        {
            get { return "This is an editortemplate for 01/01/2015"; }
        }
    }
}