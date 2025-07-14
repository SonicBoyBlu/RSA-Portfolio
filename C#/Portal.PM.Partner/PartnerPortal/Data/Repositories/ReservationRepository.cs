using System;
using System.Collections.Generic;
using System.Linq;
using Acme.Models;
using Dapper;
using Dapper.FastCrud;

namespace Acme.Data.Repositories
{
    public class ReservationRepository : BaseRepository
    {
        protected override string ConnectionString
        {
            get { return GlobalSettings.DateStores.AcmePortal;}
        }

        public List<Reservation> GetReservations()
        {
            return WithConnection<List<Reservation>>(connection =>
            {
                string query = "SELECT * FROM [Reservation] WHERE 1 = 1"; 
                query += " AND DateStart > @sqlDate";

                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("sqlDate", DateTime.Now.AddDays(-30));

                return (connection.Query<Reservation>(query, dynamicParameters)).ToList();

            });
        }

        public List<Reservation> GetReservationsMissingTypes()
        {
            return WithConnection<List<Reservation>>(connection =>
            {
                string query = "SELECT * FROM [Reservation] WHERE 1 = 1";
                query += " AND ReservationType IS NULL";
                return (connection.Query<Reservation>(query)).ToList();
            });
        }

        public List<Reservation> GetReservationsMissingCreatedAt()
        {
            return WithConnection<List<Reservation>>(connection =>
            {
                string query = "SELECT * FROM [Reservation] WHERE 1 = 1";
                query += " AND ReservationCreatedAt IS NULL";
                return (connection.Query<Reservation>(query)).ToList();
            });
        }

        public List<Reservation> GetReservations(Models.ExternalApplication.Escapia.Post.Data formData)
        {
            // status { all, arriving, inhome, departed }
            string formTask = formData.custom.task;
            string search = "%" + formData.custom.searchTerm.ToStringOrDefault().Trim().ToLower() + "%";
            string sqlStatus = formTask;
            int pagesize = formData.pageSize.ToInt32OrDefault();

            if (formTask == "all") sqlStatus = "departed";
            
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("sqlStatus", sqlStatus.ToStringOrDefault().Trim().ToLower());
            dynamicParameters.Add("search", search.ToStringOrDefault().Trim().ToLower());
            dynamicParameters.Add("pagesize", pagesize.ToInt32OrDefault());

            dynamicParameters.Add("startDate", formData.specification.stayDateRangeSearchSpecification.dateRange.startDate.ToDateTimeOrDefault());
            dynamicParameters.Add("endDate", formData.specification.stayDateRangeSearchSpecification.dateRange.endDate.ToDateTimeOrDefault());

            return WithConnection<List<Reservation>>(connection =>
            {
                string query = "SELECT top (@pagesize) * FROM [Reservation] WHERE 1 = 1";

                query += " AND DateStart >= @startDate";
                query += " AND DateStart < @endDate";

                if (formData.custom.searchTerm.IsNullOrEmpty())
                {
                    query += " AND Status = @sqlStatus";
                }
                else
                {
                    query += " AND (ReservationNumber LIKE @search";
                    query += " OR FirstName LIKE @search";
                    query += " OR LastName LIKE @search)";
                }

                
                if (formTask == "all")
                {
                    query += " Order by ReservationNumber";
                }
                else
                {
                    query += " Order by DateStart";
                }

                return (connection.Query<Reservation>(query, dynamicParameters)).ToList();

            });

        }

        public Reservation GetReservation(string reservationNumber)
        {

            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("reservationNumber", reservationNumber);

            return WithConnection<Reservation>(connection =>
            {
                string query = "SELECT TOP 1 * FROM [Reservation] WHERE reservationNumber = @reservationNumber";
                return (connection.Query<Reservation>(query, dynamicParameters)).FirstOrDefault();
            });
        }

        public Reservation GetReservation(int id)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("ID", id);

            return WithConnection<Reservation>(connection => (connection.Get<Reservation>(new Reservation { ID = id })));
        }

        public int GetReservationId(string reservationNumber)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("reservationNumber", reservationNumber);

