# Markdig.Extensions.LineHighlight

This package can be used for code line highlighting.

**Example Input:**
```html
   ```csharp {1}
      public class HelloWorld 
      {
         public void ShowMessage() 
         {
            Console.WriteLine("Hello World");
         }
      }
   \```
```

**Output**:

```html
<pre>
   <code class="language-csharp">
      <div class="highlight-line">
         <mark>
            public class HelloWorld
         </mark>
      <div>
      //...
   </code>   
</pre>
```
