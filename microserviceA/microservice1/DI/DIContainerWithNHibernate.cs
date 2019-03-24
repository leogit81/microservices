using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Reflection;

namespace microservice1.DI
{
    public class DIContainerWithNHibernate : NinjectModule
    {
        public override void Load()
        {
            var kernel = this.Kernel;

            kernel.Bind<ISessionFactory>().ToMethod(
                (context) =>
                {
                    var cfg = new Configuration();
                    var modelMapper = new ModelMapper();
                    modelMapper.AddMappings(Assembly.GetExecutingAssembly().GetTypes());
                    cfg.AddMapping(modelMapper.CompileMappingForAllExplicitlyAddedEntities());
                    cfg.CurrentSessionContext<WebSessionContext>();
                    return Fluently.Configure(cfg)
                          .Database(MsSqlConfiguration.MsSql2008
                                      .ConnectionString(c => c
                                        .FromConnectionStringWithKey("mssqlConn"))
                                        .ShowSql()
                                    )
                          .BuildSessionFactory();
                }
            ).InSingletonScope();

            kernel.Bind<ISession>().ToMethod(
                (context) =>
                {
                    var factory = context.Kernel.Get<ISessionFactory>();
                    var session = factory.OpenSession();
                    CurrentSessionContext.Bind(session);
                    return session;
                }
            ).InRequestScope()
            .OnActivation(session =>
            {
                session.BeginTransaction();
            })
             .OnDeactivation(
                (session) =>
                {
                    if (session.Transaction != null
                        && session.Transaction.IsActive
                        && !session.Transaction.WasCommitted
                        && !session.Transaction.WasRolledBack)
                    {
                        session.Transaction.Rollback();
                    }
                    else
                    {
                        session.Transaction.Commit();
                    }
                    session.Close();
                    session.Dispose();
                }
            );
        }
    }
}