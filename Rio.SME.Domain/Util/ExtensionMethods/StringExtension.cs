using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Rio.SME.Domain.Util.ExtensionMethods
{
    public static class StringExtension
    {
        /// <summary>
        /// Verifica se a string está null ou vazia
        /// </summary>
        /// <param name="str">Objeto</param>
        /// <returns>true caso null ou vazia</returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Remove o último caractere
        /// </summary>
        /// <param name="instr">Objeto</param>
        /// <returns>string sem o último caractere</returns>
        public static string RemoveLastCharacter(this String instr)
        {
            return instr.Substring(0, instr.Length - 1);
        }

        /// <summary>
        /// Remove o numero de caracteres passado a partir do ultimo
        /// </summary>
        /// <param name="instr">Objeto</param>
        /// <param name="number">Posição</param>
        /// <returns>string sem os caracteres</returns>
        public static string RemoveLast(this String instr, int number)
        {
            return instr.Substring(0, instr.Length - number);
        }

        /// <summary>
        /// Remove o primeiro caractere
        /// </summary>
        /// <param name="instr">Objeto</param>
        /// <returns>string sem o primeiro caractere</returns>
        public static string RemoveFirstCharacter(this String instr)
        {
            return instr.Substring(1);
        }

        /// <summary>
        /// Remove o numero de caracteres passado a partir do primeiro
        /// </summary>
        /// <param name="instr">Objeto</param>
        /// <param name="number">Posição</param>
        /// <returns>string sem os caracteres</returns>
        public static string RemoveFirst(this String instr, int number)
        {
            return instr.Substring(number);
        }

        /// <summary>
        /// Valida se email está válido
        /// </summary>
        /// <param name="s">Objeto</param>
        /// <returns>true se email é válido</returns>
        public static bool IsValidEmailAddress(this String s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        /// <summary>
        /// Converte string para stream
        /// </summary>
        /// <param name="str">Objeto</param>
        /// <returns>objeto stream</returns>
        public static Stream ToStream(this String str)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(str);
            return new MemoryStream(byteArray);
        }

        /// <summary>
        /// Cria estrutura de alerta para mensagem na tela
        /// </summary>
        /// <param name="mensagem">mensagem</param>
        /// <returns></returns>
        public static string ConverterScript(this string mensagem)
        {
            return string.Format(@"<script>$(function(){{ alert('{0}');}});</script>", mensagem.Replace("\n", "</br>"));
        }

        /// <summary>
        /// Remove todos os caracteres não numericos de uma string
        /// </summary>
        /// <param name="str">texto a ser limpo</param>
        /// <returns></returns>
        public static string RemoveNonNumbers(this String str)
        {
            char[] ca = str.Where(char.IsNumber).ToArray();
            return new string(ca);
        }

        /// <summary>
        /// Método para reduzir tamanho de string, acrescentando os ... (reticência)
        /// </summary>
        /// <param name="str">O texto</param>
        /// <param name="quantidadeCaracteres">A quantidade de caracteres</param>
        /// <returns>O texto modificado</returns>
        public static string ReduzirTamanhoTexto(this string str, int quantidadeCaracteres)
        {
            if (!String.IsNullOrEmpty(str))
            {
                if (str.Length > quantidadeCaracteres)
                    return str.Substring(0, quantidadeCaracteres) + "...";
            }
            return str;
        }

        public static string RemoverMascaraTelefoneCelular(this string str)
        {
            if (str.IsNullOrEmpty() == false)
            {
                str = str.Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty);
            }
            return str;
        }
        public static string RemoverMascaraCPFCNPJ(this string str)
        {
            if (str.IsNullOrEmpty() == false)
            {
                str = str.Replace(".", string.Empty).Replace("/", string.Empty).Replace("-", string.Empty);
            }
            return str;
        }

        public static bool ToBool(this string str)
        {
            if (str.IsNullOrEmpty() == false)
            {
                if (str.ToLower() == "true")
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Formata string hora para xxxxxx-xxx
        /// </summary>
        /// <param name="cep">Objeto</param>
        /// <returns>Formata Ex:"91350-222"</returns>
        public static string FormatoCEP(this string cep)
        {
            cep = cep.Replace("-", "");
            return String.IsNullOrEmpty(cep) || cep.Length != 8 ? "" : cep.Substring(0, 5) + "-" + cep.Substring(5, 3);
        }

        /// <summary>
        /// Formata string hora para xxxxxx-xxx
        /// </summary>
        /// <param name="cep">Objeto</param>
        /// <returns>Formata Ex:"91350-222"</returns>
        public static string RemoverMascaraCEP(this string cep)
        {
            if (cep.IsNullOrEmpty() == false)
                cep = cep.Replace("-", "");

            return cep.Replace("-", "");
        }

    }
}