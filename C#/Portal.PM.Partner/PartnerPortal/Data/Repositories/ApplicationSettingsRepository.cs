namespace Acme.Data.Repositories
{
    public class ApplicationSettingsRepository : BaseRepository
    {
        protected override string ConnectionString =>  GlobalSettings.DateStores.AcmePortal;
    }


}