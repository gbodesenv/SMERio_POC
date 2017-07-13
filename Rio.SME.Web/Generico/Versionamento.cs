using Rio.SME.Domain.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Rio.SME.Web.Generico
{
    public static class Versionamento
    {
        public static string BuscarUltimaModificacao()
        {
            if (HttpContext.Current.Cache["ultimaModificacao"] != null)
                return HttpContext.Current.Cache["ultimaModificacao"].ToString();

            string pastaScripts = HttpContext.Current.Server.MapPath("~/Scripts/");
            string pastaViews = HttpContext.Current.Server.MapPath("~/Views/");
            string pastaController = HttpContext.Current.Server.MapPath("~/bin/");

            List<DateTime> dateTimes = new List<DateTime>();
            dateTimes.Add(Arquivo.BuscarDataModificacaoArquivos(pastaScripts));
            dateTimes.Add(Arquivo.BuscarDataModificacaoArquivos(pastaViews));
            dateTimes.Add(Arquivo.BuscarDataModificacaoArquivos(pastaController));

            var ultimaModificacao = dateTimes.OrderByDescending(d => d.Ticks).First().ToString("dd/MM/yyyy HH:mm");
            HttpContext.Current.Cache.Insert("ultimaModificacao", ultimaModificacao, null, DateTime.Now.AddDays(7), Cache.NoSlidingExpiration);
            return ultimaModificacao;
        }
    }
}