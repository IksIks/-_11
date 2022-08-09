using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ДЗ_11.Services
{
    /// <summary>
    /// Класс для расширения разметки с привязкой ENUM 
    /// </summary>
    internal class BindEnumClass: MarkupExtension
    {
        public Type MyEnum { get; private set; }

        public BindEnumClass(Type myEnum)
        {
            if (myEnum == null || !myEnum.IsEnum)
                throw new Exception("fucking ENUM");
            MyEnum = myEnum;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(MyEnum);
        }
    }
}
