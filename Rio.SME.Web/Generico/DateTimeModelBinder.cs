using System;
using System.Globalization;
using System.Web.Mvc;

namespace Rio.SME.Web.Generico
{
    public class DateTimeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            try
            {
                var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                var date = value.ConvertTo(typeof(DateTime), CultureInfo.CurrentCulture);

                return date;
            }
            catch (Exception)
            {
                return null;
            }


            
        }
    }
}
