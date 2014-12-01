ExitStrategy is a set of tools that will help WebForms and MVC live together in one Asp.net webapplication.

This repository provides the following:
- [A Wiki](https://github.com/LodewijkSioen/ExitStrategy/wiki) with guidance on how WebForms and MVC can live together in one asp.net project
- WebForms controls that will allow you to re-use your MVC Views in the WebForms world. For Example:

````aspnet
<mcv:Partial PartialViewName="Header" runat="server" />
<mvc:Editor SelectMethod="GetModel" AdditionalViewData='<%$Object:new {htmlAttributes = new {@class = "form-control"},} %>' runat="server" />
````

The ultimate goal is to help you migrate away from WebForms into the new and shiny MVC-world. That's why you
won't find any tools to integrate WebForms into MVC, just tools to integrate the new MVC things into
your legacy WebForms pages.

You can see a [demo site here](http://exitstrategy.apphb.com/).

There is also a [Nuget Package](https://www.nuget.org/packages/ExitStrategy.ForWebforms/):

``Install-Package ExitStrategy.ForWebforms -Pre``

[![Build status](https://ci.appveyor.com/api/projects/status/6q6qxr2t7p03v2m7/branch/master?svg=true)](https://ci.appveyor.com/project/LodewijkSioen/exitstrategy/branch/master)
