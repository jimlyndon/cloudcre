using System;
using System.Web.Mvc;
using Cloudcre.Model.Queue;

namespace Cloudcre.Web.Models
{
    public class QueueModelBinder : IModelBinder
    {
        private const string CartSessionKey = "_queue";

        #region IModelBinder Members

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Some modelbinders can update properties on existing model instances.  This one doesn't 
            // need to - it's only  used to supply action method parameters.
            if (bindingContext.Model != null)
                throw new InvalidOperationException("Cannot update instances");            

            // Return the queue from Session (creating it first is necessary)
            if (controllerContext.HttpContext.Session == null)
                throw new ArgumentNullException(string.Format("Session is null: {0}", "controllerContext.HttpContext.Session"));

            var queue = (Queue)controllerContext.HttpContext.Session[CartSessionKey];

            if (queue == null)
            {
                queue = new Queue();
                controllerContext.HttpContext.Session[CartSessionKey] = queue;
            }

            return queue;
        }

        #endregion
    }
}