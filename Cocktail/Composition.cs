//====================================================================================================================
// Copyright (c) 2012 IdeaBlade
//====================================================================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
// OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
//====================================================================================================================
// USE OF THIS SOFTWARE IS GOVERENED BY THE LICENSING TERMS WHICH CAN BE FOUND AT
// http://cocktail.ideablade.com/licensing
//====================================================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using Caliburn.Micro;
using IdeaBlade.Core;
using IdeaBlade.Core.Composition;
using IdeaBlade.EntityModel;
using CompositionHost = IdeaBlade.Core.Composition.CompositionHost;

#if SILVERLIGHT
using Action = System.Action;
#endif

namespace Cocktail
{
    /// <summary>
    /// Sets up a composition container and provides means to interact with the container.
    /// </summary>
    public static class Composition
    {
#if SILVERLIGHT
        private static readonly Dictionary<string, XapDownloadOperation> XapDownloadOperations =
            new Dictionary<string, XapDownloadOperation>();
#endif
        private static readonly CompositionHelper CompositionHelper = new CompositionHelper(); 

        static Composition()
        {
            EntityManager.EntityManagerCreated += OnEntityManagerCreated;
        }

        private static void OnEntityManagerCreated(object sender, EntityManagerCreatedEventArgs args)
        {
            if (!args.EntityManager.IsClient)
                return;

            var locator = new PartLocator<IAuthenticationService>(
                CreationPolicy.Shared, () => args.EntityManager.CompositionContext);
            if (locator.IsAvailable)
                args.EntityManager.AuthenticationContext = locator.GetPart().AuthenticationContext;
        }

        /// <summary>Returns the current catalog in use.</summary>
        /// <returns>Unless a custom catalog is provided through <see cref="Configure"/>, this property returns <see cref="AggregateCatalog"/></returns>
        public static ComposablePartCatalog Catalog
        {
            get { return CompositionHelper.Catalog; }
        }

        /// <summary>
        /// Returns the AggregateCatalog in use by DevForce.
        /// </summary>
        public static AggregateCatalog AggregateCatalog
        {
            get { return CompositionHelper.AggregateCatalog; }
        }

        /// <summary>Returns the CompositionContainer in use.</summary>
        public static CompositionContainer Container
        {
            get { return CompositionHelper.Container; }
        }

        /// <summary>Configures the CompositionHost.</summary>
        /// <param name="compositionBatch">
        ///     Optional changes to the <span><see cref="CompositionContainer"/></span> to include during the composition.
        /// </param>
        /// <param name="catalog">The custom catalog to be used by Cocktail to get access to MEF exports.</param>
        public static void Configure(CompositionBatch compositionBatch = null, ComposablePartCatalog catalog = null)
        {
            CompositionHelper.Configure(catalog);

            CompositionBatch batch = compositionBatch ?? new CompositionBatch();
            if (!CompositionHelper.ExportExists<IEventAggregator>())
                batch.AddExportedValue<IEventAggregator>(new EventAggregator());

            Compose(batch);
        }

        /// <summary>Executes composition on the container, including the changes in the specified <see cref="CompositionBatch"/>.</summary>
        /// <param name="compositionBatch">
        /// 	Changes to the <see cref="CompositionContainer"/> to include during the composition.
        /// </param>
        public static void Compose(CompositionBatch compositionBatch)
        {
            CompositionHelper.Compose(compositionBatch);
        }

        /// <summary>
        /// Resets the CompositionContainer to it's initial state.
        /// </summary>
        /// <remarks>
        /// After calling <see cref="Clear"/>, <see cref="Configure"/> should be called to configure the new CompositionContainer.
        /// </remarks>
        public static void Clear()
        {
            CompositionHelper.Clear();
        }

        /// <summary>
        /// 	<para>Returns an instance of the custom implementation for the provided type.</para>
        /// </summary>
        /// <typeparam name="T">Type of the requested instance.</typeparam>
        /// <param name="requiredCreationPolicy">Optionally specify whether the returned instance should be a shared, non-shared or any instance.</param>
        /// <returns>The requested instance.</returns>
        public static T GetInstance<T>(CreationPolicy requiredCreationPolicy = CreationPolicy.Any)
        {
            return CompositionHelper.GetInstance<T>(requiredCreationPolicy);
        }

