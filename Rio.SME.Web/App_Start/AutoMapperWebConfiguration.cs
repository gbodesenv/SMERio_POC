using AutoMapper;
using System;

namespace Rio.SME.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            ConfigureDateToStringMapping();
        }

        private static void ConfigureDateToStringMapping()
        {
            Mapper.CreateMap<DateTime, string>().ConvertUsing<DateTimeToStringConverter>();
        }
    }

    public class DateTimeToStringConverter : ITypeConverter<DateTime, string>
    {
        public string Convert(ResolutionContext context)
        {
            object objDateTime = context.SourceValue;

            DateTime dateTime;

            if (objDateTime == null)
            {
                return "";
            }

            if (DateTime.TryParse(objDateTime.ToString(), out dateTime))
            {
                return dateTime.ToString("dd/MM/yyyy");
            }

            return "";
        }
    }

    public class DateTimeHourToStringConverter : ValueResolver<DateTime, string>
    {
        protected override string ResolveCore(DateTime source)
        {
            object objDateTime = source;

            DateTime dateTime;

            if (DateTime.TryParse(objDateTime.ToString(), out dateTime))
            {
                return dateTime.ToString("dd/MM/yyyy hh:mm");
            }

            return "";
        }
    }
}