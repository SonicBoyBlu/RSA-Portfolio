import { useEffect, useState } from "react";
import { CompanyCompData } from "../../../API/company";
import { fetchCompanyStockComps } from "../../../API/API-FMP";
import CompFinderItem from "./CompFinderItem";
import { v4 as uuid } from "uuid";

type Props = {
  ticker: string;
};

const CompFinder = ({ ticker }: Props) => {
  const [getComps, setComps] = useState<CompanyCompData>();
  const [isLoading, setLoading] = useState(false);

  useEffect(() => {
    const getCompsInit = async () => {
      if (!isLoading) {
        setLoading(true);
        console.info("Fetching Comp Finder List...");
        await fetchCompanyStockComps(ticker)
          .then((result) => {
            console.log(result);
            setComps(result![0]);
          })
          .finally(() => setLoading(false));
      }
    };
    getCompsInit();
  }, [ticker]);
  return (
    <div key={uuid()} className="inline-flex rounded-md shadow m-4">
      Comps:
      {getComps?.peersList.map((ticker) => {
        return <CompFinderItem ticker={ticker} />;
      })}
    </div>
  );
};

export default CompFinder;
