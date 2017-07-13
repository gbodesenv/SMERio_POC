using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Rio.SME.Domain.Util
{
    public static class ArquivoMemoria
    {
        public static string FileToBase64String(HttpPostedFileBase file)
        {
            return FileToBase64String(file.InputStream);
        }

        public static string FileToBase64String(Stream inputStream)
        {
            BinaryReader binaryReader = new BinaryReader(inputStream);
            byte[] data = binaryReader.ReadBytes(Convert.ToInt32(inputStream.Length));
            return Base64Encode(data);
        }       

        /// <summary>
        /// Transforma um array de bytes em uma string de base 64
        /// </summary>
        /// <param name="data">Array de bytes a ser convertido</param>
        /// <returns></returns>
        public static string Base64Encode(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        /// <summary>
        /// Transforma uma string de base 64 em uma string normal
        /// </summary>
        /// <param name="base64EncodedData">string em Base64</param>
        /// <returns></returns>
        public static string Base64Decode(string base64EncodedData)
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);            
            string base64Decode = Base64Decode(base64EncodedBytes);

            return base64Decode;
        }
       
        /// <summary>
        /// Transforma um array de byte de base 64 em uma string normal
        /// </summary>
        /// <param name="data">array de bytes em Base64</param>
        /// <returns></returns>
        public static string Base64Decode(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        /// <summary>
        /// Retorna o tipo de conteúdo para adicionar no header da página
        /// </summary>
        /// <param name="filenameWithExtension">nome do arquivo com extensão a buscar o header</param>
        /// <returns></returns>
        public static string GetContentType(string filenameWithExtension)
        {
            var mymeType = MimeMapping.GetMimeMapping(filenameWithExtension);

            return mymeType;
        }


        /// <summary>
        /// Retorna um booleano indicando se o arquivo é uma imagem
        /// </summary>
        /// <param name="file">o arquivo de upload</param>
        /// <returns></returns>
        public static bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = { ".jpg", ".png", ".jpeg" };

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Retorna a extensão do arquivo
        /// </summary>
        /// <param name="file">o arquivo de upload</param>
        /// <returns></returns>
        public static string GetExtension(HttpPostedFileBase file)
        {
            return Path.GetExtension(file.FileName);
        }

        /// <summary>
        /// Retorna o nome do arquivo sem a extensão
        /// </summary>
        /// <param name="file">o arquivo de upload</param>
        /// <returns></returns>

        public static string GetFileNameWithoutExtension(HttpPostedFileBase file)
        {
            return Path.GetFileNameWithoutExtension(file.FileName);
        }

        /// <summary>
        /// Faz o decode da string64 em um memory stream
        /// </summary>
        /// <param name="conteudoArquivo">o conteudo do arquivo em string64</param>
        /// <returns></returns>

        public static Stream RecuperaArquivo(string conteudoArquivo)
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(conteudoArquivo);
            MemoryStream ms = new MemoryStream(base64EncodedBytes);
            return ms;
        }
    }
}