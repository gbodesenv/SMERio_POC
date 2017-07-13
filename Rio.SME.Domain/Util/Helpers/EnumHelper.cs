using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Rio.SME.Domain.Util.Helpers
{
    public static class EnumHelper
    {
        public static string GetEnumDescription<T>(string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }

        public static string Description(this Enum value)
        {
            var enumType = value.GetType();
            var field = enumType.GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length == 0 ? value.ToString() : ((DescriptionAttribute)attributes[0]).Description;
        }

        //public static bool HasEnumeration(Enum enumValue)
        //{
        //    var attr = GetAttribute<Enums.HasNumeration>(enumValue);
        //    if (attr != null)
        //        return attr._hasNumeration;

        //    return false;
        //}

        public static T GetAttribute<T>(Enum enumValue) where T : Attribute
        {
            MemberInfo memberInfo = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();

            if (memberInfo != null)
            {
                var attribute = (T)memberInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
                return attribute;
            }
            return null;
        }

        /// <summary>
        /// Obtém a descrição do enum (valor colocado no DescriptionAttribute)
        /// </summary>
        /// <param name="value">Valor do enum</param>
        /// <returns>Descrição do enum</returns>
        public static string GetDescriptionFromEnumValue(Enum value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static T Parse<T>(string value)
        {
            return EnumHelper.Parse<T>(value, true);
        }

        public static T Parse<T>(string value, bool ignoreCase)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        public static bool TryParse<T>(string value, out T returnedValue)
        {
            return EnumHelper.TryParse<T>(value, true, out returnedValue);
        }

        public static bool TryParse<T>(string value, bool ignoreCase, out T returnedValue)
        {
            try
            {
                returnedValue = (T)Enum.Parse(typeof(T), value, ignoreCase);
                return true;
            }
            catch
            {
                returnedValue = default(T);
                return false;
            }
        }

        public static List<T> EnumToList<T>()
        {
            Type enumType = typeof(T);

            // Can't use type constraints on value types, so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);

            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (var val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }

            return enumValList;
        }

        /// <summary>
        /// Retorna uma lista de todas as descrições dos valores do enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<string> GetDescriptions<T>()
        {
            var attributes = typeof(T).GetMembers()
                .SelectMany(member => member.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>())
                .ToList();

            return attributes.Select(x => x.Description);
        }

        /// <summary>
        /// Obtém uma lista de SelectListItem, onde o Value é o valor do enum e o Text é a descrição do Enum
        /// </summary>
        /// <typeparam name="T">Tipo do enum</typeparam>
        /// <returns>lista de SelectListItem</returns>
        public static List<SelectListItem> GetSelectListItemFromValueAndDescription<T>(bool opcaoVazia = false)
        {
            Type enumType = typeof(T);

            var listaRetorno = new List<SelectListItem>();

            if (opcaoVazia)
                listaRetorno.Add(new SelectListItem { Selected = true, Text = @"Selecione..." });

            // Can't use generic type constraints on value types,
            // so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new InvalidOperationException();

            Array enumValArray = Enum.GetValues(enumType);

            foreach (T val in enumValArray)
            {
                var valorAdiconar = new SelectListItem();
                FieldInfo[] fields = enumType.GetFields();
                foreach (FieldInfo field in fields)
                {
                    var descriptionAttributes = field.GetCustomAttributes(false).OfType<DescriptionAttribute>();
                    foreach (var descAttr in descriptionAttributes)
                    {
                        if (field.Name == val.ToString())
                            valorAdiconar.Text = descAttr.Description;
                    }
                }

                valorAdiconar.Value = (Convert.ToInt32(val)).ToString();
                listaRetorno.Add(valorAdiconar);
            }

            return listaRetorno;
        }
    }
}