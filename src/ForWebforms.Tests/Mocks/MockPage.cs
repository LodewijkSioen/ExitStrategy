using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExitStrategy.ForWebforms.Tests.Mocks
{
    public class MockPage : Page
    {
        public MockPage()
        {
            var datasource = new ObjectDataSource
            {
                SelectMethod = "GetModel",
                TypeName = "ExitStrategy.ForWebforms.Tests.Mocks.MockPage",
                ID = "ModelDataSource"
            };
            var nullDataSource = new ObjectDataSource
            {
                SelectMethod = "GetNull",
                TypeName = "ExitStrategy.ForWebforms.Tests.Mocks.MockPage",
                ID = "NullModelDataSource"
            };

            Controls.Add(datasource);
            Controls.Add(nullDataSource);
        }

        public MockModel GetModel()
        {
            return MockModel.Default();
        }

        public MockModel GetNull()
        {
            return null;
        }
    }

    public class MockModel
    {
        public string StringField { get; set; }
        public bool BooleanField { get; set; }
        public DateTime DateField { get; set; }

        public static MockModel Default()
        {
            return new MockModel
            {
                StringField = "Test",
                BooleanField = true,
                DateField = new DateTime(2014, 12, 18)
            };
        }
    }
}