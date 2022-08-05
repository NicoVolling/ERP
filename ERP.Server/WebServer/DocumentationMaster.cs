using ERP.BaseLib.Objects;
using ERP.BaseLib.Serialization;
using ERP.Commands.Base;
using ERP.Parsing.Parser;
using ERP.Server.WebServer.Html;
using ERP.Server.WebServer.Html.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Server.WebServer
{
    public static class DocumentationMaster
    {
        public static WebPage GetDocumentationPage(string absolutePath)
        {
            if (absolutePath.StartsWith("/documentation/")) { absolutePath = absolutePath.Substring(15); }

            List<string> splitted = absolutePath.Split('/').ToList();

            string _Namespace = splitted.Count() > 0 ? splitted[0] : "";
            string _CommandCollection = splitted.Count() > 1 ? splitted[1] : "";
            string _Action = splitted.Count() > 2 ? splitted[2] : "";

            WebPage WebPage = new WebPage().AddInnerComponent(new head().AddInnerComponent(new title().AddInnerText("API-Documentation")));
            WebPage.AddInnerComponent(style.DocumentationStyle);
            body body = new();
            div div = new();
            WebPage.AddInnerComponent(body);
            body.AddInnerComponent(div);

            IEnumerable<Type> collections = CommandMaster.CommandCollectionTypes.Where(o => o.Namespace?.Replace(CommandCollection.ParentNamespace + ".", "").EndsWith(_Namespace) == true);

            if (_CommandCollection != string.Empty)
            {
                collections = collections.Where(o => o.Name.Equals(_CommandCollection) || o.Name.Replace("CC_", "").Equals(_CommandCollection));
            }

            List<(string Namespace, string CollectionName, string MethodName, string ReturnType, string Parameters)> list = new();

            foreach (Type collection in collections)
            {
                foreach (MethodInfo mi in collection.GetMethods())
                {
                    if (mi.ReturnType.BaseType == typeof(Result) && mi.IsPublic)
                    {
                        string methodname = mi.Name;
                        string parameters = mi.GetParameters().Length.ToString();
                        string collectionname = collection.Name.Replace("CC_", "");
                        string returntype = "";

                        if(mi.ReturnType.GenericTypeArguments.First() is Type type) 
                        {
                            returntype = GetReturnTypeFormatted(type);
                        }

                        list.Add((collection.Namespace.Replace(CommandCollection.ParentNamespace + ".", ""), collectionname, methodname, returntype, parameters));
                    }
                }
            }

            string link = "";
            string display = "";
            if (_CommandCollection == String.Empty)
            {
                string[] nslist = _Namespace.Split('.');
                link = nslist.Length > 1 ? string.Join(".", nslist.Take(nslist.Length - 1)) : "";
                display = link == String.Empty ? "See all Namespaces" : link;
            }
            else if (_Action == string.Empty)
            {
                link = _Namespace;
                display = $"See all CommandCollections in <b>{_Namespace}</b>";
            }
            else
            {
                link = $"{_Namespace}/{_CommandCollection}";
                display = $"See all Actions in <b>{_CommandCollection}</b>";
            }
            string currentNamespace = _Namespace == string.Empty ? $"All Namespaces ({collections.Count()} | {list.Count})" : $"{_Namespace} ({collections.Count()} | {list.Count})";
            string currentcollection = _CommandCollection == string.Empty ? $"All CommandCollections ({list.Count})" : $"{_CommandCollection} ({list.Count})";
            string currentaction = _Action == string.Empty ? $"All Actions" : $"{_Action}";
            WebComponent alink = link == "" && _Namespace == "" && _CommandCollection == "" && _Action == "" ? new p().AddInnerText("Unfiltered list") : new a().SetLink($"/documentation/{link}").AddInnerText(display);
            table toptable = new table();
            toptable.AddAttribute("class", "minimalistBlack");
            div.AddInnerComponent(toptable);
            toptable.AddDataRow(new tr()
                .AddColumn(new td().AddInnerComponent(alink))
                .AddColumn(new td().AddInnerComponent(new p().AddInnerText($"Namespace: <b>{currentNamespace}</b>")))
                .AddColumn(new td().AddInnerComponent(new p().AddInnerText($"CommandCollection: <b>{currentcollection}</b>")))
                .AddColumn(new td().AddInnerComponent(new p().AddInnerText($"Action: <b>{currentaction}</b>")))
                );
            div.AddInnerComponent(new br());

            if (_Action.Equals(string.Empty))
            {
                div.AddInnerComponent(GetActionListPage(_Namespace, _CommandCollection, _Action, list));
            }
            else if (!_Action.Equals(string.Empty))
            {
                div.AddInnerComponent(GetSingleActionPage(_Namespace, _CommandCollection, _Action, collections));
            }

            return WebPage;
        }

        private static string GetReturnTypeFormatted(Type type)
        {
            string returntype = "";
            if (type.GenericTypeArguments.Length < 1)
            {
                returntype = type.Name;
            }
            else
            {
                returntype = string.Concat(type.Name.TakeWhile(o => o != '`'));
                returntype += "&#60;";
                foreach (Type tp in type.GenericTypeArguments)
                {
                    if (!returntype.EndsWith("&#60;")) { returntype += ", "; }
                    returntype += tp.Name;
                }
                returntype += "&#62;";
            }
            return returntype;
        }

        public static WebPage GetDocumentationRequestPage(Result result)
        {
            WebPage WebPage = new WebPage().AddInnerComponent(new head().AddInnerComponent(new title().AddInnerText("API-Documentation")));
            WebPage.AddInnerComponent(style.DocumentationStyle);
            body body = new();
            div div = new();
            WebPage.AddInnerComponent(body);
            body.AddInnerComponent(div);

            table table = new table();
            table.AddAttribute("class", "minimalistBlack");
            thead thead = new thead();
            table.AddColumns(new tr()
                .AddColumn(new th().AddInnerText("Error"))
                .AddColumn(new th().AddInnerText("ErrorType"))
                .AddColumn(new th().AddInnerText("ErrorMessage"))
                .AddColumn(new th().AddInnerText("ReturnValueType"))
                .AddColumn(new th().AddInnerText("ReturnValue"))
                );

            Func<string, string> GetReady = input =>
            {
                if (string.IsNullOrEmpty(input))
                {
                    return input;
                }
                string result = "";
                int cnt = 0;
                foreach (char chr in input)
                {
                    string newchr = "";
                    switch (chr)
                    {
                        case '\"':
                            newchr = "<span style=\"color: darkmagenta; \">\"</span>";
                            break;

                        case ',':
                            newchr = "<span style=\"color: darkmagenta; \">,</span><br/>" + string.Concat(Enumerable.Repeat("&nbsp;", cnt * 4));
                            break;

                        case '{':
                            cnt++;
                            newchr = "<span style=\"color: darkmagenta; \">{</span><br/>" + string.Concat(Enumerable.Repeat("&nbsp;", cnt * 4));
                            break;

                        case '}':
                            cnt--;
                            newchr = "<br/>" + string.Concat(Enumerable.Repeat("&nbsp;", cnt * 4)) + "<span style=\"color: darkmagenta; \">}</span>";
                            break;

                        case '[':
                            cnt++;
                            newchr = "<span style=\"color: darkmagenta; \">[</span><br/>" + string.Concat(Enumerable.Repeat("&nbsp;", cnt * 4));
                            break;

                        case ']':
                            cnt--;
                            newchr = "<br/>" + string.Concat(Enumerable.Repeat("&nbsp;", cnt * 4)) + "<span style=\"color: darkmagenta; \">]</span>";
                            break;

                        default:
                            newchr = chr.ToString();
                            break;
                    }
                    result += newchr;
                }
                return $"<span style=\"color: darkblue; \">{result}</span>";
                return input.Replace("\"", "<b>\"</b>").Replace("{", "<b>{</b><br/>").Replace("}", "<br/><b>}</b>").Replace("[", "<b>[</b><br/>").Replace("]", "<br/><b>]</b>").Replace(",", "<b>,</b><br/>");
            };

            string color = result.Error ? "darkred" : "darkgreen";

            table.AddAttribute("style", $"border-color: {color};").AddDataRow(new tr()
                .AddColumn(new td().AddInnerText(GetReady(result.Error.ToString())))
                .AddColumn(new td().AddInnerText(GetReady(result.ErrorType?.ToString())))
                .AddColumn(new td().AddInnerText(GetReady(result.ErrorMessage)))
                .AddColumn(new td().AddInnerText(GetReady(result.ReturnValueType.Replace("<", "&#60;").Replace(">", "&#62;"))))
                .AddColumn(new td().AddInnerText(GetReady(result.ReturnValue)))
                );
            div.AddInnerComponent(table);

            return WebPage;
        }

        private static WebComponent GetActionListPage(string _Namespace, string _CommandCollection, string _Action, List<(string Namespace, string CollectionName, string MethodName, string ReturnType, string Parameters)> CommandCollectionList)
        {
            table table = new table();
            table.AddAttribute("class", "minimalistBlack");
            thead thead = new thead();
            table.AddColumns(new tr()
                .AddColumn(new th().AddInnerText("Namespace"))
                .AddColumn(new th().AddInnerText("CommandCollection"))
                .AddColumn(new th().AddInnerText("Action"))
                .AddColumn(new th().AddInnerText("ReturnType"))
                .AddColumn(new th().AddInnerText("Paramters"))
                );

            foreach ((string Namespace, string CollectionName, string MethodName, string ReturnType, string Parameters) item in CommandCollectionList.OrderBy(o => o.Namespace).ThenBy(o => o.CollectionName).ThenBy(o => o.MethodName))
            {
                WebComponent wcnamespace = item.Namespace == _Namespace ? new WebComponentText().AddInnerText(item.Namespace) : new a().SetLink($"/documentation/{item.Namespace}").AddInnerText(item.Namespace);
                WebComponent wccollection = item.CollectionName == _CommandCollection ? new WebComponentText().AddInnerText(item.CollectionName) : new a().SetLink($"/documentation/{item.Namespace}/{item.CollectionName}").AddInnerText(item.CollectionName);
                WebComponent wcnaction = item.MethodName == _Action ? new WebComponentText().AddInnerText(item.MethodName) : new a().SetLink($"/documentation/{item.Namespace}/{item.CollectionName}/{item.MethodName}").AddInnerText(item.MethodName);
                table.AddDataRow(new tr()
                    .AddColumn(new td().AddInnerComponent(wcnamespace))
                    .AddColumn(new td().AddInnerComponent(wccollection))
                    .AddColumn(new td().AddInnerComponent(wcnaction))
                    .AddColumn(new td().AddInnerText(item.ReturnType))
                    .AddColumn(new td().AddInnerText(item.Parameters))
                    );
            }
            return table;
        }

        private static WebComponent GetSingleActionPage(string _Namespace, string _CommandCollection, string _Action, IEnumerable<Type> CommandCollectionTypeList)
        {
            form form = new form();
            form.AddAttribute("action", $"/documentation/{_Namespace}/{_CommandCollection}/{_Action}");
            form.AddAttribute("method", "POST");
            form.AddAttribute("target", "_blank");
            form.AddAttribute("enctype", "text/plain");
            input inputnamespace = new input().AddAttribute("name", "Namespace").AddAttribute("type", "hidden").AddAttribute("value", _Namespace);
            input inputcollection = new input().AddAttribute("name", "CommandCollection").AddAttribute("type", "hidden").AddAttribute("value", _CommandCollection);
            input inputaction = new input().AddAttribute("name", "Action").AddAttribute("type", "hidden").AddAttribute("value", _Action);

            form.AddInnerComponent(inputnamespace);
            form.AddInnerComponent(inputcollection);
            form.AddInnerComponent(inputaction);

            table table = new table();
            form.AddInnerComponent(table);
            table.AddAttribute("class", "minimalistBlack");
            table.AddColumns(new tr()
                .AddColumn(new th().AddInnerText("ParameterTypeNamespace"))
                .AddColumn(new th().AddInnerText("ParameterType"))
                .AddColumn(new th().AddInnerText("ParameterName"))
                .AddColumn(new th().AddInnerText("ParameterValue"))
                );

            if (CommandCollectionTypeList.Count() == 1)
            {
                Type Col = CommandCollectionTypeList.First();
                IEnumerable<MethodInfo> milist = Col.GetMethods().Where(o => (o.Name.Replace("CC_", "").Equals(_Action) || o.Name.Equals(_Action)) && o.IsPublic && o.ReturnType.BaseType == typeof(Result));
                if (milist.Any())
                {
                    MethodInfo MI = milist.First();
                    foreach (ParameterInfo PI in MI.GetParameters())
                    {
                        IParser Parser = ParsingMaster.GetParser(PI.ParameterType, typeof(string));
                        string Default = Parser?.GetDefault(PI.ParameterType).ToString();

                        table.AddDataRow(new tr()
                            .AddColumn(new td().AddInnerText(PI.ParameterType.Namespace))
                            .AddColumn(new td().AddInnerText(PI.ParameterType.Name))
                            .AddColumn(new td().AddInnerText(PI.Name))
                            .AddColumn(new td().AddInnerComponent(new input().AddAttribute("name", PI.Name).AddAttribute("type", "text").AddAttribute("value", Default)))
                            );
                    }
                }
            }

            table.AddDataRow(new tr()
                            .AddColumn(new td())
                            .AddColumn(new td())
                            .AddColumn(new td())
                            .AddColumn(new td().AddInnerComponent(new input().AddAttribute("type", "submit")))
                            );
            return form;
        }
    }
}