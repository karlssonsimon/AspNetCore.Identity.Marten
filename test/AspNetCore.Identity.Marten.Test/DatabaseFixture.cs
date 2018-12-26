using System;
using Marten;

namespace AspNetCore.Identity.Marten.Test
{
    public class DatabaseFixture : IDisposable
    {
        public readonly IDocumentStore DocumentStore;

        public DatabaseFixture()
        {
            DocumentStore = global::Marten.DocumentStore.For(_ =>
            {
                _.Connection("host=localhost;database=marten_test;password=marten_pass;username=marten_user");
                _.AutoCreateSchemaObjects = AutoCreate.All;
                _.PLV8Enabled = false;
            });

            DocumentStore.Advanced.Clean.DeleteAllDocuments();
        }

        public void Dispose()
        {
            DocumentStore.Dispose();
        }
    }
}