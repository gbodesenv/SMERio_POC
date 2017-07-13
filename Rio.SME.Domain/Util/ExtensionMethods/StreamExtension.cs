using System;
using System.IO;

namespace Rio.SME.Domain.Util.ExtensionMethods
{
    public static class StreamExtension
    {
        /// <summary>
        /// Método para ler um Stream e retornar o texto
        /// </summary>
        /// <param name="stream">Objeto</param>
        /// <returns>texto lido</returns>
        public static string ToString(this Stream stream)
        {
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Copia um stream para outro.
        /// Exemplo:
        /// using(var stream = response.GetResponseStream())
        /// using(var ms = new MemoryStream())
        /// {
        ///     stream.CopyTo(ms);
        ///      // Do something
        /// }
        /// </summary>
        /// <param name="fromStream">Do stream.</param>
        /// <param name="toStream">Para stream.</param>
        public static void CopyTo(this Stream fromStream, Stream toStream)
        {
            if (fromStream == null)
                throw new ArgumentNullException("fromStream");
            if (toStream == null)
                throw new ArgumentNullException("toStream");
            var bytes = new byte[8092];
            int dataRead;
            while ((dataRead = fromStream.Read(bytes, 0, bytes.Length)) > 0)
                toStream.Write(bytes, 0, dataRead);
        }
    }
}