        /// <summary>
        /// 	<para>Returns all instances of the custom implementation for the provided type.</para>
        /// </summary>
        /// <typeparam name="T">Type of the requested instances.</typeparam>
        /// <param name="requiredCreationPolicy">Optionally specify whether the returned instances should be shared, non-shared or any instances.</param>
        /// <returns>The requested instances.</returns>
        public static IEnumerable<T> GetInstances<T>(CreationPolicy requiredCreationPolicy = CreationPolicy.Any)
        {
            return CompositionHelper.GetInstances<T>(requiredCreationPolicy);
        }

        /// <summary>
        /// 	<para>Returns an instance of the custom implementation for the provided type or contract name.</para>
        /// </summary>
        /// <param name="serviceType">The type of the requested instance.</param>
        /// <param name="key">The contract name of the instance requested. If no contract name is specified, the type will be used.</param>
        /// <param name="requiredCreationPolicy">Optionally specify whether the returned instance should be a shared, non-shared or any instance.</param>
        /// <returns>The requested instance.</returns>
        public static object GetInstance(Type serviceType, string key,
                                         CreationPolicy requiredCreationPolicy = CreationPolicy.Any)
        {
            return CompositionHelper.GetInstance(serviceType, key, requiredCreationPolicy);
        }

        /// <summary>
        /// 	<para>Returns all instances of the custom implementation for the provided type.</para>
        /// </summary>
        /// <param name="serviceType">Type of the requested instances.</param>
        /// <param name="requiredCreationPolicy">Optionally specify whether the returned instances should be shared, non-shared or any instances.</param>
        /// <returns>The requested instances.</returns>
        public static IEnumerable<object> GetInstances(Type serviceType,
                                                       CreationPolicy requiredCreationPolicy = CreationPolicy.Any)
        {
            return CompositionHelper.GetInstances(serviceType, requiredCreationPolicy);
        }

        /// <summary>Satisfies all the imports on the provided instance.</summary>
        /// <param name="instance">The instance for which to satisfy the MEF imports.</param>
        public static void BuildUp(object instance)
        {
            // Skip if in design mode.
            if (Execute.InDesignMode)
                return;

            CompositionHelper.BuildUp(instance);
        }

#if !SILVERLIGHT5

        /// <summary>
        /// Enables full design time support for the specified EntityManager type.
        /// </summary>
        /// <typeparam name="T">The type of EntityManager needing design time support.</typeparam>
        /// <remarks>This method must be called as early as possible, usually in the bootstrapper's static constructor.</remarks>
        public static void EnableDesignTimeSupport<T>() where T : EntityManager
        {
            if (Execute.InDesignMode)
            {
                string assemblyName = typeof (T).Assembly.FullName;
                if (IdeaBladeConfig.Instance.ProbeAssemblyNames.Contains(assemblyName))
                    return;

                IdeaBladeConfig.Instance.ProbeAssemblyNames.Add(assemblyName);
            }
        }

#endif

        /// <summary>
        /// Fired when the composition container is modified after initialization.
        /// </summary>
        public static event EventHandler<RecomposedEventArgs> Recomposed
        {
            add { CompositionHost.Recomposed += value; }
            remove { CompositionHost.Recomposed -= value; }
        }

        /// <summary>
        /// Fired after <see cref="Clear"/> has been called to clear the current CompositionContainer.
        /// </summary>
        public static event EventHandler<EventArgs> Cleared
        {
            add { CompositionHelper.Cleared += value; }
            remove { CompositionHelper.Cleared -= value; }
        }

#if SILVERLIGHT

    /// <summary>Asynchronously downloads a XAP file and adds all exported parts to the catalog.</summary>
    /// <param name="relativeUri">The relative URI for the XAP file to be downloaded.</param>
    /// <param name="onSuccess">User callback to be called when operation completes successfully.</param>
    /// <param name="onFail">User callback to be called when operation completes with an error.</param>
    /// <returns>Returns a handle to the download operation.</returns>
        public static OperationResult AddXap(string relativeUri, Action onSuccess = null, Action<Exception> onFail = null)
        {
            XapDownloadOperation operation;
            if (XapDownloadOperations.TryGetValue(relativeUri, out operation) && !operation.HasError)
                return operation.AsOperationResult();

            var op = XapDownloadOperations[relativeUri] = new XapDownloadOperation(relativeUri);
            op.WhenCompleted(
                args =>
                {
                    if (args.Error == null && onSuccess != null)
                        onSuccess();

                    if (args.Error != null && onFail != null)
                    {
                        args.IsErrorHandled = true;
                        onFail(args.Error);
                    }
                });
            return op.AsOperationResult();
        }

#endif

