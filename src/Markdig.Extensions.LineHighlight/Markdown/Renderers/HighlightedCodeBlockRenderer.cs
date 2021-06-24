using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Markdig.Extensions.LineHighlight.Markdown.Renderers
{
    public class HighlightedCodeBlockRenderer : HtmlObjectRenderer<CodeBlock>
    {
        public bool OutputAttributesOnPre { get; set; }

        public HashSet<string> BlocksAsDiv { get; }

        public HighlightedCodeBlockRenderer()
        {
            BlocksAsDiv = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        protected override void Write(HtmlRenderer renderer, CodeBlock codeBlock)
        {
            renderer.EnsureLine();

            var fencedCodeBlock = codeBlock as FencedCodeBlock;

            if (fencedCodeBlock?.Info != null && BlocksAsDiv.Contains(fencedCodeBlock.Info))
            {
                var infoPrefix = (codeBlock.Parser as FencedCodeBlockParser)?.InfoPrefix ??
                                 FencedCodeBlockParser.DefaultInfoPrefix;

                if (renderer.EnableHtmlForBlock)
                {
                    renderer.Write("<div")
                        .WriteAttributes(codeBlock.TryGetAttributes(),
                            cls => cls.StartsWith(infoPrefix, StringComparison.Ordinal) ? cls.Substring(infoPrefix.Length) : cls)
                        .Write('>');
                }

                renderer.WriteLeafRawLines(codeBlock, true, true, true);

                if (renderer.EnableHtmlForBlock)
                {
                    renderer.WriteLine("</div>");
                }
            }
            else
            {
                if (renderer.EnableHtmlForBlock)
                {
                    renderer.Write("<pre ");

                    if (OutputAttributesOnPre)
                    {
                        renderer.WriteAttributes(codeBlock);
                    }

                    WriteHighlightedCodeLines(renderer, fencedCodeBlock);

                    if (!OutputAttributesOnPre)
                    {
                        renderer.WriteAttributes(codeBlock);
                    }

                    renderer.Write('>');
                }

                renderer.WriteLeafRawLines(codeBlock, true, true);

                if (renderer.EnableHtmlForBlock)
                {
                    renderer.WriteLine("</code></pre>");
                }
            }

            renderer.EnsureLine();
        }

        private void WriteHighlightedCodeLines(HtmlRenderer renderer, FencedCodeBlock fencedCodeBlock)
        {
            var highlightedLines = GetHighlightedLines(fencedCodeBlock);
            if (highlightedLines.Any())
            {
                //prevents adding line-numbers for highlighted lines
                //TODO: make it optional?
                renderer.WriteAttributes(new HtmlAttributes
                {
                    Classes = new List<string> { "line-numbers" } 
                });

                var lines = string.Join(",", highlightedLines);
                renderer.Write($"data-line={lines}><code");
            }
            else
            {
                renderer.Write("><code");
            }
        }

        private HashSet<int> GetHighlightedLines(FencedCodeBlock fencedCodeBlock)
        {
            var pattern = @"\{([^}]+)\}";

            var highlightedLines = new HashSet<int>();
            if (string.IsNullOrWhiteSpace(fencedCodeBlock?.Arguments) || 
                !Regex.IsMatch(fencedCodeBlock.Arguments, pattern))
            {
                return highlightedLines;
            }

            var match = Regex.Match(fencedCodeBlock.Arguments, pattern);
            var groups = match.Groups;
            if (groups.Count < 2 || string.IsNullOrWhiteSpace(groups[1].Value))
            {
                return highlightedLines;
            }

            var lines = groups[1].Value.Split(",");
            foreach (var line in lines)
            {
                if (line.Contains("-"))
                {
                    var numbers = line.Split("-");
                    if (numbers.Length > 2)
                    {
                        continue;
                    }

                    if (!int.TryParse(numbers[0], out var minLineNumber) ||
                        !int.TryParse(numbers[1], out var maxLineNumber))
                    {
                        continue;
                    }

                    for (var lineNumber = minLineNumber; lineNumber < maxLineNumber + 1; lineNumber++)
                    {
                        highlightedLines.Add(lineNumber);
                    }
                }
                else
                {
                    if (int.TryParse(line, out var lineNumber))
                    {
                        highlightedLines.Add(lineNumber);
                    }
                }
            }

            return highlightedLines;
        }
    }
}
