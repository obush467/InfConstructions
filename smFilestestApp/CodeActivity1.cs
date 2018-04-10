using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.IO;

namespace smFilestestApp
{

    public sealed class CodeActivity1 : CodeActivity
    {
        // Определите входной аргумент действия типа string
        public InArgument<string> Name { get; set; }

        // Если действие возвращает значение, создайте класс, производный от CodeActivity<TResult>
        // и верните значение из метода Execute.
        protected override void Execute(CodeActivityContext context)
        {
            // Получите значение входного аргумента Text во время выполнения
            string name = context.GetValue(this.Name);
            

            
        }
    }
}