            return WithConnection<int>(connection =>
            {
                string query = "SELECT TOP 1 ID FROM Reservation WHERE reservationNumber = @reservationNumber";
                return (connection.Query<int>(query, dynamicParameters)).FirstOrDefault();
            });
        }

        public Reservation Add(Reservation data)
        {
            return WithConnection<Reservation>(connection =>
            {
                connection.Insert(data);
                return data;
            });

        }

        public Reservation Update(Reservation data)
        {
            return WithConnection<Reservation>(connection =>
            {
                var isSuccess = connection.Update(data);
                return data;
            });

        }

        public DateTime GetAPILastRefreshedOn()
        {
            return WithConnection<DateTime>(connection =>
            {
                string query = "SELECT TOP 1 Modified FROM dbo.ReservationEscapiaJson ORDER BY Modified DESC";
                return (connection.Query<DateTime>(query)).FirstOrDefault();
            });
        }
    }
    public class ReservationCategoryRepository : BaseRepository
    {
        protected override string ConnectionString
        {
            get { return GlobalSettings.DateStores.AcmePortal; }
        }
        public long Add(ReservationCategory data)
        {
            return WithConnection<long>(connection =>
            {
                connection.Insert(data);
                return data.ID;

            });

        }

        public ReservationCategory Update(ReservationCategory data)
        {
            return WithConnection<ReservationCategory>(connection =>
            {
                var isSuccess = connection.Update(data);
                return data;
            });

        }

        internal ReservationCategory GetByNativePMSID(string nativePMSID)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("nativePMSID", nativePMSID);

            return WithConnection<ReservationCategory>(connection =>
            {
                string query = "SELECT TOP 1 * FROM [ReservationCategory] WHERE NativePMSID = @nativePMSID";
                return (connection.Query<ReservationCategory>(query, dynamicParameters)).FirstOrDefault();
            });
        }

        internal List<ReservationCategory> GetAll()
        {
            return WithConnection<List<ReservationCategory>>(
                connection => (
                    connection.Find<ReservationCategory>().ToList()
                    )
                );
        }
    }
    public class ReservationSourceRepository : BaseRepository
    {
        protected override string ConnectionString
        {
            get { return GlobalSettings.DateStores.AcmePortal; }
        }
        public ReservationSource Add(ReservationSource data)
        {
            return WithConnection<ReservationSource>(connection =>
            {
                connection.Insert(data);
                return data;
            });

        }

        internal List<ReservationSource> GetByCategoryID(int ReservationCategoryID)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("reservationCategoryID", ReservationCategoryID);
            
            return WithConnection(connection =>
            {
                string query = "SELECT * FROM [ReservationSource] WHERE ReservationCategoryID = reservationCategoryID";
                return (connection.Query<ReservationSource>(query, dynamicParameters)).ToList();
            });
        }

        internal ReservationSource GetByCategoryIDNativePMSID(int ReservationCategoryID, string nativePMSID)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("reservationCategoryID", ReservationCategoryID);
            dynamicParameters.Add("nativePMSID", nativePMSID);

            return WithConnection(connection =>
            {
                string query = "SELECT * FROM [ReservationSource] WHERE ReservationCategoryID = reservationCategoryID AND nativePMSID = @nativePMSID";
                return (connection.Query<ReservationSource>(query, dynamicParameters)).FirstOrDefault();
            });
        }

        internal List<ReservationSource> GetByReservationCategoryID(int id)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("id", id);

            return WithConnection(connection =>
            {
                string query = "SELECT * FROM [ReservationSource] WHERE ReservationCategoryID = @id";
                return (connection.Query<ReservationSource>(query, dynamicParameters)).ToList();
            });
        }

        internal ReservationSource GetByNativePMSID(string nativePMSID)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("nativePMSID", nativePMSID);

            return WithConnection<ReservationSource>(connection =>
            {
                string query = "SELECT TOP 1 * FROM [ReservationSource] WHERE NativePMSID = @nativePMSID";
                return (connection.Query<ReservationSource>(query, dynamicParameters)).FirstOrDefault();
            });
        }

        public ReservationSource Update(ReservationSource data)
        {
            return WithConnection<ReservationSource>(connection =>
            {
                var isSuccess = connection.Update(data);
                return data;
            });
        }
    }
    public class ReservationEscapiaJson : BaseRepository
    {
        protected override string ConnectionString
        {
            get { return GlobalSettings.DateStores.AcmePortal; }
        }
        internal Acme.Models.ReservationEscapiaJson Get()
        {
          
            return WithConnection(connection =>
            {
                string query = "SELECT TOP 1 * FROM [ReservationEscapiaJson] Order by Modified desc";
                return (connection.Query<Acme.Models.ReservationEscapiaJson>(query)).FirstOrDefault();
            });
        }
        public ReservationEscapiaJson Update(ReservationEscapiaJson data)
        {
            return WithConnection<ReservationEscapiaJson>(connection =>
            {
                var isSuccess = connection.Update(data);
                return data;
            });

        }
        public Models.ReservationEscapiaJson Insert(string jsonraw, string jsonmodel, string duration, int count)
        {
            var model = new Models.ReservationEscapiaJson();
            model.JsonRaw = jsonraw;
            model.JsonModel = jsonmodel;
            model.Duration = duration;
            model.TotalReservations = count;
            model.Modified = DateTime.Now;

            return WithConnection<Models.ReservationEscapiaJson>(connection =>
            {
                //var isSuccess = connection.Insert(model);
                connection.Insert(model);
                return model;
            });

        }
    }
    public class ReservationTypeRepository : BaseRepository
    {
        protected override string ConnectionString
        {
            get { return GlobalSettings.DateStores.AcmePortal; }
        }

        internal Acme.Models.ReservationType GetByNativePMSID(string nativePMSID)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("nativePMSID", nativePMSID);

            return WithConnection<ReservationType>(connection =>
            {
                string query = "SELECT TOP 1 * FROM [ReservationType] WHERE NativePMSID = @nativePMSID";
                return (connection.Query<ReservationType>(query, dynamicParameters)).FirstOrDefault();
            });
        }

        public long Add(ReservationType data)
        {
            return WithConnection<long>(connection =>
            {
                connection.Insert(data);
                return data.ID;

            });

        }

        internal List<ReservationType> GetAll()
        {
            return WithConnection<List<ReservationType>>(
                connection => (
                    connection.Find<ReservationType>().ToList()
                )
            );
        }
    }
}