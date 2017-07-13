using System;

namespace Rio.SME.Domain.Util.ExtensionMethods
{
    public static class Formatacao
    {
        public static string FormataCPFouCPNJ(this string texto)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                texto = texto.Replace("/", "").Replace(".", "").Replace("-", "");
                if (texto.Length == 11)
                    return Convert.ToUInt64(texto).ToString(@"000\.000\.000\-00");

                return Convert.ToUInt64(texto).ToString(@"00\.000\.000\/0000\-00");
            }

            return string.Empty;
        }

        public static string FormataTamanhoMaximoTexto(this string texto, int tamanhoMaximoString)
        {
            if (!string.IsNullOrEmpty(texto) && texto.Length > tamanhoMaximoString)
            {
                return string.Concat(texto.Substring(0, tamanhoMaximoString), "...");
            }

            return texto;
        }

        /// <summary>
        /// Formata string hora para xxxx-x/xx
        /// </summary>
        /// <param name="atividadeCNAE">Objeto</param>
        /// <returns>Formata Ex:"5611-2/01"</returns>
        public static string FormatoNumeracaoCnae(this string atividadeCNAE)
        {
            atividadeCNAE = atividadeCNAE.Replace("-", "");
            atividadeCNAE = atividadeCNAE.Replace("/", "");
            atividadeCNAE = atividadeCNAE.Replace(".", "");
            return String.IsNullOrEmpty(atividadeCNAE) || atividadeCNAE.Length != 7 ? 
                "" : 
                String.Format("{0}-{1}/{2}", atividadeCNAE.Substring(0, 4), atividadeCNAE.Substring(4, 1), atividadeCNAE.Substring(5, 2));
        }

    }
}