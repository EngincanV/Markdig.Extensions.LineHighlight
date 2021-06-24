using Markdig.Extensions.LineHighlight.Markdown.Extensions;

namespace Markdig.Extensions.LineHighlight.Extensions
{
    public static class MarkdownPipelineBuilderExtensions
    {
        public static MarkdownPipelineBuilder UseHighlightedCodeBlocks(this MarkdownPipelineBuilder pipeline)
        {
            pipeline.Extensions.AddIfNotAlready<HighlightedCodeBlockExtension>();
            return pipeline;
        }
    }
}
