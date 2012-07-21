  ___| | ___  _   _  __| | ___ _ __ ___ 
 / __| |/ _ \| | | |/ _` |/ __| '__/ _ \
| (__| | (_) | |_| | (_| | (__| | |  __/
 \___|_|\___/ \__,_|\__,_|\___|_|  \___|

  See https://github.com/jimlyndon/cloudcre/wiki/Cloud-CRE
  See www.cloudcre.com

-----------------------------------------

<h2>Cloud CRE</h2>
<p>
A full featured commercial real estate appraisal reporting and comparable search 
application using asp.net mvc/razor, lucene backed nHibernate search for indexing, 
jquery, knockout.js for data binding, twitter's boostrap, google's mapping api, 
OpenXml for dynamic excel report building, and a several of other open source tools
and libraries.
</p>
<p>
- to run:
-- clone the repo, build the solution (make sure nuget pulls the dependencies), deploy the schema (mysql)
-- to enable user login see .asax and uncomment the account controller
-- to enable creation/edit of properties uncomment routes in propertybase controller
-- also, Cloudcre.Utilities contain test data loading and lucene indexing utility console apps
</p>