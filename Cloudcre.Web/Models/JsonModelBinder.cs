using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Mvc;

namespace Cloudcre.Web.Models
{
    public class JsonModelBinder : IModelBinder
    {
        #region IModelBinder Members

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");

            if (bindingContext == null)
                throw new ArgumentNullException("bindingContext");

            var serializer = new DataContractJsonSerializer(bindingContext.ModelType);

            using (var stream = controllerContext.HttpContext.Request.InputStream)
            {
                stream.Seek(0, SeekOrigin.Begin);
                return serializer.ReadObject(stream);
            }
        }

        #endregion
    }
}