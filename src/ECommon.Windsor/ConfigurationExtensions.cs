using Castle.Windsor;
using ECommon.Components;

namespace  ECommon.Configurations
{
    /// <summary>ENode configuration class Windsor extensions.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>Use Windsor as the object container.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseWindsor(this Configuration configuration)
        {
            return UseWindsor(configuration,new WindsorContainer());
        }
        /// <summary>Use Windsor as the object container.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseWindsor(this Configuration configuration, IWindsorContainer iocContainer)
        {
            ObjectContainer.SetContainer(new WindsorObjectContainer(iocContainer));
            return configuration;
        }
    }
}