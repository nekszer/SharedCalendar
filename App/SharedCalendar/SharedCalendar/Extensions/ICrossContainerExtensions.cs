using LightForms;
using SharedCalendar.Services;

namespace SharedCalendar.Extensions
{
    public static class ICrossContainerExtensions
    {

        /// <summary>
        /// Registra una factory
        /// </summary>
        /// <typeparam name="Enum"></typeparam>
        /// <typeparam name="Interface"></typeparam>
        /// <param name="container"></param>
        public static void RegisterFactory<Enum, Interface>(this ICrossContainer container) => container.Register<IEnumFactory<Enum, Interface>, EnumFactory<Enum, Interface>>();

        /// <summary>
        /// Devuelve una implementacion para la configuracion de enum / interface
        /// </summary>
        /// <typeparam name="Enum"></typeparam>
        /// <typeparam name="Interface"></typeparam>
        /// <param name="container"></param>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static Interface CreateFactory<Enum, Interface>(this ICrossContainer container, Enum @enum) => container.Create<IEnumFactory<Enum, Interface>>().Resolve(@enum);

        /// <summary>
        /// Devuelve una implementacion para el hambiente actual
        /// </summary>
        /// <typeparam name="Interface"></typeparam>
        /// <param name="container"></param>
        /// <returns></returns>
        public static Interface CreateForEnvirontMent<Interface>(this ICrossContainer container) => container.CreateFactory<Environment, Interface>(Config.Instance.Environment);


    }
}
