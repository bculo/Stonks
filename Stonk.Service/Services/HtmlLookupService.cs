using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Stonk.Application.Contracts.Services;
using Stonk.Application.Extensions;
using Stonk.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stonk.Service.Services
{
    public class HtmlLookupService : IHtmlLookupService
    {
        private readonly ILogger<HtmlLookupService> _logger;

        public HtmlDocument Document { get; set; }
        public string PreviousRequestHtml { get; set; }

        public HtmlLookupService(ILogger<HtmlLookupService> logger)
        {
            _logger = logger;
        }

        public string FindHtmlElementByClass(string html, string elementClass, bool innerHtml = false)
        {
            PrepareDocument(html);

            var element = SingleElementSearch($"//*[contains(@class, '{elementClass}')]");

            return HandleElementSearchResult(element, innerHtml);
        }

        public string FindHtmlElementById(string html, string elementId, bool innerHtml = false)
        {
            PrepareDocument(html);

            var element = SingleElementSearch($"//*[contains(@id, '{elementId}')]");

            return HandleElementSearchResult(element, innerHtml);
        }

        public string FindHtmlElementByTag(string html, string elementName, bool innerHtml = false)
        {
            PrepareDocument(html);

            var element = SingleElementSearch($"//{elementName}");

            return HandleElementSearchResult(element, innerHtml);
        }

        public string FindHtmlElementByTag(string html, string elementName, int elementIndex, bool innerHtml = false)
        {
            PrepareDocument(html);

            var elements = MultipleElementsSearch($"//{elementName}");

            if (!MultipleElementsSearchValid(elements))
            {
                return null;
            }

            if(elements.Count < elementIndex)
            {
                return null;
            }

            var element = elements[elementIndex];

            return HandleElementSearchResult(element, innerHtml);
        }

        public string FindHtmlElementByXPath(string html, string xpath, int elementIndex, bool innerHtml = false)
        {
            PrepareDocument(html);

            var elements = MultipleElementsSearch(xpath);

            if (!MultipleElementsSearchValid(elements))
            {
                return null;
            }

            if (elements.Count < elementIndex)
            {
                return null;
            }

            var element = elements[elementIndex];

            return HandleElementSearchResult(element, innerHtml);
        }

        public T GetHtmlElementDataAttributeValue<T>(string htmlElement, string attributeName)
        {
            PrepareDocument(htmlElement);

            var attributeValue = Document.DocumentNode.GetAttributeValue<T>(attributeName, default);

            return attributeValue;
        }

        private HtmlNode SingleElementSearch(string searchPattern)
        {
            return Document.DocumentNode.SelectSingleNode(searchPattern);
        }

        private HtmlNodeCollection MultipleElementsSearch(string searchPattern)
        {
            return Document.DocumentNode.SelectNodes(searchPattern);
        }

        private string HandleElementSearchResult(HtmlNode node, bool innerHtml)
        {
            if (node is null)
            {
                return null;
            }

            if (innerHtml)
            {
                return node.InnerText;
            }

            return node.OuterHtml;
        }

        private bool MultipleElementsSearchValid(HtmlNodeCollection nodes)
        {
            if (nodes is null)
            {
                return false;
            }

            if(nodes.Count == 0)
            {
                return false;
            }

            return true;
        }

        private void PrepareDocument(string html)
        {
            if(Document is null)
            {
                Document = new HtmlDocument();
                Document.LoadHtml(html);
            }

            if(PreviousRequestHtml != html)
            {
                Document = new HtmlDocument();
                Document.LoadHtml(html);
            }

            PreviousRequestHtml = html;
        }
    }
}
