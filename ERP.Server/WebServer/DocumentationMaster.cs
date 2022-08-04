using ERP.BaseLib.Objects;
using ERP.BaseLib.Serialization;
using ERP.Commands.Base;
using ERP.Parsing.Parser;
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
        private static string style = @"
  <style>
      table.minimalistBlack {
      font-family: ""Lucida Sans Unicode"", ""Lucida Grande"", sans-serif;
      border: 3px solid #000000;
      width: 100%;
      text-align: left;
      border-collapse: collapse;
    }
    table.minimalistBlack td, table.minimalistBlack th {
      border: 1px solid #000000;
      padding: 5px 4px;
    }
    table.minimalistBlack tbody td {
      font-size: 13px;
    }
    table.minimalistBlack thead {
      background: #CFCFCF;
      background: -moz-linear-gradient(top, #dbdbdb 0%, #d3d3d3 66%, #CFCFCF 100%);
      background: -webkit-linear-gradient(top, #dbdbdb 0%, #d3d3d3 66%, #CFCFCF 100%);
      background: linear-gradient(to bottom, #dbdbdb 0%, #d3d3d3 66%, #CFCFCF 100%);
      border-bottom: 3px solid #000000;
    }
    table.minimalistBlack thead th {
      font-size: 15px;
      font-weight: bold;
      color: #000000;
      text-align: left;
    }
  </style>";

        public static string GetDocumentationPage(string absolutePath)
        {
            if (absolutePath.StartsWith("/")) { absolutePath = absolutePath.Substring(1); }

            string top = @$"
<!doctype html>
<html>
  <head>
    <title>API-Documentation</title>
  </head>
  {style}
  <body>
    <div>
";
            string middle = @$"
      <table class=""minimalistBlack"">
      <thead>
      <tr>
      <th>Namespace</th>
      <th>CommandCollection</th>
      <th>Action</th>
      <th>Paramters</th>
      </tr>
      </thead>
      <tbody>";

            string bottom = @"
      </tbody>
      </table>
    </div>
  </body>
</html>
";

            List<string> splitted = absolutePath.Split('/').ToList();

            string _Namespace = splitted.Count() > 0 ? splitted[0] : "";
            string _CommandCollection = splitted.Count() > 1 ? splitted[1] : "";
            string _Action = splitted.Count() > 2 ? splitted[2] : "";

            IEnumerable<Type> collections = CommandMaster.CommandCollectionTypes.Where(o => o.Namespace?.Replace(CommandCollection.ParentNamespace + ".", "").EndsWith(_Namespace) == true);

            if (_CommandCollection != string.Empty)
            {
                collections = collections.Where(o => o.Name.Equals(_CommandCollection) || o.Name.Replace("CC_", "").Equals(_CommandCollection));
            }

            List<(string Namespace, string CollectionName, string MethodName, string Parameters)> list = new();

            foreach (Type collection in collections)
            {
                foreach (MethodInfo mi in collection.GetMethods())
                {
                    if (mi.ReturnType == typeof(Result) && mi.IsPublic)
                    {
                        string methodname = mi.Name;
                        string parameters = mi.GetParameters().Length.ToString();
                        string collectionname = collection.Name.Replace("CC_", "");

                        list.Add((collection.Namespace.Replace(CommandCollection.ParentNamespace + ".", ""), collectionname, methodname, parameters));
                    }
                }
            }

            foreach ((string Namespace, string CollectionName, string MethodName, string Parameters) item in list.OrderBy(o => o.Namespace).ThenBy(o => o.CollectionName).ThenBy(o => o.MethodName))
            {
                string linknamespace = item.Namespace == _Namespace ? item.Namespace : @$"<a href=""/{item.Namespace}"">{item.Namespace}</a>";
                string linkcollection = item.CollectionName == _CommandCollection ? item.CollectionName : @$"<a href=""{item.Namespace}/{item.CollectionName}"">{item.CollectionName}</a>";
                string linkaction = item.MethodName == _Action ? item.MethodName : @$"<a href=""/{item.Namespace}/{item.CollectionName}/{item.MethodName}"">{item.MethodName}</a>";
                string append = @$"      <tr>
    <td>{linknamespace}</td><td>{linkcollection}</td><td>{linkaction}</td><td>{item.Parameters}</td></tr>
    <tr>";
                middle += append;
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
            string alink = link == "" && _Namespace == "" && _CommandCollection == "" && _Action == "" ? "Unfiltered list" : @$"<a href=""/{link}"">{display}</a>";
            top += @$"<table class=""minimalistBlack""><tbody><tr><td>{alink}</td><td>Namespace: <b>{currentNamespace}</b></td><td>CommandCollection: <b>{currentcollection}</b></td><td>Action: <b>{currentaction}</b></td></tr></tbody></table></br>";
            if (!_Action.Equals(string.Empty))
            {
                middle = @$"
    <form action=""/{_Namespace}/{_CommandCollection}/{_Action}"" method=""POST"" target=""_blank"" enctype=""text/plain"">
    <input name=""Namespace"" type=""hidden"" value=""{_Namespace}""/>
    <input name=""CommandCollection"" type=""hidden"" value=""{_CommandCollection}""/>
    <input name=""Action"" type=""hidden"" value=""{_Action}""/>
    <table class=""minimalistBlack"">
    <thead>
    <tr>
    <th>ParameterTypeNamespace</th>
    <th>ParameterType</th>
    <th>ParameterName</th>
    <th>ParameterValue</th>
    </tr>
    </thead>
    <tbody>";
                if (collections.Count() == 1)
                {
                    Type Col = collections.First();
                    IEnumerable<MethodInfo> milist = Col.GetMethods().Where(o => (o.Name.Replace("CC_", "").Equals(_Action) || o.Name.Equals(_Action)) && o.IsPublic && o.ReturnType == typeof(Result));
                    if (milist.Any())
                    {
                        MethodInfo MI = milist.First();
                        foreach (ParameterInfo PI in MI.GetParameters())
                        {
                            IParser Parser = ParsingMaster.GetParser(PI.ParameterType, typeof(string));
                            string Default = Parser?.GetDefault(PI.ParameterType).ToString();
                            string append = @$"      <tr>
    <td>{PI.ParameterType.Namespace}</td><td>{PI.ParameterType.Name}</td><td>{PI.Name}</td><td><input name=""{PI.Name}"" type=""text"" style=""width: 97%;"" value=""{Default}""/></td></tr>
    <tr>";
                            middle += append;
                        }
                    }
                }

                middle += @$"      <tr>
    <td></td><td></td><td></td><td><input type=""submit"" style=""width: 97%;""/></td></tr>
    <tr>";
                middle += "</form>";
            }

            return top + middle + bottom;
        }

        public static string GetDocumentationRequlstPage(Result result)
        {
            string top = @$"
<!doctype html>
<html>
  <head>
    <title>API-Documentation</title>
  </head>
  {style}
  <body>
    <div>
";
            string middle = @$"
      <table class=""minimalistBlack"">
      <thead>
      <tr>
      <th>Error</th>
      <th>ErrorType</th>
      <th>ErrorMessage</th>
      <th>ReturnValue</th>
      </tr>
      </thead>
      <tbody>";

            string bottom = @"
      </tbody>
      </table>
    </div>
  </body>
</html>
";
            Func<string, string> GetReady = input =>
            {
                if (string.IsNullOrEmpty(input))
                {
                    return input;
                }
                return input.Replace("\"", "<b>\"</b>").Replace("{", "<b>{</b>").Replace("}", "<b>}</b>").Replace("[", "<b>[</b>").Replace("]", "<b>]</b>").Replace(",", "<b>,</b>");
            };

            string color = result.Error ? "red" : "#28B463";

            middle += @$"      <tr style=""color: {color};"">
    <td>{GetReady(result.Error.ToString())}</td><td>{GetReady(result.ErrorType?.ToString())}</td><td>{GetReady(result.ErrorMessage)}</td><td>{GetReady(result.ReturnValue)}</td></tr>
    <tr>";

            return top + middle + bottom;
        }
    }
}