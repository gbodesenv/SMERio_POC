using System.Web.Mvc;
using System.Web.Routing;

namespace Rio.SME.Domain.Util.Helpers
{
    public class UrlUtil
    {
        public readonly UrlHelper UrlHelper;

        public UrlUtil(RequestContext requestContext)
        {
            UrlHelper = new UrlHelper(requestContext);
        }
    }
}
