using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Rio.SME.Domain.Util.ExtensionMethods
{
    public static class ObjectExtension
    {
        /// <summary>
        /// Faz a cópia de um objeto.
        /// Nao copia a referencia em memoria, apenas os dados.
        /// </summary>
        /// <typeparam name="T">Tipo de retorno.</typeparam>
        /// <param name="item">Objeto copiado.</param>
        /// <returns>objeto copiado.</returns>
        public static T Clone<T>(this object item)
        {
            if (item != null)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();

                formatter.Serialize(stream, item);
                stream.Seek(0, SeekOrigin.Begin);

                T result = (T)formatter.Deserialize(stream);

                stream.Close();

                return result;
            }
            else
                return default(T);
        }
    }
}