interface Props {}
import { formatLargeMonetaryNumber } from "../../Helpers/formatNumbers";
import { useOutletContext } from "react-router-dom";
import { useEffect, useState } from "react";
import { fetchCompanyProfile } from "../../API/API-FMP";

import IndicateLoading from "../Controls/Loaders/indicateLoading";
import { FaRegAddressBook } from "react-icons/fa6";
import KpiTile from "./Controls/KpiTile";
import { CompanyProfile } from "../../API/company";
import CompFinder from "./Controls/CompFinderList";

const ProfileOverview = ({}: Props) => {
  const ticker = useOutletContext<string>();
  const [getCompanyProfile, setCompanyProfile] = useState<CompanyProfile>();
  const [isLoading, setLoading] = useState(false);

  useEffect(() => {
    const getCompanyProfileInit = async () => {
      if (!isLoading) {
        setLoading(true);
        await fetchCompanyProfile(ticker!)
          .then((result) => {
            setCompanyProfile(result?.data[0]);
          })
          .finally(() => setLoading(false));
      }
    };
    getCompanyProfileInit();
  }, []);

  return (
    <>
      <FaRegAddressBook className="mr-4" /> <h1>Company Profile ({ticker})</h1>
      {getCompanyProfile ? (
        <>
          <KpiTile
            title={getCompanyProfile.companyName}
            metric={getCompanyProfile.symbol}
          />
          <KpiTile
            title="Price"
            metric={formatLargeMonetaryNumber(getCompanyProfile.price)}
          />
          <KpiTile title="Sector" metric={getCompanyProfile.sector} />
          <KpiTile
            title="DCF"
            metric={formatLargeMonetaryNumber(getCompanyProfile.dcf)}
          />

          <CompFinder ticker={getCompanyProfile.symbol} />

          <p className="bg-white shadow rounded p-3 mt-1 m-4">
            {getCompanyProfile.description}
          </p>
        </>
      ) : (
        <>
          <IndicateLoading type="tiles" cols={4} />
          <IndicateLoading type="tiles" cols={7} />
          <IndicateLoading type="default" />
        </>
      )}
    </>
  );
};

export default ProfileOverview;
