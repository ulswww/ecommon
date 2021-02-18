using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ECommon.Components;

namespace ECommon.Configurations
{
    /// <summary>Windsor implementation of IObjectContainer.
    /// </summary>
    public class WindsorObjectContainer : IObjectContainer
    {
       
        private IWindsorContainer _container;

        /// <summary>Default constructor.
        /// </summary>
        public WindsorObjectContainer():this(new WindsorContainer())
        {
        }
        /// <summary>Parameterized constructor.
        /// </summary>
        public WindsorObjectContainer(IWindsorContainer container)
        {
            _container = container;
        }



        /// <summary>Build the container.
        /// </summary>
        public void Build()
        {
           
        }

        /// <summary>Register a implementation type.
        /// </summary>
        /// <param name="implementationType">The implementation type.</param>
        /// <param name="serviceName">The service name.</param>
        /// <param name="life">The life cycle of the implementer type.</param>
        public void RegisterType(Type implementationType, string serviceName = null, LifeStyle life = LifeStyle.Singleton)
        {

            ComponentRegistration<object> registration = Component.For(implementationType);

            if (serviceName != null)
            {
                registration = registration.Named(serviceName);
            }
            else
            {
                registration = registration.NamedAutomatically(implementationType.ToString());
            }

            if (life != LifeStyle.Singleton)
            {
                registration= registration.LifestyleTransient();
            }

            _container.Register(registration.IsDefault());
        }
        /// <summary>Register a implementer type as a service implementation.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <param name="implementationType">The implementation type.</param>
        /// <param name="serviceName">The service name.</param>
        /// <param name="life">The life cycle of the implementer type.</param>
        public void RegisterType(Type serviceType, Type implementationType, string serviceName = null, LifeStyle life = LifeStyle.Singleton)
        {


            ComponentRegistration<object> registration = Component.For(serviceType).ImplementedBy(implementationType);


            if (serviceName != null)
            {

                registration = registration.Named(serviceName);
            }
            else
            {
                registration = registration.NamedAutomatically(implementationType.ToString());
            }

            if (life != LifeStyle.Singleton)
            {
                registration = registration.LifestyleTransient();
            }

            _container.Register(registration.IsDefault());


        }
        /// <summary>Register a implementer type as a service implementation.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <typeparam name="TImplementer">The implementer type.</typeparam>
        /// <param name="serviceName">The service name.</param>
        /// <param name="life">The life cycle of the implementer type.</param>
        public void Register<TService, TImplementer>(string serviceName = null, LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {

            ComponentRegistration<TService> registration = Component.For<TService>().ImplementedBy<TImplementer>();


            if (serviceName != null)
            {
                registration = registration.Named(serviceName);
            }
            else
            {
                registration = registration.NamedAutomatically(typeof(TImplementer).Name.ToString());
            }

            if (life != LifeStyle.Singleton)
            {
                registration = registration.LifestyleTransient();
            }

            _container.Register(registration.IsDefault());

        }

        /// <summary>Register a implementer type instance as a service implementation.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <typeparam name="TImplementer">The implementer type.</typeparam>
        /// <param name="instance">The implementer type instance.</param>
        /// <param name="serviceName">The service name.</param>
        public void RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName = null)
            where TService : class
            where TImplementer : class, TService
        {

            ComponentRegistration<TService> registration = Component.For<TService>().Instance(instance);


            if (serviceName != null)
            {

                registration = registration.Named(serviceName);
            }
            else
            {
                registration = registration.NamedAutomatically(typeof(TImplementer).Name.ToString());
            }


            registration = registration.LifestyleTransient();

            _container.Register(registration.IsDefault());
        }

        /// <summary>Resolve a service.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <returns>The component instance that provides the service.</returns>
        public TService Resolve<TService>() where TService : class
        {
            return _container.Resolve<TService>();
        }
        /// <summary>Resolve a service.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <returns>The component instance that provides the service.</returns>
        public object Resolve(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }
        /// <summary>Try to retrieve a service from the container.
        /// </summary>
        /// <typeparam name="TService">The service type to resolve.</typeparam>
        /// <param name="instance">The resulting component instance providing the service, or default(TService).</param>
        /// <returns>True if a component providing the service is available.</returns>
        public bool TryResolve<TService>(out TService instance) where TService : class
        {
            instance = null;
            try
            {
                instance = _container.Resolve<TService>();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
        /// <summary>Try to retrieve a service from the container.
        /// </summary>
        /// <param name="serviceType">The service type to resolve.</param>
        /// <param name="instance">The resulting component instance providing the service, or null.</param>
        /// <returns>True if a component providing the service is available.</returns>
        public bool TryResolve(Type serviceType, out object instance)
        {

            instance = null;
            try
            {
                instance = _container.Resolve(serviceType);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
        /// <summary>Resolve a service.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <param name="serviceName">The service name.</param>
        /// <returns>The component instance that provides the service.</returns>
        public TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            TService instance = null;
            try
            {
                instance = _container.Resolve<TService>(serviceName);

                return instance;
            }
            catch (Exception e)
            {
                return instance;
            }
        }
        /// <summary>Resolve a service.
        /// </summary>
        /// <param name="serviceName">The service name.</param>
        /// <param name="serviceType">The service type.</param>
        /// <returns>The component instance that provides the service.</returns>
        public object ResolveNamed(string serviceName, Type serviceType)
        {
            object instance = null;
            try
            {
                instance = _container.Resolve(serviceName,serviceType);

                return instance;
            }
            catch (Exception e)
            {
                return instance;
            }
          
        }
        /// <summary>Try to retrieve a service from the container.
        /// </summary>
        /// <param name="serviceName">The name of the service to resolve.</param>
        /// <param name="serviceType">The type of the service to resolve.</param>
        /// <param name="instance">The resulting component instance providing the service, or null.</param>
        /// <returns>True if a component providing the service is available.</returns>
        public bool TryResolveNamed(string serviceName, Type serviceType, out object instance)
        {

             instance = null;
            try
            {
                instance = _container.Resolve(serviceName, serviceType);

                return true;
            }
            catch (Exception e)
            {
                return false ;
            }
          
        }
    }
}
