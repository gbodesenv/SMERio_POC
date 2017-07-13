using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rio.SME.Domain.Util
{
    public class Arquivo
    {
        /// <summary>
        /// Retorna a data do arquivo mais atualizado
        /// </summary>
        /// <param name="caminho"></param>
        /// <returns></returns>
        public static DateTime BuscarDataModificacaoArquivos(String caminho)
        {
            try
            {
                DirectoryInfo pasta = new DirectoryInfo(caminho);
                List<DateTime> listaDatas = new List<DateTime>();
                List<DirectoryInfo> subDiretorios = pasta.GetDirectories().ToList();

                listaDatas.Add(pasta.GetFiles().Max(f => f.LastWriteTime));

                if (subDiretorios.Count > 0)
                {
                    foreach (var diretorio in subDiretorios)
                    {
                        if (diretorio.GetFiles().Any())
                            listaDatas.Add(diretorio.GetFiles().Max(f => f.LastWriteTime));
                    }
                }

                var ultimaModificacao = listaDatas.OrderByDescending(d => d.Ticks).First();
                return ultimaModificacao;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Verifica existência da pasta
        /// </summary>
        /// <param name="caminho"></param>
        /// <returns></returns>
        public static bool VerificarPasta(String caminho)
        {
            DirectoryInfo pasta = new DirectoryInfo(caminho);
            return pasta.Exists;
        }

        /// <summary>
        /// Cria pasta
        /// </summary>
        /// <param name="caminho">caminho da pasta a ser criada</param>
        public static void CriarPasta(String caminho)
        {
            DirectoryInfo pasta = new DirectoryInfo(caminho);

            if (!VerificarPasta(caminho))
                pasta.Create();
        }

        /// <summary>
        /// Adiciona texto no arquivo, NÃO substitui
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        /// <param name="conteudoArquivo"></param>
        /// <param name="encode"></param>
        public static void AdicionarConteudoArquivo(String caminhoArquivo, String conteudoArquivo)
        {
            var objWriter = new StreamWriter(caminhoArquivo, true, System.Text.Encoding.GetEncoding("ISO-8859-1"));

            objWriter.WriteLine(conteudoArquivo);
            objWriter.Flush();

            if (objWriter != null)
                objWriter.Close();
        }

        ///// <summary>
        ///// Converte um arquivo em array de bytes
        ///// </summary>
        ///// <param name="file">Arquivo</param>
        ///// <returns>Array de bytes do arquivo</returns>
        //public static byte[] FileToArrayBytes(HttpPostedFileBase file)
        //{
        //    MemoryStream target = new MemoryStream();
        //    file.InputStream.CopyTo(target);
        //    byte[] data = target.ToArray();
        //    return data;
        //}

        ///// <summary>
        ///// Converte um array de bytes em arquivo
        ///// </summary>
        ///// <param name="bytes">Array de bytes</param>
        ///// <param name="extensao">extensao do arquivo</param>
        ///// <param name="nome">nome do arquivo</param>
        ///// <returns>Arquivo</returns>
        ////public static Object ArrayBytesToFile(byte[] bytes, String extensao, String nome)
        ////{
        ////    MemoryStream stream = new MemoryStream(bytes);
        ////    String content = ContentType(Path.GetExtension(extensao));
        ////    return File(stream, content, nome);
        ////    //return retorno;
        ////}

        ///// <summary>
        ///// Retorna o tipo de conteúdo para adicionar no header da página
        ///// </summary>
        ///// <param name="fileExtension">extensão do arquivo a buscar o header</param>
        ///// <returns></returns>
        //public static string ContentType(string fileExtension)
        //{
        //    Dictionary<string, string> d = new Dictionary<string, string>();
        //    //Images'
        //    d.Add(".bmp", "image/bmp");
        //    d.Add(".gif", "image/gif");
        //    d.Add(".jpeg", "image/jpeg");
        //    d.Add(".jpg", "image/jpeg");
        //    d.Add(".png", "image/png");
        //    d.Add(".tif", "image/tiff");
        //    d.Add(".tiff", "image/tiff");
        //    //Documents'
        //    d.Add(".doc", "application/msword");
        //    d.Add(".docx", "application/msword");
        //    d.Add(".pdf", "application/pdf");
        //    //Slideshows'
        //    d.Add(".ppt", "application/vnd.ms-powerpoint");
        //    d.Add(".pptx", "application/vnd.ms-powerpoint");
        //    //Data'
        //    d.Add(".xlsx", "application/vnd.ms-excel");
        //    d.Add(".xls", "application/vnd.ms-excel");
        //    d.Add(".csv", "text/csv");
        //    d.Add(".xml", "text/xml");
        //    d.Add(".txt", "text/plain");
        //    //Compressed Folders'
        //    d.Add(".zip", "application/zip");
        //    //Audio'
        //    d.Add(".ogg", "application/ogg");
        //    d.Add(".mp3", "audio/mpeg");
        //    d.Add(".wma", "audio/x-ms-wma");
        //    d.Add(".wav", "audio/x-wav");
        //    //Video'
        //    d.Add(".wmv", "audio/x-ms-wmv");
        //    d.Add(".swf", "application/x-shockwave-flash");
        //    d.Add(".avi", "video/avi");
        //    d.Add(".mp4", "video/mp4");
        //    d.Add(".mpeg", "video/mpeg");
        //    d.Add(".mpg", "video/mpeg");
        //    d.Add(".qt", "video/quicktime");

        //    if (d.ContainsKey(fileExtension))
        //        return d[fileExtension];
        //    else
        //        return "a";
        //}

        /// <summary>
        /// Verifica se o arquivo existe
        /// </summary>
        /// <param name="caminhoArquivo">Caminho do arquivo</param>
        /// <returns>True - arquivo existe, False - arquivo não existe</returns>
        public static bool VerificarExistenciaArquivo(String caminhoArquivo)
        {
            FileInfo arquivo = new FileInfo(caminhoArquivo);

            if (arquivo.Exists)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Exclui arquivo da pasta
        /// </summary>
        /// <param name="localArquivo">Caminho do arquivo</param>
        public static void ExcluirArquivo(string localArquivo)
        {
            if (System.IO.File.Exists(localArquivo))
            {
                try
                {
                    System.IO.File.Delete(localArquivo);
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Excluir pasta
        /// </summary>
        /// <param name="local">Caminho da pasta</param>
        public static void ExcluirPasta(string local)
        {
            if (System.IO.Directory.Exists(local))
            {
                try
                {
                    System.IO.Directory.Delete(local);
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Retorna lista de nome dos arquivos de uma pasta
        /// </summary>
        /// <param name="caminho">Caminho da pasta</param>
        /// <returns>Lista com o nome dos arquivos</returns>
        public static List<String> ListarArquivos(String caminho)
        {
            List<String> lstArquivos = new List<String>();

            if (VerificarPasta(caminho))
            {
                DirectoryInfo pasta = new DirectoryInfo(caminho);

                foreach (var subDiretorio in pasta.GetFiles().ToList())
                {
                    lstArquivos.Add(subDiretorio.Name);
                }
            }

            return lstArquivos;
        }
    }
}
