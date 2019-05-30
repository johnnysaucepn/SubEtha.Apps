﻿using Autofac;
using Howatworks.Matrix.Core.Entities;
using Howatworks.Matrix.Core.Repositories;

namespace Howatworks.Matrix.Service.Persistence
{
    public class RepositoryModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterType<LocationEntityRepository>().As<ILocationEntityRepository>().SingleInstance();
            builder.RegisterType<SessionEntityRepository>().As<ISessionEntityRepository>().SingleInstance();
            builder.RegisterType<StateEntityRepository<ShipStateEntity>>().As<IStateEntityRepository<ShipStateEntity>>().SingleInstance();
            builder.RegisterType<GroupRepository>().As<IGroupRepository>().SingleInstance();
        }
    }
}