        internal static void EnsureRequiredProbeAssemblies()
        {
            IdeaBladeConfig.Instance.ProbeAssemblyNames.Add(typeof (EntityManagerProvider<>).Assembly.FullName);
        }

        internal static IEnumerable<Export> GetExportsCore(Type serviceType, string key, CreationPolicy policy)
        {
            return CompositionHelper.GetExportsCore(serviceType, key, policy);
        }

        internal static bool ExportExists<T>()
        {
            return CompositionHelper.ExportExists<T>();
        }

        internal static bool IsRecomposing { get; set; }
    }

#if SILVERLIGHT

    internal class XapDownloadOperation : INotifyCompleted
    {
        private readonly DynamicXap _xap;
        private XapDownloadCompletedEventArgs _completedEventArgs;
        private Action<INotifyCompletedArgs> _notifyCompletedActions;

        public XapDownloadOperation(string xapUri)
        {
            _xap = new DynamicXap(new Uri(xapUri, UriKind.Relative));
            _xap.Loaded += (s, args) => XapDownloadCompleted(args);
        }

        private void XapDownloadCompleted(DynamicXapLoadedEventArgs args)
        {
            Exception error = null;
            if (!args.HasError)
            {
                Composition.IsRecomposing = true;
                try
                {
                    CompositionHost.Add(_xap);
                }
                catch (Exception e)
                {
                    error = e;
                }
                finally
                {
                    Composition.IsRecomposing = false;
                }
            }

            _completedEventArgs = new XapDownloadCompletedEventArgs(args.Cancelled, args.Error ?? error);

            CallCompletedActions();
        }

        protected void CallCompletedActions()
        {
            Action<INotifyCompletedArgs> actions = _notifyCompletedActions;
            _notifyCompletedActions = null;
            if (actions == null) return;
            actions(_completedEventArgs);
        }

    #region Implementation of INotifyCompleted

        /// <summary>
        /// Action to be performed when the asynchronous operation completes.
        /// </summary>
        /// <param name="completedAction"/>
        public void WhenCompleted(Action<INotifyCompletedArgs> completedAction)
        {
            if (completedAction == null) return;
            if (_completedEventArgs != null)
            {
                completedAction(_completedEventArgs);
                return;
            }
            _notifyCompletedActions =
                (Action<INotifyCompletedArgs>)Delegate.Combine(_notifyCompletedActions, completedAction);
        }

        /// <summary>
        /// Returns whether the operation completed successfully
        /// </summary>
        public bool CompletedSuccessfully
        {
            get { return _completedEventArgs != null && !_completedEventArgs.HasError; }
        }

        /// <summary>
        /// Returns whether the operation failed.
        /// </summary>
        public bool HasError
        {
            get { return _completedEventArgs != null && _completedEventArgs.HasError; }
        }

        /// <summary>
        /// The exception if the action failed.
        /// </summary>
        public Exception Error
        {
            get { return _completedEventArgs != null ? _completedEventArgs.Error : null; }
        }

    #endregion
    }

    internal class XapDownloadCompletedEventArgs : EventArgs, INotifyCompletedArgs
    {
        private readonly bool _cancelled;
        private readonly Exception _error;
        //private readonly DynamicXapLoadedEventArgs _dynamicXapLoadedEventArgs;

        public XapDownloadCompletedEventArgs(bool cancelled, Exception error)
        {
            _cancelled = cancelled;
            _error = error;
            //_dynamicXapLoadedEventArgs = dynamicXapLoadedEventArgs;
        }

    #region Implementation of INotifyCompletedArgs

        /// <summary>
        /// The exception if the action failed.
        /// </summary>
        public Exception Error
        {
            get { return _error; /*_dynamicXapLoadedEventArgs.Error;*/ }
        }

        /// <summary>
        /// Whether the action was cancelled.
        /// </summary>
        public bool Cancelled
        {
            get { return _cancelled; /*_dynamicXapLoadedEventArgs.Cancelled;*/ }
        }

        /// <summary>
        /// Returns whether the operation failed.
        /// </summary>
        public bool HasError { get { return _error != null; /*_dynamicXapLoadedEventArgs.HasError;*/ } }

        /// <summary>
        /// Whether the error was handled.
        /// </summary>
        public bool IsErrorHandled { get; set; }

    #endregion
    }

#endif
}