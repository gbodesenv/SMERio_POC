using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace Rio.SME.Web.App_Start
{
    public class JSModelTransform : IBundleTransform
    {
        /// <summary>
        /// Metodo que será chamado para processar o bundle
        /// </summary>
        /// <param name="context">Contexto do Bundle</param>
        /// <param name="response">Objeto do Response do Bundle que será preenchido a propriedade Content com a String contendo o JS que será gerado</param>
        public void Process(BundleContext context, BundleResponse response)
        {
            string strResponse = string.Empty;
            response.ContentType = "text/javascript";

            JSModelBundle bundle = (context.BundleCollection
                .Where(b => b.Path == context.BundleVirtualPath)
                .FirstOrDefault()) as JSModelBundle;


            //namespace onde ficará os ViewModels
            string @namespace = typeof(Rio.SME.Web.Models.INamespaceMarker).Namespace;
            //busca todas as classes desse namespace
            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == @namespace
                    select t;
            //adiciona todos os types a lista de models
            bundle.ModelList.AddRange(q.ToList());

            foreach (Type type in bundle.ModelList)
            {
                if (type.IsClass)
                {
                    strResponse += GetFunctionBodyForClass(type);
                }
                else if (type.IsEnum)
                {
                    strResponse += GetObjectBodyForEnum(type);
                }
            }

            response.Content = strResponse;
        }

        /// <summary>
        /// Método para transformar classes do C# pro JS
        /// </summary>
        /// <param name="type">Tipo da classe</param>
        /// <returns>String com a classe do JS</returns>
        string GetFunctionBodyForClass(Type type)
        {
            StringBuilder sbFunctionBody = new StringBuilder();
            sbFunctionBody.AppendLine("App.Models." + type.Name + " = (function() {");
            sbFunctionBody.AppendLine("    return {");

            foreach (PropertyInfo p in type.GetProperties())
            {
                sbFunctionBody.AppendLine("        " + p.Name + ": '',");
            }
            sbFunctionBody.AppendLine("    };");
            sbFunctionBody.Append("})");
            sbFunctionBody.Append("\n\n");
            return sbFunctionBody.ToString();
        }

        /// <summary>
        /// Método para transformar Enum em JS
        /// </summary>
        /// <param name="type">Tipo do Enum</param>
        /// <returns>String com o Enum gerado em JS</returns>
        string GetObjectBodyForEnum(Type type)
        {
            StringBuilder sbFunctionBody = new StringBuilder();
            sbFunctionBody.AppendLine(type.Name + " = {");

            int enumLength = type.GetEnumValues().Length;
            int index = 1;

            foreach (var v in type.GetEnumValues())
            {
                string strEnumField = v.ToString() + " : " + (int)v;
                if (index > enumLength)
                    strEnumField += ",";
                sbFunctionBody.AppendLine(strEnumField);
                index++;
            }

            sbFunctionBody.Append("}");
            sbFunctionBody.Append("\n\n");
            return sbFunctionBody.ToString();
        }
    }
}