﻿using Autofac;
using Nancy.Bootstrappers.Autofac;
using Nancy.Conventions;

namespace Howatworks.Matrix.Site
{
    public class MatrixBootstrapper : AutofacNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            existingContainer.Update(builder =>
            {

            });
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory(@"Content"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory(@"Scripts"));

            // Configure overriding conventions before the default ones, otherwise routing will trigger first
            base.ConfigureConventions(conventions);
        }
    }
}