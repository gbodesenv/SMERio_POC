using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rio.SME.Domain.Entities.DTO
{
    [Serializable]
    public class SistemaWebCoreSSO
    {
        public string Titulo { get; set; }
        public string UrlCoreSSO { get; set; }
        public string Matricula { get; set; }
        public string UrlInstituicao { get; set; }
        public string MensagemCopyright { get; set; }
        public string helpDeskContato { get; set; }
    }
}
