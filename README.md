# Markdig.Extensions.LineHighlight

This package can be used for code line highlighting. It uses [Prism](https://prismjs.com/) for highlighting.

## Installation

You can use the following CLI command to add the package into your project:

```bash
dotnet add package Markdig.Extensions.LineHighlight
```

## Configurations

First, download the prism.js for code-highligthing and put it into your libs folder.

> You can install from: https://prismjs.com/download.html

Then, add prism.cs and prism.js files to your layout.

```html
<!DOCTYPE html>
<html lang="en">
   <head>
      <!-- code abbreviated for simplicity -->
      <link rel="stylesheet" href="~/css/prism.css" asp-append-version="true"/> 
   </head>
   <body>
      <!-- code abbreviated for simplicity -->
      <script src="~/js/prism.js" asp-append-version="true"></script>  
   </body>
</html>
```

## Usage

Let's assume you have a markdown content as below:

```html
   ```csharp {3}
      public class HelloWorld 
      {
         public void ShowMessage() 
         {
            Console.WriteLine("Hello World");
         }
      }
   \```
```

To render this markdown as HTML, you can use the `Markdown.ToHtml` method. To get benefit of code highlighting use `UseHighlightedCodeBlocks` method as below:

```csharp
   var pipeline = new MarkdownPipelineBuilder()
        .UseHighlightedCodeBlocks() //use highlighted code blocks 
        .Build();
    var result = Markdown.ToHtml(text.ToString(), pipeline);
```

Then the example output will be as follows:

![](assets/output.png)

> If you encounter a problem, you can examine [the sample application](demo/LineHighlightDemo/) in this repo anytime.