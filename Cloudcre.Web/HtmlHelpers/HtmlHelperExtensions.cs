using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.WebPages;

namespace Cloudcre.Web.HtmlHelpers
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Returns an select list representing the members of an enum type
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        /// @Html.DropDownListFor(model => model.PropertyType, Model.PropertyType.ToSelectList(), new { data_bind = "value: PropertyType", @class = "text ui-widget-content ui-corner-all" })
        public static IEnumerable<SelectListItem> ToSelectList(this Enum enumValue)
        {
            var list = from Enum e in Enum.GetValues(enumValue.GetType())
                   select new SelectListItem
                   {
                       Selected = e.Equals(enumValue),
                       Text = e.ToDescription(),
                       Value = e.ToString()
                   };

            return list;
        }

        private static string ToDescription(this Enum value)
        {
            var attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        /// <summary>
        /// Enums For creating ordered and unordered lists
        /// </summary>
        public enum ListType
        {
            Ordered,
            Unordered,
            TableCell
        }

        private class ListModel
        {
            public string FirstTag { get; set; }
            public string LastTag { get; set; }
            public string InnerFirstTag { get; set; }
            public string InnerLastTag { get; set; }
        }

        /// <summary>
        /// Returns markup for ordered/unordered list of supplied items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="collection"></param>
        /// <param name="listType"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public static HelperResult ToFormattedList<T>(this HtmlHelper helper, List<T> collection,
                                                ListType listType, Func<T, HelperResult> template)
        {
            var model = new ListModel { FirstTag = "", LastTag = "", InnerFirstTag = "", InnerLastTag = "" };
            switch (listType)
            {
                case ListType.Ordered:
                    model.FirstTag = "<ol>";
                    model.LastTag = "</ol>";
                    model.InnerFirstTag = "<li>";
                    model.InnerLastTag = "</li>";
                    break;

                case ListType.Unordered:
                    model.FirstTag = "<ul>";
                    model.LastTag = "</ul>";
                    model.InnerFirstTag = "<li>";
                    model.InnerLastTag = "</li>";
                    break;
                case ListType.TableCell:
                    model.InnerFirstTag = "<td>";
                    model.InnerLastTag = "</td>";
                    break;
            }
            return new HelperResult(writer => ToFormattedList<T>(collection, template, writer, model));
        }

        private static void ToFormattedList<T>(IEnumerable<T> collection, Func<T, HelperResult> template, TextWriter writer, ListModel model)
        {
            writer.Write(model.FirstTag);
            foreach (T item in collection)
            {
                writer.Write(model.InnerFirstTag);
                writer.Write(template(item));
                writer.Write(model.InnerLastTag);
            }
            writer.Write(model.LastTag);
        }

        /// <summary>
        /// Serialies objects into json for rendering in markup
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static MvcHtmlString ToJson<T>(this HtmlHelper html, IEnumerable<T> items)
        {
            return MvcHtmlString.Create(new JavaScriptSerializer().Serialize(items));
        }


        /// <summary>
        /// Returns only the controller name for a particular controller action
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="action"></param>
        /// <param name="contoller"></param>
        /// <returns></returns>
        public static string ControllerForAction(this UrlHelper helper, string action, string contoller)
        {
            string actionRoute = helper.Action(action, contoller);

            List<string> tokens =  actionRoute.Split(new[] {'/', '\\'}, StringSplitOptions.RemoveEmptyEntries).ToList();
            
            if (tokens.Count <= 0)
            {
                return string.Empty;
            }
            
            tokens.RemoveAt(tokens.Count - 1);

            return "/" + String.Join("/", tokens);
        }
        
        /// <summary>
        /// Returns a list of links for paging through record sets
        /// </summary>
        /// <param name="html"></param>
        /// <param name="currentPage"></param>
        /// <param name="totalPages"></param>
        /// <param name="pageUrl"></param>
        /// <returns></returns>
        public static MvcHtmlString BuildPageLinksFrom(this HtmlHelper html, int currentPage, int totalPages, Func<int, string> pageUrl)
        {
            var result = new StringBuilder();

            for (int i = 1; i <= totalPages; i++)
            {
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                tag.AddCssClass(i == currentPage ? "selected" : "notselected");

                result.AppendLine(tag.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}