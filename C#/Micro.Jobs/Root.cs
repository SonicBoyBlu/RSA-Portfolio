namespace Root
{
    public class AppSettings
    {
        public static DateTime DateReloaded
        {
            get
            {
                if (_dateReloaded == DateTime.MinValue)
                    _dateReloaded = DateTime.Now;
                return _dateReloaded;
            }
        }
        private static DateTime _dateReloaded;
    }
}
