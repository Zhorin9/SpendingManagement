using SpendingManagement.Core.ViewModels;
using System;
using System.Text;
using System.Web.Mvc;

namespace SpendingManagement.Core.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int,string> pageUrl)
        {
            bool muchPages = false;
            int startPage = 1;
            int endPage = pagingInfo.TotalPages;
            StringBuilder result = new StringBuilder();
            TagBuilder tag = new TagBuilder("a");
            if (pagingInfo.TotalPages > 5) { muchPages = true; }

            if (muchPages)
            { 
                if (pagingInfo.CurrentPage > 2)                     //First page
                {
                    tag.MergeAttribute("href", pageUrl(1));
                    tag.InnerHtml = "Pierwsza";
                    tag.AddCssClass("btn btn-default");
                    result.Append(tag.ToString());
                }

                if(pagingInfo.CurrentPage - 1 >= 1)
                {
                    if(pagingInfo.CurrentPage - 2 > 1)                    
                        startPage = pagingInfo.CurrentPage - 2;                    
                    else                    
                        startPage = pagingInfo.CurrentPage - 1;
                }
                else
                {
                    startPage = pagingInfo.CurrentPage;
                }


                if (pagingInfo.CurrentPage + 1 <= pagingInfo.TotalPages)
                {
                    if (pagingInfo.CurrentPage + 2 < pagingInfo.TotalPages)
                        endPage = pagingInfo.CurrentPage + 2;
                    else
                        endPage = pagingInfo.CurrentPage + 1;
                }
                else
                {
                    endPage = pagingInfo.CurrentPage;
                }              
                
            }

            for(int i = startPage; i <= endPage; i++)
            {
                tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if(i== pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("text-white");
                    tag.AddCssClass("navbar-inverse");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            
            if (muchPages)                                                  //Last page
            {
                if (pagingInfo.CurrentPage < pagingInfo.TotalPages - 1)
                {
                    tag = new TagBuilder("a");
                    tag.MergeAttribute("href", pageUrl(pagingInfo.TotalPages));
                    tag.InnerHtml = "Ostatnia";
                    tag.AddCssClass("btn btn-default");
                    result.Append(tag.ToString());
                }
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}