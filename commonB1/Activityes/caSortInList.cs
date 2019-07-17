using System.Activities;

namespace commonB1.Activityes
{

    public sealed class CaSortInList : CodeActivity
    {
        // Определите входной аргумент действия типа string
        public InArgument<string> Text { get; set; }

        // Если действие возвращает значение, создайте класс, производный от CodeActivity<TResult>
        // и верните значение из метода Execute.
        protected override void Execute(CodeActivityContext context)
        {
            // Получите значение входного аргумента Text во время выполнения
#pragma warning disable IDE0059 // Значение, присваиваемое символу, никогда не используется
            string text = context.GetValue(this.Text);
#pragma warning restore IDE0059 // Значение, присваиваемое символу, никогда не используется
        }
    }
}
