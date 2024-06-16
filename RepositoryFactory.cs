namespace Ventas
{
    public class RepositoryFactory
    {
        public static iRepository CreateRepository()
        {
            var Context = new ventasDBEntities1();
            Context.Configuration.ProxyCreationEnabled = false;
            return new EFRepository(Context);
        }

    }
}
