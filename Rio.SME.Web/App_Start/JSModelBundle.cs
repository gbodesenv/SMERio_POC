using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Rio.SME.Web.App_Start
{
    public class JSModelBundle: Bundle
    {
        private List<Type> _modelList = new List<Type>();

        public JSModelBundle(string virtualPath)
            : base(virtualPath, new JSModelTransform())
        {
        }

        public List<Type> ModelList
        {
            get { return _modelList; }
            set { _modelList = value; }
        }
    }
}