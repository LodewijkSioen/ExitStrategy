<img src='https://raw.githubusercontent.com/LodewijkSioen/ExitStrategy/master/img/Icon_512.png' alt='ExitStrategy Logo' />
================================

What is ExitStrategy?
--------------------------------
ExitStrategy is a set of tools that will help WebForms and MVC live together in one asp.net webapplication.
The ultimate goal is to help you migrate away from WebForms into the new and shiny MVC-world. That's why you
won't find any tools to integrate WebForms into MVC, just tools to integrate the new MVC things into
your legacy WebForms pages.

There is a [demo site](http://exitstrategy.apphb.com/) where you can see it in action.

What does it provide?
--------------------------------
- [A Wiki](https://github.com/LodewijkSioen/ExitStrategy/wiki) to help you set up a hybrid project
- A library with WebControls that will allow you to re-use your MVC Views in the WebForms world. For Example:

````aspnet
<mcv:Partial PartialViewName="Header" 
             runat="server" />

<mvc:Editor SelectMethod="GetModel" 
            AdditionalViewData='<%$Object:new {htmlAttributes = new {@class = "form-control"},} %>' 
            runat="server" />
````

Where can I get it?
--------------------------------

You can install ExitStrategy from the [Nuget Package](https://www.nuget.org/packages/ExitStrategy.ForWebforms/):

``PM> Install-Package ExitStrategy.ForWebforms -Pre``

[![Build status](https://ci.appveyor.com/api/projects/status/6q6qxr2t7p03v2m7/branch/master?svg=true)](https://ci.appveyor.com/project/LodewijkSioen/exitstrategy/branch/master)
